using BookClubs.Data.Configuration;
using System.Data.Entity;

namespace BookClubs.App_Start
{
    public class StoreSeedData : DropCreateDatabaseIfModelChanges<BcContext>
    {
        protected override void Seed(BcContext context)
        {
            base.Seed(context);
        }

        //private static List<User>
    }
}