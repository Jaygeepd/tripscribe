using System.Diagnostics.CodeAnalysis;

namespace tripscribe.Api.Extensions;

[ExcludeFromCodeCoverage]
public static class StringEnvironmentExtensions
{
    public static string GetValue(this string name, string defaultValue = null)
    {
        return GetVariable(name) 
               ?? defaultValue
               ?? throw new ArgumentException($"There is no environment variable with name: {name}. Please check parameters");
    }
        
    public static T GetValue<T>(this string name) where T : IConvertible
    {
        var value = GetValue(name);
        return (T)Convert.ChangeType(value, typeof(T));
    }
        
    private static string GetVariable(this string name)
    {
        return Environment.GetEnvironmentVariable(name);
    }
}