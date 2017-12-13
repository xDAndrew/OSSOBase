using System.Collections.Generic;
using System.Linq;

namespace Client.Model
{
    class M_Object
    {
        Model.EF.Object data;
        Model.EF.Object objReference;

        List<Model.EF.Streets> streets = new List<EF.Streets>();
        Model.EF.Streets selectedStreet = null;

        private bool changed = false;
        public bool Changed
        {
            get { return changed; }
            set { changed = value; }
        }

        public M_Object(int? ID = null)
        {
            //Тут загружаем список улиц
            streets = Model.EF.EntityInstance.DBContext.StreetsSet.AsNoTracking().Where(p => true).OrderBy(p => p.Name).ToList();
            for (int i = 0; i < streets.Count; i++)
            {
                streets[i].Name += " " + Model.EF.EntityInstance.GetStreetType(streets[i].Type);
            }

            data = new Model.EF.Object();

            //Тут загружаем объект из БД или пустой объект
            if (ID != null)
            {
                objReference = Model.EF.EntityInstance.DBContext.ObjectSet.First(p => p.Cards_ID == ID.Value);
                Owner = objReference.Owner;
                Name = objReference.Name;
                Room = objReference.Room;
                Corp = objReference.Corp;
                Home = objReference.Home;
                data.Streets_ID = objReference.Streets_ID;
                changed = false;
                foreach (var item in streets)
                {
                    if (item.Streets_ID == data.Streets_ID) selectedStreet = item;
                }
            }
            else
            {
                Owner = "";
                Name = "";
                Room = "";
                Corp = "";
                Home = "";
                this.changed = false;
            }
        }

        public void Save(int ID)
        {
            //Сохраняем в БД
            data.Streets_ID = selectedStreet.Streets_ID;
            objReference.Owner = Owner;
            objReference.Name = Name;
            objReference.Room = Room;
            objReference.Corp = Corp;
            objReference.Home = Home;
            if (data.Object_ID == 0)
            {
                data.Cards_ID = ID;
                Model.EF.EntityInstance.DBContext.ObjectSet.Add(data);
            }
            Model.EF.EntityInstance.DBContext.SaveChanges();
        }

        public string Owner
        {
            get { return data.Owner; }
            set 
            { 
                data.Owner = value;
                changed = true;
            }
        }

        public string Name
        {
            get { return data.Name; }
            set 
            { 
                data.Name = value; 
                changed = true; 
            }
        }

        public List<Model.EF.Streets> Streets
        {
            get { return streets; }
        }

        public Model.EF.Streets StreetIndex
        {
            get { return selectedStreet; }
            set 
            { 
                selectedStreet = value; 
                changed = true; 
            }
        }

        public string Home
        {
            get { return data.Home; }
            set 
            { 
                data.Home = value; 
                changed = true; 
            }
        }

        public string Corp
        {
            get { return data.Corp; }
            set 
            { 
                data.Corp = value;
                changed = true;
            }
        }

        public string Room
        {
            get { return data.Room; }
            set 
            { 
                data.Room = value;
                changed = true;
            }
        }
    }
}
