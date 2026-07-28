using System.ComponentModel;
using System.Reflection;

namespace Tests;
public abstract class SealedTests<TClass, TBaseClass> : ClassTests<TClass, TBaseClass>
    where TClass : class, new()
    where TBaseClass : class {
    [TestMethod] public void IsSealedTest() => IsTrue(typeof(TClass).IsSealed);
    [TestMethod] public virtual void DisplayNameTest() => IsDisplayName();
    protected void IsDisplayName(string? displayName = null)
    {
        var actual = type?.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;
        AreEqual(displayName, actual);
    }
}