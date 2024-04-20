namespace Edgy.Core.Extensions;

public static class IHtmlContentExtensions
{
    public static string GetString(this IHtmlContent content)
    {
        if (content != null)
        {
            using (var writer = new System.IO.StringWriter())
            {
                content.WriteTo(writer, HtmlEncoder.Default);
                return writer.ToString();
            }
        }
        return "";
    }
}
