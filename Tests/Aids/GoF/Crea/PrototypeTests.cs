using Aids.GoF.Crea;

namespace Tests.Aids.GoF.Crea;

internal class OtherTestClass {
    public int? Value { get; set; }
}
internal class TestClass : ICloneable<TestClass> {
    public TestClass Clone()
    {
        var x = MemberwiseClone() as TestClass;
        if (x is null) return new TestClass();
        if (Class != null) x.Class = Class?.Clone();
        if (OtherClass != null) x.OtherClass = new() {Value = OtherClass?.Value};
        return x;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? BirthDate { get; set; }
    public TestClass? Class { get; set; }
    public OtherTestClass? OtherClass { get; set; }
}

[TestClass]
public sealed class PrototypeTests : ClassTests<Prototype, object>
{
    private readonly TestClass x = new () { Id = 1, Name = "A", BirthDate = DateTime.Now,
        Class = new() { Id = 2, Name = "B", BirthDate = DateTime.Now.AddYears(-100) },
        OtherClass = new() { Value = 3 } };
    
    private TestClass? y;

    [TestInitialize]
    public override void Initialize()
    {
        base.Initialize();
        y = new Prototype().Clone(x);
    }
    [TestMethod] public void CloneTest() => AreNotSame(x, y);
    [TestMethod] public void IdTest() => AreEqual(x.Id, y?.Id);
    [TestMethod] public void NameTest() => AreEqual(x.Name, y?.Name);
    [TestMethod] public void IsDeepNameCloneTest()
    {
        if (y is null) Assert.Fail();
        AreEqual(x.Name, y.Name);
        y.Name = "C";
        AreEqual("A", x.Name);
        AreEqual("C", y.Name);
    }
    [TestMethod] public void BirthDateTest() => AreEqual(x.BirthDate, y?.BirthDate);
    [TestMethod] public void ClassTest() => AreNotSame(x.Class, y?.Class);
    [TestMethod] public void ClassMustBeDeepCloneTest()
    {
        if (y is null) Assert.Fail();
        if (y.Class is null) Assert.Fail();
        AreEqual(x.Class?.Name, y.Class?.Name);
        y.Class.Name = "C";
        AreEqual("B", x.Class?.Name);
        AreEqual("C", y.Class.Name);
    }
    [TestMethod] public void OtherClassTest() => AreNotSame(x.OtherClass, y?.OtherClass);
    
    [TestMethod] public void OtherClassMustBeDeepCloneTest()
    {
        if (y is null) Assert.Fail();
        if (y.OtherClass is null) Assert.Fail();
        AreEqual(x.OtherClass?.Value, y.OtherClass?.Value);
        y.OtherClass.Value = 5;
        AreEqual(3, x.OtherClass?.Value);
        AreEqual(5, y.OtherClass.Value);
    }
}