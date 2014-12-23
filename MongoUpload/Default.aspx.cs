
namespace MongoUpload
{


    public partial class _Default : System.Web.UI.Page
    {


        protected void Page_Load(object sender, System.EventArgs e)
        {

        }


        protected void Unnamed1_Click(object sender, System.EventArgs e)
        {
            string fileName = @"D:\stefan.steiger\Downloads\keywords.xlsx";

            MongoDB.Driver.MongoClient client = new MongoDB.Driver.MongoClient("mongodb://localhost/27017/mydb");
            MongoDB.Driver.MongoServer server = client.GetServer();
            MongoDB.Driver.MongoDatabase database = server.GetDatabase("testdb");
            MongoGridFs gridFs = new MongoGridFs(database);

            MongoDB.Bson.ObjectId id = MongoGridFsUsage.UploadFile(gridFs, fileName);
            // byte[] baFile = DownloadFile(gridFs, id);
            // DownloadFile(gridFs, id, @"d:\test.rar");
        }


    }


}
