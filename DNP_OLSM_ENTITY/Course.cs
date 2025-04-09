using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP_OLSM_ENTITY
{
    public class CourseModel
    {
        public Int32? CourseID {  get; set; }
        public string? CourseName { get; set; }
        public string? CourseDesc { get; set; }
        public string? Createdby { get; set; }

        public Int32? CreatedByID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public DataTable GetCourseInfo { get; set; }

    }
}
