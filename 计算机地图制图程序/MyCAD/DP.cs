using System;
using System.Linq;
using System.Windows.Forms;

namespace MyCAD
{
    public partial class DP : Form
    {
        public DP()
        {
            InitializeComponent();

        }

        private void DP_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var line in MyGlo.llist)
                {
                    if (line.ID[1] != '2' && line.ID[1] != '4')
                    {
                        DouglasPeuker.after.Add(line.CoorList[0]);
                        DouglasPeuker.DP(0, line.CoorList.Count(), (float)Convert.ToDouble(this.textBox1.Text), line.CoorList);
                        line.CoorList.Clear();  //速度太慢，是否有更快的DEEP COPE方法
                        foreach (var pt in DouglasPeuker.after)
                        {
                            line.CoorList.Add(pt);
                        }
                        DouglasPeuker.after.Clear();
                    }
                }
                this.Owner.Refresh();
            }
            catch(Exception)
            {
                MessageBox.Show("输入错误");
            }
        }
    }
}
