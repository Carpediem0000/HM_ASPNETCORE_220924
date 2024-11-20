using HM_ASPNETCORE_220924.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HM_ASPNETCORE_220924.Controllers
{
    public class CustomersController : Controller
    {
        private static readonly List<Customer> _customers = new List<Customer>();

        public ActionResult Index()
        {
            return View(_customers);
        }

        // GET: CustomersController/Details/5
        public ActionResult Details(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // GET: CustomersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("FirstName", "LastName", "Email")] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    customer.Id = _customers.Any() ? _customers.Max(p => p.Id) + 1 : 1;
                    _customers.Add(customer);
                    return RedirectToAction(nameof(Details), new {id = customer.Id});
                }

                return View(customer);
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Edit/5
        public ActionResult Edit(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id", "FirstName", "LastName", "Email")] Customer updateCustomer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var customer = _customers.FirstOrDefault(c => c.Id == id);

                    customer.FirstName = updateCustomer.FirstName;
                    customer.LastName = updateCustomer.LastName;
                    customer.Email = updateCustomer.Email;

                    return RedirectToAction(nameof(Details), new { id = customer.Id });
                }

                return View(updateCustomer);
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var customer = _customers.FirstOrDefault(c => c.Id == id);

                if (customer == null)
                {
                    return NotFound();
                }

                _customers.Remove(customer);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
