using System.Collections.Generic;

namespace Demo.Libs.MultiThemes
{
    public interface IKnownThemeContext
    {
        IList<string> GetKnownThemes();
    }

    public class KnownThemeContext : IKnownThemeContext
    {
        public IList<string> GetKnownThemes()
        {
            //todo:read from config
            return new[] {"", "a"};
        }
    }
}