using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace island
{
    public class Button
    {
        //位置
        public int x = 0;
        public int y = 0;
        public int w = 0;
        public int h = 0;
        //图片
        public string b_normal_path;
        public string b_select_path;
        public string b_press_path;
        Bitmap b_normal;
        Bitmap b_select;
        Bitmap b_press;
        //状态
        public enum Status
        {
            NORMAL=1,
            SELECT=2,
            PRESS=3,
        }
        public Status status = Status.NORMAL;
        //键盘控制跳转
        public class Key_ctrl
        {
            public int up = -1;
            public int down = -1;
            public int left = -1;
            public int right = -1;
        }
        public Key_ctrl key_Ctrl = new Key_ctrl();

        public void set(int x0,int y0,int w0,int h0,
            string normal_path,string select_path,string press_path,
            int key_up,int key_down,int key_left,int key_right)
        {
            x = x0;
            y = y0;
            b_normal_path = normal_path;
            b_select_path = select_path;
            b_press_path = press_path;
            w = w0;
            h = h0;
            key_Ctrl.up = key_up;
            key_Ctrl.down = key_down;
            key_Ctrl.left = key_left;
            key_Ctrl.right = key_right;
        }
        public void load()
        {
            if (b_normal_path != null && b_normal_path != "")
            {
                b_normal = new Bitmap(b_normal_path);
                b_normal.SetResolution(96, 96);
                if (w <= 0)
                    w = b_normal.Width;
                if (h <= 0)
                    h = b_normal.Height;
            }
            if (b_select_path != null && b_select_path != "")
            {
                b_select = new Bitmap(b_select_path);
                b_select.SetResolution(96, 96);
            }
            if (b_press_path != null && b_press_path != "")
            {
                b_press = new Bitmap(b_press_path);
                b_press.SetResolution(96, 96);
            }
        }
        //绘图
        public void draw(Graphics g,int x_offset,int y_offset)
        {
            if (status == Status.NORMAL && b_normal != null)
                g.DrawImage(b_normal,x_offset+x,y_offset+y);
            if (status == Status.SELECT && b_select != null)
                g.DrawImage(b_select, x_offset + x, y_offset + y);
            if (status == Status.PRESS && b_press != null)
                g.DrawImage(b_press, x_offset + x, y_offset + y);
        }
        //点击事件
        public delegate void Click_event();
        public event Click_event click_Event;
        public void click()
        {
            if (click_Event != null)
                click_Event();
        }
        //鼠标操作——碰撞检测
        public bool is_collision(int collision_x,int collision_y)
        {
            Rectangle rect = new Rectangle(x, y, w, h);
            return rect.Contains(collision_x, collision_y);
        }
    }
    public class Panel
    {
        public static Panel panel = null;//当前激活的面板
        private static Player.Status last_player_status = Player.Status.WALK;//玩家之前的状态

        public int x;
        public int y;

        public string bitmap_path;
        public Bitmap bitmap;//背景图

        public Button[] button;//面板中所有按钮
        public int default_button = 0;
        public int cancel_button = -1;
        public int current_button = 0;

        public void set(int x0,int y0,string path,
            int default_button0,int cancel_button0)
        {
            x = x0;
            y = y0;
            bitmap_path = path;
            default_button = default_button0;
            cancel_button = cancel_button0;
        }
        public void init()
        {
            //背景图初始化
            if(bitmap_path!=null&&bitmap_path!="")
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }
            //按钮
            if(button!=null)
                for(int i = 0; i < button.Length; i++)
                {
                    if (button[i] == null)
                        continue;
                    button[i].load();
                }
        }
        public void show()
        {
            panel = this;
            current_button = default_button;
            set_button_status(Button.Status.SELECT);
            if (Player.status != Player.Status.PANEL)
                last_player_status = Player.status;
            Player.status = Player.Status.PANEL;
        }
        public void hide()
        {
            panel = null;
            Player.status = last_player_status;
        }
        public void set_button_status(Button.Status status)
        {
            if (button != null)
            {
                for(int i = 0; i < button.Length; i++)
                {
                    if (button[i] == null)
                        continue;
                    button[i].status = Button.Status.NORMAL;
                }
                if (button[current_button] != null)
                    button[current_button].status = status;
            }
        }
        public delegate void Draw_event(Graphics g, int x_offset, int y_offset);
        public event Draw_event draw_event;
        public delegate void Drawbg_event(Graphics g, int x_offset, int y_offset);
        public event Drawbg_event drawbg_event;

        public void draw_me(Graphics g)
        {
            //背景调用
            if (drawbg_event != null)
                drawbg_event(g, this.x, this.y);
            //背景图
            if (bitmap != null)
                g.DrawImage(bitmap, x, y);
            //外部调用
            if (draw_event != null)
                draw_event(g, this.x, this.y);
            //按钮
            if(button!=null)
                for(int i = 0; i< button.Length; i++)
                {
                    if (button[i] == null)
                        continue;
                    button[i].draw(g, x, y);
                }
        }
        public static void draw(Graphics g)
        {
            if (panel != null)
                panel.draw_me(g);
        }
        //键盘操作
        public static void key_ctrl(KeyEventArgs e)
        {
            if (panel != null)
                panel.key_ctrl_me(e);
        }
        public void key_ctrl_me(KeyEventArgs e)
        {
            if (button == null)
                return;
            Button btn = button[current_button];
            if (btn == null)
                return;
            //方向
            int newindex = -1;
            if (e.KeyCode == Keys.Up)
            {
                newindex = btn.key_Ctrl.up;
            }
            else if (e.KeyCode == Keys.Down)
            {
                newindex = btn.key_Ctrl.down;
            }
            else if (e.KeyCode == Keys.Left)
            {
                newindex = btn.key_Ctrl.left;
            }
            else if (e.KeyCode == Keys.Right)
            {
                newindex = btn.key_Ctrl.right;
            }
            if (newindex >= 0 && newindex < button.Length && button[newindex] != null)
            {
                current_button = newindex;
                set_button_status(Button.Status.SELECT);
            }
            //确定
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                set_button_status(Button.Status.PRESS);
                btn.click();
            }
            //取消
            else if (e.KeyCode == Keys.Escape)
            {
                if (cancel_button >= 0 && cancel_button < button.Length)
                    button[cancel_button].click();
            }
        }
        //鼠标操作
        public static void mouse_move(MouseEventArgs e)
        {
            if (panel != null)
                panel.mouse_move_me(e);
        }
        public void mouse_move_me(MouseEventArgs e)
        {
            if (button != null) 
                for(int i = 0; i < button.Length; i++)
                {
                    if (button[i] == null)
                        continue;
                    if (button[i].is_collision(e.X - x, e.Y - y))
                    {
                        current_button = i;
                        set_button_status(Button.Status.SELECT);
                        break;
                    }
                }
        }
        //响应鼠标单击事件
        public static void mouse_click(MouseEventArgs e)
        {
            if (panel != null)
                panel.mouse_click_me(e);
        }
        public void mouse_click_me(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            if(button!=null)
                for(int i = 0; i < button.Length; i++)
                {
                    if (button[i] == null)
                        continue;
                    if (button[i].is_collision(e.X - x, e.Y - y))
                    {
                        current_button = i;
                        set_button_status(Button.Status.PRESS);
                        button[i].click();
                        break;
                    }
                }
        }
    }
}
