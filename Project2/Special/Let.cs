// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
	    public Let() { }

        public override void print(Node t, int n, bool p)
        {   
            Printer.printLet(t, n, p);
        }

        //spring
        //from scheme function
        public Environment letEnv(Node exp, Environment env)
        {
            env.define(exp.getCar().getCar().getCdr(), eval(exp.getCar().getCdr().getCar().getCdr(), env)); //not sure if should be define or assign?
            return env;
        }

        public override Node eval(Node exp, Environment env)
        {
            return eval(exp.getCdr().getCdr(), letEnv(exp, env));
        }

    }
}


