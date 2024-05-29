using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrasturcture.Persistence.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
		
		
		}

        public DbSet<SurveyParticipant> SurveyParticipants { get; set; }
        public DbSet<SurveyRoute> SurveyRoutes { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<SurveyOption> SurveyOption { get; set; }




    }
}
