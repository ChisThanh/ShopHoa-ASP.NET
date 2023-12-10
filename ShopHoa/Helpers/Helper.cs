using System;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System.Text;

namespace ShopHoa.Helpers
{
    public class Helper
    {
        public static Task<bool> SendEmail(string toAddress, string subject, string body)
        {
            string fromAddress = "chithanh18042003@gmail.com";
            string password = "lkhfwvzglbmbvrgs";

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromAddress, password),
                EnableSsl = true,
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(fromAddress),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(toAddress);

            try
            {
                smtpClient.Send(mailMessage);
                return Task.FromResult(true);
            }
            catch (Exception )
            {
                return Task.FromResult(false);
            }
        }
        public static string FormatNumber(double number)
        {
            string formattedNumber = number.ToString("N0");
            formattedNumber = formattedNumber.Replace(",", ".");
            return formattedNumber;
        }
        public static string GetNextCode(string sw, string currentCode)
        {
            if (!currentCode.StartsWith(sw))
            {
                throw new ArgumentException("Invalid code format");
            }
            string lastNumberString = currentCode.Substring(2);
            if (int.TryParse(lastNumberString, out int lastNumber))
            {
                int nextNumber = lastNumber + 1;
                string nextNumberString = nextNumber.ToString("D4");
                string nextCode = sw + nextNumberString;
                return nextCode;
            }
            else
            {
                throw new ArgumentException("Invalid number in the code");
            }
        }
        public static string Encrypt(string plainText)
        {
            string key = "ThisIsASecretKey123";
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

            for (int i = 0; i < plainBytes.Length; i++)
            {
                plainBytes[i] = (byte)(plainBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            return Convert.ToBase64String(plainBytes);
        }

        public static string Decrypt(string cipherText)
        {
            string key = "ThisIsASecretKey123";
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);

            for (int i = 0; i < cipherBytes.Length; i++)
            {
                cipherBytes[i] = (byte)(cipherBytes[i] ^ keyBytes[i % keyBytes.Length]);
            }

            return Encoding.UTF8.GetString(cipherBytes);
        }
    }

}

