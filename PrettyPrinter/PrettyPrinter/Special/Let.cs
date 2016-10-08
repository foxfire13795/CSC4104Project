// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
        // TODO: Add any fields needed.
 
        // TODO: Add an appropriate constructor.
	public Let() { }

        //WOAH WOAH WOAH

        public override void print(Node t, int n, bool p)
        {
            if (p)
            {
                printLet(t, n);
            }
            else
            {
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        Console.Write(" ");
                }
                Console.Write("(let ");
            }
        }

        private void printLet(Node t, int n)
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
            printLet(t.getCdr(), n);
        }
        //end woah


        //public override void print(Node t, int n, bool p)
        //{
        // TODO: Implement this function.

        //edit
        //Console.Write("(let");

        //end edit
        //}
    }
}


