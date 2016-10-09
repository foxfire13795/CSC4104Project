// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
        // TODO: Add any fields needed.
 
        // TODO: Add an appropriate constructor.

	    public Begin()
        {
            //done, no fields necessary
        }

        public override void print(Node t, int n, bool p)
        {
            if (!p)
            {
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        Console.Write(" ");
                }

                Console.Write("(begin ");
            }
            if (!t.isNull() && t.getCar() != null)
            {
                if (!t.getCar().isNull())
                {
                    if (t.getCar().isPair())
                    {
                        Console.Write(" (");
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
    }
}

