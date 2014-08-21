using System;

namespace Bryan1Language.Common {

	public interface IParser {

		Nodes.Node Parse(Tokens.Token[] input);

	}
}

