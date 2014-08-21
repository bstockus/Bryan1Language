using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class FloatLiteralNode : LiteralNode {

		public double Value { get; private set;}

		public FloatLiteralNode(NodeRange range, double value) : base(range, LiteralNode.Type.Float) {
			this.Value = value;
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
					{"Value", this.Value.ToString()}
				};
			}
		}

		public override string Name {
			get {
				return "FloatLiteralNode";
			}
		}

		#endregion

	}
}

