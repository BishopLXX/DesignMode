using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 工厂方法模式
{
    class LeiFeng
    {
        public void Sweep()
        {
            Console.WriteLine("扫地");
        }
        public void Wash()
        {
            Console.WriteLine("洗衣");
        }
        public void BuyRice()
        {
            Console.WriteLine("买米");
        }
    }

    class Undergraduate : LeiFeng { } // 学雷锋的大学生
    class Volunteer : LeiFeng { } // 社区志愿者

    /*
    // 简单工厂模式
    class SimpleFactory
    {
        public static LeiFeng CreateLeifeng(string type)
        {
            LeiFeng result = null;
            switch (type)
            {
                case "学雷锋的大学生":
                    result = new Undergraduate();
                    break;
                case "社区志愿者":
                    result = new Volunteer();
                    break;
            }
            return result;
        }
    }*/

    // 工厂方法模式
    interface IFactory
    {
        LeiFeng CreateLeiFeng();
    }
    // 学雷锋的大学生工厂
    class UndergraduateFactory: IFactory
    {
        public LeiFeng CreateLeiFeng()
        {
            return new Undergraduate();
        }
    }
    // 社区志愿者工厂
    class VolunteerFactory : IFactory
    {
        public LeiFeng CreateLeiFeng()
        {
            return new Volunteer();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IFactory factory = new UndergraduateFactory();
            LeiFeng student = factory.CreateLeiFeng();

            student.BuyRice();
            student.Sweep();
            student.Wash();

            Console.Read();
        }
    }
}
