using System;
using Bryan1Language.Common.Nodes;

namespace Bryan1Language {

	public class ParseTreeWalker {

		public void WalkAndPrintTree(Node root) {

			this.PrintNode(root, 0, "Root");

		}

		private void PrintNode(Node node, uint level, string prefix) {

			string desc = prefix + ":[" + node.NodeType.ToString() + "::" + node.Name + "] ";

			if (node.Attributes.Count > 0) {
				bool comma = false;

				desc += "{";

				foreach (var attribute in node.Attributes) {
					if (comma) {
						desc += ", ";
					} else {
						comma = true;
					}

					desc += attribute.Key + " = \'" + attribute.Value + "\'";
				}
				desc += "}";

			}

			ParseTreeWalker.PrintLineWithIndentLevel(desc, level);

			if (node.ChildNodes.Count > 0) {

				foreach (var children in node.ChildNodes) {
					foreach (Node child in children.Value) {
						if (child != null) {
							this.PrintNode(child, level + 1, children.Key);
						}
					}
				}

			}


		}

		private const uint INDENT_SIZE = 4;

		private static void PrintLineWithIndentLevel(string line, uint level) {

			string x = "";

			for (int y = 0; y < level; y ++) {
				for (int z = 0; z < INDENT_SIZE; z ++) {
					x += " ";
				}
			}

			x += line;

			Console.WriteLine(x);

		}

	}
}

