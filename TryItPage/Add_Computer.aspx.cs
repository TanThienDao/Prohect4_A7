using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TryItPage
{
    public partial class Add_Computer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            XMLService.Service1Client proxy = new XMLService.Service1Client();
            
            try
            {
                // check for if all text box is not emmpty.
                if(!String.IsNullOrWhiteSpace(ScreensizeBox.Text)&&
                    !String.IsNullOrWhiteSpace(BrandBox.Text)&&
                    !String.IsNullOrWhiteSpace(ModelBox.Text)&&
                    !String.IsNullOrWhiteSpace(Yearbox.Text)&&
                    !String.IsNullOrWhiteSpace(Pro_threadBox.Text)&&
                    !String.IsNullOrWhiteSpace(Pro_achr_Modelbox.Text)&&
                    !String.IsNullOrWhiteSpace(Pro_Arch_GeneBOx.Text)&&
                    !String.IsNullOrWhiteSpace(Pro_clock.Text)&&
                    !String.IsNullOrWhiteSpace(Pro_Cache.Text)&&
                    !String.IsNullOrWhiteSpace(Storage_cache.Text)&&
                    !String.IsNullOrWhiteSpace(Storage_main.Text)&&
                    !String.IsNullOrWhiteSpace(Storage_harddrive.Text))
                {
                    // store the Cache(more than1) in a list 
                    List<string> Storage_Cache_list = new List<string>();
                    // split them bu comma
                    foreach (string item in Storage_cache.Text.Split(','))
                    {
                        Storage_Cache_list.Add(item);
                    }
                    // change it to array even those it a list string in my service but they like array 
                    string[] storage_cache_array = Storage_Cache_list.ToArray();
                    // call the service and put it in label result.
                    ResultLabel.Text = proxy.addComputer(ScreensizeBox.Text, BrandBox.Text, ModelBox.Text, Yearbox.Text, Pro_threadBox.Text,
                        Pro_achr_Modelbox.Text, Pro_Arch_GeneBOx.Text, Pro_clock.Text, Pro_Cache.Text, storage_cache_array, Storage_main.Text, Storage_harddrive.Text);
                }
                else
                {
                    ResultLabel.Text = "Please finish enter all info in the box!";
                }

            }
            catch(Exception ex)
            {
                ResultLabel.Text ="Error: :"+ ex.Message;
            }
        }
    }
}