using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace island
{
    public partial class Island : Form
    {
        public static Player[] player = new Player[3];
        public static Map[] map = new Map[7];
        public static Npc[] npc = new Npc[20];
        public static WMPLib.WindowsMediaPlayer music_player = new WMPLib.WindowsMediaPlayer();
        //鼠标光标
        public Bitmap mc_normal;
        public Bitmap mc_event;
        public int mc_mod = 0;//0-normal,1-event
        //精神值
        public Bitmap spri_bitmap;
        public long player_time = 0;
        public Island()
        {
            InitializeComponent();
            music_player.settings.setMode("loop", true);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
           Player.Key_ctrl(e,player,npc,map);//静态方法可直接调用
            if (Panel.panel != null)
                Panel.key_ctrl(e);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Player.Key_ctrl_Up(player, e);
        }

        private void Draw() {

            Graphics g1 = Scene.CreateGraphics();
            BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;
            BufferedGraphics myBuffer = currentContext.Allocate(g1, this.DisplayRectangle);
            Graphics g = myBuffer.Graphics;
            if (Fight.fighting == 0)
            {
                Map.draw(map, player, npc, g, new Rectangle(0, 0, Scene.Width, Scene.Height));
                //精神值,天数
                if (Comm.is_pause == false)
                {
                    g.DrawImage(spri_bitmap, new Point(757, 0));
                    player[0].draw_mentality(g, 853, 49);
                    Comm.draw_days(g, 853, 14);
                }
                //cg
                if(CG.current_cg!=-1)
                CG.draw(g);
            }
            else
                Fight.draw(g);
            //面板
            if (Panel.panel != null)
                Panel.draw(g);
            //改变鼠标光标
            draw_mouse(g);

            myBuffer.Render();
            myBuffer.Dispose();
        }

        private void Form1_Load(object sender, EventArgs e)//主角，地图素材加载处
        {
            /***********************鼠标光标*********************/
            mc_normal = new Bitmap(@"resources\picture\鼠标常规.png");
            mc_normal.SetResolution(96, 96);
            mc_event = new Bitmap(@"resources\picture\鼠标选中.png");
            mc_event.SetResolution(96, 96);
            /***********************面板类***********************/
            Title.init();
            Message.init();
            Define.define(player, npc, map);
            StatusMenu.init();
            Shop.init();
            Save.init();
            /***********************战斗***********************/
            //this.Show();
            Fight.init();
            //Fight.start(new int[] { 0, 0, -1 }, "resources/picture/战斗场景_街道.jpg", 1, 0, 1, 1, 100);
            /*************************精神值*********************/
            Comm.is_pause = true;
            spri_bitmap = new Bitmap(Properties.Resources.精神值提示框);
           

            Map.change_map(map, player,npc, 2/*第几张图*/, 20/*角色的初始x*/, 500/*角色的初始y*/, 1,music_player);
            //有BUG,暂时用图3的BGM代替标题画面的BGM
            Title.show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Player.timer_logic(player, map);
            //npc逻辑
            for (int i = 0; i < npc.Length; i++)
            {
                if (npc[i] == null)
                    continue;
                if (npc[i].map != Map.current_map)
                    continue;

                npc[i].timer_logic(map);
            }
            //精神值逻辑
            if (Comm.is_pause == false)
            {
                if (player_time >= long.MaxValue)
                    player_time = 0;
                player_time++;
                if (player_time % 180 == 0)//一天的长短从这调
                {
                    player[0].mentality--;
                    if (player[0].mentality <= 0)
                    {
                        Comm.days++;
                        player[0].mentality = 100;
                    }
                }
            }
            Draw();
        }

        private void Island_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void Scene_MouseMove(object sender, MouseEventArgs e)
        {
            if (Panel.panel != null)
                Panel.mouse_move(e);
            mc_mod = Npc.check_mouse_collision(map, player, npc, new Rectangle(0, 0, Scene.Width, Scene.Height), e);
        }

        private void Scene_MouseClick(object sender, MouseEventArgs e)
        {
            Player.mouse_click(map, player, new Rectangle(0, 0, Scene.Width, Scene.Height), e);
            Npc.mouse_click(map, player, npc, new Rectangle(0, 0, Scene.Width, Scene.Height), e);
            if (Panel.panel != null)
                Panel.mouse_click(e);
        }
        //绘制鼠标光标
        private void draw_mouse(Graphics g)
        {
            Point showpoint = Scene.PointToClient(Cursor.Position);
            if (mc_mod == 0)
                g.DrawImage(mc_normal, showpoint.X, showpoint.Y);
            else
                g.DrawImage(mc_event, showpoint.X, showpoint.Y);
        }

        private void Scene_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Hide();
        }

        private void Scene_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Show();
        }

    }
}
