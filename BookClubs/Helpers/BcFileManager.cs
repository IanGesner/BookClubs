using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BookClubs.Helpers
{
    public class BcFileManager : IFileManager
    {
        public string BuildPath(string[] parts, ForReferenceBy referenceBy)
        {
            List<string> newUrlParts = new List<string>();
            string currentPart;

            if (parts != null)
            {
                foreach (var part in parts)
                {
                    if (part != null)
                    {
                        if (referenceBy == ForReferenceBy.Client)
                        {
                            currentPart = part.Replace("\\", "/");
                            if (!currentPart.StartsWith("/"))
                                currentPart = currentPart.Insert(0, "/");
                            if (currentPart.EndsWith("/"))
                                currentPart = currentPart.Remove(currentPart.Length - 1);

                            newUrlParts.Add(currentPart);
                        }
                        else if (referenceBy == ForReferenceBy.Server)
                        {
                            currentPart = part.Replace("/", "\\");
                            if (!currentPart.StartsWith("\\"))
                                currentPart = currentPart.Insert(0, "\\");
                            if (currentPart.EndsWith("\\"))
                                currentPart = currentPart.Remove(currentPart.Length - 1);

                            newUrlParts.Add(currentPart);
                        }
                    }
                }

                var url = String.Concat(newUrlParts);

                return url;
            }

            return String.Empty;
        }

        public string ConvertPath(string path, ForReferenceBy referenceBy)
        {
            if (referenceBy == ForReferenceBy.Client)
                return path.Replace("\\", "/");

            return path.Replace("/", "\\");
        }

        public void DeleteFile(string path, HttpServerUtilityBase server)
        {
            System.IO.File.Delete(server.MapPath(path));
        }
        public string GetFileExtension(HttpPostedFileBase file)
        {
            var fileName = file.FileName;
            var extIndex = fileName.LastIndexOf('.') + 1;

            return fileName.Substring(extIndex, fileName.Length - extIndex);
        }

        public string MapServerPath(string path, HttpServerUtilityBase httpServerUtilityBase)
        {
            return httpServerUtilityBase.MapPath(path);
        }
    }

    public interface IFileManager
    {
        string BuildPath(string[] parts, ForReferenceBy referenceBy);
        string ConvertPath(string path, ForReferenceBy referenceBy);
        void DeleteFile(string path, HttpServerUtilityBase server);
        string GetFileExtension(HttpPostedFileBase file);
        string MapServerPath(string path, HttpServerUtilityBase httpServerUtilityBase);
    }

    public enum ForReferenceBy { Client, Server };
}