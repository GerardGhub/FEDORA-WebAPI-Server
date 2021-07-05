using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductionPlanController : ApiController
    {
 
        public HttpResponseMessage Get(string dateadded)
        {
            //dating = "167778635";
        //    SELECT emp.p_feed_code,emp.bags_int,emp.batch_int,emp.proddate,
        //emp.feed_type,emp.bagorbin,additional_bin,emp.series_num,emp.prod_id
        //FROM rdf_production_advance emp WHERE emp.is_selected = '1'  AND NOT emp.canceltheapprove IS NOT NULL
        //ORDER BY emp.prod_id ASC


            string query = @"
SELECT prod_id,p_feed_code,p_bags,p_nobatch,proddate, CAST(dateadded as date) as dateadded,
is_active
      ,prod_id
      ,repacking_status
      ,iscancel
      ,iscancelreason
      ,iscancelapprover
      ,iscancelapproverbit
      ,canceltheapprove
      ,feed_type
      ,approved_date
      ,approved_cancel_date
      ,planner_cancel_date
      ,approver_cancelrequest_date
      ,preparation_date_finish
      ,BMX_Status
      ,macro_prep_status
      ,macro_prep_status_date
      ,no_batch_in_production
      ,finish_production_date
      ,finish_production_by
      ,BMX_finish_date
      ,bags_printing
      ,fg_date_finish
      ,bags_int
      ,batch_int
      ,GetmyDate
      ,micro_mixing_automation
      ,series_num
      ,bagorbin
      ,start_micro_repacking
      ,start_macro_repacking
      ,end_micro_repacking
      ,end_macro_repacking
      ,mixer_capability
      ,total_reprocess_count
      ,start_of_basemixed
      ,microbasemixed_ending
      ,aproved_date_time
      ,plannerAndapprover
      ,micro_repacking_time
      ,macro_repacking_time
      ,basemixed_time
      ,start_of_production
      ,end_of_production
      ,production_time
      ,start_fg_processs
      ,end_fg_processs
      ,fg_time
      ,total_good_count
      ,total_reject_count
      ,days_of_production
      ,bmx_variance_percentage
      ,total_hours
      ,micro_bit
      ,macro_bit
      ,production_bit
      ,fg_bit
      ,total_outright_count
      ,move_order_qty
      ,actual_available
      ,last_qty_input
      ,move_status
      ,additional_bin
      ,corn_type
      ,fg_by
      ,theo_batch_macro
      ,theo_batch_liquid
      ,theo_batch_macro_status
      ,ra_feed_selection
      ,cancelProdPlanInProduction
      ,cancelProdPlanInProductionRemarks
      ,planning_by
      ,cancelProdPlaninProductionCancelBy
      ,tag_feed_type
      ,tag_prodid
      ,[tag_by]
      ,tag_status
FROM dbo.rdf_production_advance
where CAST(dateadded as date)=" + dateadded+"";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);



        }


        public HttpResponseMessage Get()
        {
            //dating = "167778635";
            //    SELECT emp.p_feed_code,emp.bags_int,emp.batch_int,emp.proddate,
            //emp.feed_type,emp.bagorbin,additional_bin,emp.series_num,emp.prod_id
            //FROM rdf_production_advance emp WHERE emp.is_selected = '1'  AND NOT emp.canceltheapprove IS NOT NULL
            //ORDER BY emp.prod_id ASC


            string query = @"
SELECT * FROM rdf_production_advance";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["EmployeeAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);



        }
        //// GET: ProductionPlan
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}