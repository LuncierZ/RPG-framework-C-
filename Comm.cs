using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace island
{
    public class Comm
    {
        public enum Direction
        {
            UP = 4,
            DOWN = 1,
            RIGHT = 3,
            LEFT = 2,
        }
        public static long Time()
        {
            DateTime dt1 = new DateTime(1970, 1, 1);
            TimeSpan ts = DateTime.Now - dt1;
            return (long)ts.TotalMilliseconds;
        }
        public static Direction opposite_direction(Direction direction)
        {
            if (direction == Direction.UP)
                return Direction.DOWN;
            else if (direction == Direction.DOWN)
                return Direction.UP;
            else if (direction == Direction.RIGHT)
                return Direction.LEFT;
            else if (direction == Direction.LEFT)
                return Direction.RIGHT;
            return Direction.DOWN;
        }
        //天数
        public static bool is_pause = true;
        public static int days = 1;
        public static void draw_days(Graphics g,int x,int y)
        {
            Font font_d = new Font("黑体", 20);
            Brush brush_d1 = Brushes.GreenYellow;
            g.DrawString("第" + days + "天", font_d, brush_d1, x, y, new StringFormat());
        }
    }
}
