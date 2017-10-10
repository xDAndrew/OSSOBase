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
        int sIndex = -1;

        View.EditWindow OwnerLink;

        public M_PKP(View.EditWindow HWND, int? ID = null)
        {
            OwnerLink = HWND;

            if (ID != null)
            {
                data = EF.EntityInstance.DBContext.PKPSet.First(p => p.PKP_ID == ID.Value);
                moduls.Load(ID.Value);
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

            //data.Serial = "Serial";
            //data.Password = "Password";
            //data.Phone = "Phone";
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

        #region Commands
        private ViewModel.Command openTSOList;
        public ViewModel.Command OpenTSOList
        {
            get
            {
                return openTSOList ?? (openTSOList = new ViewModel.Command(obj =>
                {
                    var wTemp = new View.TSOWindow();
                    var cTemp = new ViewModel.VM_TSOWindow(moduls, wTemp);
                    wTemp.Owner = OwnerLink;
                    wTemp.DataContext = cTemp;
                    wTemp.ShowDialog();
                    moduls.Save(data.PKP_ID);
                    //Model.EF.EntityInstance.DBContext.SaveChanges();
                }));
            }
        }
        #endregion
    }
}
