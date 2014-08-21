using System;

namespace Bryan1Language.Common.Nodes {

	public class NodeRange {

		public uint Begin { get; private set;}

		public uint End { get; private set;}

		public uint Length { 
			get {
				return (this.End - this.Begin) + 1;
			}
		}

		public NodeRange(uint begin, uint end) {
			this.Begin = begin;
			this.End = end;
		}

		public string ToString() {
			return this.Begin.ToString() + ".." + this.End.ToString();
		}

	}

}

