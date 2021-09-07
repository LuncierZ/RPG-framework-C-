using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace island
{
    public class CG
    {
        public static CG[] cg;
        public static int current_cg=-1;

        public Bitmap cg_bitmap;
        public string cg_path;

        public int x=0;
        public int y=0;

        public void load()
        {
            if(cg_path!=null&& cg_path !="")
            {
                cg_bitmap = new Bitmap(cg_path);
                cg_bitmap.SetResolution(96, 96);
                Comm.is_pause = true;
            }
        }
        public void unload()
        {
            if(cg_bitmap!=null)
            cg_bitmap = null;
            Comm.is_pause = false;
        }
        public static void draw(Graphics g)
        {
            if (current_cg == -1)
                return;
            g.DrawImage(cg[current_cg].cg_bitmap, cg[current_cg].x, cg[current_cg].y);
        }
    }
}
