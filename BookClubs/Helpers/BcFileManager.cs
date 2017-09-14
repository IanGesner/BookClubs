using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BookClubs.Helpers
{



    public class BcFileManager : IFileManager
    {
        private readonly string _profilePicDir = ConfigurationManager.AppSettings["ProfilePicSaveDirectory"];
        private readonly string _defaultPicDir = ConfigurationManager.AppSettings["DefaultProfilePicLocation"];

        public string ProfilePicDir { get { return _profilePicDir; } }
        public string DefaultPicDir { get { return _defaultPicDir; } }

        public string BuildUrl(string[] parts, SlashDirection slashType)
        {
            foreach (var part in parts)
            {
                //part.Replace()
            }

            return String.Empty;
        }

        public void DeleteFile(string path)
        {
            throw new NotImplementedException();
        }
    }

    public interface IFileManager
    {
        string BuildUrl(string[] parts, SlashDirection slashType);
        void DeleteFile(string path);
    }

    public enum SlashDirection { Foward, Backward };
}