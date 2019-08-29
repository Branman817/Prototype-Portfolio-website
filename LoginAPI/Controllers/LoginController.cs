using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoginAPI.Models;

namespace LoginAPI.Controllers
{

    public class LoginController : ApiController
    {
        [Route("Api/Login/UserLogin")]
            [HttpPost]
            public Response Login(Login Lg)
        {
                EmployeeEntities DB = new EmployeeEntities();
            var Obj = DB.Usp_Login(Lg.UserName, Lg.Password).ToList<Usp_Login_Result>
            ().FirstOrDefault();
            if (Obj.Status == 0)
                return new Response { Status = "Invalid", Message = "Invalid User." };
            if (Obj.Status == -1)
                return new Response { Status = "Inactive", Message = "User Inactive." };
            else
            {
                if(Lg.UserName == "shen" && Lg.Password == "shen12")
                    return new Response { Status = "Success", Message = Lg.UserName, isManager = true };
                return new Response { Status = "Success", Message = Lg.UserName, isManager = false };
            }
        }

        [Route("Api/Login/createcontact")]
            [HttpPost]
            public object createcontact(Registration Lvm)
        {
            try
            {
                EmployeeEntities db = new EmployeeEntities();
                EmployeeMaster Em = new EmployeeMaster();
                if(Em.UserId == 0)
                {
                    Em.UserName = Lvm.UserName;
                    Em.LoginName = Lvm.LoginName;
                    Em.Password = Lvm.Password;
                    Em.Email = Lvm.Email;
                    //Em.IsApproved = Lvm.IsApproved;
                    //Em.Status = Lvm.Status;
                    Em.IsApproved = 1;
                    Em.Status = 3;
                    db.EmployeeMasters.Add(Em);
                    db.SaveChanges();
                    return new Response
                    { Status = "Success", Message = "Succesfully Saved" };
                }
            }
            catch (Exception)
            {
                throw;
            }
            return new Response
            { Status = "Error", Message = "Invalid Data." };
        }
        [Route("Api/Login/fetchdata")]
            [HttpGet]
        public IEnumerable<EmployeeMaster> fetchdata()
        {
            using (EmployeeEntities entities = new EmployeeEntities())
            {
                return entities.EmployeeMasters.ToList();
            }
        }

        [Route("Api/Login/Remove/{UserId}")]
            [HttpDelete]
        public void DELETE(int UserId)
        {
            EmployeeEntities db = new EmployeeEntities();
            var user = db.EmployeeMasters.FirstOrDefault(e => e.UserId == UserId);
            Console.Write(user.UserId);
            if(user.UserName != "shen" && user.Password != "shen12")
            {
                db.EmployeeMasters.Remove(user);
                //db.Entry(user).State = System.Data.Entity.EntityState.Added;
                db.SaveChanges();
            }
        }

        [Route("Api/Login/Update")]
            [HttpPut]
        public void PUT(Row row)
        {
            //int UserId = row.UserId;
            EmployeeEntities db = new EmployeeEntities();
            EmployeeMaster employee1 = db.EmployeeMasters.Find(row.UserId);
            if(employee1 != null)
            {
                employee1.Email = row.Email;
                employee1.UserName = row.User;
                employee1.LoginName = row.LoginName;
                employee1.Password = row.password;
                employee1.IsApproved = row.IsApproved;
                employee1.Status = row.Status;

                db.Entry(employee1).State = System.Data.Entity.EntityState.Modified;

                db.SaveChanges();
            }
            else
            {
                Console.Write("Addrow object");
                EmployeeMaster Em = new EmployeeMaster();
                if (Em.UserId == 0)
                {
                    //Em.UserId = row.UserId;
                    Em.UserName = row.User;
                    Em.LoginName = row.LoginName;
                    Em.Password = row.password;
                    Em.Email = row.Email;
                    //Em.IsApproved = Lvm.IsApproved;
                    //Em.Status = Lvm.Status;
                    Em.IsApproved = 1;
                    Em.Status = 3;
                    db.EmployeeMasters.Add(Em);
                    db.SaveChanges();
                }
            }

        }
    }
}