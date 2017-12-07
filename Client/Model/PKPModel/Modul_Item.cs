namespace Client.Model.PKPModel
{
    class Modul_Item
    {
        Model.EF.Modules data;
        int count = 1;

        public delegate void Update();
        public Update UpdateCount = null;

        public Modul_Item(Model.EF.Modules data)
        {
            this.data = data;
        }

        public void Save(int ID)
        {
            var temp = new Model.EF.PKP_Modules();
            temp.PKP_ID = ID;
            temp.Modules_ID = data.Modules_ID;
            temp.Count = count;
            Model.EF.EntityInstance.DBContext.PKP_ModulesSet.Add(temp);
        }

        public int Id
        {
            get { return data.Modules_ID; }
        }

        public string Name
        {
            get { return data.Name; }
        }

        public string Count
        {
            get { return count.ToString(); }
            set 
            {
                int res;
                if (int.TryParse(value, out res) && res > 0)
                {
                    count = res;
                    if (UpdateCount != null)
                    {
                        UpdateCount();
                    }
                }
            }
        }

        public double UUSumm
        {
            get { return (double)count * data.UUCount; }
        }
    }
}
