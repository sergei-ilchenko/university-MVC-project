using System.ComponentModel.DataAnnotations;
using Facade;

namespace Tests.Facade;

[TestClass] public class RatingViewTests : SealedTests<RatingView, EntityView> {
    [TestMethod] public void TeamIdTest() => IsProperty<int>("Team ID");
    [TestMethod] public void ValueTest() => IsProperty<int>("Rating Value");
    [TestMethod] public void UpdatedAtTest() => IsProperty<DateTime>("Last Updated", DataType.DateTime);
}