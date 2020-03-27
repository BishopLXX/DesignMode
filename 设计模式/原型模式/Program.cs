using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*原型模式，用原型实例指定创建对象的种类，并且通过拷贝这些原型创建新的对象
 * 
 */

namespace 原型模式
{
    abstract class Prototype  // 原型类
    {
        private string id;
        public Prototype(string id)
        {
            this.id = id;
        }

        public string Id
        {
            get { return id; }
        }

        public abstract Prototype Clone();
    }

    class ConcretePrototypel : Prototype
    {
        public ConcretePrototypel(string id): base(id)
        {

        }

        public override Prototype Clone()
        {
            return (Prototype)this.MemberwiseClone();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            ConcretePrototypel pl = new ConcretePrototypel("I");
            ConcretePrototypel c1 = (ConcretePrototypel)pl.Clone();
            Console.WriteLine("Cloned: {0}", c1.Id);
            Console.Read();
        }
    }
}
