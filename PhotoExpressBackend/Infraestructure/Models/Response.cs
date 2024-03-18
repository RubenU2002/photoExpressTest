using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Models
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; } = null!;

        public static Response<T> Success(T data)
        {
            return new Response<T> { Data = data, IsSuccess = true };
        }

        public static Response<T> Fail(string errorMessage)
        {
            return new Response<T> { ErrorMessage = errorMessage, IsSuccess = false };
        }
    }
}
