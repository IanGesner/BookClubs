namespace BookClubs.Migrations
{
    using BookClubs.Data;
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
        private const int NUM_EVENTS_PER_GROUP = 10;
        private const int NUM_USERS_PER_GROUP = 5;
        private const int MAX_NUM_FRIENDS_PER_USER = 4;

        EfDataRepository _repo = new EfDataRepository();
        BcContext _context;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        //RULES
        protected override void Seed(BookClubs.Models.BcContext context)
        {
            this._context = context;
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();


            #region IGNORE
            //List<GroupEvent> groupOneEvents = new List<GroupEvent>()
            //{
            //    new GroupEvent
            //    {
            //        Address = "1234 A Street",
            //        City = "Salem",
            //        State = "OR",
            //        ZipCode = "97302",
            //        DateTime = new DateTime(2017, 9, 30, 18, 0, 0),
            //        Book = books[4]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "1234 A Street",
            //        City = "Salem",
            //        State = "OR",
            //        ZipCode = "97302",
            //        DateTime = new DateTime(2017, 10, 7, 18, 0, 0),
            //        Book = books[4]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "1234 A Street",
            //        City = "Salem",
            //        State = "OR",
            //        ZipCode = "97302",
            //        DateTime = new DateTime(2017, 10, 14, 18, 0, 0),
            //        Book = books[4]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "1234 A Street",
            //        City = "Salem",
            //        State = "OR",
            //        ZipCode = "97302",
            //        DateTime = new DateTime(2017, 10, 21, 18, 0, 0),
            //        Book = books[1]
            //    }
            //};
            //List<GroupEvent> groupTwoEvents = new List<GroupEvent>()
            //{
            //    new GroupEvent
            //    {
            //        Address = "1111 B Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97211",
            //        DateTime = new DateTime(2017, 9, 30, 18, 0, 0),
            //        Book = books[2]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "1111 B Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97211",
            //        DateTime = new DateTime(2017, 10, 2, 18, 0, 0),
            //        Book = books[2]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "2222 C Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97280",
            //        DateTime = new DateTime(2017, 10, 4, 18, 0, 0),
            //        Book = books[2]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "1111 B Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97211",
            //        DateTime = new DateTime(2017, 10, 6, 18, 0, 0),
            //        Book = books[2]
            //    }
            //};
            //List<GroupEvent> groupThreeEvents = new List<GroupEvent>()
            //{
            //    new GroupEvent
            //    {
            //        Address = "1111 B Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97211",
            //        DateTime = new DateTime(2017, 9, 30, 18, 0, 0),
            //        Book = books[0]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "1111 B Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97211",
            //        DateTime = new DateTime(2017, 10, 2, 18, 0, 0),
            //        Book = books[3]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "2222 C Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97280",
            //        DateTime = new DateTime(2017, 10, 4, 18, 0, 0),
            //        Book = books[0]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "1111 B Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97211",
            //        DateTime = new DateTime(2017, 10, 6, 18, 0, 0),
            //        Book = books[3]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "1111 B Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97211",
            //        DateTime = new DateTime(2017, 10, 8, 18, 0, 0),
            //        Book = books[0]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "1111 B Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97211",
            //        DateTime = new DateTime(2017, 10, 15, 18, 0, 0),
            //        Book = books[3]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "2222 C Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97280",
            //        DateTime = new DateTime(2017, 10, 17, 18, 0, 0),
            //        Book = books[0]
            //    },
            //    new GroupEvent
            //    {
            //        Address = "1111 B Street",
            //        City = "Portland",
            //        State = "OR",
            //        ZipCode = "97211",
            //        DateTime = new DateTime(2017, 10, 28, 18, 0, 0),
            //        Book = books[3]
            //    }
            //};
            #endregion

            try
            {
                //Insert users
                var users = InitializeUsers();
                foreach (var user in users)
                {
                    _repo.AddUser(user, "P@ssword123");
                }

                //Insert groups
                var groups = InitializeGroups();
                SetOrganizers(users, groups);
                foreach (var group in groups)
                {
                    _repo.AddGroup(group);
                }

                //Add some users to the groups
                SetMembers(users, groups);
                _repo.Save();

                //Create some group events with a book to read
                var books = InitializeBooks();
                var events = InitializeGroupEvents(groups, books);
                foreach (var groupEvent in events)
                {
                    _repo.AddGroupEvent(groupEvent);
                }

                //Give people a couple friends
                MakeFriends(users);
                _repo.Save();

                //Add some PendingRequests and SentRequests between
                //users that are not already friends
                foreach (var user in users)
                {
                    MakeAFriendRequest(user, users);
                    _repo.Save();
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

        private void MakeAFriendRequest(User sender, List<User> users)
        {
            foreach (var user in users)
            {
                if (user != sender
                    && !RequestExists(user, sender)
                    && !user.Friends.Contains(sender))
                {
                    sender.SentFriendRequests.Add(new FriendRequest
                    {
                        Body = "Hi! Please add me so we can read books together! How fun would that be?",
                        RecipientId = user.Id,
                        //SenderId = innerUser.Id,
                        TimeStamp = DateTime.Now
                    });
                }
            }
        }

        private void MakeFriendRequests(List<User> users)
        {
            //bool seedIt = false;

            //foreach (var user in users)
            //{
            //    foreach (var innerUser in users)
            //    {
            //        seedIt = false;

            //        FriendRequest duplicate = innerUser.PendingFriendRequests
            //                                    .Where(fr => fr.RecipientId == innerUser.Id)                                                
            //                                    .FirstOrDefault();

            //        if (duplicate == null)
            //            seedIt = true;

            //        if (innerUser != user 
            //            && !innerUser.Friends.Contains(user) 
            //            && seedIt)
            //        {
            //            innerUser.SentFriendRequests.Add(new FriendRequest
            //            {
            //                Body = "Hi! Please add me so we can read books together! How fun would that be?",
            //                RecipientId = user.Id,
            //                SenderId = innerUser.Id,
            //                TimeStamp = DateTime.Now
            //            });
            //        }
            //    }
            //}
        }

        private bool RequestExists(User user1, User user2)
        {
            foreach (var request in user1.PendingFriendRequests)
                if (request.SenderId == user2.Id)
                    return true;

            foreach (var request in user2.PendingFriendRequests)
                if (request.SenderId == user1.Id)
                    return true;

            return false;
        }

        private void MakeFriends(List<User> users)
        {
            int seed = (int)DateTime.Now.Ticks;
            Random rng = new Random(seed);

            int friendIndex = 0;

            foreach (var user in users)
            {
                for (int i = 0; i < MAX_NUM_FRIENDS_PER_USER; i++)
                {
                    friendIndex = rng.Next(users.Count);

                    if (users[friendIndex] != user && !user.Friends.Contains(users[friendIndex]))
                        user.Friends.Add(users[friendIndex]);
                }
            }
        }

        private void SetMembers(List<User> users, List<Group> groups)
        {
            int seed = (int)DateTime.Now.Ticks;
            Random rng = new Random(seed);

            int groupIndex = 0;

            foreach (var user in users)
            {
                //put user in random group that they do not organize
                while (groups[groupIndex].OrganizerId == user.Id)
                    groupIndex = rng.Next(groups.Count);

                user.GroupsIn.Add(groups[groupIndex]);
            }
        }

        private List<GroupEvent> InitializeGroupEvents(List<Group> groups, List<Book> books)
        {
            var groupEvents = new List<GroupEvent>();
            Random rng = new Random();

            foreach (var group in groups)
            {
                int bookIndex = rng.Next(books.Count);
                int day = rng.Next(1, 29);
                int hour = rng.Next(23);

                for (int i = 0; i < NUM_EVENTS_PER_GROUP; i++)
                {
                    groupEvents.Add(new GroupEvent
                    {
                        Address = "Group " + group.Id + " address",
                        Book = books[bookIndex],
                        BookId = books[bookIndex].Isbn,
                        City = group.City,
                        State = group.State,
                        GroupId = group.Id,
                        ZipCode = "12345",
                        DateTime = new DateTime(2017, 10, day, hour, 0, 0),
                    });
                }
            }

            return groupEvents;
        }

        private void SetOrganizers(List<User> users, List<Group> groups)
        {
            Random rng = new Random();

            foreach (var group in groups)
            {
                int toSkip = rng.Next(0, users.Count);

                group.OrganizerId = users.Skip(toSkip)
                                         .Take(1)
                                         .Select(u => u.Id)
                                         .FirstOrDefault();
            }
        }

        private List<User> InitializeUsers()
        {
            List<User> users = new List<User>()
            {
                new User
                {
                    FirstName="Ian",
                    LastName = "Gesner",
                    Email = "gesner.ian@gmail.com",
                    UserName = "gesner.ian@gmail.com",
                    Biography = "My biography",
                    ProfilePictureUrl = "/App_Images/_Seed_Images/User_Display_Images/linkedin_profile_pic.jpg",
                    GroupsIn = new List<Group>(),
                    Friends = new List<User>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    PendingFriendRequests = new List<FriendRequest>(),
                    PendingGroupInvitations = new List<GroupInvitation>(),
                    PendingGroupRequests = new List<GroupRequest>(),
                    SentFriendRequests = new List<FriendRequest>(),
                    SentGroupInvitations = new List<GroupInvitation>(),
                    SentGroupRequests = new List<GroupRequest>()
                },
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
                    ProfilePictureUrl = "/App_Images/_Seed_Images/User_Display_Images/1.jpg",
                    GroupsIn = new List<Group>(),
                    Friends = new List<User>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    PendingFriendRequests = new List<FriendRequest>(),
                    PendingGroupInvitations = new List<GroupInvitation>(),
                    PendingGroupRequests = new List<GroupRequest>(),
                    SentFriendRequests = new List<FriendRequest>(),
                    SentGroupInvitations = new List<GroupInvitation>(),
                    SentGroupRequests = new List<GroupRequest>()
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
                    ProfilePictureUrl = "/App_Images/_Seed_Images/User_Display_Images/2.jpg",
                    GroupsIn = new List<Group>(),
                    Friends = new List<User>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    PendingFriendRequests = new List<FriendRequest>(),
                    PendingGroupInvitations = new List<GroupInvitation>(),
                    PendingGroupRequests = new List<GroupRequest>(),
                    SentFriendRequests = new List<FriendRequest>(),
                    SentGroupInvitations = new List<GroupInvitation>(),
                    SentGroupRequests = new List<GroupRequest>()
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
                    ProfilePictureUrl = "/App_Images/_Seed_Images/User_Display_Images/3.jpg",
                    GroupsIn = new List<Group>(),
                    Friends = new List<User>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    PendingFriendRequests = new List<FriendRequest>(),
                    PendingGroupInvitations = new List<GroupInvitation>(),
                    PendingGroupRequests = new List<GroupRequest>(),
                    SentFriendRequests = new List<FriendRequest>(),
                    SentGroupInvitations = new List<GroupInvitation>(),
                    SentGroupRequests = new List<GroupRequest>()
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
                    ProfilePictureUrl = "/App_Images/_Seed_Images/User_Display_Images/4.jpg",
                    GroupsIn = new List<Group>(),
                    Friends = new List<User>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    PendingFriendRequests = new List<FriendRequest>(),
                    PendingGroupInvitations = new List<GroupInvitation>(),
                    PendingGroupRequests = new List<GroupRequest>(),
                    SentFriendRequests = new List<FriendRequest>(),
                    SentGroupInvitations = new List<GroupInvitation>(),
                    SentGroupRequests = new List<GroupRequest>()
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
                    ProfilePictureUrl = "/App_Images/_Seed_Images/User_Display_Images/5.jpg",
                    GroupsIn = new List<Group>(),
                    Friends = new List<User>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    PendingFriendRequests = new List<FriendRequest>(),
                    PendingGroupInvitations = new List<GroupInvitation>(),
                    PendingGroupRequests = new List<GroupRequest>(),
                    SentFriendRequests = new List<FriendRequest>(),
                    SentGroupInvitations = new List<GroupInvitation>(),
                    SentGroupRequests = new List<GroupRequest>()
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
                    ProfilePictureUrl = "/App_Images/_Seed_Images/User_Display_Images/6.jpg",
                    GroupsIn = new List<Group>(),
                    Friends = new List<User>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    PendingFriendRequests = new List<FriendRequest>(),
                    PendingGroupInvitations = new List<GroupInvitation>(),
                    PendingGroupRequests = new List<GroupRequest>(),
                    SentFriendRequests = new List<FriendRequest>(),
                    SentGroupInvitations = new List<GroupInvitation>(),
                    SentGroupRequests = new List<GroupRequest>()
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
                    ProfilePictureUrl = "/App_Images/_Seed_Images/User_Display_Images/7.jpg",
                    GroupsIn = new List<Group>(),
                    Friends = new List<User>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    PendingFriendRequests = new List<FriendRequest>(),
                    PendingGroupInvitations = new List<GroupInvitation>(),
                    PendingGroupRequests = new List<GroupRequest>(),
                    SentFriendRequests = new List<FriendRequest>(),
                    SentGroupInvitations = new List<GroupInvitation>(),
                    SentGroupRequests = new List<GroupRequest>()
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
                    ProfilePictureUrl = "/App_Images/_Seed_Images/User_Display_Images/8.jpg",
                    GroupsIn = new List<Group>(),
                    Friends = new List<User>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    PendingFriendRequests = new List<FriendRequest>(),
                    PendingGroupInvitations = new List<GroupInvitation>(),
                    PendingGroupRequests = new List<GroupRequest>(),
                    SentFriendRequests = new List<FriendRequest>(),
                    SentGroupInvitations = new List<GroupInvitation>(),
                    SentGroupRequests = new List<GroupRequest>()
                }
            };

            return users;
        }

        private List<Group> InitializeGroups()
        {
            List<Group> groups = new List<Group>()
            {
                new Group
                {
                    Name = "Group 1 - Private",
                    City = "Salem",
                    State = "OR",
                    GroupInfo = "Lorem ipsum dolor sit amet",
                    Privacy = GroupPrivacy.Private,
                    GroupPictureUrl = "/App_Images/_Seed_Images/Group_Display_Images/1.jpg",
                },
                new Group
                {
                    Name = "Group 2 - Public",
                    City = "Portland",
                    State = "OR",
                    GroupInfo = "Lorem ipsum dolor sit amet",
                    Privacy = GroupPrivacy.Public,
                    GroupPictureUrl = "/App_Images/blank_group_profile_picture.jpg"
                },
                new Group
                {
                    Name = "Group 3 - Public",
                    City = "Portland",
                    State = "OR",
                    GroupInfo = "Lorem ipsum dolor sit amet",
                    Privacy = GroupPrivacy.Public,
                    GroupPictureUrl = "/App_Images/_Seed_Images/Group_Display_Images/3.jpg"
                }
            };

            return groups;
        }

        private List<Book> InitializeBooks()
        {
            List<Book> books = new List<Book>()
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
                    Authors = new List<Author>
                    {
                        new Author { FirstName = "Erich", LastName = "Gamma" },
                        new Author { FirstName = "Richard", LastName = "Helm" },
                        new Author { FirstName = "Ralph", LastName = "Johnson" },
                        new Author { FirstName = "John", LastName = "Vlissides" }
                    }
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

            return books;
        }
    }
}