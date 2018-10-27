using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicObjects.Models
{
    public class Storage
    {
        public string Server { get; set; }

        public string Database { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        public string ConnectionString
        {
            get {
                string connectionString = string.Empty;
                if (string.IsNullOrEmpty(User) && string.IsNullOrEmpty(Password)) {
                    connectionString = $"Data Source={Server};Initial Catalog={Database};Trusted_Connection=True;MultipleActiveResultSets=true";
                }
                else {
                    connectionString = $"Data Source={Server};Initial Catalog={Database};Persist Security Info=False;User ID={User};Password={Password}";
                }

                return connectionString;
            }
        }
    }
}
