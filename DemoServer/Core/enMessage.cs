using System;
using System.Xml;

namespace DemoServer.Core
{
    public static class enMessage
    {
        public static bool GetActionName(XmlDocument oMessage, ref string sActionName)
        {
            sActionName = "ClientActionDealDamage"; // TMP.
            return true;
        }

        public static XmlDocument CreateErrorMessage(string sErrorMessage)
        {
            // TODO.
            return new XmlDocument();
        }

        public static XmlDocument CreateMessage(string sMessageName, XmlDocument oData)
        {
            // TODO.
            return oData;
        }
    }
}