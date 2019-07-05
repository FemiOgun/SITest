using Dapper;
using SkyInsuranceThird.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkyInsuranceThird.Controllers
{
    public class SkyInsuranceController : Controller
    {
        // Create connection string to DB

        static string strConnectionString = @"Data Source=iss-dev02;Initial Catalog=MVCDapperDB;Integrated Security=True";
       
        
        
        // GET: SkyInsurance 
        // List All Users (Main Page)


        public ActionResult Index()
        {
            List<User> UserList = new List<User>();
            using (IDbConnection db = new SqlConnection(strConnectionString))
            {
                UserList = db.Query<User>("Select * From [SITest].[dbo].[User]").ToList();
            }
            return View(UserList);
        }

        

        // GET: SkyInsurance/CreateUser

        public ActionResult CreateUser()
        {

            return View();
        }

        // POST: SkyInsurance/CreateUser
        // Creates a User 
        [HttpPost]
        public ActionResult CreateUser(User _user)
        {
            
            using(IDbConnection db = new SqlConnection(strConnectionString))
            {
                string sqlQuery = "Insert Into [SITest].[dbo].[User] (Name,Postcode) Values('" + _user.Name + "','" + _user.Postcode + "')";

                int rowsAffected = db.Execute(sqlQuery);
            }
            return RedirectToAction("Index");
        }

        // GET: SkyInsurance/Create
        public ActionResult CreateVehicle(int id)
        {
            Vehicle _vehicle = new Vehicle();
            using (IDbConnection db = new SqlConnection(strConnectionString))
            {
                _vehicle = db.Query<Vehicle>("Select Id from [SITest].[dbo].[User]" + "WHERE Id =" + id, new { id }).SingleOrDefault();
            }
            return View(_vehicle);
        }

        [HttpPost]
        public ActionResult CreateVehicle(int id, Vehicle _vehicle)
        {
            
            using (IDbConnection db = new SqlConnection(strConnectionString))
            {
                //string sqlQuery = "SELECT IDENTITY() AS UserId Insert into [SiTest].[dbo].[Vehicle] (Registration,MakeModel) Values('" + _vehicle.Registration + "','" + _vehicle.MakeModel + "')"; 
                string sqlQuery = @"INSERT INTO SITest.dbo.Vehicle(UserId, Registration, MakeModel) Values("+ id + @", '"+_vehicle.Registration+@"', '" + _vehicle.MakeModel + @"')";
                int rowsAffected = db.Execute(sqlQuery);

            }
            return RedirectToAction("Index");
        }

        // GET: SkyInsurance/Details/5
        // GET: SkyInsurance/Details/5
        public ActionResult Details(int id)
        {
            ////User _user = new User();
            //ViewModelUserVehicle viewModelUserVehicle = new ViewModelUserVehicle();
            //viewModelUserVehicle.Vehicles = Vehicle.List();
           
            using (IDbConnection db = new SqlConnection(strConnectionString))
            {
                string sql = @"Select * from [SITest].[dbo].[User] WHERE id = @id
                               Select * from [SITest].[dbo].[Vehicle] WHERE userid = @id";

                var multi = db.QueryMultiple(sql, new { id });
                var user = multi.Read<User>().Single();
                var vehicles = multi.Read<Vehicle>().ToList();

                user.Vehicles = vehicles;    
                return View(user);
            }


        }
            // GET: SkyInsurance/Edit/5
        public ActionResult Edit(int id)
        {
          

            using (IDbConnection db = new SqlConnection(strConnectionString))
            {
                string sql = @"Select * from [SITest].[dbo].[User] WHERE id = @id
                               Select * from [SITest].[dbo].[Vehicle] WHERE userid = @id";

                var multi = db.QueryMultiple(sql, new { id });
                var user = multi.Read<User>().Single();
                var vehicles = multi.Read<Vehicle>().ToList();

                user.Vehicles = vehicles;
                return View(user);
            }


        }

        // POST: SkyInsurance/Edit/5
        [HttpPost]
        public ActionResult Edit(User _user)
        {

            using (IDbConnection db = new SqlConnection(strConnectionString))
            {
                string sqlQuery = "Update [SITest].[dbo].[User] set Name='" + _user.Name + "',Postcode='" + _user.Postcode + "' where Id=" + _user.Id;

                int rowsAffected = db.Execute(sqlQuery);
            }
            return RedirectToAction("Index");
        }

        //public ActionResult EditVehicle(int id)
        //{
        //    using (IDbConnection db = new SqlConnection(strConnectionString))
        //    {
        //        string sql = @"Select * from [SITest].[dbo].[User] WHERE id = @id
        //                       Select * from [SITest].[dbo].[Vehicle] WHERE userid = @id";

        //        var multi = db.QueryMultiple(sql, new { id });
                
        //        var vehicles = multi.Read<Vehicle>().Single();

                
        //        return View(vehicles);
        //    }
        //}

        //// POST: SkyInsurance/Edit/5
        //[HttpPost]
        //public ActionResult EditVehicle(User _user)
        //{

        //    using (IDbConnection db = new SqlConnection(strConnectionString))
        //    {
        //        string sqlQuery = "Update [SITest].[dbo].[User] set Name='" + _user.Name + "',Postcode='" + _user.Postcode + "' where Id=" + _user.Id;

        //        int rowsAffected = db.Execute(sqlQuery);
        //    }
        //    return RedirectToAction("Index");
        //}

        // GET: SkyInsurance/Delete/5
        public ActionResult Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(strConnectionString))
            {
                string sql = @"Select * from [SITest].[dbo].[User] WHERE id = @id
                               Select * from [SITest].[dbo].[Vehicle] WHERE userid = @id";

                var multi = db.QueryMultiple(sql, new { id });
                var user = multi.Read<User>().Single();
                var vehicles = multi.Read<Vehicle>().ToList();

                user.Vehicles = vehicles;
                return View(user);
            }
        }

        // POST: SkyInsurance/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            using (IDbConnection db = new SqlConnection(strConnectionString))
            {
                string sqlQuery = "Delete from [SITest].[dbo].[User] WHERE id =" + id;
                int rowsAffected = db.Execute(sqlQuery);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteVehicles(int id)
        {
            List<Vehicle> ListVehicles = new List<Vehicle>();
            using (IDbConnection db = new SqlConnection(strConnectionString))
            {
                ListVehicles = db.Query<Vehicle>("Select Id from [SITest].[dbo].[Vehicle]" + "WHERE Id =" + id, new { id }).ToList();
            }
            return View(ListVehicles);
        }
        //{
        //    using (IDbConnection db = new SqlConnection(strConnectionString))
        //    {
        //        string sql = @"Select * from [SITest].[dbo].[User] WHERE id = @id
        //                       Select * from [SITest].[dbo].[Vehicle] WHERE userid = @id";

        //        var multi = db.QueryMultiple(sql, new { id });
        //        var user = multi.Read<User>().Single();
        //        var vehicles = multi.Read<Vehicle>().ToList();

        //        user.Vehicles = vehicles;
        //        return View(vehicles);
        //    }
        //}

        [HttpPost]
        public ActionResult DeleteVehicles(int id, FormCollection collection)
        {
            using (IDbConnection db = new SqlConnection(strConnectionString))
            {
                string sqlQuery = "Delete from [SITest].[dbo].[Vehicle] WHERE id =" + id;
                int rowsAffected = db.Execute(sqlQuery);
            }

            return RedirectToAction("Index");
        }


    }
}
