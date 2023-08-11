using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ClinicApp.Core.DTO;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClinicApp.Filters.ActionFilter
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                // Model state is not valid, return a response indicating the validation errors
                var _resutl = new CommonResponse
                {
                    RequestStatus = Core.Constant.RequestStatus.BadRequest,
                    Message = "BadRequest",
                    ModelError = context.ModelState.Values.SelectMany(v => v.Errors).Select(c=>c.ErrorMessage).ToList()
                };
                context.Result = new BadRequestObjectResult(_resutl);
            }
            else
            {
                base.OnActionExecuting(context);
            }
        }
    }
}
