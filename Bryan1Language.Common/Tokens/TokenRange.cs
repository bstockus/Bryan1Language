using System;

namespace Bryan1Language.Common.Tokens {

	/// <summary>
	/// The range in the source where the token came from.
	/// </summary>
	public class TokenRange {

		public uint Begin { get; private set;}

		public uint End { get; private set;}

		public uint Length { 
			get {
				return (this.End - this.Begin) + 1;
			}
		}

		public TokenRange(uint begin, uint end) {
			this.Begin = begin;
			this.End = end;
		}

		public string ToString() {
			return this.Begin.ToString() + ".." + this.End.ToString();
		}

	}
}

