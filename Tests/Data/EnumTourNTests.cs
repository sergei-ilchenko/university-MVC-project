using Data;

namespace Tests.Data;

[TestClass] public class StatusTests() : EnumTests<Status>(4) {
    [TestMethod] public void PastTest() => IsEnum(3);
    [TestMethod] public void LiveTest() => IsEnum(2);
    [TestMethod] public void UpcomingTest() => IsEnum(1);
    [TestMethod] public void UnspecifiedTest() => IsEnum(0);
}