using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MyCAD
{
    public partial class Form1 : Form
    {
        protected bool bool_down;
        protected bool bool_move;
        protected uint first_change = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
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

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
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

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {//数据导入
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

        private void exportImageToolStripMenuItem_Click(object sender, EventArgs e)
        {//导出
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
                        //bmp.Dispose();Bitmap赋值给PictureBox后，误对其进行了Dispose()的操作，
                        //在PictureBox Size改变时，重绘时导致异常，出现红叉。
                    }//if
                }//if
            }//if
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开帮助页面
            string exestr = Application.StartupPath;
            System.Diagnostics.Process.Start(exestr + @"\help\home.html\");
        }

        private void open_bt_Click(object sender, EventArgs e)
        {
            importToolStripMenuItem_Click(sender, e);
        }

        private void save_bt_Click(object sender, EventArgs e)
        {
            exportImageToolStripMenuItem_Click(sender, e);
        }

        private void clear_bt_Click(object sender, EventArgs e)
        {
            Funcs.Clear();
            this.pictureBox1.Refresh();
        }

        private void globle_bt_Click(object sender, EventArgs e)
        {
            pictureBox1_SizeChanged(sender, e);
        }

        private void move_bt_Click(object sender, EventArgs e)
        {
            //移动激活与否的转换
            if (bool_move == true)
            {
                bool_move = false;
            }
            else
            {
                bool_move = true;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //表示鼠标已经按下
            bool_down = true;

            //如果移动激活，则记录数据
            if (bool_move == true)
            {
                MyGlo.offset_tmp[0] = e.X;
                MyGlo.offset_tmp[1] = e.Y;
                MyGlo.mouse_down_position[0] = e.X;
                MyGlo.mouse_down_position[1] = e.Y;
            }  
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            float[] stg = new float[2];
            //鼠标是否已经按下，并且未弹起
            if (bool_down == true)
            {
                //如果处于移动激活状态
                if (bool_move == true)
                {
                    //显示按下点的坐标
                    stg = Funcs.ScrToGeo(MyGlo.mouse_down_position[0] - MyGlo.offset[0], MyGlo.mouse_down_position[1] - MyGlo.offset[1], this.pictureBox1.Width, this.pictureBox1.Height);
                    this.toolStripStatusLabel1.Text = "Coordinate：" + Math.Round(stg[0], 4) + "," + Math.Round(stg[1], 4);

                    //记录目前坐标并使用方法1刷屏
                    MyGlo.mouse_now_position = new Point(e.X, e.Y);
                    MyGlo.update_function = 1;
                    this.pictureBox1.Refresh();
                }//if 
            }

            //鼠标未按下
            else
            {
                //显示坐标
                //float[] stg = new float[2];
                stg = Funcs.ScrToGeo(e.X - MyGlo.offset[0], e.Y - MyGlo.offset[1], this.pictureBox1.Width, this.pictureBox1.Height);
                this.toolStripStatusLabel1.Text = "Coordinate：" + Math.Round(stg[0], 4) + "," + Math.Round(stg[1], 4);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            //如果鼠标按下未弹起
            if (bool_down == true)
            {
                //如果处于移动激活的状态
                if (bool_move == true)
                {
                    //更新偏移量
                    MyGlo.offset[0] += e.X - MyGlo.offset_tmp[0];
                    MyGlo.offset[1] += e.Y - MyGlo.offset_tmp[1];
                }

                //鼠标按下状态改为已经弹起，即未按下状态
                bool_down = false;
            }
        }

        private void enlarge_bt_Click(object sender, EventArgs e)
        {
            MyGlo.scale *= 1.2f;
            MyGlo.update_function = 1;
            this.pictureBox1.Refresh();
        }

        private void shrink_bt_Click(object sender, EventArgs e)
        {
            MyGlo.scale *= 0.8f;
            MyGlo.update_function = 1;
            this.pictureBox1.Refresh();
        }

        private void douglasPeukerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DP dp = new DP();
            dp.Owner = this;
            dp.Show();
        }
    }
}
