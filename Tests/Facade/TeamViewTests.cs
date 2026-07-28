using Facade;
using System.ComponentModel.DataAnnotations;

namespace Tests.Facade;

[TestClass] public class TeamViewTests : SealedTests<TeamView, EntityView> {
    [TestMethod] public void NameTest() => IsProperty<string?>(null, TeamView.sentenceEx);
    [TestMethod] public void PlayersCountTest() => IsProperty<int>("Player Count");
    [TestMethod] public void ValueTest() => IsProperty<int>("Rating Value");
    [TestMethod] public void RatingUpdatedAtTest() => IsProperty<DateTime>("Rating Updated At", DataType.DateTime);
}