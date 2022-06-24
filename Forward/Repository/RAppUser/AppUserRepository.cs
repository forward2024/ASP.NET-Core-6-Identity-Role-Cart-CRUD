using Forward.Data;
using Forward.Models.model;

namespace Forward.Repository.RAppUser
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly ApplicationDbContext _context;
        public AppUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(AppUser user)
        {
            var UserDb = _context.AppUsers.FirstOrDefault(x => x.Id == user.Id);
            if (UserDb != null)
            {
                UserDb.Name = user.Name;
                UserDb.Phone = user.Phone;
                UserDb.Address = user.Address;
                UserDb.City = user.City;
            }
        }
    }
}
