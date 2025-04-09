using DNP_OLSM_DS;
using DNP_OLSM_ENTITY;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP_OLSM_DL;
using DNP_OLSM_ENTITY;
using DNP_OLSM_DS;
using System.Data;
using System.Data.SqlClient;

namespace DNP_OLSM_DL
{
    public class dlCourse
    {
        public static void DAAGetCourseInformation(CourseModel CS, out DataSet ds)
        {
            try
            {

                DBAccess objDbAccess = new DBAccess();
                string SP = "GetCourseInformation";

                SqlParameter[] spc = new SqlParameter[1];
               
                spc[0] = new SqlParameter("@CreatedBy", CS.Createdby);


                ds = objDbAccess.getDataSet(SP, spc);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DAAAddEditCourseInformation(CourseModel CM , out int vCourseID)
        {
            vCourseID = 0;
            try
            {
                DataSet ds = new DataSet();

                DBAccess ObjDBAccess = new DBAccess();
                string sp = "AddEditCourseInformation";
                SqlParameter[] spc = new SqlParameter[4];
                spc[0] = new SqlParameter("@CourseName", CM.CourseName);
                spc[1] = new SqlParameter("@Coursedesc", CM.CourseDesc);
                spc[2] = new SqlParameter("@DateEntered", CM.CreatedDate);
                spc[3] = new SqlParameter("@IsActive", CM.IsActive);

                ds = ObjDBAccess.getDataSet(sp, spc);

                vCourseID = Convert.ToInt32(ds.Tables[0].Rows[0]["CourseID"]);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
