using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using System.Configuration;
using System.Threading.Tasks;
using Amazon;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using Microsoft.AspNet.Identity;
using System;

namespace lume.Services.Cognito
{
    public class CognitoUserStore : Microsoft.AspNetCore.Identity.IUserStore<CognitoUser>,
                                    IUserLockoutStore<CognitoUser, string>,
                                    IUserTwoFactorStore<CognitoUser, string>
    {
        private readonly AmazonCognitoIdentityProviderClient _client =
            new AmazonCognitoIdentityProviderClient();
        private readonly string _clientId = ConfigurationManager.AppSettings["CLIENT_ID"];
        private readonly string _poolId = ConfigurationManager.AppSettings["USERPOOL_ID"];

        public Task CreateAsync(CognitoUser user) 
        {
            // Register the user using Cognito
            var signUpRequest = new SignUpRequest
            {
                ClientId = ConfigurationManager.AppSettings["CLIENT_ID"],
                Password = user.Password,
                Username = user.Email,

            };

            var emailAttribute = new AttributeType
            {
                Name = "email",
                Value = user.Email
            };
            signUpRequest.UserAttributes.Add(emailAttribute);

            return _client.SignUpAsync(signUpRequest);
        }

        public Task<IdentityResult> CreateAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(CognitoUser user)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public Task<CognitoUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<CognitoUser> FindByIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<CognitoUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<CognitoUser> FindByNameAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> GetAccessFailedCountAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GetLockoutEnabledAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(CognitoUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserIdAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserNameAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(CognitoUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(CognitoUser user, bool enabled)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(CognitoUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(CognitoUser user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(CognitoUser user, bool enabled)
        {
            throw new System.NotImplementedException();
        }

        public Task SetUserNameAsync(CognitoUser user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(CognitoUser user)
        {
            throw new System.NotImplementedException();
        }

        Task<Microsoft.AspNetCore.Identity.IdentityResult> Microsoft.AspNetCore.Identity.IUserStore<CognitoUser>.CreateAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Microsoft.AspNetCore.Identity.IdentityResult> Microsoft.AspNetCore.Identity.IUserStore<CognitoUser>.DeleteAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        Task<Microsoft.AspNetCore.Identity.IdentityResult> Microsoft.AspNetCore.Identity.IUserStore<CognitoUser>.UpdateAsync(CognitoUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

