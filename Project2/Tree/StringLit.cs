// StringLit -- Parse tree node class for representing string literals

using System;

namespace Tree
{
    public class StringLit : Node
    {
        private string stringVal;
        private bool newline;

        public StringLit(string s)
        {
            stringVal = s;
            newline = false;
        }

        public StringLit(string s, bool b)
        {
            stringVal = s;
            newline = true;
        }

        public override void print(int n)
        {
            if (newline)
            {
                Console.WriteLine(stringVal);
            }
            else
            {
                Printer.printStringLit(n, stringVal);
            }
        }

        public override string getStr()
        {
            return stringVal;
        }

        public override bool isString()
        {
            return true;
        }

        //spring
        public override Node eval(Node exp, Environment env)
        {
            return this; //returns String node with stringVal
        }
    }
}

