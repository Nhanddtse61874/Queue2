using Persistence.DataAccess;
using Persistence.Repositories;

namespace Business.Services
{
    public abstract class ServiceBase
    {
        protected readonly IUnitOfWork _unitOfWork;


        protected ServiceBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = new UnitOfWork(new RSDbContext());
        }
    }

    
}
