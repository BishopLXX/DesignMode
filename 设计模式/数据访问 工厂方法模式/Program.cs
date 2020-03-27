using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

// 抽象工厂模式
namespace 数据访问_工厂方法模式
{
    class User
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
    class Department
    {
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _deptName;
        public string DeptName
        {
            get { return _deptName; }
            set { _deptName = value; }
        }
    }

    interface IUser
    {
        void Insert(User user);
        User GetUser(int id);
    }
    

    class SqlServerUser : IUser
    {
        public User GetUser(int id)
        {
            Console.WriteLine("在SQL server中根据ID获得User表一条记录");
            return null;
        }

        public void Insert(User user)
        {
            Console.WriteLine("在SQL Server 中给User表增加一条记录");
        }
    }

    class AccessUser : IUser
    {
        public User GetUser(int id)
        {
            Console.WriteLine("在Access server中根据ID获得User表一条记录");
            return null;
        }

        public void Insert(User user)
        {
            Console.WriteLine("在Access Server 中给User表增加一条记录");
        }
    }

    interface IDepartment
    {
        void Insert(Department department);
        Department GetDepartment(int id);
    }
    class SqlserverDepartment : IDepartment
    {
        public void Insert(Department department)
        {
            Console.WriteLine("在SQL server中给Department表添加一条记录");
        }
        public Department GetDepartment(int id)
        {
            Console.WriteLine("在SQL server中根据ID得到一条记录");
            return null;
        }
    }
    class AccessDepartment : IDepartment
    {
        public void Insert(Department department)
        {
            Console.WriteLine("在Access server中给Department表添加一条记录");
        }
        public Department GetDepartment(int id)
        {
            Console.WriteLine("在Access server中根据ID得到一条记录");
            return null;
        }
    }

    /*
     * 抽象工厂模式
    interface IFactory
    {
        IUser CreateUser();
        IDepartment CreateDepartment();
    }

    class SqlServerFactory: IFactory
    {
        public IUser CreateUser()
        {
            return new SqlServerUser();
        }
        public IDepartment CreateDepartment()
        {
            return new SqlserverDepartment();
        }
    }

    class AccessFactory : IFactory
    {
        public IUser CreateUser()
        {
            return new AccessUser();
        }
        public IDepartment CreateDepartment()
        {
            return new AccessDepartment();
        }
    }*/


    // 简单工厂模式
    class DataAccess
    {
        private static readonly string db = "SqlServer";
        public static IUser CreateUser()
        {
            IUser result = null;
            switch (db)
            {
                case "Sqlserver":
                    result = new SqlServerUser();
                    break;
                case "Access":
                    result = new AccessUser();
                    break;
                default:
                    break;
            }
            return result;
        }

        public static IDepartment CreateDepartment()
        {
            IDepartment result = null;
            switch (db)
            {
                case "Sqlserver":
                    result = new SqlserverDepartment();
                    break;
                case "Access":
                    result = new AccessDepartment();
                    break;
                default:
                    break;
            }
            return result;
        }
    }
    /* 反射
    class DataAccess
    {
        private static readonly string AssemblyName = "数据访问_工厂方法模式";
        private static readonly string db = "Sqlserver";

        public static IUser CreateUser()
        {
            string className = AssemblyName + "." + db + "User";
            return (IUser)Assembly.Load(AssemblyName).CreateInstance(className); 
        }
        public static IDepartment CreateDepartment()
        {
            string className = AssemblyName + "." + db + "User";
            return (IDepartment)Assembly.Load(AssemblyName).CreateInstance(className);
        }
    }
    */
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            Department dept = new Department();
            //IFactory factory = new SqlServerFactory();
            //IFactory factory = new AccessFactory();
            IUser iu = DataAccess.CreateUser();
            //IUser iu = (IUser)Assembly.Load("数据访问_工厂方法模式").CreateInstance("数据访问_工厂方法模式.SqlServerUser");
            iu.Insert(user);
            iu.GetUser(1);
            IDepartment id = DataAccess.CreateDepartment();
            id.Insert(dept);
            id.GetDepartment(1);
            Console.Read();
        }
    }
}
