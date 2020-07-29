using Client.Model;
using Client.Model.EquipmentModel;

namespace Client.Services.Interfaces
{
    public interface IPrintService
    {
        void PrintCardDocument(M_Card cardInfo, M_PKP pkpInfo, Equipment equipment);
    }
}
