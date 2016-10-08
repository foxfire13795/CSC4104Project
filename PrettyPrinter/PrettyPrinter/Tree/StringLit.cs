// StringLit -- Parse tree node class for representing string literals

using System;

namespace Tree
{
    public class StringLit : Node
    {
        private string stringVal;

        public StringLit(string s)
        {
            stringVal = s;
        }
        //edit
        public override bool isString() { return true; }
        //end edit

        public override void print(int n)
        {
            // There got to be a more efficient way to print n spaces.
            for (int i = 0; i < n; i++)
            {
                Console.Write(" ");
            }

            Console.Write("\"" + stringVal + "\"");
        }
    }
}

