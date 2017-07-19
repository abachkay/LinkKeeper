using System.Web.Optimization;

namespace LinkKeeper.API
{
    public class BundleConfig
    {        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/appScripts").Include(
                        "~/Scripts/app/linkKeeper.module.js",
                        "~/Scripts/app/links/welcome.controller.js",
                        "~/Scripts/app/linkKeeper.config.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/libs/angular/angular.min.js",
                        "~/Scripts/libs/angular/angular-route.min.js"));   

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/libs/jquery/jquery-3.1.1.min.js"));   
            
            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                      "~/Scripts/libs/materialize/materialize.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                      "~/Content/materialize/css/materialize.css",
                      "~/Content/site.css"));
        }
    }
}
