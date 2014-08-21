using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class OperationExpressionNode : ExpressionNode {

		public ExpressionNode LeftExpression { get; private set;}

		public ExpressionNode RightExpression { get; private set;}

		public OperatorNode Operator { get; private set;}

		public OperationExpressionNode(NodeRange range, ExpressionNode leftExpression, ExpressionNode rightExpression, OperatorNode _operator) : base(range, ExpressionNodeType.Operation) {
			this.LeftExpression = leftExpression;
			this.RightExpression = rightExpression;
			this.Operator = _operator;
		}

		#region implemented abstract members of Node

		public override Dictionary<string, Node[]> ChildNodes {
			get {
				return new Dictionary<string, Node[]> {
					{"LeftExpression", new Node[] { this.LeftExpression }},
					{"Operator", new Node[] { this.Operator }},
					{"RightExpression", new Node[] { this.RightExpression }}
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
				return "OperationExpressionNode";
			}
		}

		#endregion

	}
}

