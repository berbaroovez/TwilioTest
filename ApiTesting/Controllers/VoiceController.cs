using Microsoft.AspNetCore.Mvc;
using TextingAPI.Models;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace TextingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoiceController : TwilioController
    {



        [HttpPost]
        public TwiMLResult Index()
        {
            DateOnly randomDate = DateOnly.FromDateTime(DateTime.Now);
            var response = new VoiceResponse();
            response.Gather(numDigits: 1, action: new Uri("/api/voice/Gather", UriKind.Relative))
        .Say($"Your eviction for  is set for {randomDate.ToLongDateString()}")
        .Pause(1)
        .Say("Press 1 to confirm.")
        .Pause(1)
         .Say("Press 2 to cancel.");

            // If the user doesn't enter input, loop
            response.Redirect(new Uri("/api/voice", UriKind.Relative));
            
            return TwiML(response);
        }


        [HttpPost("Gather")]
        public TwiMLResult Gather([FromForm] VoiceRequest request)
        {
            var response = new VoiceResponse();

            // If the user entered digits, process their request
            if (!string.IsNullOrEmpty(request.Digits))
            {
                switch (request.Digits)
                {
                    case "1":
                        response.Say("Your eviction has been confirmed.");
                        break;
                    case "2":
                        response.Say("Your Eviction has been canceled. Please visit  to reschedule");
                        break;
                    default:
                        response.Say("Sorry, I don't understand that choice.").Pause();
                        response.Redirect(new Uri("/api/voice", UriKind.Relative));
                        break;
                }
            }
            else
            {
                // If no input was sent, redirect to the /voice route
                response.Redirect(new Uri("/api/voice", UriKind.Relative));
            }

            return TwiML(response);
        }

    }
}
