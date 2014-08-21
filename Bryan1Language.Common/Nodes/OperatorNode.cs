using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class OperatorNode : Node {

		public enum Type {
			Addition,
			Subtraction,
			Multiplication,
			Division,
			LessThan,
			GreaterThan,
			Equal,
			NotEqual,
			LessThanOrEqual,
			GreaterThanOrEqual
		}

		public Type OperationType { get; private set;}

		public OperatorNode(NodeRange range, Type type) : base(NodeType.Operator, range) {
			this.OperationType = type;
		}

		#region implemented abstract members of Node

		public override Dictionary<string, Node[]> ChildNodes {
			get {
				return new Dictionary<string, Node[]> {

				};
			}
		}

		public override Dictionary<string, string> Attributes {
			get {
				return new Dictionary<string, string> {
					{"OperationType", this.OperationType.ToString()}
				};
			}
		}

		public override string Name {
			get {
				return "OperatorNode";
			}
		}

		#endregion

	}
}

