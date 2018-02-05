using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Model
{
    public class RESTfulModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("msg")]
        public string Msg { get; set; }
        //[JsonProperty("data")]
        //public object Data { get; set; }
        //[JsonProperty("desc")]
        //public string Desc { get; set; }

        [JsonProperty("unionuserid", NullValueHandling = NullValueHandling.Ignore)]
        public string UnionUserId { get; set; }
        [JsonProperty("token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
        [JsonProperty("authorizedtypeid", NullValueHandling = NullValueHandling.Ignore)]
        public int? AuthorizedTypeId { get; set; }
        [JsonProperty("mobilenum", NullValueHandling = NullValueHandling.Ignore)]
        public string MobileNum { get; set; }

        [JsonProperty("smscode", NullValueHandling = NullValueHandling.Ignore)]
        public string SMSCode { get; set; }
    }
}
