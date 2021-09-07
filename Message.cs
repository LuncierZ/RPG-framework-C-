using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace island
{
    public class Message
    {
        public static Panel message = new Panel();
        public static Panel messagetip = new Panel();
        public static Panel pan_choice2 = new Panel();
        public static Panel pan_choice4 = new Panel();
        public static Bitmap face;
        //脸谱图朝左还是朝右
        public enum Face
        {
            LEFT=1,
            RIGHT=2,
        }
        public static Face face_pos = Face.LEFT;

        public static string name = "";
        public static string content = "";

        public static int pid = -1;
        public static string choice1 = "";
        public static int cv1 = -1;
        public static string choice2 = "";
        public static int cv2 = -1;

        public static void init()
        {
            Button btn_ok = new Button();
            btn_ok.set(-1000, -1000, 2000, 2000, "", "", "", -1, -1, -1, -1);
            btn_ok.click_Event += new Button.Click_event(btn_ok_event);

            message.button = new Button[1];
            message.button[0] = btn_ok;
            message.set(110, 435, "resources/picture/对话框1.png", 0, -1);
            message.draw_event += new Panel.Draw_event(msgdraw);
            message.init();

            Button btn_ok_tip = new Button();
            btn_ok_tip.set(-1000, -1000, 2000, 2000, "", "", "", -1, -1, -1, -1);
            btn_ok_tip.click_Event += new Button.Click_event(btntip_ok_event);

            messagetip.button = new Button[1];
            messagetip.button[0] = btn_ok_tip;
            messagetip.set(251, 235, "resources/picture/提示框1.png", 0, -1);
            messagetip.draw_event += new Panel.Draw_event(msgdrawtip);
            messagetip.init();

            //选择面板
            Button btn_sel1 = new Button();
            btn_sel1.set(176, 55, 0, 0, "resources/picture/分支面板按钮2.png", "resources/picture/分支面板按钮2s.png", "resources/picture/分支面板按钮2s.png", 1, 1, -1, -1);
            btn_sel1.click_Event += new Button.Click_event(btn_sel1_event);

            Button btn_sel2 = new Button();
            btn_sel2.set(176, 113, 0, 0, "resources/picture/分支面板按钮2.png", "resources/picture/分支面板按钮2s.png", "resources/picture/分支面板按钮2s.png", 0, 0, -1, -1);
            btn_sel2.click_Event += new Button.Click_event(btn_sel2_event);

            Button btn_under = new Button();
            btn_under.set(-1000, -1000, 9, 9, "", "", "", 0, 0, -1, -1);

            pan_choice2.button = new Button[3];
            pan_choice2.button[0] = btn_sel1;
            pan_choice2.button[1] = btn_sel2;
            pan_choice2.button[2] = btn_under;
            pan_choice2.set(110, 435, "resources/picture/对话框1.png", 2, -1);
            pan_choice2.draw_event += new Panel.Draw_event(choice_Draw);
            pan_choice2.init();
        }

        private static void choice_Draw(Graphics g, int x_offset, int y_offset)
        {
            //立绘
            if (face != null)
            {
                if (face_pos == Face.LEFT)
                    g.DrawImage(face, -20, 255);
                else if (face_pos == Face.RIGHT)
                    g.DrawImage(face, 666, 255);

            }
            //选项与文字
            Font content_font = new Font("黑体", 15);
            Brush content_brush = Brushes.Black;
            StringFormat content_sf = new StringFormat();
            string show_content = linefeed(content, 28);//文本换行
            if (face_pos == Face.LEFT)
            {
                pan_choice2.button[0].x = 176;
                pan_choice2.button[1].x = 176;
                g.DrawString(show_content, content_font, content_brush, x_offset + 212, y_offset + 20, content_sf);
                g.DrawString(choice1, content_font, content_brush, x_offset + 212, y_offset + 59, content_sf);
                g.DrawString(choice2, content_font, content_brush, x_offset + 212, y_offset + 117, content_sf);
            }
            else
            {
                pan_choice2.button[0].x = 50;
                pan_choice2.button[1].x = 50;
                g.DrawString(show_content, content_font, content_brush, x_offset + 90, y_offset + 20, content_sf);
                g.DrawString(choice1, content_font, content_brush, x_offset + 90, y_offset + 59, content_sf);
                g.DrawString(choice2, content_font, content_brush, x_offset + 90, y_offset + 117, content_sf);
            }
        }

        private static void btn_sel2_event()
        {
            Task.p[pid] = cv2;
            pan_choice2.hide();
        }

        private static void btn_sel1_event()
        {
            Task.p[pid] = cv1;
            pan_choice2.hide();
        }

        public static void btn_ok_event()
        {
            message.hide();
        }
        public static void btntip_ok_event()
        {
            messagetip.hide();
        }
        public static void msgdrawtip(Graphics g,int x_offset,int y_offset)
        {
            //画文字
            Font content_font = new Font("黑体", 18);
            Brush content_brush = new SolidBrush(Color.Black);
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;

            g.DrawString(content, content_font, content_brush,
                new Rectangle(x_offset, y_offset + 25, 291, 42), sf);
        }
        public static void showtip(string content0)
        {
            content = content0;
            messagetip.show();
        }
        public static void show(string name0,string content0,string face_path,Face face_pos0)
        {
            //content
            name = name0;
            content = content0;
            //face
            if (face_path != null && face_path != "")
            {
                face = new Bitmap(face_path);
                face.SetResolution(96, 96);
            }
            else
            {
                face = null;
            }
            face_pos = face_pos0;
            message.show();
        }
        //选择面板重写
        public static void show(string descripe,string c1, string c2,
            string face_path, Face face_pos0,
            int p_index,int p_value1,int p_value2)
        {
            //content
            content = descripe;
            choice1 = c1;
            choice2 = c2;
            //face
            if (face_path != null && face_path != "")
            {
                face = new Bitmap(face_path);
                face.SetResolution(96, 96);
            }
            else
            {
                face = null;
            }
            face_pos = face_pos0;
            //处理剧情变量
            pid = p_index;
            cv1 = p_value1;
            cv2 = p_value2;
            pan_choice2.show();
        }
        //自动换行方法
        public static string linefeed(string str/*源字符串*/,int num/*每行字数*/)
        {
            if (str == null)
                return null;
            string ret = "";
            int start_pos = 0;
            while (start_pos < str.Length)
            {
                if (start_pos + num > str.Length)
                    num = str.Length - start_pos;

                ret = ret + str.Substring(start_pos, num) + "\n";
                start_pos = start_pos + num;
            }
            return ret;
        }
        //绘图方法
        public static void msgdraw(Graphics g,int x_offset,int y_offset)
        {
            //立绘
            if(face != null)
            {
                if (face_pos == Face.LEFT)
                    g.DrawImage(face, -20, 255);
                else if (face_pos == Face.RIGHT)
                    g.DrawImage(face, 666, 255);

            }
            //名字
            Font name_font = new Font("黑体", 20);
            Brush name_brush = Brushes.CornflowerBlue;
            StringFormat name_sf = new StringFormat();
            if (face_pos == Face.LEFT)
                g.DrawString(name, name_font, name_brush, x_offset + 160, y_offset + 30, name_sf);
            else
                g.DrawString(name, name_font, name_brush, x_offset + 70, y_offset + 30, name_sf);
            //内容
            Font content_font = new Font("黑体",15);
            Brush content_brush = Brushes.Black;
            StringFormat content_sf = new StringFormat();
            string show_content = linefeed(content, 28);//文本换行
            if (face_pos == Face.LEFT)
                g.DrawString(show_content, content_font, content_brush, x_offset + 160, y_offset + 65, content_sf);
            else
                g.DrawString(show_content, content_font, content_brush, x_offset + 70, y_offset + 65, content_sf);

        }
    }
}
