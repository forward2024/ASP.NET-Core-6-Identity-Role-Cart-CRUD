using Forward.Models.model;

namespace Forward.Repository.RAppUser
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        void Update(AppUser user);
    }
}
