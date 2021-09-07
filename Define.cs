using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using island.story;

namespace island
{
    public class Define//用于定义资源的类
    {
        public static void define(Player[] player,Npc[] npc,Map[] map)
        {
            //******************加载玩家*********************
            Player.current_player = 0;
            Player.select_player = 0;
            Player.money = 0;

            player[0] = new Player(Properties.Resources.团长, 20, 20, 1, 90);
            player[0].is_active = 1;
            player[0].status_bitmap = new Bitmap(@"resources\picture\团长脸谱0.png");
            player[0].statusMenu_bitmap = new Bitmap(@"resources\picture\团长状态面板.png");
            player[0].status_bitmap.SetResolution(96, 96);
            player[0].statusMenu_bitmap.SetResolution(96, 96);

            player[1] = new Player(new Bitmap(@"resources\picture\小人1.png"), 10, 10, 1, 100);
            player[1].is_active = 0;
            player[1].status_bitmap = new Bitmap(@"resources\picture\小人1脸谱0.png");
            player[1].statusMenu_bitmap = new Bitmap(@"resources\picture\小人1状态面板.png");
            player[1].status_bitmap.SetResolution(96, 96);
            player[1].statusMenu_bitmap.SetResolution(96, 96);
            player[1].attack = 1;
            player[1].max_hp = 200;
            player[1].hp = 200;
            player[1].max_mp = 140;
            player[1].mp = 140;
            player[1].defense = 40;
            player[1].fspeed = 15;

            player[2] = new Player(new Bitmap(@"resources\picture\鸡行走图.png"), 35, 35, 1, 50);
            player[2].is_active = 0;
            player[2].status_bitmap = new Bitmap(@"resources\picture\鸡状态面板.png");
            player[2].statusMenu_bitmap = new Bitmap(@"resources\picture\鸡状态面板.png");
            player[2].status_bitmap.SetResolution(96, 96);
            player[2].statusMenu_bitmap.SetResolution(96, 96);
            player[2].attack = 20;
            player[2].max_hp = 60;
            player[2].hp = 60;
            player[2].max_mp = 40;
            player[2].mp = 40;
            player[2].defense = 25;
            player[2].fspeed = 10;
            player[2].fortune = 30;
            //战斗
            Animation anm_att = new Animation();
            anm_att.bitmap_path = "resources/picture/技能2.png";
            anm_att.row = 5;
            anm_att.col = 1;
            anm_att.max_frame = 5;
            anm_att.anm_rate = 1;

            Animation anm_item = new Animation();
            anm_item.bitmap_path= "resources/picture/使用物品2.png";
            anm_item.row = 5;
            anm_item.col = 1;
            anm_item.max_frame = 5;
            anm_item.anm_rate = 1;

            Animation anm_skill = new Animation();
            anm_skill.bitmap_path= "resources/picture/使用技能1.png";
            anm_skill.row = 5;
            anm_skill.col = 1;
            anm_skill.max_frame = 5;
            anm_skill.anm_rate = 1;

            player[0].fset("奥尔加1", "resources/picture/团长战斗1.png", "resources/picture/团长状态面板.png", anm_att, anm_item, anm_skill);
            player[1].fset("迷之生物", "resources/picture/小人1战斗1.png", "resources/picture/小人1状态面板.png", anm_att, anm_item, anm_skill);
            player[2].fset("公鸡", "resources/picture/鸡战斗图.png", "resources/picture/鸡战斗面板.png", anm_att, anm_item, anm_skill);


            //***************加载地图******************
            Map.is_first = true;

            map[0] = new Map();
            // map[0].map_name = "test2";
            map[0].map_path = "resources/picture/环岛公路.png"; 
             // map[0].shade_name = "test2p";
             map[0].shade_path = "resources/picture/环岛公路s.png";
            map[0].block_path = "resources/picture/环岛公路b.jpg";
            map[0].back_path = "resources/picture/环岛公路背景.jpg";
            map[0].music_path = "resources/music/前往阿里山巅峰的路.mp3";

            map[1] = new Map();
            //map[1].map_name = "tu2";
            map[1].map_path = "resources/picture/环岛公路.png";
            //map[1].shade_name = "tu2p";
            map[1].shade_path = "resources/picture/环岛公路2s.png";
            map[1].block_path = "resources/picture/环岛公路b.jpg";
            map[1].back_path = "resources/picture/环岛公路2背景.jpg";
            map[1].music_path = "resources/music/前往阿里山巅峰的路.mp3";

            map[2] = new Map();
            map[2].music_path = "resources/music/Asylum.mp3";

            map[3] = new Map();
            map[3].music_path = "";

            map[4] = new Map();
            map[4].map_path = "resources/picture/小木屋内.png";
            map[4].shade_path = "resources/picture/小木屋内s.png";
            map[4].block_path = "resources/picture/小木屋内b.jpg";
            map[4].music_path = "resources/music/万神殿.mp3";

            map[5] = new Map();
            map[5].map_path = "resources/picture/小院.png";
            map[5].shade_path = "resources/picture/小院s1.png";
            map[5].block_path = "resources/picture/小院b.jpg";
            map[5].back_path = "resources/picture/环岛公路2背景.jpg";
            map[5].music_path = "resources/music/万神殿.mp3";

            map[6] = new Map();
            map[6].map_path = "resources/picture/宝藏间.png";
            map[6].shade_path = "resources/picture/宝藏间s.png";
            map[6].block_path = "resources/picture/宝藏间b.jpg";
            map[6].music_path = "resources/music/赫里希安外围森林.mp3";
            //********************加载NPC********************
            npc[0] = new Npc();
            npc[0].map = 0;
            npc[0].x = 1100;
            npc[0].y = 600;
            npc[0].bitmap_path = "resources/picture/xuancecao.png";
            npc[0].tips_path = "resources/picture/提示1.png";
            npc[0].collision_Type = Npc.Collision_type.KEY;
            npc[0].if_init = true;//使用默认偏移值与判定区

            npc[1] = new Npc();
            npc[1].map = 1;
            npc[1].x = 500;
            npc[1].y = 500;
            npc[1].bitmap_path = "resources/picture/黑色高级车.png";
            npc[1].tips_path = "resources/picture/提示1.png";
            npc[1].region_x = 260;
            npc[1].region_y = 110;
            npc[1].collision_Type = Npc.Collision_type.KEY;

            npc[2] = new Npc();
            npc[2].map = 0;
            npc[2].x = 1665;
            npc[2].y = 625;
            npc[2].region_x = 20;
            npc[2].region_y = 625;
            npc[2].collision_Type = Npc.Collision_type.ENTER;

            npc[3] = new Npc();
            npc[3].map = 1;
            npc[3].x = 15;
            npc[3].y = 625;
            npc[3].region_x = 20;
            npc[3].region_y = 625;
            npc[3].collision_Type = Npc.Collision_type.ENTER;

            npc[4] = new Npc();
            npc[4].map = 1;
            npc[4].x = 1100;
            npc[4].y = 500;
            npc[4].bitmap_path = "resources/picture/摆手开脚舞者.png";
            npc[4].tips_path = "resources/picture/提示1.png";
            npc[4].x_tips_offset = 30;
            npc[4].if_init = true;
            npc[4].collision_Type = Npc.Collision_type.KEY;
            Animation npc4anm1 = new Animation();
            npc4anm1.bitmap_path = "resources/picture/摆手开脚舞.png";
            npc4anm1.row = 5;
            npc4anm1.col = 5;
            npc4anm1.max_frame = 25;
            npc4anm1.anm_rate = 2;
            npc[4].anm = new Animation[1];
            npc[4].anm[0] = npc4anm1;

            npc[5] = new Npc();
            npc[5].map = 0;
            npc[5].x = 400;
            npc[5].y = 500;
            npc[5].bitmap_path = "resources/picture/cat.png";
            npc[5].tips_path = "resources/picture/提示1.png";
            npc[5].collision_Type = Npc.Collision_type.KEY;
            npc[5].npc_Type = Npc.Npc_type.CHARACTER;
            npc[5].idle_walk_direction = Comm.Direction.UP;
            npc[5].idle_walk_time = 50;
            npc[5].if_init = true;
            //from now on
            npc[6] = new Npc();
            npc[6].map = 4;
            npc[6].x = 841;
            npc[6].y = 475;
            npc[6].bitmap_path = "resources/picture/小人npc.png";
            npc[6].tips_path = "resources/picture/提示1.png";
            npc[6].x_tips_offset = -30;
            npc[6].y_tips_offset = -20;
            npc[6].region_x = 114;
            npc[6].region_y = 141;
            npc[6].collision_Type = Npc.Collision_type.KEY;
            npc[5].if_init = true;

            npc[7] = new Npc();
            npc[7].map = 4;
            npc[7].x = 354;
            npc[7].y = 479;
            npc[7].bitmap_path = "resources/picture/水晶npc.png";
            npc[7].region_x = 103;
            npc[7].region_y = 170;
            npc[7].collision_Type = Npc.Collision_type.KEY;

            npc[8] = new Npc();
            npc[8].map = 4;
            npc[8].x = 608;
            npc[8].y = 384;
            npc[8].bitmap_path = "resources/picture/窗户npc.png";
            npc[8].region_x = 174;
            npc[8].region_y = 163;
            npc[8].collision_Type = Npc.Collision_type.KEY;

            npc[9] = new Npc();
            npc[9].map = 4;
            npc[9].x = 66;
            npc[9].y = 550;
            npc[9].region_x = 98;
            npc[9].region_y = 625;
            npc[9].collision_Type = Npc.Collision_type.ENTER;

            npc[10] = new Npc();
            npc[10].map = 5;
            npc[10].x = 633;
            npc[10].y = 435;
            npc[10].bitmap_path = "resources/picture/小院snpc.png";
            npc[10].region_x = 0;
            npc[10].region_y = 0;
            npc[10].collision_Type = Npc.Collision_type.KEY;

            npc[11] = new Npc();
            npc[11].map = 5;
            npc[11].x = 75;
            npc[11].y = 528;
            npc[11].bitmap_path = "resources/picture/npc花园传送.png";
            npc[11].region_x = 128;
            npc[11].region_y = 161;
            npc[11].collision_Type = Npc.Collision_type.ENTER;

            npc[12] = new Npc();
            npc[12].map = 5;
            npc[12].x = 892;
            npc[12].y = 524;
            npc[12].bitmap_path = "resources/picture/npc鸡.png";
            npc[12].tips_path = "resources/picture/提示1.png";
            npc[12].x_tips_offset = -10;
            npc[12].y_tips_offset = -10;
            npc[12].region_x = 120;
            npc[12].region_y = 66;
            npc[12].collision_Type = Npc.Collision_type.KEY;

            npc[13] = new Npc();
            npc[13].map = 5;
            npc[13].x = 727;
            npc[13].y = 397;
            npc[13].bitmap_path = "resources/picture/npc花园传送.png";
            npc[13].tips_path = "resources/picture/提示1.png";
            npc[13].x_tips_offset = -10;
            npc[13].y_tips_offset = -10;
            npc[13].region_x = 128;
            npc[13].region_y = 161;
            npc[13].collision_Type = Npc.Collision_type.KEY;

            npc[14] = new Npc();
            npc[14].map = 6;
            npc[14].x = 273;
            npc[14].y = 616;
            npc[14].bitmap_path = "resources/picture/npc宝藏传送.png";
            npc[14].tips_path = "resources/picture/提示1.png";
            npc[14].x_tips_offset = 10;
            npc[14].y_tips_offset = 10;
            npc[14].region_x = 128;
            npc[14].region_y = 161;
            npc[14].collision_Type = Npc.Collision_type.KEY;

            npc[15] = new Npc();
            npc[15].map = 5;
            npc[15].x = 1053;
            npc[15].y = 762;
            //npc[15].bitmap_path = "resources/picture/npc宝藏传送.png";
            npc[15].region_x = 15;
            npc[15].region_y = 625;
            npc[15].collision_Type = Npc.Collision_type.ENTER;

            npc[16] = new Npc();
            npc[16].map = 0;
            npc[16].x = 15;
            npc[16].y = 625;
            npc[16].region_x = 20;
            npc[16].region_y = 625;
            npc[16].collision_Type = Npc.Collision_type.ENTER;

            npc[17] = new Npc();
            npc[17].map = 6;
            npc[17].x = 829;
            npc[17].y = 125;
            npc[17].bitmap_path = "resources/picture/宝箱.png";
            npc[17].region_x = 77;
            npc[17].region_y = 77;
            npc[17].collision_Type = Npc.Collision_type.KEY;

            npc[18] = new Npc();
            npc[18].map = 6;
            npc[18].x = 235;
            npc[18].y = 377;
            npc[18].bitmap_path = "resources/items/日记本.png";
            npc[18].region_x = 90;
            npc[18].region_y = 90;
            npc[18].collision_Type = Npc.Collision_type.KEY;

            npc[19] = new Npc();
            npc[19].map = 6;
            npc[19].x = 543;
            npc[19].y = 467;
            npc[19].bitmap_path = "resources/items/断刀.png";
            npc[19].region_x = 90;
            npc[19].region_y = 90;
            npc[19].collision_Type = Npc.Collision_type.KEY;

            //***********************加载物品********************
            Item.item = new Item[8];

            Item.item[0] = new Item();
            Item.item[0].set("板蓝根", "包治百病的神药，\n恢复少量生命", "resources/items/板蓝根.png",
                1,
                0, 0, 0, 0, 0,
                20, 0, 0, 0);
            Item.item[0].cost = 10;
            Item.item[0].use_event += new Item.Use_event(Item.add_hp);
            Animation anm_item0 = new Animation();
            anm_item0.bitmap_path = "resources/picture/使用物品1.png";
            anm_item0.row = 5;
            anm_item0.col = 1;
            anm_item0.max_frame = 5;
            anm_item0.anm_rate = 1;
            Item.item[0].fset(anm_item0, -20, 0);


            Item.item[1] = new Item();
            Item.item[1].set("士力架", "饿货，来补充体力", "resources/items/士力架.png",
                1,
                0, 0, 0, 0, 0,
                0, 20, 0, 0);
            Item.item[1].cost = 10;
            Item.item[1].use_event += new Item.Use_event(Item.add_mp);
            Animation anm_item1 = new Animation();
            anm_item1.bitmap_path = "resources/picture/技能3.png";
            anm_item1.row = 5;
            anm_item1.col = 2;
            anm_item1.max_frame = 10;
            anm_item1.anm_rate = 1;
            Item.item[1].fset(anm_item1, -20, 0);
            Item.item[1].cure_type = 1;

            Item.item[2] = new Item();
            Item.item[2].set("泡泡纸", "捏它能得到快乐", "resources/items/泡泡纸.png",
                1,
                0, 0, 0, 0, 0,
                0, 0, 5, 0);
            Item.item[2].cost = 5;
            Item.item[2].use_event += new Item.Use_event(Item.add_mentality);


            Item.item[3] = new Item();
            Item.item[3].set("撬棍", "物理学圣剑", "resources/items/撬棍.png",
                1,
                1, 15, 0, 0, 0,
                0, 0, 0, 0);
            Item.item[3].cost = 25;
            Item.item[3].use_event += new Item.Use_event(Item.equip);
            Animation anm_item4 = new Animation();
            anm_item4.bitmap_path= "resources/picture/使用撬棍.png";
            anm_item4.row = 5;
            anm_item4.col = 1;
            anm_item4.max_frame = 5;
            anm_item4.anm_rate = 1;
            Item.item[3].fset(anm_item4, 50, 20);
            Item.item[3].is_gain = false;

            Item.item[4] = new Item();
            Item.item[4].set("断掉的刀", "不知为何出现在自己\n旁边的，奇怪的刀", "resources/items/断刀.png",
                1,
                1, 3, 0, 0, 50,
                0, 0, 0, 0);
            Item.item[4].use_event += new Item.Use_event(Item.equip);


            Item.item[5] = new Item();
            Item.item[5].set("学校泳衣", "女高中生的杀必死", "resources/items/学校泳衣.png",
                1,
                2, 2, 10, 15, 0,
                0, 0, 0, 0);
            Item.item[5].cost = 25;
            Item.item[5].use_event += new Item.Use_event(Item.equip);
            Item.item[5].fset(anm_item4, 25, 5);


            Item.item[6] = new Item();
            Item.item[6].set("求生指南", "字迹不清，作者不明", "resources/items/日记本.png",
                0,
                0, 0, 0, 0, 0,
                0, 0, 0, 0);
            Item.item[6].use_event += new Item.Use_event(Item.booklook);

            
            Item.item[7] = new Item();
            Item.item[7].set("记忆碎片1", "点击进入回想", "resources/items/记忆碎片1.png",
                1,
                0, 0, 0, 0, 0,
                0, 0, 0, 1);
            Item.item[7].use_event += new Item.Use_event(Item.fragment1);


            Item.add_item(1, 5);


            /********************技能类************************/
            Animation anm_skill0 = new Animation();
            anm_skill0.bitmap_path = "resources/picture/技能1.png";
            anm_skill0.row = 5;
            anm_skill0.col = 3;
            anm_skill0.max_frame = 12;
            anm_skill0.anm_rate = 1;
            Animation anm_skill1 = new Animation();
            anm_skill1.bitmap_path = "resources/picture/使用物品1.png";
            anm_skill1.row = 5;
            anm_skill1.col = 1;
            anm_skill1.max_frame = 5;
            anm_skill1.anm_rate = 1;
            Skill.skill = new Skill[2];

            Skill.skill[0] = new Skill();
            Skill.skill[0].set("记忆重载", "……………\n（恢复少量生命值）", "resources/items/断刀.png",
                20,
                20, 0, 0, 0, 0);
            Skill.skill[0].use_event += new Skill.Use_event(Skill.add_hp);
            Skill.skill[0].fset(anm_skill1, -30, 10);
            Skill.skill[0].is_gain = true;
            Skill.skill[0].player_use_anm = npc4anm1;


            Skill.skill[1] = new Skill();
            Skill.skill[1].set("普通射击", "什么嘛，我打的\n还蛮准的", "resources/items/断刀.png",
                20,
                0, 0, 0, 0, 0);
            Skill.skill[1].use_event += new Skill.Use_event(Skill.add_hp);
            Skill.skill[1].fset(anm_skill0, 50, 15);
            Skill.skill[1].is_gain = false;
            Skill.skill[1].player_use_anm = npc4anm1;


            Skill.learn_skill(0, 1, 1);
            Skill.learn_skill(1, 0, 1);

            /********************敌人类************************/
            Enemy.enemy = new Enemy[5];
            Enemy.enemy[0] = new Enemy();
            Enemy.enemy[0].set("里人格（放弃）", "resources/picture/敌人1.png",300,20,15,15,10,anm_att,anm_skill,
                new int[] { -1,-1,-1,1,-1});//出招顺序？
            Enemy.enemy[1] = new Enemy();
            Enemy.enemy[1].set("里人格（激进）", "resources/picture/敌人1.png", 100, 20, 5, 15, 10, anm_att, anm_skill,
                new int[] { -1, -1, 1, 1, 1 });//出招顺序？
            Enemy.enemy[2] = new Enemy();
            Enemy.enemy[2].set("竹叶青", "resources/picture/敌人1.png", 300, 20, 10, 15, 10, anm_att, anm_skill,
                new int[] { -1, -1, 1, 1, 1 });//出招顺序？
            Enemy.enemy[3] = new Enemy();
            Enemy.enemy[3].set("暗杀兵", "resources/picture/敌人1.png", 300, 20, 10, 15, 10, anm_att, anm_skill,
                new int[] { -1, -1, 1, 1, 1 });//出招顺序？
            Enemy.enemy[4] = new Enemy();
            Enemy.enemy[4].set("不明生物", "resources/picture/敌人1.png", 300, 20, 10, 15, 10, anm_att, anm_skill,
                new int[] { -1, -1, 1, 1, 1 });//出招顺序？
            /***************天数*************/
            Island.player[0].mentality = 100;
            Comm.days = 1;

            /*********************任务*********************/
            for (int i = 0; i < Task.p.Length; i++)
                Task.p[i] = 0;
            Task.task = new Task[100];
            Story_base.Define();

            /**************CG*********************/
            CG.cg = new CG[6];
            CG.cg[0] = new CG();
            CG.cg[0].x = 0;
            CG.cg[0].y = 0;
            CG.cg[0].cg_path = "resources/cg/夕阳街道.jpg";

            CG.cg[1] = new CG();
            CG.cg[1].x = 0;
            CG.cg[1].y = 0;
            CG.cg[1].cg_path = "resources/cg/木屋里.jpg";

            CG.cg[2] = new CG();
            CG.cg[2].x = 0;
            CG.cg[2].y = 0;
            CG.cg[2].cg_path = "resources/cg/cg1.jpg";

            CG.cg[3] = new CG();
            CG.cg[3].x = 0;
            CG.cg[3].y = 0;
            CG.cg[3].cg_path = "resources/cg/cg2.jpg";

            CG.cg[4] = new CG();
            CG.cg[4].x = 0;
            CG.cg[4].y = 0;
            CG.cg[4].cg_path = "resources/cg/cg3.jpg";

            CG.cg[5] = new CG();
            CG.cg[5].x = 0;
            CG.cg[5].y = 0;
            CG.cg[5].cg_path = "resources/picture/环岛公路2背景.jpg";
        }
    }
}
