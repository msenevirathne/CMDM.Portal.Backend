using CMDM.Core.Models;
using CMDM.DAL.Database;
using CMDM.DAL.Repositories.Interfaces;

namespace CMDM.DAL.Repositories
{
    public class CustomerMasterRepository(ApplicationDbContext context) : BaseRepository<CustomerMaster>(context), ICustomerMasterRepository
    {
    }
}
