
using MongoDB.Bson;
using MongoDB.Driver;


using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
//using MongoDB.Driver.Linq;


//using MongoDB.Bson.IO;
//using MongoDB.Bson.Serialization;
//using MongoDB.Bson.Serialization.Attributes;
//using MongoDB.Bson.Serialization.Conventions;
//using MongoDB.Bson.Serialization.IdGenerators;
//using MongoDB.Bson.Serialization.Options;
//using MongoDB.Bson.Serialization.Serializers;
//using MongoDB.Driver.Wrappers;


namespace MongoUpload
{


    // http://stackoverflow.com/questions/4988436/mongodb-gridfs-with-c-how-to-store-files-such-as-images
    public class MongoInit 
    {


        // http://blog.denouter.net/2013/07/c-mongodb-driver-mongocredentials.html
        public void CredentialTest()
        {
            // As of version 1.8 of the official mongoDB c# driver the MongoCredentials-class is gone ...
            // var client = new MongoClient("mongodb://192.168.1.2");
            // var server = client.GetServer();
            // var db = server.GetDatabase("myDatabase", new MongoCredentials("myUser", "myPassword"));


            MongoClientSettings cls = new MongoClientSettings()
            {
                Credentials = new MongoCredential[] { MongoCredential.CreateMongoCRCredential("myDatabase", "myUser", "myPassword") },
                Server = new MongoServerAddress("192.168.1.2")
            };

            MongoClient client = new MongoClient(cls);
            MongoServer server = client.GetServer();
            MongoDatabase db = server.GetDatabase("myDatabase");
        } // End Sub CredentialTest



        public class Hamster
        {
            public string Name;
        } // End Class CreateIndex


        // http://stackoverflow.com/questions/17807577/how-to-create-indexes-in-mongodb-via-net
        public void HowToCreateIndex()
        {
            MongoClient client = new MongoClient("mongodb://localhost");
            MongoDatabase db = client.GetServer().GetDatabase("db");
            MongoCollection<Hamster> collection = db.GetCollection<Hamster>("Hamsters");

            collection.CreateIndex(IndexKeys<Hamster>.Ascending(_ => _.Name));

            //collection.EnsureIndex(new IndexKeysBuilder().Ascending("EmailAddress"));
        } // End Sub CreateIndex


        // Following example show how to save file and read back from gridfs(using official mongodb driver):
        // http://www.mongovue.com/
        public void Test()
        {
            MongoDB.Driver.MongoServer server = MongoServer.Create("mongodb://localhost:27020");
            MongoDB.Driver.MongoDatabase database = server.GetDatabase("tesdb");

            string fileName = "D:\\Untitled.png";
            string newFileName = "D:\\new_Untitled.png";
            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open))
            {
                MongoDB.Driver.GridFS.MongoGridFSFileInfo gridFsInfo = database.GridFS.Upload(fs, fileName);
                BsonValue fileId = gridFsInfo.Id;

                ObjectId oid = new ObjectId(fileId.AsByteArray);
                MongoDB.Driver.GridFS.MongoGridFSFileInfo file = database.GridFS.FindOne(Query.EQ("_id", oid));

                using (System.IO.Stream stream = file.OpenRead())
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, (int)stream.Length);
                    using (System.IO.FileStream newFs = new System.IO.FileStream(newFileName, System.IO.FileMode.Create))
                    {
                        newFs.Write(bytes, 0, bytes.Length);
                    } // End Using newFs

                } // End Using stream

            } // End using fs

        } // End Sub Test


        class Doc
        {
            public ObjectId Id { get; set; }
            public string DocName { get; set; }
            public ObjectId DocId { get; set; }
        }


        static void Test2()
        {

            MongoServer ms = MongoServer.Create();
            string _dbName = "docs";

            MongoDatabase md = ms.GetDatabase(_dbName);
            if (!md.CollectionExists(_dbName))
            {
                md.CreateCollection(_dbName);
            } // End if (!md.CollectionExists(_dbName)) 

            MongoCollection<Doc> _documents = md.GetCollection<Doc>(_dbName);
            _documents.RemoveAll();
            //add file to GridFS

            MongoGridFS gfs = new MongoGridFS(md);
            MongoGridFSFileInfo gfsi = gfs.Upload(@"c:\mongodb.rtf");
            _documents.Insert(new Doc()
                {
                    DocId = gfsi.Id.AsObjectId,
                    DocName = @"c:\foo.rtf"
                }
            );

            foreach (Doc item in _documents.FindAll())
            {

                ObjectId _documentid = new ObjectId(item.DocId.ToString());
                MongoGridFSFileInfo _fileInfo = md.GridFS.FindOne(Query.EQ("_id", _documentid));
                gfs.Download(item.DocName, _fileInfo);
                System.Console.WriteLine("Downloaded {0}", item.DocName);
                System.Console.WriteLine("DocName {0} dowloaded", item.DocName);
            } // Next item

            //System.Console.ReadKey();
        } // End Sub Test2


        // http://stackoverflow.com/questions/7201847/how-to-get-the-mongo-database-specified-in-connection-string-in-c-sharp
        public void Test3()
        {
            string connectionString = "mongodb://localhost:27020/mydb";
            MongoServer _server = new MongoClient(connectionString).GetServer();

            // var _serverOld = MongoServer.Create(connectionString);
            
            //take database name from connection string
            string _databaseName = MongoUrl.Create(connectionString).DatabaseName;


            //and then get database by database name:
            MongoDB.Driver.MongoDatabase thedb = _server.GetDatabase(_databaseName);


            // Connectionstring:
            MongoDB.Driver.MongoUrl csb = MongoUrl.Create(connectionString);
            string omdb = csb.DatabaseName;

            // E.g.
            MongoClient client = new MongoClient(connectionString);
            MongoDatabase db = client.GetServer().GetDatabase(new MongoUrl(connectionString).DatabaseName);
            MongoCollection<MongoDB.Bson.BsonDocument> collection = db.GetCollection("mycollection");

            // var spec = new Document("Name", new MongoRegex(string.Format("^{0}",searchKey), "i"));
            // collection.Find(spec);
        } // End Sub Test3


    } // End Class MongoInit


} // End Namespace MongoUpload
