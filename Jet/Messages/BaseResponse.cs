using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Jet.Messages
{
    public class BaseResponse
    {
        private readonly string _message;

        public BaseResponse()
        {

        }

        public BaseResponse(Exception ex, string message = "")
        {
            Exception = ex;
            _message = message;
        }

        public BaseResponse(string message)
        {
            _message = message;
        }

        public Exception Exception { get; set; }

        private string GetMessage()
        {
            var exMsg = GetMessageFromException(Exception) ?? "";

            if (string.IsNullOrWhiteSpace(_message))
            {
                return exMsg;
            }

            return !string.IsNullOrWhiteSpace(exMsg) 
                ? $"{exMsg} Message: {_message}" 
                : $"Message: {_message}";
        }

        public string Message => GetMessage();

        private static string GetMessageFromException(Exception ex)
        {
            if (string.IsNullOrWhiteSpace(ex?.Message)){return null;}

            var str = $"Exception: {ex.Message}";
            var innerStr = GetMessageFromException(ex.InnerException);
            
            return !string.IsNullOrWhiteSpace(innerStr) 
                ? $"{str} Inner {innerStr}" 
                : str;
        }
        
        public bool Success => Exception == null;
    }
}
