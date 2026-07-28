using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Moq;
using Core.Editors;


namespace Tests.Core;

[TestClass]
public class HtmlForInputTests
{
    public class TestModel
    {
        public string Name { get; set; }
    }

    [TestMethod]
    public void ForInput_CallsForShowAndValidationMessageFor()
    {
        var htmlHelperMock = new Mock<IHtmlHelper<TestModel>>();
        var editorContent = new HtmlString("<input />");
        var validationContent = new HtmlString("<span>Validation</span>");
        var forShowContent = new HtmlString("<div>ForShow</div>");
        Expression<Func<TestModel, string>> expr = m => m.Name;

        htmlHelperMock
            .Setup(h => h.ValidationMessageFor(expr, "", It.IsAny<object>()))
            .Returns(validationContent);

        htmlHelperMock
            .Setup(h => h.ForShow(expr, editorContent, validationContent))
            .Returns(forShowContent);

        var result = HtmlForInput.ForInput(htmlHelperMock.Object, expr, editorContent);

        Assert.AreEqual(forShowContent, result);
        htmlHelperMock.Verify(h => h.ValidationMessageFor(expr, "", It.IsAny<object>()), Times.Once);
        htmlHelperMock.Verify(h => h.ForShow(expr, editorContent, validationContent), Times.Once);
    }
}