using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class VariableExpressionNode : ExpressionNode {

		public IdentifierNode Identifier { get; private set;}

		public VariableExpressionNode(NodeRange range, IdentifierNode identifier) : base(range, ExpressionNodeType.Variable) {
			this.Identifier = identifier;
		}

		#region implemented abstract members of Node

		public override Dictionary<string, Node[]> ChildNodes {
			get {
				return new Dictionary<string, Node[]> {
					{"Identifier", new Node[] { this.Identifier }}
				};
			}
		}

		public override Dictionary<string, string> Attributes {
			get {
				return new Dictionary<string, string> {

				};
			}
		}

		public override string Name {
			get {
				return "VariableExpressionNode";
			}
		}

		#endregion

	}
}

