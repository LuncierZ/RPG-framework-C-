using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace island
{
    public struct Layer_sort
    {
        public int y;
        public int index;
        //0表示主角，1表示npc
        public int type;
    }
    public class Npc
    {
        public enum Collision_type
        {
            KEY=1,
            ENTER=2,
        }
        public enum Npc_type
        {
            NORMAL=0,
            CHARACTER=1,//能够行走，定义变量的NPC
        }
        //NPC类型
        public Npc_type npc_Type = Npc_type.NORMAL;
        //行走控制
        public Comm.Direction face = Comm.Direction.DOWN;
        public int walk_frame = 0;
        public long last_walk_time = 0;
        public long walk_interval = 80;
        public int speed = 10;
        public Comm.Direction idle_walk_direction = Comm.Direction.DOWN;//徘徊方式（上下？左右？）
        public int idle_walk_time = 0;//往每个方向走的最大帧数
        public int idle_walk_time_now = 0;//当前行走时间
        public bool if_block = true;//是否被地形禁锢
        //触发事件条件
        public Collision_type collision_Type = Collision_type.KEY;
        //位置信息
        public int map = -1;
        public int x = 0;
        public int y = 0;
        public int x_offset = 0;
        public int y_offset = 0;
        //显示
        public string bitmap_path = "";
        public Bitmap bitmap;
        public bool visible = true;
        //提示框
        public string tips_path = "";
        public Bitmap tipsmap;
        public bool tips_visible = false;
        public int x_tips_offset = 0;
        public int y_tips_offset = 0;
        //碰撞判定区
        public int region_x = 0;
        public int region_y = 0;
        //是否使用默认偏移值与判定区
        public bool if_init = false;
        //动画控制
        public Animation[] anm;
        public int anm_frame = 0;//当前播放的帧
        public int current_anm = -1;//当前使用的动画
        public long last_anm_time = 0;//上一帧播放的时间,用于调控播放速率
        //鼠标碰撞区域
        public int mc_xoffset = 0;
        public int mc_yoffset = -30;
        public int mc_w = 100;
        public int mc_h = 150;//NPC碰撞区域
        public static int mc_distance_x = 300;
        public static int mc_distance_y = 200;//NPC在何区域内生效

        //加载,改这些参数加载完再改
        public void load() {
            if (bitmap_path != "")
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
                if (npc_Type != Npc_type.CHARACTER)//行走图角色的offset为每一帧的
                {
                    x_offset = -(bitmap.Width / 2);
                    y_offset = -(bitmap.Height - bitmap.Height / 10);
                }
                else
                {
                    x_offset = -(bitmap.Width/4 / 2);
                    y_offset = -(bitmap.Height/4 - bitmap.Height/4 / 10);
                }
                if (if_init == true)
                {
                    if (npc_Type != Npc_type.CHARACTER)
                    {
                        region_x = bitmap.Width / 3;
                        region_y = bitmap.Height / 5;
                    }
                    else
                    {
                        region_x = bitmap.Width/4 ;
                        region_y = bitmap.Height /4;
                    }
                }
            }
            if (tips_path != "")
            {
                tipsmap = new Bitmap(tips_path);
                tipsmap.SetResolution(96, 96);
            }
            if (anm != null)
            {
                for (int i = 0; i < anm.Length; ++i)
                    anm[i].load();
            }
            //鼠标控制相关
            if (bitmap != null)
            {
                if (npc_Type == Npc_type.NORMAL)
                {
                    if (mc_w == 0)
                        mc_w = bitmap.Width;
                    if (mc_h == 0)
                        mc_h = bitmap.Height;
                }
                else if (npc_Type == Npc_type.CHARACTER)
                {
                    if (mc_w == 0)
                        mc_w = bitmap.Width/4;
                    if (mc_h == 0)
                        mc_h = bitmap.Height/4;
                }
            }
            else
            {
                if (mc_w == 0)
                    mc_w = region_x;
                if (mc_h == 0)
                    mc_h = region_y;
            }
        }
        //卸载
        public void unload()
        {
            if (bitmap != null)
            {
                bitmap = null;
            }
            if (tipsmap != null)
            {
                tipsmap = null;
            }
            if (anm != null)
            {
                for (int i = 0; i < anm.Length; ++i)
                    anm[i].unload();
            }
        }
        //绘制
        public void draw(Graphics g,int map_sx,int map_sy) {
            if (visible != true)
                return;
            if (tips_visible == true&&tipsmap!=null)
                g.DrawImage(tipsmap, map_sx + x + x_offset+x_tips_offset, map_sy + y + y_offset+y_tips_offset);
            if (current_anm < 0)
            {
                if (npc_Type == Npc_type.NORMAL)
                {
                    if (bitmap != null)
                        g.DrawImage(bitmap, map_sx + x + x_offset, map_sy + y + y_offset);
                }
                else if (npc_Type == Npc_type.CHARACTER)
                {
                    draw_character(g, map_sx, map_sy);
                }
            }
            else
            {
                draw_anm(g, map_sx, map_sy);
            }
        }
        //绘制动画
        public void draw_anm(Graphics g,int map_sx,int map_sy)
        {
            if (anm == null || current_anm >= anm.Length || anm[current_anm] == null || anm[current_anm].bitmap_path == null)
            {
                current_anm = -1;
                anm_frame = 0;
                last_anm_time = 0;
                return;
            }
            anm[current_anm].draw(g, anm_frame, map_sx + x + x_offset, y + map_sy + y_offset);
            if (Comm.Time() - last_anm_time >= Animation.RATE)
            {
                anm_frame = anm_frame + 1;
                last_anm_time = Comm.Time();
                if (anm_frame / anm[current_anm].anm_rate >= anm[current_anm].max_frame)
                {
                    current_anm = -1;
                    anm_frame = 0;
                    last_anm_time = 0;
                }
            }
        }
        //绘制行走角色
        public void draw_character(Graphics g,int map_sx,int map_sy)
        {
            Rectangle rect = new Rectangle(bitmap.Width / 4 * (walk_frame % 4), bitmap.Height / 4 * ((int)face - 1), bitmap.Width / 4, bitmap.Height / 4);
            Bitmap bitmap0 = bitmap.Clone(rect, bitmap.PixelFormat);
            g.DrawImage(bitmap0, map_sx + x + x_offset, map_sy + y + y_offset);
        }
        public void play_anm(int index)
        {
            current_anm = index;
            anm_frame = 0;
        }
        public void walk(Map[] map,Comm.Direction direction,bool is_block) {
            //转向
            face = direction;
            //间隔判定
            if (Comm.Time() - last_walk_time <= walk_interval)
                return;
            //行走
            if (direction == Comm.Direction.UP && (!is_block || Map.can_through(map, x, y - speed)))
            {
                y = y - speed;
            }
            else if (direction == Comm.Direction.DOWN && (!is_block || Map.can_through(map, x, y + speed)))
            {
                y = y + speed;
            }
            else if (direction == Comm.Direction.LEFT && (!is_block || Map.can_through(map, x - speed, y )))
            {
                x = x - speed;
            }
            else if (direction == Comm.Direction.RIGHT && (!is_block || Map.can_through(map, x + speed, y)))
            {
                x = x + speed;
            }
            //动画帧
            walk_frame = walk_frame + 1;
            if (walk_frame >= int.MaxValue) walk_frame = 0;
            //时间
            last_walk_time = Comm.Time();
            //碰撞检测
            //Player.npc_collision(player, map, npc, e);
        }
        public void stop_walk()
        {
            walk_frame = 0;
            last_anm_time = 0;//停止行走脸朝前
        }
        public void timer_logic(Map[] map)
        {
            if (npc_Type == Npc_type.CHARACTER && idle_walk_time != 0)
            {
                Comm.Direction direction;
                if (idle_walk_time_now >= 0)
                    direction = idle_walk_direction;
                else
                    direction = Comm.opposite_direction(idle_walk_direction);
                walk(map, direction, if_block);
                if (idle_walk_time_now >= 0)
                {
                    idle_walk_time_now = idle_walk_time_now + 1;
                    if (idle_walk_time_now > idle_walk_time)
                        idle_walk_time_now =- 1;
                }
                else if (idle_walk_time_now < 0)
                {
                    idle_walk_time_now = idle_walk_time_now - 1;
                    if (idle_walk_time_now < -idle_walk_time)
                        idle_walk_time_now = 1;
                }
            }
        }
        //碰撞检测
        public bool is_collision(int collision_x,int collision_y) {
            Rectangle rect = new Rectangle(x - region_x / 2, y - region_y / 2, region_x, region_y);
            return rect.Contains(new Point(collision_x,collision_y));
        }
        //线碰撞
        public bool is_line_collsion(Point p1,Point p2)
        {
            if (is_collision(p2.X, p2.Y)) return true;

            int px, py;

            px = p1.X + (p2.X - p1.X) / 2;
            py = p1.Y + (p2.Y - p1.Y) / 2;
            if (is_collision(px, py)) return true;

            px = p2.X - (p2.X - p1.X) / 4;
            py = p2.Y - (p2.Y - p1.Y) / 4;
            if (is_collision(px, py)) return true;

            return false;
        }
        //鼠标碰撞
        public bool is_mouse_collision(int collision_x,int collision_y)
        {
            if (Fight.fighting == 1)
                return false;//bug修复
            //有图
            if (bitmap != null)
            {
                if (npc_Type == Npc_type.NORMAL)
                {
                    int center_x = x + x_offset + bitmap.Width / 2;
                    int center_y = y + y_offset + bitmap.Height / 2;
                    Rectangle rect = new Rectangle(center_x - mc_w / 2, center_y - mc_h / 2, mc_w, mc_h);
                    return rect.Contains(new Point(collision_x, collision_y));
;                }
                else
                {
                    int center_x = x + x_offset + bitmap.Width /4/ 2;
                    int center_y = y + y_offset + bitmap.Height /4/ 2;
                    Rectangle rect = new Rectangle(center_x - mc_w / 2, center_y - mc_h / 2, mc_w, mc_h);
                    return rect.Contains(new Point(collision_x, collision_y));
                }
            }
            //无图
            else
            {
                Rectangle rect = new Rectangle(x - mc_w / 2, y - mc_h / 2, mc_w, mc_h);
                return rect.Contains(new Point(collision_x, collision_y));
            }
        }
        //距离检测
        public bool check_mc_distance(Npc npc,int player_x,int player_y)
        {
            Rectangle rect = new Rectangle(npc.x - mc_distance_x / 2, npc.y - mc_distance_y / 2, mc_distance_x, mc_distance_y);
            return rect.Contains(new Point(player_x, player_y));
        }
        //鼠标操作
        public static void mouse_click(Map[] map,Player[] player,Npc[] npc,Rectangle stage,MouseEventArgs e)
        {
            if (Player.status != Player.Status.WALK)
                return;
            if (npc == null)
                return;
            for(int i = 0; i < npc.Length; i++)
            {
                if (npc[i] == null)
                    continue;
                if (npc[i].map != Map.current_map)
                    continue;

                int collision_x = e.X - Map.get_map_sx(map, player, stage);
                int collision_y = e.Y - Map.get_map_sy(map, player, stage);
                if (!npc[i].is_mouse_collision(collision_x, collision_y))
                    continue;

                //距离
                if (!npc[i].check_mc_distance(npc[i], Player.get_pos_x(player), Player.get_pos_y(player)))
                {
                    Player.stop_walk(player);
                    Message.showtip("请走近些");
                    Task.block();
                    continue;
                }
                Player.stop_walk(player);
                Task.story(i);
            }
        }
        //鼠标碰撞检测：0-没有，1-有
        public static int check_mouse_collision(Map[] map,Player[] player,Npc[] npc,Rectangle stage,MouseEventArgs e)
        {
            if (Player.status != Player.Status.WALK)
                return 0;
            if (npc == null)
                return 0;
            for(int i = 0; i < npc.Length; i++)
            {
                if (npc[i] == null)
                    continue;
                if (npc[i].map != Map.current_map)
                    continue;

                int collision_x = e.X - Map.get_map_sx(map, player, stage);
                int collision_y = e.Y - Map.get_map_sy(map, player, stage);
                if (npc[i].is_mouse_collision(collision_x, collision_y))
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
