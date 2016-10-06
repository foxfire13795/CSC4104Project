// Parser -- the parser for the Scheme printer and interpreter
//
// Defines
//
//   class Parser;
//
// Parses the language
//
//   exp  ->  ( rest
//         |  #f
//         |  #t
//         |  ' exp
//         |  integer_constant
//         |  string_constant
//         |  identifier
//    rest -> )
//         |  exp+ [. exp] )
//
// and builds a parse tree.  Lists of the form (rest) are further
// `parsed' into regular lists and special forms in the constructor
// for the parse tree node class Cons.  See Cons.parseList() for
// more information.
//
// The parser is implemented as an LL(0) recursive descent parser.
// I.e., parseExp() expects that the first token of an exp has not
// been read yet.  If parseRest() reads the first token of an exp
// before calling parseExp(), that token must be put back so that
// it can be reread by parseExp() or an alternative version of
// parseExp() must be called.
//
// If EOF is reached (i.e., if the scanner returns a NULL) token,
// the parser returns a NULL tree.  In case of a parse error, the
// parser discards the offending token (which probably was a DOT
// or an RPAREN) and attempts to continue parsing with the next token.

using System;
using Tokens;
using Tree;

namespace Parse
{
    public class Parser
    {
	
        private Scanner scanner;

        public Parser(Scanner s) { scanner = s; }

        public Node parseExp()
        {
            Token tok = scanner.getNextToken();
            return parseExp(tok);
        }

        public Node parseExp(Token tok)
        {
            // TODO: write code for parsing an exp

            //edit
            if(tok == null)
            {
                return new Node();
            }
            else if(tok.getType().ToString().Equals("LPAREN"))
            {
                return parseRest();
            }
            else if (tok.getType().ToString().Equals("FALSE"))
            {
                return new BoolLit(false);
            }
            else if (tok.getType().ToString().Equals("TRUE"))
            {
                return new BoolLit(true);
            }
            else if (tok.getType().ToString().Equals("QUOTE"))
            {
                return new Cons(new Ident("quote"), new Cons(parseExp(), new Nil()));
            }
            else if (tok.getType().ToString().Equals("INT"))
            {
                return new IntLit(tok.getIntVal());
            }
            else if (tok.getType().ToString().Equals("STRING"))
            {
                return new StringLit(tok.getStringVal());
            }
            else if (tok.getType().ToString().Equals("IDENT"))
            {
                return new Ident(tok.getName());
            }
            else
            {
                Console.WriteLine("Syntax error");
            }
            //end edit

            return null;
        }

        protected Node parseRest()
        {
            Token tok = scanner.getNextToken();
            return parseRest(tok);
        }


        protected Node parseRest(Token tok)
        {
            // TODO: write code for parsing a rest

            //edit
            if(tok.getType().ToString().Equals("RPAREN"))
            {
                return new Nil();
            } else if (!tok.getType().ToString().Equals("DOT"))
            {
                Node a = parseExp(tok);
                Node d;
                Token lookahead = scanner.getNextToken();
                if (lookahead.getType().ToString().Equals("DOT"))
                {
                    d = new Cons(new Ident("Dot"), new Cons(parseExp(), new Nil()));
                } else
                {
                    d = parseRest(lookahead);
                }

                return new Cons(a, d);
            }
            else
            {
                // error handling
                Console.WriteLine("Incorrect syntax");
            }
            //end edit

            return null;
        }

        // TODO: Add any additional methods you might need.
    }
}

