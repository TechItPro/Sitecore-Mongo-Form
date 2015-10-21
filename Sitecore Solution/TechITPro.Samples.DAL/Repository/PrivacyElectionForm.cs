using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using TechITPro.Samples.Models;
using System.Linq;

namespace TechITPro.Samples.DAL.Repository
{
    public class PrivacyElectionForm : BaseRepository<PrivacyFormData>
    {
        #region Private Variables
        #endregion

        #region ..ctor
        public PrivacyElectionForm(string connectionString, string collectionName)
            : base(connectionString, collectionName)
        {
            var client = new MongoClient(connectionString);
            var _databaseName = MongoUrl.Create(connectionString).DatabaseName;
            var server = client.GetServer();
            var database = server.GetDatabase(_databaseName);
            MongoCollection = database.GetCollection<BsonDocument>(collectionName);
        }
        #endregion

        #region Private Properties
        #endregion

        #region Public Properties
        public MongoCollection<BsonDocument> MongoCollection { get; set; }
        public override string DataBaseName { get; set; }
        public override string CollectionName { get; set; }
        public override string ConnectionString { get; set; }
        #endregion

        #region Public Methods

        public override IEnumerable<PrivacyFormData> Select(IMongoQuery query)
        {
            List<PrivacyFormData> lstretForms = new List<PrivacyFormData>();
            var lstPForms = MongoCollection.Find(query).ToList();

            foreach (var form in lstPForms)
            {
                PrivacyFormData pForm = BsonSerializer.Deserialize<PrivacyFormData>(form);
                lstretForms.Add(pForm);

            }
            return lstretForms.OrderByDescending(x => x.AuditInsertDateUTC);
        }

        public override IEnumerable<PrivacyFormData> SelectAll()
        {
            try
            {
                List<PrivacyFormData> lstretForms = new List<PrivacyFormData>();
                var lstPForms = MongoCollection.FindAll().ToList();

                foreach (var form in lstPForms)
                {
                    PrivacyFormData pForm = BsonSerializer.Deserialize<PrivacyFormData>(form);
                    lstretForms.Add(pForm);

                }
                return lstretForms.OrderByDescending(x => x.AuditInsertDateUTC);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public override void Insert(PrivacyFormData privacyForm)
        {
            
            var document = new BsonDocument();
            
            document.Add("FirstName", BsonValue.Create(privacyForm.FirstName));
            document.Add("LastName", BsonValue.Create(privacyForm.LastName));
            document.Add("Address", BsonValue.Create(privacyForm.Address));
            document.Add("City", BsonValue.Create(privacyForm.City));
            document.Add("State", BsonValue.Create(privacyForm.State));
            document.Add("ZipCode", BsonValue.Create(privacyForm.ZipCode));
            document.Add("Phone", BsonValue.Create(privacyForm.Phone));
            document.Add("AuditInsertDateUTC", new BsonDateTime(DateTime.UtcNow));            
            document.Add("SourceCd", BsonValue.Create(privacyForm.SourceCd));
            
            MongoCollection.Insert(document);
        }
        #endregion



    }
}
