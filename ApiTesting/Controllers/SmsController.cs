using Microsoft.AspNetCore.Mvc;
using TextingAPI.Models;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TextingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecieveSMSController : TwilioController
    {


        
        [HttpPost("SendReply")]
        public TwiMLResult SendReply([FromForm] TwilioSMS request)
        {

            //Need to do some sort of check to see if they have already anwsered
            var response = new MessagingResponse();
            

            if(request.Body == "1")
            {
                response.Message("Eviction is confirmed");
            }
            else if(request.Body == "2")
            {
                response.Message("Eviction has been canceled. To reschedule goto....");
            }
            else
            {
                response.Message("That is not a valid response please try again");
            }

            return TwiML(response);

        }
    }
}
