using Data;
using Facade;
using System.ComponentModel.DataAnnotations;

namespace Tests.Facade;

[TestClass] public class TourNViewTests : SealedTests<TourNView, EntityView> {
    [TestMethod] public void TitleTest() => IsProperty<string?>(null, TourNView.sentenceEx);
    [TestMethod] public void StartDateTest() => IsProperty<DateTime>("Start Date", DataType.Date);
    [TestMethod] public void PrizePoolTest() => IsProperty<decimal>("Prize Pool", DataType.Currency);
    [TestMethod] public void SponsorTest() => IsProperty<string>(null, TourNView.sentenceEx);
    [TestMethod] public void nrParticipantsTest() => IsProperty<int>("Participants");
    [TestMethod] public void WinnerTest() => IsProperty<string>(null, TourNView.sentenceEx);
    [TestMethod] public void StatusTest() => IsProperty<Status?>();
}