using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TryItPage
{
    public partial class Verification : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void VerifyButton_Click(object sender, EventArgs e)
        {
            XMLService.Service1Client proxy = new XMLService.Service1Client();
            Uri myUri;
            try
            {
                // check for the empty box
                if(!string.IsNullOrWhiteSpace(XmlBox.Text)&&!string.IsNullOrWhiteSpace(XSDBox.Text))
                {
                    // check if URL is valid for each box.
                    if(Uri.TryCreate(XmlBox.Text,UriKind.RelativeOrAbsolute,out myUri) && Uri.TryCreate(XSDBox.Text,UriKind.RelativeOrAbsolute, out myUri))
                    {
                        ResultLabel.Text = proxy.Verification(XmlBox.Text, XSDBox.Text);
                    }
                    //check for xml valid url
                    else if(!Uri.TryCreate(XmlBox.Text, UriKind.RelativeOrAbsolute, out myUri))
                    {
                        ResultLabel.Text = "The XML URL not valid.";
                    }
                    // check for csd valid url
                    else if (!Uri.TryCreate(XSDBox.Text, UriKind.RelativeOrAbsolute, out myUri))
                    {
                        ResultLabel.Text = "The XSD URL not valid.";
                    }

                }
                else
                {
                    ResultLabel.Text = "Please enter in both box.";
                }
            }
            catch(Exception ex)
            {
                ResultLabel.Text = "Error! " + ex.Message;
            }
        }
    }
}