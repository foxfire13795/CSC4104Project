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
        //public Environment letEnv(Node exp, Environment env)
        //{
            //env.define(exp.getCdr().getCar().getCar(), eval(exp.getCdr().getCar().getCdr().getCar(), env)); //not sure if should be define or assign?
            //return env;
        //}

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
            //return eval(exp.getCdr().getCdr(), letEnv(exp, env));
        }

    }
}


