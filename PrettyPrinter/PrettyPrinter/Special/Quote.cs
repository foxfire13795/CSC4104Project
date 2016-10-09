// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
    public class Quote : Special  //ERR: quote(...) not showing as '(...)
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.

        public Quote() {}

        public override void print(Node t, int n, bool p)
        {
            if (n > 0)
            {
                for (int i = 0; i < n; i++)
                Console.Write(" ");
            }

            if (p == false)
            {
                Console.Write("'(");
            }
            else
            {
                if (!t.getCar().isNull())
                {
                    t.getCar().print(n, true);
                }
            }
               
        }




        //public override void print(Node t, int n, bool p)
        //{
        // TODO: Implement this function.

        //edit
        //if(n > 0)
        //{
        //   for (int i = 0; i < n; i++)
        //    {
        //      Console.Write(" ");
        //    }
        //}
        //if (p == false)
        //{
        //    Console.Write("'(");
        //}
        //else
        //{
        //   if(t.getCar() != null)
        //    {
        //        t.getCar().print(n, true);
        //    }
        //}
        //end edit
        //}
    }
}

