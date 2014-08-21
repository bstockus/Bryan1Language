using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class StringLiteralNode : LiteralNode {

		public string Value { get; private set;}

		public StringLiteralNode(NodeRange range, string value) : base(range, LiteralNode.Type.String) {
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
				return "StringLiteralNode";
			}
		}

		#endregion

	}
}

