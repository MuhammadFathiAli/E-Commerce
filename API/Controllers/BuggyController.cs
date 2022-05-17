using API.Errors;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    //just to test the error functionality controller 
    [ApiExplorerSettings(IgnoreApi = true)] //ignore frm swagger docs 

    public class BuggyController : BaseApiController
    {
        private readonly StoreContext context;

        public BuggyController(StoreContext _context)
        {
            context = _context;
        }
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = context.Products.Find(42);
            if (thing == null)
            {
               return NotFound(new ApiResponse(404));
            }
            return Ok();
        }
        [HttpGet("servererror")]

        public ActionResult GetServerError()
        {
            var thing = context.Products.Find(42);
            var returnthing = thing.ToString(); // null refernce error
            return Ok();
        }
        [HttpGet("badrequest")]

        public ActionResult GetBadRequest()
        {
           return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText ()
        {
            return "Secret stuff";
        }
    }
}
