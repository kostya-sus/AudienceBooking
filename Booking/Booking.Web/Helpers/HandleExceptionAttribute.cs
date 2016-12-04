using System;
using System.Web;
using System.Web.Mvc;

namespace Booking.Web.Helpers
{
    public class HandleExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            var rethrowedException = ConvertInnerException(ex);

            filterContext.Result = new RedirectResult(GetRedirectUrl(rethrowedException));
        }

        private Exception ConvertInnerException(Exception e)
        {
            int code = 0;
            var exceptionType = e.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                code = 403;
            }
            else if (exceptionType == typeof(InvalidOperationException))
            {
                code = 400;
            }

            return code == 0 ? e : new HttpException(code, e.Message);
        }

        public static string GetRedirectUrl(Exception exception)
        {
            var httpException = exception as HttpException;
            string message;
            int code;
            if (httpException != null)
            {
                code = httpException.GetHttpCode();
                message = code == 404 ? Localization.Localization.NotFound404 : httpException.Message;
            }
            else
            {
                message = Localization.Localization.SomethingWentWrong;
                code = 500;
            }

            return string.Format("~/Error/Error/?message={0}&statusCode={1}", message, code);
        }
    }
}