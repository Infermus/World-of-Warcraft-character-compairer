using System;
using System.Linq;
using WowCharComparerWebApp.Models.Internal;

namespace WowCharComparerWebApp.Data.Database.Repository.BlizzardApiClient
{
    //TODO add some middle layer to handle exception
    public class DbAccessApiAuthorization
    {
        #region Inserting methods

        private ComparerDatabaseContext _comparerDatabaseContext;

        public DbAccessApiAuthorization(ComparerDatabaseContext comparerDatabaseContext)
        {
            _comparerDatabaseContext = comparerDatabaseContext;
        }

        public void AddNewApplicationClient(string clientID, string clientSecret, string clientName, DateTime validationUntil)
        {
            _comparerDatabaseContext.APIClient.Add(new APIClient
            {
                ClientID = clientID,
                ClientName = clientName,
                ClientSecret = clientSecret,
                ValidationUntil = validationUntil
            });

            _comparerDatabaseContext.SaveChanges();
        }

        #endregion

        #region Updating methods 

        public void UpdateClientSecretByClientID(string clientID, string clientSecret, DateTime validationUntil)
        {
            APIClient updatingRow = _comparerDatabaseContext.APIClient.SingleOrDefault(client => client.ClientID == clientID);

            if (updatingRow != null)
            {
                updatingRow.ClientSecret = clientSecret;
                updatingRow.ValidationUntil = validationUntil;
                _comparerDatabaseContext.SaveChanges();
            }
        }

        public void UpdateClientSecretByClientName(string clientName, string clientSecret, DateTime validationUntil)
        {
            APIClient updatingRow = _comparerDatabaseContext.APIClient.SingleOrDefault(client => client.ClientName == clientName);

            if (updatingRow != null)
            {
                updatingRow.ClientSecret = clientSecret;
                updatingRow.ValidationUntil = validationUntil;
                _comparerDatabaseContext.SaveChanges();
            }
        }

        #endregion

        #region Get methods 

        public APIClient GetClientInformation(string clientID)
        {
            return _comparerDatabaseContext.APIClient.SingleOrDefault(x => x.ClientID == clientID);
        }

        #endregion 
    }
}
