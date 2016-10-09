// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
        // TODO: Add any fields needed.
 
        // TODO: Add an appropriate constructor.
	public Set() { }

        //public override void print(Node t, int n, bool p)
        //{
        // TODO: Implement this function.

        //edit
        //Console.Write("SetStart");
        //end edit
        //}

        public override void print(Node t, int n, bool p)
        {
            if (!p)
            {
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        Console.Write(" ");
                }

                Console.Write("(let ");
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

        //WOAH WOAH WOAH
        /*public override void print(Node t, int n, bool p)
        {
            if (p)
            {
                t.getCar().print(n, true);
                if (t.getCdr().isPair() && (t.getCdr().getCdr() != null))
                {
                    Console.Write(" ");
                    t.getCdr().getCar().print(n, true);
                    t.getCdr().getCdr().print(n, true);
                }
                else if (!t.getCdr().isPair())
                {
                    Console.Write(" ");
                    t.getCdr().print(n, true);
                }
                else
                    Console.Write(p);
            }
            else
            {
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        Console.Write(" ");
                }
                Console.Write("(set ");
            }
        }*/
        //WOAH STOP
    }
}

