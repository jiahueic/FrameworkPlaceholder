using System.Web;
using System.Web.Optimization;

namespace AGTIV.Framework.MVC.UI.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = false;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryunobtrusiveajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/site.css",
                        "~/admin-lte/css/AdminLTE.css",
                        "~/admin-lte/css/skins/skin-blue.css",
                        "~/admin-lte/plugins/sweetalert2/sweetalert2.min.css"));

            bundles.Add(new ScriptBundle("~/admin-lte/js").Include(
                        "~/admin-lte/js/AdminLTE.js",
                        "~/admin-lte/plugins/fastclick/fastclick.js",
                        "~/admin-lte/plugins/sweetalert2/sweetalert2.all.min.js"));
        }
    }
}
