using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyCAD
{
    static class Funcs
    {
        public static double Fwj(Basepoint A, Basepoint B)
        {

            double AB;
            if (B.Map_X - A.Map_X < 0)
            {//看B是否在A的哪个方向，若B在A的南方
                AB = Math.Atan((B.Map_Y - A.Map_Y) / (B.Map_X - A.Map_X)) + Math.PI;
            }
            else if (B.Map_X - A.Map_X > 0)
            {
                if (B.Map_Y - A.Map_Y >= 0)
                {//若B在A的东方
                    AB = Math.Atan((B.Map_Y - A.Map_Y) / (B.Map_X - A.Map_X));
                }
                else
                {//若B在A的西方
                    AB = Math.Atan((B.Map_Y - A.Map_Y) / (B.Map_X - A.Map_X)) + 2 * Math.PI;
                }
            }
            else
            {
                if (B.Map_Y - A.Map_Y > 0)
                {
                    AB = 0.5 * Math.PI;
                }
                else if (B.Map_Y - A.Map_Y > 0)
                {
                    AB = 1.5 * Math.PI;
                }
                else return 999999999999999;
            }
            return AB;
        }//Fwj

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
                }//switch
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
                        g.FillPolygon(poly_brush, ptlist.ToArray());
                        g.DrawPolygon(line_pen, ptlist.ToArray());
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

        public static void Daoxiandian(Graphics g, int x, int y, int Width, int Height)
        {
            Pen point_pen = new Pen(Color.Black, 0.1f);  //画笔
            Brush point_brush = new SolidBrush(Color.Black);  //画刷
            g.DrawEllipse(point_pen, x - 5.5f, y - 5.5f, 11, 11);
            g.FillEllipse(point_brush, x - 1.5f, y - 1.5f, 3, 3); //填充椭圆
        }//Draw方法——导线点

        public static void Jiaotang(Graphics g, int x, int y, int Width, int Height)
        {
            Pen pen = new Pen(Color.Black, 2);  //画笔
            Brush point_brush = new SolidBrush(Color.Black);  //画刷
            g.FillEllipse(point_brush, x - 4.5f, y - 4.5f, 9, 9); //填充椭圆        
            g.DrawLine(pen,x,y-4.5f,x,y-13.5f);
            g.DrawLine(pen, x - 4.5f, y - 9f, x + 4, y - 9f);
        }//Draw方法——教堂

        public static void Update(Graphics g, int Width, int Height)
        {//UPDATE于绘图

            Point pt = new Point();
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
        }//update方法——重绘

        public static void Update(Graphics g, int Width, int Height, int i = 1)
        {
            Point pt = new Point();
            foreach (var lineshape in MyGlo.llist)
            {//线面
                List<Point> ptlist = new List<Point>();
                foreach (Linkpoint lpt in lineshape.CoorList)
                {//关联点
                    pt = Funcs.CoorTransform(lpt.Map_X, lpt.Map_Y, Width, Height);
                    pt.X += (int)(MyGlo.mouse_now_position.X - MyGlo.mouse_down_position[0] + MyGlo.offset[0]);
                    pt.Y += (int)(MyGlo.mouse_now_position.Y - MyGlo.mouse_down_position[1] + MyGlo.offset[1]);
                    Draw(g, pt.X, pt.Y, Width, Height, lpt);
                    ptlist.Add(pt);
                }
                Draw(ptlist, g, lineshape.LineSize, Convert.ToInt32(lineshape.ID[1]) - 48, lineshape); //这里'1'是49
            }

            foreach (Realpoint rpt in MyGlo.rplist)
            {//实体点
                pt = Funcs.CoorTransform(rpt.Map_X, rpt.Map_Y, Width, Height);
                pt.X += (int)(MyGlo.mouse_now_position.X - MyGlo.mouse_down_position[0] + MyGlo.offset[0]);
                pt.Y += (int)(MyGlo.mouse_now_position.Y - MyGlo.mouse_down_position[1] + MyGlo.offset[1]);
                Draw(g, pt.X, pt.Y, Width, Height, rpt);
            }

            foreach (Notepoint npt in MyGlo.nplist)
            {//注记点
                pt = Funcs.CoorTransform(npt.Map_X, npt.Map_Y, Width, Height);
                pt.X += (int)(MyGlo.mouse_now_position.X - MyGlo.mouse_down_position[0] + MyGlo.offset[0]);
                pt.Y += (int)(MyGlo.mouse_now_position.Y - MyGlo.mouse_down_position[1] + MyGlo.offset[1]);
                Draw(g, pt.X, pt.Y, Width, Height, npt);
            }
        }//if

        public static void CoorDisplay(object o, object b, MouseEventArgs e)
        {
            float[] stg = new float[2];
            //鼠标是否已经按下，并且未弹起
            if (Form1.bool_down == true)
            {
                //如果处于移动激活状态
                if (Form1.bool_move == true)
                {
                    //显示按下点的坐标
                    stg = ScrToGeo((int)(MyGlo.mouse_down_position[0] - MyGlo.offset[0]), (int)(MyGlo.mouse_down_position[1] - MyGlo.offset[1]), (b as PictureBox).Width, (b as PictureBox).Height);
                    (o as ToolStripStatusLabel).Text = "地理坐标：" + Math.Round(stg[0], 4) + "," + Math.Round(stg[1], 4);

                    //记录目前坐标并使用方法1刷屏
                    MyGlo.mouse_now_position = new Point(e.X, e.Y);
                    MyGlo.update_function = 1;
                    (b as PictureBox).Refresh();
                }//if 
            }
            //鼠标未按下
            else
            {
                //显示坐标
                stg = ScrToGeo((int)(e.X - MyGlo.offset[0]), (int)(e.Y - MyGlo.offset[1]), (b as PictureBox).Width, (b as PictureBox).Height);
                (o as ToolStripStatusLabel).Text = "地理坐标：" + Math.Round(stg[0], 4) + "," + Math.Round(stg[1], 4);
            }
        }

        public static object choice(int x, int y, int width, int height)
        {//是否被选中
            foreach (var rpt in MyGlo.rplist)
            {
                Point temp_p = Funcs.CoorTransform(rpt.Map_X, rpt.Map_Y, width, height);
                if (Math.Pow(x - temp_p.X, 2) + Math.Pow(y - temp_p.Y, 2) <= 25)
                    return rpt;
            }

            foreach (var npt in MyGlo.nplist)
            {
                Point temp_p = CoorTransform(npt.Map_X, npt.Map_Y, width, height);
                if (Math.Pow(x - temp_p.X, 2) + Math.Pow(y - temp_p.Y, 2) <= 25)
                    return npt;
            }

            foreach (var line in MyGlo.llist)
            {
                for (int i = 0; i < line.CoorList.Count - 1; i++)
                {
                    Point p1 = CoorTransform(line.CoorList[i].Map_X, line.CoorList[i].Map_Y, width, height);
                    Point p2 = CoorTransform(line.CoorList[i + 1].Map_X, line.CoorList[i + 1].Map_Y, width, height);
                    if ((x <= p1.X + 6 && x >= p2.X - 6 && y <= p1.Y + 6 && y >= p2.Y - 6) || (x >= p1.X - 6 && x <= p2.X + 6 && y <= p2.Y - 6 && y >= p1.Y + 6) ||
                        (x >= p1.X + 6 && x <= p2.X - 6 && y >= p2.Y + 6 && y <= p1.Y - 6) || (x <= p1.X - 6 && x >= p2.X + 6 && y <= p2.Y - 6 && y >= p1.Y + 6))
                    {
                        float dis = DouglasPeuker.Distance(x, y, p1.X, p1.Y, p2.X, p2.Y);
                        if (dis <= 5)
                            return line;
                    }
                }
                if (line.ID[1] == '3')
                {
                    Point p1 = CoorTransform(line.CoorList[0].Map_X, line.CoorList[0].Map_Y, width, height);
                    Point p2 = CoorTransform(line.CoorList[line.CoorList.Count - 1].Map_X, line.CoorList[line.CoorList.Count - 1].Map_Y, width, height);
                    if ((x <= p1.X + 6 && x >= p2.X - 6 && y <= p1.Y + 6 && y >= p2.Y - 6) || (x >= p1.X - 6 && x <= p2.X + 6 && y <= p2.Y - 6 && y >= p1.Y + 6) ||
                        (x >= p1.X + 6 && x <= p2.X - 6 && y >= p2.Y + 6 && y <= p1.Y - 6) || (x <= p1.X - 6 && x >= p2.X + 6 && y <= p2.Y - 6 && y >= p1.Y + 6))
                    {
                        float dis = DouglasPeuker.Distance(x, y, p1.X, p1.Y, p2.X, p2.Y);
                        if (dis <= 5)
                            return line;
                    }
                }
            }

            return null;
        }

        public static void Del(object obj, object pic)
        {//删除要素
            if (obj is Realpoint)
            {
                MyGlo.rplist.Remove(obj as Realpoint);
                (pic as PictureBox).Refresh();
                MessageBox.Show("删除成功");
            }
            else if (obj is Notepoint)
            {
                MyGlo.nplist.Remove(obj as Notepoint);
                (pic as PictureBox).Refresh();
                MessageBox.Show("删除成功");
            }
            else if (obj is Lineshape)
            {
                MyGlo.llist.Remove(obj as Lineshape);
                (pic as PictureBox).Refresh();
                MessageBox.Show("删除成功");
            }
        }

        public static object Find(float x, float y)
        {//查找要素
            foreach (Realpoint rpt in MyGlo.rplist)
            {
                if (rpt.Map_X == x && rpt.Map_Y == y)
                {
                    return rpt;
                }
            }
            foreach (Notepoint npt in MyGlo.nplist)
            {
                if (npt.Map_X == x && npt.Map_Y == y)
                {
                    return npt;
                }
            }
            foreach (Lineshape line in MyGlo.llist)
            {
                foreach (Linkpoint lpt in line.CoorList)
                {
                    if (lpt.Map_X == x && lpt.Map_Y == y)
                    {
                        return line;
                    }
                }
            }
            return null;
        }

        public static void Rdd(object o, object pictureBox1, Point p2)
        {//点与点关系判断
            object obj = Funcs.choice(p2.X, p2.Y, (pictureBox1 as PictureBox).Width, (pictureBox1 as PictureBox).Height);
            if (o is Realpoint)
            {
                if (obj is Realpoint)
                {
                    float d = DouglasPeuker.Distance((o as Realpoint).Map_X, (o as Realpoint).Map_Y, (obj as Realpoint).Map_X, (obj as Realpoint).Map_Y);
                    if (d == 0)
                    {
                        MessageBox.Show("目标重合");
                    }
                    else
                    {
                        MessageBox.Show("相离，选中目标点的地理距离为：" + d.ToString("F4"), "关系显示：");
                    }
                }
                else if (obj is Notepoint)
                {
                    float d = DouglasPeuker.Distance((o as Realpoint).Map_X, (o as Realpoint).Map_Y, (obj as Notepoint).Map_X, (obj as Notepoint).Map_Y);
                    if (d == 0)
                    {
                        MessageBox.Show("目标重合");
                    }
                    else
                    {
                        MessageBox.Show("相离，选中目标点的地理距离为：" + d.ToString("F4"), "关系显示：");
                    }
                }
                else if (obj is null || obj is Lineshape)
                {
                    MessageBox.Show("只能判断实体点和注记点的关系", "关系显示：");
                }
            }
            else if (o is Notepoint)
            {
                if (obj is Realpoint)
                {
                    float d = DouglasPeuker.Distance((o as Notepoint).Map_X, (o as Notepoint).Map_Y, (obj as Realpoint).Map_X, (obj as Realpoint).Map_Y);
                    if (d == 0)
                    {
                        MessageBox.Show("目标重合");
                    }
                    else
                    {
                        MessageBox.Show("相离，选中目标点的地理距离为：" + d.ToString("F4"), "关系显示：");
                    }
                }
                else if (obj is Notepoint)
                {
                    float d = DouglasPeuker.Distance((o as Notepoint).Map_X, (o as Notepoint).Map_Y, (obj as Notepoint).Map_X, (obj as Notepoint).Map_Y);
                    if (d == 0)
                    {
                        MessageBox.Show("目标重合");
                    }
                    else
                    {
                        MessageBox.Show("相离，选中目标点的地理距离为：" + d.ToString("F4"), "关系显示：");
                    }
                }
                else if (obj is null)
                {
                    MessageBox.Show("只能判断实体点和注记点的关系", "关系显示：");
                }
            }
        }

        public static void Rdx(object o, object pictureBox1, Point p2)
        {//点与线关系判断
            object obj = Funcs.choice(p2.X, p2.Y, (pictureBox1 as PictureBox).Width, (pictureBox1 as PictureBox).Height);
            if ((obj is Lineshape) && (obj as Lineshape).ID[0] == 'l' && (obj as Lineshape).ID[1] == '1')
            {
                List<float> temp = new List<float>();
                for (int i = 0; i < (obj as Lineshape).CoorList.Count() - 1; i++)
                {
                    if (((o as Basepoint).Map_X <= (obj as Lineshape).CoorList[i].Map_X && (o as Basepoint).Map_X >= (obj as Lineshape).CoorList[i + 1].Map_X &&
                                    (o as Basepoint).Map_Y <= (obj as Lineshape).CoorList[i].Map_Y && (o as Basepoint).Map_Y >= (obj as Lineshape).CoorList[i + 1].Map_Y) ||
                                    ((o as Basepoint).Map_X >= (obj as Lineshape).CoorList[i].Map_X && (o as Basepoint).Map_X <= (obj as Lineshape).CoorList[i + 1].Map_X &&
                                    (o as Basepoint).Map_Y <= (obj as Lineshape).CoorList[i + 1].Map_Y && (o as Basepoint).Map_Y >= (obj as Lineshape).CoorList[i].Map_Y) ||
                                    ((o as Basepoint).Map_X >= (obj as Lineshape).CoorList[i].Map_X && (o as Basepoint).Map_X <= (obj as Lineshape).CoorList[i + 1].Map_X &&
                                    (o as Basepoint).Map_Y >= (obj as Lineshape).CoorList[i + 1].Map_Y && (o as Basepoint).Map_Y <= (obj as Lineshape).CoorList[i].Map_Y) ||
                                    ((o as Basepoint).Map_X <= (obj as Lineshape).CoorList[i].Map_X && (o as Basepoint).Map_X >= (obj as Lineshape).CoorList[i + 1].Map_X &&
                                    (o as Basepoint).Map_Y <= (obj as Lineshape).CoorList[i + 1].Map_Y && (o as Basepoint).Map_Y >= (obj as Lineshape).CoorList[i].Map_Y))
                    {//是否在最小外接矩形内
                        temp.Add(DouglasPeuker.Distance((o as Basepoint).Map_X, (o as Basepoint).Map_Y, (obj as Lineshape).CoorList[i].Map_X,
                             (obj as Lineshape).CoorList[i].Map_Y, (obj as Lineshape).CoorList[i + 1].Map_X, (obj as Lineshape).CoorList[i + 1].Map_Y));
                    }
                    else
                    {//求最短距离
                        temp.Add(Math.Min(DouglasPeuker.Distance((o as Basepoint).Map_X, (o as Basepoint).Map_Y, (obj as Lineshape).CoorList[i].Map_X, (obj as Lineshape).CoorList[i].Map_Y),
                            DouglasPeuker.Distance((o as Basepoint).Map_X, (o as Basepoint).Map_Y, (obj as Lineshape).CoorList[i + 1].Map_X, (obj as Lineshape).CoorList[i + 1].Map_Y)));
                    }
                }
                if (temp.ToArray().Min() == 0)
                    MessageBox.Show("在直线上", "关系显示：");
                else
                    MessageBox.Show("相离，二者间的最小距离为：" + temp.ToArray().Min(), "关系显示：");
            }
            else
            {//不是所要的目标
                MessageBox.Show("请先选择一个实体点或注记点，然后选择一条直线段", "提示");
            }
        }

        public static void Rdm(object o, object pictureBox1, Point p2)
        {//判断店面关系————夹角和
            object obj = Funcs.choice(p2.X, p2.Y, (pictureBox1 as PictureBox).Width, (pictureBox1 as PictureBox).Height);
            if ((obj is Lineshape) && (obj as Lineshape).ID[0] == 'l' && (obj as Lineshape).ID[1] == '3')
            {
                double sum = 0;
                for (int i = 0; i < (obj as Lineshape).CoorList.Count() - 1; i++)
                {//求夹角和
                    if (Fwj((o as Basepoint), (obj as Lineshape).CoorList[i + 1]) == 999999999999999 || Fwj((o as Basepoint), (obj as Lineshape).CoorList[i]) == 999999999999999)
                    {//点在面的边线或端点上
                        MessageBox.Show("点在面上", "关系显示");
                        return;
                    }
                    double jiajiao = Fwj((o as Basepoint), (obj as Lineshape).CoorList[i + 1]) - Fwj((o as Basepoint), (obj as Lineshape).CoorList[i]);
                    if (jiajiao > Math.PI)
                        jiajiao = jiajiao - 2 * Math.PI;
                    else if (jiajiao < -Math.PI)
                        jiajiao = jiajiao + 2 * Math.PI;
                    sum += jiajiao;
                }
                double jiajiao2;
                jiajiao2 = Fwj((o as Basepoint), (obj as Lineshape).CoorList[(obj as Lineshape).CoorList.Count() - 1]) - Fwj((o as Basepoint), (obj as Lineshape).CoorList[0]);
                if (jiajiao2 > Math.PI)
                    jiajiao2 = jiajiao2 - 2 * Math.PI;
                else if (jiajiao2 < -Math.PI)
                    jiajiao2 = jiajiao2 + 2 * Math.PI;
                sum -= jiajiao2;

                if (Math.Abs(sum) <= 2 * Math.PI + 0.001 && Math.Abs(sum) >= 2 * Math.PI - 0.001)
                    MessageBox.Show("点在面内" + (Math.Abs(sum) * 180 / 3.1415926535).ToString(), "关系显示");
                else if (Math.Abs(sum) <= 0.001)
                    MessageBox.Show("点在面外" + (Math.Abs(sum) * 180 / 3.1415926535).ToString(), "关系显示");
            }
            else
            {
                MessageBox.Show("请先选择一点再选择一直线面", "提示");
            }
        }

        public static void MouseClear()
        {//清空鼠标偏移及记录
            MyGlo.mouse_down_position[0] = 0;
            MyGlo.mouse_down_position[1] = 0;
            MyGlo.offset[0] = 0;
            MyGlo.offset[1] = 0;
            MyGlo.offset_tmp[0] = 0;
            MyGlo.offset_tmp[1] = 0;
        }

        public static void Clear()
        {//还原所有内存中的全局变量
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

