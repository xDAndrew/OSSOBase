using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    class M_Card
    {
        private Model.EF.Cards data;

        public M_Card(Model.EF.Cards card)
        {
            this.data = card;
        }

        public string Owner
        {
            get { return "Owner"; }
        }

        public string ObjectView
        {
            get { return "Objet"; }
        }

        public string Address
        {
            get { return "Address"; }
        }

        public double UU
        {
            get { return data.Amount; }
        }
    }
}
