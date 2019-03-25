using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyCAD
{
    class DouglasPeuker
    {
        public static List<Basepoint> after = new List<Basepoint>();

        public static float Distance(float fx, float fy, float lx, float ly)
        {//求点到点的距离
            return (float)Math.Sqrt(Math.Pow(fy - ly, 2) + Math.Pow(fx - lx, 2));
        }

        public static float Distance(float x, float y, float fx, float fy, float lx, float ly)
        {//求点到直线的距离
            float distance;
            if (ly - fy == 0)
            {//斜率不存在
                distance = Math.Abs(y - fy);
                return distance;
            }
            else
            {//斜率存在
                float[] kb = new float[2];
                kb[0] = (lx - fx) / (ly - fy);
                kb[1] = fx - kb[0] * fy;
                distance = (Math.Abs(kb[0] * y - x + kb[1])) / (float)Math.Sqrt(kb[0] * kb[0] + 1);
                return distance;
            }
        }

        public static void DP(int num1, int num2, float d, List<Basepoint> before)
        {//DP算法
            //定义拥有最大距离值的点的编号
            int max = 0;

            //假设第二个点为最大距离点
            float maxx = Distance(before[num1 + 1].Map_X, before[num1 + 1].Map_Y, before[num1].Map_X, before[num1].Map_Y, before[num2 - 1].Map_X, before[num2 - 1].Map_Y);

            //从第二个点遍历到最后一个点前方的点
            for (int i = num1 + 1; i < num2 - 1; i++)
            {
                float temp = Distance(before[i].Map_X, before[i].Map_Y, before[num1].Map_X, before[num1].Map_Y, before[num2 - 1].Map_X, before[num2 - 1].Map_Y);      
                if (temp > d && temp >= maxx)
                {//找出拥有最大距离的点的编号
                    max = i;
                    maxx = temp;
                }
            }

            //若不存在最大距离点，则只将首尾点存入after，结束这一次的道格拉斯运算
            if (max == 0)
            {
                if (!after.Contains(before[num2 - 1]))
                {
                    after.Add(before[num2 - 1]);
                    return;
                }
            }

            //如果第二个点是最大距离点，则以此点和尾点作为参数进行道格拉斯运算
            else if (num1 + 1 == max && num2 - 2 != max)
            {
                after.Add(before[max]);
                DP(max, num2, d, before);
            }

            //如果倒数第二个点是最大距离点，则以首点和此点作为参数进行道格拉斯运算,并在此次运算结束时将尾点加入
            else if (num2 - 2 == max && num1 + 1 != max)
            {

                DP(num1, max + 1, d, before);
                after.Add(before[max + 1]);
            }

            //如果首点尾点夹住最大距离点，则将最大距离点和尾点存入after
            else if (num1 + 1 == max && num2 - 2 == max)
            {
                after.Add(before[max]);
                after.Add(before[max + 1]);
                return;
            }

            //如果不满足上述条件，以最大值为分界点，分两部分进行道格拉斯运算
            else
            {
                DP(num1, max + 1, d, before);
                DP(max, num2, d, before);
            }
        }
    }
}