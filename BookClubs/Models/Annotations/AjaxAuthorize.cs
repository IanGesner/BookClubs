using System.Web.Mvc;

namespace BookClubs.Models.Annotations
{
    // AjaxAuthorizeAttribute code borrowed from Mr. Ronnie Overby
    // https://stackoverflow.com/users/64334/ronnie-overby
    // https://stackoverflow.com/questions/5258721/authorize-attribute-and-jquery-ajax-in-asp-net-mvc
    public class AjaxAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                var urlHelper = new UrlHelper(context.RequestContext);
                context.HttpContext.Response.StatusCode = 403;
                context.Result = new JsonResult
                {
                    Data = new
                    {
                        Error = "NotAuthorized",
                        LogOnUrl = urlHelper.Action("Login", "Account")
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(context);
            }
        }
    }
}