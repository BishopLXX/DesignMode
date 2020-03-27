using System;
using System.Collections.Generic;
using System.Text;

namespace 组合模式
{
    /* 组合模式原型
    // 原型
    abstract class Component
    {
        protected string name;
        public Component(string name)
        {
            this.name = name;
        }

        public abstract void Add(Component c);
        public abstract void Remove(Component c);
        public abstract void Display(int depth);
    }

    class Leaf : Component
    {
        public Leaf(string name): base(name) { }

        public override void Add(Component c)
        {
            Console.WriteLine("Cannot add to a leaf");
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
        }

        public override void Remove(Component c)
        {
            Console.WriteLine("Cannot remove to a leaf");
        }
    }

    class Composite : Component
    {
        private List<Component> children = new List<Component>();
        public Composite(string name) : base(name) { }

        public override void Add(Component c)
        {
            children.Add(c);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new String('-', depth) + name);
            foreach (Component component in children)
            {
                component.Display(depth + 2);
            }
        }

        public override void Remove(Component c)
        {
            children.Remove(c);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Composite root = new Composite("root");
            root.Add(new Leaf("Leaf A"));
            root.Add(new Leaf("Leaf B"));
            Composite comp = new Composite("Composite X");
            comp.Add(new Leaf("Leaf XA"));
            comp.Add(new Leaf("Leaf XB"));
            root.Add(comp);
            Composite comp2 = new Composite("Composite XY");
            comp.Add(new Leaf("Leaf XYA"));
            comp.Add(new Leaf("Leaf XYB"));
            comp.Add(comp2);
            root.Add(new Leaf("Leaf C"));
            Leaf leaf = new Leaf("Leaf D");
            root.Add(leaf);
            root.Remove(leaf);
            root.Display(1);
            Console.Read();
        }
    }
    */

    abstract class Company
    {
        protected string name;

        public Company(string name)
        {
            this.name = name;
        }

        public abstract void Add(Company c);
        public abstract void Remove(Company c);
        public abstract void Display(int depth);
        public abstract void LineOfDuty();
    }

    class ConcreteCompany : Company
    {
        private List<Company> childen = new List<Company>();

        public ConcreteCompany(string name) : base(name) { }
        public override void Add(Company c)
        {
            childen.Add(c);
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);

            foreach (Company company in childen)
            {
                company.Display(depth + 2);
            }
        }

        public override void LineOfDuty()
        {
            foreach (Company company in childen)
            {
                company.LineOfDuty();
            }
        }

        public override void Remove(Company c)
        {
            childen.Remove(c);
        }
    }

    class HRDepartment : Company
    {

        public HRDepartment(string name) : base(name) { }
        public override void Add(Company c)
        {
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);

        }

        public override void LineOfDuty()
        {
            Console.WriteLine("{0} 员工招聘培训管理", name);
        }

        public override void Remove(Company c)
        {
        }
    }

    class FinanceDepartment : Company
    {

        public FinanceDepartment(string name) : base(name) { }
        public override void Add(Company c)
        {
        }

        public override void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + name);

        }

        public override void LineOfDuty()
        {
            Console.WriteLine("{0} 公司财务收支管理", name);
        }

        public override void Remove(Company c)
        {
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            ConcreteCompany root = new ConcreteCompany("北京总公司");
            root.Add(new HRDepartment("总公司人力资源部"));
            root.Add(new FinanceDepartment("总公司财务部"));

            ConcreteCompany comp = new ConcreteCompany("上海华东分公司");
            root.Add(new HRDepartment("华东分公司人力资源部"));
            root.Add(new FinanceDepartment("华东分公司财务部"));
            root.Add(comp);

            ConcreteCompany comp1 = new ConcreteCompany("南京办事处");
            root.Add(new HRDepartment("南京办事处人力资源部"));
            root.Add(new FinanceDepartment("南京办事处财务部"));
            comp.Add(comp1);

            ConcreteCompany comp2 = new ConcreteCompany("杭州办事处");
            root.Add(new HRDepartment("杭州办事处人力资源部"));
            root.Add(new FinanceDepartment("杭州办事处财务部"));
            comp.Add(comp2);

            Console.WriteLine("\n 结构图: ");
            root.Display(1);

            Console.WriteLine("\n 职责: ");
            root.LineOfDuty();

            Console.Read();
        }
    }
}
