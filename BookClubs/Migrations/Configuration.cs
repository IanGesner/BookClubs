namespace BookClubs.Migrations
{
    using BookClubs.Helpers;
    using BookClubs.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        // These fields control the number of records to seed.
        private const int USERS = 15;
        private const int GROUPS_PER_USER = 5;
        private const int EVENTS_PER_GROUP = 5;

        List<Book> books = new List<Book>
        {
            new Book()
            {
            Isbn = "9780982692639",
            Title = "Embedded Systems with ARM Cortex-M Microcontrollers in Assembly Language and C",
            Authors = new List<Author> { new Author { FirstName = "Yifeng", LastName = "Zhu" } },
            },
            new Book()
            {
            Isbn = "9780262033848",
            Title = "Introduction to Algorithms",
            Authors = new List<Author> {
                new Author { FirstName = "Thomas", LastName = "Cormen" },
                new Author { FirstName = "Charles", LastName = "Leiserson" },
                new Author { FirstName = "Ronald", LastName = "Rivest" },
                new Author { FirstName = "Clifford", LastName = "Stein" } }
            },
            new Book()
            {
            Isbn = "9780201633610",
            Title = "Design Patterns: Elements of Reusable Object-Oriented Software",
            Authors = new List<Author> {
                new Author { FirstName = "Erich", LastName = "Gamma" },
                new Author { FirstName = "Richard", LastName = "Helm" },
                new Author { FirstName = "Ralph", LastName = "Johnson" },
                new Author { FirstName = "John", LastName = "Vlissides" } }
            },
            new Book()
            {
            Isbn = "9780132350884",
            Title = "Clean Code: A Handbook of Agile Software Craftsmanship",
            Authors = new List<Author> { new Author { FirstName = "Robert", LastName = "Martin" } },
            }
        };


        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "BookClubs.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //{
            //    System.Diagnostics.Debugger.Launch();
            //}

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            SeedUsers(userManager, context);
        }

        private void SeedUsers(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            for (int i = 0; i < USERS; i++)
            {
                var userName = "username" + i;
                var email = "email" + i + "@domain.com";
                var firstName = "FirstName" + i;
                var lastName = "LastName" + i;
                var bio = "Biography" + i;

                var newUser = new ApplicationUser()
                {
                    UserName = userName,
                    Email = email,
                    FirstName = firstName,
                    LastName = lastName,
                    Biography = bio,
                    Groups = CreateGroups(context),
                    DisplayPicture = GetDefaultPicture(),
                    ProfilePictures = new List<ProfilePicture>() { GetDefaultPicture() }
                };

                userManager.Create(newUser, "P@ssword123");
            }
        }

        private ICollection<Group> CreateGroups(ApplicationDbContext context)
        {
            var groups = new List<Group>();
            Group group;
            var rand = new Random();

            for (int i = 0; i < GROUPS_PER_USER; i++)
            {
                group = new Group()
                {
                    Name = "Group" + i,
                    City = "City" + i,
                    State = "State" + rand.Next(1, 51),
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

            for (int i = 0; i < EVENTS_PER_GROUP; i++)
            {
                //Build zipcode and a event date
                string zipCode = i.ToString().PadLeft(5, '0');
                int year = rand.Next(2017, 2019);
                int month = rand.Next(1, 13);
                int day = rand.Next(1, 29);
                int bookIndex = rand.Next(0, 4);

                //Create one record
                groupEvent = new GroupEvent()
                {
                    Address = "Address" + groupNumber + i,
                    City = "City" + groupNumber + i,
                    State = "State" + rand.Next(1, 51),
                    ZipCode = zipCode,
                    DateTime = new DateTime(year, month, day),
                    //Book = CreateBookWithAuthor(groupNumber, context)
                    Book = books[bookIndex]
                };

                groupEvents.Add(groupEvent);
            }
            groupEvents.Sort(GroupEventDateComparer.Instance);

            return groupEvents;
        }

        private ProfilePicture GetDefaultPicture()
        {
            var profilePicture = new ProfilePicture()
            {
                Url = "~\\Content\\Images\\blank_profile_picture.png",
                Image = null
            };

            return profilePicture;
        }
    }
}


