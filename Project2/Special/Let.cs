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
        public Environment letEnv(Node exp, Environment env)
        {
            Node defName;
            Node defValue;
            if(exp.getCar().isPair())
            {
                defName = exp.getCar().getCar();
                defValue = exp.getCar().getCdr().getCar();
                env.define(defName, defValue);
            }
            if(!exp.getCdr().isNull() && exp.getCdr().isPair())
            {
                if(!exp.getCdr().getCdr().isNull())
                {
                    env = letEnv(exp.getCdr(), env);
                }
                else
                {
                    defName = exp.getCdr().getCar().getCar();
                    defValue = exp.getCdr().getCar().getCdr().getCar();
                    env.define(defName, defValue);
                }
            }
            return env;
        }


        public override Node eval(Node exp, Environment env)
        {
            Node def = exp.getCdr().getCar();
            if(def.isPair())
            {
                env = letEnv(def, env);
            }
            return exp.getCdr().getCdr().eval(env);
        }

    }
}


