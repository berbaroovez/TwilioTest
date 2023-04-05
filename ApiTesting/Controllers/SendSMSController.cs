
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML.Voice;
using Twilio.TwiML;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;


namespace ApiTesting.Controllers;


[ApiController]
[Route("api/[controller]")]
public class SendSMSController : ControllerBase
{
    string accountSid = "";
    string authToken = "";

    [HttpPost("SendText")]
    public ActionResult SendReminder(string phoneNumber)
    {

        TwilioClient.Init(accountSid, authToken);
        DateOnly randomDate = DateOnly.FromDateTime(DateTime.Now);

        var message = MessageResource.Create(
            body: $"Your eviction for is set for {randomDate.ToLongDateString()}. Reply 1 to confirm or 2 to cancel",
            from: new Twilio.Types.PhoneNumber("+777"),
            to: new Twilio.Types.PhoneNumber("+1" + phoneNumber)
        );

        Console.WriteLine(message.Sid);

        return StatusCode(200, new { message = message.Sid });

    }


    [HttpPost("SendCallReminder")]
    public void SendCallReminder(string phoneNumber)
    {

        TwilioClient.Init(accountSid, authToken);


        DateOnly randomDate = DateOnly.FromDateTime(DateTime.Now);
        var call = CallResource.Create(
         twiml: new Twilio.Types.Twiml($"<Response>\r\n    <Gather numDigits=\"1\" action=\"https://0a93-149-75-49-120.ngrok.io/api/voice/Gather\">\r\n        <Say>Your eviction for  is set for {randomDate.ToLongDateString()}</Say>\r\n        <Pause length=\"1\" />\r\n        <Say>Press 1 to confirm.</Say>\r\n        <Pause length=\"1\" />\r\n        <Say>Press 2 to cancel.</Say>\r\n    </Gather>\r\n</Response>"),
           to: new Twilio.Types.PhoneNumber("+1" + phoneNumber),
            from: new Twilio.Types.PhoneNumber("+777")
        //url: new Uri("/api/voice/Gather", UriKind.Relative)
        );

    }




    }
