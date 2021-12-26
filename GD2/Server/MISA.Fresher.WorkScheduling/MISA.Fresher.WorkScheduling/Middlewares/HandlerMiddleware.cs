using Microsoft.AspNetCore.Http;
using MISA.Fresher.WorkScheduling.Core.Entities;
using MISA.Fresher.WorkScheduling.Core.Properties;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MISA.Fresher.WorkScheduling.Middlewares
{
    public class HandlerMiddleware
    {
        #region DECLARE
        private readonly RequestDelegate next;
        #endregion

        #region Constructor
        public HandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        #endregion

        #region Methods
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            // Trả về 500 nếu có lỗi, kết quả không như mong đợi
            var code = HttpStatusCode.InternalServerError;

            var result = JsonConvert.SerializeObject(
                new ApiReturn
                {
                    UserMsg = Resources.MISA_ResponseMessage_Default,
                    ErrorCode = Core.Enums.MISAEnum.UnexpectedError.ToString(),
                    DevMsg = e.Message,
                    TraceId = context?.TraceIdentifier,
                    MoreInfo = e.Source
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
        #endregion
    }
}
