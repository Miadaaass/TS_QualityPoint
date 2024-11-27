using TS_QualityPoint.Models;
using System.Threading.Tasks;


namespace TS_QualityPoint.Services
{
    public interface IAddressService
    {
        Task<AddressResponse> StandardizeAddressAsync(string address);
    }
}