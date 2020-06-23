using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 职责链模式实现塔防关卡设计
{
    public struct LevelData
    {
        public int fight;
        public int build;
        public int science;

        public LevelData(int fight = -1, int build = -1, int science = -1)
        {
            this.fight = fight;
            this.build = build;
            this.science = science;
        }

    }

    public abstract class LevelHandler
    {
        protected LevelHandler successor;

        public void SetSuccessor(LevelHandler successor)
        {
            this.successor = successor;
        }

        public abstract void ConsultRequest(LevelData levelData);
    }

    public class Level_1 : LevelHandler
    {
        private LevelData levelData = new LevelData(5,0,0);

        public override void ConsultRequest(LevelData levelData)
        {
            if (levelData.fight <= this.levelData.fight)
            {
                Console.WriteLine("创建一级小怪:");
            } 
            else
            {
                this.successor.ConsultRequest(levelData);
            }
        }
    }

    public class Level_2 : LevelHandler
    {
        private LevelData levelData = new LevelData(20,15,0);

        public override void ConsultRequest(LevelData levelData)
        {
            if (levelData.build <= this.levelData.build || levelData.fight <= this.levelData.fight) 
            {
                Console.WriteLine("创建二级小怪:");
            } 
            else
            {
                this.successor.ConsultRequest(levelData);
            }
        }
    }

    public class Level_3 : LevelHandler
    {
        private LevelData levelData = new LevelData(30,10,29);

        public override void ConsultRequest(LevelData levelData)
        {
            if (levelData.science <= this.levelData.science) 
            {
                Console.WriteLine("创建三级小怪:");
            } 
            else
            {
                Console.WriteLine("创建Boss");
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            LevelData levelData = new LevelData(0,0,0);
            Level_1 level_1 = new Level_1();
            Level_2 level_2 = new Level_2();
            Level_3 level_3 = new Level_3();
            level_1.SetSuccessor(level_2);
            level_2.SetSuccessor(level_3);


            for (int i = 0; i < 30; i++)
			{
                levelData.fight += 1;
                levelData.build += 1;
                levelData.science += 1;
                Console.WriteLine(levelData.fight.ToString() +" "+ levelData.build.ToString() + " " + levelData.science.ToString());
                level_1.ConsultRequest(levelData);
			}

            Console.Read();
        }
    }
}
