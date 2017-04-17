using System.Web;
using System.Web.Optimization;

namespace NextGenCMS.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            LoadJavaScripts(bundles);
            LoadStyleSheets(bundles);
        }

        private static void LoadStyleSheets(BundleCollection bundles)
        {
            LoadBootStarpStyle(bundles);
            LoadBootGloablStyle(bundles);
        }

        private static void LoadJavaScripts(BundleCollection bundles)
        {
            LoadJquery(bundles);
            LoadAngular(bundles);
            LoadBoostrap(bundles);
        }

        #region Javascripts
        private static void LoadJquery(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Jquery/js").Include(
                      "~/Scripts/Jquery/jquery-1.10.2.min.js"
                      ));
        }

        private static void LoadAngular(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/Angular/js").Include
             (
               "~/Scripts/Angular/angular.min.js",
              "~/Scripts/Angular/angular-resource.min.js",
              "~/Scripts/Angular/angular-ui-router.js",
                   "~/Scripts/Angular/app.js",
                  "~/Scripts/Angular/route.js"
             ));
        }

        private static void LoadBoostrap(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/bootstrap/js").Include
             (
               "~/Scripts/bootstrap/bootstrap.min.js"
             ));
        }

        #endregion
        #region StyleSheets
        private static void LoadBootStarpStyle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/bootstrap/css").Include
            (
              "~/css/bootstrap/bootstrap.min.css"
            ));
        }
        private static void LoadBootGloablStyle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/css/css").Include
            (
              "~/css/global.css"
            ));
        }
        #endregion
    }
}
