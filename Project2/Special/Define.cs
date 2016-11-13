// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
	public Define() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printDefine(t, n, p);
        }

        //spring 
        //from scheme function...mostly
        public override Node eval(Node exp, Environment env)
        {
            Node name = exp.getCdr().getCar(); //we'll see
            if(name.isSymbol())
            {
                env.define(name, exp.getCdr().getCdr().getCar());
            }
            else
            {
                Node parm = exp.getCdr().getCar().getCdr();
                Node body = exp.getCdr().getCdr();
                //...this
                Closure function = new Closure(new Cons(parm, body), env);
                env.define(name, function); //other uses name.getCar()
            }
            return new StringLit(";no values returned");
        }
    }
}


