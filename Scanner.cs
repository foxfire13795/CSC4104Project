// Scanner -- The lexical analyzer for the Scheme printer and interpreter

using System;
using System.IO;
using Tokens;

namespace Parse
{
    public class Scanner
    {
        private TextReader In;

        // maximum length of strings and identifier
        private const int BUFSIZE = 1000;
        private char[] buf;

        public Scanner(TextReader i) { In = i; }
  
        // TODO: Add any other methods you need

        public Token getNextToken()
        {
            int ch;
            buf =  new char[BUFSIZE];
            try
            {
                // It would be more efficient if we'd maintain our own
                // input buffer and read characters out of that
                // buffer, but reading individual characters from the
                // input stream is easier.
                ch = In.Read();
   
                // TODO: skip white space and comments

                if (ch == -1)
                    return null;
        
                // Special characters
                else if (ch == '\'')
                    return new Token(TokenType.QUOTE);
                else if (ch == '(')
                    return new Token(TokenType.LPAREN);
                else if (ch == ')')
                    return new Token(TokenType.RPAREN);
                else if (ch == '.')
                    // We ignore the special identifier `...'.
                    return new Token(TokenType.DOT);
                
                // Boolean constants
                else if (ch == '#')
                {
                    ch = In.Read();

                    if (ch == 't')
                        return new Token(TokenType.TRUE);
                    else if (ch == 'f')
                        return new Token(TokenType.FALSE);
                    else if (ch == -1)
                    {
                        Console.Error.WriteLine("Unexpected EOF following #");
                        return null;
                    }
                    else
                    {
                        Console.Error.WriteLine("Illegal character '" +
                                                (char)ch + "' following #");
                        return getNextToken();
                    }
                }

                // String constants
                else if (ch == '"')
                {
                    // TODO: scan a string into the buffer variable buf

                    //edit
                    int i = 0;
                    ch = In.Read();
                    while(ch != '"')
                    {
                        buf[i] = (char) ch;
                        i++;
                        ch = In.Read();
                    }
                    String str = new string(buf, 0, i);

                    return new StringToken(str);
                    //end edit

                }

    
                // Integer constants
                else if (ch >= '0' && ch <= '9')
                {
                    int i = 0;
                    // TODO: scan the number and convert it to an integer
                    char s = Convert.ToChar(ch);

                    //edit
                    String str = System.String.Empty;
                    str += s;
                    while(In.Peek() >= '0' && In.Peek() <= '9')
                    {
                        char t = Convert.ToChar(In.Read());
                        str += t;
                    }
                    i = Convert.ToInt32(str);
                    //end edit

                    // make sure that the character following the integer
                    // is not removed from the input stream
                    return new IntToken(i);
                }
        
                // Identifiers
                else if (ch >= 'A' && ch <= 'z')
                         // or ch is some other valid first character
                         // for an identifier
                {
                    // TODO: scan an identifier into the buffer

                    //edit
                    buf[0] = (char) ch;
                    int i = 1;
                    while (In.Peek() != ' ')
                    {
                        buf[i] = (char)In.Read();
                        i++;
                    }
                    // make sure that the character following the integer
                    // is not removed from the input stream

                    return new IdentToken(new String(buf, 0, i+1));
                    //end edit 

                }

                //edit to skip comments
                else if(ch == ';')
                {
                    while(In.Peek() != '\n')
                    {
                        In.Read();
                    }
                    In.Read();
                    return getNextToken();
                }
                //skip white space, tab, newline, etc
                else if(ch == ' ' || ch == '\f' || ch == '\t' || ch =='\v' || ch =='\r' || ch == '\n')
                {
                    return getNextToken();
                }
                //end edit
    
                // Illegal character
                else
                {
                    Console.Error.WriteLine("Illegal input character '" + (char)ch + '\'');
                    return getNextToken();
                }
            }
            catch (IOException e)
            {
                Console.Error.WriteLine("IOException: " + e.Message);
                return null;
            }
        } //end getNextToken
        
    }

}

