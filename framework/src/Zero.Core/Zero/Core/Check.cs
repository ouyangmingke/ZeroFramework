using JetBrains.Annotations;

using System.Diagnostics;

namespace Zero.Core;

[DebuggerStepThrough]
public static class Check
{
    [ContractAnnotation("value:null => halt")]
    public static T NotNull<T>(
    T value,
    [InvokerParameterName][NotNull] string parameterName)
    {
        if (value == null)
        {
            throw new ArgumentNullException(parameterName);
        }

        return value;
    }

    public static string NotNullOrWhiteSpace(
     string value,
     [InvokerParameterName][NotNull] string parameterName,
     int maxLength = int.MaxValue,
     int minLength = 0)
    {
        if (value.IsNullOrWhiteSpace())
        {
            throw new ArgumentException($"{parameterName} can not be null, empty or white space!", parameterName);
        }

        if (value.Length > maxLength)
        {
            throw new ArgumentException($"{parameterName} length must be equal to or lower than {maxLength}!", parameterName);
        }

        if (minLength > 0 && value.Length < minLength)
        {
            throw new ArgumentException($"{parameterName} length must be equal to or bigger than {minLength}!", parameterName);
        }

        return value;
    }

    /// <summary>
    /// indicates whether this string is null, empty, or consists only of white-space characters.
    /// </summary>
    [ContractAnnotation("str:null => true")]
    public static bool IsNullOrWhiteSpace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }
}
