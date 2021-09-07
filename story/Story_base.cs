using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace island.story
{
    public class Story_base
    {
        //接任务NPC
        public static int meet(int task_id,int step)
        {
            Task.talk("快乐钩男", "止まるんじゃねぇぞ",
                        "resources/picture/玄策脸谱0.png", Message.Face.RIGHT);
            Task.talk("奥尔加", "陌生人", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("奥尔加", "你也停不下来吗……（指被削）", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("奥尔加", "（奇怪，我为什么要说也？）", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("快乐钩男", "是啊……我们都停不下来了。",
                        "resources/picture/玄策脸谱0.png", Message.Face.RIGHT);
            Task.talk("快乐钩男", "但是他，那个人的话，还有停下来的可能性！",
                        "resources/picture/玄策脸谱0.png", Message.Face.RIGHT);
            Task.talk("奥尔加", "那个人是……。", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("迷之生物", "（指向左边）",
                      "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
            Task.talk("奥尔加", "好的，我知道了。", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("奥尔加", "走吧！去让他停下来！（接受任务）", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("快乐钩男", "拿着这个，奥尔加。",
                       "resources/picture/玄策脸谱0.png", Message.Face.RIGHT);
            Task.tip("获得撬棍");
            Task.add_item(3, 1);
            return 0;
        }
        public static int aftermeet(int task_id,int step)
        {
            Task.talk("快乐钩男", "他还是没能停下来。",
                   "resources/picture/玄策脸谱0.png", Message.Face.RIGHT);
            return 0;
        }
        public static int reward(int task_id,int step)
        {
            Task.talk("快乐钩男", "Yatazze!来，收下这个碎片吧，也许它可以让你看到自己的过去。",
                   "resources/picture/玄策脸谱0.png", Message.Face.RIGHT);
            Task.talk("奥尔加", "屑屑你。", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.tip("获得记忆碎片1");
            Task.add_item(7, 1);
            return 0;
        }
        public static int afterreward(int task_id,int step)
        {
            Task.talk("快乐钩男", "只要我们停下来，道路就会戛然而止……",
                       "resources/picture/玄策脸谱0.png", Message.Face.RIGHT);
            return 0;
        }
        //任务对象NPC
        public static int walkman(int task_id,int step)
        {
            Task.tip("          停不下来的人");
            return 0;
        }
        public static int manfight(int task_id,int step)
        {
            //战斗前
            if (step == 0)
            {
                Task.talk("停不下来男", "想要让我停下来，就要击败你自己！", "");
                Task.fight(new int[] { 1, 0, 1 }, "resources/picture/战斗场景_街道.jpg", 0, 1, 1, 0, 0);
                Task.block();
                return 1;
            }
            //战斗后
            else
            {
                if (Fight.iswin == 1)
                {
                    Task.talk("停不下来男", "你终于想通了吗……奥尔加。",
                    "", Message.Face.RIGHT);
                    Task.talk("停不下来男", "我等这一刻等了好久，终于……终于可以停下来了！",
                    "", Message.Face.RIGHT);
                    Task.talk("迷之生物", "加拉尔……号角……",
                     "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
                    Task.talk("奥尔加", "你们在说什么啊……（虽然听不懂但感觉这些台词十分令人怀念。）", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
                    Task.tip("任务完成");
                    Task.set_npc_pos(5, -1000, -1000);
                    Task.p[0] = 2;
                    return 0;
                }
                else
                    return 0;
            }
        }
        //商店
        public static int shop(int task_id, int step)
        {
            //Island.player[2].is_active = 1;
            int[] s = { 0, 1, 2, 3, -1, -1, -1 };
            Task.shop_show(s);
            Task.block();
            //Task.fight(new int[] { 1, 0, 1 }, "resources/picture/战斗场景_街道.jpg", 0, 1, 1, 0, 0);
            return 0;
        }
        //动画
        public static int girl_anm(int task_id,int step)
        {
            Task.play_npc_anm(4, 0);
            Task.talk("存档点", "存储你的进度", "");
            Save.show(0);
            Task.block();
            return 0;
        }
        //起始地图
        public static int open_newmap(int task_id,int step)
        {
            //if (Task.p[1] == 0)
            //{
                Task.talk("", "你还有什么东西没调查。", "");
                Island.player[Player.current_player].x += 10;
           // }
           // else// if(Task.p[1]==1)
            //    Map.change_map(Island.map, Island.player, Island.npc, 5, 232, 609, 3, Island.music_player);
            return 0;
        }
        public static int open_newmap1(int task_id, int step)
        {
            Map.change_map(Island.map, Island.player, Island.npc, 5, 232, 609, 3, Island.music_player);
            return 0;
        }
        //窗户
        public static int windows_npc(int task_id, int step)
        {
            CG.cg[5].load();
            CG.current_cg = 5;
            if (Player.current_player == 0)
            {
                Task.talk("奥尔加", "外面是海滩，这是个海边小屋。", "");
                Task.talk("奥尔加", "可以出去看看。", "");
            }
            else if(Player.current_player == 1)
            {
                Task.talk("迷之生物", "………………", "");
                Task.talk("", "……希望之花，连接着羁绊。", "");
            }
            CG.cg[5].unload();
            CG.current_cg = -1;
            return 0;
        }
        //水晶
        public static int money_npc(int task_id, int step)
        {
            Task.set_npc_pos(7, -1000, -1000);
            Task.tip("获得金钱111");
            Player.money += 111;
            return 0;
        }
        //队友1
        public static int duiyuan1_npc(int task_id, int step)
        {
            Task.talk("奥尔加", "你是个啥？", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("迷之生物", "……",
                       "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
            Task.talk("奥尔加", "听得懂我的话吗？", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("迷之生物", "……暗杀",
                       "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
            Task.talk("迷之生物", "哒哒哒哒哒哒哒哒哒",
                      "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
            Task.talk("奥尔加", "这家伙在说什么？？？", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("迷之生物", "（走近）",
                      "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
            Task.talk("奥尔加", "你想跟着我？", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("迷之生物", "（点头）",
                      "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
            Task.talk("奥尔加", "…………好吧，虽然不知从哪里开始吐槽。", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("奥尔加", "但总觉得你这家伙能让我想起些什么。", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.set_npc_pos(6, -1000, -1000);
            Task.tip("加入了新伙伴:迷之生物");
            Task.talk("", "可以从状态面板中查看队员信息，行走中按Tab键可切换当前队员。", "");
            Island.player[1].is_active = 1;
            return 0;
        }
        //队友2
        public static int duiyuan2_npc(int task_id, int step)
        {
            Task.talk("鸡", "咯咯咯！", "");
            Task.talk("奥尔加", "很常见的大红玉，毛色很靓。", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("奥尔加", "这是家禽，看来这附近有村落。", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.talk("迷之生物", "…………",
                      "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
            Task.talk("迷之生物", "带上它",
                      "resources/picture/小人1脸谱0右.png", Message.Face.RIGHT);
            Task.talk("奥尔加", "有道理，能打能吃岂不美哉。", "resources/picture/团长脸谱0.png", Message.Face.LEFT);
            Task.set_npc_pos(12, -1000, -1000);
            Task.tip("加入了新伙伴:公鸡");
            Task.talk("", "公鸡具有一定战斗力，并且速度很快，可以用来跑图。", "");
            Island.player[2].is_active = 1;
            return 0;
        }
        //宝藏间传送
        public static int treasure_in(int task_id, int step)
        {
            Task.talk("", "进入隐藏地图——宝藏间", "");
            Map.change_map(Island.map, Island.player, Island.npc, 6, 232, 500, 3, Island.music_player);
            return 0;
        }
        //宝箱
        public static int treasure(int task_id, int step)
        {
            Task.set_npc_pos(17, -1000, -1000);
            Task.tip("获得：荒野求生五件套");
            Item.add_item(0, 3);
            Item.add_item(1, 3);
            Item.add_item(2, 3);
            Item.add_item(3, 1);
            //Item.add_item(4, 1);
            Item.add_item(5, 1);
            Task.talk("", "包含：板蓝根x3,士力架x3,泡泡纸x3,断刀x1,学校泳衣x1。打开背包确认详情。", "");
            return 0;
        }
        //指南
        public static int book(int task_id, int step)
        {
            Task.set_npc_pos(18, -1000, -1000);
            Task.tip("获得：荒野求生指南");
            Item.add_item(6, 1);
            return 0;
        }
        //断刀
        public static int swoard(int task_id, int step)
        {
            Task.set_npc_pos(19, -1000, -1000);
            Task.tip("获得：断掉的刀");
            Item.add_item(4, 1);
            return 0;
        }
        //传主地图
        public static int mainmap_in(int task_id, int step)
        {
            if (Task.p[2] == 0)
            {
                Task.talk("", "你还有什么东西没调查。", "");
                Island.player[Player.current_player].x -= 10;
            }
            else if(Task.p[2] == 1)
            {
                Task.tip("开启新地图——环海公路");
                Map.change_map(Island.map, Island.player, Island.npc, 0, 15, 500, 3, Island.music_player);
                Task.p[2] = 2;
            }
            else if (Task.p[2] == 2)
            {
                Map.change_map(Island.map, Island.player, Island.npc, 0, 15, 500, 3, Island.music_player);
            }
            return 0;
        }
            public static void Define()
        {
            //地图1，切换点
            Task.task[0] = new Task();
            Task.task[0].set(2, new Task.Task_event(Task.change_map), 1, 15, 500, 3);
            //地图2，切换点
            Task.task[1] = new Task();
            Task.task[1].set(3, new Task.Task_event(Task.change_map), 0,1660, 500, 2);
            //陌生人 见面
            Task.task[2] = new Task();
            Task.task[2].set(0, new Task.Task_event(meet), 0, 0, Task.VARTYPE.ANY, 0/*p[0]*/, 1, Task.VARRESULT.ASSIGN);//p[0]=1
            //催促
            Task.task[3] = new Task();
            Task.task[3].set(0, new Task.Task_event(aftermeet), 0, 1, Task.VARTYPE.EQUAL, 0, 0, Task.VARRESULT.NOTHING);
            //给予奖励
            Task.task[4] = new Task();
            Task.task[4].set(0, new Task.Task_event(reward), 0/*p[0]*/, 2/*p[0]==2*/, Task.VARTYPE.EQUAL,
                0, 3, Task.VARRESULT.ASSIGN);
            //给予奖励之后
            Task.task[5] = new Task();
            Task.task[5].set(0, new Task.Task_event(afterreward), 0, 3, Task.VARTYPE.EQUAL, 0, 0, Task.VARRESULT.NOTHING);
            //敌人 默认
            Task.task[6] = new Task();
            Task.task[6].set(5, new Task.Task_event(walkman), 0, 0, Task.VARTYPE.ANY, 0, 0, Task.VARRESULT.NOTHING);
            //敌人 战斗
            Task.task[7] = new Task();
            Task.task[7].set(5, new Task.Task_event(manfight), 0, 1, Task.VARTYPE.EQUAL, 0, 0, Task.VARRESULT.NOTHING);
            //开启商店
            Task.task[8] = new Task();
            Task.task[8].set(1, shop);
            //摆手开脚舞
            Task.task[9] = new Task();
            Task.task[9].set(4, girl_anm);

            //起始地图，切换,和第325行TASK23成对
            Task.task[10] = new Task();
            Task.task[10].set(9, new Task.Task_event(open_newmap),1,0,Task.VARTYPE.EQUAL,1,0,Task.VARRESULT.NOTHING);
            //获得金币
            Task.task[11] = new Task();
            Task.task[11].set(7, money_npc);
            //调查窗户
            Task.task[12] = new Task();
            Task.task[12].set(8, windows_npc);
            //获得队友
            Task.task[13] = new Task();
            Task.task[13].set(6, new Task.Task_event(duiyuan1_npc), 1, 0, Task.VARTYPE.ANY, 1, 1, Task.VARRESULT.ASSIGN);
            //花园传送进屋
            Task.task[14] = new Task();
            Task.task[14].set(11, new Task.Task_event(Task.change_map), 4, 233, 541, 3);
            //获得队友2
            Task.task[15] = new Task();
            Task.task[15].set(12, new Task.Task_event(duiyuan2_npc), 2, 0, Task.VARTYPE.ANY, 2, 1, Task.VARRESULT.ASSIGN);
            //进入隐藏地图
            Task.task[16] = new Task();
            Task.task[16].set(13, treasure_in);
            //传回小院
            Task.task[17] = new Task();
            Task.task[17].set(14, new Task.Task_event(Task.change_map), 5, 585, 391, 1);
            //传进主地图
            Task.task[18] = new Task();
            Task.task[18].set(15, mainmap_in);
            //主地图传回小院
            Task.task[19] = new Task();
            Task.task[19].set(16, new Task.Task_event(Task.change_map), 5, 970, 647, 2);
            //获得宝物
            Task.task[20] = new Task();
            Task.task[20].set(17, treasure);
            Task.task[21] = new Task();
            Task.task[21].set(18, book);
            Task.task[22] = new Task();
            Task.task[22].set(19, swoard);
            //起始地图，调查后切换
            Task.task[23] = new Task();
            Task.task[23].set(9, new Task.Task_event(open_newmap1), 1, 1, Task.VARTYPE.EQUAL, 1, 2, Task.VARRESULT.NOTHING);
        }
    }
}
