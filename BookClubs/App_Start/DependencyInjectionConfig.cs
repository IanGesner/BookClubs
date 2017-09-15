﻿using BookClubs.Data;
using BookClubs.Helpers;
using BookClubs.Models;
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
            container.Register<IDataRepository, EfDataRepository>(Lifestyle.Scoped);
            container.Register<IFileManager, BcFileManager>(Lifestyle.Scoped);
            //container.Register<IGroupService, GroupService>(Lifestyle.Scoped);
            //container.Register<IBookService, BookService>(Lifestyle.Scoped);
            //container.Register<IBookProxy, BookProxy>(Lifestyle.Scoped);
            //container.Register<ICreditRatingService, CreditRatingService>();

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}