using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 策略模式
{
    abstract class Strategy
    {
        public abstract void AlgorithmInterface();
    }

    class ConcreteStrategyA: Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("算法A实现");
        }
    }
    class ConcreteStrategyB : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("算法B实现");
        }
    }
    class ConcreteStrategyC : Strategy
    {
        public override void AlgorithmInterface()
        {
            Console.WriteLine("算法C实现");
        }
    }

    class Context
    {
        Strategy Strategy;
        // 也可以结合简单工厂模式，实现类型名称字符串
        public Context(Strategy strategy)
        {
            this.Strategy = strategy;
        }

        public void ContextInterface()
        {
            Strategy.AlgorithmInterface();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Context context;
            context = new Context(new ConcreteStrategyA());
            context.ContextInterface();
            context = new Context(new ConcreteStrategyB());
            context.ContextInterface();
            context = new Context(new ConcreteStrategyC());
            context.ContextInterface();

            Console.Read();
        }
    }
}
