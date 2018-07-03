using Microsoft.AspNetCore.Mvc;
using SimpleGPIO.Boards;
using SimpleGPIO.GPIO;

namespace SimpleGPIO.Examples.Web.Controllers
{
    [ApiController]
    public class PiController : ControllerBase
    {
        private readonly IPinInterface _redLED;
        private readonly IPinInterface _yellowLED;
        private readonly IPinInterface _greenLED;

        public PiController(RaspberryPi pi)
        {
            _redLED = pi.Pin16;
            _yellowLED = pi.Pin18;
            _greenLED = pi.Pin22;
        }

        [HttpPost("red")]
        public void Red() => _redLED.Toggle();

        [HttpPost("yellow")]
        public void Yellow() => _yellowLED.Toggle();

        [HttpPost("green")]
        public void Green() => _greenLED.Toggle();
    }
}
