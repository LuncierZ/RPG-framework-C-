using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace island
{
    public class Save
    {
        //面板
        private static Panel pan_save = new Panel();
        public static Panel pan_confirm = new Panel();
        private static int menu = 0;
        private static int page = 1;
        private static int selnow = 1;
        public static Bitmap bitmap_sel;

        private static string[] info;

        public static void init()
        {
            bitmap_sel = new Bitmap(@"resources\StatusMenu\物品选框.png");
            bitmap_sel.SetResolution(96, 96);

            Button previous_page = new Button();
            previous_page.set(22, 260, 0, 0, "resources/StatusMenu/上一页n.png", "resources/StatusMenu/上一页s.png", "resources/StatusMenu/上一页s.png", -1, -1, -1, -1);
            previous_page.click_Event += new Button.Click_event(click_previous_page);

            Button next_page = new Button();
            next_page.set(191, 260, 0, 0, "resources/StatusMenu/下一页n.png", "resources/StatusMenu/下一页s.png", "resources/StatusMenu/下一页s.png", -1, -1, -1, -1);
            next_page.click_Event += new Button.Click_event(click_next_page);

            Button save = new Button();
            save.set(107, 260, 0, 0, "resources/picture/储存n.png", "resources/picture/储存s.png", "resources/picture/储存s.png", -1, -1, -1, -1);
            save.click_Event += new Button.Click_event(click_save);

            Button load = new Button();
            load.set(107, 260, 0, 0, "resources/picture/读取n.png", "resources/picture/读取s.png", "resources/picture/读取s.png", -1, -1, -1, -1);
            load.click_Event += new Button.Click_event(click_load);

            Button close = new Button();
            close.set(258, 0, 0, 0, "resources/StatusMenu/关闭n.png", "resources/StatusMenu/关闭s.png", "resources/StatusMenu/关闭s.png", -1, -1, -1, -1);
            close.click_Event += new Button.Click_event(click_close);

            Button sel1 = new Button();
            sel1.set(22, 51, 0, 0, "resources/StatusMenu/物品选框n.png", "resources/StatusMenu/物品选框.png", "resources/StatusMenu/物品选框.png", -1, -1, -1, -1);
            sel1.click_Event += new Button.Click_event(click_sel1);

            Button sel2 = new Button();
            sel2.set(22, 114, 0, 0, "resources/StatusMenu/物品选框n.png", "resources/StatusMenu/物品选框.png", "resources/StatusMenu/物品选框.png", -1, -1, -1, -1);
            sel2.click_Event += new Button.Click_event(click_sel2);

            Button sel3 = new Button();
            sel3.set(22, 177, 0, 0, "resources/StatusMenu/物品选框n.png", "resources/StatusMenu/物品选框.png", "resources/StatusMenu/物品选框.png", -1, -1, -1, -1);
            sel3.click_Event += new Button.Click_event(click_sel3);

            Button yes = new Button();
            yes.set(23, 126, 0, 0, "resources/picture/确定n.png", "resources/picture/确定s.png", "resources/picture/确定s.png", -1, -1, -1, -1);
            yes.click_Event += new Button.Click_event(click_yes);

            Button no = new Button();
            no.set(140, 126, 0, 0, "resources/picture/取消n.png", "resources/picture/取消s.png", "resources/picture/取消s.png", -1, -1, -1, -1);
            no.click_Event += new Button.Click_event(click_no);

            Button under = new Button();//空图片
            under.set(-100, -100, 2000, 2000, "", "", "", -1, -1, -1, -1);

            pan_save.button = new Button[9];
            pan_save.button[0] = previous_page;
            pan_save.button[1] = next_page;
            pan_save.button[2] = save;
            pan_save.button[3] = load;
            pan_save.button[4] = close;
            pan_save.button[5] = sel1;
            pan_save.button[6] = sel2;
            pan_save.button[7] = sel3;
            pan_save.button[8] = under;
            pan_save.set(380, 151, "resources/picture/存取面板背景.png", 8, 4);
            pan_save.draw_event += new Panel.Draw_event(draw_save);
            pan_save.drawbg_event += new Panel.Drawbg_event(pan_save_drawbg);
            pan_save.init();

            pan_confirm.button = new Button[2];
            pan_confirm.button[0] = yes;
            pan_confirm.button[1] = no;
            pan_confirm.set(403, 201, "resources/picture/存取面板确认.png", 0, 1);
            pan_confirm.draw_event += new Panel.Draw_event(draw_confirm);
            pan_confirm.drawbg_event += new Panel.Drawbg_event(pan_confirm_drawbg);
            pan_confirm.init();
        }

        private static void draw_confirm(Graphics g, int x_offset, int y_offset)
        {
            //
        }

        private static void draw_save(Graphics g, int x_offset, int y_offset)
        {
            //标签
            Font font = new Font("黑体", 20);
            Brush brush = Brushes.White;
            if (menu == 0)
                g.DrawString("存储", font, brush, x_offset + 105, y_offset + 9, new StringFormat());
            else
                g.DrawString("读取", font, brush, x_offset + 105, y_offset + 9, new StringFormat());
            //显示信息
            drawinfo(g, x_offset, y_offset);
            //显示选择框
            g.DrawImage(bitmap_sel, x_offset + 22, y_offset + 51 + (selnow - 1) * 63);
        }
        private static void pan_confirm_drawbg(Graphics g, int x_offset, int y_offset)
        {
            Save.pan_save.draw_me(g);
        }

        private static void pan_save_drawbg(Graphics g, int x_offset, int y_offset)
        {
            if (menu == 1)
                Title.title.draw_me(g);
        }
        public static void drawinfo(Graphics g,int x_offset,int y_offset)
        {
            Font font_n = new Font("黑体", 12);
            Brush brush_n = Brushes.GreenYellow;
            Font font_d = new Font("黑体", 10);
            Brush brush_d = Brushes.LawnGreen;
            for(int i = 0; i < 3; i++)
            {
                string str = "存档" + ((page - 1) * 3 + i).ToString();
                g.DrawString(str,font_n,brush_n, x_offset + 86, y_offset + 53 + i * 63, new StringFormat());
                g.DrawString(info[i],font_d,brush_d, x_offset + 86, y_offset + 74 + i * 63, new StringFormat());
            }
        }

        public static void show(int menu)
        {
            page = 1;
            Save.menu = menu;
            pan_save.show();
            //保存
            if (menu == 0)
            {
                pan_save.button[2].x = 107;
                pan_save.button[2].y = 260;
                pan_save.button[3].x = -9000;
                pan_save.button[3].y = -9000;
            }
            //读取
            else
            {
                pan_save.button[3].x = 107;
                pan_save.button[3].y = 260;
                pan_save.button[2].x = -9000;
                pan_save.button[2].y = -9000;
            }
            info = get_save_info((page - 1) * 3);
        }

        private static void click_no()
        {
            Save.show(0);
        }

        private static void click_yes()
        {
            int index = (page - 1) * 3 + selnow - 1;
            save(index);
            pan_save.hide();
        }

        private static void click_load()
        {
            int index = (page - 1) * 3 + selnow - 1;
            if (!File.Exists("save" + index.ToString() + ".dat"))
                return;
            load(index);
        }

        private static void click_save()
        {
            int index = (page - 1) * 3 + selnow - 1;
            if (!File.Exists("save" + index.ToString() + ".dat"))
                save(index);
            else
                pan_confirm.show();
            info = get_save_info((page - 1) * 3);
        }

        private static void click_sel3()
        {
            selnow = 3;
        }

        private static void click_sel2()
        {
            selnow = 2;
        }

        private static void click_sel1()
        {
            selnow = 1;
        }

        private static void click_close()
        {
            if (menu == 0)
                pan_save.hide();
            else
                Title.show();
        }
        private static void click_next_page()
        {
            page++;
            info = get_save_info((page - 1) * 3);
        }

        private static void click_previous_page()
        {
            page--;
            if (page < 1) page = 1;
            info= get_save_info((page - 1) * 3);
        }
        //获取保存的文件信息
        public static string[] get_save_info(int start)
        {
            string[] ret = new string[] { "", "", "" };
            for(int i = 0; i < 3; i++)
            {
                if (!File.Exists("save" + (start + i).ToString() + ".dat"))
                    continue;
                try
                {
                    FileStream fs = new FileStream("save" + (start + i).ToString() + ".dat", FileMode.Open);
                    BinaryReader br = new BinaryReader(fs);
                    //时间
                    ret[i] = br.ReadString();
                    br.Close();
                    fs.Close();
                }
                catch
                {
                    MessageBox.Show("读取文件信息出错");
                }
            }
            return ret;
        }
        //保存
        public static void save(int index)
        {
            try
            {
                FileStream fs = new FileStream("save" + index.ToString() + ".dat", FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                //时间
                bw.Write(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
                //剧情变量
                for(int i = 0; i < Task.p.Length; i++)
                {
                    bw.Write(Task.p[i]);
                }
                //地图数据
                bw.Write(Map.current_map);
                //角色数据
                bw.Write(Player.current_player);
                bw.Write(Player.select_player);
                bw.Write(Player.money);
                for(int i = 0; i < Island.player.Length; i++)
                {
                    if (Island.player[i] == null)
                        continue;
                    Player p = Island.player[i];
                    bw.Write(p.x);
                    bw.Write(p.y);
                    bw.Write(p.face);

                    bw.Write(p.walk_interval);
                    bw.Write(p.speedx);
                    bw.Write(p.speedy);
                    bw.Write(p.offset_x);
                    bw.Write(p.offset_y);
                    bw.Write(p.max_hp);
                    bw.Write(p.hp);
                    bw.Write(p.max_mp);
                    bw.Write(p.mp);
                    bw.Write(p.max_mentality);
                    bw.Write(p.mentality);
                    bw.Write(p.max_memory);
                    bw.Write(p.memory);
                    bw.Write(p.attack);
                    bw.Write(p.defense);
                    bw.Write(p.fspeed);
                    bw.Write(p.fortune);
                    bw.Write(p.equip_att);
                    bw.Write(p.equip_def);
                    bw.Write(p.fx_offset);
                    bw.Write(p.fy_offset);
                    bw.Write(p.name);
                    bw.Write(p.is_active);
                    bw.Write(p.collision_ray);
                    //角色技能
                    for(int i2 = 0; i2 < 10; i2++)
                    {
                        bw.Write(p.skill[i2]);
                    }
                }
                //NPC数据
                for(int i = 0; i < Island.npc.Length; i++)
                {
                    if (Island.npc[i] == null)
                        continue;
                    Npc n = Island.npc[i];
                    bw.Write(n.map);
                    bw.Write(n.x);
                    bw.Write(n.y);
                    bw.Write(n.x_offset);
                    bw.Write(n.y_offset);
                    bw.Write(n.bitmap_path);
                    bw.Write(n.tips_visible);
                    bw.Write(n.visible);
                    bw.Write(n.region_x);
                    bw.Write(n.region_y);
                    bw.Write((int)n.face);
                    bw.Write(n.walk_frame);
                    bw.Write(n.walk_interval);
                    bw.Write(n.idle_walk_time);
                    bw.Write(n.idle_walk_time_now);
                    bw.Write(n.mc_xoffset);
                    bw.Write(n.mc_yoffset);
                    bw.Write(n.mc_w);
                    bw.Write(n.mc_h);
                }
                //物品
                for(int i = 0; i < Item.item.Length; i++)
                {
                    if (Item.item[i] == null)
                        continue;
                    Item item = Item.item[i];
                    bw.Write(item.num);
                }
                //天数
                bw.Write(Comm.days);
                bw.Write(Comm.is_pause);
                //cg
                bw.Write(CG.current_cg);

                //清空缓冲区，相当于写入数据
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch
            {
                MessageBox.Show("保存文件失败");
            }
        }
        //读取
        public static void load(int index)
        {
            int current_map;
            try
            {
                Define.define(Island.player, Island.npc, Island.map);
                FileStream fs = new FileStream("save" + index.ToString() + ".dat", FileMode.Open);
                BinaryReader br = new BinaryReader(fs);

                //时间
                string time = br.ReadString();
                //剧情变量
                for(int i = 0; i < Task.p.Length; i++)
                {
                    Task.p[i]= br.ReadInt32();
                }
                //地图数据
                current_map = br.ReadInt32();
                //角色数据
                Player.current_player = br.ReadInt32();
                Player.select_player = br.ReadInt32();
                Player.money = br.ReadInt32();

                for(int i = 0; i < Island.player.Length; i++)
                {
                    if (Island.player[i] == null)
                        continue;

                    Island.player[i].x = br.ReadInt32();
                    Island.player[i].y = br.ReadInt32();
                    Island.player[i].face = br.ReadInt32();
                    Island.player[i].walk_interval = br.ReadInt64();
                    Island.player[i].speedx = br.ReadInt32();
                    Island.player[i].speedy = br.ReadInt32();
                    Island.player[i].offset_x = br.ReadInt32();
                    Island.player[i].offset_y = br.ReadInt32();
                    Island.player[i].max_hp = br.ReadInt32();
                    Island.player[i].hp = br.ReadInt32();
                    Island.player[i].max_mp = br.ReadInt32();
                    Island.player[i].mp = br.ReadInt32();
                    Island.player[i].max_mentality = br.ReadInt32();
                    Island.player[i].mentality = br.ReadInt32();
                    Island.player[i].max_memory = br.ReadInt32();
                    Island.player[i].memory = br.ReadInt32();
                    Island.player[i].attack = br.ReadInt32();
                    Island.player[i].defense = br.ReadInt32();
                    Island.player[i].fspeed = br.ReadInt32();
                    Island.player[i].fortune = br.ReadInt32();
                    Island.player[i].equip_att = br.ReadInt32();
                    Island.player[i].equip_def = br.ReadInt32();
                    Island.player[i].fx_offset = br.ReadInt32();
                    Island.player[i].fy_offset = br.ReadInt32();
                    Island.player[i].name = br.ReadString();
                    Island.player[i].is_active = br.ReadInt32();
                    Island.player[i].collision_ray = br.ReadInt32();

                    for(int i2 = 0; i2 < 10; i2++)
                    {
                        Island.player[i].skill[i2] = br.ReadInt32();
                    }
                }
                //npc
                for(int i = 0; i < Island.npc.Length; i++)
                {
                    if (Island.npc[i] == null)
                        continue;
                    Island.npc[i].map = br.ReadInt32();
                    Island.npc[i].x = br.ReadInt32();
                    Island.npc[i].y = br.ReadInt32();
                    Island.npc[i].x_offset= br.ReadInt32();
                    Island.npc[i].y_offset= br.ReadInt32();
                    Island.npc[i].bitmap_path = br.ReadString();
                    if (Island.npc[i].bitmap_path != "")
                    {
                        Island.npc[i].bitmap = new Bitmap(Island.npc[i].bitmap_path);
                        Island.npc[i].bitmap.SetResolution(96, 96);
                    }

                    Island.npc[i].tips_visible = br.ReadBoolean();
                    Island.npc[i].visible = br.ReadBoolean();
                    Island.npc[i].region_x = br.ReadInt32();
                    Island.npc[i].region_y = br.ReadInt32();
                    Island.npc[i].face = (Comm.Direction)br.ReadInt32();
                    Island.npc[i].walk_frame = br.ReadInt32();
                    Island.npc[i].walk_interval= br.ReadInt64();
                    Island.npc[i].idle_walk_time = br.ReadInt32();
                    Island.npc[i].idle_walk_time_now = br.ReadInt32();
                    Island.npc[i].mc_xoffset = br.ReadInt32();
                    Island.npc[i].mc_yoffset = br.ReadInt32();
                    Island.npc[i].mc_w = br.ReadInt32();
                    Island.npc[i].mc_h = br.ReadInt32();
                }
                //物品数据
                for(int i = 0; i < Item.item.Length; i++)
                {
                    if (Item.item[i] == null)
                        continue;
                    Item.item[i].num = br.ReadInt32();
                }
                //天数
                Comm.days = br.ReadInt32();
                Comm.is_pause = br.ReadBoolean();
                //cg
                CG.current_cg = br.ReadInt32();

                br.Close();
                fs.Close();
            }
            catch
            {
                MessageBox.Show("读取文件失败");
                return;
            }
            int x = Island.player[Player.current_player].x;
            int y = Island.player[Player.current_player].y;
            int f = Island.player[Player.current_player].face;
            Task.change_map(current_map, x, y, f);
            Save.pan_save.hide();
        }
    }
}
