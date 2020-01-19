namespace MyFormBuilder.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MyFormBuilderModel : DbContext
    {
        // Your context has been configured to use a 'MyFormBuilderModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'MyFormBuilder.Models.MyFormBuilderModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'MyFormBuilderModel' 
        // connection string in the application configuration file.
        public MyFormBuilderModel()
            : base("name=MyFormBuilderModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<MyForm> MyForms { get; set; }
        public virtual DbSet<MyFormSubmission> MyFormSubmissions { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}

    public class MyForm
    {

        public int Id { get; set; }
        public string FormName { get; set; }
        public string FormLayout { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class MyFormSubmission
    {
        public int Id { get; set; }
        public MyForm MyFormId { get; set; }
        public string ApplicationUserID { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public string SubmittedData { get; set; }
    }
}