using System;
using System.Collections.Generic;
using Bryan1Language.Common;
using Bryan1Language.Common.Streams;
using Bryan1Language.Common.Tokens;

namespace Bryan1Language.Lexer {

	public class BasicLexer : ILexer {

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

		public IEnumerable<Token> Lex(string input) {
			chars = input.ToCharArray();
			position = 0;

			Token token;
			do {

				token = this.NextToken();

				if (token != null) yield return token;

			} while (token != null);
		}

		#endregion

		#region Lexing methods

		private void ConsumeWhitespace() {

			while (!this.IsEnd()) {
				if (!BasicLexer.IsWhitespace(this.NextChar())) break;
				this.Consume();
			}

			this.Consume();

		}

		private Token NextToken() {

			while (true) {

				if (this.position >= this.chars.Length) return null;

				char c = this.CurrentChar();

				if (BasicLexer.IsLetter(c)) {
					return this.MatchName();
				} else if (BasicLexer.IsDigit(c)) {
					return this.MatchNumber();
				} else if (BasicLexer.IsSymbol(c)) {
					return this.MatchSymbol();
				} else if (BasicLexer.IsQuote(c)) {
					return this.MatchQuote();
				} else if (BasicLexer.IsWhitespace(c)) {
					this.ConsumeWhitespace();
					continue;
				} else {
					throw new Exception("Syntax Error");
				}

			}
		}

		private char CurrentChar() {
			return this.chars[this.position];
		}

		private char NextChar() {
			return this.chars[this.position + 1];
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

		#region Rule Matching methods

		private Token MatchName() {

			uint tokenStart = this.position;
			string value = this.CurrentChar().ToString();

			while (!this.IsEnd()) {
				char c = this.Next();

				if (BasicLexer.IsCharacter(c)) {
					value += c;
				} else {
					break;
				}

			}

			this.Consume();

			uint tokenEnd = this.position;
			if (!this.IsEnd()) tokenEnd -= 1;

			Token token = this.MatchKeyword(value, tokenStart, tokenEnd);

			if (token == null) {
				return new IdentifierToken(new TokenRange(tokenStart, tokenEnd), value);
			} else {
				return token;
			}
		}

		private Token MatchKeyword(string value, uint tokenStart, uint tokenEnd) {

			for (int i = 0; i < BasicLexer.KEYWORDS.Length; i ++) {
				string keyword = BasicLexer.KEYWORDS[i];
				if (value.ToUpper() == keyword.ToUpper()) {
					return new KeywordToken(new TokenRange(tokenStart, tokenEnd), BasicLexer.KEYWORD_VALUES[i]);
				}
			}

			return null;
		} 

		private Token MatchNumber() {

			uint tokenStart = this.position;
			string value = this.CurrentChar().ToString();
			string decimalValue = "";
			bool isDecimal = false;

			while (!this.IsEnd()) {
				char c = this.Next();

				if (BasicLexer.IsDigit(c)) {
					if (isDecimal) {
						decimalValue += c;
					} else {
						value += c;
					}
				} else if (BasicLexer.IsMember(".", c)) {
					isDecimal = true;
				} else {
					break;
				}

			}

			//this.Consume();

			uint tokenEnd = this.position;
			if (!this.IsEnd()) tokenEnd -= 1;

			if (isDecimal) {
				return new LiteralToken<double>(new TokenRange(tokenStart, tokenEnd), Double.Parse(value + "." + decimalValue));
			} else {
				return new LiteralToken<int>(new TokenRange(tokenStart, tokenEnd), int.Parse(value));
			}

		}

		private Token MatchSymbol() {

			uint tokenStart = this.position;
			string value = this.CurrentChar().ToString();

			if (!this.IsEnd()) {
				if (BasicLexer.LOOKAHEAD_SYMBOLS.ContainsKey(this.CurrentChar())) {
					char c = this.NextChar();
					if (BasicLexer.IsMember(BasicLexer.LOOKAHEAD_SYMBOLS[this.CurrentChar()], c)) {
						value += c;
						this.Consume();
					}
				}
			}

			this.Consume();

			uint tokenEnd = this.position - 1;
			if (!this.IsEnd()) tokenEnd -= 1;

			for (int i = 0; i < BasicLexer.SYMBOLS.Length; i ++) {
				if (BasicLexer.SYMBOLS[i] == value) {
					return new SymbolToken(new TokenRange(tokenStart, tokenEnd), BasicLexer.SYMBOL_VALUES[i]);
				}
			}

			throw new Exception("Unrecognized symbol!");
		}

		private Token MatchQuote() {

			uint tokenStart = this.position;
			string value = "";

			while (!this.IsEnd()) {

				char c = this.Next();

				if (BasicLexer.IsQuote(c)) {
					break;
				} else {
					value += c;
				}

			}

			this.Consume();

			uint tokenEnd = this.position - 1;
			if (!this.IsEnd()) tokenEnd -= 1;

			return new LiteralToken<string>(new TokenRange(tokenStart, tokenEnd), value);
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
			return BasicLexer.IsMember(chars.ToCharArray(), c);
		}

		private static bool IsWhitespace(char c) {
			return BasicLexer.IsMember(BasicLexer.WHITESPACE_TERMINALS, c);
		}

		private static bool IsLetter(char c) {
			return BasicLexer.IsMember(BasicLexer.LETTER_TERMINALS, c);
		}

		private static bool IsDigit(char c) {
			return BasicLexer.IsMember(BasicLexer.DIGIT_TERMINALS, c);
		}

		private static bool IsCharacter(char c) {
			return BasicLexer.IsLetter(c) || BasicLexer.IsDigit(c);
		}

		private static bool IsSymbol(char c) {
			return BasicLexer.IsMember(BasicLexer.SYMBOL_TERMINALS, c);
		}

		private static bool IsQuote(char c) {
			return BasicLexer.IsMember(BasicLexer.QUOTE_TERMINALS, c);
		}

		#endregion

	}
}

