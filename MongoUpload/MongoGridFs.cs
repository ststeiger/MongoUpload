
// http://docs.mongodb.org/ecosystem/tutorial/use-csharp-driver/#csharp-driver-tutorial

// As a minimum add the following using statements to your source files:
using MongoDB.Bson;
using MongoDB.Driver;


// Additionally you will frequently add some of the following using statements:
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;



// In some cases you might add some of the following using statements 
// if you are using some of the optional parts of the C# Driver:
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


    // http://odetocode.com/blogs/scott/archive/2013/04/16/using-gridfs-in-mongodb-from-c.aspx
    // http://odetocode.com/Articles/List
    public class MongoGridFs
    {


        private readonly MongoDatabase _db;
        private readonly MongoGridFS _gridFs;


        public MongoGridFs(MongoDatabase db)
        {
            _db = db;
            _gridFs = _db.GridFS;
        }


        public ObjectId AddFile(System.IO.Stream fileStream, string fileName)
        {
            MongoGridFSFileInfo fileInfo = _gridFs.Upload(fileStream, fileName);
            return (ObjectId)fileInfo.Id;
        }


        public System.IO.Stream GetFile(ObjectId id)
        {
            MongoGridFSFileInfo file = _gridFs.FindOneById(id);
            return file.OpenRead();
        }


    } // End Class MongoGridFs


} // End Namespace MongoUpload
