using System;
using System.ComponentModel.DataAnnotations;
using Sitecore.Data;
using Sitecore.Data.Items;
using Newtonsoft.Json;
namespace TechITPro.Samples.Models
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class SitecoreValidationAttribute : ValidationAttribute
    {
        public ValidationAttribute InnerValidationAttribute { get; set; }

        private Item _item;
        private string _id;
        private string _field;
        private string _jsonProperties;
        private Type _type;

        public SitecoreValidationAttribute()
        { }
        /// <param name="type">Type of ValidationAttribute to use.</param>
        /// <param name="id">Item ID of the Sitecore item to use for getting the error message string.</param>
        /// <param name="field">Field name of the Sitecore item to use for getting the error message string.</param>
        /// <param name="jsonProperties">Optional Json representation of properties to set on the validation attribute.</param>
        /// <param name="pattern">RegEx to be used for RegularExpressio Validations</param>

        public SitecoreValidationAttribute(Type type, string id, string field, string jsonProperties = null, string pattern = null)
            : base()
        {
            if (type == typeof(RegularExpressionAttribute))
            {
                InnerValidationAttribute = new RegularExpressionAttribute(pattern);//Activator.CreateInstance(type) as RegularExpressionAttribute;
            }
            else
            {
                InnerValidationAttribute = Activator.CreateInstance(type) as ValidationAttribute;

            }
            _type = type;
            _id = id;
            _field = field;
            _jsonProperties = jsonProperties;

            SetErrorMessage();
            if (!string.IsNullOrWhiteSpace(_jsonProperties)) SetProperties();
        }

        private void SetProperties()
        {
            dynamic jsonProperties = JsonConvert.DeserializeObject(_jsonProperties);
            foreach (var property in jsonProperties)
            {
                var propertyName = property.Name;
                var propertyValue = property.Value.Value;

                // Get a property on the type that is stored in the 
                // property string
                var propertyInfo = _type.GetProperty(propertyName);

                // Set the value of the given property on the given instance
                propertyInfo.SetValue(InnerValidationAttribute, propertyValue, null);
            }

        }

        private void SetErrorMessage()
        {
            if (ID.IsID(_id))
            {
                var db = Sitecore.Context.Database;
                if (db == null) return;

                _item = db.GetItem(new ID(_id));

                if (_item != null)
                {
                    var fieldValue = _item[_field];
                    if (!string.IsNullOrWhiteSpace(fieldValue))
                    {
                        ErrorMessage = fieldValue;
                    }
                }
            }
        }

        public override bool IsValid(object value)
        {
            return InnerValidationAttribute.IsValid(value);
        }

        // We are overriding TypeId, because during validation, the validator only allows 1 instance of an attribute
        // to be used on a property at a time.  This validation attribute is written such that it is a "generic" attribute
        // that can be used many times to act like the inner validation attribute.  We override the typeid and set it 
        // to the value of the inner validation attribute type.
        public override object TypeId
        {
            get
            {
                return InnerValidationAttribute.TypeId;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            SetErrorMessage();
            InnerValidationAttribute.ErrorMessage = ErrorMessage;
            return InnerValidationAttribute.FormatErrorMessage(name);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }

    }
}
