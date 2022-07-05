using System.Globalization;
using System.Threading;
using System.Web;

namespace DEF.Shared
{
    public static class LanguageInfo
    {
        /// <summary>
        /// Returns the Current Language
        /// </summary>
        /// <returns></returns>
        public static string CurrentCulture => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        /// <summary>
        /// Returns the current CultureInfo
        /// </summary>
        public static CultureInfo CultureInfo => Thread.CurrentThread.CurrentCulture;

        /// <summary>
        /// Returns the language Switcher inverse culture based url
        /// </summary>
        /// <returns>string</returns>
        public static string LanguageSwitcher()
        {
            string ctxUrl = HttpContext.Current.Request.RawUrl;

            string culture = string.Empty;

            if (CurrentCulture == "en")
                culture = "ar";
            else if (CurrentCulture == "ar")
                culture = "en";

            if (ctxUrl.Contains("/ar/") || ctxUrl.Contains("/ar"))
            {
                if (ctxUrl.Contains("/ar/"))
                    ctxUrl = ctxUrl.Replace("/ar/", "/" + culture + "/");
                else
                    ctxUrl = ctxUrl.Replace("/ar", "/" + culture);
            }
            else if (ctxUrl.Contains("/en/") || ctxUrl.Contains("/en"))
            {
                if (ctxUrl.Contains("/en/"))
                    ctxUrl = ctxUrl.Replace("/en/", "/" + culture + "/");
                else
                    ctxUrl = ctxUrl.Replace("/en", "/" + culture);
            }
            else
            {
                ctxUrl = string.Concat("/", culture, ctxUrl);
            }

            return ctxUrl.ToLower().TrimEnd('/');
        }
        public static string LanguageSwitcher(string pageUrl)
        {
            string ctxUrl = pageUrl;

            string culture = string.Empty;

            if (CurrentCulture == "en")
                culture = "ar";
            else if (CurrentCulture == "ar")
                culture = "en";


            if (ctxUrl.Contains("/ar/") || ctxUrl.Contains("/ar"))
            {
                if (ctxUrl.Contains("/ar/"))
                    ctxUrl = ctxUrl.Replace("/ar/", "/" + culture + "/");
                else
                    ctxUrl = ctxUrl.Replace("/ar", "/" + culture);
            }
            else if (ctxUrl.Contains("/en/") || ctxUrl.Contains("/en"))
            {
                if (ctxUrl.Contains("/en/"))
                    ctxUrl = ctxUrl.Replace("/en/", "/" + culture + "/");
                else
                    ctxUrl = ctxUrl.Replace("/en", "/" + culture);
            }
            else
            {
                ctxUrl = string.Concat("/", culture, ctxUrl);
            }

            return ctxUrl.ToLower().TrimEnd('/');
        }
    }

}
