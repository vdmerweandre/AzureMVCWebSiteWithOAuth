using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

//https://www.windowsazure.com/en-us/documentation/articles/web-sites-dotnet-rest-service-aspnet-api-sql-database/#bkmk_createmvc4app

//You should require anti-forgery tokens for any nonsafe methods (POST, PUT, DELETE). 
//Also, make sure that safe methods (GET, HEAD) do not have any side effects. Moreover, 
//if you enable cross-domain support, such as CORS or JSONP, then even safe methods like GET 
//are potentially vulnerable to CSRF attacks, allowing the attacker to read potentially sensitive data.

namespace AzureMVCWebSiteWithOAuth.Filters
{
    public class ValidateHttpAntiForgeryTokenAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);

            HttpRequestMessage request = actionContext.ControllerContext.Request;

            try
            {
                if (IsAjaxRequest(request))
                {
                    ValidateRequestHeader(request);
                }
                else
                {
                    AntiForgery.Validate();
                }
            }
            catch (HttpAntiForgeryException e)
            {
                actionContext.Response = request.CreateErrorResponse(HttpStatusCode.Forbidden, e);
            }
        }

        private bool IsAjaxRequest(HttpRequestMessage request)
        {
            IEnumerable<String> xRequestWithHeaders;

            if (request.Headers.TryGetValues("X-Request-With", out xRequestWithHeaders))
            {
                string headerValue = xRequestWithHeaders.FirstOrDefault();
                if (!String.IsNullOrEmpty(headerValue))
                {
                    return String.Equals(headerValue, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
                }
            }

            return false;
        }

        private void ValidateRequestHeader(HttpRequestMessage request)
        {
            string cookieToken = String.Empty;
            string formToken = String.Empty;
            IEnumerable<String> tokenHeaders;

            if (request.Headers.TryGetValues("RequestVerificationToken", out tokenHeaders))
            {
                string tokenValue = tokenHeaders.FirstOrDefault();

                if (!String.IsNullOrEmpty(tokenValue))
                {
                    string[] tokens = tokenValue.Split(':');
                    if (tokens.Length == 2)
                    {
                        cookieToken = tokens[0].Trim();
                        formToken = tokens[1].Trim();
                    }
                }
            }
            AntiForgery.Validate(cookieToken, formToken);
        }
    }
}