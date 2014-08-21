using System;

namespace Bryan1Language.Common.Streams {

	/// <summary>
	/// A character stream that reads from a file.
	/// </summary>
	public class FileCharacterStream : ICharacterStream {

		public FileCharacterStream() {

		}

		#region ICharacterStream implementation

		public void Open() {
			throw new NotImplementedException();
		}

		public void Close() {
			throw new NotImplementedException();
		}

		#endregion

		#region IStream implementation

		public uint Position {
			get {
				throw new NotImplementedException();
			}
		}

		public void Reset() {
			throw new NotImplementedException();
		}

		public char Current() {
			throw new NotImplementedException();
		}

		public char Next() {
			throw new NotImplementedException();
		}

		public char Peek() {
			throw new NotImplementedException();
		}

		public char Peek(uint i) {
			throw new NotImplementedException();
		}

		public void Skip() {
			throw new NotImplementedException();
		}

		public void Skip(uint i) {
			throw new NotImplementedException();
		}

		public bool IsEndOfStream() {
			throw new NotImplementedException();
		}
			
		public bool IsEndOfStream(uint i) {
			throw new NotImplementedException();
		}

		#endregion

	}
}

