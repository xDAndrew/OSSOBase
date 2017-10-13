using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    class M_Object
    {
        Model.EF.Object data;
        List<Model.EF.Streets> streets = new List<EF.Streets>();
        Model.EF.Streets selectedStreet = null;

        public M_Object(int? ID = null)
        {
            //Тут загружаем список улиц
            streets = Model.EF.EntityInstance.DBContext.StreetsSet.Where(p => true).ToList();
            for (int i = 0; i < streets.Count; i++)
            {
                var temp = new Model.EF.Streets();
                temp.Streets_ID = streets[i].Streets_ID;
                temp.Object = streets[i].Object;
                temp.Name = streets[i].Name + " " + Model.EF.EntityInstance.GetStreetType(streets[i].Type);
                temp.Type = streets[i].Type;
                streets[i] = temp;
            }

            //Тут загружаем объект из БД или пустой объект
            if (ID != null)
            {
                data = Model.EF.EntityInstance.DBContext.ObjectSet.First(p => p.Cards_ID == ID.Value);
                foreach (var item in streets)
                {
                    if (item.Streets_ID == data.Streets_ID) selectedStreet = item;
                }
            }
            else
            {
                data = new Model.EF.Object();
                Owner = "";
                Name = "";
                Room = "";
                Corp = "";
                Home = "";
            }
        }

        public void Save(int ID)
        {
            data.Streets_ID = selectedStreet.Streets_ID;
            //Сохраняем в БД
            if (data.Object_ID == 0)
            {
                data.Cards_ID = ID;
                Model.EF.EntityInstance.DBContext.ObjectSet.Add(data);
            }
            Model.EF.EntityInstance.DBContext.SaveChanges();
        }

        #region Properties
        public string Owner
        {
            get
            {
                return data.Owner;
            }
            set
            {
                data.Owner = value;
            }
        }

        public string Name
        {
            get
            {
                return data.Name;
            }
            set
            {
                data.Name = value;
            }
        }

        public List<Model.EF.Streets> Streets
        {
            get
            {
                return streets;
            }
        }

        public Model.EF.Streets StreetIndex
        {
            get
            {
                return selectedStreet;
            }
            set
            {
                selectedStreet = value;
            }
        }

        public string Home
        {
            get
            {
                return data.Home;
            }
            set
            {
                data.Home = value;
            }
        }

        public string Corp
        {
            get
            {
                return data.Corp;
            }
            set
            {
                data.Corp = value;
            }
        }

        public string Room
        {
            get
            {
                return data.Room;
            }
            set
            {
                data.Room = value;
            }
        }
        #endregion
    }
}
