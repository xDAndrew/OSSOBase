using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Model
{
    class M_PKP
    {
        EF.PKP data;
        PKPModel.Modul_Collection moduls = new PKPModel.Modul_Collection();

        List<string> PKP = new List<string>();
        List<EF.PKPModels> PKPModelsList = new List<EF.PKPModels>();
        int sIndex = -1;

        public M_PKP(int? ID = null)
        {
            if (ID != null)
            {
                data = EF.EntityInstance.DBContext.PKPSet.First(p => p.Cards_ID == ID.Value);
                sIndex = data.PKPModels_ID;
                moduls.Load(ID.Value);
            }
            else
            {
                data = new EF.PKP();
                Serial = "";
                Phone = "";
                Password = "";
                data.Date = DateTime.Now;
            }

            var lTemp = Model.EF.EntityInstance.DBContext.PKPModelsSet.Where(p => p.Visible == true).ToList();
            foreach (var item in lTemp)
            {
                PKPModelsList.Add(item);
                PKP.Add(item.Name);
            }
        }

        public void Save(int ID)
        {
            data.PKPModels_ID = PKPModelsList[PKPIndex].PKPModels_ID;
            data.Cards_ID = ID;
            Model.EF.EntityInstance.DBContext.PKPSet.Add(data);
            Model.EF.EntityInstance.DBContext.SaveChanges();
            moduls.Save(data.PKP_ID);
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

        public string Date
        {
            get { return data.Date.ToString("dd MM yyyy"); }
            set { data.Date = Convert.ToDateTime(value); }
        }

        public PKPModel.Modul_Collection Moduls
        {
            get { return moduls; }
        }
        #endregion
    }
}
