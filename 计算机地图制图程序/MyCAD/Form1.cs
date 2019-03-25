using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyCAD
{
    public partial class Form1 : Form
    {
        protected uint first_change = 0;
        protected bool bool_del; //是否启动删除
        public static bool bool_down; //按下与否
        public static bool bool_move; //是否需要移动
        public static bool bool_cul; //是否要记录点击的点
        protected bool bool_dis; //是否开始计算距离
        protected bool bool_dd; //是否进行点点关系判断
        protected bool bool_dx; //是否进行点线关系判断
        protected bool bool_dm; //是否进行点面关系判断
        protected bool fuhao1; //是否进行符号1绘制
        protected bool fuhao2; //是否进行符号2绘制
        protected bool fd; //是否进行放大
        protected bool sx; //是否进行缩小
        protected Point p;  //距离计算的点坐标
        protected object o; //记录选中要素

        public void NoActive()
        {//把功能全部置否
            o = null;
            bool_del = bool_cul = bool_move = bool_dd = bool_dx = bool_dm = fuhao1 = fuhao2 = fd = sx = false;
            this.Cursor = Cursors.Default;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)//可以删掉第一个刷新方法合并两种方法
        {
            try
            {
                //清屏
                e.Graphics.Clear(Color.White);

                //重绘
                switch (MyGlo.update_function)
                {
                    case 0://全图显示刷新
                        Funcs.Update(e.Graphics, this.pictureBox1.Width, this.pictureBox1.Height);
                        Funcs.MouseClear();
                        break;
                    case 1://移动刷新
                        Funcs.Update(e.Graphics, this.pictureBox1.Width, this.pictureBox1.Height, 1);
                        break;
                }

                //默认全图显示刷新
                MyGlo.update_function = 0;
            }
            catch
            {
                MessageBox.Show("绘制图像时出错");
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //刷新，激活重绘
                if (first_change > 2)
                {
                    Graphics g = this.pictureBox1.CreateGraphics();
                    MyGlo.scale = 999999999999999;
                    Funcs.Scale(this.pictureBox1.Width, this.pictureBox1.Height);
                    this.pictureBox1.Refresh();
                }
                else
                    first_change += 1;
            }
            catch
            {
                MessageBox.Show("绘制图像时出错");
            }
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {//数据导入
            try
            {
                this.ImportFDL.Filter = "csv文件（*.csv)|*.csv"; //导入csv数据文件
                if (ImportFDL.ShowDialog() == DialogResult.OK)
                {
                    //读取文件
                    StreamReader import_file = new StreamReader(this.ImportFDL.FileName);

                    //清屏，绘图，刷新
                    Graphics g = this.pictureBox1.CreateGraphics();
                    Funcs.Importfile(import_file, g, this.pictureBox1.Height, this.pictureBox1.Width);
                    Funcs.Maximum();
                    Funcs.Scale(this.pictureBox1.Width, this.pictureBox1.Height);
                    this.pictureBox1.Refresh();

                    //关闭文件
                    import_file.Close();
                }//if
            }
            catch
            {
                MessageBox.Show("数据中只有一点或格式错误");
            }
        }

        private void exportImageToolStripMenuItem_Click(object sender, EventArgs e)
        {//导出
            try
            {
                SaveFileDialog saveImageDialog = new SaveFileDialog();
                saveImageDialog.Title = "Export image";
                saveImageDialog.Filter = @"jpeg|*.jpg|bmp|*.bmp";
                if (saveImageDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveImageDialog.FileName.ToString();
                    if (fileName != "" && fileName != null)
                    {
                        string fileExtName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToString();
                        System.Drawing.Imaging.ImageFormat imgformat = null;


                        if (fileExtName != "")
                        {
                            switch (fileExtName)
                            {
                                case "jpg":
                                    imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                                    break;
                                case "bmp":
                                    imgformat = System.Drawing.Imaging.ImageFormat.Bmp;
                                    break;
                                default:
                                    imgformat = System.Drawing.Imaging.ImageFormat.Jpeg;
                                    break;
                            }
                            Bitmap bmp = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
                            Graphics g = Graphics.FromImage(bmp);
                            g.Clear(Color.White);
                            Funcs.Update(g, this.pictureBox1.Width, this.pictureBox1.Height);
                            this.pictureBox1.Image = bmp;
                            bmp.Save(fileName, imgformat);//保存
                            MessageBox.Show("Save successfully !                             ", "Reminder");//提示保存成功
                                                                                                            //bmp.Dispose();Bitmap赋值给PictureBox后，误对其进行了Dispose()的操作，在PictureBox Size改变时，重绘时导致异常，出现红叉。
                        }//if
                    }//if
                }//if
            }
            catch
            {
                MessageBox.Show("导出错误");
            }
        }

        private void 导出为CSVToolStripMenuItem_Click(object sender, EventArgs e)
        {//导出CSV文件
            try
            {
                SaveFileDialog saveImageDialog = new SaveFileDialog();
                saveImageDialog.Title = "Export CSV";
                saveImageDialog.Filter = @"csv|*.csv";
                if (saveImageDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveImageDialog.FileName.ToString();
                    if (fileName != "" && fileName != null)
                    {
                        string fileExtName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToString();
                        if (fileExtName != "")
                        {
                            StreamWriter w = new StreamWriter(fileName, false);
                            w.Write("pr,,,\n");
                            foreach (Realpoint rpt in MyGlo.rplist)
                            {//实体点
                                w.Write(rpt.Map_X.ToString() + "," + rpt.Map_Y.ToString() + "," + rpt.Map_Z.ToString() + "," + rpt.Name + " " + rpt.Info
                                    + " " + ((rpt.Size - 1) / 2).ToString() + " " + rpt.color_in.Name + " " + rpt.color_out.Name + "\n");
                            }
                            w.Write(",,,\n");

                            w.Write("pn,,,\n");
                            foreach (Notepoint npt in MyGlo.nplist)
                            {//注记点
                                w.Write(npt.Map_X.ToString() + "," + npt.Map_Y.ToString() + "," + npt.Map_Z.ToString() + "," + npt.NoteInfo +
                                    " " + ((npt.Size - 1) / 2).ToString() + " " + npt.color_in.Name + " " + npt.color_out.Name + "\n");
                            }

                            //线、面
                            foreach (Lineshape line in MyGlo.llist)
                            {
                                w.Write(",,,\n");
                                w.Write(line.ID[0].ToString() + line.ID[1].ToString() + "," + line.Name + " " + line.LinkID + " " + line.LineSize.ToString() + " " + line.color_in.Name + " " + line.color_out.Name + "\n");
                                foreach (Linkpoint lpt in line.CoorList)
                                {
                                    w.Write(lpt.Map_X.ToString() + "," + lpt.Map_Y.ToString() + "," + lpt.Map_Z.ToString() + "," + lpt.LinkID + " " + ((lpt.Size - 1) / 2).ToString() + " " + lpt.color_in.Name + " " + lpt.color_out.Name + "\n");
                                }
                            }
                            //关闭写入并提示
                            w.Close();
                            MessageBox.Show("Save successfully !                             ", "提示");
                        }//if
                    }//if
                }
            }
            catch
            {
                MessageBox.Show("输出时出错");
            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //打开帮助页面
                string exestr = Application.StartupPath;
                System.Diagnostics.Process.Start(exestr + @"\help\文件.html\");
            }
            catch
            {
                MessageBox.Show("帮助文件移位或删除");
            }
        }

        private void open_bt_Click(object sender, EventArgs e)
        {
            importToolStripMenuItem_Click(sender, e);
        }

        private void save_bt_Click(object sender, EventArgs e)
        {
            //导出图片
            exportImageToolStripMenuItem_Click(sender, e);
        }

        private void clear_bt_Click(object sender, EventArgs e)
        {//清空屏幕
            try
            {
                Funcs.Clear();
                this.pictureBox1.Refresh();
            }
            catch
            {
                MessageBox.Show("清屏时出现错误");
            }
        }

        private void globle_bt_Click(object sender, EventArgs e)
        {//全局视图
            pictureBox1_SizeChanged(sender, e);
        }

        private void move_bt_Click(object sender, EventArgs e)
        {
            try
            {
                //移动激活与否的转换
                if (bool_move == true)
                {
                    NoActive();// bool_move = false;
                }
                else
                {
                    NoActive();
                    bool_move = true;
                }
            }
            catch
            {
                MessageBox.Show("未知错误");
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                //距离量测
                if (bool_cul == true)
                {
                    if (bool_dis == true)
                    {
                        p = pictureBox1.PointToClient(MousePosition);
                        bool_dis = false;
                    }
                    else
                    {
                        Point p2 = pictureBox1.PointToClient(MousePosition);
                        object obj = Funcs.choice(p2.X, p2.Y, pictureBox1.Width, pictureBox1.Height);
                        if (obj == null)
                        {
                            float d = DouglasPeuker.Distance(p2.X, p2.Y, p.X, p.Y);
                            MessageBox.Show("未选中目标，鼠标两点地理距离为：" + (d / MyGlo.scale).ToString("F4"), "距离显示：");
                            NoActive();
                        }
                        else
                        {
                            if (obj is Realpoint)
                            {
                                Point temp_p;
                                temp_p = Funcs.CoorTransform((obj as Realpoint).Map_X, (obj as Realpoint).Map_Y, pictureBox1.Width, pictureBox1.Height);
                                float d = DouglasPeuker.Distance(p.X, p.Y, temp_p.X, temp_p.Y);
                                MessageBox.Show("选中实体点，鼠标两点地理距离为：" + (d / MyGlo.scale).ToString("F4"), "距离显示：");
                                NoActive();
                            }
                            else if (obj is Notepoint)
                            {
                                Point temp_p;
                                temp_p = Funcs.CoorTransform((obj as Notepoint).Map_X, (obj as Notepoint).Map_Y, pictureBox1.Width, pictureBox1.Height);
                                float d = DouglasPeuker.Distance(p.X, p.Y, temp_p.X, temp_p.Y);
                                MessageBox.Show("选中注记点，鼠标两点地理距离为：" + (d / MyGlo.scale).ToString("F4"), "距离显示：");
                                NoActive();
                            }
                            else if (obj is Lineshape)
                            {
                                if ((obj as Lineshape).ID[0] == 'l' && (obj as Lineshape).ID[1] == '1' && (obj as Lineshape).CoorList.Count == 2)
                                {
                                    if ((p.X <= (obj as Lineshape).CoorList[0].Map_X && p.X >= (obj as Lineshape).CoorList[1].Map_X &&
                                        p.Y <= (obj as Lineshape).CoorList[0].Map_Y && p.Y >= (obj as Lineshape).CoorList[1].Map_Y) ||
                                        (p.X >= (obj as Lineshape).CoorList[0].Map_X && p.X <= (obj as Lineshape).CoorList[1].Map_X &&
                                        p.Y <= (obj as Lineshape).CoorList[1].Map_Y && p.Y >= (obj as Lineshape).CoorList[0].Map_Y) ||
                                        (p.X >= (obj as Lineshape).CoorList[0].Map_X && p.X <= (obj as Lineshape).CoorList[1].Map_X &&
                                        p.Y >= (obj as Lineshape).CoorList[1].Map_Y && p.Y <= (obj as Lineshape).CoorList[0].Map_Y) ||
                                        (p.X <= (obj as Lineshape).CoorList[0].Map_X && p.X >= (obj as Lineshape).CoorList[1].Map_X &&
                                        p.Y <= (obj as Lineshape).CoorList[1].Map_Y && p.Y >= (obj as Lineshape).CoorList[0].Map_Y))
                                    {
                                        Point temp_p1, temp_p2;
                                        temp_p1 = Funcs.CoorTransform((obj as Lineshape).CoorList[0].Map_X, (obj as Lineshape).CoorList[0].Map_Y, pictureBox1.Width, pictureBox1.Height);
                                        temp_p2 = Funcs.CoorTransform((obj as Lineshape).CoorList[1].Map_X, (obj as Lineshape).CoorList[1].Map_Y, pictureBox1.Width, pictureBox1.Height);
                                        float d = DouglasPeuker.Distance(p.X, p.Y, temp_p1.X, temp_p1.Y, temp_p2.X, temp_p2.Y);
                                        MessageBox.Show("选中直线，点到地理距离为：" + (d / MyGlo.scale).ToString("F4"), "距离显示：");
                                        NoActive();
                                    }
                                    else
                                    {
                                        Point temp_p1, temp_p2;
                                        temp_p1 = Funcs.CoorTransform((obj as Lineshape).CoorList[0].Map_X, (obj as Lineshape).CoorList[0].Map_Y, pictureBox1.Width, pictureBox1.Height);
                                        temp_p2 = Funcs.CoorTransform((obj as Lineshape).CoorList[1].Map_X, (obj as Lineshape).CoorList[1].Map_Y, pictureBox1.Width, pictureBox1.Height);
                                        float d = Math.Min(DouglasPeuker.Distance(p.X, p.Y, temp_p1.X, temp_p1.Y), DouglasPeuker.Distance(p.X, p.Y, temp_p2.X, temp_p2.Y));
                                        MessageBox.Show("选中直线，点到地理距离为：" + (d / MyGlo.scale).ToString("F4"), "距离显示：");
                                        NoActive();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("暂不支持曲线/面和多点线/面，请选择两点直线段,如果想求点到多段线的最小距离可以使用点线关系工具", "提示");
                                    NoActive();
                                }
                            }
                        }

                    }
                }

                //点点关系，写的看不下去，可使用basepoint简化
                if (bool_dd == true)
                {
                    Point p2 = pictureBox1.PointToClient(MousePosition);
                    if (o == null)
                    {
                        o = Funcs.choice(p2.X, p2.Y, (pictureBox1 as PictureBox).Width, (pictureBox1 as PictureBox).Height);
                    }
                    else if (o is Realpoint || o is Notepoint)
                    {
                        Funcs.Rdd(o, pictureBox1, p2);
                        NoActive();
                    }
                    else
                    {
                        MessageBox.Show("请选择两点");
                        NoActive();
                    }
                }

                //点线关系
                if (bool_dx == true)
                {
                    Point p2 = pictureBox1.PointToClient(MousePosition);
                    if (o == null)
                    {
                        o = Funcs.choice(p2.X, p2.Y, (pictureBox1 as PictureBox).Width, (pictureBox1 as PictureBox).Height);
                    }
                    else if (o is Realpoint || o is Notepoint)
                    {
                        Funcs.Rdx(o, pictureBox1, p2);
                        NoActive();
                    }
                    else
                    {
                        MessageBox.Show("请选择一点和一条直线");
                        NoActive();
                    }
                }

                //点面关系
                if (bool_dm == true)
                {
                    Point p2 = pictureBox1.PointToClient(MousePosition);
                    if (o == null)
                    {
                        o = Funcs.choice(p2.X, p2.Y, (pictureBox1 as PictureBox).Width, (pictureBox1 as PictureBox).Height);
                    }
                    else if (o is Realpoint || o is Notepoint)
                    {
                        Funcs.Rdm(o, pictureBox1, p2);
                        NoActive();
                    }
                    else
                    {
                        MessageBox.Show("请选择一点和一直线面");
                        NoActive();
                    }
                }

                //删除对象
                if (bool_del == true)
                {
                    Point p2 = pictureBox1.PointToClient(MousePosition);
                    object obj = Funcs.choice(p2.X, p2.Y, pictureBox1.Width, pictureBox1.Height);
                    Funcs.Del(obj, pictureBox1);
                    NoActive();
                    this.Cursor = Cursors.Default;
                }

                //符号绘制
                if (fuhao1 == true)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    Funcs.Daoxiandian(g, pictureBox1.PointToClient(MousePosition).X, pictureBox1.PointToClient(MousePosition).Y, pictureBox1.Width, pictureBox1.Height);
                    NoActive();
                }
                if (fuhao2 == true)
                {
                    Graphics g = pictureBox1.CreateGraphics();
                    Funcs.Jiaotang(g, pictureBox1.PointToClient(MousePosition).X, pictureBox1.PointToClient(MousePosition).Y, pictureBox1.Width, pictureBox1.Height);
                    NoActive();
                }
            }
            catch
            {
                MessageBox.Show("未知错误");
            }

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)//可以删去一个变量！
        {
            try
            {
                //表示鼠标已经按下
                bool_down = true;

                //如果移动激活，则记录数据
                if (bool_move == true)
                {//记录移动前的鼠标坐标，可以删去一个变量！
                    MyGlo.offset_tmp[0] = e.X;
                    MyGlo.offset_tmp[1] = e.Y;
                    MyGlo.mouse_down_position[0] = e.X;
                    MyGlo.mouse_down_position[1] = e.Y;
                }
            }
            catch
            {
                MessageBox.Show("未知错误");
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)//距离测量可以修改
        {
            try
            {
                //坐标显示及更新
                Funcs.CoorDisplay(this.toolStripStatusLabel1, this.pictureBox1, e);

                if (bool_dis == true)
                {//光标变为十字
                    this.Cursor = Cursors.Cross;
                }

                if (bool_move == true)
                {//光标变为四向箭头
                    this.Cursor = Cursors.NoMove2D;
                }
            }
            catch
            {
                MessageBox.Show("未知错误");
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                //如果鼠标按下未弹起
                if (bool_down == true)
                {
                    //如果处于移动激活的状态
                    if (bool_move == true)
                    {
                        //更新偏移量
                        MyGlo.offset[0] += (e.X - MyGlo.offset_tmp[0]);
                        MyGlo.offset[1] += (e.Y - MyGlo.offset_tmp[1]);
                    }
                    //鼠标按下状态改为已经弹起，即未按下状态
                    bool_down = false;
                }
            }
            catch
            {
                MessageBox.Show("未知错误");
            }
        }

        private void enlarge_bt_Click(object sender, EventArgs e)
        {//放大
            NoActive();
            fd = true;
        }

        private void shrink_bt_Click(object sender, EventArgs e)
        {//缩小
            NoActive();
            sx = true;
        }

        private void douglasPeukerToolStripMenuItem_Click(object sender, EventArgs e)
        {//数据压缩
            try
            {
                DP dp = new DP();
                dp.Owner = this;
                dp.Show();
            }
            catch
            {
                MessageBox.Show("数据抽稀时发现错误，请仔细检查数据");
            }
        }

        private void dis_ToolStripMenuItem_Click(object sender, EventArgs e)
        {//距离量测
            NoActive();
            bool_dis = true;
            bool_cul = true;
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {//点点关系
            NoActive();
            bool_dd = true;
            this.Cursor = Cursors.Cross;
        }

        private void del_ToolStripMenuItem_Click(object sender, EventArgs e)
        {//删除
            NoActive();
            bool_del = true;
            this.Cursor = Cursors.Hand;
        }

        private void dx_ToolStripMenuItem_Click(object sender, EventArgs e)
        {//点线关系
            NoActive();
            bool_dx = true;
        }

        private void dm_ToolStripMenuItem_Click(object sender, EventArgs e)
        {//点面关系
            NoActive();
            bool_dm = true;
        }

        private void se_ToolStripMenuItem_Click(object sender, EventArgs e)
        {//查找
            try
            {
                find fd = new find();
                fd.Owner = this;
                fd.Show();
            }
            catch
            {
                MessageBox.Show("查找时出现错误");
            }
        }

        private void add_ToolStripMenuItem_Click(object sender, EventArgs e)
        {//增加点
            try
            {
                Form2 f = new Form2();
                f.Owner = this;
                f.Show();
            }
            catch
            {
                MessageBox.Show("增加点时出现错误");
            }
        }

        private void 导线点符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoActive();
            fuhao1 = true;
        }

        private void 教堂符号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NoActive();
            fuhao2 = true;
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {//双击放大/缩小
            try
            {
                if (fd == true)
                {//放大
                    MyGlo.scale *= 1.2f;
                    MyGlo.update_function = 1;
                    MyGlo.mouse_down_position[0] = 0;
                    MyGlo.mouse_down_position[1] = 0;
                    MyGlo.mouse_now_position.X = 0;
                    MyGlo.mouse_now_position.Y = 0;
                    this.pictureBox1.Refresh();
                }
                else if (sx == true)
                {//缩小
                    MyGlo.scale *= 0.8f;
                    MyGlo.update_function = 1;
                    MyGlo.mouse_down_position[0] = 0;
                    MyGlo.mouse_down_position[1] = 0;
                    MyGlo.mouse_now_position.X = 0;
                    MyGlo.mouse_now_position.Y = 0;
                    this.pictureBox1.Refresh();
                }
                else
                    return;
            }
            catch
            {
                MessageBox.Show("未知错误");
            }
        }

        private void 数据加密ToolStripMenuItem_Click(object sender, EventArgs e)
        {//数据加密
            try
            {
                foreach (Lineshape line in MyGlo.llist)
                {
                    if (line.ID[1] == '1')
                    {
                        line.ID[1] = '2';
                    }
                    else if (line.ID[1] == '3')
                    {
                        line.ID[1] = '4';
                    }
                    MyGlo.update_function = 1;
                    this.pictureBox1.Refresh();
                }
            }
            catch
            {
                MessageBox.Show("曲线光滑时出现错误");
            }
        }
    }
}
