namespace SystemConnectionWebApplication.Model
{
    public class ConnectionModel
    {
        public string AppID { get; set; }

        public string AppType { get; set; }    

        public ConnectionStrInfoModel[] ConnectionStrInfos { get; set; }
    }
}
