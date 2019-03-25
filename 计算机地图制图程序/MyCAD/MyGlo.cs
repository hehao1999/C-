using System.Collections.Generic;
using System.Drawing;

namespace MyCAD
{
    public class MyGlo
    {//作全局变量
        //刷新方法
        public static uint update_function = 0;

        //记录点数，做nid
        public static uint rp_num;
        public static uint np_num;
        public static uint l_num;

        //记录最小外接矩形和比例尺
        public static float max_x { get; set; }
        public static float max_y { get; set; }
        public static float min_x { get; set; }
        public static float min_y { get; set; }
        public static float scale = 99999999999999;

        //拖动鼠标移动图层时
        public static float[] offset = new float[2];//记录偏移量
        public static float[] offset_tmp = new float[2];
        public static int[] mouse_down_position = new int[2];//鼠标拖动时记录按下瞬间的坐标值
        public static Point mouse_now_position = new Point();//鼠标目前位置坐标
        
        //记录点线集
        public static List<Basepoint> rplist = new List<Basepoint>(); //实体点集
        public static List<Basepoint> nplist = new List<Basepoint>(); //注记点集
        public static List<Lineshape> llist = new List<Lineshape>(); //线与面集
    }
}