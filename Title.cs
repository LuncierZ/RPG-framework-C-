using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace island
{
    public class Title
    {
        public static Panel title = new Panel();
        public static Panel confirm = new Panel();
        public static Panel confirm2 = new Panel();
        public static string title_music = "resources/music/Asylum.mp3";
        public static Bitmap bg_1 = new Bitmap("resources/picture/bg1.jpg");
        public static Bitmap bg_2 = new Bitmap("resources/picture/bg2.jpg");
        public static Bitmap bg_3 = new Bitmap("resources/picture/bg3.jpg");
        public static Bitmap bg_font = new Bitmap("resources/picture/logo1.png");
        public static long last_change_bg_time = 0;
        public static int bg_now = 2;

        public static void init()
        {
            bg_1.SetResolution(96, 96);
            bg_2.SetResolution(96, 96);
            bg_3.SetResolution(96, 96);
            bg_font.SetResolution(96, 96);

            Button btn_new = new Button();
            btn_new.set(300, 330, 0, 0, "resources/picture/bts_n.png",
                "resources/picture/bts_s.png", "resources/picture/bts_p.png",
                2, 1, -1, -1);
            btn_new.click_Event += new Button.Click_event(newgame);
            Button btn_load = new Button();
            btn_load.set(300, 420, 0, 0, "resources/picture/btl_n.png",
                "resources/picture/btl_s.png", "resources/picture/btl_p.png",
                0, 2, -1, -1);
            btn_load.click_Event += new Button.Click_event(loadgame);

            Button btn_exit = new Button();
            btn_exit.set(300, 510, 0, 0, "resources/picture/bte_n.png",
                "resources/picture/bte_s.png", "resources/picture/bte_p.png",
                1, 0, -1, -1);
            btn_exit.click_Event += new Button.Click_event(exitgame);

            Button btn_yes = new Button();
            btn_yes.set(65, 110,0,0, "resources/picture/离开面板确认.png",
                "resources/picture/离开面板确认s.png", "resources/picture/离开面板确认p.png",
                -1, -1, 1, 1);
            btn_yes.click_Event += new Button.Click_event(confirm_yes);

            Button btn_yes1 = new Button();
            btn_yes1.set(65, 110, 0, 0, "resources/picture/离开面板确认.png",
                "resources/picture/离开面板确认s.png", "resources/picture/离开面板确认p.png",
                -1, -1, 1, 1);
            btn_yes1.click_Event += new Button.Click_event(confirm2_yes);

            Button btn_no = new Button();
            btn_no.set(200, 110, 0, 0, "resources/picture/离开面板取消.png",
                "resources/picture/离开面板取消s.png", "resources/picture/离开面板取消p.png",
                -1, -1, 0, 0);
            btn_no.click_Event += new Button.Click_event(confirm_no);

            Button btn_no1 = new Button();
            btn_no1.set(200, 110, 0, 0, "resources/picture/离开面板取消.png",
                "resources/picture/离开面板取消s.png", "resources/picture/离开面板取消p.png",
                -1, -1, 0, 0);
            btn_no1.click_Event += new Button.Click_event(confirm2_no);

            confirm.button = new Button[2];
            confirm.button[0] = btn_yes;
            confirm.button[1] = btn_no;
            confirm.set(330, 220, "resources/picture/离开面板.png", 0,1);
            confirm.drawbg_event += new Panel.Drawbg_event(drawconfirm);
            confirm.init();

            title.button = new Button[3];
            title.button[0] = btn_new;
            title.button[1] = btn_load;
            title.button[2] = btn_exit;
            title.set(0, 0, "", 0, -1);
            title.init();

            confirm2.button = new Button[2];
            confirm2.button[0] = btn_yes1;
            confirm2.button[1] = btn_no1;
            confirm2.set(330, 220, "resources/picture/离开面板.png", 0, 1);
            //confirm2.drawbg_event += new Panel.Drawbg_event(drawconfirm);
            confirm2.init();

            title.draw_event += new Panel.Draw_event(drawtitle);
        }

        private static void confirm2_no()
        {
            confirm2.hide();
        }

        private static void confirm2_yes()
        {
            Define.define(Island.player, Island.npc, Island.map);
            Comm.is_pause = true;
            Title.show();
        }

        public static void drawtitle(Graphics g,int x_offset,int y_offset)
        {
            //绘制背景
            if (bg_now == 0)
                g.DrawImage(bg_1, 0, 0);
            else if (bg_now == 1)
                g.DrawImage(bg_2, 0, 0);
            else if (bg_now == 2)
                g.DrawImage(bg_3, 0, 0);
            //绘制logo
            g.DrawImage(bg_font, 0, 0);
            //背景处理
            if (Comm.Time() - last_change_bg_time > 5000)
            {
                bg_now = bg_now + 1;
                if (bg_now > 2)
                    bg_now = 0;
                last_change_bg_time = Comm.Time();
            }
        }
        public static void newgame()
        {
            Comm.is_pause = false;
            Define.define(Island.player, Island.npc, Island.map);
           // Map.change_map(Island.map, Island.player, Island.npc, 5, 548, 539, 1, Island.music_player);
            title.hide();
            startStory();
           //Task.fight(new int[] { 0, 0, 0 }, "resources/picture/战斗场景_街道.jpg", 0, 1, 1, 0, 0);
        }
        public static void loadgame()
        {
            //Comm.is_pause = false;
            Save.show(1);
        }
        public static void exitgame()
        {
            confirm.show();
        }
        public static void show()
        {
            Island.music_player.URL = title_music;
            title.show();
        }
        //离开面板确认取消
        public static void confirm_yes()
        {
            //Application.Exit();
            System.Environment.Exit(0);
        }
        public static void confirm_no()
        {
            title.show();
        }
        //离开面板BUG修补
        public static void drawconfirm(Graphics g,int x_offset,int y_offset)
        {
            title.draw_me(g);
        }
        public static void startStory()
        {
            Map.change_map(Island.map, Island.player, Island.npc, 3, -100, -500, 1, Island.music_player);
            Island.music_player.URL = "";
            CG.cg[0].load();
            CG.current_cg = 0;
            title.hide();
            Message.show("", "五月二日，晴", "", Message.Face.LEFT);
            Task.block();
            Message.show("？？？", "这是当然的啊！大家都有救，高木也那么努力，我也是加把劲骑士。", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "是啊，只要我们不停下脚步，道路就会无限延伸。", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "……", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "我们所做的努力，并非全部白费。", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "……", "", Message.Face.LEFT);
            Task.block();
            Message.show("？？？", "团长！你在做什么啊！团长！", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "所以啊……不要停下来……", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "……", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "脑内依稀回响着与某个人的交谈，这大概是我来到这里前最后的记忆。", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "是的，", "", Message.Face.LEFT);
            Task.block();
            CG.cg[0].unload();
            CG.cg[1].load();
            CG.current_cg = 1;
            Message.show("奥尔加", "不知为何我来到了这个陌生的地方，听着外面的海浪声，我判断这里是一座岛。", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加", "可恶，什么都想不起来了……", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加", "要先探索一下四周吗？", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            //p3
            Message.show("(要跳过新手教程吗？)", "要！！！！！！！！！！！！！！", "不要.", "resources/picture/团长脸谱0.png", Message.Face.LEFT, 3, 0, 1);
            Task.block();
            CG.cg[1].unload();
            CG.current_cg = -1;
            if (Task.p[3] == 0)
            {
                Map.change_map(Island.map, Island.player, Island.npc, 4, 652, 551, 1, Island.music_player);
            }
            else if(Task.p[3]==1)
            {
                Map.change_map(Island.map, Island.player, Island.npc, 4, 652, 551, 1, Island.music_player);
                Message.show("操作方法:", "方向键或鼠标移动人物；空格、回车或鼠标左键确认/调查目标；ESC或鼠标右键打开状态面板；H键返回标题界面。", "", Message.Face.LEFT);
                Task.block();
                Message.show("操作方法:", "右上角为精神值，精神值降为0时将进入睡眠并开启新的一天。", "", Message.Face.LEFT);
                Task.block();
            }
        }
    }
}
