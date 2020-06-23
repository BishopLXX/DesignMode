using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 备忘录模式实现地图对象存档
{
    // 备忘录的实际备份值
    public class Meme
    {
        protected string owner = "";
        public string Owner
        {
            get { return owner; }
        }
        protected float[] position = new float[3];
        public float[] Position
        {
            get { return position; }
        }
        protected int level = 0;
        public int Level
        {
            get { return level; }
        }

        public Meme(string owner, float[] position, int level)
        {
            this.owner = owner;
            this.position = position;
            this.level = level;
        }
    }

    public class BuildingMeme : Meme
    {
        
        private int science = 0;
        public int Science
        {
            get { return science; }
        }

        public BuildingMeme(string owner, float[] position, int level, int science) : base(owner, position, level)
        {
            this.science = science;
        }
    }

    public class RoleMeme : Meme
    {

        private int atk = 0;
        public int Atk
        {
            get { return atk; }
        }

        private int def = 0;
        public int Def
        {
            get { return def; }
        }

        public RoleMeme(string owner, float[] position, int level, int atk, int def) : base(owner, position, level)
        {
            this.atk = atk;
            this.def = def;
        }
    }

    // 备忘录的发起人
    public abstract class IOriginator
    {
        /// <summary>
        /// 恢复存档
        /// </summary>
        public abstract void RecoveryState(Meme meme);
        /// <summary>
        /// 存档
        /// </summary>
        public abstract Meme SaveState();
    }

    public class Role : IOriginator
    {
        public int atk = 0;
        public int def = 0;
        public string owner = "";
        public float[] position = new float[3];
        public int level = 0;

        public Role(int atk, int def, string owner, float[] position, int level)
        {
            this.atk = atk;
            this.def = def;
            this.owner = owner;
            this.position = position;
            this.level = level;
        }

        public void Display()
        {
            Console.WriteLine("Role:    Atk-" + this.atk.ToString() + "  def-" + this.def.ToString() + "  owner-" + this.owner.ToString() + "  position-" + this.position[0].ToString()+" " + this.position[1].ToString()+ " " + this.position[2].ToString() + "  level=" + this.level.ToString());
        }

        public override void RecoveryState(Meme meme)
        {
            this.atk = (meme as RoleMeme).Atk;
            this.def = (meme as RoleMeme).Def;
            this.owner = (meme as RoleMeme).Owner;
            this.position = (meme as RoleMeme).Position;
            this.level = (meme as RoleMeme).Level;
        }

        public override Meme SaveState()
        {
            return new RoleMeme(this.owner, this.position, this.level, this.atk, this.def);
        }
    }

    public class Build : IOriginator
    {
        public int science = 0;
        public string owner = "";
        public float[] position = new float[3];
        public int level = 0;

        public Build(int science, string owner, float[] position, int level)
        {
            this.science = science;
            this.owner = owner;
            this.position = position;
            this.level = level;
        }

        public void Display()
        {
            Console.WriteLine("Build:    science-" + this.science.ToString() + "  owner-" + this.owner.ToString() + "  position-" + this.position[0].ToString()+" " + this.position[1].ToString()+ " " + this.position[2].ToString() + "  level=" + this.level.ToString());
        }

        public override void RecoveryState(Meme meme)
        {
            this.science = (meme as BuildingMeme).Science;
            this.owner = (meme as BuildingMeme).Owner;
            this.position = (meme as BuildingMeme).Position;
            this.level = (meme as BuildingMeme).Level;
        }

        public override Meme SaveState()
        {
            return new BuildingMeme(this.owner, this.position, this.level, this.science);
        }
    }

    // 备忘录
    public class Memento
    {
        private Dictionary<IOriginator, Meme> originators = new Dictionary<IOriginator, Meme>();

        public void AddOriginator(IOriginator originator)
        {
            this.originators.Add(originator, originator.SaveState());
        }

        public void RemoveOriginator(IOriginator originator)
        {
            this.originators.Remove(originator);
        }

        public void RecoveryAll()
        {
            foreach (var item in originators)
            {
                item.Key.RecoveryState(item.Value);
            }
        }

        public void SaveAll()
        {
            List<IOriginator> temp =  originators.Keys.ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                var key = temp[i];
                originators[key] = key.SaveState();
            }
        }

        public void Recovery(IOriginator originator)
        {
            Meme meme;
            originators.TryGetValue(originator, out meme);
            originator.RecoveryState(meme);
        }

        public void Save(IOriginator originator)
        {
            Meme meme = originator.SaveState();

            List<IOriginator> temp = originators.Keys.ToList();
            for (int i = 0; i < temp.Count; i++)
            {
                var key = temp[i];
                if (key == originator)
                {
                    originators[key] = key.SaveState();
                    return;
                }
            }
        }

        public Memento close()
        {
            Memento memento = new Memento();
            foreach (var item in this.originators)
	        {
                memento.originators.Add(item.Key, new Meme(item.Value.Owner, item.Value.Position, item.Value.Level));
	        }
            return memento;
        }
    }

    // 备忘录代理
    public class Caretaker
    {
        private Dictionary<string, Memento> mementos = new Dictionary<string, Memento>();
        private Memento currentMemento = new Memento();
        
        public void Save(string name = "")
        {
            Console.WriteLine("存档：" + name);
            if (!this.mementos.ContainsKey(name))
            {
                Memento memento = this.currentMemento.close();
                memento.SaveAll();
                this.mementos.Add(name, memento);
            }
            else 
            {
                Console.WriteLine("111");
                List<string> temp = this.mementos.Keys.ToList();
                for (int i = 0; i < temp.Count; i++)
                {
                    Console.WriteLine("222");
                    var key = temp[i];
                    if (this.mementos[key] == this.currentMemento)
                    {
                        Console.WriteLine("333");
                        this.mementos[key].SaveAll();
                        return;
                    }   
                }

               
            }
        }

        public void Recovery(string name)
        {
            Console.WriteLine("读档：" + name);
            
            foreach (var item in mementos)
	        {
                if (item.Key == name) 
                {
                    item.Value.RecoveryAll();
                    this.currentMemento =  item.Value;
                }
	        }
        }

        public void AddOriginator(IOriginator originator)
        {
            this.currentMemento.AddOriginator(originator);
        }

        public void RemoveOriginator(IOriginator originator)
        {
            this.currentMemento.RemoveOriginator(originator);
        }
        public void Recovery(IOriginator originator)
        {
            this.currentMemento.Recovery(originator);
        }

        public void Save(IOriginator originator)
        {
            this.currentMemento.Save(originator);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Caretaker caretaker = new Caretaker();

            Role player = new Role(1, 1, "player", new float[3]{0.1f, 0.2f, 0.3f}, 0);
            Role enemy = new Role(2, 2, "enemy", new float[3]{0.1f, 0.3f, 0.3f}, 0);
            Build playerHome = new Build(1, "player", new float[3]{0.0f, 0.0f, 0.0f}, 0);
            Build enemyHome = new Build(1, "enemy", new float[3]{0.2f, 0.2f, 0.2f}, 0);
            caretaker.AddOriginator(player);
            caretaker.AddOriginator(enemy);
            caretaker.AddOriginator(playerHome);
            caretaker.AddOriginator(enemyHome);

            player.Display();
            enemy.Display();
            playerHome.Display();
            enemyHome.Display();
            
            caretaker.Save("存档1");

            Console.WriteLine("玩家升级了");
            player.atk = 10;
            player.def = 10;
            player.level = 2;
            player.Display();
            enemy.Display();
            playerHome.Display();
            enemyHome.Display();

            caretaker.Recovery("存档1");

            player.Display();
            enemy.Display();
            playerHome.Display();
            enemyHome.Display();

            Console.WriteLine("玩家和敌人都升天了");
            player.position = new float[3]{10,10,10};
            enemy.position = new float[3]{10,10,10};
            player.Display();
            enemy.Display();
            playerHome.Display();
            enemyHome.Display();

            caretaker.Save(player);  // 局部保存玩家数据到当前的存档（存档1）
            Console.WriteLine("玩家自己数据存档了(存档1)中");
            player.Display();
            Console.WriteLine("之后都回到地面");
            player.position = new float[3]{1,1,1};
            enemy.position = new float[3]{1,1,1};
            player.Display();

            Console.WriteLine("敌人升级了");
            enemy.atk = 40;
            enemy.def = 40;
            player.Display();
            enemy.Display();
            playerHome.Display();
            enemyHome.Display();

            caretaker.Save("存档2");

            caretaker.Recovery("存档1");

            player.Display();
            enemy.Display();
            playerHome.Display();
            enemyHome.Display();

            caretaker.Recovery("存档2");
            player.Display();
            enemy.Display();
            playerHome.Display();
            enemyHome.Display();


            Console.Read();
        }
    }
}
