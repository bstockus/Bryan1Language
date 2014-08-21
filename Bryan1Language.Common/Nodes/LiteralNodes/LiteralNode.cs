using System;

namespace Bryan1Language.Common.Nodes {

	public abstract class LiteralNode : Node {

		public enum Type {
			Integer,
			Float,
			String,
			Boolean
		}

		public Type LiteralType { get; private set;}

		public LiteralNode(NodeRange range, Type type) : base(NodeType.Literal, range) {
			this.LiteralType = type;
		}

	}
}

