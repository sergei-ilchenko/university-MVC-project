namespace Tests;

[TestClass] public class UnitTests : StaticTests {

    protected override Type? setType() => null;
    [TestMethod] public override void IsTested() { }
    private object? x;
    private object? y;
    [TestInitialize] public override void Initialize()
    {
        base.Initialize();
        x = new object();
        y = new object();
    }
    [TestCleanup]
    public override void Cleanup()
    {
        base.Cleanup();
        x = null;
        y = null;
    }

    [DataRow("abc", "abc")]
    [DataRow(1, 1)]
    [DataRow(true, true)]

    [TestMethod]
    public void AreEqualTest(object x, object y) => AreEqual(x, y);

    // [TestMethod]
    // public void AreEqualFailsTest() => Assert.AreEqual(1, 2);

    [TestMethod]
    public void AreNotEqualTest() => AreNotEqual("aaa", "bbb");

    // [TestMethod]
    // public void AreNotEqualFailsTest() => Assert.AreNotEqual("aaa", "aaa");

    [TestMethod]
    public void IsTrueTest() => IsTrue(true);

    [TestMethod]
    public void IsFalseTest() => IsFalse(false);

    [TestMethod]
    public void IsNullTest() => IsNull<object>(null);

    [TestMethod]
    public void IsNotNullTest() => IsNotNull("abc");

    // [TestMethod]
    // public void FailTest() => Assert.Fail();

    [TestMethod]
    public void InconclusiveTest() => NotTested("see test vajab tegemist");

    [TestMethod]
    public void IsInstanceOfTypeTest() => IsOfType("abc", typeof(string));

    [TestMethod]
    public void IsNotInstanceOfTypeTest() => IsNotOfType("abc", typeof(int));

    [TestMethod]
    public void AreSameTest()
    {
        var x = new object();
        var y = x;
        AreSame(x, y);
    }

    [TestMethod]
    public void AreNotSameTest()
    {
        var x = new object();
        var y = new object();
        AreNotSame(x, y);
    }
}
