using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class VarStatementNode : StatementNode {

		public TypeNode Type { get; private set;}

		public IdentifierNode Identifier { get; private set;}

		public LiteralNode Literal { get; private set;}

		public VarStatementNode(NodeRange range, TypeNode type, IdentifierNode identifier, LiteralNode literal) : base(range, StatementNodeType.Var) {
			this.Type = type;
			this.Identifier = identifier;
			this.Literal = literal;
		}

		#region implemented abstract members of Node

		public override Dictionary<string, Node[]> ChildNodes {
			get {
				return new Dictionary<string, Node[]> {
					{"Type", new Node[] { this.Type }},
					{"Identifier", new Node[] { this.Identifier }},
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
				return "VarStatementNode";
			}
		}

		#endregion

	}
}

