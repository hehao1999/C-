using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace MyCAD
{
    public abstract class Basepoint
    {//abstract基础点类(不允许创建实例)，含有X、Y坐标，所属类ID

        public float Map_X { get; set; }
        public float Map_Y { get; set; }
        public float Map_Z { get; set; }
        public char[] ID { get; set; }
        public int Size { get; set; }
        public Color color_in { get; set; }
        public Color color_out { get; set; }
    }//Basepoint

    class Realpoint : Basepoint
    {//实体点，继承自Basepoint类，增加NID、Name、Info属性  

        private uint NID { get; set; }
        private string Name { get; set; }
        private string Info { get; set; }

        public Realpoint(string temp)
        {//构造函数构造实体点
            this.ID = new char[2];
            this.ID[0] = 'p';
            this.ID[1] = 'r';
            this.NID = MyGlo.rp_num + (uint)1;
            this.Map_X = float.Parse(temp.Split(',')[0]);
            this.Map_Y = float.Parse(temp.Split(',')[1]);
            this.Map_Z = float.Parse(temp.Split(',')[2]);
            this.Name = temp.Split(',')[3].Split(' ')[0];
            this.Info = temp.Split(',')[3].Split(' ')[1];
            this.Size = Convert.ToInt32(temp.Split(',')[3].Split(' ')[2]) * 2 + 1;
            if (temp.Split(',')[3].Split(' ').Count() > 4)
            {
                this.color_in = Color.FromName(temp.Split(',')[3].Split(' ')[3]);
                this.color_out = Color.FromName(temp.Split(',')[3].Split(' ')[4]);
            }
            else
            {
                this.color_in = Color.Red;
                this.color_out = Color.Black;
            }
        }//Realpoint构造函数
    }//Realpoint

    class Notepoint : Basepoint
    {//注记点，继承自Basepoint类，拥有NID、NoteInfo、Choice属性    

        private uint NID { get; set; }
        string NoteInfo { get; set; }

        public Notepoint(string temp)
        {
            this.ID = new char[2];
            this.ID[0] = 'p';
            this.ID[1] = 'n';
            this.NID = MyGlo.np_num + (uint)1;
            this.Map_X = float.Parse(temp.Split(',')[0]);
            this.Map_Y = float.Parse(temp.Split(',')[1]);
            this.Map_Z = float.Parse(temp.Split(',')[2]);
            this.NoteInfo = temp.Split(',')[3].Split(' ')[0];
            this.Size = Convert.ToInt32(temp.Split(',')[3].Split(' ')[1]) * 2 + 1;
            if (temp.Split(',')[3].Split(' ').Count() > 3)
            {
                this.color_in = Color.FromName(temp.Split(',')[3].Split(' ')[2]);
                this.color_out = Color.FromName(temp.Split(',')[3].Split(' ')[3]);
            }
            else
            {
                this.color_in = Color.Blue;
                this.color_out = Color.Black;
            }
        }
    }

    class Linkpoint : Basepoint
    {//关联点类，与线或面具有某种关联关系，继承自Basepoint类，增加LinkID属性    

        private string LinkID { get; set; }

        public Linkpoint(string temp)
        {
            this.ID = new char[2];
            this.ID[0] = 'p';
            this.ID[1] = 'l';
            this.Map_X = float.Parse(temp.Split(',')[0]);
            this.Map_Y = float.Parse(temp.Split(',')[1]);
            this.Map_Z = float.Parse(temp.Split(',')[2]);
            this.LinkID = temp.Split(',')[3].Split(' ')[0];
            this.Size = Convert.ToInt32(temp.Split(',')[3].Split(' ')[1]) * 2 + 1;
            if (temp.Split(',')[3].Split(' ').Count() > 3)
            {
                this.color_in = Color.FromName(temp.Split(',')[3].Split(' ')[2]);
                this.color_out = Color.FromName(temp.Split(',')[3].Split(' ')[3]);
            }
            else
            {
                this.color_in = Color.Gray;
                this.color_out = Color.Black;
            }
        }// Linkpoint构造函数
    }

    public class Lineshape
    {//线类型，拥有ID、NID、IsClose、NodeNum、Len、CoorList、SyLine属性

        public char[] ID { get; set; }
        private uint NID { get; set; }
        private uint NodeNum { get; set; }
        private string Name { get; set; }
        private string LinkID { get; set; }
        public int LineSize { get; set; }
        public Color color_in { get; set; }
        public Color color_out { get; set; }
        public List<Basepoint> CoorList = new List<Basepoint>();

        public Lineshape(List<Basepoint> temp, string hdr)
        {
            this.ID = new char[2];
            this.ID[0] = hdr[0];
            this.ID[1] = hdr[1];
            this.NID = MyGlo.l_num + (uint)1;
            this.NodeNum = (uint)temp.Count();
            this.Name = hdr.Split(',')[1].Split(' ')[0];
            this.LinkID = hdr.Split(',')[1].Split(' ')[1];
            this.LineSize = Convert.ToInt32(hdr.Split(',')[1].Split(' ')[2]);
            if (hdr.Split(',')[1].Split(' ').Count() > 4)
            {
                this.color_in = Color.FromName(hdr.Split(',')[1].Split(' ')[3]);
                this.color_out = Color.FromName(hdr.Split(',')[1].Split(' ')[4]);
            }
            else
            {
                this.color_in = Color.Gray;
                this.color_out = Color.Black;
            }
            this.CoorList = temp;
        }// Lineshape构造函数
    }
}
