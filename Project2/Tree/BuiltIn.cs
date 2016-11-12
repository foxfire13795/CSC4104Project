// BuiltIn -- the data structure for built-in functions

// Class BuiltIn is used for representing the value of built-in functions
// such as +.  Populate the initial environment with
// (name, new BuiltIn(name)) pairs.

// The object-oriented style for implementing built-in functions would be
// to include the C# methods for implementing a Scheme built-in in the
// BuiltIn object.  This could be done by writing one subclass of class
// BuiltIn for each built-in function and implementing the method apply
// appropriately.  This requires a large number of classes, though.
// Another alternative is to program BuiltIn.apply() in a functional
// style by writing a large if-then-else chain that tests the name of
// the function symbol.

using System;
using Parse;

namespace Tree
{
    public class BuiltIn : Node
    {
        private Node symbol;            // the Ident for the built-in function
        protected static Environment interaction_environment = new Environment();

        public BuiltIn(Node s)		{ symbol = s; }
        public BuiltIn(Environment env) { interaction_environment = env; }

        public Node getSymbol()		{ return symbol; }

        // TODO: The method isProcedure() should be defined in
        // class Node to return false.
        public override bool isProcedure()	{ return true; }

        public override void print(int n)
        {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            if (symbol != null)
                symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }

        // TODO: The method apply() should be defined in class Node
        // to report an error.  It should be overridden only in classes
        // BuiltIn and Closure.
        public override Node apply(Node args)
        {
            // Grabbing the arguments and checking if they're null
            if(args == null)
            {
                return null;
            }

            string symName = symbol.getName();
            Node l1 = args.getCar();

            if (l1 == null || l1.isNull())
            {
                l1 = Nil.getInstance();
            }

            Node l2 = args.getCdr();

            if (l2 == null || l2.isNull())
            {
                l2 = Nil.getInstance();
            } else
            {
                l2 = l2.getCar();
            }

            // Various cases for the built in functions
            if(symName.Equals("b+"))
            {
                if(l1.isNumber() && l2.isNumber())
                {
                    int x = l1.getVal();
                    int y = l2.getVal();
                    return new IntLit(x + y);
                } else
                {
                    Console.Error.WriteLine("Bad argument for b+");
                    return new StringLit("");
                }
            }
            else if(symName.Equals("b-"))
            {
                if (l1.isNumber() && l2.isNumber())
                {
                    int x = l1.getVal();
                    int y = l2.getVal();
                    return new IntLit(x - y);
                }
                else
                {
                    Console.Error.WriteLine("Bad argument for b-");
                    return new StringLit("");
                }
            }
            else if(symName.Equals("b*"))
            {
                if (l1.isNumber() && l2.isNumber())
                {
                    int x = l1.getVal();
                    int y = l2.getVal();
                    return new IntLit(x * y);
                }
                else
                {
                    Console.Error.WriteLine("Bad argument for b*");
                    return new StringLit("");
                }
            }
            else if (symName.Equals("b/"))
            {
                if (l1.isNumber() && l2.isNumber())
                {
                    int x = l1.getVal();
                    int y = l2.getVal();
                    return new IntLit(x / y);
                }
                else
                {
                    Console.Error.WriteLine("Bad argument for b/");
                    return new StringLit("");
                }
            }
            else if (symName.Equals("b="))
            {
                if (l1.isNumber() && l2.isNumber())
                {
                    int x = l1.getVal();
                    int y = l2.getVal();
                    return BoolLit.getInstance(x == y);
                }
                else
                {
                    Console.Error.WriteLine("Bad argument for b=");
                    return new StringLit("");
                }
            }
            else if (symName.Equals("b<"))
            {
                if (l1.isNumber() && l2.isNumber())
                {
                    int x = l1.getVal();
                    int y = l2.getVal();
                    return BoolLit.getInstance(x < y);
                }
                else
                {
                    Console.Error.WriteLine("Bad argument for b<");
                    return new StringLit("");
                }
            }
            else if (symName.Equals("b>"))
            {
                if (l1.isNumber() && l2.isNumber())
                {
                    int x = l1.getVal();
                    int y = l2.getVal();
                    return BoolLit.getInstance(x > y);
                }
                else
                {
                    Console.Error.WriteLine("Bad argument for b>");
                    return new StringLit("");
                }
            }
            else if (symName.Equals("car"))
            {
                if(l1.isNull())
                {
                    return l1;
                }
                else
                {
                    return l1.getCar();
                }
            }
            else if (symName.Equals("cdr"))
            {
                if (l1.isNull())
                {
                    return l1;
                }
                else
                {
                    return l1.getCdr();
                }
            }
            else if (symName.Equals("cons"))
            {
                return new Cons(l1, l2);
            }
            else if (symName.Equals("set-car!"))
            {
                l1.setCar(l2);
                return l1;
            }
            else if (symName.Equals("set-cdr!"))
            {
                l1.setCdr(l2);
                return l1;
            }
            else if (symName.Equals("symbol?"))
            {
                return BoolLit.getInstance(l1.isSymbol());
            }
            else if (symName.Equals("number?"))
            {
                return BoolLit.getInstance(l1.isNumber());
            }
            else if (symName.Equals("null?"))
            {
                return BoolLit.getInstance(l1.isNull());
            }
            else if (symName.Equals("pair?"))
            {
                return BoolLit.getInstance(l1.isPair());
            }
            else if (symName.Equals("eq?"))
            {
                if (l1.isNumber() && l2.isNumber())
                {
                    int x = l1.getVal();
                    int y = l2.getVal();
                    return BoolLit.getInstance(x == y);
                }
                else if (l1.isBool() && l2.isBool())
                {
                    bool x = l1.getBool();
                    bool y = l2.getBool();
                    return BoolLit.getInstance(x == y);
                }
                else if (l1.isString() && l2.isString())
                {
                    string x = l1.getStr();
                    string y = l2.getStr();
                    return BoolLit.getInstance(x.Equals(y));
                }
                else if (l1.isPair() && l2.isPair())
                {
                    Node cars = new Cons(l1.getCar(), new Cons(l2.getCar(), Nil.getInstance()));
                    Node cdrs = new Cons(l1.getCdr(), new Cons(l2.getCdr(), Nil.getInstance()));
                    return BoolLit.getInstance(apply(cars).getBool() && apply(cdrs).getBool());
                }
                else if (l1.isSymbol() && l2.isSymbol())
                {
                    string x = l1.getName();
                    string y = l2.getName();
                    return BoolLit.getInstance(x.Equals(y));
                }
                else if (l1.isNull() && l2.isNull())
                {
                    return BoolLit.getInstance(true);
                }
                else
                {
                    return BoolLit.getInstance(false);
                }
            }
            else if (symName.Equals("procedure?"))
            {
                return BoolLit.getInstance(l1.isProcedure());
            }
            else if (symName.Equals("display"))
            {
                return l1;
            }
            else if (symName.Equals("newline"))
            {
                return new StringLit("", true);
            }
            else if (symName.Equals("exit"))
            {
                System.Environment.Exit(0);
            }
            else if (symName.Equals("write"))
            {
                l1.print(0);
                return new StringLit("");
            }
            else if (symName.Equals("eval"))
            {
                return l1;
            }
            else if (symName.Equals("apply"))
            {
                return l1.apply(l2);
            }
            else if (symName.Equals("read"))
            {
                Scanner scanner = new Scanner(Console.In);
                TreeBuilder builder = new TreeBuilder();

                Parser parser = new Parser(scanner, builder);

                Node root = (Node) parser.parseExp();
                return root;
            }
            else if (symName.Equals("interaction-environment"))
            {
                interaction_environment.print(0);
            } else
            {
                l1.print(0);
                return Nil.getInstance();
            }

            return new StringLit(">");

        }

        public Node eval(Node n, Environment env)
        {
            return Nil.getInstance();
        }
    }    
}

