using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 享元模式
{
    class Program
    {
        abstract class Flyweight
        {
            public abstract void Operation(int extrinsicestate);
        }

        class ConcreteFlyweight: Flyweight
        {
            public override void Operation(int extrinsicestate)
            {
                Console.WriteLine("具体Flyweight:" + extrinsicestate);
            }
        }

        class UnSharedConcreteFlyweight : Flyweight
        {
            public override void Operation(int extrinsicestate)
            {
                Console.WriteLine("不共享的Flyweight:" + extrinsicestate);
            }
        }

        class FlyweightFactory
        {
            private Hashtable flyweights = new Hashtable();

            public FlyweightFactory()
            {
                flyweights.Add("X", new ConcreteFlyweight());
                flyweights.Add("Y", new ConcreteFlyweight());
                flyweights.Add("Z", new ConcreteFlyweight());
            }

            public Flyweight GetFlyweight(string key)
            {
                return ((Flyweight)flyweights[key]);
            }
        }

        static void Main(string[] args)
        {
            int extrinsicstate = 22;
            FlyweightFactory f = new FlyweightFactory();
            Flyweight fx = f.GetFlyweight("X");
            fx.Operation(--extrinsicstate);
            Flyweight fy = f.GetFlyweight("Y");
            fy.Operation(--extrinsicstate);
            Flyweight fz = f.GetFlyweight("Z");
            fz.Operation(--extrinsicstate);
            Flyweight uf = new UnSharedConcreteFlyweight();
            uf.Operation(--extrinsicstate);
            Console.Read();
        }
    }
}
