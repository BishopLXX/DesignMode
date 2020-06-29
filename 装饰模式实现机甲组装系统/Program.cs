using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 装饰模式实现机甲组装系统
{
    // 实际的实例化物体
    class ModeObject
    {
        public int L = 0;

        public void Refit()
        {
            Console.Write(" - ");  // 实例化的改装
            L++;
        }
    }

    class IMeCha 
    {
        public virtual void Assemble(ModeObject modeObject) 
        {
            modeObject.Refit();
        }
    }

    class Gundam : IMeCha
    {

        public override void Assemble(ModeObject modeObject)
        {
            base.Assemble(modeObject);
            Console.Write(" ");
        }
    }

    class AdvGundam : IMeCha
    {
        public AdvGundam gundam;

        public AdvGundam(AdvGundam gundam)
        {
            this.gundam = gundam;
        }

        public override void Assemble(ModeObject modeObject)
        {
            if (this.gundam != null)
            {
                gundam.Assemble(modeObject);
            }
        }
    }

    class SSRGundam : AdvGundam
    {
        public SSRGundam(AdvGundam gundam) : base(gundam)
        {
            
        }

        public override void Assemble(ModeObject modeObject)
        {
            base.Assemble(modeObject);
            modeObject.Refit();
            Console.Write("SSR级机身 ");
        }
    }

    class TitansGundam : AdvGundam
    {

        public TitansGundam(AdvGundam gundam) : base(gundam)
        {
            
        }
        public override void Assemble(ModeObject modeObject)
        {
            base.Assemble(modeObject);
            modeObject.Refit();  // 这一行代表实例化的改装，也可以是修改这个对象的东西
            Console.Write("泰坦级动力引擎 ");  // 代表其他的改装
        }
    }

    class RuinGundam : AdvGundam
    {

        public RuinGundam(AdvGundam gundam) : base(gundam)
        {
            
        }
        public override void Assemble(ModeObject modeObject)
        {
            base.Assemble(modeObject);
            modeObject.Refit();
            Console.Write("左武器槽：摧星炮 ");
        }
    }

    class ZeusGundam : AdvGundam
    {

        public ZeusGundam(AdvGundam gundam) : base(gundam)
        {
            
        }
        public override void Assemble(ModeObject modeObject)
        {
            base.Assemble(modeObject);
            modeObject.Refit();
            Console.Write("右武器槽：宙斯盾 ");
        }
    }

    class EagleGundam : AdvGundam
    {
        public EagleGundam(AdvGundam gundam) : base(gundam)
        {
            
        }
        public override void Assemble(ModeObject modeObject)
        {
            base.Assemble(modeObject);
            Console.Write("  " + "搭载--鹰眼雷达 ");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ModeObject modeObject = new ModeObject();

            IMeCha myGundam = new Gundam();

            Console.WriteLine("裸机：");

            myGundam.Assemble(modeObject);
            Console.WriteLine("  等级：" + modeObject.L);

            myGundam = new SSRGundam(myGundam as AdvGundam);
            
            myGundam = new TitansGundam(myGundam as AdvGundam);
         
            myGundam = new RuinGundam(myGundam as AdvGundam);
            
            myGundam = new ZeusGundam(myGundam as AdvGundam);

            Console.WriteLine("");
            Console.WriteLine("普通组装：");
            myGundam.Assemble(modeObject);
            Console.WriteLine("  等级：" + modeObject.L);
            
            Console.WriteLine("");
            Console.WriteLine("VIP组装：");
            myGundam = new EagleGundam(myGundam as AdvGundam);

            
            myGundam.Assemble(modeObject);
            Console.WriteLine("  等级：" + modeObject.L);

            Console.Read();
        }
    }
}
