#pragma checksum "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\DeleteBook.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2bfbaafac414085ed642ec4aaec84ccb0f1d8263"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Librarian_DeleteBook), @"mvc.1.0.view", @"/Views/Librarian/DeleteBook.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Librarian/DeleteBook.cshtml", typeof(AspNetCore.Views_Librarian_DeleteBook))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2bfbaafac414085ed642ec4aaec84ccb0f1d8263", @"/Views/Librarian/DeleteBook.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"271ea46e2b6cbfe9685a3c004f00b4afd139dae8", @"/Views/_ViewImports.cshtml")]
    public class Views_Librarian_DeleteBook : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<LMS.SharedFiles.DTOs.BookDTO>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "DeleteConfirmed", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 4 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\DeleteBook.cshtml"
  
    ViewData["Title"] = "Delete";

#line default
#line hidden
            BeginContext(83, 19, true);
            WriteLiteral("\r\n<h2>Delete Book: ");
            EndContext();
            BeginContext(103, 11, false);
#line 8 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\DeleteBook.cshtml"
            Write(Model.title);

#line default
#line hidden
            EndContext();
            BeginContext(114, 201, true);
            WriteLiteral("</h2>\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n    <h4>Book</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            Title\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(316, 37, false);
#line 19 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\DeleteBook.cshtml"
       Write(Html.DisplayFor(model => model.title));

#line default
#line hidden
            EndContext();
            BeginContext(353, 92, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Author\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(446, 38, false);
#line 25 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\DeleteBook.cshtml"
       Write(Html.DisplayFor(model => model.author));

#line default
#line hidden
            EndContext();
            BeginContext(484, 91, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Price\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(576, 37, false);
#line 31 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\DeleteBook.cshtml"
       Write(Html.DisplayFor(model => model.price));

#line default
#line hidden
            EndContext();
            BeginContext(613, 91, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            Genre\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(705, 37, false);
#line 37 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\DeleteBook.cshtml"
       Write(Html.DisplayFor(model => model.genre));

#line default
#line hidden
            EndContext();
            BeginContext(742, 36, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n        ");
            EndContext();
            BeginContext(778, 94, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4a7d0af36e254fb3bd812908585a55b6", async() => {
                BeginContext(862, 6, true);
                WriteLiteral("Submit");
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
#line 40 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Librarian\DeleteBook.cshtml"
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
            BeginContext(872, 12, true);
            WriteLiteral(" |\r\n        ");
            EndContext();
            BeginContext(884, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "16e9ce516fb945b19d993014b4105310", async() => {
                BeginContext(906, 12, true);
                WriteLiteral("Back to List");
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
            BeginContext(922, 10, true);
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
