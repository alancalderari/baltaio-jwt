using System.Security.Claims;

namespace baltaio_jwt.Extensions;

public static class ClaimTypeExtension
{
    public static int Id(this ClaimsPrincipal claimsPrincipal)
    {
        try
        {
            var id = claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "Id")?.Value ?? "0";
            return int.Parse(id);
        }
        catch
        {
            return 0;
        }
    }
    public static string Name(this ClaimsPrincipal claimsPrincipal)
    {
        try
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "";
        }
        catch
        {
            return string.Empty;
        }
    }
    public static string Email(this ClaimsPrincipal claimsPrincipal)
    {
        try
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";
        }
        catch
        {
            return string.Empty;
        }
    }
    public static string Image(this ClaimsPrincipal claimsPrincipal)
    {
        try
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == "image")?.Value ?? "";
        }
        catch
        {
            return string.Empty;
        }
    }
    public static string GivenName(this ClaimsPrincipal claimsPrincipal)
    {
        try
        {
            return claimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? "";
        }
        catch
        {
            return string.Empty;
        }
    }
}