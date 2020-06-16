using Kolokwium2.Models;
using System.Threading.Tasks;

namespace Kolokwium2.Services
{
    public interface IMusicDbService
    {
       
        Task GetMusician(int id);
    }
}
