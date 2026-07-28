using Aids.GoF.Crea;

namespace Tests.Aids.GoF.Crea;
[TestClass] public sealed class SingletonTests : StaticTests {
    protected override Type setType() => typeof(Singleton);

    [TestMethod]
    public void NewTest() => AreSame(Singleton.New(), Singleton.New());
}