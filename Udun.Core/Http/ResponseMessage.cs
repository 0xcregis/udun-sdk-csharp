using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Udun.Core.Http
{
    public class ResponseMessage<T>
    {

        public static int SUCCESS_CODE = 200;
        public static string SUCCESS_MESSAGE = "SUCCESS";
        public static int ERROR_CODE = 500;
        public static string ERROR_MESSAGE = "ERROR";

        public int code { get; set; }
        public string message { get; set; }
        public T data { get; set; }

        public ResponseMessage()
        {
        }

        public ResponseMessage(int status, string message)
        {
            this.code = status;
            this.message = message;
        }

        public ResponseMessage(string message)
        {
            this.message = message;
        }

        public static ResponseMessage<T> success(int status, string message)
        {
            return new ResponseMessage<T>(status, message);
        }

        public static ResponseMessage<T> error(int status, string message)
        {
            return new ResponseMessage<T>(status, message);
        }

        public static ResponseMessage<T> success(string message)
        {
            return new ResponseMessage<T>(SUCCESS_CODE, message);
        }

        public static ResponseMessage<T> error(string message)
        {
            return new ResponseMessage<T>(ERROR_CODE, message);
        }

        public static ResponseMessage<T> error()
        {
            return new ResponseMessage<T>(ERROR_CODE, ERROR_MESSAGE);
        }

        public static ResponseMessage<T> success()
        {
            return new ResponseMessage<T>(SUCCESS_CODE, SUCCESS_MESSAGE);
        }
    }
}
