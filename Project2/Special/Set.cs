// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
	public Set() { }
	
        public override void print(Node t, int n, bool p)
        {
            Printer.printSet(t, n, p);
        }

        //fixed by Brittani
        public override Node eval(Node exp, Environment env)
        {
            Node binding = env.lookup(exp.getCdr().getCar());

            if(binding != null)
            {
                Node id = exp.getCdr().getCar();
                Node val = exp.getCdr().getCdr().getCar();

                env.define(id, val);

                return new StringLit(";no values returned", true);
            }
            return null;
        }
    }
}

