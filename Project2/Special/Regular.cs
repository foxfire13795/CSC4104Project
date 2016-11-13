// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        public Regular() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printRegular(t, n, p);
        }

        //spring
        public override Node eval(Node exp, Environment env)
        {
            Node car = exp.getCar();
            Node toApply = evallist(exp.getCdr(), env);

            while (car.isSymbol())
            {
                car = env.lookup(car);
            }
            if (car.isNull() || car == null)
            {
                return null;
            }
            else if (car.isProcedure())
            {
                return car.apply(toApply);
            }
            else if (toApply.getCar().isNull()) //added .getCar()
            {
                return car.eval(env); 
            }
            else
                return car.eval(env).apply(toApply);
        }

        public Node evallist(Node exp, Environment env)
        {
            if (exp.isNull() || exp == null)
            {
                return new Cons(Nil.getInstance(), Nil.getInstance());
            }
            else
            {
                Node car = exp.getCar();
                Node cdr = exp.getCdr();
                if(car.isSymbol())
                {
                    car = env.lookup(car);
                }
                if(car.isNull() || car == null)
                {
                    return null;
                }
                return new Cons(car.eval(env), evallist(cdr, env));
            }
        }
    }
}


