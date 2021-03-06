﻿using System.Web;
using System.Web.Optimization;

namespace Argos.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
          
            bundles.Add(new ScriptBundle("~/bundles/Jquery").Include(
                 "~/Vendor/jquery/dist/jquery.min.js")); 

                    
            //necesarios para el template
            bundles.Add(new ScriptBundle("~/bundles/Scripts").Include(
                       "~/Scripts/bootstrap.min.js",
                       "~/Scripts/_jquery-ui-1.12.1.min.js",
                       "~/Scripts/jquery.maskedinput.min.js",
                       "~/Scripts/fastclick.js",
                       "~/Scripts/nprogress.min.js",
                       "~/Vendor/gauge.js/dist/gauge.min.js",
                       "~/Vendor/bootstrap-progressbar/bootstrap-progressbar.min.js",
                       "~/Vendor/iCheck/icheck.min.js",
                       "~/Vendor/validator/validator.js",
                       "~/Vendor/devbridge-autocomplete/dist/jquery.autocomplete.min.js",
                       "~/Vendor/pnotify/dist/pnotify.js",
                       "~/Vendor/pnotify/dist/pnotify.buttons.js",
                       "~/Vendor/pnotify/dist/pnotify.nonblock.js",

                       "~/Vendor/moment/min/moment.min.js",
                       "~/Vendor/moment/min/locales.min.js",
                       "~/Vendor/moment/locale/es.js",
                       "~/Vendor/bootstrap-datetimepicker/build/js/bootstrap-datetimepicker.min.js",
                       
                       "~/Libs/Gentelella/js/custom.js"));

            //necesarios para el template
            bundles.Add(new ScriptBundle("~/bundles/Charts").Include(
                      "~/Vendor/Chart.js/dist/Chart.min.js",
                      "~/Vendor/Flot/jquery.flot.js",
                      "~/Vendor/Flot/jquery.flot.pie.js",
                      "~/Vendor/Flot/jquery.flot.stack.js",
                      "~/Vendor/Flot/jquery.flot.time.js",
                      "~/Vendor/Flot/jquery.flot.resize.js",
                      "~/Vendor/flot.orderbars/js/jquery.flot.orderBars.js",
                      "~/Vendor/flot-spline/js/jquery.flot.spline.min.js",
                      "~/Vendor/flot.curvedlines/curvedLines.js"));

            //necesarios para el template
            bundles.Add(new ScriptBundle("~/bundles/Maps").Include(
                        "~/Vendors/jqvmap/dist/jquery.vmap.js",
                        "~/Vendors/jqvmap/dist/maps/jquery.vmap.world.js",
                        "~/Vendors/jqvmap/examples/js/jquery.vmap.sampledata.js",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/DataTables/jquery.dataTables.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //estos son los viejitos
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/respond.js"));

            //estos los uso con el template
            bundles.Add(new StyleBundle("~/bundles/Styles").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/nprogress.css",
                      "~/Vendor/iCheck/skins/flat/green.css",
                      "~/Vendor/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css",
                      "~/Vendor/jqvmap/dist/jqvmap.min.css",
                      
                      "~/Vendor/bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.min.css",
                      "~/Vendor/pnotify/dist/pnotify.css",
                      "~/Vendor/pnotify/dist/pnotify.buttons.css",
                      "~/Vendor/pnotify/dist/pnotify.nonblock.css",
                      "~/Libs/Gentelella/css/custom.min.css",
                      "~/Content/Overrides.css"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                       "~/AppScripts/Global.js",
                       "~/AppScripts/Catalog.js",
                       "~/AppScripts/Operative.js"));
         

            //DATA TABLE STYLE
            bundles.Add(new StyleBundle("~/bundles/DataTableStyles").Include(
                      //"~/Vendor/datatables.net/css/jquery.dataTables.min.css",
                      "~/Vendor/datatables.net-bs/css/dataTables.bootstrap.min.css",
                      "~/Vendor/datatables.net-buttons-bs/css/buttons.bootstrap.min.css",
                      "~/Vendor/datatables.net-fixedheader-bs/css/fixedHeader.bootstrap.min.css",
                      "~/Vendor/datatables.net-fixedcolumns/css/fixedColumns.dataTables.min.css",
                      "~/Vendor/datatables.net-responsive-bs/css/responsive.bootstrap.min.css",
                      "~/Vendor/datatables.net-scroller-bs/css/scroller.bootstrap.min.css"));

            //DATA TABLE SCRIPTS
            bundles.Add(new ScriptBundle("~/bundles/DataTableScripts").Include(
            "~/Vendor/datatables.net/js/jquery.dataTables.min.js",
            "~/Vendor/datatables.net-bs/js/dataTables.bootstrap.min.js",
             "~/Vendor/datatables.net-fixedheader/js/dataTables.fixedHeader.min.js",
            "~/Vendor/datatables.net-fixedcolumns/js/dataTables.fixedColumns.min.js",
            "~/Vendor/pdfmake/build/pdfmake.min.js",
            "~/Vendor/pdfmake/build/vfs_fonts.js",
            "~/Vendor/jszip/dist/jszip.min.js",
            "~/Vendor/datatables.net-buttons/js/dataTables.buttons.min.js",
            "~/Vendor/datatables.net-buttons-bs/js/buttons.bootstrap.min.js",
            "~/Vendor/datatables.net-buttons/js/buttons.flash.min.js",
            "~/Vendor/datatables.net-buttons/js/buttons.html5.min.js",
            "~/Vendor/datatables.net-buttons/js/buttons.colVis.min.js",
            "~/Vendor/datatables.net-buttons/js/buttons.print.min.js",
            "~/Vendor/datatables.net-keytable/js/dataTables.keyTable.min.js",
            "~/Vendor/datatables.net-responsive/js/dataTables.responsive.min.js",
            "~/Vendor/datatables.net-responsive-bs/js/responsive.bootstrap.js",
            "~/Vendor/datatables.net-scroller/js/dataTables.scroller.min.js"));


        }

    }
}
