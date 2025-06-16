using CMDM.Core.Models;
using CMDM.DAL.Database;
using CMDM.DAL.Repositories.Interfaces;

namespace CMDM.DAL.Repositories
{
    public class CustomerReferenceRepository(ApplicationDbContext context) : BaseRepository<CustomerReference>(context), ICustomerReferenceRepository
    {
    }
}
