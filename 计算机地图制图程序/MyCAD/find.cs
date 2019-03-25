using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCAD
{
    public partial class find : Form
    {
        public find()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = textBox2.Text = textBox3.Text = null;
            this.Refresh();
        }

        private void find_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double x, y;
                x = Convert.ToDouble(this.textBox1.Text);
                y = Convert.ToDouble(this.textBox2.Text);
                object obj = Funcs.Find((float)x, (float)y);
                if (obj is Realpoint)
                {
                    this.textBox3.Text = "X坐标:" + (obj as Realpoint).Map_X.ToString() + "\r\n" +
                        "Y坐标：" + (obj as Realpoint).Map_Y.ToString() + "\r\n" +"abcd"+
                        "Z坐标：" + (obj as Realpoint).Map_Y.ToString() + "\r\n" +
                        "ID：" + (obj as Realpoint).ID[0] + (obj as Realpoint).ID[1] + "\r\n" +
                        "NID：" + (obj as Realpoint).NID.ToString() + "\r\n" +
                        "Name：" + (obj as Realpoint).Name + "\r\n" +
                        "Size：" + (obj as Realpoint).Size + "\r\n" +
                        "Info：" + (obj as Realpoint).Info + "\r\n";
                }
                else if (obj is Notepoint)
                {
                    this.textBox3.Text = "X坐标:" + (obj as Notepoint).Map_X.ToString() + "\r\n" +
                        "Y坐标：" + (obj as Notepoint).Map_Y.ToString() + "\r\n" +
                        "Z坐标：" + (obj as Notepoint).Map_Y.ToString() + "\r\n" +
                        "ID：" + (obj as Notepoint).ID[0] + (obj as Notepoint).ID[1] + "\r\n" +
                        "NID：" + (obj as Notepoint).NID.ToString() + "\r\n" +
                        "NoteInfo：" + (obj as Notepoint).NoteInfo + "\r\n" +
                        "Size" + (obj as Notepoint).Size + "\r\n";
                }
                else if (obj is Lineshape)
                {
                    this.textBox3.Text = "ID:" + (obj as Lineshape).ID[0] + (obj as Lineshape).ID[1] + "\r\n" +
                        "LineSize：" + (obj as Lineshape).LineSize.ToString() + "\r\n" +
                        "LiNKID：" + (obj as Lineshape).LinkID + "\r\n" +
                        "Name：" + (obj as Lineshape).Name + "\r\n" +
                        "NID：" + (obj as Lineshape).NID.ToString() + "\r\n" +
                        "Name：" + (obj as Lineshape).NodeNum.ToString();
                }
                else
                {
                    this.textBox3.Text = "没有找到要素";
                }
            }
            catch
            {
                MessageBox.Show("输入错误。。。");
            }
        }
    }
}
