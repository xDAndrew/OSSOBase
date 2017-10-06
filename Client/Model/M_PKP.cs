using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    class M_PKP
    {
        EF.PKP data;
        List<string> PKP = new List<string>();
        int sIndex = -1;

        public M_PKP(int? ID = null)
        {
            if (ID != null)
            {
                data = EF.EntityInstance.DBContext.PKPSet.First(p => p.PKP_ID == ID.Value);
            }
            else
            {
                data = new EF.PKP();
            }

            var lTemp = Model.EF.EntityInstance.DBContext.PKPModelsSet.Where(p => p.Visible == true).ToList();
            foreach (var item in lTemp)
            {
                PKP.Add(item.Name);
            }

            data.Serial = "Serial";
            data.Password = "Password";
            data.Phone = "Phone";
            data.Date = DateTime.Now;
        }

        #region Properties
        public List<string> PKPList
        {
            get { return PKP; }
        }

        public int PKPIndex
        {
            get { return sIndex; }
            set { sIndex = value; }
        }

        public string Serial
        {
            get { return data.Serial; }
            set { data.Serial = value; }
        }

        public string Phone
        {
            get { return data.Phone; }
            set { data.Phone = value; }
        }

        public string Password
        {
            get { return data.Password; }
            set { data.Password = value; }
        }

        public DateTime Date
        {
            get { return data.Date; }
            set { data.Date = value; }
        }
        #endregion
    }
}
