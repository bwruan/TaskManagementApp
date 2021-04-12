using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectTask.Api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    [Authorize]
    public class TaskCommentController : ControllerBase
    {
    }
}