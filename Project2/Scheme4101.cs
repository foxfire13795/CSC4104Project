// SPP -- The main program of the Scheme pretty printer.

using System;
using Parse;
using Tokens;
using Tree;

public class Scheme4101
{
    public static int Main(string[] args)
    {
        // Create scanner that reads from standard input
        Scanner scanner = new Scanner(Console.In);
        
        if (args.Length > 1 ||
            (args.Length == 1 && ! args[0].Equals("-d")))
        {
            Console.Error.WriteLine("Usage: mono SPP [-d]");
            return 1;
        }
        
        // If command line option -d is provided, debug the scanner.
        if (args.Length == 1 && args[0].Equals("-d"))
        {
            // Console.Write("Scheme 4101> ");
            Token tok = scanner.getNextToken();
            while (tok != null)
            {
                TokenType tt = tok.getType();

                Console.Write(tt);
                if (tt == TokenType.INT)
                    Console.WriteLine(", intVal = " + tok.getIntVal());
                else if (tt == TokenType.STRING)
                    Console.WriteLine(", stringVal = " + tok.getStringVal());
                else if (tt == TokenType.IDENT)
                    Console.WriteLine(", name = " + tok.getName());
                else
                    Console.WriteLine();

                // Console.Write("Scheme 4101> ");
                tok = scanner.getNextToken();
            }
            return 0;
        }

        // Create parser
        TreeBuilder builder = new TreeBuilder();
        Parser parser = new Parser(scanner, builder);
        Node root;

        // TODO: Create and populate the built-in environment and
        // create the top-level environment
        Tree.Environment builtInEnv = new Tree.Environment();
        Ident id;
        string[] builtInIds = { "b+", "b-", "b*", "b/", "b=", "b<", "b>", "car", "cdr", "cons", "set-car!", "set-cdr!", "symbol?", "number?", "null?", "pair?", "eq?", "procedure?", "display", "newline", "exit", "quit", "read", "write", "eval", "apply", "interaction-environment"};
        foreach(string s in builtInIds)
        {
            id = new Ident(s);
            builtInEnv.define(id, new BuiltIn(id));
        }

        Tree.Environment topLevelEnv = new Tree.Environment(builtInEnv);
        BuiltIn useTopLevelEnv = new BuiltIn(topLevelEnv);

        // Read-eval-print loop

        // TODO: print prompt and evaluate the expression
        root = (Node) parser.parseExp();
        while (root != null) 
        {
            if(topLevelEnv != null)
            {
                try
                {
                    root.eval(topLevelEnv).print(0);
                } catch (NullReferenceException e)
                {
                    Console.WriteLine("NullReferenceException: Undefined Variable");
                } finally
                {
                    root = (Node)parser.parseExp();
                }
            }
        }

        return 0;
    }
}
