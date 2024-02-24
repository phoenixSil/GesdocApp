using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gesd.Entite.Responses
{
    public class ApiResponse<T> : RequestResponse where T: class
    {
        public T Data { get; set; }

        public ApiResponse(int statusCode, bool success, string message, List<string> errors, T data)
            : base(statusCode, success, message, errors)
        {
            Data = data;
        }

        public ApiResponse()
        {
        }

        public static ApiResponse<T> IS_Success(T data, string message = "Success", int status = 200)
        {
            return new ApiResponse<T>(status, true, message, null, data);
        }

        public static ApiResponse<T> IS_NotFound(string message = "Not found", int status = 404)
        {
            return new ApiResponse<T>(status, false, message, null, null);
        }

        public static ApiResponse<T> IS_BadRequest(string message = "Bad request", int status = 400, List<string> errors = null)
        {
            return new ApiResponse<T>(status, false, message, errors, null);
        }

        public static ApiResponse<T> IS_ServerError(string message = "Internal server error", int status = 500, List<string> errors = null)
        {
            return new ApiResponse<T>(status, false, message, errors, null);
        }
    }
}
