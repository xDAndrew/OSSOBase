using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client.Model.EquipmentModel
{
    class Branch
    {
        Model.EF.Branch data;
        double UUAmount = 0.0;
        int[] arr = new int[15];

        bool nullVisible = false;

        //Прямо из БД он подтянет все нужны сведения
        public Branch(Model.EF.Branch data)
        {
            //Парсим бинарные данные из даты в массив
            this.data = data;

            var buff = new MemoryStream(data.Data);
            arr = (int[])new BinaryFormatter().Deserialize(buff);
        }

        public Branch(byte num)
        {
            //Создается новая запись БД
            this.data = new Model.EF.Branch();
            this.data.Number = num;
            this.data.Name = "Привет";
            for (int i = 0; i < 15; i++)
            {
                arr[i] = i;
            }
        }

        #region Arguments
        public string arg0
        {
            get 
            {
                if (arr[0] > 0) return arr[0].ToString();
                if (arr[0] == 0 && nullVisible) return arr[0].ToString();
                return "";
            }
            set 
            { 
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[0] = res;
                }
            }
        }

        public string arg1
        {
            get
            {
                if (arr[1] > 0) return arr[1].ToString();
                if (arr[1] == 0 && nullVisible) return arr[1].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[1] = res;
                }
            }
        }

        public string arg2
        {
            get
            {
                if (arr[2] > 0) return arr[2].ToString();
                if (arr[2] == 0 && nullVisible) return arr[2].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[2] = res;
                }
            }
        }

        public string arg3
        {
            get
            {
                if (arr[3] > 0) return arr[3].ToString();
                if (arr[3] == 0 && nullVisible) return arr[3].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[3] = res;
                }
            }
        }

        public string arg4
        {
            get
            {
                if (arr[4] > 0) return arr[4].ToString();
                if (arr[4] == 0 && nullVisible) return arr[4].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[4] = res;
                }
            }
        }

        public string arg5
        {
            get
            {
                if (arr[5] > 0) return arr[5].ToString();
                if (arr[5] == 0 && nullVisible) return arr[5].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[5] = res;
                }
            }
        }

        public string arg6
        {
            get
            {
                if (arr[6] > 0) return arr[6].ToString();
                if (arr[6] == 0 && nullVisible) return arr[6].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[6] = res;
                }
            }
        }

        public string arg7
        {
            get
            {
                if (arr[7] > 0) return arr[7].ToString();
                if (arr[7] == 0 && nullVisible) return arr[7].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[7] = res;
                }
            }
        }

        public string arg8
        {
            get
            {
                if (arr[8] > 0) return arr[8].ToString();
                if (arr[8] == 0 && nullVisible) return arr[8].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[8] = res;
                }
            }
        }

        public string arg9
        {
            get
            {
                if (arr[9] > 0) return arr[9].ToString();
                if (arr[9] == 0 && nullVisible) return arr[9].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[9] = res;
                }
            }
        }

        public string arg10
        {
            get
            {
                if (arr[10] > 0) return arr[10].ToString();
                if (arr[10] == 0 && nullVisible) return arr[10].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[10] = res;
                }
            }
        }

        public string arg11
        {
            get
            {
                if (arr[11] > 0) return arr[11].ToString();
                if (arr[11] == 0 && nullVisible) return arr[11].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[11] = res;
                }
            }
        }

        public string arg12
        {
            get
            {
                if (arr[12] > 0) return arr[12].ToString();
                if (arr[12] == 0 && nullVisible) return arr[12].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[12] = res;
                }
            }
        }

        public string arg13
        {
            get
            {
                if (arr[13] > 0) return arr[13].ToString();
                if (arr[13] == 0 && nullVisible) return arr[13].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[13] = res;
                }
            }
        }

        public string arg14
        {
            get
            {
                if (arr[14] > 0) return arr[14].ToString();
                if (arr[14] == 0 && nullVisible) return arr[14].ToString();
                return "";
            }
            set
            {
                int res;
                if (int.TryParse(value, out res) && res >= 0)
                {
                    arr[14] = res;
                }
            }
        }
        #endregion

        public void SetVisibleSetting(bool v)
        {
            nullVisible = v;
        }

        public int Number
        {
            get { return data.Number; }
        }

        public string Name
        {
            get { return data.Name; }
            set { data.Name = value; }
        }

        public int this[int index]
        {
            get
            {
                return arr[index];
            }
            set
            {
                arr[index] = value;
            }
        }

        public double Summ
        {
            get { return this.UUAmount; }
            set { this.UUAmount = value; }
        }

        public int SummTSO
        {
            get 
            {
                int summ = 0;
                foreach (var item in arr)
                {
                    summ += item;
                }
                return summ;
            }
        }

        public void Save(int ID)
        {
            var mem = new MemoryStream();
            var formatter = new BinaryFormatter();

            data.Cards_ID = ID;
            
            formatter.Serialize(mem, arr);
            data.Data = mem.GetBuffer();

            Model.EF.EntityInstance.DBContext.BranchSet.Add(data);
            try
            {
                Model.EF.EntityInstance.DBContext.SaveChanges();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }
}
