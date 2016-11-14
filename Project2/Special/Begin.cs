// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
	public Begin() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printBegin(t, n, p);
        }

        //spring
        public override Node eval(Node exp, Environment env)
        {
            return exp.getCdr().eval(env); //this returns null because it it only used to start the program
                         //if all works out properly, the program will skip it and move on to evaluate the next exp
        }
    }
}

