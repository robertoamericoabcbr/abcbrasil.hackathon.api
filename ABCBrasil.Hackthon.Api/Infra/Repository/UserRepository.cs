using ABCBrasil.Hackthon.Api.Domain.Interfaces.Repository;
using ABCBrasil.Hackthon.Api.Domain.Models;
using ABCBrasil.Hackthon.Api.Infra.Contexts;

namespace ABCBrasil.Hackthon.Api.Infra.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}