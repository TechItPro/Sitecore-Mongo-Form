using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechITPro.Samples.Models
{
    public static class Constants
    {
        public static class SitecoreTemplates
        {
            public const string PrivacyElectionFormTemplateId = "{187FEC0E-73E9-463A-A30F-E5CADBABF5F1}";
        }
        public static class RegEx
        {
            public const string VALID_ZIPCODE = @"(^\d{5}$)";
            public const string VALID_PHONENUMBER = @"(^\d{10}$)";
        }

        public static class DataBaseConstants
        {
            public static class CollectionNames
            {
                public const string PRIVACYELECTIONS = "PrivacyElections";

            }
        }
    }

}
