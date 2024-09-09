using Microsoft.Ajax.Utilities;
using MyProject1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Antlr.Runtime.Misc;
using System.Web.UI.WebControls;

namespace MyProject1.Controllers
{
    [CheckSession]
    public class AdminController : Controller
    {
        //public bool CheckSession()
        //{


        //}

        // GET: Admin
        public ActionResult Index()
        {
            //if (Session["admin"] == null)
            //{
            //    return RedirectToAction("adminlogin", "home");
            //}




                return View();
        }
        public ActionResult submittedtask()
        {
            
            string query = "select s.name,t.title,ans.ans_file,ans.id ,ans.total_marks,ans.obtain_marks from tbl_submittedtask ans left join tbl_task t on ans.task_id=t.task_id left join tbl_student s on s.email=ans.email_id";
            //DataTable task = db.executeSelect(query);
            ViewBag.ans = db.executeSelect(query);
            return View();

            
        }
        [HttpPost]
        public ActionResult marking(int? ansid,int? maxmark,int? obtainmark)
        {

            if(ansid.HasValue && maxmark.HasValue && obtainmark.HasValue)
            {
                string query = "update tbl_submittedtask set obtain_marks="+obtainmark+",total_marks="+maxmark+" where id="+ansid+"";
                int result = db.executeInsertUpdateDelete(query);
                if (result > 0) {
                    return Content("<script>alert('marks upadate');location.href='/admin/submittedtask'</script>");
            }
                else
                {
                    return Content("<script>alert('marks not updated');'/admin/index'</script>");
                }
            }
            else
            {
                return Content("<script>alert('Something going wrong');'/admin/index'</script>");
            }
        }
        public ActionResult success()
        {

            return View();
        }
        [HttpPost]
        public ActionResult success(string name,string collegename,string course,string companyname,string post,string field,string package,HttpPostedFileBase companylogo,HttpPostedFileBase studentpic)
        {
            string query = "insert into tbl_placement values('"+name+"','"+collegename+"','"+course+"','"+companyname+"','"+post+"','"+field+"','"+package+"','"+companylogo.FileName+"','"+studentpic.FileName+ "','"+DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt")+"')";
            int result = db.executeInsertUpdateDelete(query);

            if (result > 0)
            {
                companylogo.SaveAs(Server.MapPath("~/Content/clogo/") + companylogo.FileName);
                studentpic.SaveAs(Server.MapPath("~/Content/spic/") + studentpic.FileName);
                return Content("<script>alert('Data added successfully');location.href='/admin/success'</script>");
            }
            else
            {
                return Content("<script>alert('not Data added successfully');location.href='/admin/success'</script>");
            }


        }


        public ActionResult Category()
        {
            DataTable dt = db.executeSelect("select * from tbl_videocategory order by cid desc");
            ViewBag.Category = dt;
            return View();
        }
        DBManager db = new DBManager();
        [HttpPost]
        public ActionResult Category(string catname,HttpPostedFileBase caticon)
        {
            if(catname!=null && caticon!=null)
            {
                string query = "insert into tbl_videocategory values('"+catname+"','"+caticon.FileName+"')";
                int result = db.executeInsertUpdateDelete(query);
                if (result > 0)
                {
                    // server .MapPath() function is used to get location od any folder present at server
                    caticon.SaveAs(Server.MapPath("/Content/caticons/")+caticon.FileName);
                    return Content("<script>alert('Data Added');location.href='/admin/category'</script>");
                }
            
                else
                {
                    return Content("<script>alert('Data not Added');location.href='/admin/category'</script>");

                }
            }
            else
            {
                return Content("<script>alert('Fill all fields properly');location.href='/admin/category'</script>");
            }
            
        }
        public ActionResult AddVideo()
        {
            DataTable dt = db.executeSelect("select bid, bname from tbl_batch");
            ViewBag.batches = dt;
            DataTable cat = db.executeSelect("select cid,cname from tbl_videocategory");
            ViewBag.cat = cat;
            DataTable videos = db.executeSelect("select * from tbl_video order by vid desc");
            ViewBag.videos = videos;
            return View();
        }
        [HttpPost]
        public ActionResult AddVideo(int? cbatch,int? category,string title,string description,HttpPostedFileBase videofile)
        {
            videofile.SaveAs(Server.MapPath("/Content/videos") +videofile.FileName);
            string query = "insert into tbl_video values("+ cbatch + ","+ category + ",'"+title+"','"+description+"','"+videofile.FileName+"','"+DateTime.Now+"')";
            int result = db.executeInsertUpdateDelete(query);
            if (result > 0)
            {

                videofile.SaveAs(Server.MapPath("/Content/videos/") + videofile.FileName);
                return Content("<script>alert(' Video  Added');location.href='/admin/AddVideo';</script>");
            }
            else
            {
                return Content("<script>alert('Video not Added');location.href='/admin/AddVideo';</script>");

            }
        }

        public ActionResult AddBatch()
        {
            DataTable dt = db.executeSelect("select * from tbl_batch  order by bid asc");
            ViewBag.AddBatch = dt;
            return View();
        }
         
        [HttpPost]
        public  ActionResult AddBatch(string batchname,string batchsdate,string batchedate,int totalfee,string batchtopic,HttpPostedFileBase batchpic)
        {
            string query = "insert into tbl_batch values('"+batchname+"','"+batchsdate+"','"+ batchedate+"',"+totalfee+ ",'"+batchtopic+ "','"+batchpic.FileName+"')";
            int result = db.executeInsertUpdateDelete(query);
            if (result > 0)
            {
                batchpic.SaveAs(Server.MapPath("/Content/BatchPic/")+batchpic.FileName);
                return Content("<script>alert('Data Added Successfully');location.href='/admin/AddBatch';</script>");
            }
            else
            {
                return Content("<script>alert('Batch Not added Successfully');location.href='/admin/AddBatch';</script>");
            }
        }

        public ActionResult AddAssignment()
        {
            DataTable dt = db.executeSelect("select bid,bname from tbl_batch");
            ViewBag.batches = dt;
            string query = "select * from tbl_task order by task_id desc";

            DataTable task = db.executeSelect(query);
            ViewBag.task = task;
            return View();
        }
        [HttpPost]
        public ActionResult AddAssignment(int Cbatch,string Vtitle,string description, HttpPostedFileBase Ctask,string author)
        {
            string query = "insert into tbl_task values("+Cbatch+",'"+Vtitle+"','"+ description + "','"+ Ctask.FileName+"','"+author+"','"+DateTime.Now+"')";
            int result = db.executeInsertUpdateDelete(query);

            if (result > 0)
            {
                Ctask.SaveAs(Server.MapPath("/Content/Task/") +Ctask.FileName);
                return Content("<script>alert(' Add Task Successfully');location.href='/admin/AddAssignment';</script>");
            }
            else
            {
                return Content("<script>alert(' Not add Task Successfully');location.href='/admin/AddAssignment';</script>");
            }
        
        }

        public ActionResult AddClasslink()
        {

            DataTable dt = db.executeSelect("select bid,bname from tbl_batch");
            ViewBag.batches = dt;
            DataTable link = db.executeSelect("select * from tbl_classlink");
            ViewBag.link = link;
            return View();
        }
        [HttpPost]
         public ActionResult AddClasslink(int? batch,string link, DateTime date,string stime,string etime,string message)
        {
            string query = "insert into tbl_classlink values("+ batch + ",'"+ link + "','"+date.ToString("yyyy-MM-dd")+"','"+stime+"','"+etime+"','"+message+"','"+DateTime.Now.ToString("yyyy-MM-dd")+"')";
            int result = db.executeInsertUpdateDelete(query);
            if (result > 0)
            {
                //Ctask.SaveAs(Server.MapPath("/Content/Task/") + Ctask.FileName);
                return Content("<script>alert(' Add Link Successfully');location.href='/admin/AddAClasslink';</script>");
            }
            else
            {
                return Content("<script>alert(' Not add link Successfully');location.href='/admin/AddClasslink';</script>");
            }

            


        }



        public ActionResult StudentMT()
        {
            return View();
        }
        public ActionResult EnquiryMt()
        {
            return View();
        }
        public ActionResult SubmittedMt()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult Logout()
        {
            Session.Remove("admin");
            //return Content("<script>location.href='/home/signin';<script>");
            return RedirectToAction("adminlogin", "home");
        }

    }



    //Let's create a action filter to check session , if session is not action redirect to the adminlogin
    class CheckSession : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //throw new NotImplementedException();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //throw new NotImplementedException();

          if(filterContext.HttpContext.Session["admin"] == null)
            {
                filterContext.Result =new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
                {
                    {"Controller","Home" },
                    {"Action","adminlogin" }
                });
            }

        }
        
    }
   

}