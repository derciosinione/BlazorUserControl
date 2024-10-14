namespace Application.Helpers;

public static class Utils
{
    public static string ToQueryString(object obj)
    {
        var properties = from p in obj.GetType().GetProperties()
            where p.GetValue(obj, null) != null
            select $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(obj, null)?.ToString() ?? string.Empty)}";

        return string.Join("&", properties);
    }
}