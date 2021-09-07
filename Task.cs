using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace island
{
    public class Task
    {
        //剧情变量
        public static int p0 = 0;
        //控制变量
        public static int[] p = new int[200];
        public static Task[] task;//保存所有任务
        public static int id = 0;//当前任务id
        public static int step = 0;//当前的步骤
        public static Player.Status player_last_status = Player.Status.WALK;
        //预设条件1-触发的NPC
        public int npc_id = -1;
        public enum VARTYPE
        {
            ANY=0,//不做判断
            EQUAL=1,//相等
            GREATER=2,//大于
            LESS=3,//小于
        }
        //预设条件2-两组剧情变量
        public int cvar1_index = 0;//剧情变量下标
        public int cvar1 = 0;//判断的值
        public VARTYPE cvar1_type = VARTYPE.ANY;//判断方式
        public int cvar2_index = 0;//剧情变量下标
        public int cvar2 = 0;//判断的值
        public VARTYPE cvar2_type = VARTYPE.ANY;//判断方式
        //预设条件3-金钱
        public int money = 0;
        public VARTYPE money_type = VARTYPE.ANY;

        //剧情变量变化
        public enum VARRESULT
        {
            NOTHING=0,//不处理
            ASSIGN=1,//赋值
            ADD=2,//加
            SUB=3,//减
        }
        public int rvar1_index = 0;//哪个剧情变量
        public int rvar1 = 0;//操作值
        public VARRESULT rvar1_type = VARRESULT.NOTHING;//哪种操作
        public int rvar2_index = 0;
        public int rvar2 = 0;
        public VARRESULT rvar2_type = VARRESULT.NOTHING;
        //辅助变量
        public int var1 = 0;
        public int var2 = 0;
        public int var3 = 0;
        public int var4 = 0;

        /*******************set********************/
        public void set(int npc_id,Task_event evt,
            int cvar1_index,int cvar1,VARTYPE cvar1_type,
            int cvar2_index,int cvar2,VARTYPE cvar2_type,
            int money,VARTYPE money_type,
            int rvar1_index,int rvar1,VARRESULT rvar1_type,
            int rvar2_index,int rvar2,VARRESULT rvar2_type,
            int var1,int var2,int var3,int var4)
        {
            this.npc_id = npc_id;
            this.evt += evt;
            this.cvar1_index = cvar1_index;
            this.cvar1 = cvar1;
            this.cvar1_type = cvar1_type;
            this.cvar2_index = cvar2_index;
            this.cvar2 = cvar2;
            this.cvar2_type = cvar2_type;
            this.money = money;
            this.money_type = money_type;
            this.rvar1_index = rvar1_index;
            this.rvar1 = rvar1;
            this.rvar1_type = rvar1_type;
            this.rvar2_index = rvar2_index;
            this.rvar2 = rvar2;
            this.rvar2_type = rvar2_type;
            this.var1 = var1;
            this.var2 = var2;
            this.var3 = var3;
            this.var4 = var4;
        }
        //只用一个剧情变量
        public void set(int npc_id, Task_event evt,
                        int cvar1_index, int cvar1, VARTYPE cvar1_type,
                        int rvar1_index, int rvar1, VARRESULT rvar1_type,
                        int var1, int var2, int var3, int var4)
        {
            set(npc_id, evt,
                cvar1_index, cvar1, cvar1_type,
                0, 0, VARTYPE.ANY,
                0, VARTYPE.ANY,
                rvar1_index, rvar1, rvar1_type,
                0, 0, VARRESULT.NOTHING,
                var1, var2, var3, var4);
        }
        //只用一个剧情变量，四个辅助变量都为0
        public void set(int npc_id, Task_event evt,
                        int cvar1_index, int cvar1, VARTYPE cvar1_type,
                        int rvar1_index, int rvar1, VARRESULT rvar1_type)
        {
            set(npc_id, evt,
                cvar1_index, cvar1, cvar1_type,
                rvar1_index, rvar1, rvar1_type,
                0,0,0,0);
        }
        //只设置Npc_id，剧情事件和4个辅助变量
        public void set(int npc_id, Task_event evt,
                int var1,int var2,int var3,int var4)
        {
            set(npc_id, evt,
                0, 0, VARTYPE.ANY,
                0,0,VARRESULT.NOTHING,
                var1,var2,var3,var4);
        }
        //只设置npc_id和剧情事件
        public void set(int npc_id,Task_event evt)
        {
            set(npc_id, evt, 0, 0, 0, 0);
        }

        //预设条件判断,0-符合,1-不符合
        public int check_conditions(int index)
        {
            //预设条件
            //id
            if (index != npc_id)
                return -1;
            //var1
            if (cvar1_type == VARTYPE.EQUAL)
            {
                if (p[cvar1_index] != cvar1)
                    return -1;
            }
            else if (cvar1_type == VARTYPE.GREATER)
            {
                if (p[cvar1_index] <= cvar1)
                    return -1;
            }
            else if (cvar1_type == VARTYPE.LESS)
            {
                if (p[cvar1_index] >= cvar1)
                    return -1;
            }
            //var2
            if (cvar2_type == VARTYPE.EQUAL)
            {
                if (p[cvar2_index] != cvar2)
                    return -1;
            }
            else if(cvar2_type == VARTYPE.GREATER)
            {
                if (p[cvar2_index] <= cvar2)
                    return -1;
            }
            else if (cvar2_type == VARTYPE.LESS)
            {
                if (p[cvar2_index] >= cvar2)
                    return -1;
            }
            //money
            if (money_type == VARTYPE.EQUAL)
            {
                if (Player.money != cvar2)
                    return -1;
            }
            else if (money_type == VARTYPE.GREATER)
            {
                if (Player.money <=cvar2)
                    return -1;
            }
            else if (money_type == VARTYPE.LESS)
            {
                if (Player.money >= cvar2)
                    return -1;
            }
            return 0;
        }

        //预设结果处理
        public void deal_result()
        {
            //预设结果处理
            //var1
            if (rvar1_type == VARRESULT.ASSIGN)
            {
                p[rvar1_index] = rvar1;
            }
            else if (rvar1_type == VARRESULT.ADD)
            {
                p[rvar1_index] += rvar1;
            }
            else if (rvar1_type == VARRESULT.SUB)
            {
                p[rvar1_index] -= rvar1;
            }
            //var2
            if (rvar2_type == VARRESULT.ASSIGN)
            {
                p[rvar2_index] = rvar2;
            }
            else if (rvar2_type == VARRESULT.ADD)
            {
                p[rvar2_index] += rvar2;
            }
            else if (rvar2_type == VARRESULT.SUB)
            {
                p[rvar2_index] -= rvar2;
            }
        }

        //剧情处理委托
        public delegate int Task_event(int index, int step);
        public event Task_event evt;
        //task_event 返回值
        //0-处理成功且完成
        //其他-走到第n步
        public int task_event(int task_id,int step)
        {
            int ret = 0;
            if (evt != null)
            {
                ret = evt(task_id, step);
                Task.step = ret;
            }
            return ret;
        }
        public int storyname(int index,int step)
        {
            /*自定义条件
            if (xxxx)
                return -1;
            */
            //用于战斗的步骤
            if (step == 0)
            {
                //第一段剧情
                return 1;
            }
            if (step == 1)
            {
                //第二段剧情
                return 0;
            }
            return -1;
        }

        /********************stor**************************/
        public static void story(int npc_id,int type)//type:0-正常,1-回调
        {
            //保存状态
            if (Player.status != Player.Status.TASK)
                player_last_status = Player.status;
            Player.status = Player.Status.TASK;
            //事件
            if (task == null)
                return;

            if (type == 1 && id >= 0)
            {
                int ret = task[id].task_event(id, step);
                if (ret == 0)
                    task[id].deal_result();
            }
            else if(type==0)
                for(int i = task.Length - 1; i >= 0; i--) {
                    if (task[i] == null)
                        continue;
                    if (task[i].check_conditions(npc_id) != 0)
                        continue;
                    id = i;
                    step = 0;
                    int ret = task[i].task_event(i, step);
                    if (ret == 0)
                        task[i].deal_result();
                    break;
                }
            //恢复状态
            Player.status = player_last_status;
        }
        public static void story(int i)
        {
            story(i, 0);
        }

        /***********************常用功能******************/
        //切换地图：var1:map_id,var2/var3:坐标,var4:面向
        public static int change_map(int task_id, int step)
        {
            if (task == null)
                return -1;
            if (task[task_id] == null)
                return -1;

            int map_id = task[task_id].var1;
            int x = task[task_id].var2;
            int y = task[task_id].var3;
            int f = task[task_id].var4;
            Map.change_map(Island.map, Island.player, Island.npc, map_id, x, y, f, Island.music_player);
            return 0;
        }
        public static void change_map(int map_id,int x,int y,int face)
        {
            Map.change_map(Island.map, Island.player, Island.npc, map_id, x, y, face, Island.music_player);
        }
        //对话
        public static void talk(string name,string str,string face,Message.Face fpos)
        {
            Message.show(name, str, face, fpos);
            block();
        }
        public static void talk(string name,string str,string face) {
            Message.show(name, str, face, Message.Face.LEFT);
            block();
        }
        //提示
        public static void tip(string str)
        {
            Message.showtip(str);
            block();
        }
        //设置NPC位置
        public static void set_npc_pos(int npc_id,int x,int y)
        {
            if (Island.npc == null)
                return;
            if (Island.npc[npc_id] == null)
                return;

            Island.npc[npc_id].x = x;
            Island.npc[npc_id].y = y;
        }
        //播放Npc动画
        public static void play_npc_anm(int npc_id,int anm_id)
        {
            if (Island.npc == null)
                return;
            if (Island.npc[npc_id] == null)
                return;

            Island.npc[npc_id].play_anm(anm_id);
        }
        //战斗
        public static void fight(int[] enemy,string bg_path,int isgameover,int winitem1,int winitem2,int winitem3,int losemoney)
        {
            Fight.start(enemy, bg_path, isgameover, winitem1, winitem2, winitem3, losemoney);
        }
        //增减物品
        public static void add_item(int item_id,int num)
        {
            Item.add_item(item_id, num);
        }
        //学习技能
        public static void learn_skill(int p_id,int skill_id,int type)
        {
            Skill.learn_skill(p_id, skill_id, type);
        }
        //商店
        public static void shop_show(int[] items)
        {
            Shop.show(items);
        }
        //恢复
        public static void recover() {
            if (Island.player == null)
                return;
            for(int i = 0; i < Island.player.Length; i++)
            {
                if (Island.player[i] == null)
                    continue;
                Island.player[i].hp = Island.player[i].max_hp;
                Island.player[i].mp = Island.player[i].max_mp;
            }
            tip("完全恢复！");
        }
        //保存
        public static void save()
        {
            Task.talk("存档点", "存储你的进度", "");
            Save.show(0);
            Task.block();
        }
        //阻断方法
        public static void block()
        {
            while (Player.status == Player.Status.PANEL)
                Application.DoEvents();
        }
    }
}
