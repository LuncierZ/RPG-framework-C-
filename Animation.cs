using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace island
{
    public class Animation
    {
        public static long RATE = 100;//每100毫秒播放一帧，一秒放十帧
        public string bitmap_path;
        public Bitmap bitmap;
        public int row=1;//动画图的行数
        public int col = 1;
        public int max_frame = 1;//该动画一共几帧
        public int anm_rate;//以RATE为基准的播放速率

        public void load()
        {
            if (bitmap_path != null && bitmap_path != "")
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }
        }
        public void unload()
        {
            if (bitmap != null)
                bitmap = null;
        }
        public Bitmap get_bitmap(int frame)
        {
            if (bitmap == null)
                return null;
            if (frame >= max_frame)
                return null;
            //裁剪第几帧
            Rectangle rect = new Rectangle(bitmap.Width/row*(frame%row),bitmap.Height/col*(frame/row),bitmap.Width/row,bitmap.Height/col);
            return bitmap.Clone(rect, bitmap.PixelFormat);
        }
        public void draw(Graphics g,int frame,int x,int y)
        {
            Bitmap bitmap = get_bitmap(frame / anm_rate);
            if (bitmap == null)
                return;
            g.DrawImage(bitmap, x, y);
        }
    }
}
