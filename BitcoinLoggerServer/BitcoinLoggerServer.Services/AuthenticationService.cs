using BitcoinLoggerServer.Contracts.Models;
using BitcoinLoggerServer.Framework.Models;
using BitcoinLoggerServer.Framework.Security;
using System.Linq;


namespace BitcoinLoggerServer.Services
{
    public class AuthenticationService
    {
        private readonly UserService _UserService;
        private readonly UserSessionService _UserSessionsService;
        private readonly JWTService _JWTService;
        private readonly EncryptionService _EncryptionService;

        public AuthenticationService(UserService userService, UserSessionService userSessionsService, JWTService JWTService, EncryptionService encryptionService)
        {
            _UserService = userService;
            _UserSessionsService = userSessionsService;
            _JWTService = JWTService;
            _EncryptionService = encryptionService;
        }

        public UserSession Login(User user)
        {
            if (user == null) throw new BusinessException("User cannot be null.");
            if (string.IsNullOrEmpty(user.Email)) throw new BusinessException("User Email cannot be null.");

            _UserService.Filter(entity => entity.Email == user.Email);
                        
            User dbUser = this._UserService.Filter(dbUser => dbUser.Email.Trim().ToLower() == user.Email.Trim().ToLower()).SingleOrDefault();

            if (dbUser == null) throw new BusinessException("User with email " + user.Email + " does not exist.");

            if (dbUser.Password != _EncryptionService.EncryptPassword(user.Password)) throw new BusinessException("Email/Password don't match.");
                        
            return new UserSession()
            {
                UserId = dbUser.Id,
                SessionKey = _JWTService.GenerateUserSession(user)
            };
        }
    }
}
