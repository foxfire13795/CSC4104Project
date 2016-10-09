// Lambda -- Parse tree node strategy for printing the special form lambda

using System;

namespace Tree
{
    public class Lambda : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
	public Lambda() { }

        public override void print(Node t, int n, bool p)
        {
            if (!p)
            {
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        Console.Write(" ");
                }

                Console.Write("(lambda ");
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
                t.getCar().print(-n, false);
                Console.WriteLine();
                printLambda(t.getCdr(), n);
            }
            else
            {
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        Console.Write(" ");
                }
                Console.Write("(lambda ");
            }
        }

        private void printLambda(Node t, int n)
        {
            if (t.getCdr().isNull())
            {
                t.getCar().print(n + 4, false);
                Console.WriteLine();
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        Console.Write(" ");
                }
                t.getCdr().print(n + 4, true);
                return;
            }
            t.getCar().print(n + 4, false);
            Console.WriteLine();
            printLambda(t.getCdr(), n);
        }*/
        //end woah

        //public override void print(Node t, int n, bool p)
        //{
        // TODO: Implement this function.

        //edit
        // Console.Write("(lambda");
        //end edit
        //}
    }
}

