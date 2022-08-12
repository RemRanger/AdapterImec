using AdapterImec.Api.Filters.Models;
using AdapterImec.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AdapterImec.Api.Filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ObjectResult response = null;

            if (context.Exception is NotFoundException nfEx)
            {
                response = new JoinDataObjectResult(JoinDataResponseModel.Create(nfEx));
                context.Result = response;
                context.ExceptionHandled = true;
            }
            else if (context.Exception is ValidationException vEx)
            {
                response = new JoinDataObjectResult(JoinDataResponseModel.Create(vEx));
                context.Result = response;
                context.ExceptionHandled = true;
            }
            else
            {
                response = new JoinDataObjectResult(JoinDataResponseModel.Create(context.Exception));
                context.Result = response;
                context.ExceptionHandled = true;
            }


        }
    }

    public class JoinDataObjectResult : ObjectResult
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"></param>
        public JoinDataObjectResult(JoinDataResponseModel model) : base(model)
        {
            StatusCode = (int)model.StatusCode;
        }
    }
}
