// Type: OpenRasta.Codecs.Razor.OpenRastaCSharpRazorCodeGenerator
// Assembly: OpenRasta.Codecs.Razor, Version=1.0.0.0, Culture=neutral
// Assembly location: C:\Repositories\gleanBoard\lib\OpenRasta.Codecs.Razor.dll

using System.Web.Razor;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser.SyntaxTree;

namespace OpenRasta.Codecs.Razor
{
    public class OpenRastaCSharpRazorCodeGenerator : CSharpRazorCodeGenerator
    {
        public OpenRastaCSharpRazorCodeGenerator(string className, string rootNamespaceName, string sourceFileName,
                                                 RazorEngineHost host);

        protected override bool TryVisitSpecialSpan(Span span);
    }
}
