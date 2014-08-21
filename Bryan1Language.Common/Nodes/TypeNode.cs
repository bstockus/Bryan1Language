using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class TypeNode : Node {

		public enum Type {

			Integer,
			Float,
			String,
			Boolean

		}

		public Type TypeType { get; private set;}

		public TypeNode(NodeRange range, Type type) : base(NodeType.Type, range) {
			this.TypeType = type;
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
					{"TypeType", this.TypeType.ToString()}
				};
			}
		}

		public override string Name {
			get {
				return "TypeNode";
			}
		}

		#endregion

	}
}

