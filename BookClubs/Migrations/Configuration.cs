namespace BookClubs.Migrations
{
    using BookClubs.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BookClubs.Models.BcContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BookClubs.Models.BcContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            #region ENTITY INITIALIZATIONS
            #region IGNORE FOR NOW
            /*List<Book> books = new List<Book>()
        {
            new Book()
            {
            Isbn = "9780982692639",
            Title = "Embedded Systems with ARM Cortex-M Microcontrollers in Assembly Language and C",
            Authors = new List<Author> { new Author { FirstName = "Yifeng", LastName = "Zhu" } }
            },
            new Book()
            {
            Isbn = "9780262033848",
            Title = "Introduction to Algorithms",
            Authors = new List<Author>
            {
                new Author { FirstName = "Thomas", LastName = "Cormen" },
                new Author { FirstName = "Charles", LastName = "Leiserson" },
                new Author { FirstName = "Ronald", LastName = "Rivest" },
                new Author { FirstName = "Clifford", LastName = "Stein" }}
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
            },
            new Book()
            {
            Isbn = "9781476733951",
            Title = "Wool",
            Authors = new List<Author> { new Author { FirstName = "Hugh", LastName = "Howey" } },
            }
            };
            List<GroupEvent> groupOneEvents = new List<GroupEvent>()
        {
            new GroupEvent
            {
                Address = "1234 A Street",
                City = "Salem",
                State = "OR",
                ZipCode = "97302",
                DateTime = new DateTime(2017, 9, 30, 18, 0, 0),
                Book = books[4]
            },
            new GroupEvent
            {
                Address = "1234 A Street",
                City = "Salem",
                State = "OR",
                ZipCode = "97302",
                DateTime = new DateTime(2017, 10, 7, 18, 0, 0),
                Book = books[4]
            },
            new GroupEvent
            {
                Address = "1234 A Street",
                City = "Salem",
                State = "OR",
                ZipCode = "97302",
                DateTime = new DateTime(2017, 10, 14, 18, 0, 0),
                Book = books[4]
            },
            new GroupEvent
            {
                Address = "1234 A Street",
                City = "Salem",
                State = "OR",
                ZipCode = "97302",
                DateTime = new DateTime(2017, 10, 21, 18, 0, 0),
                Book = books[1]
            }
            };
            List<GroupEvent> groupTwoEvents = new List<GroupEvent>()
        {
            new GroupEvent
            {
                Address = "1111 B Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97211",
                DateTime = new DateTime(2017, 9, 30, 18, 0, 0),
                Book = books[2]
            },
            new GroupEvent
            {
                Address = "1111 B Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97211",
                DateTime = new DateTime(2017, 10, 2, 18, 0, 0),
                Book = books[2]
            },
            new GroupEvent
            {
                Address = "2222 C Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97280",
                DateTime = new DateTime(2017, 10, 4, 18, 0, 0),
                Book = books[2]
            },
            new GroupEvent
            {
                Address = "1111 B Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97211",
                DateTime = new DateTime(2017, 10, 6, 18, 0, 0),
                Book = books[2]
            }
        };
            List<GroupEvent> groupThreeEvents = new List<GroupEvent>()
        {
            new GroupEvent
            {
                Address = "1111 B Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97211",
                DateTime = new DateTime(2017, 9, 30, 18, 0, 0),
                Book = books[0]
            },
            new GroupEvent
            {
                Address = "1111 B Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97211",
                DateTime = new DateTime(2017, 10, 2, 18, 0, 0),
                Book = books[3]
            },
            new GroupEvent
            {
                Address = "2222 C Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97280",
                DateTime = new DateTime(2017, 10, 4, 18, 0, 0),
                Book = books[0]
            },
            new GroupEvent
            {
                Address = "1111 B Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97211",
                DateTime = new DateTime(2017, 10, 6, 18, 0, 0),
                Book = books[3]
            },
            new GroupEvent
            {
                Address = "1111 B Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97211",
                DateTime = new DateTime(2017, 10, 8, 18, 0, 0),
                Book = books[0]
            },
            new GroupEvent
            {
                Address = "1111 B Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97211",
                DateTime = new DateTime(2017, 10, 15, 18, 0, 0),
                Book = books[3]
            },
            new GroupEvent
            {
                Address = "2222 C Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97280",
                DateTime = new DateTime(2017, 10, 17, 18, 0, 0),
                Book = books[0]
            },
            new GroupEvent
            {
                Address = "1111 B Street",
                City = "Portland",
                State = "OR",
                ZipCode = "97211",
                DateTime = new DateTime(2017, 10, 28, 18, 0, 0),
                Book = books[3]
            }
        };
            List<Group> groups = new List<Group>()
            {
                new Group
                {
                    Name = "Group 1",
                    City = "Salem",
                    State = "OR",
                    GroupEvents = groupOneEvents,
                    Users = new List<ApplicationUser>()
                },
                new Group
                {
                    Name = "Group 2",
                    City = "Portland",
                    State = "OR",
                    GroupEvents = groupTwoEvents,
                    Users = new List<ApplicationUser>()
                },
                new Group
                {
                    Name = "Group 3",
                    City = "Portland",
                    State = "OR",
                    GroupEvents = groupThreeEvents,
                    Users = new List<ApplicationUser>()
                }
            };*/
            #endregion
            List<User> users = new List<User>()
            {
                new User
                {
                    FirstName="Tim",
                    LastName = "Peterson",
                    Email = "Tim.Peterson@gmail.com",
                    UserName = "Tim.Peterson@gmail.com",
                    Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                    "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                    "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                    "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur " +
                    "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt " +
                    "mollit anim id est laborum.",
                    ProfilePictureUrl = "/App_Profile_Pictures/Seed_Images/1.jpg",
                    Groups = new List<Group>()
                },
                new User
                {
                    FirstName="James",
                    LastName = "Johnson",
                    Email = "James.Johnson@yahoo.com",
                    UserName = "James.Johnson@yahoo.com",
                    Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                    "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                    "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                    "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur " +
                    "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt " +
                    "mollit anim id est laborum.",
                    ProfilePictureUrl = "/App_Profile_Pictures/Seed_Images/2.jpg",
                    Groups = new List<Group>()
                },
                new User
                {
                    FirstName="Dustin",
                    LastName = "Franklin",
                    Email = "MrDusty@msn.com",
                    UserName = "MrDusty@msn.com",
                    Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                    "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                    "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                    "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur " +
                    "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt " +
                    "mollit anim id est laborum.",
                    ProfilePictureUrl = "/App_Profile_Pictures/Seed_Images/3.jpg",
                    Groups = new List<Group>()
                },
                new User
                {
                    FirstName="Tran",
                    LastName = "Nguyen",
                    Email = "Tran.Nguyen@gmail.com",
                    UserName = "Tran.Nguyen@gmail.com",
                    Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                    "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                    "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                    "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur " +
                    "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt " +
                    "mollit anim id est laborum.",
                    ProfilePictureUrl = "/App_Profile_Pictures/Seed_Images/4.jpg",
                    Groups = new List<Group>()
                },
                new User
                {
                    FirstName="Rita",
                    LastName = "James",
                    Email = "Rita.James@gmail.com",
                    UserName = "Rita.James@gmail.com",
                    Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                    "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                    "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                    "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur " +
                    "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt " +
                    "mollit anim id est laborum.",
                    ProfilePictureUrl = "/App_Profile_Pictures/Seed_Images/5.jpg",
                    Groups = new List<Group>()
                },
                new User
                {
                    FirstName="Eduard",
                    LastName = "Worth",
                    Email = "Worth.Ed@gmail.com",
                    UserName = "Worth.Ed@gmail.com",
                    Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                    "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                    "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                    "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur " +
                    "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt " +
                    "mollit anim id est laborum.",
                    ProfilePictureUrl = "/App_Profile_Pictures/Seed_Images/6.jpg",
                    Groups = new List<Group>()
                },
                new User
                {
                    FirstName="Tim",
                    LastName = "Wong",
                    Email = "Tim.Wong@gmail.com",
                    UserName = "Tim.Wong@gmail.com",
                    Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                    "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                    "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                    "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur " +
                    "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt " +
                    "mollit anim id est laborum.",
                    ProfilePictureUrl = "/App_Profile_Pictures/Seed_Images/7.jpg",
                    Groups = new List<Group>()
                },
                new User
                {
                    FirstName="Ted",
                    LastName = "Thompson",
                    Email = "Ted.Thompson@gmail.com",
                    UserName = "Ted.Thompson@gmail.com",
                    Biography = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " +
                    "sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
                    "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " +
                    "nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit " +
                    "in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur " +
                    "sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt " +
                    "mollit anim id est laborum.",
                    ProfilePictureUrl = "/App_Profile_Pictures/Seed_Images/8.jpg",
                    Groups = new List<Group>()
                }
            };

            //users[0].Groups.Add(groups[0]);
            //users[0].Groups.Add(groups[2]);
            //users[1].Groups.Add(groups[0]);
            //users[1].Groups.Add(groups[2]);
            //users[2].Groups.Add(groups[2]);
            //users[2].Groups.Add(groups[1]);
            //users[3].Groups.Add(groups[2]);
            //users[3].Groups.Add(groups[1]);
            //users[4].Groups.Add(groups[0]);
            //users[4].Groups.Add(groups[1]);
            //users[5].Groups.Add(groups[0]);
            //users[5].Groups.Add(groups[1]);
            //users[6].Groups.Add(groups[0]);
            //users[7].Groups.Add(groups[1]);
            #endregion

            try
            {
                //Remember: 1. Add everything up to any many-to-many relationships then save changes.
                //          2. Add any many-to-many relationships using only one of the objects involved in the relationship.
                //              IE: Don't add user[1] to group[1] then add group[1] to user[1].
                //          3. When dealing with ApplicationUser, it is easiest to seed it 
                //          using a UserManager. This way, it populates columns like PasswordHash, etc.
                //context.Books.AddOrUpdate(books.ToArray());
                //context.GroupEvents.AddOrUpdate(groupOneEvents.ToArray());
                //context.GroupEvents.AddOrUpdate(groupTwoEvents.ToArray());
                //context.GroupEvents.AddOrUpdate(groupThreeEvents.ToArray());

                //context.SaveChanges();

                var userManager = new UserManager<User>(new UserStore<User>(context));
                foreach (var user in users)
                {
                    userManager.Create(user, "P@ssword123");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }


        }
    }
}