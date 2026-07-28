
namespace Facade;

public abstract class UserView : EntityView {
    internal const string sentenceEx = @"^[A-Z][a-zA-Z\s]*$";
    public string? Nick { get; set; }
    public string? Name { get; set; }


}