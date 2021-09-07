using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace island
{
    //资源文件是嵌入到应用程序中的（exe） 是一开始就调入内存中的，因为资源文件通常不大（大的话就不能作为资源文件嵌入到exe，否则效率就低了）
    public class Map
    {
        public static int current_map = 0;

        //public string map_name;
        public string map_path;
        public Bitmap bitmap;
        //遮挡层
        //public string shade_name;//命名规则：某地图的遮挡层名字在该地图后加p
        public string shade_path;
        public Bitmap shade;

        public string block_path;
        public Bitmap block;

        public string back_path;
        public Bitmap back;

        public string music_path;
        public static bool is_first=true;//同主题地图是否第一次播放该音乐

        public Map()
        {
            map_path = "";
            shade_path = "";
            block_path = "";
            back_path = "";
        }
        public static void change_map(Map[] map,Player[] player,Npc[] npc,int newindex,int x,int y,int face,WMPLib.WindowsMediaPlayer music_player)//切换地图
        {
            //卸载旧地图资源
            if(map[current_map].bitmap!=null)
            {
                map[current_map].bitmap = null;
            }
            if (map[current_map].shade != null)
            {
                map[current_map].shade = null;
            }
            if (map[current_map].block != null)
            {
                map[current_map].block = null;
            }
            if (map[current_map].back != null)
            {
                map[current_map].back = null;
            }
            //加载新地图资源
            // map[newindex].bitmap = Map.name_to_map(map[newindex].map_name);
            if (map[newindex].map_path != null && map[newindex].map_path != "")
            {
                map[newindex].bitmap = new Bitmap(map[newindex].map_path);
                map[newindex].bitmap.SetResolution(96, 96);
            }
            //map[newindex].shade = Map.name_to_shade(map[newindex].shade_name);
            if (map[newindex].shade_path != null && map[newindex].shade_path != "")
            {
                map[newindex].shade = new Bitmap(map[newindex].shade_path);
                map[newindex].shade.SetResolution(96, 96);
            }
            if (map[newindex].block_path != null && map[newindex].block_path != "")
            {
                map[newindex].block = new Bitmap(map[newindex].block_path);
                map[newindex].block.SetResolution(96, 96);
            }
            if (map[newindex].back_path != null && map[newindex].back_path != "")
            {
                map[newindex].back = new Bitmap(map[newindex].back_path);
                map[newindex].back.SetResolution(96, 96);
            }
            //音乐切换,判断是否为同主题地图的音乐
            if (map[current_map].music_path != map[newindex].music_path)
                is_first = true;
            if (is_first == true)
            {
                music_player.URL = map[newindex].music_path;
                is_first = false;
            }
            //变更当前地图
            current_map = newindex;
            //NPC资源
            for(int i = 0; i < npc.Length; i++)
            {
                if (npc[i] == null)
                    continue;
                if (npc[i].map == current_map)//卸载旧的NPC资源
                    npc[i].unload();
                if (npc[i].map == newindex)//加载新的NPC资源
                    npc[i].load();
            }
            //玩家位置
            Player.set_pos(player, x, y, face);
           // music_player.URL = map[current_map].music_path;
        }
        public static int get_map_sx(Map[] map, Player[] player, Rectangle stage)
        {
            Map m = map[current_map];
            if (m.bitmap == null)
                return 0;
            int map_sx = 0;
            int p_x = Player.get_pos_x(player);
            int map_w = m.bitmap.Width;
            if (p_x <= stage.Width / 2)//左右变换
            {
                map_sx = 0;
            }
            else if (p_x >= map_w - stage.Width / 2)
            {
                map_sx = stage.Width - map_w;
            }
            else
            {
                map_sx = stage.Width / 2 - p_x;
            }
            return map_sx;
        }
        public static int get_map_sy(Map[] map, Player[] player, Rectangle stage)
        {
            Map m = map[current_map];
            if (m.bitmap == null)
                return 0;
            int map_sy = 0;
            int p_y = Player.get_pos_y(player);
            int map_h = m.bitmap.Height;
            if (p_y <= stage.Height / 2)//上下变换
            {
                map_sy = 0;
            }
            else if (p_y >= map_h - stage.Height / 2)
            {
                map_sy = stage.Height - map_h;
            }
            else
            {
                map_sy = stage.Height / 2 - p_y;
            }
            return map_sy;
        }
        public static void draw_player_npc(Map[] map,Player[] player,Npc[] npc,Graphics g,int map_sx,int map_sy)
        {   
            //绘制主角和NPC
            Layer_sort[] layer_sort = new Layer_sort[npc.Length+1];
            for(int i = 0; i < npc.Length; ++i)
            {
                if (npc[i] != null)
                {
                    layer_sort[i].y = npc[i].y;
                    layer_sort[i].index = i;
                    layer_sort[i].type = 1;
                }
                else
                {
                    layer_sort[i].y =int.MaxValue;
                    layer_sort[i].index = i;
                    layer_sort[i].type = 1;
                }
            }
            layer_sort[npc.Length].y = Player.get_pos_y(player);
            layer_sort[npc.Length].index = 0;
            layer_sort[npc.Length].type = 0;

            System.Array.Sort(layer_sort, new Layer_sort_comparer());
            for (int i = 0; i < layer_sort.Length; i++)
            {
                //画主角
                if (layer_sort[i].type == 0)
                {
                    Player.draw(player, g, map_sx, map_sy);
                }
                //画npc
                else if (layer_sort[i].type == 1)
                {
                    int index = layer_sort[i].index;
                    if (npc[index] == null)
                        continue;
                    if (npc[index].map != current_map)
                        continue;
                    npc[index].draw(g, map_sx, map_sy);
                }
            }
        }
        public static void draw(Map[] map,Player[] player,Npc[] npc,Graphics g,Rectangle stage)
        {
            Map m = map[current_map];
            int map_sx = get_map_sx(map,player,stage);
            int map_sy = get_map_sy(map, player, stage); ;
            if (m.back != null)
                g.DrawImage(m.back, 0, 0);
            if (m.bitmap!=null)
            g.DrawImage(m.bitmap, map_sx, map_sy);

            draw_player_npc(map, player, npc, g, map_sx, map_sy);
            if(m.shade!=null)
            g.DrawImage(m.shade, map_sx, map_sy);

            Player.draw_flag(g, map_sx, map_sy);
        }
        public static bool can_through(Map[] map,int x,int y) {
            Map m = map[current_map];

            if (x < 0) return false;
            else if (m.block!=null&&x >= m.block.Width)
            {
                return false;
            }
            else if (y < 0) return false;
            else if (m.block != null && y >= m.block.Height) return false;
            if (m.block != null && m.block.GetPixel(x, y).B == 0)
                return false;
            else
                return true;
        }
    }
}
