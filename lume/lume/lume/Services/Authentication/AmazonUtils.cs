using System;
using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using lume.Services;

namespace Roma3Assistant.Services.Authentication
{
    public class AmazonUtils
    {
        // Store the User locally
        private static CognitoUser cognitoUser;
        public static string CognitoUserId = string.Empty;
        public static CognitoUser CognitoUser
        {
            get
            {
                if (cognitoUser == null)
                {
                    cognitoUser = GetCurrentUser(CognitoUserId);
                }
                return cognitoUser;
            }
        }

        // Get current user to verify password
        private static CognitoUser GetCurrentUser(string userId)
        {
            CognitoUser tempUser = new CognitoUser(userId, Constants.ClientID, UserPool, IdentityClientProvider);
            return tempUser;
        }

        // Reference the user pool where the user data is stored
        private static CognitoUserPool userPool;
        public static CognitoUserPool UserPool
        {
            get
            {
                if (userPool == null)
                {
                    userPool = new CognitoUserPool(Constants.PoolID, Constants.ClientID, IdentityClientProvider);
                }

                return userPool;
            }
        }

        // Identity provider
        private static AmazonCognitoIdentityProviderClient identityClientProvider;
        public static AmazonCognitoIdentityProviderClient IdentityClientProvider
        {
            get
            {
                if (identityClientProvider == null)
                {
                    identityClientProvider = new AmazonCognitoIdentityProviderClient(new AnonymousAWSCredentials(), RegionEndpoint.USEast1);
                }

                return identityClientProvider;
            }
        }
    }
}