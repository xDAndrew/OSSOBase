using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Client.Model.EquipmentModel
{
    class Branch : INotifyPropertyChanged
    {
        Model.EF.Branch data;
        double UUAmount = 0.0;
        int[] arr = new int[15];

        bool nullVisible = false;

        public delegate void Update();
        public Update UpdateStatus;

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
            this.data.Name = "";
            Clear();
        }

        public void Clear()
        {
            arg0 = "0";
            arg1 = "0";
            arg2 = "0";
            arg3 = "0";
            arg4 = "0";
            arg5 = "0";
            arg6 = "0";
            arg7 = "0";
            arg8 = "0";
            arg9 = "0";
            arg10 = "0";
            arg11 = "0";
            arg12 = "0";
            arg13 = "0";
            arg14 = "0";
            Summ = 0;
            
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
                else
                {
                    if (value == "") arr[0] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg0");
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
                else
                {
                    if (value == "") arr[1] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg1");
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
                else
                {
                    if (value == "") arr[2] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg2");
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
                else
                {
                    if (value == "") arr[3] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg3");
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
                else
                {
                    if (value == "") arr[4] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg4");
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
                else
                {
                    if (value == "") arr[5] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg5");
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
                else
                {
                    if (value == "") arr[6] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg6");
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
                else
                {
                    if (value == "") arr[7] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg7");
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
                else
                {
                    if (value == "") arr[8] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg8");
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
                else
                {
                    if (value == "") arr[9] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg9");
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
                else
                {
                    if (value == "") arr[10] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg10");
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
                else
                {
                    if (value == "") arr[11] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg11");
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
                else
                {
                    if (value == "") arr[12] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg12");
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
                else
                {
                    if (value == "") arr[13] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg13");
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
                else
                {
                    if (value == "") arr[14] = 0;
                }
                if (UpdateStatus != null)
                {
                    UpdateStatus();
                }
                OnPropertyChanged("arg14");
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
            get { return arr[index]; }
            set { arr[index] = value; }
        }

        public double Summ
        {
            get { return this.UUAmount; }
            set 
            { 
                this.UUAmount = value;
                OnPropertyChanged("Summ");
            }
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
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
