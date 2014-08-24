using System;
using Bryan1Language.Common;
using Bryan1Language.Common.Tokens;
using System.Collections.Generic;

namespace Bryan1Language.Lexer {

    public class BasicLexer2 : ILexer {

        #region Terminal Constants

        private const string WHITESPACE_TERMINALS = " \t\n\r";
        private const string LETTER_TERMINALS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string DIGIT_TERMINALS = "0123456789";
        private const string SYMBOL_TERMINALS = "+-*/=_[]{}()!@#$%^&|\\:;<>,/?";
        private const string QUOTE_TERMINALS = "\'\"";

        #endregion

        #region Keyword Constants

        private static readonly string[] KEYWORDS = new string[] {
            "IF",
            "ELSE",
            "FOR",
            "DO",
            "WHILE",
            "BREAK",
            "CONTINUE",
            "RETURN",
            "INT",
            "FLOAT",
            "STRING",
            "BOOL",
            "LET",
            "VAR",
            "TRUE",
            "FALSE"
        };

        private static readonly KeywordToken.Keyword[] KEYWORD_VALUES = new KeywordToken.Keyword[] {
            KeywordToken.Keyword.If,
            KeywordToken.Keyword.Else,
            KeywordToken.Keyword.For,
            KeywordToken.Keyword.Do,
            KeywordToken.Keyword.While,
            KeywordToken.Keyword.Break,
            KeywordToken.Keyword.Continue,
            KeywordToken.Keyword.Return,
            KeywordToken.Keyword.Int,
            KeywordToken.Keyword.Float,
            KeywordToken.Keyword.Str,
            KeywordToken.Keyword.Bool,
            KeywordToken.Keyword.Let,
            KeywordToken.Keyword.Var,
            KeywordToken.Keyword.True,
            KeywordToken.Keyword.False
        };

        #endregion

        #region Symbol Constants

        private static readonly string[] SYMBOLS = new string[] {
            "+",
            "-",
            "*",
            "/",
            "=",
            "%",
            "==",
            "!=",
            "<",
            ">",
            "<=",
            ">=",
            ";",
            "(",
            ")",
            "{",
            "}"
        };

        private static readonly SymbolToken.Symbol[] SYMBOL_VALUES = new SymbolToken.Symbol[] {
            SymbolToken.Symbol.Plus,
            SymbolToken.Symbol.Minus,
            SymbolToken.Symbol.Times,
            SymbolToken.Symbol.Division,
            SymbolToken.Symbol.Assignment,
            SymbolToken.Symbol.Mod,
            SymbolToken.Symbol.Equal,
            SymbolToken.Symbol.NotEqual,
            SymbolToken.Symbol.LessThan,
            SymbolToken.Symbol.GreaterThan,
            SymbolToken.Symbol.LessThanOrEqual,
            SymbolToken.Symbol.GreaterThanOrEqual,
            SymbolToken.Symbol.Semicolon,
            SymbolToken.Symbol.OpenParens,
            SymbolToken.Symbol.CloseParens,
            SymbolToken.Symbol.OpenBrace,
            SymbolToken.Symbol.CloseBrace
        };

        private static readonly Dictionary<char, char[]> LOOKAHEAD_SYMBOLS = new Dictionary<char, char[]> {
            { '=', new char[] {'='} },
            { '!', new char[] {'='} },
            { '<', new char[] {'='} },
            { '>', new char[] {'='} }
        };

        #endregion

        private char[] chars;
        private uint position;

        #region ILexer implementation

        public System.Collections.Generic.IEnumerable<Token> Lex(string input) {

            this.chars = input.ToCharArray();
            this.position = 0;

            Token token;
            do {

                token = this.NextToken();

                if (token != null) yield return token;

            } while (token != null);

        }

        #endregion

        #region Token Rule methods

        private Token NextToken() {

            while (true) {

                char c = this.CurrentChar();

                if (BasicLexer2.IsLetter(c)) {
                    return this.MatchName();
                } else if (BasicLexer2.IsDigit(c)) {
                    return this.MatchNumber();
                } else if (BasicLexer2.IsQuote(c)) {
                    return this.MatchQuote();
                } else if (BasicLexer2.IsSymbol(c)) {
                    return this.MatchSymbol();
                } else if (BasicLexer2.IsWhitespace(c)) {
                    this.ConsumerWhitespace();
                } else {
                    throw new Exception("Unknown Character found in the input stream.");
                }

            }

        }

        private void ConsumerWhitespace() {

            this.Consume();

            while (BasicLexer2.IsWhitespace(this.CurrentChar())) {
                this.Consume();
            }

        }

        private Token MatchName() {

            //Rule: Name -> <LETTER> (<CHARACTER>)?

            string value = this.CurrentChar().ToString();
            this.Consume();

            while (true) {

                if (BasicLexer2.IsCharacter(this.CurrentChar())) {
                    value += this.CurrentChar().ToString();
                    this.Consume();
                } else {
                    break;
                }

            }

            Token token = (Token)this.MatchKeyword(value);

            if (token == null) {
                IdentifierToken idToken = new IdentifierToken(null, value);
                return (Token)idToken;
            } else {
                return token;
            }

        }

        private KeywordToken MatchKeyword(string value) {

            for (uint index = 0; index < BasicLexer2.KEYWORDS.Length; index++) {

                if (BasicLexer2.KEYWORDS[index].ToUpper().Equals(value.ToUpper())) {
                    return new KeywordToken(null, BasicLexer2.KEYWORD_VALUES[index]);
                }

            }
            
            return null;
        }

        private Token MatchNumber() {

            //Rule: Number -> (<DIGIT>)* '.' (<DIGIT>)?

            string value = this.CurrentChar().ToString();
            this.Consume();

            bool isDecimal = false;

            while (true) {

                if (BasicLexer2.IsDigit(this.CurrentChar())) {
                    value += this.CurrentChar().ToString();
                    this.Consume();
                } else if (BasicLexer2.IsMember(".", this.CurrentChar()) && !isDecimal) {
                    isDecimal = true;
                    value += this.CurrentChar().ToString();
                    this.Consume();
                } else {
                    break;
                }

                if (isDecimal) {
                    return (Token)(new LiteralToken<double>(null, Double.Parse(value)));
                } else {
                    return (Token)(new LiteralToken<int>(null, int.Parse(value)));
                }

            }

            return null;
        }

        private Token MatchSymbol() {


            return null;
        }

        private Token MatchQuote() {

            //Rule: Quote -> <QUOTE> (!<QUOTE>)* <QUOTE>

            string value = "";
            this.Consume();

            while (true) {

                if (!BasicLexer2.IsQuote(this.CurrentChar())) {
                    value += this.CurrentChar().ToString();
                    this.Consume();
                } else {
                    break;
                }

            }

            return (Token)(new LiteralToken<string>(null, value));
        }

        #endregion

        #region Terminal Matching methods

        private static bool IsMember(char[] chars, char c) {
            foreach (char x in chars) {
                if (x == c) return true;
            }
            return false;
        }

        private static bool IsMember(string chars, char c) {
            return BasicLexer2.IsMember(chars.ToCharArray(), c);
        }

        private static bool IsWhitespace(char c) {
            return BasicLexer2.IsMember(BasicLexer2.WHITESPACE_TERMINALS, c);
        }

        private static bool IsLetter(char c) {
            return BasicLexer2.IsMember(BasicLexer2.LETTER_TERMINALS, c);
        }

        private static bool IsDigit(char c) {
            return BasicLexer2.IsMember(BasicLexer2.DIGIT_TERMINALS, c);
        }

        private static bool IsCharacter(char c) {
            return BasicLexer2.IsLetter(c) || BasicLexer2.IsDigit(c);
        }

        private static bool IsSymbol(char c) {
            return BasicLexer2.IsMember(BasicLexer2.SYMBOL_TERMINALS, c);
        }

        private static bool IsQuote(char c) {
            return BasicLexer2.IsMember(BasicLexer2.QUOTE_TERMINALS, c);
        }

        #endregion

        #region Char helpers

        private char CurrentChar() {
            return this.chars[this.position];
        }

        private char NextChar() {
            return this.NextChar(0);
        }

        private char NextChar(uint i) {
            return this.chars[this.position + 1 + i];
        }

        private void Consume() {
            this.position++;
        }

        private char Next() {
            this.Consume();
            return this.CurrentChar();
        }

        private bool IsEnd() {
            return ((this.position + 1) >= this.chars.Length);
        }

        #endregion
    }
}

