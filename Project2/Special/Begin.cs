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
        

        public override Node eval(Node exp, Environment env)
        {
            Node retNode = exp.getCdr();
            Node evalNode = null;
            if(!retNode.isNull())
            {
                evalNode = retNode.getCdr();
                retNode = retNode.getCar().eval(env);
                while (!evalNode.isNull())
                {
                    retNode = evalNode.getCar().eval(env);
                    evalNode = evalNode.getCdr();
                }
                return retNode;
            }
            return Nil.getInstance();
            
        }
    }
}

