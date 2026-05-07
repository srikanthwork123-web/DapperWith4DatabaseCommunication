using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Data;
using System.Net;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Newtonsoft.Json;
using DapperWith4DatabaseCommunication.Interfaces;


namespace DapperWith4DatabaseCommunication.Middlewares
{
    //this is one custom middleware class for handling global errors in the application. The GlobalErrorHandlerMiddleware class is designed
    //to catch exceptions thrown during the request processing pipeline and return a standardized error response.
    public class GlobalErrorHandlerMiddleware
    {
        //RequestDelegate is predefined delegate, that can process an HTTP request.
        private readonly RequestDelegate _next;

        private readonly ILoggingFactory _loggingFactory;

        // Middleware constructor takes the next RequestDelegate in the pipeline
        /*
         * Each middleware can:
          Process the request itself.
          Pass it to the next middleware.
          Process the response after the next middleware completes.
         * 
         */
        public GlobalErrorHandlerMiddleware(RequestDelegate next, ILoggingFactory loggingFactory)
        {//inject the RequestDelegate into the constructor to allow the middleware to call the next middleware in the pipeline.
            _next = next;
            _loggingFactory = loggingFactory;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            //HttpContext is predfeined class,it  represents all the information about an individual HTTP request and response.
            //It provides properties and methods to access request data, manipulate the response, and manage the overall request processing.

            try
            {
                //If you are getting any error ,it will call the next middleware in the pipeline,
                //and if any exception occurs during the processing of the request, it will be caught in the catch block.
                await _next(context);//if you are not geeting any error,it will Call the next middleware in the application pipeline
            }
            catch (Exception error)
            {
                await _loggingFactory.AddLoggingMessages("chandu", "Information", "GlobalErrorHandlerMiddleware: Excution Starts");//logg the message in database using custom logging factory
                var response = context.Response;//here we are getting the response object from the http context to set the status code and content type for the error response.
                response.ContentType = "application/json";
                switch (error)
                {
                    case AppException:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException:
                        // not found error 
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                //here we are creating a JSON object that contains the status code, error message, stack trace, and inner InnerException details of the exception. This information can be useful for debugging and logging purposes.
                //to see the exception details,we need to use message,stack trace and inner exception properties of the exception object.             
                var result = JsonConvert.SerializeObject(new
                {
                    StatusCode = response.StatusCode.ToString(),
                    ErrorMessage = error?.Message,
                    StackTraceError = error?.StackTrace?.ToString(),
                    InnerExceptionError = error?.InnerException?.ToString()
                });
                //here log the messages in the text file by using serilog.
                Log.Error("Custom Failure: {@StatusCode}, {@ErrorMessage}, {@StackTraceError},{@InnerExceptionError}",
                response.StatusCode.ToString(), Convert.ToString(error?.Message), Convert.ToString(error?.StackTrace), Convert.ToString(error?.InnerException));
                await _loggingFactory.AddLoggingMessages("chandu", "Error", $"Custom Failure: StatusCode:{response.StatusCode}, ErrorMessage:{Convert.ToString(error?.Message)}, StackTraceError:{Convert.ToString(error?.StackTrace)}, InnerExceptionError:{Convert.ToString(error?.InnerException)}");
                //here log the message in our project text file by using serilog.
                //in sqlserver database also we are logging the exceptions.
                //in Azure application insights Service we are logging the exceptions
                //in Aws we are logging the exceptions in cloud watch
                //in  network log also some of the companies log the error messages.
                //here log the messages in sql server database.

                await _loggingFactory.AddProjectLevelErrorlogAsync(response.StatusCode.ToString(), Convert.ToString(error?.Message), Convert.ToString(error?.StackTrace), Convert.ToString(error?.InnerException));

                //.......Write The logic In Future Based on Your Cloud Usage requirment.
                //If you use Azure cloud,Add the Azure Application Insights Logic Here.To Log The Exceptions in Azure cloud.
                //If You use Aws cloud Add the Aws CloudWatchLogic Here.To Log The exceptions In Aws cloud.
                await _loggingFactory.AddLoggingMessages("chandu", "Information", "GlobalErrorHandlerMiddleware: Excution Ends");//logg the message in database using custom logging factory
                var errorFriendlyMessage = new ProblemDetails
                {//we can't return orginal error to api response,we need to return userfriendly error message like below.
                    Type = "API Exception",
                    Status = (short)HttpStatusCode.InternalServerError,
                    Title = "Internal server error occured in the api"
                };
                //while returning the message to api show user friendly error message
                //here i am converting object into json format using JsonConvert.SerializeObject() method.
                var ErrorResult = JsonConvert.SerializeObject(errorFriendlyMessage);
                await response.WriteAsync(ErrorResult);
            }
        }
    }
}/*
  * in sql server we can see the logs in below tables in hotelmanagement database:
  * Select * from ProjectLevelLog:(this table is used to log the normal messages in database)
  * Select * from ProjectLevelErrorlog:(this table is used to log the error messages in database)
 */

