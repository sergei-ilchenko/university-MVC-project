using Core;
using Microsoft.JSInterop;
using Moq;

namespace Tests.Core;

[TestClass]
public class ExampleJsInteropTests
{
    [TestMethod]
    public async Task Prompt_CallsShowPromptAndReturnsResult()
    {
        var jsRuntimeMock = new Mock<IJSRuntime>();
        var jsModuleMock = new Mock<IJSObjectReference>();
        string expected = "result";
        string message = "test message";

        jsModuleMock
            .Setup(m => m.InvokeAsync<string>(
                "showPrompt",
                It.Is<object[]>(args => args.Length == 1 && (string)args[0] == message)
            ))
            .ReturnsAsync(expected);

        jsRuntimeMock
            .Setup(j => j.InvokeAsync<IJSObjectReference>(
                "import", It.IsAny<object[]>()))
            .ReturnsAsync(jsModuleMock.Object);

        var interop = new ExampleJsInterop(jsRuntimeMock.Object);

       
        var result = await interop.Prompt(message);

       
        Assert.AreEqual(expected, result);
        jsRuntimeMock.Verify(j => j.InvokeAsync<IJSObjectReference>(
            "import", It.Is<object[]>(args => args[0]!.ToString()!.Contains("exampleJsInterop.js"))), Times.Once);
        jsModuleMock.Verify(m => m.InvokeAsync<string>(
            "showPrompt",
            It.Is<object[]>(args => args.Length == 1 && (string)args[0] == message)
        ), Times.Once);
    }
    [TestMethod]
    public async Task DisposeAsync_DisposesModuleIfLoaded()
    {
       
        var jsRuntimeMock = new Mock<IJSRuntime>();
        var jsModuleMock = new Mock<IJSObjectReference>();

        jsModuleMock
            .Setup(m => m.InvokeAsync<string>("showPrompt", It.IsAny<object[]>()))
            .ReturnsAsync("ok");

        jsModuleMock
            .Setup(m => m.DisposeAsync())
            .Returns(ValueTask.CompletedTask)
            .Verifiable();

        jsRuntimeMock
            .Setup(j => j.InvokeAsync<IJSObjectReference>(
                "import", It.IsAny<object[]>()))
            .ReturnsAsync(jsModuleMock.Object);

        var interop = new ExampleJsInterop(jsRuntimeMock.Object);

        
        await interop.Prompt("dispose test");
        await interop.DisposeAsync();

       
        jsModuleMock.Verify(m => m.DisposeAsync(), Times.Once);
    }

    [TestMethod]
    public async Task DisposeAsync_DoesNotDisposeIfNotLoaded()
    {
       
        var jsRuntimeMock = new Mock<IJSRuntime>();
        var jsModuleMock = new Mock<IJSObjectReference>();

        jsModuleMock
            .Setup(m => m.DisposeAsync())
            .Returns(ValueTask.CompletedTask)
            .Verifiable();

        jsRuntimeMock
            .Setup(j => j.InvokeAsync<IJSObjectReference>(
                "import", It.IsAny<object[]>()))
            .ReturnsAsync(jsModuleMock.Object);

        var interop = new ExampleJsInterop(jsRuntimeMock.Object);

        
        await interop.DisposeAsync();

        
        jsModuleMock.Verify(m => m.DisposeAsync(), Times.Never);
    }
}
