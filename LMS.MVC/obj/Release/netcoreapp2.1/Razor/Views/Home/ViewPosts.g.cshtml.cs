#pragma checksum "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Home\ViewPosts.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a96192e592aec1fad72b07da5b989abc351ad8c2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_ViewPosts), @"mvc.1.0.view", @"/Views/Home/ViewPosts.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Home/ViewPosts.cshtml", typeof(AspNetCore.Views_Home_ViewPosts))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a96192e592aec1fad72b07da5b989abc351ad8c2", @"/Views/Home/ViewPosts.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"271ea46e2b6cbfe9685a3c004f00b4afd139dae8", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_ViewPosts : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<LMS.SharedFiles.DTOs.PostDTO>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "CreatePost", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("glyphicon glyphicon-plus"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_AzureSearchBarPartial", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(50, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Home\ViewPosts.cshtml"
  
    ViewData["Title"] = "Discussion Forum";

#line default
#line hidden
            BeginContext(104, 29, true);
            WriteLiteral("\r\n<h2>Discussion Forum</h2>\r\n");
            EndContext();
            BeginContext(133, 77, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "fe56d6ae13ef4b03a84e4d3a029db053", async() => {
                BeginContext(193, 13, true);
                WriteLiteral("     New-Post");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(210, 20, true);
            WriteLiteral("\r\n<br />\r\n<br />\r\n\r\n");
            EndContext();
            BeginContext(230, 41, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "95251202098b443080a86ec3ce1c3e3e", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(271, 12, true);
            WriteLiteral("\r\n\r\n<br />\r\n");
            EndContext();
#line 15 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Home\ViewPosts.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
            BeginContext(332, 120, true);
            WriteLiteral("            <div class=\"panel panel-default\">\r\n                <div class=\"panel-heading\">\r\n                    Email:  ");
            EndContext();
            BeginContext(453, 13, false);
#line 19 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Home\ViewPosts.cshtml"
                       Write(item.username);

#line default
#line hidden
            EndContext();
            BeginContext(466, 9, true);
            WriteLiteral(" | Role: ");
            EndContext();
            BeginContext(476, 9, false);
#line 19 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Home\ViewPosts.cshtml"
                                              Write(item.role);

#line default
#line hidden
            EndContext();
            BeginContext(485, 3, true);
            WriteLiteral(" | ");
            EndContext();
            BeginContext(489, 11, false);
#line 19 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Home\ViewPosts.cshtml"
                                                           Write(item.postId);

#line default
#line hidden
            EndContext();
            BeginContext(500, 106, true);
            WriteLiteral("\r\n                </div>\r\n                \r\n                <div class=\"panel-body\">\r\n                    ");
            EndContext();
            BeginContext(607, 9, false);
#line 23 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Home\ViewPosts.cshtml"
               Write(item.text);

#line default
#line hidden
            EndContext();
            BeginContext(616, 46, true);
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n");
            EndContext();
#line 26 "C:\Users\demeht\Documents\TONE\LMS - Final\LMS.MVC\Views\Home\ViewPosts.cshtml"
        }

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<LMS.SharedFiles.DTOs.PostDTO>> Html { get; private set; }
    }
}
#pragma warning restore 1591
