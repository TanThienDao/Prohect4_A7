using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Prohect4_A7
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);
        [OperationContract]
        string Verification(string RULxml, string URLxsd);
        [OperationContract]
        string addComputer(string screen_size, string brand, string model, string year, string Pro_Thread,
            string Pro_Arch_model, string Pro_Arch_generation, string Pro_clock, string Pro_cache,
            List<string>/*string*/ Sto_cache, string Sto_main, string Sto_harddrive);
        [OperationContract]
        string XPathSearch(string URLxml,string path);
        [OperationContract]
        string XpathSe(string URLxml, string path, string name);



    }


    
}
