using Aids;
namespace Tests.Aids;

internal class CopyTestClass1 {
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime? ValidFrom { get; set; }
}
internal class CopyTestClass2 {
    public string? Id { get; set; }
    public string? Name { get; set; }
}
[TestClass] public sealed class CopyTests : StaticTests {
    protected override Type setType() => typeof(Copy);
    private CopyTestClass2? x;
    [TestInitialize] public override void Initialize() {
        base.Initialize();
        x = Copy.Members(new CopyTestClass1 { Id = 1, Name = "A", ValidFrom = DateTime.Now },
            new CopyTestClass2());
    }
    [TestMethod] public void MembersTest() => AreEqual("A", x?.Name);
    [TestMethod] public void IdTest() => AreEqual(null, x?.Id);
}
