using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookClubs.Helpers
{
    internal static class BcHelper
    {
        internal static string GetFileExtension(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var extIndex = fileName.LastIndexOf('.') + 1;
           
            return fileName.Substring(extIndex, fileName.Length - extIndex);
        }

        internal static void AddOrUpdateDisplayPicture(string userId)
        {
            throw new NotImplementedException();
        }

        internal static string GetSrcURL(string profilePictureUrl)
        {
            return profilePictureUrl.Replace('\\', '/');
        }


        //public static HttpPostedFileBase GetProfilePicture(string username)
        //{
        //    HttpPostedFile file = new HttpPostedFile();

        //    return new HttpPostedFileBase();
        //}
    }
}