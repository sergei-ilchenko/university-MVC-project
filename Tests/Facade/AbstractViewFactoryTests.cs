using Data;
using Facade;

namespace Tests.Facade;

[TestClass]
public class AbstractViewFactoryTests
    : AbstractTests<AbstractViewFactory<TourNData, TourNView>, object> {
    protected override AbstractViewFactory<TourNData, TourNView> CreateObject()
        => new TourNViewFactory();
}