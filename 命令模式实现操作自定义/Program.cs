using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 命令模式实现操作自定义
{
    public abstract class Command
    {
         protected Receiver receiver;
         public char Key;
         public Command(Receiver receiver, char Key)
         {
              this.receiver = receiver;
              this.Key = Key;
         }
        abstract public void Execute();
    }

    public class KeyCommand : Command
    {
        public KeyCommand(Receiver receiver, char Key) : base(receiver, Key) 
        {

        }

        public override void Execute()
        {
            receiver.Action();
        }
    }

    public abstract class Receiver 
    {
        public abstract void Action();
    }

    public class UpReceiver : Receiver
    {
        public override void Action()
        {
            Console.WriteLine("玩家向上走");
        }
    }

    public class DownReceiver : Receiver
    {
        public override void Action()
        {
            Console.WriteLine("玩家向下走");
        }
    }

    public class LeftReceiver : Receiver
    {
        public override void Action()
        {
            Console.WriteLine("玩家向左走");
        }
    }

    public class RightReceiver : Receiver
    {
        public override void Action()
        {
            Console.WriteLine("玩家向右走");
        }
    }

    public  class Invoker
    {
        private List<Command> commands = new List<Command>();
        public void AddCommand(Command command)
        {
            commands.Add(command);
        }

        public void ExecuteCommand(char key)
        {
            foreach (var command in commands)
	        {
                if (command.Key == key)
                    command.Execute();
	        }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Invoker invoker = new Invoker();
            Console.WriteLine("***************** 改建功能 *****************");
            Console.WriteLine("输入向上操作的键：");
            invoker.AddCommand(new KeyCommand(new UpReceiver(), Console.ReadKey().KeyChar));
            Console.WriteLine("输入向下操作的键：");
            invoker.AddCommand(new KeyCommand(new DownReceiver(),  Console.ReadKey().KeyChar));
            Console.WriteLine("输入向左操作的键：");
            invoker.AddCommand(new KeyCommand(new LeftReceiver(),  Console.ReadKey().KeyChar));
            Console.WriteLine("输入向右操作的键：");
            invoker.AddCommand(new KeyCommand(new RightReceiver(),  Console.ReadKey().KeyChar));

            while(true) {
                Console.WriteLine("  操作：");
                invoker.ExecuteCommand(Console.ReadKey().KeyChar);
            }
        }
    }
}
