using System;

namespace Bryan1Language.Common.Nodes {

	public abstract class StatementNode : Node {

		public StatementNodeType StatementType { get; private set;} 

		public StatementNode(NodeRange range, StatementNodeType statementType) : base(NodeType.Statement, range) {
			this.StatementType = statementType;
		}

	}
}

