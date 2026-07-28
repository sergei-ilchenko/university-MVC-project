using Data;

namespace Facade;

public abstract class UserViewFactory<TData, TView> :
    AbstractViewFactory<TData, TView> where TData : EntityData<TData>, new() where TView : EntityView, new() { }