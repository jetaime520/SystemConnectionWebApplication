namespace SystemConnectionWebApplication.Model
{
    public class DataResultModel<T>
    {
        /// <summary>
        /// 回傳訊息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 狀態代碼
        /// </summary>
        public string Code { get; set; }

        private T data;

        /// <summary>
        /// 資料內容
        /// </summary>
        public virtual T Data
        {
            get { return data; }    
            set { data = value; }
        }   
    }
}
