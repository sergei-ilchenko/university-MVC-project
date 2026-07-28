using Data;
using Domain;
using Facade;
using Soft.Controllers;

namespace Tests.Soft.Controllers;

[TestClass]
public class TourNControllerTests
    : ControllerBaseTests<TourNController, TourN, TourNData, TourNView>
{
    protected override TourN? createEntity(Func<TourNData> getData)
        => new(getData());

    protected override TourNController CreateObject() => new(dbContext!);

    [TestMethod]
    public override async Task EditViewTest()
    {
        var d1 = createData();
        var v2 = createView();
        if (v2 != null)
        {
            v2.Status = Status.Past;
            v2.Winner = "Testvalue";
            v2.Sponsor = "Testvalue";
            v2.Title = "ValidTitle";
            v2.nrParticipants = 4;
            v2.PrizePool = 1000;
            v2.StartDate = DateTime.Today;

            v2.Id = d1.Id;

            addToSet(d1);
            await obj!.Edit(v2.Id, v2);
            var d = dbSet!.Find(d1!.Id);

            foreach (var pi in typeof(TourNData).GetProperties())
            {
                var vpi = typeof(TourNView).GetProperty(pi.Name);
                if (vpi == null) continue;
                var actual = pi.GetValue(d);
                var expected = vpi.GetValue(v2);

                if (vpi.PropertyType == typeof(string))
                {
                    if (!object.Equals(expected, actual))
                        Console.WriteLine(
                            $"Property {pi.Name}: expected '{expected ?? "null"}', actual '{actual ?? "null"}'");
                    AreEqual(expected, actual);
                }
                else
                {
                    AreEqual(expected, actual);
                }
            }
        }
    }
}
