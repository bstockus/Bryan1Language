using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class IdentifierNode : Node {

		public string ID { get; private set;}

		public IdentifierNode(NodeRange range, string id) : base(NodeType.Identifier, range) {
			this.ID = id;
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
					{"ID", this.ID}
				};
			}
		}

		public override string Name {
			get {
				return "IdentifierNode";
			}
		}

		#endregion

	}
}

