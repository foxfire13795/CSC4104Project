// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        // TODO: Add any fields needed.
    
        // TODO: Add an appropriate constructor.
        public Regular() { }


        public override void print(Node t, int n, bool p)
        {
        //TODO: Implement this function.

            //edit
            if (t != null && p)
            {
                if(t.getCar() != null)
                {
                    Console.Write(" ");
                }
                t.print(n, p);
            }
            else if (!p)
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write(" ");
                }

                Console.Write("(");
                
                if (t.isPair() && t.getCdr() != null)
                {
                    t.getCar().print(n, true);
                    if (!t.getCdr().isNull())
                        Console.Write(" ");
                    t.getCdr().print(0 - n, true);
                }
                else if (!t.isPair())
                    t.print(0 - n, true);
            }
            //end edit
            }
    }
}


