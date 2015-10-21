using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Sitecore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Configuration;
using TechITPro.Samples.DAL.Repository;
using TechITPro.Samples.Models;

namespace SCMongoForm.sitecore.admin.TechITPro
{
    public partial class PrivacyElectionFormAdmin : Sitecore.sitecore.admin.AdminPage
    {
        protected override void OnInit(EventArgs e)
        {
            FormError.Visible = false;
            base.CheckSecurity(true); //Required!
            base.OnInit(e);
        }
              
        protected void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                FormError.Visible = false;
                FormResults.Visible = true;
                IEnumerable<PrivacyFormData> privacyForms = ViewPrivacyForms(Convert.ToDateTime(startDate.Value), Convert.ToDateTime(toDate.Value));
                rptPForms.DataSource = privacyForms;
                rptPForms.DataBind();
            }
            catch (Exception ex)
            {
                FormError.Visible = true;
                FormResults.Visible = false;
                Log.Error("Error viewing Privacy Form Data" + ex, this);
            }
        }
        public static IEnumerable<PrivacyFormData> ViewPrivacyForms(DateTime startDate, DateTime endDate)
        {
            string conString = ConfigurationManager.ConnectionStrings["privacyelection.write"].ToString();
            string collectionName = Constants.DataBaseConstants.CollectionNames.PRIVACYELECTIONS;

            PrivacyElectionForm pFormRepo = new PrivacyElectionForm
                (conString,
                collectionName);
            IMongoQuery startDatequery = Query<PrivacyFormData>.GTE(p => p.AuditInsertDateUTC, new BsonDateTime(startDate.ToUniversalTime()));
            IMongoQuery endDatequery = Query<PrivacyFormData>.LTE(p => p.AuditInsertDateUTC, new BsonDateTime(endDate.AddHours(23.59).ToUniversalTime()));

            return pFormRepo.Select(Query.And(startDatequery, endDatequery));

        }
    }
}