using System.Reflection;

namespace Tests;
public abstract class AssemblyTests(string namespaceName) : StaticTests {
    protected override Type? setType() => null;
    [TestMethod] public override void IsTested() {
        var testAssembly = Assembly.GetExecutingAssembly();
        var testClasses = testAssembly
            .GetTypes()
            .Where(t => (t?.Namespace is not null))
            .Select(t => t.Name)
            .ToArray();

        var domain = AppDomain.CurrentDomain;
        var assemblies = domain.GetAssemblies();
        var assembly = assemblies
            .FirstOrDefault(a => (a?.FullName is not null) && a.FullName.StartsWith(namespaceName));
        if (assembly == null) NotTested($"Assembly {namespaceName} not found.");

        var classes = assembly?.GetTypes()
            .Where(t => !t.IsInterface && t.IsPublic)
            .Select(t => t.Name)
            .Select(t => {
                var i = t.IndexOf('`');
                return i > 0 ? t.Substring(0, i) : t;
            })
            .Distinct()
            .Where(t => !testClasses.Contains(t + "Tests")).ToArray();

        if (classes?.Length == 0) return;
        var notTestedClasses = string.Join(", ", classes ?? []);
        if (classes?.Length == 1)
            NotTested($"Test class for <{notTestedClasses}> not found.");
        NotTested($"Test classes for <{notTestedClasses}> not found.");
    }
}