using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;

namespace Roma3Assistant.Services.Authentication
{
    public class AuthenticationService
    {
        private readonly CognitoUser _user;


        public AuthenticationService(string username)
        {
            AmazonUtils.CognitoUserId = username;
            _user = AmazonUtils.CognitoUser;
        }

        public async Task<string> AuthenticateUser(string password)
        {
            var initiateSrpAuth = new InitiateSrpAuthRequest { Password = password };
            var response = await _user.StartWithSrpAuthAsync(initiateSrpAuth);
            var accessToken = response.AuthenticationResult.AccessToken;
            return accessToken;
        }

        public static List<AttributeType> GetUserAttributes(string accessToken)
        {
            var getUserAttributesRequest = new GetUserRequest { AccessToken = accessToken };
            var getUser = AmazonUtils.IdentityClientProvider.GetUserAsync(getUserAttributesRequest);
            var userAttributes = getUser.Result.UserAttributes;
            return userAttributes;
        }
    }
}