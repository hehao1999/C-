using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyCAD
{
    class DouglasPeuker
    {
        public static List<Basepoint> after = new List<Basepoint>();

        public static float[] Kb(float fx, float fy, float lx, float ly)
        {//求斜率和截距
            float[] kb = new float[2];
            kb[0] = (lx - fx) / (ly - fy);
            kb[1] = fx - kb[0] * fy;
            return kb;
        }

        public static float Distance(float x, float y, float[] kb)
        {//求点到直线的距离
            float distance = (Math.Abs(kb[0] * y - x + kb[1])) / (float)Math.Sqrt(kb[0] * kb[0] + 1);
            return distance;
        }

        public static void DP(int num1, int num2, float d, List<Basepoint> before)
        {//DP算法
            int max = 0;//定义拥有最大距离值的点的编号

            float[] kb = new float[2];
            kb = Kb(before[num1].Map_X, before[num1].Map_Y, before[num2 - 1].Map_X, before[num2 - 1].Map_Y);
            float maxx = Distance(before[num1 + 1].Map_X, before[num1 + 1].Map_Y, kb);//假设第二个点为最大距离点
            for (int i = num1 + 1; i < num2 - 1; i++)//从第二个点遍历到最后一个点前方的点
            {
                if (Distance(before[i].Map_X, before[i].Map_Y, kb) > d && Distance(before[i].Map_X, before[i].Map_Y, kb) >= maxx)//找出拥有最大距离的点
                {
                    max = i;
                    maxx = Distance(before[i].Map_X, before[i].Map_Y, kb);
                }
            }
            if (max == 0)//若不存在最大距离点，则只将首尾点存入after，结束这一次的道格拉斯运算
            {
                if (!after.Contains(before[num2 - 1]))
                {
                    after.Add(before[num2 - 1]);
                    return;
                }
            }
            else if (num1 + 1 == max && num2 - 2 != max)//如果第二个点是最大距离点，则以此点和尾点作为参数进行道格拉斯运算
            {
                after.Add(before[max]);
                DP(max, num2, d, before);
            }
            else if (num2 - 2 == max && num1 + 1 != max)//如果倒数第二个点是最大距离点，则以首点和此点作为参数进行道格拉斯运算,并在此次运算结束时将尾点加入
            {

                DP(num1, max + 1, d, before);
                after.Add(before[max + 1]);
            }
            else if (num1 + 1 == max && num2 - 2 == max)//如果首点尾点夹住最大距离点，则将最大距离点和尾点存入after
            {
                after.Add(before[max]);
                after.Add(before[max + 1]);
                return;
            }
            else //如果不满足上述条件，以最大值为分界点，分两部分进行道格拉斯运算
            {

                DP(num1, max + 1, d, before);
                DP(max, num2, d, before);
            }
        }
    }
}