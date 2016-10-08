// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
        // TODO: Add any fields needed.

        // TODO: Add an appropriate constructor.
	public Cond() { }
        

        //edit
        public override void print(Node t, int n, bool p)
        {
            if(!p)
            {
                Console.WriteLine("(cond");
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

        }
        //end edit
    }
}


