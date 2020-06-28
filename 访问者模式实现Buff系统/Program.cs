using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 访问者模式实现Buff系统
{
    class BuffBase 
    {
        protected int duration;
        public int Duration  { get { return duration;} }  
        protected RoleBase owner;
        public RoleBase Owner  { get { return owner;} }                               


        protected BuffPool buffPool;


        public virtual void turnStart()
        {

        }
        public virtual void turnAccept() 
        {

        }
        public virtual void turnEnd() 
        {

        }

        public BuffBase(BuffPool buffPool, int duration, RoleBase owner) 
        {
            this.buffPool = buffPool;
            this.duration = duration;
            this.owner = owner;
        }

        public void AddDuration(int addNumber) 
        {
            this.duration += addNumber;
        }
    }

    class limitedBuff : BuffBase
    {
        public override void turnStart()
        {
            this.duration--;
        }

        public override void turnEnd()
        {
            if (this.duration == 0)
            {
                buffPool.RemoveBuff(this);
            }
        }

        public limitedBuff(BuffPool buffPool, int duration, RoleBase owner) : base(buffPool, duration, owner)
        {

        }
    }

    // 独立计算的。一直叠加
    class independentBuff : BuffBase 
    {
        public override void turnStart()
        {
            this.duration--;
        }

        public override void turnEnd()
        {
            if (this.duration == 0)
            {
                buffPool.RemoveBuff(this);
            }
        }

        public independentBuff(BuffPool buffPool, int duration, RoleBase owner) : base(buffPool, duration, owner)
        {

        }
    }

    class Reinforce : independentBuff
    {
        public override void turnAccept()
        {
            Console.WriteLine("加固");
            this.owner.Def *= 2;
        }

        public override void turnEnd()
        {
            base.turnEnd();
            this.owner.Def /=2;
        }

        public Reinforce(BuffPool buffPool, int duration, RoleBase owner) : base(buffPool, duration, owner)
        {

        }
    }


    class ReplylimitedBuff : limitedBuff
    {
        protected int replayNumber;

        public ReplylimitedBuff(BuffPool buffPool, int duration, RoleBase owner, int replayNumber) : base(buffPool, duration, owner)
        {
            this.replayNumber = replayNumber;
        }

        public override void turnAccept()
        {
            Console.WriteLine(owner.Name + "回复" + this.replayNumber + "血");
            owner.HP += this.replayNumber;
        }
    }

    class RagelimitedBuff : limitedBuff
    {
        protected int rageNumber;
        protected int costHpNumber;

        public RagelimitedBuff(BuffPool buffPool, int duration, RoleBase owner, int rageNumber, int costHpNumber) : base(buffPool, duration, owner)
        {
            this.rageNumber = rageNumber;
            this.costHpNumber = costHpNumber;
        }

        public override void turnAccept()
        {
            Console.WriteLine(owner.Name + "牺牲" + this.costHpNumber + "血，狂暴：  +" + rageNumber + "攻击力");
            owner.HP -= this.rageNumber;
            owner.Atk += this.costHpNumber;
        }
    }

    class BuffPool
    {
        private IList<BuffBase> buffBases = new List<BuffBase>();

        public int Count { get {return buffBases.Count;}}


        public void AddBuff(BuffBase buffBase)
        {

            if (typeof(limitedBuff).IsAssignableFrom(buffBase.GetType())) {
                foreach (BuffBase buff in buffBases)
	            {
                    if (buff.Owner == buffBase.Owner)
                    {
                        buff.AddDuration(buffBase.Duration);
                        return;
                    }
	            }
                buffBases.Add(buffBase);
            }

            if (typeof(independentBuff).IsAssignableFrom(buffBase.GetType())) {
                Console.WriteLine("123");
                buffBases.Add(buffBase);
            }
            
        }

        public bool RemoveBuff(BuffBase buffBase)
        {
            return buffBases.Remove(buffBase);
        }

        public void turnStart()
        {
            foreach (BuffBase buffBase in buffBases)
	        {
                buffBase.turnStart();
	        }
        }

        public void turnAccept() 
        {
            foreach (BuffBase buffBase in buffBases)
	        {
                buffBase.turnAccept();
	        }
        }

        public void turnEnd() 
        {
            // foreach (BuffBase buffBase in buffBases)
	        // {
            //     buffBase.turnEnd();
	        // }

            int n = buffBases.Count;
            for (int i = 0; i < n; i++)
			{
                Console.WriteLine(n);
                buffBases[i].turnEnd();
                if (n == buffBases.Count)
                {
                    continue;
                } 
                else
                {
                    int d = n - buffBases.Count;
                    n = buffBases.Count;
                    i--;
                }
			}
        }
    }

    class RoleBase
    {
        public string Name;
        public int HP;
        public int Atk;
        public int Def;

        public RoleBase(string name, int hp = 30, int atk = 1, int def = 1)
        {
            this.Name = name;
            this.HP = hp;
            this.Atk = atk;
            this.Def = def;
        }

        public void Display()
        {
            Console.WriteLine(this.Name + " " + this.HP + " " + this.Atk + " "+ this.Def);
        }
    }

    class Stage
    {
        public virtual void StageRun(BuffPool buffPool) {}
    }

    class StartStage : Stage
    {
         public override void StageRun(BuffPool buffPool)
         {
             buffPool.turnStart();
         }
    }

    class AcceptStage : Stage
    {
         public override void StageRun(BuffPool buffPool)
         {
             buffPool.turnAccept();
         }
    }

    class EndStage : Stage
    {
         public override void StageRun(BuffPool buffPool)
         {
             buffPool.turnEnd();
         }
    }

    class TurnStructure
    {
        private int current = 0;
        private List<Stage> turnStages = new List<Stage>();

        public void Attach(Stage turnStage)
        {
            turnStages.Add(turnStage);
        }

        public void Detach(Stage turnStage)
        {
            turnStages.Remove(turnStage);
        }

        public void Turn(BuffPool buffPool)
        {
            if (buffPool.Count > 0)
            {
                turnStages[current].StageRun(buffPool);
            }
            
            current++;
            if (current >= turnStages.Count) current = 0;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            TurnStructure turnStructure = new TurnStructure();
            turnStructure.Attach(new StartStage());
            turnStructure.Attach(new AcceptStage());
            turnStructure.Attach(new EndStage());

            BuffPool buffPool = new BuffPool();
            RoleBase player = new RoleBase("Player");
            RoleBase enemy = new RoleBase("Enemy");

            Console.WriteLine("第一回合");
            turnStructure.Turn(buffPool);
            turnStructure.Turn(buffPool);
            turnStructure.Turn(buffPool);

            ReplylimitedBuff reply = new ReplylimitedBuff(buffPool, 3, player, 1);
            buffPool.AddBuff(reply);

            RagelimitedBuff rage = new RagelimitedBuff(buffPool, 5, enemy, 2, 10);
            buffPool.AddBuff(rage);

            Console.WriteLine("第二回合");
            turnStructure.Turn(buffPool);
            turnStructure.Turn(buffPool);
            turnStructure.Turn(buffPool);
            player.Display();
            enemy.Display();

            ReplylimitedBuff reply2 = new ReplylimitedBuff(buffPool, 5, enemy, 3);
            buffPool.AddBuff(reply2);

            RagelimitedBuff rage2 = new RagelimitedBuff(buffPool, 5, enemy, 5, 10);
            buffPool.AddBuff(rage2);

            Console.WriteLine("第三回合");
            turnStructure.Turn(buffPool);
            turnStructure.Turn(buffPool);
            turnStructure.Turn(buffPool);
            player.Display();
            enemy.Display();

            Console.WriteLine("第四回合");
            turnStructure.Turn(buffPool);
            turnStructure.Turn(buffPool);
            turnStructure.Turn(buffPool);
            player.Display();
            enemy.Display();

            Reinforce reinforce = new Reinforce(buffPool, 2, player);
            buffPool.AddBuff(reinforce);

            Console.WriteLine("第五回合");
            turnStructure.Turn(buffPool);
            turnStructure.Turn(buffPool);

            Console.WriteLine("回合中");
            player.Display();
            enemy.Display();

            turnStructure.Turn(buffPool);
            Console.WriteLine("回合结束后");
            player.Display();
            enemy.Display();

            Reinforce reinforce2 = new Reinforce(buffPool, 2, player);
            buffPool.AddBuff(reinforce2);

            Console.WriteLine("第六回合");
            turnStructure.Turn(buffPool);
            turnStructure.Turn(buffPool);

            Console.WriteLine("回合中");
            player.Display();
            enemy.Display();
            turnStructure.Turn(buffPool);
            Console.WriteLine("回合结束后");
            player.Display();
            enemy.Display();

            Console.Read();
        }
    }
}
