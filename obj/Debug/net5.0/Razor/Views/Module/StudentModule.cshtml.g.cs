#pragma checksum "D:\Elearning\Elearn\Views\Module\StudentModule.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1f91c33e562a378348cde71743145caa8bd5871d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Module_StudentModule), @"mvc.1.0.view", @"/Views/Module/StudentModule.cshtml")]
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
#nullable restore
#line 1 "D:\Elearning\Elearn\Views\_ViewImports.cshtml"
using Elearn;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Elearning\Elearn\Views\_ViewImports.cshtml"
using Elearn.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1f91c33e562a378348cde71743145caa8bd5871d", @"/Views/Module/StudentModule.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fdd51a6fe1f0aff8695cda020bdd558fe8d512ac", @"/Views/_ViewImports.cshtml")]
    public class Views_Module_StudentModule : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Elearn.ElearnModel.Module>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Elearning\Elearn\Views\Module\StudentModule.cshtml"
  
    ViewData["Title"] = "StudentModule";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<style>
    body {
        color: white;
    }
    /* Style the buttons that are used to open and close the accordion panel */
    .accordion {
        background-color: #eee;
        font-size:20px;
        color: #444;
        cursor: pointer;
        padding: 18px;
        width: 100%;
        text-align: center;
        border: none;
        outline: none;
        transition: 0.4s;

    }

        /* Add a background color to the button if it is clicked on (add the .active class with JS), and when you move the mouse over it (hover) */
        .active, .accordion:hover {
            background-color: #86C232;
        }

    /* Style the accordion panel. Note: hidden by default */
    .panel {
        padding: 0 18px;
        background-color: white;
        display: none;
        overflow: hidden;
    }
    .uif {
        height: 400px;
        width: 800px;
        margin-left: auto;
        margin-right: auto;
        display: block
    }
</style>
");
#nullable restore
#line 46 "D:\Elearning\Elearn\Views\Module\StudentModule.cshtml"
 foreach (var item in Model)
{


#line default
#line hidden
#nullable disable
            WriteLiteral("    <button class=\"accordion\"> ");
#nullable restore
#line 49 "D:\Elearning\Elearn\Views\Module\StudentModule.cshtml"
                          Write(Html.DisplayFor(modelItem => item.Modulename));

#line default
#line hidden
#nullable disable
            WriteLiteral("</button>\r\n    <div class=\"panel\">\r\n        <div >\r\n            <iframe class=\"uif\"");
            BeginWriteAttribute("src", " src=\"", 1301, "\"", 1331, 1);
#nullable restore
#line 52 "D:\Elearning\Elearn\Views\Module\StudentModule.cshtml"
WriteAttributeValue("", 1307, Url.Content(item.Video), 1307, 24, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" title=\"description\">VS</iframe>\r\n        </div></div>\r\n");
#nullable restore
#line 54 "D:\Elearning\Elearn\Views\Module\StudentModule.cshtml"



}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"



<script>

    var acc = document.getElementsByClassName(""accordion"");
    var i;

    for (i = 0; i < acc.length; i++) {
        acc[i].addEventListener(""click"", function () {
            /* Toggle between adding and removing the ""active"" class,
            to highlight the button that controls the panel */
            this.classList.toggle(""active"");

            /* Toggle between hiding and showing the active panel */
            var panel = this.nextElementSibling;
            if (panel.style.display === ""block"") {
                panel.style.display = ""none"";
            } else {
                panel.style.display = ""block"";
            }
        });
    }
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Elearn.ElearnModel.Module>> Html { get; private set; }
    }
}
#pragma warning restore 1591