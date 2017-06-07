using System.Text;

namespace MobileApi.Helpers
{
    public class AuthenticationTokenWetlandsAttribute : System.Web.Http.Filters.AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                string token = actionContext.Request.Headers.Authorization.Parameter;
                if (token != "p4OqMiplghVdWPbVv5rx84jdlskdJk*jdlsKDIE84")
                    actionContext.Response = new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
            }
        }
    }
}