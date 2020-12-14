using System;
using System.Collections.Generic;
using System.Text;

namespace Mikroportal.MODEL.RequestResponseClasses
{
    public class ResponseBase
    {
        public string ErrorMessage { get; set; }
        public bool isSuccess { get; set; }
    }
}
