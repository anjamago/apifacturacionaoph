using System;
using System.Net;

namespace BackEnd.OpheliaTest.Entities.Responses
{
    public class ResponseBase<T>
    {
     
        public ResponseBase(HttpStatusCode code = HttpStatusCode.OK, string message = null, T data = default, int count = 0)
        {
            ResponseTime = DateTime.UtcNow.AddHours(-5);
            Code = (int)code;
            Message = message;
            Data = data;
            Count = count;
        }

        [Newtonsoft.Json.JsonProperty("message")]
        public string Message { get; set; }

       
        [Newtonsoft.Json.JsonProperty("count")]
        public int Count { get; set; }

        [Newtonsoft.Json.JsonProperty("responseTime")]
        public DateTime ResponseTime { get; set; }

        [Newtonsoft.Json.JsonProperty("data")]
        public T Data { get; set; }

        [Newtonsoft.Json.JsonProperty("code")]
        public int Code { get; set; }
    }
}

