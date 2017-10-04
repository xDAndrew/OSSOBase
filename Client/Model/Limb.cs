using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    class Limb
    {
        EF.Limb data;
        int[] arr = new int[15];

        public Limb(EF.Limb data)
        {
            this.data = data;
        }

        public int Number
        {
            get
            {
                return data.Number;
            }
        }

        public string Name
        {
            get
            {
                return data.Name;
            }
        }

        #region arguments
        public string arg0
        {
            get
            {
                return arr[0].ToString();
            }
        }

        public string arg1
        {
            get
            {
                return arr[1].ToString();
            }
        }

        public string arg2
        {
            get
            {
                return arr[2].ToString();
            }
        }

        public string arg3
        {
            get
            {
                return arr[3].ToString();
            }
        }

        public string arg4
        {
            get
            {
                return arr[4].ToString();
            }
        }

        public string arg5
        {
            get
            {
                return arr[5].ToString();
            }
        }

        public string arg6
        {
            get
            {
                return arr[6].ToString();
            }
        }

        public string arg7
        {
            get
            {
                return arr[7].ToString();
            }
        }

        public string arg8
        {
            get
            {
                return arr[8].ToString();
            }
        }

        public string arg9
        {
            get
            {
                return arr[9].ToString();
            }
        }

        public string arg10
        {
            get
            {
                return arr[10].ToString();
            }
        }

        public string arg11
        {
            get
            {
                return arr[11].ToString();
            }
        }

        public string arg12
        {
            get
            {
                return arr[12].ToString();
            }
        }

        public string arg13
        {
            get
            {
                return arr[13].ToString();
            }
        }

        public string arg14
        {
            get
            {
                return arr[14].ToString();
            }
        }
        #endregion

        public static byte[] getBinary()
        {
            int bin = 123;
            return BitConverter.GetBytes(bin);
        }
    }
}
