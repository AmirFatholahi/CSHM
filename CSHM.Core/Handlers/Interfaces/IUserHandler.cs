using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using CSHM.Presentation.Base;
using CSHM.Domain;
using CSHM.Widget.Rest;
using CSHM.Presentation.OTP;
using CSHM.Presentations.User;
using CSHM.Presentations.Login;

namespace CSHM.Core.Handlers.Interfaces;

public interface IUserHandler
{
    public ResultViewModel<UserViewModel> KYC(UserViewModel entity, bool isNeedCaptcha = true);

    public MessageViewModel AddUserBridge(UserViewModel entity, int creatorID, string ip);

    public TokenViewModel GetRefreshToken(int step, string validAudience, int userID, string oldJti, DateTime firstLogin);

    public MessageViewModel SelectCellphoneOfUser(int userID);


    public bool GetUserGaurdStatus(int userID);

    public MessageViewModel SetUserGaurdStatus(int userID, int creatorID);

    public List<string> GetUserClaims(ClaimsPrincipal user, string key);

    public int GetUserID(ClaimsPrincipal user);


    public TokenViewModel? Login(LoginViewModel entity, string validAudience, bool withoutCaptcha, string ip);

    public MessageViewModel Logout(int userID, string ip, string oldJti);

    public List<KeyValueViewModel> GetClaimsList(string userName);

    public Base64ViewModel GetQRCodeOfSecretKey(int userID);

    public MessageViewModel GenerateSecretKey(int userID);
}