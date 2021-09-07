using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace island
{
    public class Player
    {
        public int x = 0;
        public int y = 0;
        public int offset_x = -144;
        public int offset_y = -122;
        public int face = 1;
        public int anm_frame = 0;
        public long last_walk_time = 0;
        public long walk_interval = 100;//行走频率,单位毫秒
        public int speedx = 10, speedy = 10;
        public Bitmap bitmap;

        public static int current_player = 0;
        public int is_active = 0;
        public static int select_player = 0;
        //碰撞检测相关
        public int collision_ray = 20;
        //主角状态
        public enum Status
        {
            WALK=1,
            PANEL=2,
            TASK=3,
            FIGHT=4,
        }
        public static Status status = Status.WALK;
        //鼠标操作
        public static int target_x = -1;
        public static int target_y = -1;
        //地图标记
        public static Bitmap move_flag;
        public static long FLAG_SHOW_TIME = 10000;
        public static long flag_start_time = 0;
        //角色状态（物品技能相关）
        public Bitmap status_bitmap;//当前选择角色的图
        public Bitmap statusMenu_bitmap;
        public int max_hp = 100;
        public int hp = 100;
        public int max_mp = 100;
        public int mp = 100;//体力
        public int max_mentality = 100;//精神力
        public int mentality = 100;
        public int max_memory = 10;
        public int memory = 0;//记忆恢复度
        public int attack = 10;
        public int defense = 30;
        public int fspeed = 10;
        public int fortune = 10;
        public static int money = 0;//钱
        public int equip_att = -1;
        public int equip_def = -1;
        public int[] skill = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        //战斗
        public Bitmap fbitmap;
        public int fx_offset = 0;
        public int fy_offset = 0;
        public Bitmap fface;
        public Animation anm_att;
        public Animation anm_item;
        public Animation[] anm_item1=new Animation[10];
        public Animation anm_skill;
        public Animation[] anm_skill1 = new Animation[10];
        public string name = "";

        //方法
        public Player() {
            bitmap = Properties.Resources.团长;
        }
        public Player(Bitmap bitmap,int sx,int sy,int face,int rate) {
            if (bitmap == null)
                bitmap = Properties.Resources.团长;
            this.bitmap = bitmap;
            bitmap.SetResolution(96, 96);
            speedx = sx;
            speedy = sy;
            this.face = face;
            walk_interval = rate;
            offset_x = -bitmap.Width / 4 / 2;
            offset_y = -(bitmap.Height/4 - bitmap.Height/4/ 10);
            collision_ray = sx;
            move_flag = new Bitmap(@"resources\picture\地图标记2.png");
            move_flag.SetResolution(96, 96);
        }

        public static void Key_ctrl(KeyEventArgs e,Player[] player,Npc[] npc,Map[] map) {
            if (Player.status != Status.WALK)
                return;
            Player p = player[current_player];
            if (e.KeyCode == Keys.Tab)
            {
                key_change_player(player);
            }
            if (e.KeyCode == Keys.Up )
            {
                walk(player, map, Comm.Direction.UP);
            }
            else if (e.KeyCode == Keys.Down )
            {
                walk(player, map, Comm.Direction.DOWN);
            }
            else if (e.KeyCode == Keys.Left )
            {
                walk(player, map, Comm.Direction.LEFT);
            }
            else if (e.KeyCode == Keys.Right)
            {
                walk(player, map, Comm.Direction.RIGHT);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                StatusMenu.show();
                Task.block();
            }
            else if (e.KeyCode == Keys.H)
            {
                Title.confirm2.show();
            }
            npc_collision(player,map,npc,e);
        }
        public static void walk(Player[] player, Map[] map, Comm.Direction direction)
        {
            Player p = player[current_player];
            //转向
            p.face = (int)direction;
            //间隔判定
            if (Comm.Time() - p.last_walk_time <= p.walk_interval)
                return;
            //行走
            if (direction==Comm.Direction.UP && Map.can_through(map, p.x, p.y - p.speedy))
            {
                p.y = p.y - p.speedy;
            }
            else if (direction == Comm.Direction.DOWN && Map.can_through(map, p.x, p.y + p.speedy))
            {
                p.y = p.y + p.speedy;
            }
            else if (direction == Comm.Direction.LEFT && Map.can_through(map, p.x - p.speedx, p.y))
            {
                p.x = p.x - p.speedx;
            }
            else if (direction == Comm.Direction.RIGHT && Map.can_through(map, p.x + p.speedx, p.y))
            {
                p.x = p.x + p.speedx;
            }
            //动画帧
            p.anm_frame = p.anm_frame + 1;
            if (p.anm_frame >= int.MaxValue)
                p.anm_frame = 0;
            //时间
            p.last_walk_time = Comm.Time();
        }
        public static void Key_ctrl_Up(Player[] player,KeyEventArgs e)
        {
            stop_walk(player);
            /*
            Player p = player[current_player];
            p.anm_frame = 0;
            p.last_walk_time = 0;
           */
        }
        public static void draw(Player[] player,Graphics g,int map_sx,int map_sy) {
            //自定义绘图
            Player p = player[current_player];
            Rectangle rec1 = new Rectangle(p.bitmap.Width / 4 * (p.anm_frame % 4),
                p.bitmap.Height / 4 * (p.face - 1), p.bitmap.Width / 4, p.bitmap.Height / 4);//定义区域
            Bitmap bitmap0 = p.bitmap.Clone(rec1, p.bitmap.PixelFormat);
            g.DrawImage(bitmap0, map_sx+p.x+p.offset_x, map_sy+p.y+p.offset_y);
        }
        public static void key_change_player(Player[] player)
        {
            for(int i=current_player+1;i<player.Length;++i)
                if (player[i].is_active == 1)
                {
                    set_player(player, current_player, i);
                    return;
                }//第一次循环
            for(int i=0;i<current_player;i++)
                if (player[i].is_active == 1)
                {
                    set_player(player, current_player, i);
                    return;
                }//第二次循环
        }
        public static void set_player(Player[] player, int oldindex, int newindex) {
            current_player = newindex;
            player[newindex].x = player[oldindex].x;
            player[newindex].y = player[oldindex].y;
            player[newindex].face = player[oldindex].face;
        }
        public static void set_pos(Player[] player,int x,int y,int face)
        {
            player[current_player].x = x;
            player[current_player].y = y;
            player[current_player].face = face;
        }
        public static int get_pos_x(Player[] player)
        {
            return player[current_player].x;
        }
        public static int get_pos_y(Player[] player)
        {
            return player[current_player].y;
        }
        public static int get_pos_f(Player[] player)
        {
            return player[current_player].face;
        }
        public static Point get_collsion_point(Player[] player)
        {
            Player p = player[current_player];
            int collision_x = 0;
            int collision_y = 0;

            if (p.face == (int)Comm.Direction.UP)
            {
                collision_x = p.x;
                collision_y = p.y - p.collision_ray;
            }
            if (p.face == (int)Comm.Direction.DOWN)
            {
                collision_x = p.x;
                collision_y = p.y + p.collision_ray;
            }
            if (p.face == (int)Comm.Direction.LEFT)
            {
                collision_x = p.x-p.collision_ray;
                collision_y = p.y;
            }
            if (p.face == (int)Comm.Direction.RIGHT)
            {
                collision_x = p.x + p.collision_ray;
                collision_y = p.y;
            }
            return new Point(collision_x, collision_y);
        }
        public static void npc_collision(Player[] player,Map[] map,Npc[] npc,KeyEventArgs e)
        {
            Player p = player[current_player];
            Point p1 = new Point(p.x, p.y);
            Point p2 = get_collsion_point(player);

            for(int i = 0; i < npc.Length; ++i)
            {
                if (npc[i] == null)
                    continue;
                if (npc[i].map != Map.current_map)
                    continue;
                if (npc[i].is_line_collsion(p1, p2))
                {
                    npc[i].tips_visible = true;
                    if (npc[i].collision_Type == Npc.Collision_type.ENTER)//触碰触发
                    {
                        Task.story(i);
                        break;
                    }
                    else if(npc[i].collision_Type==Npc.Collision_type.KEY)//按键触发
                    {
                        if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
                        {
                            Task.story(i);
                            break;
                        }
                    }
                }
                else npc[i].tips_visible = false;
            }
        }
        //鼠标操作
        //角色是否到达目的地
        public static int is_reach_x(Player[] player,int target_x)
        {
            Player p = player[current_player];
            if (p.x - target_x > p.speedx / 2) return 1;
            if (p.x - target_x < -p.speedx / 2) return -1;
            return 0;
        }
        public static int is_reach_y(Player[] player, int target_y)
        {
            Player p = player[current_player];
            if (p.y - target_y > p.speedy / 2) return 1;
            if (p.y - target_y < -p.speedy / 2) return -1;
            return 0;
        }
        //角色处于行走状态时，按下左键
        public static void mouse_click(Map[] map,Player[] player,Rectangle stage,MouseEventArgs e)
        {
            if (Player.status != Status.WALK)
                return;
            if (e.Button == MouseButtons.Left)
            {
                target_x = e.X - Map.get_map_sx(map, player, stage);
                target_y = e.Y - Map.get_map_sy(map, player, stage);
                flag_start_time = Comm.Time();
            }
            else if (e.Button == MouseButtons.Right)
            {
                StatusMenu.show();
                Task.block();
            }
        }
        public static void timer_logic(Player[] player,Map[] map)
        {
            move_logic(player, map);
        }
        public static void move_logic(Player[] player,Map[] map)
        {
            if (target_x < 0 || target_y < 0)
                return;
            step_to(player, map, target_x, target_y);
        }
        //角色停止行走
        public static void stop_walk(Player[] player)
        {
            Player p = player[current_player];
            //动画帧
            p.anm_frame = 0;
            p.last_walk_time = 0;
            //目标位置
            target_x = -1;
            target_y = -1;
        }
        //简单路径判定算法
        public static void step_to(Player[] player,Map[] map,int target_x,int target_y)
        {
            if (is_reach_x(player, target_x) == 0 && is_reach_y(player, target_y) == 0)
            {
                stop_walk(player);
                return;
            }

            Player p = player[current_player];
            //能够往x的正方向行走
            if (is_reach_x(player, target_x) > 0 && Map.can_through(map, p.x - p.speedx, p.y))
            {
                walk(player, map, Comm.Direction.LEFT);
                return;
            }
            //能否往x的反方向行走
            else if(is_reach_x(player, target_x) < 0 && Map.can_through(map, p.x + p.speedx, p.y))
            {
                walk(player, map, Comm.Direction.RIGHT);
                return;
            }

            //y的正反方向
            if (is_reach_y(player, target_y) > 0 && Map.can_through(map, p.x, p.y - p.speedy))
            {
                walk(player, map, Comm.Direction.UP);
                return;
            }
            else if (is_reach_y(player, target_y) < 0 && Map.can_through(map, p.x, p.y + p.speedy))
            {
                walk(player, map, Comm.Direction.DOWN);
                return;
            }

            //无路可走
            stop_walk(player);
        }
        //绘制标记
        public static void draw_flag(Graphics g,int map_sx,int map_sy)
        {
            if (target_x < 0 || target_y < 0)
                return;
            if (move_flag == null)
                return;
            if (Comm.Time() - flag_start_time > FLAG_SHOW_TIME)
                return;
            g.DrawImage(move_flag, map_sx + target_x - 16, map_sy + target_y - 25);
        }
        //画精神值
        public void draw_mentality(Graphics g,int x,int y)
        {
            Font font_m = new Font("黑体", 16);
            Brush brush_m1 = Brushes.GreenYellow;
            Brush brush_m2 = Brushes.Red;
            if (mentality>=20)
            {
                g.DrawString("精神值："+mentality.ToString(),
                       font_m, brush_m1,
                      x, y, new StringFormat());
            }
            else
            {
                g.DrawString("精神值：" + mentality.ToString(),
                       font_m, brush_m2,
                      x, y, new StringFormat());
            }
        }
        //战斗
        public void fset(string name,string fbitmap_path,string fface_path, int fx_offset,int fy_offset,
            Animation anm_att,Animation anm_item,Animation anm_skill)
        {
            this.name = name;
            if (fbitmap_path != null && fbitmap_path != "")
            {
                this.fbitmap = new Bitmap(fbitmap_path);
                this.fbitmap.SetResolution(96, 96);
            }
            this.fx_offset = fx_offset;
            this.fy_offset = fy_offset;
            if (fface_path != null && fface_path != "")
            {
                this.fface = new Bitmap(fface_path);
                this.fface.SetResolution(96, 96);
            }

            this.anm_att = anm_att;
            this.anm_item = anm_item;
            this.anm_skill = anm_skill;

            anm_att.load();
            anm_item.load();
            anm_skill.load();
        }
        public void fset(string name, string fbitmap_path,string fface_path,
            Animation anm_att, Animation anm_item, Animation anm_skill)
        {
            this.name = name;
            if (fbitmap_path != null && fbitmap_path != "")
            {
                this.fbitmap = new Bitmap(fbitmap_path);
                this.fbitmap.SetResolution(96, 96);
            }
            fx_offset = fbitmap.Width / 4 / 2;
            fy_offset = fbitmap.Height - fbitmap.Height / 10;
            if (fface_path != null && fface_path != "")
            {
                this.fface = new Bitmap(fface_path);
                this.fface.SetResolution(96, 96);
            }
            this.anm_att = anm_att;
            this.anm_item = anm_item;
            this.anm_skill = anm_skill;

            anm_att.load();
            anm_item.load();
            anm_skill.load();
        }
    }
}
