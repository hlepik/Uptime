using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IRouteRepository
    {
        Route? GetRouteObject();
    }
}