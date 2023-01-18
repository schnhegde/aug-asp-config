using ASPConfig.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASPConfig.Filters
{
    public class LogFilter : IActionFilter
    {

        private ILogManager manager;

        public LogFilter(ILogManager manager)
        {
            this.manager = manager;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string incomingLogLevel = context.ActionArguments["logLevel"] as string;
            if (!(manager.checkIfCanLog(incomingLogLevel)))
            {
                context.Result = new UnauthorizedObjectResult("log level not supported");
            }
            
        }
    }
}
