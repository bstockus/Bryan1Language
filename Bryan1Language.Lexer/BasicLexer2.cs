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

			return null;
		}

		private Token MatchName() {



			return null;
		}

		private Token MatchNumber() {

			return null;
		}

		private Token MatchSymbol() {

			return null;
		}

		private Token MatchQuote() {

			return null;
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

		private Token CurrentChar() {
			return this.chars[this.position];
		}

		private Token NextChar() {
			return this.NextChar(0);
		}

		private Token NextChar(uint i) {
			return this.chars[this.position + 1 + i];
		}

		private void Consume() {
			this.position++;
		}

		private Token Next() {
			this.Consume();
			return this.CurrentChar();
		}

		private bool IsEnd() {
			return ((this.position + 1) >= this.chars.Length);
		}

		#endregion
	}
}

