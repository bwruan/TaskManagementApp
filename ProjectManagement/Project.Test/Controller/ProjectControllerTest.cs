using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Project.Api.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.Test.Controller
{
    [TestFixture]
    public class ProjectControllerTest
    {
        [Test]
        public void Test_GetProjectByIdSuccess()
        {
            //let me show you how to set the authorization header you will need to complete unit testing
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers["Authorization"] = "Bearer testtoken"; //here you create a header
            var controller = new ProjectController() //pass in moq objects as dependencies.
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = httpContext //set the context that contains the auth header
                }
            };
        }
    }
}
