#pragma checksum "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Student\BookDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2ad6b0cbaa462359602bc5ef85e8434004f61fc6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_BookDetails), @"mvc.1.0.view", @"/Views/Student/BookDetails.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Student/BookDetails.cshtml", typeof(AspNetCore.Views_Student_BookDetails))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2ad6b0cbaa462359602bc5ef85e8434004f61fc6", @"/Views/Student/BookDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"271ea46e2b6cbfe9685a3c004f00b4afd139dae8", @"/Views/_ViewImports.cshtml")]
    public class Views_Student_BookDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LMS.SharedFiles.DTOs.BookDTO>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 4 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Student\BookDetails.cshtml"
  
    ViewData["Title"] = "Book Details";

#line default
#line hidden
            BeginContext(89, 24, true);
            WriteLiteral("\r\n<h2>Details</h2>\r\n\r\n\r\n");
            EndContext();
#line 11 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Student\BookDetails.cshtml"
 if (@Model.imageUrl != null)
{

#line default
#line hidden
            BeginContext(147, 8, true);
            WriteLiteral("    <img");
            EndContext();
            BeginWriteAttribute("src", " src=", 155, "", 175, 1);
#line 13 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Student\BookDetails.cshtml"
WriteAttributeValue("", 160, Model.imageUrl, 160, 15, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(175, 42, true);
            WriteLiteral(" alt=\"Image\" height=\"150\" width=\"100\" />\r\n");
            EndContext();
#line 14 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Student\BookDetails.cshtml"
}

#line default
#line hidden
            BeginContext(220, 146, true);
            WriteLiteral("\r\n<div>\r\n    <h4>Book</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            Title\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(367, 37, false);
#line 24 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Student\BookDetails.cshtml"
       Write(Html.DisplayFor(model => model.title));

#line default
#line hidden
            EndContext();
            BeginContext(404, 92, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Author\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(497, 38, false);
#line 30 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Student\BookDetails.cshtml"
       Write(Html.DisplayFor(model => model.author));

#line default
#line hidden
            EndContext();
            BeginContext(535, 91, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Price\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(627, 37, false);
#line 36 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Student\BookDetails.cshtml"
       Write(Html.DisplayFor(model => model.price));

#line default
#line hidden
            EndContext();
            BeginContext(664, 91, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Genre\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(756, 37, false);
#line 42 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Student\BookDetails.cshtml"
       Write(Html.DisplayFor(model => model.genre));

#line default
#line hidden
            EndContext();
            BeginContext(793, 47, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            EndContext();
            BeginContext(840, 39, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "bad76876e50e454887e3ba73161e2df1", async() => {
                BeginContext(862, 13, true);
                WriteLiteral(" Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(879, 10, true);
            WriteLiteral("\r\n</div>\r\n");
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
