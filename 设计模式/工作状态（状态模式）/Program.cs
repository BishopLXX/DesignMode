using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 工作状态_状态模式_
{
    class Program
    {
        public abstract class State
        {
            public abstract void WriteProgram(Work w);
        }

        public class ForenoonState : State
        {
            public override void WriteProgram(Work w)
            {
                if (w.Hour < 12)
                {
                    Console.WriteLine("当前时间：{0}点 上午工作，精神百倍", w.Hour);
                }
                else
                {
                    w.SetState(new NoonState());
                    w.WriteProgram();
                }
            }
        }

        public class NoonState : State
        {
            public override void WriteProgram(Work w)
            {
                if (w.Hour < 13)
                {
                    Console.WriteLine("当前时间：{0}点 饿了，午饭；犯困，午休", w.Hour);
                }
                else
                {
                    w.SetState(new AfternoonState());
                    w.WriteProgram();
                }
            }
        }
        public class AfternoonState : State
        {
            public override void WriteProgram(Work w)
            {
                if (w.Hour < 17)
                {
                    Console.WriteLine("当前时间：{0}点 下午状态不错，继续工作", w.Hour);
                }
                else
                {
                    w.SetState(new EveningState());
                    w.WriteProgram();
                }
            }
        }
        public class EveningState : State
        {
            public override void WriteProgram(Work w)
            {
                if (w.TaskFinished)
                {
                    w.SetState(new RestState());
                    w.WriteProgram();
                }
                else if (w.Hour < 21)
                {
                    Console.WriteLine("当前时间：{0}点 加班哦，疲倦至极", w.Hour);
                }
                else
                {
                    w.SetState(new SleepingState());
                    w.WriteProgram();
                }
            }
        }

        public class SleepingState : State
        {
            public override void WriteProgram(Work w)
            {
                    Console.WriteLine("当前时间：{0}点 不行了，睡觉了", w.Hour);
            }
        }
        public class RestState : State
        {
            public override void WriteProgram(Work w)
            {
                Console.WriteLine("当前时间：{0}点 下班回家了", w.Hour);
            }
        }

        public class Work
        {
            private State current;
            public Work()
            {
                current = new ForenoonState();
            }

            private double hour;
            public double Hour
            {
                get { return hour; }
                set { hour = value; }
            }

            private bool finish = false;
            public bool TaskFinished
            {
                get { return finish; }
                set { finish = value; }
            }

            public void SetState(State s)
            {
                current = s;
            }
            public void WriteProgram()
            {
                current.WriteProgram(this);
            }
        }

        static void Main(string[] args)
        {
            Work emergencyProjects = new Work();
            emergencyProjects.Hour = 9;
            emergencyProjects.WriteProgram();
            emergencyProjects.Hour = 10;
            emergencyProjects.WriteProgram();
            emergencyProjects.Hour = 12;
            emergencyProjects.WriteProgram();
            emergencyProjects.Hour = 13;
            emergencyProjects.WriteProgram();
            emergencyProjects.Hour = 14;
            emergencyProjects.WriteProgram();
            emergencyProjects.Hour = 17;
            emergencyProjects.WriteProgram();
            emergencyProjects.TaskFinished = false;
            emergencyProjects.Hour = 19;
            emergencyProjects.WriteProgram();
            emergencyProjects.Hour = 22;
            emergencyProjects.WriteProgram();
            Console.Read();
        }
    }
}  
