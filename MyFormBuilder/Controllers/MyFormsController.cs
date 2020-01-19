using MyFormBuilder.Models;
using MyFormBuilder.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyFormBuilder.Controllers
{
    public class MyFormsController : Controller
    {
        private MyFormBuilderModel db = new MyFormBuilderModel();

        // GET: MyForms
        public ActionResult Index()
        {
            return View(db.MyForms.ToList());
        }

        // GET: MyForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyForm myForm = db.MyForms.Find(id);
            if (myForm == null)
            {
                return HttpNotFound();
            }
            return View(myForm);
        }

        [HttpPost]
        public ActionResult SaveForm(MyFormBuilder.Models.MyForm Model)
        {
            if (Model != null)
            {
                //save the form to the database
                var newForm = new Models.MyForm
                {
                    FormName = Model.FormName,
                    FormLayout = Model.FormLayout,
                    IsActive = Model.IsActive
                };

                db.MyForms.Add(newForm);
                db.SaveChanges();


                return Json("Success");
            }
            else
            {
                return Json("An Error Has occured");
            }
        }

        public ActionResult SubmitForm(MyFormSubmissionVM submission)
        {
            if (ModelState.IsValid)
            {
                var myform = db.MyForms.Find(submission.MyFormId); //find the relevant form

                var submitteddata = JsonConvert.SerializeObject(submission.SubmittedData);               
                

                //now write the submission to the database
                MyFormSubmission submit = new MyFormSubmission
                {
                    ApplicationUserID = "ABCD",
                    DateTimeCreated = DateTime.Now,
                    MyFormId = myform,
                    SubmittedData = submitteddata
                };

                db.MyFormSubmissions.Add(submit);
                db.SaveChanges();

                return Json("Success");
            }
            else
            {
                return Json("We have a problem");
            }
        }

        public ActionResult ViewSubmissions(int ID) //the form id
        {
            //get the submisisons for this form
            var Submissions = db.MyFormSubmissions.Where(s => s.MyFormId.Id == ID).ToList();

            //get the form details
            var Form = db.MyForms.Find(ID);

            //get the form data 
            dynamic FormLayout = JsonConvert.DeserializeObject(Form.FormLayout);

            //store all the required headers in a dictionary object for later
            IDictionary<string, string> fields = new Dictionary<string, string>();
            foreach(var layout in FormLayout)
            {
                if (layout.type.ToString() != "header" && layout.type.ToString() != "paragraph")
                {
                    //an input type
                    fields.Add(layout.name.ToString(), layout.label.ToString());
                }
            }

            //object to store the output
            List<ViewFormSubmissionsVM> displayList = new List<ViewFormSubmissionsVM>();

            
            //loop submissions
            foreach(var submission in Submissions)
            {
                ViewFormSubmissionsVM displayItem = new ViewFormSubmissionsVM 
                {
                    ApplicationUser = submission.ApplicationUserID,
                    DateTimeCreated = submission.DateTimeCreated,
                    FormName = Form.FormName,
                    Id = submission.Id,
                };

                //now we have to convert the data
                dynamic des = JsonConvert.DeserializeObject(submission.SubmittedData);

                List<FormData> formDatas = new List<FormData>();

                //loop each data item (field)
                foreach(var de in des) {
                    if (fields.TryGetValue(de.name.ToString(), out string result))
                    {
                        //continue       
                    }
                    else
                    {
                        //can't find a matching field - use the field name
                        result = de.name.ToString();
                    }
                    //add the new field with the submitted data
                    formDatas.Add(new FormData
                    {
                        name = result, //
                        value = de.value.ToString()
                    });
                }
                //add the form datas
                displayItem.SubmittedData = formDatas;

                displayList.Add(displayItem);
            }

            //pass field list 
            ViewBag.FieldList = fields;

            //form name for display purposes
            ViewBag.FormName = Form.FormName;
            //now we have to loop all of the 
            return View(displayList);

        }

        // GET: MyForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MyForms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FormName,FormLayout,IsActive")] MyForm myForm)
        {
            if (ModelState.IsValid)
            {
                db.MyForms.Add(myForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(myForm);
        }

        // GET: MyForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyForm myForm = db.MyForms.Find(id);
            if (myForm == null)
            {
                return HttpNotFound();
            }
            return View(myForm);
        }

        // POST: MyForms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FormName,FormLayout,IsActive")] MyForm myForm)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myForm).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(myForm);
        }

        // GET: MyForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyForm myForm = db.MyForms.Find(id);
            if (myForm == null)
            {
                return HttpNotFound();
            }
            return View(myForm);
        }

        // POST: MyForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyForm myForm = db.MyForms.Find(id);
            db.MyForms.Remove(myForm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
