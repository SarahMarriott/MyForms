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

    public class FormDataItems
    {
        public List<FormData> data { get; set; }
    }

    public class ViewFormSubmissionsVM
    {
        public int Id { get; set; } //Form ID
        public string FormName { get; set; }
        public string ApplicationUser { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public ICollection<FormData> SubmittedData { get; set; }
    }

    public class LayoutItems
    {
        public List<LayoutItem> Items { get; set; }
    }

    public class LayoutItem
    {
        public string Name { get; set; }
        public string Label { get; set; }
    }
}