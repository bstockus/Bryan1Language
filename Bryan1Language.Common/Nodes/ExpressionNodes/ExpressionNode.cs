using System;

namespace Bryan1Language.Common.Nodes {

	public abstract class ExpressionNode : Node {

		public ExpressionNodeType ExpressionType { get; private set;}

		public ExpressionNode(NodeRange range, ExpressionNodeType type) : base(NodeType.Expression, range) {
			this.ExpressionType = type;
		}

	}
}

