using System;

namespace Client.Services.Models
{
    public enum ConnectionType
    {
        Button, Signalization
    }

    public class SimReportData
    {
        public string ContractNumber { get; set; }

        public DateTime ContractDate { get; set; }

        public string Maker { get; set; }

        public ConnectionType ConnectionType { get; set; }

        public string Operator1 { get; set; }

        public string Operator2 { get; set; }

        public string Phone1 { get; set; }

        public string Phone2 { get; set; }
    }
}
