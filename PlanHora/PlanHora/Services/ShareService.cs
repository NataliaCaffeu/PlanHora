using Microsoft.Maui.ApplicationModel.DataTransfer;
using System.Threading.Tasks;

namespace PlanHora.Services
{
    public class ShareService
    {
        public async Task CompartirArchivoAsync(string filePath)
        {
            await Share.Default.RequestAsync(new ShareFileRequest
            {
                Title = "Compartir cuadrante semanal",
                File = new ShareFile(filePath)
            });
        }
    }
}
