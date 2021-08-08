using System;
using System.Collections.Generic;
using System.Text;

namespace SystemCApiAssessment
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Root
    {
        public string id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int itemCount { get; set; }
        public bool isActive { get; set; }
        public DateTime createdDateTime { get; set; }
        public DateTime modifiedDateTime { get; set; }
        public string description { get; set; }
    }
}
