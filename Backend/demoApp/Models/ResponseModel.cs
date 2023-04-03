using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace demoApp.Models
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public ApiResponse()
        {

        }
        public ApiResponse(bool isSuccess, object result = null, string errorMessage = null)
        {
            IsSuccess = isSuccess;
            Data = result;
            Message = errorMessage;
        }
    }
}