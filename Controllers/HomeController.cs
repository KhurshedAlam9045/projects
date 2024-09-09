using Antlr.Runtime.Misc;
using MyProject1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace MyProject1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {

            return View();
        }
        public ActionResult Adminlogin()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Adminlogin( string adminid,string password)
        {
            string query = "select * from tbl_adminlogin where adminid='"+adminid+"' and password='"+password+"' ";
            DBManager db = new DBManager();
            DataTable dt= db.executeSelect(query);

            if (dt.Rows.Count > 0)
            {
                Session["admin"] = adminid;
                return RedirectToAction("index","admin");
            }
            else
            {

                return Content("<script>alert('Id or Password is invalid');location.href='/home/AdminLogin'</script>");
            }
            
        }



        public ActionResult SingUp()
        {
            DBManager db = new DBManager();
            DataTable dt = db.executeSelect("select bid,bname from tbl_batch");
            ViewBag.batches = dt;
            DataTable Singup = db.executeSelect("select * from tbl_student order by name asc");
            ViewBag.SingUp = dt;
            return View();
        }
        [HttpPost]
        public ActionResult SingUp(string name, string email, string password, long mobno, string gender, string college, string course,int batch)
        {

            DBManager dm = new DBManager();
            int x = dm.executeInsertUpdateDelete("insert into tbl_student values('" + name + "','" + email + "','" + password + "'," + mobno + ",'" + gender + "','" + college + "','" + course + "',"+batch+",'" +DateTime.Now + "')");
            if (x > 0)
            {
                return Content("<script>alert('Thanks for Register with us...');location.href='/home/signUp'</script>");
            }

            else
            {
                return Content("<script>alert('Data not Register....');location.href='/home/signUp'</script>");
            }
            
        }
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(string name,long mobno,string email,string message)
        {
            DBManager dm = new DBManager();
        int x=dm.executeInsertUpdateDelete("insert into tbl_contact values('"+name+"',"+mobno+",'"+email+"','"+message+"','"+DateTime.Now+"')");
            if (x > 0)
            {
                Response.Write("<script>alert('Thanks for contacting with us...')</script>");
            }

            else { 
                Response.Write("<script>alert('Data not saved....')</script>");
            }
            return View();
        }
        public ActionResult SingIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SingIn(string name,HttpPostedFileBase imagefile,string college,string message)
        {
            DBManager dm = new DBManager();
            int x = dm.executeInsertUpdateDelete("insert into tbl_feedback values('"+name+"','"+imagefile.FileName+"','"+college+"','"+ message+ "')");
            if (x > 0)
            {

                return Content("<script>alert('Successfully Your  Feadback...');location.href='/home/SignIn'</script>");
            }
            else
            {
                return Content("<script>alert('Not Successfulley Your Feadback...');location.href='/home/SignIn'</script>");
            }


           
        }
        public ActionResult StdudentLogin()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult StdudentLogin(string userid,string password)
        {
            string query = "select * from tbl_student where email='" + userid + "' and password='" + password +"'";
            DBManager db = new DBManager();
            DataTable dt = db.executeSelect(query);

            if (dt.Rows.Count > 0)
            {
                Session["semail"] = dt.Rows[0][1];
                Session["sname"] = dt.Rows[0][0];
                Session["sbatch"] = dt.Rows[0][7];
                return RedirectToAction("index", "Student");
            }
            else{
                return Content("<script>alert('Invalid id or password');location.href='/Home/StdudentLogin'</script>");

            }

        }

        // Jion To View Profile

        public ActionResult JoinProfile()
        {
            return View();

        }
        public ActionResult JoinProfileKa()
        {

            return View();

        }
        //Join To End View

        public ActionResult SuccessStories()
        {
            string query = "select * from tbl_placement order by id desc";
            DBManager db = new DBManager();
            DataTable placement= db.executeSelect(query);
            ViewBag.placement = placement;
            return View();
        }

        public ActionResult Facility()
        {

            return View();
        }

    }

}

            
