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
        public HttpResponseMessage Get()
        {

        //    SELECT emp.p_feed_code,emp.bags_int,emp.batch_int,emp.proddate,
        //emp.feed_type,emp.bagorbin,additional_bin,emp.series_num,emp.prod_id
        //FROM rdf_production_advance emp WHERE emp.is_selected = '1'  AND NOT emp.canceltheapprove IS NOT NULL
        //ORDER BY emp.prod_id ASC


            string query = @"
                   SELECT * FROM rdf_production_advance

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
        //// GET: ProductionPlan
        //public ActionResult Index()
        //{
        //    return View();
        //}
    }
}