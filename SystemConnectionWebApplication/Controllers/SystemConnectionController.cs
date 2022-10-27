using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemConnectionWebApplication.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SystemConnectionWebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemConnectionController : ControllerBase
    {
        // GET: api/<SystemConnectionController>
        [HttpGet]
        public List<ConnectionStrInfoModel> Get()
        {
            StreamReader str = new StreamReader("temp.json");
            string jsonString = str.ReadToEnd();

            var result = JsonConvert.DeserializeObject<List<ConnectionModel>>(jsonString);
            //var result =  new DataResultModel<List<ConnectStrModel>>
            //{
            //    Message = "",
            //    IsSuccess = true,
            //    Code = "",
            //    Data = JsonConvert.DeserializeObject<List<ConnectionModel>>(jsonString)
            //};

            var list = new List<ConnectionStrInfoModel>();
            list.AddRange(result.SelectMany(item=> item.ConnectionStrInfos));           

            return list;
        }

        // GET api/<SystemConnectionController>/5
        [HttpGet("{appid}")]
        public List<ConnectionStrInfoModel> Get(string appid)
        {
            StreamReader str = new StreamReader("temp.json");
            string jsonString = str.ReadToEnd();            
            var result = JsonConvert.DeserializeObject<List<ConnectionModel>>(jsonString);

            var list = new List<ConnectionStrInfoModel>();
            list.AddRange(result.Where(item => item.AppID.Equals(appid)).SelectMany(item => item.ConnectionStrInfos));

            return list;
        }

        [HttpGet("{appid}/{connectionid}")]
        public string Get(string appid, string connectionid)
        {
            StreamReader str = new StreamReader("temp.json");
            string jsonString = str.ReadToEnd();
            var result = JsonConvert.DeserializeObject<List<ConnectionModel>>(jsonString);

            return result.Where(item => item.AppID.Equals(appid)).SelectMany(item => item.ConnectionStrInfos).Where(info => info.ConnectID.Equals(Convert.ToInt32(connectionid))).FirstOrDefault().ConnectStr;
        }

        // POST api/<SystemConnectionController>
        [HttpPost]
        public List<ConnectionStrInfoModel> Post([FromBody] ConnectionModel model)
        {
            StreamReader str = new StreamReader("temp.json");
            string jsonString = str.ReadToEnd();
            var result = JsonConvert.DeserializeObject<List<ConnectionModel>>(jsonString);

            var list = new List<ConnectionStrInfoModel>();
            list.AddRange(result.Where(item => item.AppID.Equals(model.AppID)).SelectMany(item => item.ConnectionStrInfos));

            return list;
        }

        // PUT api/<SystemConnectionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SystemConnectionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class SystemConnectByConditionController : ControllerBase
    {
        // GET: api/<SystemConnectionController>
        [HttpGet]
        public List<ConnectionStrInfoModel> Get()
        {
            StreamReader str = new StreamReader("temp.json");
            string jsonString = str.ReadToEnd();

            var result = JsonConvert.DeserializeObject<List<ConnectionModel>>(jsonString);

            var list = new List<ConnectionStrInfoModel>();
            list.AddRange(result.SelectMany(item => item.ConnectionStrInfos));

            return list;
        }

        [HttpPost]
        public string PostByConnectID([FromBody] ConnectionModel model)
        {
            StreamReader str = new StreamReader("temp.json");
            string jsonString = str.ReadToEnd();
            var result = JsonConvert.DeserializeObject<List<ConnectionModel>>(jsonString);

            return result.Where(item => item.AppID.Equals(model.AppID)).SelectMany(item => item.ConnectionStrInfos).Where(info => info.ConnectID.Equals(Convert.ToInt32(model.ConnectionStrInfos[0].ConnectID))).FirstOrDefault().ConnectStr;
        }

        // PUT api/<SystemConnectionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SystemConnectionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
