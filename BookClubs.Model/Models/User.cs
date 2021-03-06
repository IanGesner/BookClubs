﻿using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookClubs.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool Public { get; set; }

        public virtual ICollection<User> Friends { get; set; }

        public virtual ICollection<GroupWallPost> GroupWallPosts { get; set; }

        public virtual ICollection<Group> GroupsIn { get; set; }
        public virtual ICollection<Group> GroupsOrganized { get; set; }

        public virtual ICollection<FriendRequest> PendingFriendRequests { get; set; }
        public virtual ICollection<FriendRequest> SentFriendRequests { get; set; }

        public virtual ICollection<GroupInvitation> PendingGroupInvitations { get; set; }
        public virtual ICollection<GroupInvitation> SentGroupInvitations { get; set; }

        public virtual ICollection<GroupRequest> PendingGroupRequests { get; set; }
        public virtual ICollection<GroupRequest> SentGroupRequests { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }


}