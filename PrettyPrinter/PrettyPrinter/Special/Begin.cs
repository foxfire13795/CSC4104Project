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

        //public override void print(Node t, int n, bool p)
        //{
        // TODO: Implement this function.

        //edit
        //  if (n > 0)
        //  {
        //      for (int i = 0; i < n; i++)
        //      {
        //          Console.Write(" ");
        //      }
        //  }
        //  Console.WriteLine("(begin");
        //end edit
        //}

        //WOAH WOAH WHOAH
        public override void print(Node t, int n, bool p)
        {
            if (p)
            {
                printBegin(t, n);
            }
            else
            {
                if (n > 0)
                {
                    for (int i = 0; i < n; i++)
                        Console.Write(" ");
                }
                Console.WriteLine("(begin ");
            }
        }

        private void printBegin(Node t, int n)
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
            printBegin(t.getCdr(), n);
        }
        //end woah

        //edit: add printing
    }
}

