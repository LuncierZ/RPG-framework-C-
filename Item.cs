using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace island
{
    public class Item
    {
        //Item数组
        public static Item[] item;
        //数量
        public int num = 0;
        public string name = "";
        public string description = "";
        public Bitmap bitmap;
        public int isdepletion = 1;//是否为消耗类道具,1是0不是
        //消耗品
        public int value_hp = 0;
        public int value_mp = 0;
        public int value_mental = 0;
        public int value_memory = 0;
        //装备
        public int value1 = 0;
        public int value2 = 0;
        public int value3 = 0;
        public int value4 = 0;
        public int value5 = 0;
        public int cost = 10;
        //战斗属性
        public int canfuse = 0;
        public int fvalue1 = 0;
        public int fvalue2 = 0;
        public Animation fanm;
        //是否为增益物品
        public bool is_gain=true;
        //加血还是加蓝
        public int cure_type = 0;//0-回血，1-回蓝

        public void set(string name,string description,string bitmap_path,int isdepletion,
            int value1,int value2,int value3,int value4,int value5,
            int value_hp,int value_mp,int value_mental,int value_memory)
        {
            this.name = name;
            this.description = description;
            if (bitmap_path != null && bitmap_path != "")
            {
                bitmap = new Bitmap(bitmap_path);
                bitmap.SetResolution(96, 96);
            }

            this.isdepletion = isdepletion;
            this.value1 = value1;
            this.value2 = value2;
            this.value3 = value3;
            this.value4 = value4;
            this.value5 = value5;

            this.value_hp = value_hp;
            this.value_mp = value_mp;
            this.value_mental = value_mental;
            this.value_memory = value_memory;
        }
        //使用物品委托
        public delegate void Use_event(Item item);
        public event Use_event use_event;
        public void use()
        {
            if (num <= 0)
                return;
            if (isdepletion != 0)
                num--;
            if (use_event != null)
                use_event(this);
        }
        public static void add_item(int index,int num)
        {
            if (item == null) return;
            if (index < 0) return;
            if (index >= item.Length) return;
            if (item[index] == null) return;

            item[index].num += num;

            if (item[index].num < 0)
                item[index].num = 0;
        }
        //回复生命，体力，精神力，记忆恢复度
        public static void add_hp(Item item)
        {
            Player player = Island.player[Player.select_player];
            player.hp += item.value_hp;
            if (player.hp > player.max_hp)
                player.hp = player.max_hp;//有错改value1
            if (player.hp < 0)
                player.hp = 0;//没救了
        }
        public static void add_mp(Item item)
        {
            Player player = Island.player[Player.select_player];
            player.mp += item.value_mp;//有错改value1
            if (player.mp > player.max_mp)
                player.mp = player.max_mp;
        }
        public static void add_mentality(Item item)//对0有效
        {
                Player player = Island.player[0];
                player.mentality += item.value_mental;
                if (player.mentality > player.max_mentality)
                    player.mentality = player.max_mentality;
        }
        public static void add_memory(Item item)//对0有效
        {
                Player player = Island.player[0];
                player.memory += item.value_memory;
                if (player.memory > player.max_memory)
                    player.memory = player.max_memory;
        }
        //装备穿戴
        //脱装备
        public static void unequip(int type)
        {
            int index;
            if (type == 1)//武器类
            {
                index = Island.player[Player.select_player].equip_att;//记录武器类装备
                Island.player[Player.select_player].equip_att = -1;
            }
            else if (type == 2)
            {
                index = Island.player[Player.select_player].equip_def;
                Island.player[Player.select_player].equip_def = -1;
            }
            else return;

            if (item == null) return;
            if (index < 0) return;//无装可脱
            if (index >= item.Length) return;
            if (item[index] == null) return;

            add_item(index, 1);
        }
        //穿装备,value1=:1-att,2-def;value2-5:att,def,fspeed,forune
        public static void equip(Item item)
        {
            //遍历物品，找到装备ID
            if (Item.item == null) return;
            if (item == null) return;
            int index = -1;
            for(int i = 0; i < Item.item.Length; i++)
            {
                if (Item.item[i] == null) continue;
                if (item.name == Item.item[i].name && item.description == Item.item[i].description)
                {
                    index = i;
                    break;
                }
            }
            if (index < 0) return;
            if (index >= Item.item.Length) return;
            if (Item.item[index] == null) return;

            unequip(item.value1);//穿武器脱武器，穿装备脱装备
            //穿，当前装备标记换为新ID
            if (item.value1 == 1)
                Island.player[Player.select_player].equip_att = index;
            else if (item.value1 == 2)
                Island.player[Player.select_player].equip_def = index;
            else return;
        }

        //特殊使用事件
        //日记本
        public static void booklook(Item item)
        {
            Message.show("", "求生者技能：","",Message.Face.LEFT);
            Task.block();
            Message.show("", "1.在夜晚来临之前，务必找到庇护所。", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "2.寻找河流，沿着河流走。", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "3.夜晚有野兽闯入营地，不要出声。", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "4.遇到野兽追随，往河里走。若水流湍急则另外寻路", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "5.在家里带着多好非要出来作死", "", Message.Face.LEFT);
            Task.block();
        }
        //记忆碎片
        public static void fragment1(Item item)
        {
            string temp_url = Island.map[Map.current_map].music_path;

            Island.music_player.URL = "resources/music/Recollecting Memories.mp3";
            //加回复度
            Player player = Island.player[0];
            player.memory += item.value_memory;
            if (player.memory > player.max_memory)
                player.memory = player.max_memory;
            //事件
            CG.cg[2].load();
            CG.current_cg = 2;
            Message.show("", "总部通讯被切断后，我终于意识到了……", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "那些家伙根本不会对我们劝降，他们只想把我们全部杀光。", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "只要团员能有救，我怎么样都没关系！", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "……高木", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "我明白了，我会想办法的。", "", Message.Face.LEFT);
            Task.block();
            Message.show("", "……", "", Message.Face.LEFT);
            Task.block();
            Message.show("少女的声音", "这么说，只要能回到地球的话，大家就都得救了！", "", Message.Face.LEFT);
            Task.block();
            Message.show("？？？", "团长？阿吉姐说了什么", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "行得通了！路已经打开了，接下来只要前进就好！", "", Message.Face.LEFT);
            Task.block();
            Message.show("？？？", "（欢呼）团长！那么接下来要我去做什么。", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "【？？】！ride on！把车子开到后面去！", "", Message.Face.LEFT);
            Task.block();
            Message.show("ride on", "收到！", "", Message.Face.LEFT);
            Task.block();
            CG.cg[2].unload();
            CG.cg[3].load();
            CG.current_cg = 3;
            Message.show("ride on", "团长！车准备好了！", "", Message.Face.LEFT);
            Task.block();
            Message.show("少女的声音", "那么，【？？？】就拜托你了！", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "啊……", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "你的梦想，一定要实现啊。", "", Message.Face.LEFT);
            Task.block();
            CG.cg[3].unload();
            CG.cg[4].load();
            CG.current_cg = 4;
            Message.show("", "……", "", Message.Face.LEFT);
            Task.block();
            Message.show("ride on", "总觉得很安静呢，街上也没有加拉尔号角的人，和总部真实天差地别啊。", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "啊。大概是把火星的战力都调过去了。", "", Message.Face.LEFT);
            Task.block();
            Message.show("ride on", "嘛，反正那些已经不重要了~~", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "看给你高兴的。", "", Message.Face.LEFT);
            Task.block();
            Message.show("ride on", "这是当然的啊，大家都有救，高木又那么努力，我也是加把劲骑士。", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "（啊，没错。）", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "（我们一直以来的努力，并非全部白费。今后也是，只要我们不停下来，）", "", Message.Face.LEFT);
            Task.block();
            Message.show("奥尔加伊兹卡", "道路就会无限延伸……", "", Message.Face.LEFT);
            Task.block();
            CG.cg[4].unload();
            CG.current_cg = -1;

            Island.music_player.URL = temp_url;
            Task.talk("奥尔加", "这是我的记忆？原来我是个团长吗？", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("奥尔加", "ride on...之后发生了什么啊...", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("迷之生物", "……铁华团",
                       "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
            Task.talk("奥尔加","！！", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("迷之生物", "……碎片，继续收集。",
            "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
            Task.talk("Tips:", "每次使用记忆碎片会增加记忆恢复度，并且全员状态回满。", "", Message.Face.LEFT);
            Task.recover();
        }
        //战斗中方法
        //战斗属性设置
        public void fset(Animation fanm,int fvalue1,int fvalue2)
        {
            this.fanm = fanm;
            this.fvalue1 = fvalue1;
            this.fvalue2 = fvalue2;
            this.canfuse = 1;//只要被设置了都是战斗物品
            if (fanm != null)
                fanm.load();
        }
        //使用物品判断
        public bool check_fuse()
        {
            if (num <= 0)
                return false;
            if (canfuse != 1)
                return false;
            if (isdepletion != 0)
                num--;
            return true;//可以用于画物品，不能用的不画
        }
    }
}
