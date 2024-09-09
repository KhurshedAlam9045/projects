using MyProject1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MyProject1.Controllers
{
    [CheckStudentSession]
   
    public class StudentController : Controller
    {
        // GET: Student
        DBManager db = new DBManager();
        public ActionResult Index()
        {
            string email = Session["semail"].ToString();
            string query = "select * from tbl_student where email='" + email + "'";
            ViewBag.profile = db.executeSelect(query);

            int batch = Convert.ToInt32(Session["sbatch"].ToString());
            string q1 = "select top 1 * from tbl_classlink where batch=" + batch + " order by lid desc";
            ViewBag.link = db.executeSelect(q1);
            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("StdudentLogin", "home");
        }
        public ActionResult Assignment()
        {
            if (Session["sbatch"]!= null)
            {
                int batch = Convert.ToInt32(Session["sbatch"].ToString());
                string email = Session["semail"].ToString();
                //string query = "select * from tbl_task where batch_id="+batch+" order by task_id desc";
                string query = "select task.*,ans.id from tbl_task task left join tbl_submittedtask ans on task.task_id=ans.task_id and ans.email_id='"+email+"' where task.batch_id="+batch+"";
                DataTable task= db.executeSelect(query);
                ViewBag.task = task;
                return View();
            }
            else
            {
                return Content("<script>alert('Error occured . Please Login again');loaction.href='/home/stdudentloginn'</script>");
            }
            //return View();
        }
        [HttpPost]
        public ActionResult Assignment(int? tid,HttpPostedFileBase tfile)
        {
            string email = Session["semail"].ToString();
            string query = "insert into tbl_submittedtask values("+tid+",'"+email+"','"+tfile.FileName+"',0,0,'"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")+"')";
            int result = db.executeInsertUpdateDelete(query);
            if (result > 0)
            {
                tfile.SaveAs(Server.MapPath("/Content/answerfile/")+tfile.FileName);
                return Content("<script>alert('Your task submitted');location.href=/student/assignment'</script>");
            }
            else
            {
                return Content("<script>alert('Please try again');location.href=/student/assignment'</script>");
            }
        }

        public ActionResult lecture()
        {
            string query = "select * from tbl_videocategory order by cid desc";
            ViewBag.vcat = db.executeSelect(query);
            return View();
        }

        public ActionResult Videos(int? cid)
        {

            if (Session["sbatch"]!= null && cid.HasValue)
            {

                int batch = Convert.ToInt32(Session["sbatch"].ToString());
                string query = "select * from tbl_video where batch_id="+batch+" and cat_id="+cid+" order by vid desc";
                ViewBag.Video = db.executeSelect(query);
                return View();
            }
            else
            {
                return Content("<script>alert('please choose category');loaction.href='/student/lecture'</script>");
            }
        }
        public ActionResult changaPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult changaPassword(string opass,string npass,string cnpass)

        {
            if (npass.Equals(cnpass))
            {
                string email = Session["semail"].ToString();
                string query = "update tbl_student set password='" + npass + "' where email='" + email + "' and password='" + opass + "'";
                int result = db.executeInsertUpdateDelete(query);
                if (result > 0)
                {
                    Session.RemoveAll();
                    return Content("<script><alert('Password update. please login again');location.href='/home/studentlogin'script>");
                }
                else
                {
                    return Content("<script><alert('Old Password not matched');location.href='/student/changaPassword'script>");
                }
            }
            else
            {
                return Content("<script>alert('New Password and confirm not matched');location.href='/student/chnagePassword'<script>");
            }
           
        }
        public ActionResult profile()
        {
            string email = Session["semail"].ToString();
            string query = "select * from tbl_student where email='" + email + "'";
            ViewBag.profile = db.executeSelect(query);
            return View();
        }
        [HttpPost]
        public ActionResult profile(string name,long mobno,string college)
        {
            string email = Session["semail"].ToString();
            string query = "update tbl_student set name='" + name + "',mobileno=" + mobno +",college='"+college+"' where email='"+email+"'";
            int result = db.executeInsertUpdateDelete(query);
            if (result > 0)
            {
                return Content("<script>alert('update  Profile');location.href='/student/profile'</script>");
            }
            else
            {
                return Content("<script>alert('few error');location.href='/student/profile'</script>");
            }

           
        }


        class CheckStudentSession : ActionFilterAttribute
     {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //throw new NotImplementedException();

            if (filterContext.HttpContext.Session["semail"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"Controller","Home" },
                    {"Action","Studentlogin" }
                });
            }

        }

      }


    }

}