using Client.BindingContexts;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Client.Model.EquipmentModel;
using Client.View;

namespace Client.ViewModel
{
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    internal class TsoEditWindowContext : BaseBindingContext
    {
        public ObservableCollection<Model.EF.TSO> Tso { get; set; } = new ObservableCollection<Model.EF.TSO>();

        public ObservableCollection<Model.EF.TSOGroup> Groups { get; set; } = new ObservableCollection<Model.EF.TSOGroup>();

        private int _groupsIndex = -1;
        public int GroupsIndex
        {
            get => _groupsIndex;
            set 
            {
                _groupsIndex = value;
                LoadTsoList(value);
                if (Tso.Count > 0)
                {
                    TsoIndex = 0;
                }
            }
        }

        private int _tsoIndex = -1;
        public int TsoIndex
        {
            get => _tsoIndex;
            set 
            { 
                _tsoIndex = value;
                OnPropertyChanged("TSOIndex");
                OnPropertyChanged("DelButtonStatus");
                OnPropertyChanged("SetButtonStatus");
            }
        }

        private int _activeTsoIndex = -1;
        public int ActiveTsoIndex
        {
            get => _activeTsoIndex;
            set 
            { 
                _activeTsoIndex = value;
                OnPropertyChanged("ActiveTSOIndex");
                OnPropertyChanged("DelButtonStatus");
                OnPropertyChanged("SetButtonStatus");
            }
        }

        private readonly TSO_Collection _activeTsoListLink;

        private readonly TSO_Collection _tempActiveTsoList = new TSO_Collection();

        public ObservableCollection<TSO_Item> ActiveTsoList => _tempActiveTsoList.Items;
        
        public bool DelButtonStatus => _activeTsoIndex > -1;

        public bool SetButtonStatus => _tsoIndex > -1;

        public string Count => $"Элементы: {_tempActiveTsoList.Items.Count}/15";

        public TsoEditWindowContext(TSO_Collection activeTsoList)
        {
            _activeTsoListLink = activeTsoList;
            _tempActiveTsoList.Items.Clear();
            foreach (var item in activeTsoList.Items)
            {
                _tempActiveTsoList.Items.Add(item);
            }

            var temp = Model.EF.EntityInstance.DBContext.TSOGroupSet.Where(p => p.TSOGroup_ID > 0).ToList();
            foreach (var item in temp)
            {
                Groups.Add(item);
            }

            if (Groups.Count > 0)
            {
                GroupsIndex = 0;
            }
        }

        private void LoadTsoList(int id)
        {
            Tso.Clear();
            var index = Groups[id].TSOGroup_ID;
            var temp = Model.EF.EntityInstance.DBContext.TSOSet.Where(p => p.Group_ID == index).ToList();
            foreach (var item in temp)
            {
                var del = false;
                foreach (var t in _tempActiveTsoList.Items)
                {
                    if (item.TSO_ID != t.Id) continue;
                    del = true;
                    break;
                }

                if (!del)
                {
                    Tso.Add(item);
                }
            }
        }

        public Command SetModule => new Command(obj =>
        {
            if (_tsoIndex > -1 && _tempActiveTsoList.Items.Count < 15)
            {
                _tempActiveTsoList.Items.Add(new TSO_Item(Tso[_tsoIndex]));

                Tso.Remove(Tso[_tsoIndex]);
                if (Tso.Count > 0)
                {
                    TsoIndex = 0;
                }

                if (ActiveTsoList.Count > 0 && ActiveTsoIndex == -1)
                {
                    ActiveTsoIndex = 0;
                }
                OnPropertyChanged("DelButtonStatus");
                OnPropertyChanged("SetButtonStatus");
                OnPropertyChanged("Count");
            }
        });
        
        public Command DelModule => new Command(obj =>
        {
            if (_activeTsoIndex > -1)
            {
                _tempActiveTsoList.Items.Remove(_tempActiveTsoList.Items[_activeTsoIndex]);
            }

            LoadTsoList(GroupsIndex);

            if (Tso.Count > 0)
            {
                TsoIndex = 0;
            }

            if (ActiveTsoList.Count > 0)
            {
                ActiveTsoIndex = 0;
            }

            OnPropertyChanged("DelButtonStatus");
            OnPropertyChanged("SetButtonStatus");
            OnPropertyChanged("Count");
        });

        public Command SaveChange => new Command(obj =>
        {
            _activeTsoListLink.Items.Clear();
            for (var i = 0; i < _tempActiveTsoList.Items.Count; i++)
            {
                _activeTsoListLink.Items.Add(_tempActiveTsoList.Items[i]);
            }

            ((TSOEditWindow)obj).Close();
        });
    }
}
