using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 何时使用外观模式：
 * 首先，在设计初期阶段，应该要有意识的将不同的两个层分离，层与层之间建立外观。
 * 其次，在开发阶段，子系统往往因为不断的重构演化而变得越来越复杂，增加外观Facade可以提供一个简单的接口，减少他们之间的依赖。
 * 第三，在维护一个一流的大型系统时，可能这个系统已经非常难以维护和扩展了，为新系统开发一个外观facade类，来提供设计粗糙或高度复杂的遗留代码的比较清晰简单的接口
 * 让新系统与Facade对象交互，Facade与一流代码交互所有复杂的工作。
 */


namespace 外观模式
{
    class SubSystemOne
    {
        public void MethodOne()
        {
            Console.WriteLine("子系统方法一");
        }
    }
    class SubSystemTwo
    {
        public void MethodTwo()
        {
            Console.WriteLine("子系统方法二");
        }
    }
    class SubSystemThree
    {
        public void MethodThree()
        {
            Console.WriteLine("子系统方法三");
        }
    }
    class SubSystemFour
    {
        public void MethodFour()
        {
            Console.WriteLine("子系统方法四");
        }
    }

    class Facade
    {
        SubSystemOne one;
        SubSystemTwo two;
        SubSystemThree three;
        SubSystemFour four;

        public Facade()
        {
            one = new SubSystemOne();
            two = new SubSystemTwo();
            three = new SubSystemThree();
            four = new SubSystemFour();
        }

        public void MethodA()
        {
            Console.WriteLine("\n 方法组A()-----");
            one.MethodOne();
            two.MethodTwo();
            four.MethodFour();
        }
        public void MethodB()
        {
            Console.WriteLine("\n 方法组B()-----");
            two.MethodTwo();
            three.MethodThree();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Facade facade = new Facade();
            facade.MethodA();
            facade.MethodB();
            Console.Read();
        }
    }
}

