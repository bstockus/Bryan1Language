using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public class ProgramNode : Node {

		public StatementNode[] Statements { get; private set;}

		public ProgramNode(NodeRange range, StatementNode[] statements) : base(NodeType.Program, range) {
			this.Statements = statements;
		}

		#region implemented abstract members of Node

		public override Dictionary<string, Node[]> ChildNodes {
			get {
				Node[] statements = new Node[this.Statements.Length];
				for (int x = 0; x < this.Statements.Length; x ++) {
					statements[x] = (Node)this.Statements[x];
				}
				return new Dictionary<string, Node[]> {
					{"Statements", statements}
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
				return "ProgramNode";
			}
		}

		#endregion

	}
}

