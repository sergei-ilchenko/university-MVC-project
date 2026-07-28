using System.Reflection;

namespace Tests;
public abstract class EnumTests<TEnum>(int count) : StaticTests where TEnum : Enum {
    protected override Type? setType() => typeof(TEnum);
    protected string[] memberNames { get; set; } = getNames();
    private static string[] getNames()
    {
        var t = typeof(TEnum);
        var fields = t.GetFields(BindingFlags.Public | BindingFlags.Static);
        var names = fields.Select(f => f.Name);
        var result = names.ToArray();
        return result;
    }
    [TestMethod] public void CountTest() => AreEqual(memberNames.Length, count);
    [TestMethod] public override void IsTested()
    {
        var testMethods = GetType()
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetCustomAttribute<TestMethodAttribute>() != null)
            .Select(m => m.Name).ToArray();
        IsNotNull(type);
        var members = memberNames
            .Where(name => !testMethods.Contains(name + "Test"))
            .ToArray();
        if (members.Length == 0) return;
        var notTestedMembers = string.Join(", ", members);
        if (members.Length == 1)
            NotTested($"Test method for <{notTestedMembers}> not found.");
        NotTested($"Test methods for <{notTestedMembers}> not found.");
    }
    protected void IsEnum(int value)
    {
        var name = GetPropertyName();
        IsNotNull(name);
        var actual = Enum.Parse(typeof(TEnum), name!);
        AreEqual((int)actual, value);
    }
}