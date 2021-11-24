using System;
using System.Net.Sockets;

namespace Client.Model.EF
{
    class EntityInstance
    {
        public static DataModelContainer DBContext = new DataModelContainer();
        public static int UserID = 1;
     
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