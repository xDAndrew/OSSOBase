using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.EF
{
    class EntityInstance
    {
        public static DataModelContainer DBContext = new DataModelContainer();
        public static int UserID = 1;

        //<add name="DataModelContainer" connectionString="metadata=res://*/Model.EF.DataModel.csdl|res://*/Model.EF.DataModel.ssdl|res://*/Model.EF.DataModel.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=(localdb)\Projects;initial catalog=OSSOBase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />

        public static string GetStreetType(int index)
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
        }
    }
}