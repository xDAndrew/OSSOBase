using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Client.ViewModel
{
   

    class VM_TSOWindow
    {
        private class Mod_Item
        {
            int Id;
            string Name;
            int count;
        }

        private List<Model.EF.Modules> Modules;
        double UUSumm = 0.0;

        public VM_TSOWindow(int? ID = null)
        {
            Modules = Model.EF.EntityInstance.DBContext.ModulesSet.Where(p => p.Modules_ID > 0).ToList();

            if (ID != null)
            {

            }
            else
            {

            }
        }

        public List<Model.EF.Modules> ModulsView
        {
            get
            {
                return Modules;
            }
        }
    }
}
