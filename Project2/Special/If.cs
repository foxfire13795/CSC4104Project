// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
	public If() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printIf(t, n, p);
        }

        //spring
        //using the scheme function
        public override Node eval(Node exp, Environment env)
        {
            if(exp.getCdr().getCar().eval(env).getBool())
            {
                return exp.getCdr().getCdr().getCar().eval(env); //for true
            }
            else if(!exp.getCdr().getCdr().getCdr().isNull())
            {
                return exp.getCdr().getCdr().getCdr().getCar().eval(env); //for false
            }
            else
            {
                return Nil.getInstance();
            }
        }
    }
}

