using LocalCommunitySite.API.Extentions.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace LocalCommunitySite.API.Extentions.Filters
{
    public class CustomModelStateFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = context.ModelState.Values.SelectMany(x => x.Errors);

                List<ErrorDetails> errors = new List<ErrorDetails>();

                foreach (ModelError error in allErrors)
                {
                    errors.Add(new ErrorDetails()
                    {
                        Message = error.ErrorMessage,
                        StatusCode = (int)HttpStatusCode.UnprocessableEntity
                    });
                }

                context.Result = new ObjectResult(errors)
                {
                    StatusCode = (int)HttpStatusCode.UnprocessableEntity,
                };
            }
        }
    }
}
