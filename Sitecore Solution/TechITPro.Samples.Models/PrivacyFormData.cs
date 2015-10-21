using MongoDB.Bson.Serialization.Attributes;
using System;
using Glass.Mapper.Sc.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace TechITPro.Samples.Models
{
    [BsonIgnoreExtraElements]
    public class PrivacyFormData
    {
        public PrivacyFormData()
        {

        }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string SourceCd { get; set; }
        public DateTime AuditInsertDateUTC { get; set; }
        
    }

    [SitecoreType(TemplateId = Constants.SitecoreTemplates.PrivacyElectionFormTemplateId)]
    public class PrivacyForm 
    {
        private const string privacyFormId = "{CEC4839B-D427-46FA-83A6-EF519BA0421A}";

        public virtual bool SaveSuccessful { get; set; }
        public virtual bool FormValid { get; set; }
        public virtual bool FormSubmitted { get; set; }
        
        [SitecoreValidation(typeof(RequiredAttribute), privacyFormId, "First Name Required Message")]
        [SitecoreField(FieldName = "FirstName")]
        public virtual string FirstName { get; set; }

        [SitecoreValidation(typeof(RequiredAttribute), privacyFormId, "Last Name Required Message")]
        [SitecoreField(FieldName = "LastName")]
        public virtual string LastName { get; set; }

        [SitecoreValidation(typeof(RequiredAttribute), privacyFormId, "Address Required Message")]
        [SitecoreField(FieldName = "Address")]
        public virtual string Address { get; set; }

        [SitecoreValidation(typeof(RequiredAttribute), privacyFormId, "City Required Message")]
        [SitecoreField(FieldName = "City")]
        public virtual string City { get; set; }

        [SitecoreValidation(typeof(RequiredAttribute), privacyFormId, "State Required Message")]
        [SitecoreField(FieldName = "State")]
        public virtual string State { get; set; }

        [SitecoreValidation(typeof(RequiredAttribute), privacyFormId, "Zip Code Required Message")]
        [SitecoreValidation(typeof(RegularExpressionAttribute), privacyFormId, "Zip Code Format Message", "", TechITPro.Samples.Models.Constants.RegEx.VALID_ZIPCODE)]
        [SitecoreField(FieldName = "Zip")]
        public virtual string ZipCode { get; set; }

        [SitecoreValidation(typeof(RequiredAttribute), privacyFormId, "Phone Required Message")]
        [SitecoreValidation(typeof(RegularExpressionAttribute), privacyFormId, "Phone Format Message", "", TechITPro.Samples.Models.Constants.RegEx.VALID_PHONENUMBER)]
        [SitecoreField(FieldName = "Phone")]
        public virtual string Phone { get; set; }
        
        [SitecoreField("States")]        
        public virtual IDictionary<string, string> States { get; set; }

        [SitecoreField(FieldName = "Success Alert")]
        public virtual string SuccessAlert { get; set; }

        [SitecoreField(FieldName = "Error Alert")]
        public virtual string ErrorAlert { get; set; }

        [SitecoreField(FieldName = "First Name Placeholder")]
        public virtual string FirstNamePlaceholder { get; set; }

        [SitecoreField(FieldName = "Last Name Placeholder")]
        public virtual string LastNamePlaceholder { get; set; }

        [SitecoreField(FieldName = "Address Placeholder")]
        public virtual string AddressPlaceholder { get; set; }

        [SitecoreField(FieldName = "City Placeholder")]
        public virtual string CityPlaceholder { get; set; }

        [SitecoreField(FieldName = "State Placeholder")]
        public virtual string StatePlaceholder { get; set; }

        [SitecoreField(FieldName = "Zip Code Placeholder")]
        public virtual string ZipCodePlaceholder { get; set; }

        [SitecoreField(FieldName = "Phone Number Placeholder")]
        public virtual string PhonePlaceholder { get; set; }
        
        [SitecoreField(FieldName = "Submit Button Text")]
        public virtual string SubmitButtonText { get; set; }
    }
}

