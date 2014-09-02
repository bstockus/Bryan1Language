using System;
using Bryan1Language.Common.Nodes;
using Bryan1Language.Common;
using Bryan1Language.Common.Tokens;
using System.Collections.Generic;

namespace Bryan1Language.Parser {

    public class BasicParser : IParser {

        private Token[] tokens;
        private uint position;

        private Stack<uint> markers;

        #region IParser implementation

        public Node Parse(Token[] input) {

            this.tokens = input;
            this.position = 0;
            this.markers = new Stack<uint>();

            return this.ParseProgram();
        }

        #endregion

        #region Parse Rule methods

        private ProgramNode ParseProgram() {

            List<StatementNode> statements = new List<StatementNode>();

            while (!this.IsEnd()) {
                statements.Add(this.ParseStatement());
            }

            return new ProgramNode(null, statements.ToArray());

        }

        private TypeNode ParseType() {

            Token t = this.CurrentToken();

            if (BasicParser.IsIntKeyword(t)) {
                this.Consume();
                return new TypeNode(null, TypeNode.Type.Integer);
            } else if (BasicParser.IsFloatKeyword(t)) {
                this.Consume();
                return new TypeNode(null, TypeNode.Type.Float);
            } else if (BasicParser.IsStrKeyword(t)) {
                this.Consume();
                return new TypeNode(null, TypeNode.Type.String);
            } else if (BasicParser.IsBoolKeyword(t)) {
                this.Consume();
                return new TypeNode(null, TypeNode.Type.Boolean);
            } else {
                throw new Exception("Expecting a Type");
            }
        }

        private IdentifierNode ParseIdentifier() {

            Token t = this.CurrentToken();

            if (BasicParser.IsIdentifier(t)) {
                this.Consume();
                return new IdentifierNode(null, ((IdentifierToken)t).Value);
            } else {
                throw new Exception("Expecting an Identifier");
            }

        }

        #region Expression Parse Rule methods

        private ExpressionNode ParseExpression() {

            return null;
        }

        private ExpressionNode ParseRelationalExpression() {

            if (this.SpeculateEqualRelationalExpression()) {

                ExpressionNode leftExpression = this.ParseNumericalExpression();

                if (!BasicParser.IsEqualSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Equal Symbol");
                }

                this.Consume();

                ExpressionNode rightExpression = this.ParseRelationalExpression();

                OperationExpressionNode results = new OperationExpressionNode(null, leftExpression, rightExpression, new OperatorNode(null, OperatorNode.Type.Equal));

                return (ExpressionNode)results;

            } else if (this.SpeculateNotEqualRelationalExpression()) {

                ExpressionNode leftExpression = this.ParseNumericalExpression();

                if (!BasicParser.IsNotEqualSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Not Equal Symbol");
                }

                this.Consume();

                ExpressionNode rightExpression = this.ParseRelationalExpression();

                OperationExpressionNode results = new OperationExpressionNode(null, leftExpression, rightExpression, new OperatorNode(null, OperatorNode.Type.NotEqual));

                return (ExpressionNode)results;

            } else if (this.SpeculateGreaterThanRelationalExpression()) {

                ExpressionNode leftExpression = this.ParseNumericalExpression();

                if (!BasicParser.IsGreaterThanSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Greater Than Symbol");
                }

                this.Consume();

                ExpressionNode rightExpression = this.ParseRelationalExpression();

                OperationExpressionNode results = new OperationExpressionNode(null, leftExpression, rightExpression, new OperatorNode(null, OperatorNode.Type.GreaterThan));

                return (ExpressionNode)results;

            } else if (this.SpeculateGreaterThanOrEqualRelationalExpression()) {

                ExpressionNode leftExpression = this.ParseNumericalExpression();

                if (!BasicParser.IsGreaterThanOrEqualSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Greater Than Or Equal Symbol");
                }

                this.Consume();

                ExpressionNode rightExpression = this.ParseRelationalExpression();

                OperationExpressionNode results = new OperationExpressionNode(null, leftExpression, rightExpression, new OperatorNode(null, OperatorNode.Type.GreaterThanOrEqual));

                return (ExpressionNode)results;

            } else if (this.SpeculateLessThanRelationalExpression()) {

                ExpressionNode leftExpression = this.ParseNumericalExpression();

                if (!BasicParser.IsLessThanSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Less Than Symbol");
                }

                this.Consume();

                ExpressionNode rightExpression = this.ParseRelationalExpression();

                OperationExpressionNode results = new OperationExpressionNode(null, leftExpression, rightExpression, new OperatorNode(null, OperatorNode.Type.LessThan));

                return (ExpressionNode)results;

            } else if (this.SpeculateLessThanOrEqualRelationalExpression()) {

                ExpressionNode leftExpression = this.ParseNumericalExpression();

                if (!BasicParser.IsLessThanOrEqualSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Less Than Or Equal Symbol");
                }

                this.Consume();

                ExpressionNode rightExpression = this.ParseRelationalExpression();

                OperationExpressionNode results = new OperationExpressionNode(null, leftExpression, rightExpression, new OperatorNode(null, OperatorNode.Type.LessThanOrEqual));

                return (ExpressionNode)results;

            } else if (this.SpeculateNumericalExpressionRelationalExpression()) {

                return this.ParseNumericalExpression();

            } else {

                throw new Exception("Expecting a Relational Expression.");

            }

            return null;
        }

        private ExpressionNode ParseNumericalExpression() {

            if (this.SpeculateAdditionNumericalExpression()) {

                ExpressionNode leftExpression = this.ParseTerm();

                if (!BasicParser.IsPlusSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Plus Symbol");
                }

                this.Consume();

                ExpressionNode rightExpression = this.ParseNumericalExpression();

                OperationExpressionNode results = new OperationExpressionNode(null, leftExpression, rightExpression, new OperatorNode(null, OperatorNode.Type.Addition));

                return (ExpressionNode)results;

            } else if (this.SpeculateSubtractionNumericalExpression()) {

                ExpressionNode leftExpression = this.ParseTerm();

                if (!BasicParser.IsMinusSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Minus Symbol");
                }

                this.Consume();

                ExpressionNode rightExpression = this.ParseNumericalExpression();

                OperationExpressionNode results = new OperationExpressionNode(null, leftExpression, rightExpression, new OperatorNode(null, OperatorNode.Type.Subtraction));

                return (ExpressionNode)results;

            } else if (this.SpeculateTermNumericalExpression()) {

                return this.ParseTerm();

            } else {

                throw new Exception("Expecting a Numerical Expression");

            }

            return null;
        }

        private ExpressionNode ParseTerm() {

            if (this.SpeculateMultiplicationTerm()) {

                ExpressionNode leftExpression = this.ParseFactor();

                if (!BasicParser.IsTimesSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Times Symbol");
                }

                this.Consume();

                ExpressionNode rightExpression = this.ParseTerm();

                OperationExpressionNode results = new OperationExpressionNode(null, leftExpression, rightExpression, new OperatorNode(null, OperatorNode.Type.Multiplication));

                return (ExpressionNode)results;

            } else if (this.SpeculateDivisionTerm()) {

                ExpressionNode leftExpression = this.ParseFactor();

                if (!BasicParser.IsDivisionSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Division Symbol");
                }

                this.Consume();

                ExpressionNode rightExpression = this.ParseTerm();

                OperationExpressionNode results = new OperationExpressionNode(null, leftExpression, rightExpression, new OperatorNode(null, OperatorNode.Type.Division));

                return (ExpressionNode)results;

            } else if (this.SpeculateFactorTerm()) {

                return this.ParseFactor();

            } else {

                throw new Exception("Expecting a Term.");
            }

        }

        private ExpressionNode ParseFactor() {

            Token t = this.CurrentToken();

            if (BasicParser.IsOpenParensSymbol(t)) {
                this.Consume();

                ExpressionNode expression = this.ParseExpression();

                if (!BasicParser.IsCloseParensSymbol(this.CurrentToken())) {
                    throw new Exception("Expected a Close Parantheses Symbol");
                }

                this.Consume();

                return expression;

            } else if (BasicParser.IsIntegerLiteral(t)) {

                IntegerLiteralNode integerLiteral = this.ParseIntegerLiteral();

                LiteralExpressionNode result = new LiteralExpressionNode(null, integerLiteral);

                return (ExpressionNode)result;

            } else if (BasicParser.IsFloatLiteral(t)) {

                FloatLiteralNode floatLiteral = this.ParseFloatLiteral();

                LiteralExpressionNode result = new LiteralExpressionNode(null, floatLiteral);

                return (ExpressionNode)result;

            } else if (BasicParser.IsIdentifier(t)) {

                IdentifierNode identifier = this.ParseIdentifier();

                VariableExpressionNode result = new VariableExpressionNode(null, identifier);

                return (ExpressionNode)result;

            } else {
                throw new Exception("Expecting a Factor");
            }
        }

        #endregion

        #region Literal Parse Rule methods

        private LiteralNode ParseLiteral() {

            Token t = this.CurrentToken();

            if (BasicParser.IsStringLiteral(t)) {
                return (LiteralNode)this.ParseStringLiteral();
            } else if (BasicParser.IsIntegerLiteral(t)) {
                return (LiteralNode)this.ParseIntegerLiteral();
            } else if (BasicParser.IsFloatLiteral(t)) {
                return (LiteralNode)this.ParseFloatLiteral();
            } else if (BasicParser.IsTrueKeyword(t) || BasicParser.IsFalseKeyword(t)) {
                return (LiteralNode)this.ParseBooleanLiteral();
            } else {
                throw new Exception("Expecting a Literal");
            }

        }

        private StringLiteralNode ParseStringLiteral() {
            Token t = this.CurrentToken();

            if (BasicParser.IsStringLiteral(t)) {
                this.Consume();
                return new StringLiteralNode(null, ((LiteralToken<string>)t).Value);
            } else {
                throw new Exception("Expecting String Literal");
            }
        }

        private IntegerLiteralNode ParseIntegerLiteral() {
            Token t = this.CurrentToken();

            if (BasicParser.IsIntegerLiteral(t)) {
                this.Consume();
                return new IntegerLiteralNode(null, ((LiteralToken<int>)t).Value);
            } else {
                throw new Exception("Expecting Integer Literal");
            }
        }

        private FloatLiteralNode ParseFloatLiteral() {
            Token t = this.CurrentToken();

            if (BasicParser.IsFloatLiteral(t)) {
                this.Consume();
                return new FloatLiteralNode(null, ((LiteralToken<double>)t).Value);
            } else {
                throw new Exception("Expecting Float Literal");
            }
        }

        private BooleanLiteralNode ParseBooleanLiteral() {
            Token t = this.CurrentToken();

            if (BasicParser.IsTrueKeyword(t)) {
                this.Consume();
                return new BooleanLiteralNode(null, true);
            } else if (BasicParser.IsFalseKeyword(t)) {
                this.Consume();
                return new BooleanLiteralNode(null, false);
            } else {
                throw new Exception("Expecting Boolean Literal");
            }
        }

        #endregion

        #region Statement Parse Rule methods

        private StatementNode ParseStatement() {

            Token t = this.CurrentToken();

            StatementNode statement = null;

            if (BasicParser.IsVarKeyword(t)) {
                statement = this.ParseVarStatement();
            } else if (BasicParser.IsLetKeyword(t)) {
                statement = this.ParseLetStatement();
            } else {
                throw new Exception("Expected A Statement.");
            }

            if (!BasicParser.IsSemicolonSymbol(this.CurrentToken())) {
                throw new Exception("Expecting a Semicolon");
            }

            this.Consume();

            return statement;
        }

        private VarStatementNode ParseVarStatement() {

            this.Consume();

            TypeNode type = this.ParseType();

            IdentifierNode identifier = this.ParseIdentifier();

            LiteralNode literal = null;

            if (BasicParser.IsAssignmentSymbol(this.CurrentToken())) {

                this.Consume();

                literal = this.ParseLiteral();

            }

            return new VarStatementNode(null, type, identifier, literal);
        }

        private LetStatementNode ParseLetStatement() {

            this.Consume();

            IdentifierNode identifier = this.ParseIdentifier();

            if (!BasicParser.IsAssignmentSymbol(this.CurrentToken())) {
                throw new Exception("Expecting an assignment symbol.");
            }

            this.Consume();

            ExpressionNode expression = this.ParseRelationalExpression();

            return new LetStatementNode(null, expression, identifier);

        }

        #endregion

        #endregion

        #region Backtracking methods

        
        // This is just a template for the actual Speculate methods, not intended to be ever executed.
        // TODO: Remove this when all Speculate methods have been implemented.
        private bool Speculate() {
            bool success = true;
            this.Mark();

            try {



            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        #region Term Backtracking methods

        private bool SpeculateMultiplicationTerm() {
            bool success = true;
            this.Mark();

            try {

                this.ParseFactor();

                if (!BasicParser.IsTimesSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Times Symbol");
                }

                this.Consume();

                this.ParseTerm();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        private bool SpeculateDivisionTerm() {
            bool success = true;
            this.Mark();

            try {

                this.ParseFactor();

                if (!BasicParser.IsDivisionSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Divsion Symbol");
                }

                this.Consume();

                this.ParseTerm();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        private bool SpeculateFactorTerm() {
            bool success = true;
            this.Mark();

            try {
                this.ParseFactor();
            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        #endregion

        #region Numerical Expression Backtracking methods

        private bool SpeculateAdditionNumericalExpression() {
            bool success = true;
            this.Mark();

            try {

                this.ParseTerm();

                if (!BasicParser.IsPlusSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Plus Symbol");
                }

                this.Consume();

                this.ParseNumericalExpression();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        private bool SpeculateSubtractionNumericalExpression() {
            bool success = true;
            this.Mark();

            try {

                this.ParseTerm();

                if (!BasicParser.IsMinusSymbol(this.CurrentToken())) {
                    throw new Exception("Expecting a Minus Symbol");
                }

                this.Consume();

                this.ParseNumericalExpression();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        private bool SpeculateTermNumericalExpression() {
            bool success = true;
            this.Mark();

            try {

                this.ParseTerm();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        #endregion

        #region Relational Expression Backtracking methods

        private bool SpeculateLessThanRelationalExpression() {
            bool success = true;
            this.Mark();

            try {

                this.ParseNumericalExpression();

                if (!BasicParser.IsLessThanSymbol(this.CurrentToken())) {
                    throw new Exception();
                }

                this.Consume();

                this.ParseRelationalExpression();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        private bool SpeculateGreaterThanRelationalExpression() {
            bool success = true;
            this.Mark();

            try {

                this.ParseNumericalExpression();

                if (!BasicParser.IsGreaterThanSymbol(this.CurrentToken())) {
                    throw new Exception();
                }

                this.Consume();

                this.ParseRelationalExpression();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        private bool SpeculateEqualRelationalExpression() {
            bool success = true;
            this.Mark();

            try {

                this.ParseNumericalExpression();

                if (!BasicParser.IsEqualSymbol(this.CurrentToken())) {
                    throw new Exception();
                }

                this.Consume();

                this.ParseRelationalExpression();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        private bool SpeculateNotEqualRelationalExpression() {
            bool success = true;
            this.Mark();

            try {

                this.ParseNumericalExpression();

                if (!BasicParser.IsNotEqualSymbol(this.CurrentToken())) {
                    throw new Exception();
                }

                this.Consume();

                this.ParseRelationalExpression();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        private bool SpeculateLessThanOrEqualRelationalExpression() {
            bool success = true;
            this.Mark();

            try {

                this.ParseNumericalExpression();

                if (!BasicParser.IsLessThanOrEqualSymbol(this.CurrentToken())) {
                    throw new Exception();
                }

                this.Consume();

                this.ParseRelationalExpression();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        private bool SpeculateGreaterThanOrEqualRelationalExpression() {
            bool success = true;
            this.Mark();

            try {

                this.ParseNumericalExpression();

                if (!BasicParser.IsGreaterThanOrEqualSymbol(this.CurrentToken())) {
                    throw new Exception();
                }

                this.Consume();

                this.ParseRelationalExpression();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        private bool SpeculateNumericalExpressionRelationalExpression() {
            bool success = true;
            this.Mark();

            try {

                this.ParseNumericalExpression();

            } catch (Exception e) {
                success = false;
            }

            this.Rewind();
            return success;
        }

        #endregion

        #endregion

        #region Token Matching methods

        private static bool IsIdentifier(Token t) {
            return t.Type == TokenType.Identifier;
        }

        private static bool IsKeyword(Token t) {
            return t.Type == TokenType.Keyword;
        }

        private static bool IsLiteral(Token t) {
            return t.Type == TokenType.Literal;
        }

        private static bool IsSymbol(Token t) {
            return t.Type == TokenType.Symbol;
        }

        #region Keyword Token Matching methods

        private static bool IsKeywordOfType(Token t, KeywordToken.Keyword k) {
            if (BasicParser.IsKeyword(t)) {
                return ((KeywordToken)t).Value == k;
            }
            return false;
        }

        private static bool IsIfKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.If);
        }

        private static bool IsElseKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Else);
        }

        private static bool IsForKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.For);
        }

        private static bool IsDoKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Do);
        }

        private static bool IsWhileKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.While);
        }

        private static bool IsBreakKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Break);
        }

        private static bool IsContinueKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Continue);
        }

        private static bool IsReturnKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Return);
        }

        private static bool IsIntKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Int);
        }

        private static bool IsFloatKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Float);
        }

        private static bool IsStrKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Str);
        }

        private static bool IsBoolKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Bool);
        }

        private static bool IsLetKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Let);
        }

        private static bool IsVarKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.Var);
        }

        private static bool IsTrueKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.True);
        }

        private static bool IsFalseKeyword(Token t) {
            return BasicParser.IsKeywordOfType(t, KeywordToken.Keyword.False);
        }

        #endregion

        #region Literal Token Matching methods

        private static bool IsStringLiteral(Token t) {
            if (BasicParser.IsLiteral(t)) {
                return (t is LiteralToken<string>);
            }
            return false;
        }

        private static bool IsIntegerLiteral(Token t) {
            if (BasicParser.IsLiteral(t)) {
                return (t is LiteralToken<int>);
            }
            return false;
        }

        private static bool IsFloatLiteral(Token t) {
            if (BasicParser.IsLiteral(t)) {
                return (t is LiteralToken<double>);
            }
            return false;
        }

        #endregion

        #region Symbol Token Matching methods

        private static bool IsSymbolOfType(Token t, SymbolToken.Symbol s) {
            if (BasicParser.IsSymbol(t)) {
                return ((SymbolToken)t).Value == s;
            }
            return false;
        }

        private static bool IsPlusSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.Plus);
        }

        private static bool IsMinusSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.Minus);
        }

        private static bool IsTimesSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.Times);
        }

        private static bool IsDivisionSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.Division);
        }

        private static bool IsAssignmentSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.Assignment);
        }

        private static bool IsModSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.Mod);
        }

        private static bool IsEqualSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.Equal);
        }

        private static bool IsNotEqualSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.NotEqual);
        }

        private static bool IsGreaterThanSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.GreaterThan);
        }

        private static bool IsLessThanSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.LessThan);
        }

        private static bool IsGreaterThanOrEqualSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.GreaterThanOrEqual);
        }

        private static bool IsLessThanOrEqualSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.LessThanOrEqual);
        }

        private static bool IsSemicolonSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.Semicolon);
        }

        private static bool IsOpenParensSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.OpenParens);
        }

        private static bool IsCloseParensSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.CloseParens);
        }

        private static bool IsOpenBraceSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.OpenBrace);
        }

        private static bool IsCloseBraceSymbol(Token t) {
            return BasicParser.IsSymbolOfType(t, SymbolToken.Symbol.CloseBrace);
        }

        #endregion

        #endregion

        #region Token helpers

        private Token CurrentToken() {
            return this.tokens[this.position];
        }

        private Token NextToken() {
            return this.NextToken(0);
        }

        private Token NextToken(uint i) {
            return this.tokens[this.position + 1 + i];
        }

        private void Consume() {
            this.position++;
        }

        private Token Next() {
            this.Consume();
            return this.CurrentToken();
        }

        private bool IsEnd() {
            return ((this.position + 1) >= this.tokens.Length);
        }

        #endregion

        #region Backtracking helpers

        private void Mark() {
            this.markers.Push(this.position);
        }

        private void Rewind() {
            this.position = this.markers.Pop();
        }

        #endregion

    }
}

