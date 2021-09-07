using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace island
{
    public class StatusMenu
    {
        public static Panel status = new Panel();

        public static int menu = 0;//0-物品，1-技能
        public static Bitmap bitmap_menu_item;
        public static Bitmap bitmap_menu_eqip;

        public static int page = 1;
        public static int selnow = 1;
        public static Bitmap bitmap_sel;

        public static void init()
        {
            bitmap_menu_item = new Bitmap(@"resources\StatusMenu\选择物品.png");
            bitmap_menu_item.SetResolution(96, 96);
            bitmap_menu_eqip = new Bitmap(@"resources\StatusMenu\选择技能.png");
            bitmap_menu_eqip.SetResolution(96, 96);
            bitmap_sel = new Bitmap(@"resources\StatusMenu\物品选框.png");
            bitmap_sel.SetResolution(96, 96);

            Button equip_att = new Button();
            equip_att.set(29, 28, 0, 0, "resources/StatusMenu/装备选框n.png", "resources/StatusMenu/装备选框.png", "resources/StatusMenu/装备选框.png", -1, -1, -1, -1);
            equip_att.click_Event += new Button.Click_event(click_equip_att);

            Button equip_def = new Button();
            equip_def.set(29, 91, 0, 0, "resources/StatusMenu/装备选框n.png", "resources/StatusMenu/装备选框.png", "resources/StatusMenu/装备选框.png", -1, -1, -1, -1);
            equip_def.click_Event += new Button.Click_event(click_equip_def);

            Button next_player = new Button();
            next_player.set(239, 187, 0, 0, "resources/StatusMenu/换人按钮n.png", "resources/StatusMenu/换人按钮s.png", "resources/StatusMenu/换人按钮s.png", -1, -1, -1, -1);
            next_player.click_Event += new Button.Click_event(click_next_player);

            Button item_menu = new Button();
            item_menu.set(539, 45, 0, 0, "resources/StatusMenu/空图片1.png", "resources/StatusMenu/空图片1.png", "resources/StatusMenu/空图片1.png", -1, -1, -1, -1);
            item_menu.click_Event += new Button.Click_event(click_item_menu);

            Button skill_menu = new Button();
            skill_menu.set(539, 130, 0, 0, "resources/StatusMenu/空图片1.png", "resources/StatusMenu/空图片1.png", "resources/StatusMenu/空图片1.png", -1, -1, -1, -1);
            skill_menu.click_Event += new Button.Click_event(click_skill_menu);

            Button previous_page = new Button();
            previous_page.set(284, 223, 0, 0, "resources/StatusMenu/上一页n.png", "resources/StatusMenu/上一页s.png", "resources/StatusMenu/上一页s.png", -1, -1, -1, -1);
            previous_page.click_Event += new Button.Click_event(click_previous_page);

            Button next_page = new Button();
            next_page.set(453, 223, 0, 0, "resources/StatusMenu/下一页n.png", "resources/StatusMenu/下一页s.png", "resources/StatusMenu/下一页s.png", -1, -1, -1, -1);
            next_page.click_Event += new Button.Click_event(click_next_page);

            Button use = new Button();
            use.set(369, 223, 0, 0, "resources/StatusMenu/使用n.png", "resources/StatusMenu/使用s.png", "resources/StatusMenu/使用s.png", -1, -1, -1, -1);
            use.click_Event += new Button.Click_event(click_use);

            Button close = new Button();
            close.set(522, 0, 0, 0, "resources/StatusMenu/关闭n.png", "resources/StatusMenu/关闭s.png", "resources/StatusMenu/关闭s.png", -1, -1, -1, -1);
            close.click_Event += new Button.Click_event(click_close);

            Button sel1 = new Button();
            sel1.set(283, 23, 0, 0, "resources/StatusMenu/物品选框n.png", "resources/StatusMenu/物品选框.png", "resources/StatusMenu/物品选框.png", -1, -1, -1, -1);
            sel1.click_Event += new Button.Click_event(click_sel1);

            Button sel2 = new Button();
            sel2.set(283, 87, 0, 0, "resources/StatusMenu/物品选框n.png", "resources/StatusMenu/物品选框.png", "resources/StatusMenu/物品选框.png", -1, -1, -1, -1);
            sel2.click_Event += new Button.Click_event(click_sel2);

            Button sel3 = new Button();
            sel3.set(283, 151, 0, 0, "resources/StatusMenu/物品选框n.png", "resources/StatusMenu/物品选框.png", "resources/StatusMenu/物品选框.png", -1, -1, -1, -1);
            sel3.click_Event += new Button.Click_event(click_sel3);

            Button under = new Button();//空图片
            under.set(-100, -100, 2000, 2000, "", "", "", -1, -1, -1, -1);

            status.button = new Button[13];
            status.button[0] = equip_att;
            status.button[1] = equip_def;
            status.button[2] = next_player;
            status.button[3] = item_menu;
            status.button[4] = skill_menu;
            status.button[5] = previous_page;
            status.button[6] = next_page;
            status.button[7] = use;
            status.button[8] = close;
            status.button[9] = sel1;
            status.button[10] = sel2;
            status.button[11] = sel3;
            status.button[12] = under;
            status.set(200, 151, "resources/StatusMenu/status_bg.png", 2, 8);
            status.draw_event += new Panel.Draw_event(draw);
            status.init();
        }

        public static void show()
        {
            menu = 0;
            page = 1;
            status.show();
        }
        private static void draw(Graphics g, int x_offset, int y_offset)
        {
            //画角色状态
            Player p = Island.player[Player.select_player];
            g.DrawImage(p.statusMenu_bitmap, x_offset+81, y_offset+20);
            //状态数字
            Font font = new Font("黑体", 10);
            Brush brush = Brushes.Black;
            g.DrawString(p.hp.ToString(), font, brush, x_offset + 66, y_offset + 228, new StringFormat());
            g.DrawString(p.attack.ToString(), font, brush, x_offset + 66, y_offset + 254, new StringFormat());
            g.DrawString(p.fspeed.ToString(), font, brush, x_offset + 66, y_offset + 278, new StringFormat());
            g.DrawString(p.mp.ToString(), font, brush, x_offset + 182, y_offset + 228, new StringFormat());
            g.DrawString(p.defense.ToString(), font, brush, x_offset + 182, y_offset + 254, new StringFormat());
            g.DrawString(p.fortune.ToString(), font, brush, x_offset + 182, y_offset + 278, new StringFormat());
            
            //装备加成  
            int svalue1 = 0;
            int svalue2 = 0;
            int svalue3 = 0;
            int svalue4 = 0;

                if (p.equip_att >= 0)
                {
                    svalue1 = Item.item[p.equip_att].value2;
                    svalue2 = Item.item[p.equip_att].value3;
                    svalue3 = Item.item[p.equip_att].value4;
                    svalue4 = Item.item[p.equip_att].value5;
                }
                if (p.equip_def >= 0)
                {
                    svalue1 += Item.item[p.equip_def].value2;
                    svalue2 += Item.item[p.equip_def].value3;
                    svalue3 += Item.item[p.equip_def].value4;
                    svalue4 += Item.item[p.equip_def].value5;
                }
            //装备加成
            Font font_eq = new Font("黑体", 10);
            Brush brush_eq = Brushes.Red;
            if (svalue1 != 0)
                g.DrawString("+" + svalue1, font_eq, brush_eq, x_offset + 80, y_offset + 254, new StringFormat());
            if (svalue2 != 0)
                g.DrawString("+" + svalue2, font_eq, brush_eq, x_offset + 80, y_offset + 278, new StringFormat());
            if (svalue3 != 0)
                g.DrawString("+" + svalue3, font_eq, brush_eq, x_offset +196, y_offset + 254, new StringFormat());
            if (svalue4 != 0)
                g.DrawString("+" + svalue4, font_eq, brush_eq, x_offset + 196, y_offset + 278, new StringFormat());
            //装备图标
            if (p.equip_att >= 0 && Item.item[p.equip_att].bitmap != null)
                g.DrawImage(Item.item[p.equip_att].bitmap, x_offset + 29, y_offset + 28);
            if (p.equip_def >= 0 && Item.item[p.equip_def].bitmap != null)
                g.DrawImage(Item.item[p.equip_def].bitmap, x_offset + 29, y_offset + 91);
            //绘制金钱
            Font font_m = new Font("黑体", 14);
            Brush brush_m = Brushes.DarkBlue;
            g.DrawString(Player.money.ToString(), font_m, brush_m, x_offset + 371, y_offset + 269,new StringFormat());
            //物品装备选框
            if (StatusMenu.menu == 0)
                g.DrawImage(bitmap_menu_item, x_offset+538, y_offset+36);
            else
                g.DrawImage(bitmap_menu_eqip, x_offset+538, y_offset+36);
            //显示物品
            if (StatusMenu.menu == 0)
            {
                for(int i=0,count=0,showcount=0;
                    i < Item.item.Length && showcount < 3; i++)
                {
                    if (Item.item[i].num <= 0)
                        continue;
                    count++;

                    if (count <= (page - 1) * 3)
                        continue;

                    if (Item.item[i].bitmap != null)
                        g.DrawImage(Item.item[i].bitmap, x_offset + 289, y_offset + 28 + showcount * 64);
                    Font font_n = new Font("黑体", 12);
                    Brush brush_n = Brushes.GreenYellow;
                    g.DrawString(Item.item[i].name + "×" + Item.item[i].num.ToString(),
                        font_n, brush_n,
                        x_offset + 351, y_offset + 30 + showcount * 64, new StringFormat());
                    Font font_d=new Font("黑体", 10);
                    Brush brush_d = Brushes.LawnGreen;
                    g.DrawString(Item.item[i].description, font_d, brush_d,
                        x_offset + 351, y_offset + 51 + showcount * 64, new StringFormat());
                    showcount++;
                }
            }
            //显示技能
            else if (StatusMenu.menu == 1)
            {
                int[] pskill = Island.player[Player.select_player].skill;
                for(int i=0,count=0,showcount=0;
                    i < pskill.Length && showcount < 3; i++)
                {
                    if (pskill[i] < 0)
                        continue;
                    count++;

                    if (count <= (page - 1) * 3)//前几页不画
                        continue;

                    if (Skill.skill[pskill[i]].bitmap != null)
                        g.DrawImage(Skill.skill[pskill[i]].bitmap,
                            x_offset + 289, y_offset + 28 + showcount * 64);
                    Font font_n = new Font("黑体", 12);
                    Brush brush_n = Brushes.GreenYellow;
                    g.DrawString(Skill.skill[pskill[i]].name,
                        font_n, brush_n,
                        x_offset + 351, y_offset + 30 + showcount * 64, new StringFormat());
                    Font font_d = new Font("黑体", 10);
                    Brush brush_d = Brushes.LawnGreen;
                    g.DrawString(Skill.skill[pskill[i]].description, font_d, brush_d,
                        x_offset + 351, y_offset + 51 + showcount * 64, new StringFormat());
                    showcount++;
                }
            }
            //显示选择框
            g.DrawImage(StatusMenu.bitmap_sel, x_offset + 283, y_offset + 23 + (selnow - 1) * 64);
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
            status.hide();
        }

        private static void click_use()
        {
            if (menu == 0)
            {
                int index = -1;
                for(int i = 0, count = 0; i < Item.item.Length; i++)
                {
                    if (Item.item[i].num <= 0)
                        continue;//continue语句的作用是跳过本次循环体中余下尚未执行的语句，立即进行下一次的循环条件判定，可以理解为仅结束本次循环。
                    count++;

                    if (count <= (page - 1) * 3 + selnow - 1)
                        continue;
                    index = i;
                    break;
                }
                if(index>=0)
                {
                    Item.item[index].use();
                    /*
                    MessageBox.Show(Island.player[Player.select_player].attack + "," + Island.player[Player.select_player].defense
                + "," + Island.player[Player.select_player].fortune + "," + Island.player[Player.select_player].fspeed+","
                + Island.player[Player.select_player].equip_att+"," + Island.player[Player.select_player].equip_def);
                */
                }
            }
            else
            {
                int index = -1;
                int[] pskill = Island.player[Player.select_player].skill;
                for (int i = 0, count = 0; i < pskill.Length; i++){
                    if (pskill[i] < 0)
                        continue;
                    count++;

                    if (count <= (page - 1) * 3 + selnow - 1)
                        continue;

                    index = i;
                    break;
                }
                if (index >= 0)
                {
                    Skill.skill[pskill[index]].use();
                }
            }
        }

        private static void click_next_page()
        {
            page++;
        }

        private static void click_previous_page()
        {
            page--;
            if (page < 1) page = 1;
        }

        private static void click_skill_menu()
        {
            page = 1;
            selnow = 1;
            StatusMenu.menu = 1;
        }

        private static void click_item_menu()
        {
            page = 1;
            selnow = 1;
            StatusMenu.menu = 0;
        }

        private static void click_next_player()
        {
            Player.select_player = Player.select_player + 1;
            for(int i=Player.select_player;i<Island.player.Length;i++)
                if (Island.player[i].is_active == 1)
                {
                    Player.select_player = i;
                    return;
                }
            for (int i = 0; i < Player.select_player; i++)
                if (Island.player[i].is_active == 1)
                {
                    Player.select_player = i;
                    return;
                }
        }

        private static void click_equip_def()
        {
            Item.unequip(2);
        }

        private static void click_equip_att()
        {
            Item.unequip(1);
        }
    }
}
