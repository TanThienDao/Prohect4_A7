using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.XPath;

namespace Prohect4_A7
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }
        public string Verification(string URLxml, string URLxsd)
        {
            string result = "no error!";
            string xml;
            var setSchema = new XmlSchemaSet();
            Uri myuri;
            // check if the XML was a valid webside
            using(var webC = new WebClient())
            {
                try
                {
                    xml = webC.DownloadString(URLxml);
                }
                catch (WebException e)
                {
                    result = "XML Not valid! "+ e.Message;
                    return result;
                }
            }
            // this to check if we can loand xml to xmldocument.
            var xd = new XmlDocument();
            try
            {
                xd.Load(URLxml);
            }
            catch(XmlException e)
            {
                result = e.ToString();
                return result;
            }

            var xdoc = XDocument.Load(new XmlNodeReader(xd));
            try
            {
                // try to make sure it URL is valid
                if(!Uri.TryCreate(URLxsd,UriKind.RelativeOrAbsolute, out myuri))
                {
                    return result = "XSD Error !";
                }
                setSchema.Add(null, URLxsd);
            }
            catch(XmlException e )
            {
                result = e.Message;
            }
            xdoc.Validate(setSchema, (o, e) => { result ="Validation Error: "+ e.Message; });


            return result;
        }
        public string addComputer(string screen_size,string brand,string model,string year, string Pro_Thread,
            string Pro_Arch_model, string Pro_Arch_generation, string Pro_clock,string Pro_cache,
            List<string>/*string*/ Sto_cache, string Sto_main, string Sto_harddrive)
        {
            string result = "";
            Computer addComputer = new Computer();
            // add all the infor to object Computer so i can use it later.
            addComputer.Screen_size = screen_size;
            addComputer.Brand = brand;
            addComputer.Model = model;
            addComputer.Year = year;
            addComputer.Processor.Architecture.Model = Pro_Arch_model;
            addComputer.Processor.Architecture.Generation = Pro_Arch_generation;
            addComputer.Processor.Clock = Pro_clock;
            addComputer.Processor.Cache = Pro_cache;
            addComputer.Processor.Thread = Pro_Thread;
            addComputer.Storage.Main = Sto_main;
            // because Cach in storage can have more than 1 so i used list to call and add it in computer.
            foreach(string item in Sto_cache)
            {
                addComputer.Storage.Cache.Add(item);
            }
            //addComputer.Storage.Cache= Sto_cache;
            addComputer.Storage.Harddrive = Sto_harddrive;
            // find the path in to store xml in App_data. can you HttpRuntime.AppDomainAppPath + "\\
            string addData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data/Computers.xml");
            //HttpContext.Current.Request.PhysicalApplicationPath
            XmlDocument xd = new XmlDocument();
            xd.Load(addData);
            // add the element in the xml
            try
            {
                XElement xml = XElement.Load(addData);     
                xml.Add(new XElement("Computer",
                    new XAttribute("Screen_size", addComputer.Screen_size),
                    new XElement("Brand",addComputer.Brand),
                    new XElement("Model",addComputer.Model),
                    new XElement("Year",addComputer.Year),
                    new XElement("Processor", 
                    new XAttribute("Thread",addComputer.Processor.Thread),
                    new XElement("Architecture",
                    new XElement("Model",addComputer.Processor.Architecture.Model), 
                    new XElement("Generation",addComputer.Processor.Architecture.Generation)),
                    new XElement("Clock",addComputer.Processor.Clock),
                    new XElement("Cache",addComputer.Processor.Cache)),

                    new XElement("Storage",
                    //new XElement("Cache", addComputer.Storage.Cache),
                    //new XElement("Cache",
                    from cache in addComputer.Storage.Cache select new XElement("Cache",cache), 

                    new XElement("Main",addComputer.Storage.Main),
                    new XElement("Harddrive",addComputer.Storage.Harddrive))
                    ));
                // save it to xml file.
                xml.Save(addData);



                result = "New computer has been added!";
            }
            catch (Exception e)
            {
                result = e.Message;
            }

          


            return result;
        }
        // I try to do Xpath but dont like it.
        // http://api.openweathermap.org/data/2.5/weather?q=Phoenix&mode=xml&appid=25519bc3dabd4e0ef126170527403c64
        ///current/city/coord/@lon 
        // / root/ element/@atribute
        public string XPathSearch(string URLxml,string path)
        {
            string result = "ii"; // when error path
            string value = "";
            string attribute = "none";
           // path += "//";
            XPathDocument dx = new XPathDocument(URLxml);
            XPathNavigator nav = dx.CreateNavigator();
            XPathNodeIterator iterator = nav.Select(path);

            /*XmlDocument xx = new XmlDocument();
            xx.Load(URLxml);
            preorderTraverse(xx.DocumentElement);
            string xmlText = xx.ToString();
            XDocument xDoc = XDocument.Parse(xmlText);
            List<string> results = xDoc.Root.Elements("Computer").Select(x => x.Element("Processor").Attribute("Threads").Value).ToList();*/

            while (iterator.MoveNext())
            {
                
                XPathNodeIterator Xnode = iterator.Current.Select(path);
                
                Xnode.MoveNext();

                attribute = Xnode.Current.GetAttribute("","");
                
                //Xnode.Current.GetAttribute()
                value = Xnode.Current.Value;
                

                result =" Value: " + value + " Atrubute: " + attribute;

            }

            return result;


            
        }

        public string XpathSe(string URLxml, string path,string name )
        {
            string result="not found";
            XmlDocument doc = new XmlDocument();
            doc.Load(URLxml);
            foreach(XmlNode row in doc.SelectNodes(path))
            {
                //string rowName = row.SelectSingleNode(name).InnerText;
                XmlAttributeCollection attributes = row.SelectSingleNode(name).Attributes;
                string rowname = attributes.ToString();
                result = rowname;
            }
            return result;
        }



    }

    // class for store addCOmputer info. 
    public class Computer
    {
        string screen_size;
        string brand;
        string model;
        string year;
        ProcessorO processor = new ProcessorO();
        StorageO storage = new StorageO();
        
        public string Screen_size
        {
            get { return screen_size; }
            set { screen_size = value; }
        }
        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        public string Year
        {
            get { return year; }
            set { year = value; }
        }
        public ProcessorO Processor
        {
            get { return processor; }
            set { processor = value; }
        }
        public StorageO Storage
        {
            get { return storage; }
            set { storage = value; }
        }


   
    }
    public class ProcessorO
    {
        string thread;
        ArchitectureO architecture = new ArchitectureO();
        string clock;
        string cache;
        public string Thread
        {
            get { return thread; }
            set { thread = value; }
        }

        public ArchitectureO Architecture
        {
            get { return architecture; }
            set { architecture = value; }
        }
        public string Clock
        {
            get { return clock; }
            set { clock = value; }
        }
        public string Cache
        {
            get { return cache; }
            set { cache = value; }
        }

    }
    public class ArchitectureO
    {
        string model;
        string generation;
        public string Model
        {
            get { return model; }
            set { model = value; }
        }
        public string Generation
        {
            get { return generation; }
            set { generation = value; }
        }


    }
    public class StorageO
    {
        List<string> cache = new List<string>();
        //string cache;
        string main;
        string harddrive;
        public List<string> Cache
        {
            get { return cache; }
            set { cache = value; }
        }
        /*public string Cache
        {
            get { return cache; }
            set { cache = value; }
        }*/

        public string Main
        {
            get { return main; }
            set { main = value; }
        }
        public string Harddrive
        {
            get { return harddrive; }
            set { harddrive = value; }
        }
    }
}
