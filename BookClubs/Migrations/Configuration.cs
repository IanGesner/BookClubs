namespace BookClubs.Migrations
{
    using BookClubs.Data.Configuration;
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

    internal sealed class Configuration : DbMigrationsConfiguration<BcContext>
    {
        private const int NUM_EVENTS_PER_GROUP = 10;
        private const int NUM_USERS_PER_GROUP = 5;
        private const int MAX_NUM_FRIENDS_PER_USER = 4;

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BcContext context)
        {
            if (Debugger.IsAttached == false)
                Debugger.Launch();

            try
            {
                var userManager = new UserManager<User>(new UserStore<User>(context));

                //Insert users
                var users = InitializeUsers();
                foreach (var user in users)
                {
                    userManager.Create(user, "P@ssword123");
                }

                //Insert groups
                var groups = InitializeGroups();
                SetOrganizers(users, groups);
                foreach (var group in groups)
                {
                    context.Groups.Add(group);
                }

                //Add some users to the groups
                SetMembers(users, groups);
                context.Commit();

                //Create some group events with a book to read
                var books = InitializeBooks();
                var events = InitializeGroupEvents(groups, books);
                foreach (var groupEvent in events)
                {
                    groups[groupEvent.GroupId-1].GroupEvents.Add(groupEvent);

                }

                //Give people a couple friends
                SetFriends(users);

                SetFriendRequests(users);

                SetGroupInvites(groups, users);

                SetGroupRequests(groups, users);

                context.Commit();
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

        private void SetGroupRequests(List<Group> groups, List<User> users)
        {
            groups[0].GroupRequests.Add(new GroupRequest
            {
                Body = "Please invite me to your group, so I can read with all of you!",
                Recipient = groups[0].Organizer,
                Sender = users[8],
                TimeStamp = DateTime.Now
            });
        }

        private void SetGroupInvites(List<Group> groups, List<User> users)
        {
            groups[2].GroupInvitations.Add(new GroupInvitation
            {
                Body = "Join our group and come read with us sometime!",
                Recipient = users[0],
                Sender = groups[2].Organizer,
                TimeStamp = DateTime.Now
            });
        }

        private void SetFriendRequests(List<User> users)
        {
            users[5].SentFriendRequests.Add(new FriendRequest
            {
                Recipient = users[0],
                Body = "You should add me, so we can read together sometime! :)",
                TimeStamp = DateTime.Now
            });
        }

        private void SetFriends(List<User> users)
        {
            users[0].Friends.Add(users[1]);
            users[0].Friends.Add(users[2]);
            users[0].Friends.Add(users[3]);

            users[1].Friends.Add(users[2]);
            users[1].Friends.Add(users[3]);
            users[1].Friends.Add(users[4]);

            users[2].Friends.Add(users[3]);
            users[2].Friends.Add(users[4]);
            users[2].Friends.Add(users[5]);

            users[3].Friends.Add(users[4]);
            users[3].Friends.Add(users[5]);
            users[3].Friends.Add(users[6]);

            users[4].Friends.Add(users[5]);
            users[4].Friends.Add(users[6]);
            users[4].Friends.Add(users[7]);

            users[5].Friends.Add(users[6]);
            users[5].Friends.Add(users[7]);
            users[5].Friends.Add(users[8]);
        }

        private void SetMembers(List<User> users, List<Group> groups)
        {
            users[0].GroupsIn.Add(groups[0]);
            users[1].GroupsIn.Add(groups[0]);
            users[2].GroupsIn.Add(groups[0]);
            users[3].GroupsIn.Add(groups[0]);
            users[4].GroupsIn.Add(groups[0]);
            users[5].GroupsIn.Add(groups[0]);
            users[6].GroupsIn.Add(groups[0]);
            users[7].GroupsIn.Add(groups[0]);

            users[0].GroupsIn.Add(groups[1]);
            users[1].GroupsIn.Add(groups[1]);
            users[2].GroupsIn.Add(groups[1]);
            users[3].GroupsIn.Add(groups[1]);

            users[2].GroupsIn.Add(groups[2]);
            users[3].GroupsIn.Add(groups[2]);
            users[4].GroupsIn.Add(groups[2]);
            users[5].GroupsIn.Add(groups[2]);
            users[6].GroupsIn.Add(groups[2]);
            users[7].GroupsIn.Add(groups[2]);
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

            groups[0].Organizer = users[0];
            groups[1].Organizer = users[1];
            groups[2].Organizer = users[3];
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
                    ProfilePictureUrl = "/App_Images/blank_profile_picture.png",
                    GroupsIn = new List<Group>(),
                    Friends = new List<User>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    PendingFriendRequests = new List<FriendRequest>(),
                    PendingGroupInvitations = new List<GroupInvitation>(),
                    PendingGroupRequests = new List<GroupRequest>(),
                    SentFriendRequests = new List<FriendRequest>(),
                    SentGroupInvitations = new List<GroupInvitation>(),
                    SentGroupRequests = new List<GroupRequest>(),
                    Public = false
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
                    SentGroupRequests = new List<GroupRequest>(),
                    Public = true
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
                    SentGroupRequests = new List<GroupRequest>(),
                    Public = true
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
                    SentGroupRequests = new List<GroupRequest>(),
                    Public = true
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
                    SentGroupRequests = new List<GroupRequest>(),
                    Public = true
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
                    SentGroupRequests = new List<GroupRequest>(),
                    Public = true
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
                    SentGroupRequests = new List<GroupRequest>(),
                    Public = true
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
                    SentGroupRequests = new List<GroupRequest>(),
                    Public = true
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
                    SentGroupRequests = new List<GroupRequest>(),
                    Public = false
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
                    Public = false,
                    GroupPictureUrl = "/App_Images/_Seed_Images/Group_Display_Images/1.jpg",
                    GroupInvitations = new List<GroupInvitation>(),
                    GroupRequests = new List<GroupRequest>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    GroupEvents = new List<GroupEvent>()
                },
                new Group
                {
                    Name = "Group 2 - Public",
                    City = "Portland",
                    State = "OR",
                    GroupInfo = "Lorem ipsum dolor sit amet",
                    Public = true,
                    GroupPictureUrl = "/App_Images/blank_group_profile_picture.jpg",
                    GroupInvitations = new List<GroupInvitation>(),
                    GroupRequests = new List<GroupRequest>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    GroupEvents = new List<GroupEvent>()
                },
                new Group
                {
                    Name = "Group 3 - Public",
                    City = "Portland",
                    State = "OR",
                    GroupInfo = "Lorem ipsum dolor sit amet",
                    Public = true,
                    GroupPictureUrl = "/App_Images/_Seed_Images/Group_Display_Images/3.jpg",
                    GroupInvitations = new List<GroupInvitation>(),
                    GroupRequests = new List<GroupRequest>(),
                    GroupWallPosts = new List<GroupWallPost>(),
                    GroupEvents = new List<GroupEvent>()
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
