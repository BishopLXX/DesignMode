using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* 远程代理： 为一个对象在不同的地址空间提供局部代表。这样可以隐藏一个对象存在不同地址空间的事实
 * 虚拟代理： 根据需要创建开销很大的对象，通过它来存放实例化需要很长时间的真实对象
 * 安全代理： 用来控制真实对象访问时的权限
 * 智能指引： 当调用真是的对象时，代理处理另外一些事
*/
namespace 代理模式
{
    abstract class Subject
    {
        public abstract void Request();
    }

    class RealSubject:Subject
    {
        public override void Request()
        {
            Console.WriteLine("真实的请求");
        }
    }

    class Proxy : Subject
    {
        RealSubject realSubject;
        public override void Request()
        {
            if (realSubject == null)
            {
                realSubject = new RealSubject();
            }
            realSubject.Request();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Proxy proxy = new Proxy();
            proxy.Request();
            Console.Read();
        }
    }
}
