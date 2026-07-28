using Data;
using Facade;
using System.ComponentModel.DataAnnotations;

namespace Tests.Facade;

[TestClass] public class PlayerViewTests : SealedTests<PlayerView, UserView> {
    [TestMethod] public void NickTest() => IsProperty<string?>("Player", PlayerView.sentenceEx);
    [TestMethod] public void NameTest() => IsProperty<string?>(null, PlayerView.sentenceEx);
    [TestMethod] public void NationalityTest() => IsProperty<Nationality?>();
    [TestMethod] public void TeamIdTest() => IsProperty<int>("Current Team ID");
    [TestMethod] public void AgeTest() => IsProperty<int>("Age", isReadOnly:true);
    [TestMethod] public void BornTest() => IsProperty<DateTime>("Born", DataType.Date);
    [TestMethod] public void TeamNameTest() => IsProperty<string?>("Current Team");
    [TestMethod] public void StartDate_Is70YearsAgoFromToday() {
        var expected = DateTime.Today.AddYears(-70);
        var actual = typeof(PlayerView).GetField("start", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null);
        Assert.AreEqual(expected, actual);
    }
    [TestMethod] public void EndDate_Is13YearsAgoFromToday() {
        var expected = DateTime.Today.AddYears(-13);
        var actual = typeof(PlayerView).GetField("end", 
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null);
        Assert.AreEqual(expected, actual);
    }
}