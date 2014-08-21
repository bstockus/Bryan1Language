using System;

namespace Bryan1Language.Common.Tokens {

	public abstract class Token {

		public TokenType Type { get; private set;}

		public TokenRange Range { get; private set;}

		public Token(TokenType type, TokenRange range) {
			this.Type = type;
			this.Range = range;
		}

	}
}

