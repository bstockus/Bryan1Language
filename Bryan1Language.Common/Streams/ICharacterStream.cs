using System;

namespace Bryan1Language.Common.Streams {

	/// <summary>
	/// A Stream of Characters
	/// </summary>
	public interface ICharacterStream : IStream<char> {

		/// <summary>
		/// Open the Character Stream for reading.
		/// </summary>
		void Open();

		/// <summary>
		/// Close the Character Stream.
		/// </summary>
		void Close();

	}
}

