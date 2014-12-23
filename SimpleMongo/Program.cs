
using System.Windows.Forms;

using MongoDB.Bson;
using MongoDB.Driver;


namespace SimpleMongo
{


    static class Program
    {


        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [System.STAThread]
        public static void Main (string[] args)
        {
            // http://stackoverflow.com/questions/6499268/mongodb-connection-refused
            // I ran into the same issue because I upgraded my mongo using brew. 
            // To fix this issue open the config file /etc/mongod.conf
            // and comment out the "bind_ip" property.

            // http://docs.mongodb.org/ecosystem/tutorial/use-csharp-driver/

            // mongodb://server2/?ssl=true
            // mongodb://[username:password@]hostname[:port][/[database][?options]]
            // string connectionString = "mongodb://localhost:27020/mydb";
            string connectionString = "mongodb://localhost:27017/mydb";
            // string connectionString = "mongodb://localhost:27017";
            MongoServer server = new MongoClient(connectionString).GetServer();
            System.Collections.Generic.List<string> ls = (System.Collections.Generic.List<string>) 
                server.GetDatabaseNames ();

            System.Console.WriteLine (ls.Count);
            System.Console.WriteLine ("Hello World!");
        } // End Sub Main


    } // End Class Program


} // End Namespace SimpleMongo
