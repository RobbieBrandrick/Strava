using System;
using System.Threading.Tasks;
using Strava.Core.Data;
using Strava.Core.Models;

namespace Strava.Core.Services
{
    public interface IStravaApiTransactionService
    {
        Task Add(string resource, string request, string response);
    }

    public class StravaApiTransactionService : IStravaApiTransactionService
    {
        private readonly StravaDbContext _dbContext;

        public StravaApiTransactionService(StravaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task Add(string resource, string request, string response)
        {

            var transaction = new StravaApiTransaction()
            {
                CreateDate = DateTime.Now,
                Resouce = resource,
                Request = request,
                Response = response
            };

            _dbContext.StravaApiTransactions.Add(transaction);

            await _dbContext.SaveChangesAsync();

        }
        
    }
}