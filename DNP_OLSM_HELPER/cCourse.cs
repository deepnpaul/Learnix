using DNP_OLSM_DL;
using DNP_OLSM_ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNP_OLSM_HELPER
{
    public class cCourse
    {
        public static DataTable CGetCourseInformation(CourseModel CS)
        {
            DataSet dst = new DataSet();
            dlCourse.DAAGetCourseInformation(CS, out dst);
            return dst.Tables[0];
        }

        public static int cAddEditCourseInformation(CourseModel CM)
        {
            int sCourseID = 0;
            dlCourse obj = new();
            obj.DAAAddEditCourseInformation(CM, out sCourseID);
            return sCourseID;
        }
    }
}
