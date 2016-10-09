// Cons -- Parse tree node class for representing a Cons node

using System;

namespace Tree
{
    public class Cons : Node
    {
        private Node car;
        private Node cdr;
        private Special form;
    
        public Cons(Node a, Node d)
        {
            car = a;
            cdr = d;
            parseList();
        }

        public override bool isPair() { return true; }

        public override void setCar(Node a)
        {
            car = a;
            parseList();
        }

        public override void setCdr(Node d)
        {
            cdr = d;
            parseList();
        }

        public override Node getCar()
        {
            return car;
        }

        public override Node getCdr()
        {
            return cdr;
        }
        

        // parseList() `parses' special forms, constructs an appropriate
        // object of a subclass of Special, and stores a pointer to that
        // object in variable form.  It would be possible to fully parse
        // special forms at this point.  Since this causes complications
        // when using (incorrect) programs as data, it is easiest to let
        // parseList only look at the car for selecting the appropriate
        // object from the Special hierarchy and to leave the rest of
        // parsing up to the interpreter.
        void parseList()
        {
            // TODO: implement this function and any helper functions
            // you might need.

            if(car.getName() == "begin")
            {
                form = new Begin();
            }
            else if (car.getName() == "cond")
            {
                form = new Cond();
            }
            else if (car.getName() == "define")
            {
                form = new Define();
            }
            else if (car.getName() == "if")
            {
                form = new If();
            }
            else if (car.getName() == "lambda")
            {
                form = new Lambda();
            }
            else if (car.getName() == "let")
            {
                form = new Let();
            }
            else if (car.getName() == "quote")
            {
                form = new Quote();
            }
            else if (car.getName() == "set")
            {
                form = new Set();
            }
            else
            {
                form = new Regular();
            }

        }

        public override void print(int n)
        {
            form.print(this.car, n, false);
            form.print(this.cdr, n, true);
        }

        public override void print(int n, bool p)
        {
            if (this.car.isPair())
            {
                car.print(n, false);
                //form.print(this.car, n, false);
            }
            else
            {
                form.print(this.car, n, p);
            }
            form.print(this.cdr, n, true);
        }
    }
}

