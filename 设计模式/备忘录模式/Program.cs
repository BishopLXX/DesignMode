using System;
using System.Collections.Generic;
using System.Text;

namespace 备忘录模式
{
    class GameRole
    {
        private int vit;
        private int atk;
        private int def;
        public RoleStateMemento SaveState()
        {
            return (new RoleStateMemento(vit, atk, def));
        }

        public void RecoveryState(RoleStateMemento memento)
        {
            this.vit = memento.Vitality;
            this.atk = memento.Attack;
            this.def = memento.Defense;
        }

        internal void GetInitState()
        {
            this.vit = 100;
            this.atk = 100;
            this.def = 100;
        }

        internal void StateDisplay()
        {
            Console.WriteLine("vit: " + this.vit);
            Console.WriteLine("atk: " + this.atk);
            Console.WriteLine("def: " + this.def);
        }

        internal void Fight()
        {
            Random random = new Random();
            this.vit -= random.Next(100);
            this.atk -= random.Next(100);
            this.def -= random.Next(100);
        }
    }

    class RoleStateMemento
    {
        private int vit;
        private int atk;
        private int def;

        public RoleStateMemento(int vit, int atk, int def)
        {
            this.vit = vit;
            this.atk = atk;
            this.def = def;
        }

        public int Vitality
        {
            get { return vit; }
            set { vit = value; }
        }
        public int Attack
        {
            get { return atk; }
            set { vit = value; }
        }
        public int Defense
        {
            get { return def; }
            set { def = value; }
        }
    }

    class RoleStateCaretaker
    {
        private RoleStateMemento memento;
        public RoleStateMemento Memento
        {
            get { return memento; }
            set { memento = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // 大战boos前
            GameRole liuxiaoxiao = new GameRole();
            liuxiaoxiao.GetInitState();
            liuxiaoxiao.StateDisplay();

            // 保存进度
            RoleStateCaretaker stateAdmin = new RoleStateCaretaker();
            stateAdmin.Memento = liuxiaoxiao.SaveState();

            // 大战boss时，损耗严重
            liuxiaoxiao.Fight();
            liuxiaoxiao.StateDisplay();

            // 恢复之前状态
            liuxiaoxiao.RecoveryState(stateAdmin.Memento);
            liuxiaoxiao.StateDisplay(); 

            Console.Read();
        }
    }



}
 