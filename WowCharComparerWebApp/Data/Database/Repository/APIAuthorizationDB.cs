using System;
using System.Collections.Generic;
using System.Linq;
using WowCharComparerWebApp.Models.Internal;

namespace WowCharComparerWebApp.Data.Database.Repository
{
    //TODO add some middle layer to handle exception
    public class APIAuthorizationDB
    {
        #region Inserting methods

        public static void AddNewApplicationClient(string clientID, string clientSecret, string clientName, DateTime validationUntil)
        {
            using (var db = new ComparerDatabaseContext())
            {
                db.APIClient.Add(new APIClient
                {
                    ClientID = clientID,
                    ClientName = clientName,
                    ClientSecret = clientSecret,
                    ValidationUntil = validationUntil
                });

                db.SaveChanges();
            }
        }

        #endregion

        #region Updating methods 

        public static void UpdateClientSecretByClientID(string clientID, string clientSecret, DateTime validationUntil)
        {
            using (var db = new ComparerDatabaseContext())
            {
                APIClient updatingRow = db.APIClient.SingleOrDefault(client => client.ClientID == clientID);

                if (updatingRow != null)
                {
                    updatingRow.ClientSecret = clientSecret;
                    updatingRow.ValidationUntil = validationUntil;
                    db.SaveChanges();
                }
            }
        }

        public static void UpdateClientSecretByClientName(string clientName, string clientSecret, DateTime validationUntil)
        {
            using (var db = new ComparerDatabaseContext())
            {
                APIClient updatingRow = db.APIClient.SingleOrDefault(client => client.ClientName == clientName);

                if (updatingRow != null)
                {
                    updatingRow.ClientSecret = clientSecret;
                    updatingRow.ValidationUntil = validationUntil;
                    db.SaveChanges();
                }
            }
        }

        #endregion

        #region Get methods 

        public static APIClient GetClientInformation(string clientID)
        {
            using (var db = new ComparerDatabaseContext())
            {
                return db.APIClient.SingleOrDefault(x => x.ClientID == clientID);
            }
        }

        #endregion 
    }
}
