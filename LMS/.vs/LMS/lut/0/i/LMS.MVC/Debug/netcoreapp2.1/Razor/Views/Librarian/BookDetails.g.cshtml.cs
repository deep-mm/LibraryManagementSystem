#pragma checksum "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\BookDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "49e89d7e8af9627f5ae65e54d8d28c12baa820a4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Librarian_BookDetails), @"mvc.1.0.view", @"/Views/Librarian/BookDetails.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Librarian/BookDetails.cshtml", typeof(AspNetCore.Views_Librarian_BookDetails))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\_ViewImports.cshtml"
using LMS.MVC;

#line default
#line hidden
#line 2 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\_ViewImports.cshtml"
using LMS.MVC.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"49e89d7e8af9627f5ae65e54d8d28c12baa820a4", @"/Views/Librarian/BookDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"271ea46e2b6cbfe9685a3c004f00b4afd139dae8", @"/Views/_ViewImports.cshtml")]
    public class Views_Librarian_BookDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LMS.SharedFiles.DTOs.BookDTO>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditBook", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("glyphicon glyphicon-pencil"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(0, 2, true);
            WriteLiteral("\r\n");
            EndContext();
            BeginContext(39, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\BookDetails.cshtml"
  
    ViewData["Title"] = "Book Details";

#line default
#line hidden
            BeginContext(89, 24, true);
            WriteLiteral("\r\n<h2>Details</h2>\r\n\r\n\r\n");
            EndContext();
#line 11 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\BookDetails.cshtml"
 if(@Model.imageUrl!=null){

#line default
#line hidden
            BeginContext(142, 4, true);
            WriteLiteral("<img");
            EndContext();
            BeginWriteAttribute("src", " src=", 146, "", 166, 1);
#line 12 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\BookDetails.cshtml"
WriteAttributeValue("", 151, Model.imageUrl, 151, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(166, 42, true);
            WriteLiteral(" alt=\"Image\" height=\"150\" width=\"100\" />\r\n");
            EndContext();
#line 13 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\BookDetails.cshtml"
}

#line default
#line hidden
            BeginContext(211, 144, true);
            WriteLiteral("<div>\r\n    <h4>Book</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            Title\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(356, 37, false);
#line 22 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\BookDetails.cshtml"
       Write(Html.DisplayFor(model => model.title));

#line default
#line hidden
            EndContext();
            BeginContext(393, 92, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Author\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(486, 38, false);
#line 28 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\BookDetails.cshtml"
       Write(Html.DisplayFor(model => model.author));

#line default
#line hidden
            EndContext();
            BeginContext(524, 91, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Price\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(616, 37, false);
#line 34 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\BookDetails.cshtml"
       Write(Html.DisplayFor(model => model.price));

#line default
#line hidden
            EndContext();
            BeginContext(653, 91, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Genre\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(745, 37, false);
#line 40 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\BookDetails.cshtml"
       Write(Html.DisplayFor(model => model.genre));

#line default
#line hidden
            EndContext();
            BeginContext(782, 47, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            EndContext();
            BeginContext(829, 97, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "713bdaf51cae4e73b27350a19301455a", async() => {
                BeginContext(918, 4, true);
                WriteLiteral("    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 45 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\BookDetails.cshtml"
                               WriteLiteral(Model.bookId);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(926, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(932, 39, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "990b795344054ef8be029425efbc6c52", async() => {
                BeginContext(954, 13, true);
                WriteLiteral(" Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(971, 14, true);
            WriteLiteral("\r\n\r\n\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<LMS.SharedFiles.DTOs.BookDTO> Html { get; private set; }
    }
}
#pragma warning restore 1591
