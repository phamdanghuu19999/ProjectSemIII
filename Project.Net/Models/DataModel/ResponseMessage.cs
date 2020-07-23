using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.Net.Models.DataModel
{
    public class ResponseMessage
    {
        public string Type { get; set; }
        public string Message { get; set; }

        public ResponseMessage()
        {

        }

        public ResponseMessage(string type, string message)
        {
            this.Type = type;
            this.Message = message;
        }
    }
}