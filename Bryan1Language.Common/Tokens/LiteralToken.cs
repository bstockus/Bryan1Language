using System;

namespace Bryan1Language.Common.Tokens {

	public enum LiteralType {
		Integer,
		Float,
		String,
		Unknown
	}

	public class LiteralToken<T> : Token {

		public T Value { get; private set;}

		public LiteralType ValueType {
			get {
				if (this.Value is int) {
					return LiteralType.Integer;
				} else if (this.Value is double) {
					return LiteralType.Float;
				} else if (this.Value is string) {
					return LiteralType.String;
				} else {
					return LiteralType.Unknown;
				}
			}
		}

		public LiteralToken(TokenRange range, T value) : base(TokenType.Literal, range) {
			this.Value = value;
		}

		public override string ToString() {
			switch (this.ValueType) {
				case LiteralType.Float:
					return string.Format("<FLOAT: {0}>", this.Value);
				case LiteralType.Integer:
					return string.Format("<INT: {0}>", this.Value);
				case LiteralType.String:
					return string.Format("<STRING: \'{0}\'>", this.Value);
				case LiteralType.Unknown:
					return string.Format("<UNKNOWN: {0}>", this.Value);
				default:
					return "<>"; 
			}

		}

	}
}

