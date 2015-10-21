using Glass.Mapper.Sc.Web.Mvc;
using Sitecore.Diagnostics;
using System;
using System.Configuration;
using System.Web.Mvc;
using TechITPro.Samples.DAL.Repository;
using TechITPro.Samples.Models;
using TechITPro.Samples.Models.Utility;

namespace SCMongoForm.TechITPro.Controllers
{
    class PrivacyFormController : GlassController
    {
        public ActionResult ProcessPrivacyForm()
        {
            PrivacyForm model = defaultModel();
            if (model == null)
            {
                return new EmptyResult();
            }
            return View("~/TechITPro/Views/Renderings/Sections/Components/PrivacyForm/PrivacyForm.cshtml", model);
        }

        [HttpPost]
        public ActionResult ProcessPrivacyForm(PrivacyForm model)
        {
            try
            {
                model.FormSubmitted = true;
                if (ModelState.IsValid)
                {
                    //TODO -- simplify -- just use connection string in connection strings - and just have 1 constant for dbName and CollectionName
                    string conString = ConfigurationManager.ConnectionStrings["privacyelection.write"].ToString();
                    string collectionName = Constants.DataBaseConstants.CollectionNames.PRIVACYELECTIONS;

                    PrivacyFormData formData = new PrivacyFormData();
                    formData.FirstName = model.FirstName;
                    formData.LastName = model.LastName;
                    formData.Address = model.Address;
                    formData.City = model.City;
                    formData.State = model.State;
                    formData.ZipCode = model.ZipCode;
                    formData.Phone = model.Phone;
                    PrivacyElectionForm pFormRepo = new PrivacyElectionForm(conString, collectionName);
                    pFormRepo.Insert(formData);
                    //Need to clear form controls so loading model again
                    model = defaultModel();
                    model.SaveSuccessful = true;
                    model.FormSubmitted = true;
                    return View("~/TechITPro/Views/Renderings/Sections/Components/PrivacyForm/PrivacyForm.cshtml", model);
                }
                else
                {
                    //Set error when form is not submitted
                    ModelState.AddModelError("", model.ErrorAlert);
                    model.FormSubmitted = false;
                    var combinedmodel = this.CombineModel(model);
                    return View("~/TechITPro/Views/Renderings/Sections/Components/PrivacyForm/PrivacyForm.cshtml", combinedmodel);
                }
            }
            catch (Exception ex)
            {
                model.SaveSuccessful = false;
                Log.Error("Error Submitting Privacy Form" + ex.StackTrace, this);
                return View("~/TechITPro/Views/Renderings/Sections/Components/PrivacyForm/PrivacyForm.cshtml");
            }
        }

        private PrivacyForm defaultModel()
        {
            var model = SitecoreContext.Cast<PrivacyForm>(SitecoreUtility.GetDataSourceItem());
            return model;
        }
        private PrivacyForm CombineModel(PrivacyForm model)
        {
            var defModel = defaultModel();
            return new PrivacyForm
            {
                FirstName = model.FirstName,
                FirstNamePlaceholder = defModel.FirstNamePlaceholder,
                LastName = model.LastName,
                LastNamePlaceholder = defModel.LastNamePlaceholder,
                Address = model.Address,
                AddressPlaceholder = defModel.AddressPlaceholder,
                City = model.City,
                CityPlaceholder = defModel.CityPlaceholder,
                ZipCode = model.ZipCode,
                ZipCodePlaceholder = defModel.ZipCodePlaceholder,
                Phone = model.Phone,
                PhonePlaceholder = defModel.PhonePlaceholder,                
                States = defModel.States,
                StatePlaceholder = defModel.StatePlaceholder,
                
                SaveSuccessful = model.SaveSuccessful,
                FormSubmitted = model.FormSubmitted,
                SuccessAlert = defModel.SuccessAlert,
                ErrorAlert = defModel.ErrorAlert,
                SubmitButtonText = defModel.SubmitButtonText,
                
            };
        }
    }
}
