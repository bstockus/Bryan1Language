using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class LiteralExpressionNode : ExpressionNode {

		public LiteralNode Literal { get; private set;}

		public LiteralExpressionNode(NodeRange range, LiteralNode literal) : base(range, ExpressionNodeType.Literal) {
			this.Literal = literal;
		}

		#region implemented abstract members of Node

		public override Dictionary<string, Node[]> ChildNodes {
			get {
				return new Dictionary<string, Node[]> {
					{"Literal", new Node[] { this.Literal }}
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
				return "LiteralExpressionNode";
			}
		}

		#endregion

	}
}

