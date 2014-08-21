using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class LetStatementNode : StatementNode {

		public ExpressionNode Expression { get; private set;}

		public IdentifierNode Variable { get; private set;}

		public LetStatementNode(NodeRange range, ExpressionNode expression, IdentifierNode variable) : base(range, StatementNodeType.Let) {
			this.Expression = expression;
			this.Variable = variable;
		}

		#region implemented abstract members of Node

		public override Dictionary<string, Node[]> ChildNodes {
			get {
				return new Dictionary<string, Node[]> {
					{"Variable", new Node[] { this.Variable }},
					{"Expression", new Node[] { this.Expression }}
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
				return "LetStatementNode";
			}
		}

		#endregion

	}
}

