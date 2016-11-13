// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
	public Cond() { }

        public override void print(Node t, int n, bool p)
        { 
            Printer.printCond(t, n, p);
        }

        //spring
        //using the scheme function ... kind of
        //public override Node eval(Node exp, Environment env)
        //{
        //    if(exp.isNull()) //should be right form?
        //    {
        //        return Nil.getInstance();
        //    }
        //    String nodeName = exp.getCar().getName();
        //    if (string.Equals(nodeName, "cond", StringComparison.OrdinalIgnoreCase))
        //    {
        //        exp.getCdr().eval(env); //evalcond
        //    }
        //    Node test = exp.getCar().getCar();
        //    Node body = exp.getCdr().getCar();
        //    if (test.isSymbol())
        //    {
        //        String testName = test.getName();
        //        if (string.Equals(testName, "else", StringComparison.OrdinalIgnoreCase)) //lol hope this works
        //        {
        //            if (body.isNull()) //idk if right or not
        //            {
        //                return Nil.getInstance();
        //            }
        //            else
        //            {
        //                return eval(body, env); //evalbody
        //            }
        //        }
        //    }
        //    Node val = eval(test, env);
        //    if (!val.isNull())
        //    {
        //        if (body.isNull())
        //        {
        //            return Nil.getInstance();
        //        }
        //        else
        //        {
        //            return eval(body, env); //evalbody
        //        }
        //    }
        //    else
        //    {
        //        return exp.getCdr().eval(env); //tries again if exp is also a cond, perhaps? --evalcond call
        //    }
        //}

        //evalbody method --> seems that it should just continue iterating through the exp? -- unnnecessary in OO context?
        //in the scheme version, it reads a list (the cdr) and evals each thing in it
        //public Node evalbody(Node exp, Environment env)
        //{
        //    return eval(exp, env); 
        //}

        //new section by spring
        public override Node eval(Node exp, Environment env)
        {
            if(exp.isNull())
            {
                return Nil.getInstance();
            }
            String nodeName = exp.getCar().getName();
            if (string.Equals(nodeName, "cond", StringComparison.OrdinalIgnoreCase))
            {
                return evalcond(exp.getCdr(), env);
            }
            else
            {
                return evalcond(exp, env);
            }
        }

        public Node evalcond(Node exp, Environment env)
        {
            Node test = exp.getCar().getCar();
            Node body = exp.getCar().getCdr();
            String testName = test.getName();
            if (string.Equals(testName, "else", StringComparison.OrdinalIgnoreCase))
            {
                if(body.isNull())
                {
                    return Nil.getInstance();
                }
                else
                {
                    return body.eval(env);
                    //return eval(body, env); //evalbody
                }
            }
            else
            {
                Node val = test.eval(env); //see if this works
                if(val.getBool())
                {
                    if(body.isNull())
                    {
                        return val;
                    }
                    else
                    {
                        return body.eval(env);
                        //return eval(body, env); //evalbody
                    }
                }
                else
                {
                    return evalcond(exp.getCdr(), env);
                }
            }
        }
    }
}


