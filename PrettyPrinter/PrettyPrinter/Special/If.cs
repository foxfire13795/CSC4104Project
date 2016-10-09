// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
        // TODO: Add any fields needed.
 
        // TODO: Add an appropriate constructor.
	public If() { }

        //edit
        public override void print(Node t, int n, bool p)
        {
            if(!p)
            {
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        Console.Write(" ");
                }

                Console.Write("(if ");
            }
            if (!t.isNull() && t.getCar() != null)
            {
                if(!t.getCar().isNull())
                {
                    if(t.getCar().isPair())
                    {
                        t.getCar().print(0 - n, false);
                        Console.WriteLine();
                    }
                    else
                    {
                        t.getCar().print(n, p);
                        Console.WriteLine();
                    }
                    
                    printHelper(t.getCdr(), n);

                } else
                {
                    t.getCar().print(n, p);
                    Console.WriteLine();
                }
            }
        }

        public void printHelper(Node t, int n)
        {
            if(t.getCdr().isNull())
            {
                t.indent(n + 4);
                t.getCar().print(0 - n, false);
                Console.WriteLine();
                t.indent(n);
                t.getCdr().print(n + 4, true);
                Console.WriteLine();
            }
            else
            {
                t.getCar().print(n + 4, false);
                Console.WriteLine();
                printHelper(t.getCdr(), n);
            }
        }
    }
}

