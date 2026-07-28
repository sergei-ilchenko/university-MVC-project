using System.Reflection;

namespace Tests;
public abstract class BaseClassTests<TClass, TBaseClass> : StaticTests
    where TClass : class
    where TBaseClass : class {
    protected TClass? obj;
    protected abstract TClass CreateObject();
    protected override Type setType() => typeof(TClass);
    [TestInitialize] public virtual void Initialize()
    {
        base.Initialize();
        obj = CreateObject();
    }
    [TestCleanup] public virtual void Cleanup()
    {
        base.Cleanup();
        obj = null;
    }
    [TestMethod] public void CanCreateTest() => IsNotNull(obj);
    [TestMethod] public void IsTypeTest() => IsOfType(obj, typeof(TClass));
    [TestMethod] public virtual void IsBaseTypeTest() => AreEqual(typeof(TClass).BaseType, typeof(TBaseClass));

    protected override void CanGet<T>(PropertyInfo pi, T? expected) where T : default
    {
        var actual = pi.GetValue(obj);
        AreEqual(actual, expected);
    }
    protected override void CanSet<T>(PropertyInfo pi, T? v) where T : default
        => pi.SetValue(obj, v);
}