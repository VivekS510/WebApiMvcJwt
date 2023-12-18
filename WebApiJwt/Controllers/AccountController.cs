using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApiJwt.Controllers
{
    public class AccountController : ApiController
    {
        [HttpGet]

        public HttpResponseMessage ValidLogin(string userName, string Password)

        {
            if (userName == "admin" && Password == "12345678")
            {
                return Request.CreateResponse(HttpStatusCode.OK, TokenManager.GenerateToken(userName));
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, "User name and password is invelid");
            }
        }
        [HttpGet]
        [CustomeAuthenticationFilter]
        public HttpResponseMessage GetEmployee()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Successfully Valid");
        }
    }
}
