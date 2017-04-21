using System;
using System.ServiceModel;
namespace serviceDemo
{
    /// <summary>
    /// The main WCF Service. 
    /// When adding a new EndPoint with a new ServiceContract Interface, 
    /// add the Interface to the list and implement its OperationContract methods 
    /// </summary>
    [ServiceContract(Namespace = Constants.serviceNamespace)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public partial class DemoService
    {
        /// <summary>
        /// Get the current date and time
        /// </summary>
        /// <returns>The current date and time</returns>
        [OperationContract]
        public DateTime? GetDateTime()
        {
            DateTime? dtNow = DateTime.Now;
            return dtNow;
        }

        /// <summary>
        /// Returns greetings
        /// </summary>
        /// <param>name - The name</param>
        /// <returns>Returns greeting</returns>
        [OperationContract]
        public string Greetings(string name)
        {
            return string.Concat("Hello ", name);
        }
    }
}
