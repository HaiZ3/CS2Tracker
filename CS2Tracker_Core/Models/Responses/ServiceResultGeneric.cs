using System;
using System.Collections.Generic;
using System.Text;

namespace FormsApp_CS2_Arbitrage_And_Investment_Tracker.Models.Responses
{
    public class ServiceResultGeneric<T> : ServiceResult
    {
        public T? Data { get; set; }

        public static ServiceResultGeneric<T> Ok(T data)
            => new ServiceResultGeneric<T>
            {
                Success = true,
                Data = data
            };

        public new static ServiceResultGeneric<T> Fail(string message)
            => new ServiceResultGeneric<T>
            {
                Success = false,
                ErrorMessage = message
            };
    }
}
