using System;
using Bryan1Language.Common.Tokens;
using Bryan1Language.Common.Streams;
using System.Collections.Generic;

namespace Bryan1Language.Common {

	public interface ILexer {

		IEnumerable<Token> Lex(string input);

	}
}

