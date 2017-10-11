using BookClubs.Data;
using BookClubs.Data.Infrastructure;
using BookClubs.Data.Repositories;
using BookClubs.Helpers;
using BookClubs.Models;
using BookClubs.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BookClubs.App_Start
{
    public class DependencyInjectionConfig
    {
        public static void Register()
        {
            // Create the container
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register concrete types
            container.Register<IFileManager, BcFileManager>(Lifestyle.Scoped);
            container.Register<IDbFactory, DbFactory>(Lifestyle.Scoped);
            container.Register<IUnitOfWork, UnitOfWork>(Lifestyle.Scoped);

            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.Register<IUserService, UserService>(Lifestyle.Scoped);

            container.Register<IGroupRepository, GroupRepository>(Lifestyle.Scoped);
            container.Register<IGroupService, GroupService>(Lifestyle.Scoped);

            container.Register<IFriendRequestRepository, FriendRequestRepository>(Lifestyle.Scoped);
            container.Register<IFriendRequestService, FriendRequestService>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}