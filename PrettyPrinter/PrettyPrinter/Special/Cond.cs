// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
	public Cond() { }

        public override void print(Node t, int n, bool p)
        {
            if (!p)
            {
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        Console.Write(" ");
                }

                Console.Write("(cond ");
            }
            if (!t.isNull() && t.getCar() != null)
            {
                if (!t.getCar().isNull())
                {
                    if (t.getCar().isPair())
                    {
                        t.getCar().print(n, false);
                        Console.WriteLine();
                    }
                    else
                    {
                        t.getCar().print(n, p);
                        Console.WriteLine();
                    }

                    t.getCdr().print(n, true);
                    Console.WriteLine();

                }
                else
                {
                    t.getCar().print(n, p);
                    Console.WriteLine();
                }
            }
        }

        //edit
        /*public override void print(Node t, int n, bool p)
        {
            if(!p)
            {
                Console.WriteLine("(cond ");
            }
            else
            {
                for(int i = 0; i < n; i++)
                {
                    Console.Write(" ");
                }
            }
            if(t != null && !t.isNull())
            {
                print(t.getCar(), n, true);
                print(t.getCdr(), n, true);
            }
            else if(t != null)
            {
                t.print(n, true);
            }
        }

        public void printCond(Node t, int n)
        {

        }*/
        //end edit
    }
}


