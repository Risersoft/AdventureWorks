using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Risersoft.Framework.Dependency;
using Xamarin.Forms;

[assembly: Dependency(typeof(ConfigService))]
namespace Risersoft.Framework.Dependency
{
    public class ConfigService : IConfiguration


    {
        public string LoginServiceHost()
        {
            return "http://risersoft.login.appsframework.com";
            //return "http://dse3.kanohar.net:11626";
        }

        public string RestServiceHost()
        {
           
            return "http://risersoft.pub.appsframework.com";
        }


        public string AppName()
        {
            return "AdventureWorks";
        }
        
        public string ClientId()
        {
            return "advw.demo";
        }

        public string ClientSecret()
        {
              return "advwdemo123";
        }
        public string AppList() {
            return "advdem";
        }

    }
}