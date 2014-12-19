
namespace MongoUpload
{


    public class MongoGridFsUsage
    {


        // http://odetocode.com/blogs/scott/archive/2013/04/16/using-gridfs-in-mongodb-from-c.aspx
        public void Test()
        {
            string fileName = "clip_image071.jpg";

            MongoDB.Driver.MongoClient client = new MongoDB.Driver.MongoClient();
            MongoDB.Driver.MongoServer server = client.GetServer();
            MongoDB.Driver.MongoDatabase database = server.GetDatabase("testdb");
            MongoGridFs gridFs = new MongoGridFs(database);


            MongoDB.Bson.ObjectId id = UploadFile(gridFs, fileName);
            byte[] baFile = DownloadFile(gridFs, id);
            DownloadFile(gridFs, id, @"d:\test.rar");
        } // End Sub Test 


        public MongoDB.Bson.ObjectId UploadFile(MongoGridFs gridFs, string fileName)
        {
            MongoDB.Bson.ObjectId id = MongoDB.Bson.ObjectId.Empty;
            // UploadFile
            using (System.IO.Stream file = System.IO.File.OpenRead(fileName))
            {
                id = gridFs.AddFile(file, fileName);
            } // End Using file

            return id;
        } // End Sub UploadFile 


        public void DownloadFile(MongoGridFs gridFs, MongoDB.Bson.ObjectId id, string path)
        {
            using (System.IO.Stream file = gridFs.GetFile(id))
            {
                int bytesRead = 0;
                int offset = 0;

                const int BUFFER_SIZE = 1024 * 1024 * 10;
                byte[] buffer = new byte[BUFFER_SIZE];

                using (System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Create))
                {

                    while ((bytesRead = (int)file.Read(buffer, offset, BUFFER_SIZE)) > 0)
                    {
                        fs.Write(buffer, 0, bytesRead);
                        offset += bytesRead;
                    } // Whend

                    fs.Close();
                } // End Using fs 

            } // End Using file

        } // End Sub DownloadFile


        public byte[] DownloadFile(MongoGridFs gridFs, MongoDB.Bson.ObjectId id)
        {
            byte[] buffer = null;

            using (System.IO.Stream file = gridFs.GetFile(id))
            {
                buffer = new byte[file.Length];
                // note - you'll probably want to read in
                // small blocks or stream to avoid 
                // allocating large byte arrays like this
                file.Read(buffer, 0, (int)file.Length);
            } // End Using file

            return buffer;
        } // End Function DownloadFile


    } // End Class MongoGridFsUsage


} // End Namespace MongoUpload
