
using MongoDB.Bson;
using MongoDB.Driver;


// Additionally you will frequently add some of the following using statements:
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;




// http://kkovacs.eu/cassandra-vs-mongodb-vs-couchdb-vs-redis
// http://try.mongodb.org/?_ga=1.228225109.975985742.1418036374



// http://docs.mongodb.org/manual/tutorial/install-mongodb-on-windows/
// D:\Programme\MongoDB\bin\mongod.exe --dbpath D:\Programme\MongoDB\data



namespace MongoUpload
{


    public class QueryTest  
    {


        public void Test()
        {
            MongoServerSettings settings = new MongoServerSettings();

            // var x = new MongoIdentity();


            //settings.Credentials = new MongoCredential("mechanism", "Mongoid", "mongoevid");

            //var settings = new MongoServerSettings()
            //{

            //    new global::MongoDB.Driver.MongoCredential("mech" "mongoid",)


            //    ConnectionMode = ConnectionMode.ReplicaSet,
            //    ReplicaSetName = "mongors",
            //    ReadPreference = new ReadPreference(ReadPreferenceMode.PrimaryPreferred),
            //    SafeMode = SafeMode.True,
            //    DefaultCredentials = new MongoCredentials("user", "password"),
            //    Servers = new[] { new MongoServerAddress("server.net", 27020), 
            //            new MongoServerAddress("server.net", 27019),
            //            new MongoServerAddress("server.net", 27018)}

            //};


            string id = "bla";

            while (true)
            {
                MongoServer server = MongoServer.Create(settings);
                MongoDatabase db = server.GetDatabase("db");
                MongoCollection<TaggedAction> collection = db.GetCollection<TaggedAction>("actions");
                IMongoQuery query = Query.EQ("_id", id);
                TaggedAction entity = collection.FindOne(query);

                System.Console.WriteLine(System.DateTime.Now + " " + entity.ActionName);
                System.Threading.Thread.Sleep(2500);
            } // Whend 

        } // End Sub Test 


        public class TaggedAction
        {
            public string ActionName;   
        }


    } // End Class QueryTest


} // End Namespace MongoUpload
