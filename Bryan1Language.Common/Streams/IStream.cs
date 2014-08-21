using System;

namespace Bryan1Language.Common.Streams {

	/// <summary>
	/// A Generic Stream
	/// </summary>
	public interface IStream<T> {

		/// <summary>
		/// Gets the current position of the stream.
		/// </summary>
		/// <value>The position.</value>
		uint Position { get;}

		/// <summary>
		/// Reset the stream back to the beginning.
		/// </summary>
		void Reset();

		/// <summary>
		/// Get the current value in the stream.
		/// </summary>
		T Current();

		/// <summary>
		/// Get next value in the stream, and advance the stream's position.
		/// </summary>
		T Next();

		/// <summary>
		/// Get next value in the stream, and do not advance the stream's position.
		/// </summary>
		T Peek();

		/// <summary>
		/// Get the <c>i</c>th next value in the stream, and do not advance the stream's position.
		/// </summary>
		/// <param name="i">The amount to go forward in the stream.</param>
		T Peek(uint i);

		/// <summary>
		/// Move the stream's position forward by 1.
		/// </summary>
		void Skip();

		/// <summary>
		/// Move the stream's position forward by the specified i.
		/// </summary>
		/// <param name="i">The position.</param>
		void Skip(uint i);

		/// <summary>
		/// Determines whether the stream's current position is the last in the stream.
		/// </summary>
		/// <returns><c>true</c> if this is the last position in the stream; otherwise, <c>false</c>.</returns>
		bool IsEndOfStream();

		/// <summary>
		/// Determines whether the stream's next <c>i</c>th position is the last in the stream.
		/// </summary>
		/// <returns><c>true</c> if this is the last position in the stream; otherwise, <c>false</c>.</returns>
		/// <param name="i">The position.</param>
		bool IsEndOfStream(uint i);

	}
}

