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
                }
            }
            else
            {
                Node val = test.eval(env); 
                if(val.getBool())
                {
                    if(body.isNull())
                    {
                        return val;
                    }
                    else
                    {
                        return body.eval(env);
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


