// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
	public Define() { }

        public override void print(Node t, int n, bool p)
        {
            if(!p)
            {
                Console.Write("(define ");
            }
            if(!t.isNull() && t.getCar() != null)
            {
                if (!t.getCar().isNull())
                {
                    if (t.getCar().isPair())
                    {
                        t.getCar().print(n, false);
                    }
                    else
                    {
                        t.getCar().print(n, p);
                    }
                    if (p && !t.getCdr().isNull())
                    {
                        Console.Write(" ");
                    }
                    if (t.getCdr().isPair())
                    {
                        Console.WriteLine();
                    }
                    t.getCdr().print(n, true); //should either use recursion or return a Nil node
                }
                else
                {
                    t.getCar().print(n, p);
                }
            }
            
        }

        ////WOAH WOAH WOAH
        //private bool isFunc;

        //private void ifFunc(Node t)
        //{
        //    if (t.getCar().isPair())
        //        isFunc = true;
        //}

        //public override void print(Node t, int n, bool p)
        //{
        //    if (p)
        //    {
        //        this.ifFunc(t);
        //        if (isFunc)
        //        {
        //            if (t.getCdr() != null)
        //            {
        //                t.getCar().print(-n, false);
        //                Console.WriteLine();
        //                printDefine(t.getCdr(), n);
        //            }
        //            else
        //                Console.Write("NullPointer");
        //        }
        //        else
        //        { t.print(n, p); }
        //    }
        //    else
        //    {
        //        if (n > 0)
        //        {
        //            for (int i = 0; i < n; i++)
        //                Console.Write(" ");
        //        }
        //        Console.Write("(define ");
        //    }
        //}

        //private void printDefine(Node t, int n)
        //{
        //    if (t.getCdr().isNull())
        //    {
        //        t.getCar().print(n + 4, false);
        //        Console.WriteLine();
        //        if (n > 0)
        //        {
        //            for (int i = 0; i < n; i++)
        //                Console.Write(" ");
        //        }
        //        t.getCdr().print(n + 4, true);
        //        return;
        //    }
        //    t.getCar().print(n + 4, false);
        //    Console.WriteLine();
        //    printDefine(t.getCdr(), n);
        //}
        //end WOAH

        //public override void print(Node t, int n, bool p)
        //{
        // TODO: Implement this function.

        //edit
        //Console.Write("(define");
        //end edit

        //}
    }
}


