using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 观察者模式实现成就系统
{
    public class PlayerInfoSubject
    {
        private int killed = 0;
        private int cured = 0;
        public int Killed
        {
            get { return killed; }
            set { killed = value;
                this.Notify();
            }
        }
        public int Cured
        {
            get { return cured; }
            set
            {
                cured = value;
                this.Notify();
            }
        }


        private List<AchievementObserver> achievementObservers = new List<AchievementObserver>();

        public void Attach(AchievementObserver observer)
        {
            achievementObservers.Add(observer);
        }
        public void Detach(AchievementObserver observer)
        {
            achievementObservers.Remove(observer);
        }

        public void Notify()
        {
            foreach (AchievementObserver observer in achievementObservers)
            {
                observer.Update();
            }
        }
    }

    public abstract class AchievementObserver
    {
        protected bool isGeted = false;
        protected PlayerInfoSubject playerInfoSubject;
        public AchievementObserver(PlayerInfoSubject playerInfoSubject)
        {
            this.playerInfoSubject = playerInfoSubject;
        }

        public abstract void Update();
    }

    public class Kill_1_AchievementObServer : AchievementObserver
    {
        private int kill = 1;

        public Kill_1_AchievementObServer(PlayerInfoSubject playerInfoSubject) : base(playerInfoSubject)
        {

        }

        public override void Update()
        {
            if (this.isGeted == false && playerInfoSubject.Killed >= kill)
            {
                this.isGeted = true;
                Console.WriteLine("完成成就: 第一滴血");
            }
        }
    }

    public class Kill_10_AchievementObServer : AchievementObserver
    {
        private int kill = 10;

        public Kill_10_AchievementObServer(PlayerInfoSubject playerInfoSubject) : base(playerInfoSubject)
        {

        }

        public override void Update()
        {
            if (this.isGeted == false && playerInfoSubject.Killed >= kill)
            {
                this.isGeted = true;
                Console.WriteLine("完成成就: 十人斩");
            }
        }
    }

    public class Cure_1_AchievementObServer : AchievementObserver
    {
        private int cure = 1;

        public Cure_1_AchievementObServer(PlayerInfoSubject playerInfoSubject) : base(playerInfoSubject)
        {

        }

        public override void Update()
        {
            if (this.isGeted == false && playerInfoSubject.Cured >= cure)
            {
                this.isGeted = true;
                Console.WriteLine("完成成就: 救死扶伤");
            }
        }
    }

    public class Cure_10_AchievementObServer : AchievementObserver
    {
        private int cure = 10;

        public Cure_10_AchievementObServer(PlayerInfoSubject playerInfoSubject) : base(playerInfoSubject)
        {

        }

        public override void Update()
        {
            if (this.isGeted == false && playerInfoSubject.Cured >= cure)
            {
                this.isGeted = true;
                Console.WriteLine("完成成就: 妙手回春");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PlayerInfoSubject playerInfoSubject = new PlayerInfoSubject();
            playerInfoSubject.Attach(new Kill_1_AchievementObServer(playerInfoSubject));
            playerInfoSubject.Attach(new Kill_10_AchievementObServer(playerInfoSubject));
            playerInfoSubject.Attach(new Cure_1_AchievementObServer(playerInfoSubject));
            playerInfoSubject.Attach(new Cure_10_AchievementObServer(playerInfoSubject));

            for (int i = 0; i <= 10; i++)
            {
                playerInfoSubject.Killed = i;
                playerInfoSubject.Cured = i;
                Console.WriteLine(playerInfoSubject.Killed.ToString() + " " + playerInfoSubject.Cured.ToString());
            }
            
            Console.Read();
        }
    }
}
