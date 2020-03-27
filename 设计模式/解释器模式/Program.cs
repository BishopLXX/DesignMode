using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 解释器模式
{
    #region 原型
    //abstract class AbsstractExpression
    //{
    //    public abstract void Interpret(Context context);
    //}

    //class TerminalExpression: AbsstractExpression
    //{
    //    public override void Interpret(Context context)
    //    {
    //        Console.WriteLine("终端解释器");
    //    }
    //}

    //class NonteTerminalExpression : AbsstractExpression
    //{
    //    public override void Interpret(Context context)
    //    {
    //        Console.WriteLine("非终端解释器");
    //    }
    //}

    //class Context
    //{
    //    private string input;
    //    public string Input
    //    {
    //        get { return input; }
    //        set { input = value; }
    //    }

    //    private string output;
    //    public string Output
    //    {
    //        get { return output; }
    //        set { output = value; }
    //    }
    //}
    #endregion

    class PlayContext
    {
        // 演奏文本
        private string text;
        public string PlayText
        {
            get { return text; }
            set { text = value; }
        }
    }

    abstract class Expression
    {
        // 解释器
        public void Interpret(PlayContext context)
        {
            if (context.PlayText.Length == 0)
            {
                return;
            }
            else
            {
                string playKey = context.PlayText.Substring(0, 1);
                context.PlayText = context.PlayText.Substring(2);
                double playValue = Convert.ToDouble(context.PlayText.Substring(0, context.PlayText.IndexOf(" ")));
                context.PlayText = context.PlayText.Substring(context.PlayText.IndexOf(" ") + 1);
                Excute(playKey, playValue);
            }
        }

        public abstract void Excute(string key, double value);
    }

    class Note: Expression
    {
        public override void Excute(string key, double value)
        {
            string note = "";
            switch (key)
            {
                case "C":
                    note = "1";
                    break;
                case "D":
                    note = "2";
                    break;
                case "E":
                    note = "3";
                    break;
                case "F":
                    note = "4";
                    break;
                case "G":
                    note = "5";
                    break;
                case "A":
                    note = "6";
                    break;
                case "B":
                    note = "7";
                    break;
            }
            Console.Write("{0} ", note);
        }
    }

    class Scale : Expression
    {
        public override void Excute(string key, double value)
        {
            string scale = "";
            switch (Convert.ToInt32(value))
            {
                case 1:
                    scale = "低音";
                    break;
                case 2:
                    scale = "中音";
                    break;
                case 3:
                    scale = "高音";
                    break;
            }
            Console.Write("{0} ", scale);
        }
    }

    class Program
    {
        //static void Main(string[] args)
        //{
        //    Context context = new Context();
        //    IList<AbsstractExpression> list = new List<AbsstractExpression>();
        //    list.Add(new TerminalExpression());
        //    list.Add(new NonteTerminalExpression());
        //    list.Add(new TerminalExpression());
        //    list.Add(new TerminalExpression());

        //    foreach (AbsstractExpression expression in list)
        //    {
        //        expression.Interpret(context);
        //    }

        //    Console.Read();
        //}

        static void Main(string[] args)
        {
            PlayContext context = new PlayContext();
            Console.WriteLine("上海滩： ");
            context.PlayText = "O 2 E 0.5 G 0.5 A 3 E 0.5 G 0.5 D 3 E 0.5 G 0.5 A 0.5 O 3 C 1 O 3 C 1 O 2 A 0.5 G 1 C 0.5 E 0.5 D 3 ";
            Expression expression = null;
            try
            {
                while (context.PlayText.Length > 0)
                {
                    string str = context.PlayText.Substring(0, 1);
                    switch (str)
                    {
                        case "O":
                            expression = new Scale();
                            break;
                        case "C":
                        case "D":
                        case "E":
                        case "F":
                        case "G":
                        case "A":
                        case "B":
                        case "P":
                            expression = new Note();
                            break;
                    }
                    expression.Interpret(context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Read();
        }
    }
}
