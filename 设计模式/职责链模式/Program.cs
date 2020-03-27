using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 职责链模式
{
    #region 原型
    //abstract class Handler {
    //    protected Handler successor;
    //    public void SetSuccessor(Handler successor)
    //    {
    //        this.successor = successor;
    //    }
    //    public abstract void HandleRequest(int request);
    //}

    //class ConcreteHandler1 : Handler
    //{
    //    public override void HandleRequest(int request)
    //    {
    //        if (request >= 0 && request < 10)
    //        {
    //            Console.WriteLine("{0} 处理请求 {1}", this.GetType().Name, request);
    //        } else if (successor != null)
    //        {
    //            successor.HandleRequest(request);
    //        }
    //    }
    //}
    //class ConcreteHandler2 : Handler
    //{
    //    public override void HandleRequest(int request)
    //    {
    //        if (request >= 10 && request < 20)
    //        {
    //            Console.WriteLine("{0} 处理请求 {1}", this.GetType().Name, request);
    //        }
    //        else if (successor != null)
    //        {
    //            successor.HandleRequest(request);
    //        }
    //    }
    //}
    //class ConcreteHandler3 : Handler
    //{
    //    public override void HandleRequest(int request)
    //    {
    //        if (request >= 20 && request < 30)
    //        {
    //            Console.WriteLine("{0} 处理请求 {1}", this.GetType().Name, request);
    //        }
    //        else if (successor != null)
    //        {
    //            successor.HandleRequest(request);
    //        }
    //    }
    //}
    #endregion


    // 申请
    class Request
    {
        // 申请类别
        private string requestType;
        public string RequestType
        {
            get { return requestType; }
            set { requestType = value; }
        }

        // 申请类容
        private string requestContent;
        public string RequestContent
        {
            get { return requestContent; }
            set { requestContent = value; }
        }

        // 数量
        private int number;
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
    }

    abstract class Manager
    {
        protected string name;
        protected Manager superior;  // 管理者的上级

        public Manager(string name)
        {
            this.name = name;
        }

        public void SetSuperoir(Manager auperior)
        {
            this.superior = auperior;
        }

        // 申请请求
        abstract public void RequestApplications(Request request);
    }

    // 经理
    class CommonManager: Manager
    {
        public CommonManager(string name): base(name) { }

        public override void RequestApplications(Request request)
        {
            if (request.RequestType == "请假" && request.Number <= 2)
            {
                Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
            }
            else
            {
                if (superior != null)
                    superior.RequestApplications(request);
            }
        }
    }

    class MajorManager : Manager
    {
        public MajorManager(string name) : base(name) { }

        public override void RequestApplications(Request request)
        {
            if (request.RequestType == "请假" && request.Number <= 5)
            {
                Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
            }
            else
            {
                if (superior != null)
                    superior.RequestApplications(request);
            }
        }
    }

    class GeneralManager : Manager
    {
        public GeneralManager(string name) : base(name) { }

        public override void RequestApplications(Request request)
        {
            if (request.RequestType == "请假")
            {
                Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
            }
            else if(request.RequestType == "加薪" && request.Number <= 500)
            {
                Console.WriteLine("{0}:{1} 数量{2} 被批准", name, request.RequestContent, request.Number);
            }
            else if (request.RequestType == "加薪" && request.Number > 500)
            {
                Console.WriteLine("{0}:{1} 数量{2} 再说吧", name, request.RequestContent, request.Number);
            }
        }
    }




    class Program
    {
        //static void Main(string[] args)
        //{
        //    Handler h1 = new ConcreteHandler1();
        //    Handler h2 = new ConcreteHandler2();
        //    Handler h3 = new ConcreteHandler3();
        //    h1.SetSuccessor(h2);
        //    h2.SetSuccessor(h3);
        //    int[] requests = { 2, 5, 14, 22, 18, 3, 27, 20 };
        //    foreach (int request in requests)
        //    {
        //        h1.HandleRequest(request);
        //    }
        //    Console.Read();
        //}
        static void Main(string[] args)
        {
            CommonManager jinli = new CommonManager("经理");
            MajorManager zongjian = new MajorManager("总监");
            GeneralManager zongjinli = new GeneralManager("总监理");
            jinli.SetSuperoir(zongjian);
            zongjian.SetSuperoir(zongjinli);

            Request request = new Request();
            request.RequestType = "请假";
            request.RequestContent = "小菜请假";
            request.Number = 1;
            jinli.RequestApplications(request);

            Request request2 = new Request();
            request2.RequestType = "请假";
            request2.RequestContent = "小菜请假";
            request2.Number = 4;
            jinli.RequestApplications(request2);

            Request request3 = new Request();
            request3.RequestType = "加薪";
            request3.RequestContent = "小菜请求加薪";
            request3.Number = 400;
            jinli.RequestApplications(request3);

            Console.Read();
        }
    }
}
