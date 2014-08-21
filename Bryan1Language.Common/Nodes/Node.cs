using System;
using System.Collections.Generic;

namespace Bryan1Language.Common.Nodes {

	public abstract class Node {

		public NodeRange Range { get; private set;}

		public NodeType NodeType { get; private set;}

		public Node(NodeType type, NodeRange range) {
			this.Range = range;
			this.NodeType = type;
		}

		public abstract Dictionary<string, Node[]> ChildNodes { get; }

		public abstract Dictionary<string, string> Attributes { get; }

		public abstract string Name { get; }

	}
}

