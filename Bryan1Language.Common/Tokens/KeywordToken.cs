using System;

namespace Bryan1Language.Common.Tokens {

	public class KeywordToken : Token {

		public enum Keyword {
			If,
			Else,
			For,
			Do,
			While,
			Break,
			Continue,
			Return,
			Int,
			Float,
			Str,
			Bool,
			Let,
			Var,
			True,
			False
		}

		public Keyword Value { get; private set;}

		public KeywordToken(TokenRange range, Keyword value) : base(TokenType.Keyword, range) {
			this.Value = value;
		}

		public override string ToString() {
			return string.Format("<{0}>", this.Value.ToString());
		}

	}
}

