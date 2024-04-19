using Microsoft.Extensions.Logging;
using Moq;
using OTP_Backend.Controllers;

namespace OTP_Backend.Tests
{
    public class OTPControllerTests
    {
        [Fact]
        public void GenerateOTPTest()
        {
            var encryptedOTPLength = 24;

            var mock = new Mock<ILogger<OTPController>>();
            ILogger<OTPController> _logger = mock.Object;

            var otpController = new OTPController(_logger);

            var result = otpController.GenerateOTP();

            Assert.True(encryptedOTPLength == result.Value.ToString().Length);

        }

        [Fact]
        public void ValidateOTPTest()
        {
            //var encryptedOTPLength = 24;

            var mock = new Mock<ILogger<OTPController>>();
            ILogger<OTPController> _logger = mock.Object;

            var otpController = new OTPController(_logger);
            //scris in fisier
            string fileName = "./" + "generatedOTP.txt";
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();

            StreamWriter sw = new StreamWriter(fileName);

            sw.WriteLine("xzCytdYC");
            sw.Close();

            var result = otpController.ValidateOTP("iRyf3Xuo/XcftYOqFG7P/Q==");

            //golit fisier
            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();

            sw = new StreamWriter(fileName);

            sw.WriteLine("");
            sw.Close();

            Assert.True(result.Value.ToString().Equals("Correct"));

        }
    }

    //xzCytdYC
    //iRyf3Xuo/XcftYOqFG7P/Q==

}