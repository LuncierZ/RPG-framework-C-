using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace island
{
    public class Shop
    {
        public static Panel shop = new Panel();

        public static int page = 1;
        public static int selnow = 1;
        public static Bitmap bitmap_sel;
        public static int[] list;

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

            Button buy = new Button();
            buy.set(107, 260, 0, 0, "resources/StatusMenu/购买n.png", "resources/StatusMenu/购买s.png", "resources/StatusMenu/使用s.png", -1, -1, -1, -1);
            buy.click_Event += new Button.Click_event(click_buy);

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

            Button under = new Button();//空图片
            under.set(-100, -100, 2000, 2000, "", "", "", -1, -1, -1, -1);

            shop.button = new Button[8];
            shop.button[0] = previous_page;
            shop.button[1] = next_page;
            shop.button[2] = buy;
            shop.button[3] = close;
            shop.button[4] = sel1;
            shop.button[5] = sel2;
            shop.button[6] = sel3;
            shop.button[7] = under;
            shop.set(380, 151, "resources/StatusMenu/商店背景.png", 7, 3);
            shop.draw_event += new Panel.Draw_event(draw);
            shop.init();
        }

        private static void draw(Graphics g, int x_offset, int y_offset)
        {
            for (int i = 0, count = 0, showcount = 0;
                   i < Item.item.Length && showcount < 3; i++)
            {
                if (Item.item[i].num <= 0)
                    continue;
                count++;

                if (count <= (page - 1) * 3)
                    continue;

                if (Item.item[i].bitmap != null)
                    g.DrawImage(Item.item[i].bitmap, x_offset + 24, y_offset + 53 + showcount * 63);
                Font font_n = new Font("黑体", 12);
                Brush brush_n = Brushes.GreenYellow;
                g.DrawString(Item.item[i].name + "  需要碎片：" + Item.item[i].cost.ToString(),
                    font_n, brush_n,
                    x_offset + 86, y_offset + 53 + showcount * 63, new StringFormat());
                Font font_d = new Font("黑体", 10);
                Brush brush_d = Brushes.LawnGreen;
                g.DrawString(Item.item[i].description, font_d, brush_d,
                    x_offset + 86, y_offset + 74 + showcount * 63, new StringFormat());
                showcount++;
            }
            //显示选择框
            g.DrawImage(Shop.bitmap_sel, x_offset + 22, y_offset + 51 + (selnow - 1) * 63);
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
            shop.hide();
        }

        private static void click_buy()
        {
            int index = -1;
            for (int i = 0, count = 0; i < Item.item.Length; i++)
            {
                if (Item.item[i].num <= 0)
                    continue;
                count++;

                if (count <= (page - 1) * 3 + selnow - 1)
                    continue;
                index = i;
                break;
            }
            if (index >= 0)
            {
                if (Player.money >= Item.item[index].cost)
                {
                    Player.money -= Item.item[index].cost;
                    Item.add_item(index, 1);
                    Message.showtip("购买成功");
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

        public static void show(int[] list)
        {
            Shop.list = list;
            page = 1;
            shop.show();
        }
    }
}
