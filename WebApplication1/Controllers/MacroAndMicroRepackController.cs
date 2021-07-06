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
    public class MacroAndMicroRepackController : ApiController
    {
        // GET: MacroAndMicro
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public HttpResponseMessage Get()
        {




            string query = @"   
        SELECT * from rdf_repackin_entry
";
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

        public HttpResponseMessage Get(string dateadded)
        {




            string query = @"


    SELECT [repack_id]
          ,[rp_item_id]
          ,[rp_item_category]
          ,[rp_item_code]
          ,[rp_supplier]
          ,[rp_item_description]
          ,[rp_classification]
          ,[rp_quantity]
          ,[rp_receiving_date]
          ,[rp_receiving_details]
          ,[is_active]
          ,[added_by]
          ,[days_to_expired]
          ,[uniquedate]
          ,[totalnstock]
          ,[actual_count_good]
          ,[actual_count_reject]
          ,[tracking_po_sum_id]
          ,[oracle_qty_uom]
          ,[selected_uom]
          ,[rp_qty_delivered]
          ,[rp_mfg_date]
          ,[rp_expiry_date]
          ,[rp_qty_ordered]
          ,[rp_QA_by]
          ,[rp_expected]
          ,[rp_missing]
          ,[rp_actual_receiving]
          ,[QA_rgood]
          ,[QA_reject]
          ,[rp_receiving_by]
          ,[rp_total_delivered]
          ,[rp_waiting_delivered]
          ,[is_selected]
          ,[total_repack]
          ,[repack_by]
          ,[raw_rp_available]
          ,[rp_balance]
          ,[rp_qtyneeded]
          ,[rp_qtyshared]
          ,[rp_posumid]
          ,[raw_rp_remaining]
          ,[rp_batch]
          ,[rp_feed_code]
          ,[deb_vendor_name]
          ,[deb_received_id]
          ,[deb_item_code]
          ,[deb_dof_received]
          ,[deb_xpdays]
          ,[deb_total_qty]
          ,[deb_total_shared]
          ,[deb_actual_remaining]
          ,[string_id]
          ,[prod_id_repack]
          ,[macrocount]
          ,[is_prepared]
          ,[printing_count]
          ,[production_date]
          ,CAST(repacking_date_time as date) as dateadded
          ,[uom]
      FROM dbo.rdf_repackin_entry
    where CAST(repacking_date_time as date)=" + dateadded + "";
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
    }
}