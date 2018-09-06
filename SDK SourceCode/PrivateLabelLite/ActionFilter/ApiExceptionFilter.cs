using PrivateLabelLite.Entities.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PrivateLabelLite.Services.Logs;
using System.Web.Routing;
using System.Net;

namespace PrivateLabelLite.ActionFilter
{
    public class ApiExceptionFilter : HandleErrorAttribute, IExceptionFilter
    {
        private readonly ILogService _logService = new LogService();
        private int diffStreamOneErrorCodeThanHttpResponseCode = 10; //10 is taken as random number, We can use any code othen than 10
        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is ApiErrorException)
            {

                var apiError = filterContext.Exception as ApiErrorException;
                ApiRequest apiRequest = new ApiRequest()
                {
                    CurrentExecutionFilePath = filterContext.RequestContext.HttpContext.Request.CurrentExecutionFilePath,
                    RequestType = filterContext.RequestContext.HttpContext.Request.RequestType,
                    UserHostAddress = filterContext.RequestContext.HttpContext.Request.UserHostAddress,
                    UserHostName = filterContext.RequestContext.HttpContext.Request.UserHostName,
                    TimeStamp = filterContext.RequestContext.HttpContext.Timestamp,
                    Browser = filterContext.RequestContext.HttpContext.Request.Browser.Browser
                };

                filterContext.ExceptionHandled = true;
                int response;
                if(int.TryParse(apiError.ErrorCode, out response))
                {
                    if (Enum.GetName(typeof(HttpStatusCode), response) != null)
                    {
                        filterContext.HttpContext.Response.StatusCode = Convert.ToInt32(apiError.ErrorCode);
                    }
                    else
                    {
                        filterContext.HttpContext.Response.StatusCode = diffStreamOneErrorCodeThanHttpResponseCode;
                    }
                }
               
                else
                {
                    filterContext.HttpContext.Response.StatusCode = diffStreamOneErrorCodeThanHttpResponseCode;
                }


                filterContext.HttpContext.Response.StatusDescription = apiError.ErrorMessage.ToString();
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;


                //Log-Exception in database
                _logService.LogException(apiError, apiRequest);


            }

        }
    }
}