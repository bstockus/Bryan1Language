using System;

namespace Bryan1Language.Common.Tokens {

	public class IdentifierToken : Token {

		public string Value { get; private set;}

		public IdentifierToken(TokenRange range, string value) : base(TokenType.Identifier, range) {
			this.Value = value;
		}

		public override string ToString() {
			return "<ID: \'" + this.Value + "\'>";
		}

	}
}

