using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Reflection;
using Random = Aids.Random;

namespace Tests;
public abstract class StaticTests : BaseTests {
    protected Type? type;
    protected abstract Type? setType();
    [TestInitialize] public virtual void Initialize() => type = setType();
    [TestCleanup] public virtual void Cleanup() => type = null;
    [TestMethod] public virtual void IsTested()
    {
        var testMethods = GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<TestMethodAttribute>() != null)
            .Select(m => m.Name)
            .ToArray();
        IsNotNull(type);
        var members = type!
            .GetMembers(BindingFlags.Public 
                | BindingFlags.Instance 
                | BindingFlags.Static 
                | BindingFlags.DeclaredOnly)
            .Select(m => m.Name)
            .Where(m => !m.Contains("get_") && !m.Contains("set_") && !m.Contains(".ctor"))
            .Where(m => !testMethods.Contains(m + "Test"))
            .ToArray();
        if (members.Length == 0) return;
        var notTestedMembers = string.Join(", ", members);
        if (members.Length == 1) NotTested($"Test method for <{notTestedMembers}> not found.");
        NotTested($"Test methods for <{notTestedMembers}> not found.");
    }
    protected void IsProperty<T>(bool isReadOnly = false)
    {
        var pi = GetPropertyInfo();
        IsNotNull(pi);
        IsType<T>(pi!);
        CanRead(pi!);
        if (!isReadOnly) CanWrite(pi!);
        if (isReadOnly) return;
        var v = Random.Type<T>();
        CanSet(pi!, v);
        CanGet(pi!, v);
    }
    protected void IsReadOnly<T>(T? value)
    {
        IsProperty<T>(true);
        var pi = GetPropertyInfo();
        CanGet(pi!, value);
    }
    protected void IsProperty<T>(string? displayName, string regExpr, bool isReadOnly = false)
    {
        IsProperty<T>(displayName, (DataType?)null, isReadOnly);
        var pi = GetPropertyInfo();
        IsRegularExpr(pi, regExpr);
    }
    private void IsRegularExpr(PropertyInfo? pi, string regExpr)
    {
        var actual = pi?.GetCustomAttribute<RegularExpressionAttribute>()?.Pattern;
        AreEqual(regExpr, actual);
    }
    protected void IsProperty<T>(string? displayName, DataType? dataType = null, bool isReadOnly = false)
    {
        IsProperty<T>(isReadOnly);
        var pi = GetPropertyInfo();
        IsDisplayName(pi, displayName);
        IsDataType(pi, dataType);
    }
    private void IsDataType(PropertyInfo? pi, DataType? dataType)
    {
        var actual = pi?.GetCustomAttribute<DataTypeAttribute>()?.DataType;
        AreEqual(dataType, actual);
    }
    private void IsDisplayName(PropertyInfo? pi, string? displayName)
    {
        var actual = pi?.GetCustomAttribute<DisplayAttribute>()?.Name;
        AreEqual(actual, displayName);
    }
    protected PropertyInfo? GetPropertyInfo()
    {
        var n = GetPropertyName();
        if (n is null) return null;
        return type?.GetProperty(n);
    }
    protected static string? GetPropertyName()
    {
        var stack = new StackTrace();
        for (var i = 1; i < stack.FrameCount; i++)
        {
            var m = stack.GetFrame(i)?.GetMethod();
            if (m is null) continue;
            var isTest = m.GetCustomAttributes(typeof(TestMethodAttribute), true).Any();
            if (isTest) return m.Name.Replace("Test", string.Empty);
        }

        return null;
    }
    private void IsType<T>(PropertyInfo pi)
    {
        if (pi!.PropertyType?.Name != typeof(T).Name)
            Fail($"Property <{pi.Name}> is not a type of <{typeof(T).Name}>.");
    }
    protected void CanRead(PropertyInfo pi) => IsTrue(pi.CanRead, $"Property '{pi.Name}' does not have a getter.");
    protected void CanWrite(PropertyInfo pi) => IsTrue(pi.CanWrite, $"Property '{pi.Name}' does not have a setter.");
    protected virtual void CanSet<T>(PropertyInfo pi, T? v) => Fail();
    protected virtual void CanGet<T>(PropertyInfo pi, T? v) => Fail();
}