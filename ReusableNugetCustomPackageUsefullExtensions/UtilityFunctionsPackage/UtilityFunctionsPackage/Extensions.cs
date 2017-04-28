using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
//http://extensionmethod.net/csharp
namespace UtilityFunctionsPackage.Extensions
{

    /* 
     * Gets the description attribute assigned to an item in an Enum.
     * Example:
     * public enum EnumGrades
            {
                [Description("Passed")]
                Pass,
                [Description("Failed")]
                Failed,
                [Description("Promoted")]
                Promoted
            }
 
        string description = Extensions.EnumHelper<EnumGrades>.GetEnumDescription("pass");
     */
    public static class EnumHelper<T>
    {
        public static string GetEnumDescription(string value)
        {
            Type type = typeof(T);
            var name = Enum.GetNames(type).Where(f => f.Equals(value, StringComparison.CurrentCultureIgnoreCase)).Select(d => d).FirstOrDefault();

            if (name == null)
            {
                return string.Empty;
            }
            var field = type.GetField(name);
            var customAttribute = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttribute.Length > 0 ? ((DescriptionAttribute)customAttribute[0]).Description : name;
        }
    }

    public static class IEnumerableExtensions
    {
        /*
         * Orders a list based on a sortexpression. Useful in object databinding scenarios 
         * where the objectdatasource generates a dynamic sortexpression (example: "Name desc") that specifies the property of the object sort on.
         * Example:
         * class Customer
            {
              public string Name{get;set;}
            }
 
            var list = new List<Customer>();
 
            list.OrderBy("Name desc");
         * */
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> list, string sortExpression)
        {
            sortExpression += "";
            string[] parts = sortExpression.Split(' ');
            bool descending = false;
            string property = "";

            if (parts.Length > 0 && parts[0] != "")
            {
                property = parts[0];

                if (parts.Length > 1)
                {
                    descending = parts[1].ToLower().Contains("esc");
                }

                PropertyInfo prop = typeof(T).GetProperty(property);

                if (prop == null)
                {
                    throw new Exception("No property '" + property + "' in + " + typeof(T).Name + "'");
                }

                if (descending)
                    return list.OrderByDescending(x => prop.GetValue(x, null));
                else
                    return list.OrderBy(x => prop.GetValue(x, null));
            }

            return list;
        }

        /*
         * OrderBy() is nice when you want a consistent & predictable ordering. 
         * This method is NOT THAT! Randomize() - Use this extension method when you want a different or random order every time! 
         * Useful when ordering a list of things for display to give each a fair chance of landing at the top or bottom on each hit. {customers, support techs, or even use as a randomizer for your lottery ;) }
         * // use this on any collection that implements IEnumerable!
            // List, Array, HashSet, Collection, etc
         * Example:
            List<string> myList = new List<string> { "hello", "random", "world", "foo", "bar", "bat", "baz" };
 
            foreach (string s in myList.Randomize())
            {
                Console.WriteLine(s);
            } 
         * */
        public static IEnumerable<t> Randomize<t>(this IEnumerable<t> target)
        {
            Random r = new Random();

            return target.OrderBy(x => (r.Next()));
        }
    }

    /*
     * Encrypt and decrypt a string using the RSACryptoServiceProvider.
     * Example:
     * string secret = "My Secret";
        string encoded = secret.Encrypt("mykey");
        string decoded = encoded.Decrypt("mykey");
     * */
    public static class RSACryptoServiceProviderExtensions
    {
        /// <summary>
        /// Encryptes a string using the supplied key. Encoding is done using RSA encryption.
        /// </summary>
        /// <param name="stringToEncrypt">String that must be encrypted.</param>
        /// <param name="key">Encryptionkey.</param>
        /// <returns>A string representing a byte array separated by a minus sign.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToEncrypt or key is null or empty.</exception>
        public static string Encrypt(this string stringToEncrypt, string key)
        {
            if (string.IsNullOrEmpty(stringToEncrypt))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot encrypt using an empty key. Please supply an encryption key.");
            }

            CspParameters cspp = new CspParameters();
            cspp.KeyContainerName = key;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            byte[] bytes = rsa.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(stringToEncrypt), true);

            return BitConverter.ToString(bytes);
        }

        /// <summary>
        /// Decryptes a string using the supplied key. Decoding is done using RSA encryption.
        /// </summary>
        /// <param name="stringToDecrypt">String that must be decrypted.</param>
        /// <param name="key">Decryptionkey.</param>
        /// <returns>The decrypted string or null if decryption failed.</returns>
        /// <exception cref="ArgumentException">Occurs when stringToDecrypt or key is null or empty.</exception>
        public static string Decrypt(this string stringToDecrypt, string key)
        {
            string result = null;

            if (string.IsNullOrEmpty(stringToDecrypt))
            {
                throw new ArgumentException("An empty string value cannot be encrypted.");
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Cannot decrypt using an empty key. Please supply a decryption key.");
            }

            try
            {
                CspParameters cspp = new CspParameters();
                cspp.KeyContainerName = key;

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;

                string[] decryptArray = stringToDecrypt.Split(new string[] { "-" }, StringSplitOptions.None);
                byte[] decryptByteArray = Array.ConvertAll<string, byte>(decryptArray, (s => Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber))));


                byte[] bytes = rsa.Decrypt(decryptByteArray, true);

                result = System.Text.UTF8Encoding.UTF8.GetString(bytes);

            }
            finally
            {
                // no need for further processing
            }

            return result;
        }


        /*Export DataReader to CSV (List<String>). 
       Basic example that to export data to csv from a datareader. 
        * Handle value if it contains the separator and/or double quotes but can be easily 
        * be expended to include culture (date, etc...) , max errors, and more.
        * 
        * Example:
        * List<string> rows = null;
           using (SqlDataReader dataReader = command.ExecuteReader())
               {
                   rows = dataReader.ToCSV(includeHeadersAsFirstRow, separator);
                   dataReader.Close();
               }
        */
        public static List<string> ToCSV(this IDataReader dataReader, bool includeHeaderAsFirstRow, string separator)
        {
            List<string> csvRows = new List<string>();
            StringBuilder sb = null;

            if (includeHeaderAsFirstRow)
            {
                sb = new StringBuilder();
                for (int index = 0; index < dataReader.FieldCount; index++)
                {
                    if (dataReader.GetName(index) != null)
                        sb.Append(dataReader.GetName(index));

                    if (index < dataReader.FieldCount - 1)
                        sb.Append(separator);
                }
                csvRows.Add(sb.ToString());
            }

            while (dataReader.Read())
            {
                sb = new StringBuilder();
                for (int index = 0; index < dataReader.FieldCount - 1; index++)
                {
                    if (!dataReader.IsDBNull(index))
                    {
                        string value = dataReader.GetValue(index).ToString();
                        if (dataReader.GetFieldType(index) == typeof(String))
                        {
                            //If double quotes are used in value, ensure each are replaced but 2.
                            if (value.IndexOf("\"") >= 0)
                                value = value.Replace("\"", "\"\"");

                            //If separtor are is in value, ensure it is put in double quotes.
                            if (value.IndexOf(separator) >= 0)
                                value = "\"" + value + "\"";
                        }
                        sb.Append(value);
                    }

                    if (index < dataReader.FieldCount - 1)
                        sb.Append(separator);
                }

                if (!dataReader.IsDBNull(dataReader.FieldCount - 1))
                    sb.Append(dataReader.GetValue(dataReader.FieldCount - 1).ToString().Replace(separator, " "));

                csvRows.Add(sb.ToString());
            }
            dataReader.Close();
            sb = null;
            return csvRows;
        }


        /*Export datatable to CSV file
        * Example:
        * DataTable dataTableExportToCSV;
          dataTableExportToCSV.ToCSV (",",false);
        */
        public static void ToCSV(this DataTable table, string delimiter, bool includeHeader)
        {
            StringBuilder result = new StringBuilder();

            if (includeHeader)
            {
                foreach (DataColumn column in table.Columns)
                {
                    result.Append(column.ColumnName);
                    result.Append(delimiter);
                }

                result.Remove(--result.Length, 0);
                result.Append(Environment.NewLine);
            }

            foreach (DataRow row in table.Rows)
            {
                foreach (object item in row.ItemArray)
                {
                    if (item is System.DBNull)
                        result.Append(delimiter);
                    else
                    {
                        string itemAsString = item.ToString();
                        // Double up all embedded double quotes
                        itemAsString = itemAsString.Replace("\"", "\"\"");

                        // To keep things simple, always delimit with double-quotes
                        // so we don't have to determine in which cases they're necessary
                        // and which cases they're not.

                        itemAsString = "\"" + itemAsString + "\"";
                        result.Append(itemAsString + delimiter);
                    }
                }

                result.Remove(--result.Length, 0);
                result.Append(Environment.NewLine);
            }

            using (StreamWriter writer = new StreamWriter(@"C:\log.csv", true))
            {
                writer.Write(result.ToString());
            }

        }

    }

    /*Converts an IEnumerable<IGrouping<TKey,TValue>> to a Dictionary<TKey,List<TValue>> 
     * so that you can easily convert the results of a GroupBy clause to a Dictionary of Groupings. 
     * The out-of-the-box ToDictionary() LINQ extension methods require a key and element extractor which 
     * are largely redundant when being applied to an enumeration of groupings, so this is a short-cut.
    // This class contains helper additions to linq.
     * Example:
     * Dictionary<string,List<Product>> results = productList.GroupBy(product => product.Category).ToDictionary();
    */
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Converts an enumeration of groupings into a Dictionary of those groupings.
        /// </summary>
        /// <typeparam name="TKey">Key type of the grouping and dictionary.</typeparam>
        /// <typeparam name="TValue">Element type of the grouping and dictionary list.</typeparam>
        /// <param name="groupings">The enumeration of groupings from a GroupBy() clause.</param>
        /// <returns>A dictionary of groupings such that the key of the dictionary is TKey type and the value is List of TValue type.</returns>
        public static Dictionary<TKey, List<TValue>> ToDictionary<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> groupings)
        {
            return groupings.ToDictionary(group => group.Key, group => group.ToList());
        }
    }

    /*Serializes an object to XML
     * Example:
     * public class Foo
        {
            public string bar { get; set; }
            public List<string> baz { get; set; }
        }
 
        internal class Program
        {
            private static void Main(string[] args)
            {
                var f = new Foo
                            {
                                bar = "hi",
                                baz = new List<string> {"quick", "brown", "fox"}
                            };
                Console.WriteLine(f.ToXML());
 
                //Output:
                //<?xml version="1.0"?>
                //<Foo xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
                //  <bar>hi</bar>
                //  <baz>
                //  <string>quick</string>
                //  <string>brown</string>
                //  <string>fox</string>
                //  </baz>
                //</Foo>
            }
        }
     */
    public static class XMLSerializeExtension
    {
        public static string ToXML<T>(this T o)
            where T : new()
        {
            string retVal;
            using (var ms = new MemoryStream())
            {
                var xs = new XmlSerializer(typeof(T));
                xs.Serialize(ms, o);
                ms.Flush();
                ms.Position = 0;
                var sr = new StreamReader(ms);
                retVal = sr.ReadToEnd();
            }
            return retVal;
        }
    }
}
