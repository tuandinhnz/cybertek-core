namespace Cybertek.Validation;

public static class Validate
{
    #region IsNotNull

    private const string IsNotNullMessage = "Parameter is null";

    public static void IsNotNull<T>(T parameter,
        string? name = null,
        string? message = null)
    {
        if (parameter == null)
        {
            throw new ArgumentNullException(name, message ?? IsNotNullMessage);
        }
    }

    #endregion IsNotNull

    #region IsNull

    private const string IsNullMessage = "Parameter is not null";

    public static void IsNull<T>(T parameter,
        string? name = null,
        string? message = null)
    {
        if (parameter != null)
        {
            throw new ArgumentException(message ?? IsNullMessage, name);
        }
    }

    #endregion

    #region IsNotDefault

    private const string IsNotDefaultMessage = "Parameter is default";

    public static void IsNotDefault<T>(T parameter,
        string? name = null,
        string? message = null)
    {
        if (EqualityComparer<T>.Default.Equals(parameter, default))
        {
            throw new ArgumentException(message ?? IsNotDefaultMessage, name);
        }
    }

    #endregion

    #region IsDefault

    private const string IsDefaultMessage = "Parameter is not default";

    public static void IsDefault<T>(T parameter,
        string? name = null,
        string? message = null)
    {
        if (!EqualityComparer<T>.Default.Equals(parameter, default))
        {
            throw new ArgumentException(message ?? IsDefaultMessage, name);
        }
    }

    #endregion

    #region IsNotNullOrDefault

    private const string IsNotNullOrDefaultMessage = "Parameter is null or default";

    public static void IsNotNullOrDefault<T>(T parameter,
        string? name = null,
        string? message = null)
    {
        if (parameter == null || EqualityComparer<T>.Default.Equals(parameter, default))
        {
            throw new ArgumentException(message ?? IsNotNullOrDefaultMessage, name);
        }
    }

    #endregion

    #region IsNullOrDefault

    private const string IsNullOrDefaultMessage = "Parameter is not null or default";

    public static void IsNullOrDefault<T>(T parameter,
        string? name = null,
        string? message = null)
    {
        if (parameter != null && !EqualityComparer<T>.Default.Equals(parameter, default))
        {
            throw new ArgumentException(message ?? IsNullOrDefaultMessage, name);
        }
    }

    #endregion

    #region IsNotEmpty

    private const string IsNotEmptyMessage = "Parameter is empty";

    public static void IsNotEmpty(string parameter,
        string? name = null,
        string? message = null)
    {
        if (parameter != null && string.Equals(parameter, string.Empty, StringComparison.Ordinal))
        {
            throw new ArgumentException(message ?? IsNotEmptyMessage, name);
        }
    }

    #endregion

    #region IsEmpty

    private const string IsEmptyMessage = "Parameter is not empty";

    public static void IsEmpty(string parameter,
        string? name = null,
        string? message = null)
    {
        if (!string.Equals(parameter, string.Empty, StringComparison.Ordinal))
        {
            throw new ArgumentException(message ?? IsEmptyMessage, name);
        }
    }

    #endregion

    #region IsNotNullOrEmpty

    private const string IsNotNullOrEmptyMessage = "Parameter is null or empty";

    public static void IsNotNullOrEmpty(string parameter,
        string? name = null,
        string? message = null)
    {
        if (string.IsNullOrEmpty(parameter))
        {
            throw new ArgumentException(message ?? IsNotNullOrEmptyMessage, name);
        }
    }

    #endregion

    #region IsNullOrEmpty

    private const string IsNullOrEmptyMessage = "Parameter is not null or empty";

    public static void IsNullOrEmpty(string parameter,
        string? name = null,
        string? message = null)
    {
        if (!string.IsNullOrEmpty(parameter))
        {
            throw new ArgumentException(message ?? IsNullOrEmptyMessage, name);
        }
    }
    
    #endregion

    #region IsNotNullOrWhiteSpace

    private const string IsNotNullOrWhiteSpaceMessage = "Parameter is null, empty or white space";

    public static void IsNotNullOrWhiteSpace(string parameter,
        string? name = null,
        string? message = null)
    {
        if (string.IsNullOrWhiteSpace(parameter))
        {
            throw new ArgumentException(message ?? IsNotNullOrWhiteSpaceMessage, name);
        }
    }

    #endregion

    #region IsNullOrWhiteSpace

    private const string IsNullOrWhiteSpaceMessage = "Parameter is not null, empty or white space";

    public static void IsNullOrWhiteSpace(string parameter,
        string? name = null,
        string? message = null)
    {
        if (!string.IsNullOrWhiteSpace(parameter))
        {
            throw new ArgumentException(message ?? IsNullOrWhiteSpaceMessage, name);
        }
    }

    #endregion
}
