using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace OTP_Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OTPController : ControllerBase
    {
        private readonly ILogger<OTPController> _logger;
        private string _generatedPassword = "";
        private readonly string _secret = "fghlvdcpouuzixmn";

        public OTPController(ILogger<OTPController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("GenerateOTP")]
        public JsonResult GenerateOTP()
        {
            //generate random string
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            _generatedPassword = new String(stringChars);

            //scris in fisier
            string fileName = "./" + "generatedOTP.txt";
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();

            StreamWriter sw = new StreamWriter(fileName);

            sw.WriteLine(_generatedPassword);
            sw.Close();

            byte[] key = Encoding.UTF8.GetBytes(_secret);

            byte[] plainBytes = Encoding.UTF8.GetBytes(_generatedPassword);


            byte[] cipherBytes = CryptService.Encrypt(plainBytes, key);
            string cipherPassword = Convert.ToBase64String(cipherBytes);
            Console.WriteLine("Cipher Text: " + cipherPassword);



            //byte[] decryptedBytes = CryptService.Decrypt(cipherBytes, key);
            //string decryptedPassword = Encoding.UTF8.GetString(decryptedBytes);
            //Console.WriteLine("Decrypted Text: " + decryptedPassword);

            //Console.ReadLine();
            Console.WriteLine("Generated password: " + _generatedPassword);
            ClearOTP();

            return new JsonResult(cipherPassword);
        }

        [HttpPost]
        [Route("ValidateOTP")]
        public JsonResult ValidateOTP([FromBody] String password)
        {

            byte[] key = Encoding.UTF8.GetBytes(_secret);
            byte[] passwordBytes = Convert.FromBase64String(password);


            byte[] decryptedBytes = CryptService.Decrypt(passwordBytes, key);
            string decryptedPassword = Encoding.UTF8.GetString(decryptedBytes);


            //citit din fisier
            string fileName = "./" + "generatedOTP.txt";
            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                _generatedPassword = reader.ReadLine();
                reader.Close();
            }


            if (decryptedPassword.Equals(_generatedPassword))
            {
                //scris in fisier
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Close();

                StreamWriter sw = new StreamWriter(fileName);

                sw.WriteLine("");
                sw.Close();

                return new JsonResult("Correct");
            }
            else
            {
                return new JsonResult("Incorrect");
            }
        }

        public async Task ClearOTP()
        {
            //bool flag = true;
            await Task.Delay(1000 * 30);
            //scris in fisier
            string fileName = "./" + "generatedOTP.txt";
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Close();

            StreamWriter sw = new StreamWriter(fileName);

            sw.WriteLine("");
            sw.Close();
            Console.WriteLine("OTP cleared");

        }
    }
}
