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

        List<EF.PKPModels> PKP = new List<EF.PKPModels>();
        EF.PKPModels sIndex = null;

        private bool changed = false;
        public bool Changed
        {
            get { return changed; }
            set { changed = value; }
        }

        public M_PKP(int? ID = null)
        {
            PKP = Model.EF.EntityInstance.DBContext.PKPModelsSet.AsNoTracking().Where(p => true).ToList();

            if (ID != null)
            {
                data = EF.EntityInstance.DBContext.PKPSet.First(p => p.Cards_ID == ID.Value);
                foreach (var item in PKP)
	            {
                    if (item.PKPModels_ID == data.PKPModels_ID)
                    {
		                sIndex = item;
                    }
	            }
                moduls.Load(data.PKP_ID);
            }
            else
            {
                data = new EF.PKP();
                Serial = "";
                Phone = "";
                Password = "";
                data.Date = DateTime.Now;
            }
        }

        public void Save(int ID)
        {
            data.PKPModels_ID = sIndex.PKPModels_ID;
            if (data.PKP_ID == 0)
            {
                data.Cards_ID = ID;
                Model.EF.EntityInstance.DBContext.PKPSet.Add(data);
            }
            moduls.Save(data.PKP_ID);
            Model.EF.EntityInstance.DBContext.SaveChanges();
        }

        public List<EF.PKPModels> PKPList
        {
            get { return PKP; }
        }

        public EF.PKPModels PKPIndex
        {
            get { return sIndex; }
            set 
            { 
                sIndex = value;
                changed = true;
            }
        }

        public string Serial
        {
            get { return data.Serial; }
            set 
            { 
                data.Serial = value;
                changed = true;
            }
        }

        public string Phone
        {
            get { return data.Phone; }
            set 
            { 
                data.Phone = value;
                changed = true;
            }
        }

        public string Password
        {
            get { return data.Password; }
            set 
            { 
                data.Password = value;
                changed = true;
            }
        }

        public DateTime SelectedDate
        {
            get { return data.Date; }
            set 
            { 
                data.Date = value;
                changed = true;
            }
        }

        public PKPModel.Modul_Collection Moduls
        {
            get { return moduls; }
        }
    }
}
