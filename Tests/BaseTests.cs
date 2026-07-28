namespace Tests;
public class BaseTests {
    protected const int repeatCount = 1000;
    protected void AreEqual<T>(T? x, T? y, string? msg = null) => Assert.AreEqual(x, y, msg);
    protected void AreNotEqual<T>(T? x, T? y, string? msg = null) => Assert.AreNotEqual(x, y, msg);
    protected void IsTrue(bool x, string? msg = null) => Assert.IsTrue(x, msg);
    protected void IsFalse(bool x, string? msg = null) => Assert.IsFalse(x, msg);
    protected void IsNull<T>(T? x, string? msg = null) => Assert.IsNull(x, msg);
    protected void IsNotNull<T>(T? x, string? msg = null) => Assert.IsNotNull(x, msg);
    protected void IsOfType(object? x, Type t, string? msg = null) => Assert.IsInstanceOfType(x, t, msg);
    protected void IsNotOfType(object? x, Type t, string? msg = null) => Assert.IsNotInstanceOfType(x, t, msg);
    protected void NotTested(string? msg = null) => Assert.Inconclusive(msg);
    protected void AreSame<T>(T? x, T? y, string? msg = null) => Assert.AreSame(x, y, msg);
    protected void AreNotSame<T>(T? x, T? y, string? msg = null) => Assert.AreNotSame(x, y, msg);
    protected void Fail(string? msg = null) => Assert.Fail(msg);
}