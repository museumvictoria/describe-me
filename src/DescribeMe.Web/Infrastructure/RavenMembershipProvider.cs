﻿using System;
using System.Collections.Generic;
using System.Web.Security;
using DescribeMe.Core.DomainModels;
using DescribeMe.Core.Indexes;
using DescribeMe.Core.Utilities;
using Microsoft.Practices.ServiceLocation;
using Raven.Client;
using Raven.Client.Linq;
using System.Linq;
using WebMatrix.WebData;

namespace DescribeMe.Web.Infrastructure
{
    public class RavenMembershipProvider : ExtendedMembershipProvider
    {
        private readonly IDocumentStore _documentStore;

        public RavenMembershipProvider()
        {
            _documentStore = ServiceLocator.Current.GetInstance<IDocumentStore>();
        }

        public override int GetUserIdFromOAuth(string provider, string providerUserId)
        {
            using (var documentSession = _documentStore.OpenSession())
            {

                var user = documentSession
                    .Query<User, Users_ByProvider>()
                    .Where(x => x.ProviderUserId == providerUserId && x.Provider == provider)
                    .FirstOrDefault();

                if (user != null)
                    return RavenIdResolver.ResolveToInt(user.Id);
            }

            return -1;
        }

        public override string GetUserNameFromId(int userId)
        {
            using (var documentSession = _documentStore.OpenSession())
            {
                var user = documentSession.Load<User>(userId);
                if (user != null)
                {
                    return user.Name;                    
                }                
            }

            return string.Empty;
        }

        #region NotImplemented

        public override MembershipUser CreateUser(string username, string password, string email,
                                                  string passwordQuestion, string passwordAnswer, bool isApproved,
                                                  object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
                                                             string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
                                                                 out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
                                                                  out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override ICollection<OAuthAccountData> GetAccountsForUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override string CreateUserAndAccount(string userName, string password, bool requireConfirmation,
                                                    IDictionary<string, object> values)
        {
            throw new NotImplementedException();
        }

        public override string CreateAccount(string userName, string password, bool requireConfirmationToken)
        {
            throw new NotImplementedException();
        }

        public override bool ConfirmAccount(string userName, string accountConfirmationToken)
        {
            throw new NotImplementedException();
        }

        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteAccount(string userName)
        {
            throw new NotImplementedException();
        }

        public override string GeneratePasswordResetToken(string userName, int tokenExpirationInMinutesFromNow)
        {
            throw new NotImplementedException();
        }

        public override int GetUserIdFromPasswordResetToken(string token)
        {
            throw new NotImplementedException();
        }

        public override bool IsConfirmed(string userName)
        {
            throw new NotImplementedException();
        }

        public override bool ResetPasswordWithToken(string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override int GetPasswordFailuresSinceLastSuccess(string userName)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetCreateDate(string userName)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetPasswordChangedDate(string userName)
        {
            throw new NotImplementedException();
        }

        public override DateTime GetLastPasswordFailureDate(string userName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}