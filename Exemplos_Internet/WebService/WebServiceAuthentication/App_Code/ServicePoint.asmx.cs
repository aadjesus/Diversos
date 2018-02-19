using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Services;

namespace WebServiceAuthentication
{
    /// <summary>
    /// ServicePoint for authentication
    /// </summary>
    public class ServicePoint : System.Web.Services.WebService
    {
        // max time to authenticate = (maxmin+1)*60-1 seconds
        const int maxmin = 0;

        public ServicePoint()
        {
        }

        [WebMethod]
        public string Authenticate(string Key)
        {
            string[] HashArray;
            string UserName, level;

            // Key string: HASH|User|OptionalData
            HashArray = Key.Split('|');
            level = "-1";	//defaul level

            if (TestHash(HashArray[0], HashArray[1], 0, "ANY"))
            {
                try
                {
                    UserName = HashArray[1];
                    // JUST FOR TEST: the User authentication level is hard-coded
                    // but may/shuold be retrieved from a DataBase
                    switch (UserName)
                    {
                        case "MyUserName":
                            level = "1";
                            break;
                        case "OtherUser":
                            level = "2";
                            break;
                        default:
                            level = "-1";
                            break;
                    }
                    if (level == "") level = "-1";
                    return "Authentication level: " + level;
                }
                catch (Exception exc)
                {
                    return "Authentication failure: " + exc.ToString();
                }
            }
            return "Authentication failure";
        }

        [WebMethod]
        public string GetToken()
        {
            string ToHash, sResult;
            DateTime dt = DateTime.Now;
            ToHash = dt.ToString("yyyyMMdd") + "|" + dt.ToString("HHmm");
            sResult = Hash(ToHash);
            return sResult;
        }

        [WebMethod]
        public string UseService(string Key, string ServiceName)
        {
            string[] HashArray;
            string UserName, level;

            // Key string: HASH|User|OptionalData
            HashArray = Key.Split('|');
            level = "-1";	//defaul level

            if (TestHash(HashArray[0], HashArray[1], 0, ServiceName))
            {
                try
                {
                    UserName = HashArray[1];
                    // JUST FOR TEST: the User authentication level is hard-coded
                    // but may/shuold be retrieved from a DataBase
                    switch (UserName)
                    {
                        case "MyUserName":
                            level = "1";
                            break;
                        case "OtherUser":
                            level = "2";
                            break;
                        default:
                            level = "-1";
                            break;
                    }
                    if (Convert.ToInt16(level) >= 1) return "YOU ARE AUTHORIZED";
                }
                catch (Exception exc)
                {
                    return "Authentication failure: " + exc.ToString();
                }
            }
            return "Authentication failure";
        }

        private bool TestHash(string HashStr, string UserName, int minutes, string ServiceName)
        {
            string Pwd, ToHash;
            string sResult, sResultT, sResultToken;
            try
            {
              

                if (ServiceName == "ANY")
                    // JUST FOR TEST: the password is hard-coded:
                    Pwd = "SeCrEt";
                else
                    // JUST FOR TEST: the password is hard-coded:
                    Pwd = "SeCrEt" + ServiceName;

                DateTime dt = DateTime.Now;
                System.TimeSpan minute = new System.TimeSpan(0, 0, minutes, 0, 0);
                dt = dt - minute;
                //before hashing we have:
                //USERNAME|PassWord|YYYYMMDD|HHMM
                ToHash = UserName.ToUpper() + "|" + Pwd + "|" + dt.ToString("yyyyMMdd") + "|" + dt.ToString("HHmm");
                sResult = Hash(ToHash);
                //TokenWeGotBefore
                ToHash = dt.ToString("yyyyMMdd") + "|" + dt.ToString("HHmm");
                sResultToken = Hash(ToHash);
                //USERNAME|PassWord|TokenWeGotBefore
                ToHash = UserName.ToUpper() + "|" + Pwd + "|" + sResultToken;
                sResultT = Hash(ToHash);

                if ((sResult == HashStr) || (sResultT == HashStr))
                    return true;
                else
                    if (minutes <= maxmin) // allowed maxmin+1 minutes minus 1 second to call web service
                        return TestHash(HashStr, UserName, minutes+1, ServiceName);
                    else
                        return false;
            }
            catch
            {
                return false;
            }
        }

        private string Hash(string ToHash)
        {
            // First we need to convert the string into bytes, which means using a text encoder.
            Encoder enc = System.Text.Encoding.ASCII.GetEncoder();

            // Create a buffer large enough to hold the string
            byte[] data = new byte[ToHash.Length];
            enc.GetBytes(ToHash.ToCharArray(), 0, ToHash.Length, data, 0, true);

            // This is one implementation of the abstract class MD5.
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);

            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }
    }
}
