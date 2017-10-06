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
        List<string> streets = new List<string>();
        int sIndex;

        public M_Object(int? ID = null)
        {
            if (ID != null)
            {
                data = Model.EF.EntityInstance.DBContext.ObjectSet.First(p => p.Object_ID == ID.Value);
            }
            else
            {
                data = new Model.EF.Object();
            }

            var sTemp = Model.EF.EntityInstance.DBContext.StreetsSet.OrderBy(p => p.Name).Where(p => p.Streets_ID > 0);
            foreach (var item in sTemp)
            {
                streets.Add(item.Name + " " + GetStreetType(item.Type));
            }

            data.Owner = "Owner";
            data.Name = "Name";
            data.Room = "Room";
            data.Corp = "Corp";
            data.Home = "Home";
            sIndex = -1;
        }

        public void SaveObject()
        {
            data.Streets_ID = sIndex;
            Model.EF.EntityInstance.DBContext.ObjectSet.Add(data);
        }

        private string GetStreetType(int index)
        {
            switch (index)
            {
                case 0:
                    return "тр.";
                case 1:
                    return "пр.";
                case 2:
                    return "ул.";
                case 3:
                    return "пер.";
                case 4:
                    return "пр-д";
                case 5:
                    return "т.";
                case 6:
                    return "пл.";
                default:
                    return "";
            }

        } //Служебная функция

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

        public List<string> Streets
        {
            get
            {
                return streets;
            }
        }

        public int StreetIndex
        {
            get
            {
                return sIndex;
            }
            set
            {
                sIndex = value;
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
