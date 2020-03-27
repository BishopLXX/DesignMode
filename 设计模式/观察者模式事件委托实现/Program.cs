using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 观察者模式事件委托实现
{
    class StockObserver
    {
        private string name;
        private Subject sub;
        public StockObserver(string name, Subject sub)
        {
            this.name = name;
            this.sub = sub;
        }
        public void CloseStockMarket()
        {
            Console.WriteLine("{0}{1}关闭股票行情， 继续工作!", sub.SubjectState, name);
        }
    }
    class NBAObserver
    {
        private string name;
        private Subject sub;
        public NBAObserver(string name, Subject sub)
        {
            this.name = name;
            this.sub = sub;
        }

        public void CloseNBADirectSeeding()
        {
            Console.WriteLine("{0}{1}关闭NBA直播，继续工作!", sub.SubjectState, name);
        }
    }

    interface Subject
    {
        void Notify();
        string SubjectState
        {
            get;
            set;
        }
    }

    delegate void EventHandler();

    class Boss : Subject
    {
        public event EventHandler Update;
        private string action;
        public void Notify()
        {
            Update();
        }
        public string SubjectState
        {
            get { return action; }
            set { action = value; }
        }
    }

    class Secretary : Subject
    {
        public event EventHandler Update;
        private string action;
        public void Notify()
        {
            Update();
        }
        public string SubjectState
        {
            get { return action; }
            set { action = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Boss huhansan = new Boss();
            StockObserver tongshi1 = new StockObserver("魏冠斋", huhansan);
            NBAObserver tongshi2 = new NBAObserver("以观察", huhansan);
            huhansan.Update += new EventHandler(tongshi1.CloseStockMarket);
            huhansan.Update += new EventHandler(tongshi2.CloseNBADirectSeeding);

            huhansan.SubjectState = "我胡汉三回来了";
            huhansan.Notify();
            Console.Read();
        }
    }
}
