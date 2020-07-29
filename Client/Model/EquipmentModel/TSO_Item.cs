using System.Linq;

namespace Client.Model.EquipmentModel
{
    public class TSO_Item
    {
        Model.EF.TSO data;
        double UUAmount = 0.0;

        public TSO_Item(Model.EF.TSO data)
        {
            this.data = data;
            try
            {
                this.UUAmount = Model.EF.EntityInstance.DBContext.TSOGroupSet.First(p => p.TSOGroup_ID == data.Group_ID).Amount;
            }
            catch { }
        }

        public int Id
        {
            get { return data.TSO_ID; }
        }

        public string Name
        {
            get { return data.Name; }
        }

        public double UU
        {
            get { return UUAmount; }
        }

        public void Save(int Id, byte Number)
        {
            var item = new Model.EF.Cards_TSO();
            item.TSO_ID = data.TSO_ID;
            item.Cards_ID = Id;
            item.Number = Number;
            Model.EF.EntityInstance.DBContext.Cards_TSOSet.Add(item);
        }
    }
}
