// Type: OpenRasta.Configuration.WebFormsConfigurationExtensions
// Assembly: OpenRasta.Codecs.WebForms, Version=2.0.3.0, Culture=neutral
// Assembly location: C:\Repositories\gleanBoard\wraps\_cache\openrasta-aspnet-2.1.0.48827861\bin-net35\OpenRasta.Codecs.WebForms.dll

using OpenRasta.Configuration.Fluent;
using System;

namespace OpenRasta.Configuration
{
    public static class WebFormsConfigurationExtensions
    {
        [Obsolete("The configuration syntax has changed. Update to RenderedByAspx or .And.RenderedByAspx instead.")]
        public static ICodecDefinition AndRenderedByAspx(this ICodecParentDefinition codecParentDefinition,
                                                         string pageVirtualPath);

        [Obsolete("The configuration syntax has changed. Update to RenderedByAspx or .And.RenderedByAspx instead.")]
        public static ICodecDefinition AndRenderedByUserControl(this ICodecParentDefinition codecParentDefinition,
                                                                string userControlVirtualPath);

        public static ICodecDefinition RenderedByAspx(this ICodecParentDefinition codecParentDefinition,
                                                      string pageVirtualPath);

        public static ICodecDefinition RenderedByAspx(this ICodecParentDefinition codecParentDefinition,
                                                      object viewVirtualPaths);

        public static ICodecDefinition RenderedByUserControl(this ICodecParentDefinition codecParentDefinition,
                                                             string userControlVirtualPath);
    }
}
