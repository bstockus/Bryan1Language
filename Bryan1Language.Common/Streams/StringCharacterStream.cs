using System;

namespace Bryan1Language.Common.Streams {

	/// <summary>
	/// A character stream that reads from a string
	/// </summary>
	public class StringCharacterStream : ICharacterStream {

		private String _string;
		private char[] charArray;

		private uint currentPosition = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="Bryan1Language.Common.Streams.StringCharacterStream"/> class.
		/// </summary>
		/// <param name="_string">the <c>String</c> to read from</param>
		public StringCharacterStream(String _string) {
			this.charArray = _string.ToCharArray();
		}

		#region ICharacterStream implementation

		public void Open() {
			this.Reset();
		}

		public void Close() {

		}

		#endregion

		#region IStream implementation

		public uint Position {
			get {
				return currentPosition;
			}
		}

		public void Reset() {
			this.currentPosition = 0;
		}

		public char Current() {
			return this.charArray[this.currentPosition];
		}

		public char Next() {
			return this.charArray[++this.currentPosition];
		}

		public char Peek() {
			return this.Peek(0);
		}

		public char Peek(uint i) {
			return this.charArray[this.currentPosition + 1 + i];
		}

		public void Skip() {
			this.currentPosition++;
		}

		public void Skip(uint i) {
			this.currentPosition += i;
		}

		public bool IsEndOfStream() {
			return this.IsEndOfStream(0);
		}

		public bool IsEndOfStream(uint i) {
			return ((this.currentPosition + 1 + i) >= (this.charArray.Length - 1));
		}

		#endregion

	}
}

