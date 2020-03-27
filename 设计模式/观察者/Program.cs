using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 观察者
{
    abstract class Subject
    {
        private IList<Observer> observers = new List<Observer>();

        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }
        public void Detach(Observer observer)
        {
            observers.Remove(observer);
        }
        public void Notify()
        {
            foreach (Observer observer in observers)
            {
                observer.Update();
            }
        }
    }

    abstract class Observer
    {
        public abstract void Update();
    }

    class ConcreteSubject : Subject
    {
        private string subjectState;
        public string SubjectState
        {
            get { return subjectState; }
            set { subjectState = value; }
        }
    }

    class ConCreteObserver : Observer
    {
        private string name;
        private string observerState;
        private ConcreteSubject subject;

        public ConCreteObserver(ConcreteSubject subject, string name)
        {
            this.subject = subject;
            this.name = name;
        }
        public override void Update()
        {
            observerState = subject.SubjectState;
            Console.WriteLine("观察者{0}的新状态是{1}", name, observerState);
        }

        public ConcreteSubject Subject
        {
            get { return subject; }
            set { subject = value; }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            ConcreteSubject s = new ConcreteSubject();

            s.Attach(new ConCreteObserver(s, "X"));
            s.Attach(new ConCreteObserver(s, "Y"));
            s.Attach(new ConCreteObserver(s, "Z"));

            s.SubjectState = "ABC";
            s.Notify();
            Console.Read();
        }
    }
}
