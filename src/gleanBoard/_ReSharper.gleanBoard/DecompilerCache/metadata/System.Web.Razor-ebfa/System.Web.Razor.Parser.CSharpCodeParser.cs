// Type: System.Web.Razor.Parser.CSharpCodeParser
// Assembly: System.Web.Razor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: c:\Program Files (x86)\Microsoft ASP.NET\ASP.NET Web Pages\v1.0\Assemblies\System.Web.Razor.dll

using System.Collections.Generic;
using System.Web.Razor.Parser.SyntaxTree;

namespace System.Web.Razor.Parser
{
    public class CSharpCodeParser : CodeParser
    {
        public CSharpCodeParser();
        protected internal Dictionary<string, CodeParser.BlockParser> RazorKeywords { get; }
        protected internal override ISet<string> TopLevelKeywords { get; }
        public override bool IsAtExplicitTransition();
        public override bool IsAtImplicitTransition();
        protected override bool TryRecover(bool allowTransition, SpanFactory previousSpanFactory);
        public override void ParseBlock();

        protected internal CodeParser.BlockParser WrapSimpleBlockParser(BlockType type,
                                                                        CodeParser.BlockParser blockParser);

        protected bool HandleReservedWord(CodeBlockInfo block);
        protected internal virtual bool ParseInheritsStatement(CodeBlockInfo block);
        protected internal virtual bool ParseImplicitExpression(CodeBlockInfo block);

        protected internal virtual bool ParseImplicitExpression(CodeBlockInfo block, bool isWithinCode,
                                                                bool expectIdentifierFirst);

        protected internal virtual void ParseStatement(CodeBlockInfo block);
        protected internal virtual void ParseInvalidMarkupSwitch();
        protected internal virtual bool ParseConditionalBlockStatement(CodeBlockInfo block);
        protected internal virtual bool ParseControlFlowBody(CodeBlockInfo block);
        protected internal virtual bool ParseTryStatement(CodeBlockInfo block);
        protected internal virtual bool ParseDoStatement(CodeBlockInfo block);
        protected internal virtual bool ParseIfStatement(CodeBlockInfo block);
        protected internal virtual void AcceptWhiteSpaceAndComments();
        protected internal virtual bool ParseCaseBlock(CodeBlockInfo block);
        protected internal override bool HandleTransition(SpanFactory spanFactory);
        protected internal override bool TryAcceptStringOrComment();
        protected internal override void AcceptGenericArgument();
    }
}
