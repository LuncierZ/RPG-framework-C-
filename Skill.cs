using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace island
{
    public class Skill
    {
        //skill数组
        public static Skill[] skill;

        public int mp = 10;
        public string name = "";
        public string description = "";
        public Bitmap bitmap;

        public int value1 = 0;
        public int value2 = 0;
        public int value3 = 0;
        public int value4 = 0;
        public int value5 = 0;

        //战斗属性
        public int canfuse = 0;
        public int fvalue1 = 0;
        public int fvalue2 = 0;
        public Animation fanm;
        public Animation player_use_anm;
        //是否为增益技能
        public bool is_gain = false;

        public void set(string name,string description,string bitmap_path,
            int mp,
            int value1,int value2,int value3,int value4,int value5)
        {
            this.name = name;
            this.description = description;
            if (bitmap_path != null && bitmap_path != "")
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }
            this.mp = mp;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.value4 = value4;
            this.value5 = value5;
        }
        //委托与事件
        public delegate void Use_event(Skill skill);
        public event Use_event use_event;
        public void use()
        {
            if (Island.player[Player.select_player].mp < mp)
                return;
            Island.player[Player.select_player].mp -= mp;
            if (use_event != null)
                use_event(this);
        }
        //学习技能方法
        //type-0:解除，type-1:学习
        public static void learn_skill(int player_index,int index,int type)
        {
            if (skill == null) return;
            if (index < 0) return;
            if (index >= skill.Length) return;
            if (skill[index] == null) return;

            if (type == 0)
            {
                for(int i = 0; i < Island.player[player_index].skill.Length; i++)
                {
                    if (Island.player[player_index].skill[i] == index)
                        Island.player[player_index].skill[i] = -1;//解除技能
                }
            }
            else
            {
                for (int i = 0; i < Island.player[player_index].skill.Length; i++)
                {
                    if (Island.player[player_index].skill[i] == index)
                        return;
                }
                for (int i = 0; i < Island.player[player_index].skill.Length; i++)
                {
                    if (Island.player[player_index].skill[i] == -1)//技能栏的空位
                    {
                        Island.player[player_index].skill[i] = index;//添加新技能
                        return;
                    }
                }
            }
        }
        //治疗类技能
        public static void add_hp(Skill skill)
        {
            Player player = Island.player[Player.select_player];
            player.hp += skill.value1;
            if (player.hp > player.max_hp)
                player.hp = player.max_hp;
            if (player.hp < 0)
                player.hp = 0;
        }
        //战斗设置
        public void fset(Animation fanm, int fvalue1, int fvalue2)
        {
            this.fanm = fanm;
            this.fvalue1 = fvalue1;
            this.fvalue2 = fvalue2;
            this.canfuse = 1;//只要被设置了都是战斗物品
            if (fanm != null)
                fanm.load();
        }
        public bool check_fuse(int mp)
        {
            if (canfuse != 1)
                return false;
            if (mp < this.mp)
                return false;
            return true;
        }
    }
}
