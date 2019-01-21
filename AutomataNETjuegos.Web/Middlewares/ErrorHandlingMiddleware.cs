using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutomataNETjuegos.Compilador.Excepciones;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace AutomataNETjuegos.Web.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                if (context.Request.ContentType != "application/json")
                {
                    throw ex;
                }

                await HandleExceptionAsync(context, ex);
            }
        }

        private async static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            IList<string> errors = new List<string>() { ex.Message };
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            string result;

            if (ex is ExcepcionCompilacion)
            {
                errors = ((ExcepcionCompilacion)ex).ErroresCompilacion;
                code = HttpStatusCode.BadRequest;
            }

            result = JsonConvert.SerializeObject(new { hResult = ex.HResult, errors });

            context.Response.StatusCode = (int)code;
            await context.Response.WriteAsync(result);
        }
    }
}
