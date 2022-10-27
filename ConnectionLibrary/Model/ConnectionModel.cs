namespace SystemConnectionWebApplication.Model
{
    public class ConnectionModel
    {
        public string appid { get; set; }

        public string apptype { get; set; }    

        public ConnectionStrInfoModel[] connectionstrinfos { get; set; }
    }
}
