using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNP_OLSM_DL;
using DNP_OLSM_ENTITY;

namespace DNP_OLSM_HELPER
{
    public class cUsers
    {
        public static string cAddUser(User US)
        {
            string vUserid = "";
            dlAddUser obj = new();
            obj.DAAddUser(US, out vUserid);
            return vUserid;
        }

        public static DataSet VerifyUser(User Us)
        {
            DataSet ds = new DataSet();
            List<User> ListOut = new List<User>();
            dlAddUser.DAAVerifyUser(Us, out ds);
            return ds;
        }
    }
}
