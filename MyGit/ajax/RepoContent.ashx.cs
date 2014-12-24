
using System;
using System.Collections.Generic;
using System.Web;


namespace MyGit.ajax
{


    /// <summary>
    /// Zusammenfassungsbeschreibung für RepoContent
    /// </summary>
    public class RepoContent : IHttpHandler
    {


        public class fsInfo
        {
            public bool isDirectory;
            public string Name;
            public string Path;
            public System.DateTime LastWriteTimeUtc;

            public fsInfo()
            {
            
            }

            public fsInfo(System.IO.FileSystemInfo fi)
            {
                this.isDirectory = (fi.Attributes == System.IO.FileAttributes.Directory);
                this.Name = fi.Name;
                this.Path = fi.FullName;
                this.LastWriteTimeUtc = fi.LastWriteTimeUtc;
            }
            
        }


        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            string path = request.Params["path"];

            if (string.IsNullOrEmpty(path))
                path = @"D:\Stefan.Steiger\Source\Repos";

            // System.IO.Directory.GetFileSystemEntries(path);
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
            System.IO.FileSystemInfo[] fis = di.GetFileSystemInfos();

            List<fsInfo> ls = new List<fsInfo>();

            foreach (System.IO.FileSystemInfo fi in fis)
            {
                ls.Add(new fsInfo(fi));
                Console.WriteLine(fi);
                
            }

            

            // string resp = Newtonsoft.Json.JsonConvert.SerializeObject(ls);
            string resp = resp = MyGit.JsonHelper.SerializePretty(ls);

            context.Response.ContentType = "application/JSON";
            context.Response.Write(resp);
        }


        public class xxx : System.Collections.IComparer
        {

            public int Compare(object x, object y)
            {
                throw new NotImplementedException();
            }
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }


    }


}
