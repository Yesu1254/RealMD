using RealMD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RealMD.Controllers
{
    public class AddressBookController : Controller
    {
        public AddressBooks addressBooks = new AddressBooks();
        public SqlManager sqlManager = new SqlManager();

        public ActionResult Index()
        {
            var dt = sqlManager.GetDataTable($@"SELECT * FROM AddressBooks");
            var lst = sqlManager.ConvertDataTable<AddressBook>(dt);
            addressBooks.LstAddressBooks = lst.ToList();
            return View(addressBooks);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetAddressBooks(string firstName)
        {
            var sqlManager = new SqlManager();
            var dt = sqlManager.GetDataTable($@"SELECT * FROM AddressBooks WHERE firstName like '{firstName}%'");
            var lst = sqlManager.ConvertDataTable<AddressBook>(dt);
            addressBooks.LstAddressBooks = lst.ToList();
            return View(addressBooks);
        }

        public ActionResult Save(AddressBook addressbook)
        {
            var query = "";
            if (addressbook.id <= 0)
            {
                query = $@"INSERT INTO AddressBooks(firstName,lastName,mobileNo,dateofbirth,email)
                        VALUES('{addressbook.firstName}', '{addressbook.lastName}', '{addressbook.mobileNo}', '{DateTime.Now.ToString()}','{addressbook.email}' )";
            }
            else
            {
                query = $@"UPDATE a SET a.firstName = '{addressbook.firstName}',a.lastName = '{addressbook.lastName}', a.mobileNo = '{addressbook.mobileNo}', a.email = '{addressbook.email}'
                          FROM AddressBooks a WHERE a.id = {addressbook.id}";
            }
            sqlManager.ExecuteNonQuery(query);
            return View(true);
        }

        public ActionResult Delete(int id)
        {
            var query = "";
            if (id > 0)
            {
                query = $@"DELETE FROM AddressBooks WHERE id = {id}";
            }
            sqlManager.ExecuteNonQuery(query);
            return View(true);
        }
    }
}