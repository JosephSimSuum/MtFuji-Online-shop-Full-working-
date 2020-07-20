using System.Web;
using System.Web.Optimization;

namespace MtFuji
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/js/jquery-{version}.js"  ,
                        "~/js/bootstrap.min.js",
                        "~/Scripts/bootstrap.js",
                       "~/js/jquery-ui.min.js" ,
                        "~/js/jquery.nicescroll.min.js",
                        "~/js/jquery.slicknav.min.js",
                        "~/js/jquery.zoom.min.js",
                        "~/js/owl.carousel.min.js",
                        "~/js/main.js"
                
));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/css/bootstrap.css",
                      "~/css/site.css",
                      "~/css/bootstrap.min.css",
                     "~/css/PagedList.css" ,
                      "~/css/slicknav.min.css",
                      "~/css/flaticon.css",
                      "~/css/font-awesome.min.css",
                      "~/css/owl.carousel.min.css",
                     "~/css/animate.css",
                     "~/css/style.css" ,
                     "~/css/login1.css"
                      
                      ));
            BundleTable.EnableOptimizations = true;
        }
    }
}
