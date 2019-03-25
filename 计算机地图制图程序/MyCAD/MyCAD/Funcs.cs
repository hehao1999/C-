using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace MyCAD
{
    static class Funcs
    {
        public static void Maximum()
        {//求最值
            MyGlo.max_x = 0;
            MyGlo.max_y = 0;
            MyGlo.min_x = 99999999999999;
            MyGlo.min_y = 99999999999999;
            foreach (var point in MyGlo.rplist)
            {
                MyGlo.max_x = point.Map_X > MyGlo.max_x ? point.Map_X : MyGlo.max_x;
                MyGlo.max_y = point.Map_Y > MyGlo.max_y ? point.Map_Y : MyGlo.max_y;
                MyGlo.min_x = point.Map_X < MyGlo.min_x ? point.Map_X : MyGlo.min_x;
                MyGlo.min_y = point.Map_Y < MyGlo.min_y ? point.Map_Y : MyGlo.min_y;
            }
            foreach (var point in MyGlo.nplist)
            {
                MyGlo.max_x = point.Map_X > MyGlo.max_x ? point.Map_X : MyGlo.max_x;
                MyGlo.max_y = point.Map_Y > MyGlo.max_y ? point.Map_Y : MyGlo.max_y;
                MyGlo.min_x = point.Map_X < MyGlo.min_x ? point.Map_X : MyGlo.min_x;
                MyGlo.min_y = point.Map_Y < MyGlo.min_y ? point.Map_Y : MyGlo.min_y;
            }
            foreach (var line in MyGlo.llist)
            {
                foreach (var point in line.CoorList)
                {
                    MyGlo.max_x = point.Map_X > MyGlo.max_x ? point.Map_X : MyGlo.max_x;
                    MyGlo.max_y = point.Map_Y > MyGlo.max_y ? point.Map_Y : MyGlo.max_y;
                    MyGlo.min_x = point.Map_X < MyGlo.min_x ? point.Map_X : MyGlo.min_x;
                    MyGlo.min_y = point.Map_Y < MyGlo.min_y ? point.Map_Y : MyGlo.min_y;
                }
            }
        }//Maximum方法

        public static void Scale(int ptb_width, int ptb_height)
        {//求比例尺
            int width = (int)(ptb_width - ptb_width * 0.05);
            int height = (int)(ptb_height - ptb_width * 0.05);
            MyGlo.scale = Math.Min(Math.Min(width / (MyGlo.max_y - MyGlo.min_y),//求比例尺
                  height / (MyGlo.max_x - MyGlo.min_x)), MyGlo.scale);
        }//Scale求比例尺

        public static void Compress(float d)
        {
            foreach (var line in MyGlo.llist)
            {

            }
        }//Compress数据压缩

        public static Point CoorTransform(float x, float y, int width, int height)
        {//坐标转换
            float temp;
            float Screen_X, Screen_Y;

            Screen_Y = (y - MyGlo.min_y) * MyGlo.scale; //减左下角坐标
            Screen_X = (x - MyGlo.min_x) * MyGlo.scale;

            temp = Screen_X;//交换方向并使得图像居中
            Screen_X = (float)(Screen_Y + 0.5 * (width - MyGlo.scale * (MyGlo.max_y - MyGlo.min_y)));
            Screen_Y = (float)((height - temp) - 0.5 * (height - MyGlo.scale * (MyGlo.max_x - MyGlo.min_x)));

            Point pt = new Point((int)Screen_X, (int)Screen_Y);
            return pt;//返回屏幕坐标
        }//transform方法，坐标转换

        public static float[] ScrToGeo(int X, int Y, int width, int height)
        {//屏幕坐标转地理坐标
            float[] geo = new float[2];
            geo[0] = (float)(height - Y - 0.5 * (height - MyGlo.scale * (MyGlo.max_x - MyGlo.min_x))) / MyGlo.scale + MyGlo.min_x;
            geo[1] = (float)(X - 0.5 * (width - MyGlo.scale * (MyGlo.max_y - MyGlo.min_y))) / MyGlo.scale + MyGlo.min_y;
            return geo;
        }//ScrToGeo，屏幕坐标转地理坐标

        public static uint DataType(string hdr)
        {//判断数据类型
            uint rt;
            char[] id = new char[] { hdr[0], hdr[1] };
            if (id[0] == 'p') //是否为点
                rt = id[1] == 'r' ? (uint)1 : (uint)2; // 1实体点，2注记点
            else
            {
                if (id[1] == '1')
                    rt = 3;
                else if (id[1] == '2')
                    rt = 4;
                else if (id[1] == '3')
                    rt = 5;
                else
                    rt = 6;
            }
            return rt;
        }//DataType方法，判断导入数据类型

        public static void Importfile(StreamReader f, Graphics g, int Height, int Width)
        {
            while (f.Peek() > -1) //判断文件有没有读取完
            {
                string tmp = f.ReadLine();//读取第一行信息
                switch (Funcs.DataType(tmp)) //根据数据类型绘制
                {
                    case 1://为实体点
                        {
                            string temp = f.ReadLine(); //再读一行字符串，判断是否为空，不为空就录入
                            while (temp != null && temp != ",,,")
                            {//实例化实体点并加入实体点集
                                Realpoint realpoint = new Realpoint(temp);
                                MyGlo.rplist.Add(realpoint);
                                temp = f.ReadLine();
                            }//while
                        }//case 1
                        break;

                    case 2://为注记点
                        {
                            string temp = f.ReadLine();
                            while (temp != null && temp != ",,,")
                            {
                                Notepoint notepoint = new Notepoint(temp);
                                MyGlo.nplist.Add(notepoint);
                                temp = f.ReadLine();
                            }
                        }//case 2
                        break;

                    case 3: //为常规线
                        {
                            string temp = f.ReadLine();
                            List<Basepoint> bt_temp = new List<Basepoint>();
                            while (temp != null && temp != ",,,")
                            {
                                Linkpoint linkpoint = new Linkpoint(temp);
                                bt_temp.Add(linkpoint);
                                temp = f.ReadLine();
                            }
                            Lineshape lineshape = new Lineshape(bt_temp, tmp);//实例化线对象
                            MyGlo.llist.Add(lineshape);//加入列表
                        }//case3
                        break;

                    case 4: //为弧线
                        {
                            string temp = f.ReadLine();
                            List<Basepoint> bt_temp = new List<Basepoint>();
                            while (temp != null && temp != ",,,")
                            {
                                Linkpoint linkpoint = new Linkpoint(temp);
                                bt_temp.Add(linkpoint);
                                temp = f.ReadLine();
                            }
                            Lineshape lineshape = new Lineshape(bt_temp, tmp);
                            MyGlo.llist.Add(lineshape);
                        }
                        break; //case4
                    case 5:
                        {
                            string temp = f.ReadLine();
                            List<Basepoint> bt_temp = new List<Basepoint>();
                            while (temp != null && temp != ",,,")
                            {
                                Linkpoint linkpoint = new Linkpoint(temp);
                                bt_temp.Add(linkpoint);
                                temp = f.ReadLine();
                            }
                            Lineshape lineshape = new Lineshape(bt_temp, tmp);
                            MyGlo.llist.Add(lineshape);
                        }
                        break;//case5

                    case 6:
                        {
                            string temp = f.ReadLine();
                            List<Basepoint> bt_temp = new List<Basepoint>();
                            while (temp != null && temp != ",,,")
                            {
                                Linkpoint linkpoint = new Linkpoint(temp);
                                bt_temp.Add(linkpoint);
                                temp = f.ReadLine();
                            }
                            Lineshape lineshape = new Lineshape(bt_temp, tmp);
                            MyGlo.llist.Add(lineshape);
                        }
                        break;//case6
                }//switch   Funcs.Update(g, Width, Height);
            }//while
        } //importfile，导入roi

        public static void Draw(Graphics g, int x, int y, int Width, int Height, Realpoint rpt)
        {//绘制一个实体点图像
            Pen point_pen = new Pen(rpt.color_out, 1);  //画笔
            Brush point_brush = new SolidBrush(rpt.color_in);  //画刷
            g.DrawEllipse(point_pen, x - (rpt.Size - 1) / 2, y - (rpt.Size - 1) / 2, rpt.Size, rpt.Size);
            g.FillEllipse(point_brush, x - (rpt.Size - 1) / 2, y - (rpt.Size - 1) / 2, rpt.Size, rpt.Size); //填充椭圆
        }//Draw方法——实体点

        public static void Draw(Graphics g, int x, int y, int Width, int Height, Notepoint npt)
        {//绘制一个注记点图像
            Pen point_pen = new Pen(npt.color_out, 1);  //画笔
            Brush point_brush = new SolidBrush(npt.color_in);  //画刷
            g.DrawEllipse(point_pen, x - (npt.Size - 1) / 2, y - (npt.Size - 1) / 2, npt.Size, npt.Size);
            g.FillEllipse(point_brush, x - (npt.Size - 1) / 2, y - (npt.Size - 1) / 2, npt.Size, npt.Size); //填充椭圆
        }//Draw方法——注记点

        public static void Draw(Graphics g, int x, int y, int Width, int Height, Linkpoint lpt)
        {//绘制一个关联点
            Pen point_pen = new Pen(lpt.color_out, 1);  //画笔
            Brush point_brush = new SolidBrush(lpt.color_in);  //画刷
            g.DrawEllipse(point_pen, x - (lpt.Size - 1) / 2, y - (lpt.Size - 1) / 2, lpt.Size, lpt.Size);
            g.FillEllipse(point_brush, x - (lpt.Size - 1) / 2, y - (lpt.Size - 1) / 2, lpt.Size, lpt.Size); //填充椭圆
        }//Draw方法——关联点

        public static void Draw(List<Point> ptlist, Graphics g, int size, int i, Lineshape line)
        { //绘制线或面
            Pen line_pen = new Pen(line.color_out, size);
            Brush poly_brush = new SolidBrush(line.color_in);
            switch (i)
            {
                case 1://画直线段
                    g.DrawLines(line_pen, ptlist.ToArray());
                    break;
                case 2://画弧线段
                    g.DrawCurve(line_pen, ptlist.ToArray());
                    break;
                case 3://画直段面
                    {
                        g.DrawPolygon(line_pen, ptlist.ToArray());
                        g.FillPolygon(poly_brush, ptlist.ToArray());
                    }
                    break;
                case 4://画弧线面
                    {
                        g.FillClosedCurve(poly_brush, ptlist.ToArray());
                        g.DrawClosedCurve(line_pen, ptlist.ToArray());
                    }          
                    break;
            }
        }//Draw方法——线段/面

        public static void Update(Graphics g, int Width, int Height)
        {//UPDATE于绘图
            Point pt = new Point();
            foreach (Realpoint rpt in MyGlo.rplist)
            {//实体点
                pt = CoorTransform(rpt.Map_X, rpt.Map_Y, Width, Height);
                Draw(g, pt.X, pt.Y, Width, Height, rpt);
            }

            foreach (Notepoint npt in MyGlo.nplist)
            {//注记点
                pt = CoorTransform(npt.Map_X, npt.Map_Y, Width, Height);
                Draw(g, pt.X, pt.Y, Width, Height, npt);
            }

            foreach (var lineshape in MyGlo.llist)
            {//线面
                List<Point> ptlist = new List<Point>();
                foreach (Linkpoint lpt in lineshape.CoorList)
                {//关联点
                    pt = CoorTransform(lpt.Map_X, lpt.Map_Y, Width, Height);
                    Draw(g, pt.X, pt.Y, Width, Height, lpt);
                    ptlist.Add(pt);
                }
                Draw(ptlist, g, lineshape.LineSize, Convert.ToInt32(lineshape.ID[1]) - 48, lineshape); //这里'1'是49
            }
        }//update方法——重绘

        public static void Update(Graphics g, int Width, int Height, int i = 1)
        {
            Point pt = new Point();
            foreach (Realpoint rpt in MyGlo.rplist)
            {//实体点
                pt = Funcs.CoorTransform(rpt.Map_X, rpt.Map_Y, Width, Height);
                pt.X += MyGlo.mouse_now_position.X - MyGlo.mouse_down_position[0] + MyGlo.offset[0];
                pt.Y += MyGlo.mouse_now_position.Y - MyGlo.mouse_down_position[1] + MyGlo.offset[1];
                Funcs.Draw(g, pt.X, pt.Y, Width, Height, rpt);
            }

            foreach (Notepoint npt in MyGlo.nplist)
            {//注记点
                pt = Funcs.CoorTransform(npt.Map_X, npt.Map_Y, Width, Height);
                pt.X += MyGlo.mouse_now_position.X - MyGlo.mouse_down_position[0] + MyGlo.offset[0];
                pt.Y += MyGlo.mouse_now_position.Y - MyGlo.mouse_down_position[1] + MyGlo.offset[1];
                Funcs.Draw(g, pt.X, pt.Y, Width, Height, npt);
            }

            foreach (var lineshape in MyGlo.llist)
            {//线面
                List<Point> ptlist = new List<Point>();
                foreach (Linkpoint lpt in lineshape.CoorList)
                {//关联点
                    pt = Funcs.CoorTransform(lpt.Map_X, lpt.Map_Y, Width, Height);
                    pt.X += MyGlo.mouse_now_position.X - MyGlo.mouse_down_position[0] + MyGlo.offset[0];
                    pt.Y += MyGlo.mouse_now_position.Y - MyGlo.mouse_down_position[1] + MyGlo.offset[1];
                    Funcs.Draw(g, pt.X, pt.Y, Width, Height, lpt);
                    ptlist.Add(pt);
                }
                Funcs.Draw(ptlist, g, lineshape.LineSize, Convert.ToInt32(lineshape.ID[1]) - 48, lineshape); //这里'1'是49
            }
        }//if

        public static void MouseClear()
        {
            MyGlo.mouse_down_position[0] = 0;
            MyGlo.mouse_down_position[1] = 0;
            MyGlo.offset[0] = 0;
            MyGlo.offset[1] = 0;
            MyGlo.offset_tmp[0] = 0;
            MyGlo.offset_tmp[1] = 0;
        }

        public static void Clear()
        {
            MyGlo.llist.Clear();
            MyGlo.nplist.Clear();
            MyGlo.rplist.Clear();
            MyGlo.max_x = 0;
            MyGlo.max_y = 0;
            MyGlo.min_x = 999999999999999999;
            MyGlo.min_y = 999999999999999999;
            MyGlo.scale = 999999999999999999;
            MyGlo.np_num = 0;
            MyGlo.rp_num = 0;
            MyGlo.l_num = 0;
            MouseClear();
        }//clear方法清空
    }
}

