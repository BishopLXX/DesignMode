using System;
using System.Collections.Generic;
using System.Text;

/*
 * 桥接模式适用于：当设计多个角色，多个武器，任意角色可以使用任意武器的时候。
 * 分别定义一个角色接口和武器接口，武器接口定义开火方法，角色接口定义开火方法（调用武器接口的开火方法）。
 */


namespace 桥接模式
{
    abstract class Implementor
    {
        public abstract void Operation();
    }

    class ConcreteImplementorA : Implementor
    {
        public override void Operation()
        {
            Console.WriteLine("具体实现A的方法执行");
        }
    }

    class ConcreteImplementorB : Implementor {
        public override void Operation()
        {
            Console.WriteLine("具体实现B的方法执行");
        }
    }

    class Abstraction
    {
        protected Implementor implementor;
        public void SetImplementor(Implementor implementor)
        {
            this.implementor = implementor;
        }

        public virtual void Operation()
        {
            implementor.Operation();
        }
    }

    class RefinedAbstraction : Abstraction
    {
        public override void Operation()
        {
            implementor.Operation();
        }
    }

    class RefinedAbstraction2 : Abstraction
    {
        public override void Operation()
        {
            implementor.Operation();
        }
    }
    

    class Program
    {
        static void Main(string[] args)
        {
            Abstraction ab = new RefinedAbstraction();
            ab.SetImplementor(new ConcreteImplementorA());
            ab.Operation();
            ab.SetImplementor(new ConcreteImplementorB());
            ab.Operation();
            Abstraction ab2 = new RefinedAbstraction2();
            ab2.SetImplementor(new ConcreteImplementorA());
            ab2.Operation();
            ab2.SetImplementor(new ConcreteImplementorB());
            ab2.Operation();
            Console.Read();
        }
    }
}
