using System;
using System.Xml;

namespace DemoServer.Core
{
    public static class enMessage
    {
        public static bool GetActionName(XmlDocument oMessage, ref string sActionName)
        {
            sActionName = "ClientActionDealDamage"; // TEMP.
            return true;
        }

        public static XmlDocument CreateErrorMessage(string sErrorMessage)
        {
            return new XmlDocument();
        }
    }
}