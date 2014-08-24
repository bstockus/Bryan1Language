using System;
using Bryan1Language.Lexer;
using Bryan1Language.Common;
using Bryan1Language.Common.Streams;
using Bryan1Language.Common.Tokens;
using System.Collections.Generic;
using Bryan1Language.Parser;
using Bryan1Language.Common.Nodes;

namespace Bryan1Language {

    class MainClass {

        public static void Main(string[] args) {

            Console.WriteLine("Bryan1 Language");

            while (true) {

                /*
                Console.Write("> ");
                string str = Console.ReadLine();

                if (str.ToUpper() == "EXIT") break;

                ILexer lexer = new BasicLexer2();

                List<Token> tokens = new List<Token>();

                foreach (Token t in lexer.Lex(str)) {
                    Console.Write(t.ToString() + " ");
                    tokens.Add(t);
                }

                Console.WriteLine();
                */

                
                try {
                    Console.Write("> ");
                    string str = Console.ReadLine();

                    if (str.ToUpper() == "EXIT") break;

                    ILexer lexer = new BasicLexer2();

                    List<Token> tokens = new List<Token>();

                    foreach (Token t in lexer.Lex(str)) {
                        Console.Write(t.ToString() + " ");
                        tokens.Add(t);
                    }

                    Console.WriteLine();

                    IParser parser = new BasicParser();

                    ProgramNode program = (ProgramNode)parser.Parse(tokens.ToArray());

                    ParseTreeWalker ptw = new ParseTreeWalker();

                    ptw.WalkAndPrintTree((Node)program);
                } catch (Exception e) {
                    Console.WriteLine();
                    Console.WriteLine("ERROR: " + e.Message);
                }
                

            }



            return;

        }

    }
}
