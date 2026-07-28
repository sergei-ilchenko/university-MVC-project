using Aids.GoF.Crea;

namespace Tests.Aids.GoF.Crea;
[TestClass] public sealed class FactoryMethodTests : StaticTests {
    protected override Type setType() => typeof(FactoryMethod);
    [TestMethod] public void CreateTest() {
        var x = new CopyTestClass1() { Id = 10001, Name = "Aaa Bbb Ccc", ValidFrom = DateTime.Now};
        var y = FactoryMethod.Create<CopyTestClass2, CopyTestClass1>(x);
        IsOfType(y, typeof(CopyTestClass2));
        AreEqual("Aaa Bbb Ccc", y.Name);
        IsNull(y.Id);
    }
}