using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.EquipmentModel
{
    class TSOModel
    {
        Model.EF.TSO data;
        double UUAmount = 0.0;

        public TSOModel(Model.EF.TSO data)
        {
            this.data = data;
            this.UUAmount = Model.EF.EntityInstance.DBContext.TSOGroupSet.First(p => p.TSOGroup_ID == data.Group_ID).Amount;
        }

        public string Name
        {
            get { return data.Name; }
        }

        public double UU
        {
            get { return UUAmount; }
        }

        public void Save()
        {
            //Сохраняется в базу данных
        }
    }
}
