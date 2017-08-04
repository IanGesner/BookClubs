namespace BookClubs.Migrations
{
    using BookClubs.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private const int GROUP_EVENTS_PER_GROUP = 20;
        private const int USERS_TO_SEED = 20;
        private const int GROUPS_PER_USER = 3;
        private const int AUTHORS_TO_SEED = 50;
        private const int BOOKS_TO_SEED = 10;

        Book book = new Book
        {
            ISBN = "1".PadLeft(13, '0'),
            Title = "We Forgot the Harpoons",
            Authors = new List<Author> { new Author { FirstName = "Ian", LastName = "Gesner" } },
        };

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BookClubs.Models.ApplicationDbContext";
        }

        //TODO: Try yield return for gaining more control over seeding
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            for (int i = 0; i < USERS_TO_SEED; i++)
            {
                var email = "email" + i + "@domain.com";
                var firstName = "FirstName" + i;
                var lastName = "FastName" + i;
                var bio = "Biography" + i;

                var newUser = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Biography = bio,
                    Groups = CreateGroups(context)
                };

                userManager.Create(newUser, "P@ssword123");
            }
        }

        private ICollection<Group> CreateGroups(ApplicationDbContext context)
        {
            var groups = new List<Group>();
            Group group;

            for (int i = 0; i < GROUPS_PER_USER; i++)
            {
                group = new Group()
                {
                    Name = "Group" + i,
                    GroupEvents = CreateGroupEvents(i + 1, context)
                };

                groups.Add(group);
            }

            return groups;
        }

        private ICollection<GroupEvent> CreateGroupEvents(int groupNumber, ApplicationDbContext context)
        {
            GroupEvent groupEvent;
            var groupEvents = new List<GroupEvent>();
            var rand = new Random();

            for (int i = 0; i < GROUP_EVENTS_PER_GROUP; i++)
            {
                //Build zipcode and a event date
                string zipCode = i.ToString().PadLeft(5, '0');
                int year = rand.Next(2017, 2019);
                int month = rand.Next(1, 13);
                int day = rand.Next(1, 29);

                //Create one record
                groupEvent = new GroupEvent()
                {
                    Address = "Address" + groupNumber + i,
                    City = "City" + groupNumber + i,
                    State = "State" + rand.Next(1, 51),
                    ZipCode = zipCode,
                    DateTime = new DateTime(year, month, day),
                    //Book = CreateBookWithAuthor(groupNumber, context)
                    Book = book
                };

                groupEvents.Add(groupEvent);
            }

            return groupEvents;
        }

        //private Book CreateBookWithAuthor(int groupNumber, ApplicationDbContext context)
        //{
        //    //string isbn = groupNumber.ToString().PadLeft(13, '0');

        //    //Book book = context.Books.Find(isbn);

        //    //if (book != null)
        //    //    return book;
        //    //else
        //    //{
        //        var author = new Author
        //        {
        //            FirstName = "AuthorFN" + groupNumber,
        //            LastName = "AuthorLN" + groupNumber,
        //        };

        //        var authors = new List<Author> { author };

        //        Book book = new Book()
        //        {
        //            //ISBN = isbn,
        //            Authors = authors,
        //            Title = "Book" + groupNumber
        //        };

        //        return book;
        //    //}
        //}
    }
}


