using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEF.Shared
{
    public static class Constant
    {
        public static class DocumentTypes
        {
            #region Global
            public static readonly string Site = "site";
            #endregion

            #region Page Alias
            public static readonly string Home = "home";
            #endregion

            #region Folder Alias
            public static readonly string Datasources = "datasources";
            public static readonly string PageComponentsFolder = "pageComponentsFolder";
            public static readonly string ComponentsFolder = "componentsFolder";
            public static readonly string Settings = "settings";
            public static readonly string SiteComponentsFolder = "siteComponentsFolder";
            #endregion

            #region Composition Alias
            public static readonly string SeoTag = "seoTag";
            #endregion

            #region Settings
            public static readonly string Configuration = "configuration";
            #endregion
        }
    }
}
