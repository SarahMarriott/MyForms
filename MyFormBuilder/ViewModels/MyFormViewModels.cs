using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFormBuilder.ViewModels
{

    public class MyFormSubmissionVM
    {
        public int Id { get; set; }
        public int MyFormId { get; set; }
        public string ApplicationUserID { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public ICollection<FormData> SubmittedData { get; set; }
    }

    public class FormData
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}