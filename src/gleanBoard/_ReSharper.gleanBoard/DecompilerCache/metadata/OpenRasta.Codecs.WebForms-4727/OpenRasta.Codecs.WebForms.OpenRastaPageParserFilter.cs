// Type: OpenRasta.Codecs.WebForms.OpenRastaPageParserFilter
// Assembly: OpenRasta.Codecs.WebForms, Version=2.0.3.0, Culture=neutral
// Assembly location: C:\Repositories\gleanBoard\wraps\_cache\openrasta-aspnet-2.1.0.48827861\bin-net35\OpenRasta.Codecs.WebForms.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace OpenRasta.Codecs.WebForms
{
    public class OpenRastaPageParserFilter : PageParserFilter
    {
        public OpenRastaPageParserFilter();
        public override bool AllowCode { get; }
        public override int NumberOfControlsAllowed { get; }
        public override int NumberOfDirectDependenciesAllowed { get; }
        public override int TotalNumberOfDependenciesAllowed { get; }
        public static Type GetTypeFromCSharpType(string typeName, IEnumerable<string> namespaces);
        public static Type GetTypeFromFriendlyType(string typeName, IEnumerable<string> namespaces);
        public override bool AllowBaseType(Type baseType);
        public override bool AllowControl(Type controlType, ControlBuilder builder);
        public override bool AllowServerSideInclude(string includeVirtualPath);
        public override bool AllowVirtualReference(string referenceVirtualPath, VirtualReferenceType referenceType);
        public string ParseInheritsAttribute(string originalAttribute);
        public override void PreprocessDirective(string directiveName, IDictionary attributes);
        public override bool ProcessCodeConstruct(CodeConstructType codeType, string code);
    }
}
