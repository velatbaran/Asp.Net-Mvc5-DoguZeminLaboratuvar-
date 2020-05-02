using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace DoguZemin.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            //bundles.Add(new ScriptBundle("~/bundles/js").Include(
            //  "~/Content/AdminTemplate/js/bootstrap.js",
            //  "~/Content/AdminTemplate/js/scripts.js",
            //  "~/Content/AdminTemplate/js/jquery.nicescroll.js",
            //  "~/Content/AdminTemplate/js/classie.js"));

            //bundles.Add(new ScriptBundle("~/bundles-js-modern/js").Include(
            //                    "~/Scripts/jquery-3.0.0.min.js",
            //    "~/Content/AdminTemplate/js/modernizr.custom.js"
            //    ));

            //bundles.Add(new ScriptBundle("~/bundles-custom/js").Include(
            //    "~/Content/AdminTemplate/js/underscore-min.js",
            //    "~/Content/AdminTemplate/js/moment-2.2.1.js",
            //    "~/Content/AdminTemplate/js/clndr.js",
            //    "~/Content/AdminTemplate/js/site.js",
            //    "~/Content/AdminTemplate/js/metisMenu.min.js",
            //    "~/Content/AdminTemplate/js/custom.js",
            //    "~/Content/ckeditor/ckeditor.js"
            //    ));

            //bundles.Add(new StyleBundle("~/Content-custom/css").Include(
            // "~/Content/AdminTemplate/css/clndr.css",
            // "~/Content/AdminTemplate/css/custom.css",
            // "~/Content/AdminTemplate/css/bootstrap.css",
            // "~/Content/AdminTemplate/css/style.css",
            // "~/Content/AdminTemplate/css/font-awesome.css",
            // "~/Content/AdminTemplate/css/animate.css"
            // ));
        }


    }
}