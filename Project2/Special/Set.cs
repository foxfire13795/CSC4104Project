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

        //spring
        //from scheme function
        public override Node eval(Node exp, Environment env)
        {
            Node binding = env.lookup(exp.getCar().getCdr());
            if(binding.isPair())
            {
                binding.setCdr(eval(exp.getCar().getCdr().getCdr(), env));
            }
            return binding;
        }
    }
}

