using Microsoft.AspNetCore.Http;

namespace Demo.Libs.MultiThemes
{
    public interface IThemeContextService
    {
        string GetCurrentTheme();
    }

    public class ThemeContextService : IThemeContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ThemeContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentTheme()
        {
            //todo read from 
            var allowedThemes = new[] {"", "a"};

            var context = _httpContextAccessor.HttpContext;
            if (context == null)
            {
                return string.Empty;
            }

            if (context.Items.TryGetValue("_theme", out var themeId))
            {
                return themeId == null ? string.Empty : themeId.ToString();
            }

            if (context.Request.Query.TryGetValue("theme", out var themeValues))
            {
                //NULL OR EMPTY
                if (string.IsNullOrWhiteSpace(themeValues[0]))
                {
                    context.Items["_theme"] = string.Empty;
                    return string.Empty;
                }

                context.Items["_theme"] = themeValues[0];
                return themeValues[0];
            }

            return string.Empty;
        }
    }
}
