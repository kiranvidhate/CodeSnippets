using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Globalization;
using System.Text;
using System.Security.Cryptography;
using System.Security.Principal;

namespace UtilityFunctionsPackage
{
    /// <summary>
    /// Class for userful funcitons
    /// </summary>
    public class Functions
    {
        #region --Public Methods--
        /// <summary>
        /// Check if file exists or not
        /// </summary>
        public static int chkFileExistcallcount = 0;
        /// <summary>
        /// Parses the integer.
        /// </summary>
        /// <param name="intValue">The int value.</param>
        /// <returns></returns>
        public static int ParseInteger(string intValue)
        {
            int intVar = 0;
            int result = 0;

            if (!string.IsNullOrEmpty(intValue))
            {
                if (int.TryParse(intValue, out intVar))
                {
                    result = Convert.ToInt32(intValue);
                }
            }

            return result;
        }

        /// <summary>
        /// Parses the decimal.
        /// </summary>
        /// <param name="decValue">The dec value.</param>
        /// <returns></returns>
        public static decimal ParseDecimal(string decValue)
        {
            decimal decVar = 0;
            decimal result = 0;

            if (!string.IsNullOrEmpty(decValue))
            {
                if (decimal.TryParse(decValue, out decVar))
                {
                    result = Convert.ToDecimal(decValue);
                }
            }

            return result;
        }

        /// <summary>
        /// Parses the double.
        /// </summary>
        /// <param name="dobValue">The dob value.</param>
        /// <returns></returns>
        public static double ParseDouble(string dobValue)
        {
            double dobVar = 0;
            double result = 0;

            if (!string.IsNullOrEmpty(dobValue))
            {
                if (double.TryParse(dobValue, out dobVar))
                {
                    result = Convert.ToDouble(dobVar);
                }
            }

            return result;
        }

        /// <summary>
        /// Parses the string.
        /// </summary>
        /// <param name="strValue">The STR value.</param>
        /// <returns></returns>
        public static string ParseString(object strValue)
        {
            string result = string.Empty;

            if (strValue != null)
            {
                result = strValue.ToString();
            }

            return result;
        }

        /// <summary>
        /// Parses the long.
        /// </summary>
        /// <param name="lngValue">The LNG value.</param>
        /// <returns></returns>
        public static long ParseLong(string lngValue)
        {
            long lngVar = 0;
            long result = 0;

            if (!string.IsNullOrEmpty(lngValue))
            {
                if (long.TryParse(lngValue, out lngVar))
                {
                    result = Convert.ToInt64(lngVar);
                }
            }

            return result;
        }

        /// <summary>
        /// Parses the boolean.
        /// </summary>
        /// <param name="boolValue">The bool value.</param>
        /// <returns></returns>
        public static bool ParseBoolean(string boolValue)
        {
            bool decVar = false;
            bool result = false;

            if (!string.IsNullOrEmpty(boolValue))
            {
                if (bool.TryParse(boolValue, out decVar))
                {
                    result = Convert.ToBoolean(boolValue);
                }
            }

            if (!result)
            {
                result = boolValue.ToLower() == "yes";
            }
            else if (!result)
            {
                result = boolValue.ToLower() == "true";
            }

            return result;
        }

        /// <summary>
        /// Parses the date time.
        /// </summary>
        /// <param name="dateTimeValue">The date time value.</param>
        /// <returns></returns>
        public static DateTime ParseDateTime(string dateTimeValue)
        {
            DateTime dtDate = new DateTime();
            DateTime result = new DateTime(9999, 12, 31);
            //  IFormatProvider format = new CultureInfo("en-GB", true);

            if (!string.IsNullOrEmpty(dateTimeValue))
            {
                if (DateTime.TryParse(dateTimeValue, out dtDate))
                {
                    //result = Convert.ToDateTime(dateTimeValue, format);
                    result = Convert.ToDateTime(dateTimeValue);
                }
            }

            return result;
        }

        /// <summary>
        /// Determines whether the specified DataTableReader has column.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <returns>
        /// 	<c>true</c> if the specified dr has column; otherwise, <c>false</c>.
        /// </returns>
        public static bool HasColumn(DataTableReader dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Validates the length of the string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static bool ValidateStringLength(string value, int length)
        {
            return (value.Length <= length);
        }

        /// <summary>
        /// Formats the date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        public static string FormatDate(string date)
        {
            string dateFormat = "DDMMYYYY";
            return String.Format("{0:" + dateFormat + "}", ParseDateTime(date));
        }

        /// <summary>
        /// Determines whether [is null or empty] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is null or empty] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(object value)
        {
            if (value != null)
            {
                return (string.IsNullOrEmpty(value.ToString()));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Determines whether [is valid email] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid email] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        /// 
        public static bool IsValidEmail(string value)
        {
            StringBuilder pattern = new StringBuilder();
            pattern.Append(@"^(([^<>()[\]\\.,;:\s@\""]+");
            pattern.Append(@"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@");
            pattern.Append(@"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}");
            pattern.Append(@"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+");
            pattern.Append(@"[a-zA-Z]{2,}))$");

            return (new Regex(pattern.ToString()).IsMatch(value));
        }

        /// <summary>
        /// Determines whether [is alpha numeric] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is alpha numeric] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAlphaNumeric(string value)
        {
            StringBuilder pattern = new StringBuilder();
            pattern.Append("^[0-9a-zA-Z]+$");
            return (new Regex(pattern.ToString()).IsMatch(value));
        }

        /// <summary>
        /// Determines whether [is valid license key] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        ///   <c>true</c> if [is valid license key] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidLicenseKey(string value)
        {
            StringBuilder pattern = new StringBuilder();
            pattern.Append(@"[^A-Z0-9]");
            return (!new Regex(pattern.ToString()).IsMatch(value));
        }

        /// <summary>
        /// Determines whether [is valid URL] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is valid URL] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValidUrl(string value)
        {
            StringBuilder pattern = new StringBuilder();
            pattern.Append(@"(?:(?<protocol>http(?:s?)|ftp)(?:\:\/\/)) ");
            pattern.Append(@"(?:(?<usrpwd>\w+\:\w+)(?:\@))? (?<domain>[^/\r\n\:]+)?");
            pattern.Append(@"(?<port>\:\d+)? (?<path>(?:\/.*)*\/)? ");
            pattern.Append(@" (?<filename>.*?\.(?<ext>\w{2,4}))? ");
            pattern.Append(@"(?<qrystr>\??(?:\w+\=[^\#]+)(?:\&?\w+\=\w+)*)");
            pattern.Append(@"* (?<bkmrk>\#.*)?");

            return (new Regex(pattern.ToString()).IsMatch(value));
        }

        /// <summary>
        /// Regulars the expression.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static string RegularExpression(string key)
        {
            StringBuilder pattern = new StringBuilder();
            switch (key)
            {
                case "email":
                    {
                        pattern.Append(@"^(([^<>()[\]\\.,;:\s@\""]+");
                        pattern.Append(@"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@");
                        pattern.Append(@"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}");
                        pattern.Append(@"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+");
                        pattern.Append(@"[a-zA-Z]{2,}))$");
                    }
                    break;

                case "telephone":
                    {
                        pattern.Append(@"(\(?\+44\)?\s?(1|2|3|7|8)\d{3}|\(?(01|02|03|07|08)\d{3}\)?)");
                        pattern.Append(@"\s?\d{3}\s?\d{3}|(\(?\+44\)?\s?(1|2|3|5|7|8)\d{2}|\(?(01|02|03|05|07|08)\d{2}\)?)");
                        pattern.Append(@"\s?\d{3}\s?\d{4}|(\(?\+44\)?\s?(5|9)\d{2}|\(?(05|09)\d{2}\)?)\s?\d{3}\s?\d{3}");
                    }
                    break;

                case "url":
                    {
                        pattern.Append(@"/^(?:http:\/\/)?(?:[\w-]+\.)+[a-z]{2,6}$/i ");
                    }
                    break;
            }

            return pattern.ToString();
        }

        /// <summary>
        /// Determines whether [is it number] [the specified inputvalue].
        /// </summary>
        /// <param name="inputvalue">The inputvalue.</param>
        /// <returns>
        ///   <c>true</c> if [is it number] [the specified inputvalue]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsItNumber(string inputvalue)
        {
            Regex isnumber = new Regex("[^0-9\\-]");
            return !isnumber.IsMatch(inputvalue);
        }

        /// <summary>
        /// Creates the random password.
        /// </summary>
        /// <param name="passwordLength">Length of the password.</param>
        /// <returns></returns>
        public static string CreateRandomPassword(int passwordLength)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789!@$?_-";
            char[] chars = new char[passwordLength];
            Random rd = new Random();

            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }

        /// <summary>
        /// Gets the common data path.
        /// </summary>
        /// <returns></returns>
        public static string GetCommonDataPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        }

        // Added by Chandrashekhar Mahale
        /// <summary>
        /// Encodes the password.
        /// </summary>
        /// <param name="originalPassword">The original password.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string EncodePassword(string originalPassword)
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            StringBuilder encodedString = new StringBuilder();

            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(originalPassword);
            encodedBytes = md5.ComputeHash(originalBytes);

            foreach (byte readByte in encodedBytes)
            {
                encodedString.Append(readByte.ToString("x2").ToLower());
            }

            //Convert encoded bytes back to a 'readable' string
            return (encodedString.ToString());
        }



        /// <summary>
        /// Gets replace the qoute inorder to avoid sql inject attack.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string GetReplaceQuote(string sqlQuery)
        {
            return sqlQuery.Replace("'", "''");
        }

        /// <summary>
        /// Converts seconds to HH:MM:SS format.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        public static string ConvertSecondsToHHMMSS(double seconds)
        {
            TimeSpan tsHHMMSS = TimeSpan.FromSeconds(seconds);

            string answer = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                   tsHHMMSS.Hours,
                                   tsHHMMSS.Minutes,
                                   tsHHMMSS.Seconds);
            return answer;
        }

        /// <summary>
        /// Converts the seconds to MMSS.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        public static string ConvertSecondsToMMSS(double seconds)
        {
            TimeSpan tsHHMMSS = TimeSpan.FromSeconds(seconds);

            string answer = string.Format("{0:D2}:{1:D2}",
                                   tsHHMMSS.Minutes,
                                   tsHHMMSS.Seconds);
            return answer;
        }

        /// <summary>
        /// Converts the seconds to MM.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        public static string ConvertMintuesToMMSS(double mintues)
        {
            TimeSpan tsHHMMSS = TimeSpan.FromMinutes(mintues);

            // Converted HH to mintues and added it to min value
            string answer = string.Format("{0:D2}:{1:D2}",
                                  ((Functions.ParseInteger(tsHHMMSS.Hours.ToString()) * 60) + Functions.ParseInteger(tsHHMMSS.Minutes.ToString())),
                                  tsHHMMSS.Seconds);

            return answer;
        }

        /// <summary>
        /// Converts HH:MM:SS to seconds format.
        /// </summary>
        /// <param name="seconds">The time.</param>
        /// <returns></returns>
        public static double ConvertHHMMSSToSeconds(string time)
        {
            double seconds = TimeSpan.Parse(time).TotalSeconds;
            return seconds;
        }
        /// <summary>
        /// Converts the seconds to MM.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        /// <returns></returns>
        public static int ConvertSecondsToMM(double seconds)
        {
            TimeSpan tsMM = TimeSpan.FromSeconds(seconds);

            int answer = tsMM.Minutes;
            return answer;
        }

        /// <summary>
        /// Determines whether [is file exists] [the specified file name].
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        ///   <c>true</c> if [is file exists] [the specified file name]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        /// <summary>
        /// Encodes string to MD5.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string EncodeToMD5(string value)
        {
            MD5CryptoServiceProvider objMD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] objBytes = System.Text.Encoding.UTF8.GetBytes(value);
            objBytes = objMD5CryptoServiceProvider.ComputeHash(objBytes);
            StringBuilder s = new StringBuilder();
            foreach (byte eachByte in objBytes)
            {
                s.Append(eachByte.ToString("x2").ToLower());
            }

            return s.ToString();
        }


        /// <summary>
        /// Base64 encoding
        /// </summary>
        /// <param name="toEncode"></param>
        /// <returns></returns>
        static public string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = System.Text.Encoding.UTF8.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        /// <summary>
        /// BAse64 decoding
        /// </summary>
        /// <param name="encodedData"></param>
        /// <returns></returns>
        static public string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }

        /// <summary>
        /// Determines whether this instance is administrator.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is administrator; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Set string to the Title case.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToTitleCase(string value)
        {
            try
            {
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                return textInfo.ToTitleCase(value);
            }
            catch (Exception ex)
            {
                //Catch Exception
            }

            return value;
        }

        /// <summary>
        /// Gets the width of the screen.
        /// </summary>
        /// <returns></returns>
        public static int GetScreenWidth()
        {
            return 0;// System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
        }

        /// <summary>
        /// Gets the height of the screen.
        /// </summary>
        /// <returns></returns>
        public static int GetScreenHeight()
        {
            return 0;// System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
        }

        /// <summary>
        /// Determines whether file is available and not locked for use.
        /// </summary>
        /// <param name="file">The file name.</param>
        /// <returns>
        ///   <c>true</c> if [is file locked] [the specified file]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFileAvailable(string fileName)
        {
            FileStream stream = null;
            FileInfo file = new FileInfo(fileName);

            if (file.Exists)
            {
                try
                {
                    stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                }
                catch (IOException e)
                {
                    //the file is unavailable because it is:
                    //still being written                     //or being processed by another thread
                    //or does not exist (has already been processed)

                    if (chkFileExistcallcount < 2)
                    {
                        chkFileExistcallcount++;
                        GC.Collect();
                        //Catch Exception
                        IsFileAvailable(fileName);

                    }
                    else
                    {
                        //Catch Exception
                    }


                    return false;
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                    file = null;
                }
            }
            else
            {
                return false;
            }

            //file is available and not locked
            return true;
        }
        #endregion
    }
}
