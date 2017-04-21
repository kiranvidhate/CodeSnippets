using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAPIDemo
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTestAPI_Click(object sender, EventArgs e)
        {
             string uri ;
            if (txtProductId.Text != string.Empty)
            {
                uri = string.Concat("http://localhost:47503/api/products/GetProduct?id=", txtProductId.Text);
            }
            else
            {
                uri = "http://localhost:47503/api/products";
            }


            HttpWebRequest webrequest =  (HttpWebRequest)WebRequest.Create(uri);

            HttpWebResponse webresponse;
            webresponse = (HttpWebResponse)webrequest.GetResponse();
            StreamReader loResponseStream = new StreamReader((webresponse.GetResponseStream()));

            string Response = loResponseStream.ReadToEnd();
            loResponseStream.Close();
            webresponse.Close();
            lblResults.Text = Response;
        }
    }
}