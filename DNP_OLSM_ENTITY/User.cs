using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP_OLSM_ENTITY
{
    public class User
    {
        public Int32? ID { get; set; }
        public string? UserName { get; set; }

        public  string Name { get; set; }
        public  string Email { get; set; }
        public string Password { get; set; }
        public  DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public Int32? UserTypeID { get; set; }
        public string? UserTypeName { get; set; }
        public string? ActorID { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DOB { get; set; }
        public string PhoneNo { get; set; }
        public string ZipCode { get; set; }


        public DataSet GetUserData {  get; set; }
    }
}
