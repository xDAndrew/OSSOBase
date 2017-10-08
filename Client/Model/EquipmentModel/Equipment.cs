using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model.EquipmentModel
{
    class Equipment
    {
        private TSO_Collection TSOModels = new TSO_Collection();
        public TSO_Collection Models
        {
            get { return TSOModels; }
        }

        private ObservableCollection<Branch> branches = new ObservableCollection<Branch>();
        public ObservableCollection<Branch> Branches
        {
            get { return branches; }
        }

        private Branch results;
        public Branch Results
        {
            get { return results; }
        }

        public Equipment()
        {
            results = new Branch(0);

            branches.Add(new Branch(1));
            branches.Add(new Branch(2));
        }
    }
}
