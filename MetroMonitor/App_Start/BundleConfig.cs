using System.Web;
using System.Web.Optimization;

namespace MetroMonitor
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/underscore").Include(
                    "~/Scripts/underscore.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/Scripts/angular.js"));

            bundles.Add(new ScriptBundle("~/bundles/angularControllers").Include(
                "~/Scripts/AngularControllers/Home.AngularModule.js",
                "~/Scripts/AngularControllers/Home.AngularController.js",
                "~/Scripts/AngularControllers/About.AngularController.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/MetroMonitor.css"));
        }
    }
}
