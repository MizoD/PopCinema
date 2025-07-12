using PopCinema.Models;
using PopCinema.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PopCinema.Repositories
{
    public class ApplicationUserOTPRepository : Repository<ApplicationUserOTP>, IApplicationUserOTPRepository
    {

        public ApplicationUserOTPRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
