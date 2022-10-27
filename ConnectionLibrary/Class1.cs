using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using SystemConnectionWebApplication.Model;

namespace ConnectionLibrary
{
    public class Class1
    {
        #region parameter
        private string _apiUrl { get { return "https://192.168.1.195/getconnap/api/SystemConnection/"; } }

        private string _apiUrlByCondition { get { return "https://192.168.1.195/getconnap/api/SystemConnectByCondition/"; } }

        //private string _apiUrl { get { return "https://localhost:44322/api/SystemConnection/"; } }
        #endregion

        /// <summary>
        /// 取得系統所有連線資訊
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        private async Task<DataResultModel<string>> CallApiForConnectionStrs(string appId)
        {
            var result = new DataResultModel<string>();
            try
            {
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
                var client = new HttpClient(handler);

                var jsonObj = JsonConvert.SerializeObject(new ConnectionModel() { appid = appId });
                var contentPost = new StringContent(jsonObj, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_apiUrl, contentPost).ConfigureAwait(false);

                result = new DataResultModel<string>()
                {
                    Code = ((int)response.StatusCode).ToString(),
                    IsSuccess = response.IsSuccessStatusCode,
                    ErrorMessage = response.IsSuccessStatusCode ? "" : response.ToString(),
                    Data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                };
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message.ToString();
            }

            return result;
        }

        /// <summary>
        /// 取得系統特定連線資訊
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        private async Task<DataResultModel<string>> CallApiForConnectionStrBy(string appId, string connectionId)
        {
            var result = new DataResultModel<string>();

            try
            {
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => { return true; };
                var client = new HttpClient(handler);

                var input = new ConnectionModel()
                {
                    appid = appId,
                    connectionstrinfos = new ConnectionStrInfoModel[] { new ConnectionStrInfoModel() { connectid = Convert.ToInt32(connectionId) } }
                };
                var stringPayload = JsonConvert.SerializeObject(input);
                var contentPost = new StringContent(stringPayload, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_apiUrlByCondition, contentPost).ConfigureAwait(false);
                
                result = new DataResultModel<string>()
                {
                    Code = ((int)response.StatusCode).ToString(),
                    IsSuccess = response.IsSuccessStatusCode,
                    ErrorMessage = response.IsSuccessStatusCode ? "" : response.ToString(),
                    Data = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                };
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message.ToString();
            }

            return result;
        }

        public DataResultModel<string> GetConnectionStrBy(string appId, string connectionId = "")
        {
            var task = string.IsNullOrEmpty(connectionId)
                ? CallApiForConnectionStrs(appId)
                : CallApiForConnectionStrBy(appId, connectionId);

            var result = task.GetAwaiter().GetResult();

            return result;
        }
    }
}
