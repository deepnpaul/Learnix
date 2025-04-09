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
    public class dlAddUser
    {
        public void DAAddUser(User US, out string vUserID)
        {
            vUserID = string.Empty;
            try
            {
               DataSet DS = new();

                DBAccess ObjDBAccess = new();
                string SP = "AddEditUser";
                SqlParameter[] spc = new SqlParameter[9];
                spc[0] = new SqlParameter("@Name", US.Name);
                spc[1] = new SqlParameter("@Email", US.Email);
                spc[2] = new SqlParameter("@Password", US.Password);
                spc[3] = new SqlParameter("@IsActive", US.IsActive);
                spc[4] = new SqlParameter("@UserTypeID", US.UserTypeID);
                spc[5] = new SqlParameter("@UserTypeName", US.UserTypeName);
                spc[6] = new SqlParameter("@DOB", US.DOB);
                spc[7] = new SqlParameter("@PhoneNo", US.PhoneNo);
                spc[8] = new SqlParameter("@ZipCode", US.ZipCode);


                DS = ObjDBAccess.getDataSet(SP, spc);

                vUserID = DS.Tables[0].Rows[0]["UserId"].ToString();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static void DAAVerifyUser(User Us, out DataSet ds)
        {
            try
            {

                DBAccess objDbAccess = new DBAccess();
                string SP = "GetUserDetails";

                SqlParameter[] spc = new SqlParameter[2];
                spc[0] = new SqlParameter("@Email", Us.Email);
                spc[1] = new SqlParameter("@Password", Us.Password);
               

                ds = objDbAccess.getDataSet(SP, spc);
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
