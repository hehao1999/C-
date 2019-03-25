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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {//添加点
            if (MyGlo.rplist.Count()+MyGlo.nplist.Count<1&&MyGlo.llist.Count ==0)
            {
                MessageBox.Show("请先输入图像");
            }
            else
                try
                {
                    if (this.radioButton1.Checked == true)
                    {
                        Realpoint rpt = new Realpoint(
                            textBox1.Text + "," +
                            textBox2.Text + "," +
                            textBox3.Text + "," +
                            textBox4.Text + " " +
                            textBox5.Text + " " +
                            textBox6.Text + " " +
                            textBox7.Text + " " +
                            textBox8.Text
                            );
                        MyGlo.rplist.Add(rpt);
                        this.Owner.Refresh();
                    }
                    if (this.radioButton2.Checked == true)
                    {
                        Notepoint npt = new Notepoint(
                            textBox1.Text + "," +
                            textBox2.Text + "," +
                            textBox3.Text + "," +
                            textBox5.Text + " " +
                            textBox6.Text + " " +
                            textBox7.Text + " " +
                            textBox8.Text
                            );
                        MyGlo.nplist.Add(npt);
                        this.Owner.Refresh();
                    }
                }
                catch
                {
                    MessageBox.Show("请核对信息是否正确");
                }
        }
    }
}
