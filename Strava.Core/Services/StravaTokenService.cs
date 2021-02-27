using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Strava.Core.Data;
using Strava.Core.Models;

namespace Strava.Core.Services
{
    public interface IStravaTokenService
    {
        Task Add(StravaToken token);
        Task<StravaToken> GetToken();
    }

    public class StravaTokenService : IStravaTokenService
    {
        private readonly StravaDbContext _dbContext;

        public StravaTokenService(StravaDbContext dbContext)
        {
            
            _dbContext = dbContext;
            
        }

        public async Task Add(StravaToken token)
        {

            _dbContext.Tokens.Add(token);

            await _dbContext.SaveChangesAsync();

        }

        public async Task<StravaToken> GetToken()
        {

           StravaToken token =  await _dbContext
               .Tokens
               .OrderByDescending(e => e.Expiry)
               .FirstOrDefaultAsync();

           return token;

        }
        
    }
}