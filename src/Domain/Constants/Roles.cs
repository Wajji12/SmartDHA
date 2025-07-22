namespace DHAFacilitationAPIs.Domain.Constants;

public abstract class Roles
{
    public const string Administrator = nameof(Administrator);
    public const string Tbo = nameof(Tbo);
    public static IEnumerable<string> GetRoles()
    {
        return new[] { Administrator, Tbo };
    }
}
