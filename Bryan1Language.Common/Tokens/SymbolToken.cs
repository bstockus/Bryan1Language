using System;

namespace Bryan1Language.Common.Tokens {

	public class SymbolToken : Token {

		public enum Symbol {
			Plus,
			Minus,
			Times,
			Division,
			Assignment,
			Mod,
			Equal,
			NotEqual,
			LessThan,
			GreaterThan,
			LessThanOrEqual,
			GreaterThanOrEqual,
			Semicolon,
			OpenParens,
			CloseParens,
			OpenBrace,
			CloseBrace
		}

		public Symbol Value { get; private set;}

		public SymbolToken(TokenRange range, Symbol value) : base(TokenType.Symbol, range) {
			this.Value = value;
		}

		public override string ToString() {
			return string.Format("<{0}>", this.Value.ToString());
		}

	}
}

