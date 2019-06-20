using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZenProgramming.Heimdallr.Data.Repositories;
using ZenProgramming.Heimdallr.Entities;
using ZenProgramming.Heimdallr.Structures;
using ZenProgramming.Chakra.Core.Data;
using ZenProgramming.Chakra.Core.DataAnnotations.Extensions;
using ZenProgramming.Chakra.Core.ServicesLayers;
using ZenProgramming.Chakra.Core.Utilities.Security;

namespace ZenProgramming.Heimdallr.ServiceLayers
{
    /// <summary>
    /// Service layer for identity services
    /// </summary>
    public class IdentityServiceLayer: DataServiceLayerBase
    {
        #region Private fields
        private readonly IUserRepository _UserRepository;
        private readonly IAudienceRepository _AudienceRepository;
        private readonly IRefreshTokenRepository _RefreshTokenRepository;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataSession">Active data session</param>
        public IdentityServiceLayer(IDataSession dataSession)
            : base(dataSession)
        {
            //Inizializzo i repository
            _UserRepository = dataSession.ResolveRepository<IUserRepository>();
            _AudienceRepository = dataSession.ResolveRepository<IAudienceRepository>();
            _RefreshTokenRepository = dataSession.ResolveRepository<IRefreshTokenRepository>();
        }

        #region User

        /// <summary>
        /// Fetch list of all users
        /// </summary>
        /// <returns></returns>
        public IList<User> FetchAllUsers()
        {
            //Esecuzione in transazione
            using (var t = DataSession.BeginTransaction())
            {
                //Estrazione dati e commit
                var result = _UserRepository.Fetch();
                t.Commit();
                return result;
            }
        }

        /// <summary>
        /// Sign-in using credentials and return user
        /// </summary>
        /// <param name="userName">UserName</param>
        /// <param name="password">Password</param>
        /// <returns>Returns signed-in result</returns>
        public SignInResult SignIn(string userName, string password)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;

            //Recupero l'utente tramite username
            var user = GetUserByUserName(userName);

            //Se non è stato tovato, esco
            if (user == null)
                return null;

            //Calcolo l'hash delle credenziali passate
            string hash = ShaProcessor.Sha256Encrypt(password);

            //Se NON corrisponde con quello su user, esco
            if (!hash.Equals(user.PasswordHash))
                return null;           

            //TODO: Blocco l'account se ci sono le condizioni (es. troppi tentativi ravvicinati)
            //LockAccountOnConditions(user);

            //Eseguo il salvataggio dell'ultimo accesso, precedente a 
            //questo, perchè l'operazione di sign-in a tutti gli effetti
            //esegue un aggiornamento di questo campo. Ma è corretto
            //che nel risultato la data di emissione sia quella precedente
            //in modo che le informazioni mostrate all'utente siano in qualche
            //modo orientate a rendere l'utente stesso consapevole del
            //timing con cui è stato fatto l'ultimo accesso. Questa cosa gli
            //potrebbe permettere di verificare accessi fraudolenti al sistema
            var previousAccessDate = user.LastAccessDate;

            //Aggiorno la data di ultimo accesso
            user.LastAccessDate = DateTime.UtcNow;
            var validations = SaveEntity(user, _UserRepository);
            if (validations.Count > 0)
                throw new InvalidOperationException($"Validation of user failed: {validations.ToValidationSummary()}");

            //TODO: Se l'account è locked, verifico le condizioni di sblocco e lo sblocco
            //UnlockAccountOnConditions(user);

            //Imposto il provider interno di sign-in
            const string InternalSignInProvider = "default";

            //Eseguo la composizione ed emissione del result
            return new SignInResult
            {
                UserName = user.UserName,
                Email = user.Email,
                PersonName = user.PersonName,
                PersonSurname = user.PersonSurname,
                IsEnabled = user.IsEnabled,
                IsLocked = user.IsLocked,
                LastAccessDate = previousAccessDate, 
                SignInProvider = InternalSignInProvider
            };
        }

        /// <summary>
        /// Get single user by user name
        /// </summary>
        /// <param name="userName">User name</param>
        /// <returns>Returns user or null</returns>
        public User GetUserByUserName(string userName)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));

            //Esecuzione in transazione
            using (var t = DataSession.BeginTransaction())
            {
                //Estrazione dati e commit
                var result = _UserRepository.GetUserByUserName(userName);
                t.Commit();
                return result;
            }            
        }

        /// <summary>
        /// Checks if provided user has administrative grants
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>Returns true or false</returns>
        public bool HasAdministrativeGrants(User user)
        {
            //Validazione argomenti
            if (user == null) throw new ArgumentNullException(nameof(user));

            //Se il nome dell'utente è "Administrator"
            return user.UserName == "Administrator";
        }
        #endregion

        #region Audience

        /// <summary>
        /// Get single audience by client id
        /// </summary>
        /// <param name="clientId">Client id</param>
        /// <returns>Returns audience or null</returns>
        public Audience GetAudienceByClientId(string clientId)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(clientId)) throw new ArgumentNullException(nameof(clientId));

            //Esecuzione in transazione
            using (var t = DataSession.BeginTransaction())
            {
                //Estrazione dati e commit
                var result = _AudienceRepository.GetSingle(a => a.ClientId == clientId);
                t.Commit();
                return result;
            }
        }

        /// <summary>
        /// Saves audience on storage
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>Returns list of validations</returns>
        public IList<ValidationResult> SaveAudience(Audience entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Utilizzo il metodo base
            return SaveEntity(entity, _AudienceRepository);
        }

        /// <summary>
        /// Fetch list of audiences
        /// </summary>
        /// <returns>Returns list of audiences</returns>
        public IList<Audience> FetchAudiences()
        {
            //Esecuzione in transazione
            using (var t = DataSession.BeginTransaction())
            {
                //Estrazione dati e commit
                var result = _AudienceRepository.Fetch();
                t.Commit();
                return result;
            }
        }
        #endregion

        #region RefreshToken

        /// <summary>
        /// Get single refresh token using provided token
        /// </summary>
        /// <param name="tokenHash">Refresh token</param>
        /// <returns>Returns entity of null</returns>
        public RefreshToken GetRefreshToken(string tokenHash)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(tokenHash)) throw new ArgumentNullException(nameof(tokenHash));

            //Esecuzione in transazione
            using (var t = DataSession.BeginTransaction())
            {
                //Estrazione dati e commit
                var result = _RefreshTokenRepository.GetSingle(a => a.TokenHash == tokenHash);
                t.Commit();
                return result;
            }
        }

        /// <summary>
        /// Get refresh token using user and audience
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="audience">Audience</param>
        /// <returns>Returns refresh token or null</returns>
        private RefreshToken GetRefreshToken(User user, Audience audience)
        {
            //Validazione argomenti
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (audience == null) throw new ArgumentNullException(nameof(audience));

            //Esecuzione in transazione
            using (var t = DataSession.BeginTransaction())
            {
                //Estrazione dati e commit
                var result = _RefreshTokenRepository.GetSingle(a =>
                    a.UserName == user.UserName && a.ClientId == audience.ClientId);
                t.Commit();
                return result;
            }
        }

        /// <summary>
        /// Fetch list of refresh tokens on platform
        /// </summary>
        /// <returns>Returns list of refresh tokens</returns>
        public IList<RefreshToken> FetchRefreshTokens()
        {
            //Esecuzione in transazione
            using (var t = DataSession.BeginTransaction())
            {
                //Estrazione dati e commit
                var result = _RefreshTokenRepository.Fetch();
                t.Commit();
                return result;
            }
        }       

        /// <summary>
        /// Updates or creates refresh token using provided user and audience
        /// </summary>
        /// <param name="user">User</param>
        /// <param name="audience">Audience</param>
        /// <returns>Returns instance of refresh token</returns>
        public RefreshToken CreateOrUpdateRefreshToken(User user, Audience audience)
        {
            //Validazione argomenti
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (audience == null) throw new ArgumentNullException(nameof(audience));

            //Tento il recupero del refresh token dallo storage
            RefreshToken existing = GetRefreshToken(user, audience);

            //Se esiste devo verifica che non sia scaduto
            if (existing != null)
            {
                //Se è scaduto (data di scadenza minore di now)
                if (existing.ExpiresUtc < DateTime.UtcNow)
                {
                    //Eseguo la cancellazione
                    DeleteRefreshToken(existing);

                    //Imposto existing a null
                    existing = null;
                }

                //Se è ancora presente, lo ritorno
                if (existing != null)
                    return existing;
            }

            //Se arriviamo a questo punto è null
            //Creazione della nuova entità
            var newRefreshToken = new RefreshToken
            {
                ClientId = audience.ClientId,
                UserName = user.UserName,
                TokenHash = Guid.NewGuid().ToString("D"),
                ExpiresUtc = DateTime.UtcNow.AddMinutes(audience.RefreshTokenLifeTime),
                IssuedUtc = DateTime.UtcNow,
                ProtectedTicket = "EMPTY FOR NOW"
            };

            //Salvataggio sullo storage
            var validations = SaveEntity(newRefreshToken, _RefreshTokenRepository);
            if (validations.Count > 0)
                throw new InvalidOperationException($"Validation of refresh token during create failed: {validations.ToValidationSummary()}");

            //Ritorno l'istanza creata
            return newRefreshToken;
        }

        /// <summary>
        /// Deletes single refresh token
        /// </summary>
        /// <param name="entity">Token to delete</param>
        public void DeleteRefreshToken(RefreshToken entity)
        {
            //Validazione argomenti
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            //Esecuzione in transazione
            using (var t = DataSession.BeginTransaction())
            {
                //Estrazione dati e commit
                _RefreshTokenRepository.Delete(entity);
                t.Commit();
            }
        }

        #endregion

        /// <summary>
        /// Release resources on dispose
        /// </summary>
        /// <param name="isDisposing">Is explicit dispose</param>
        protected override void Dispose(bool isDisposing)
        {
            //Se siamo in rilascio
            if (isDisposing)
            {
                //RIlascio le risorse
                _UserRepository.Dispose();
                _AudienceRepository.Dispose();
                _RefreshTokenRepository.Dispose();
            }

            //Chiamo il metodo base
            base.Dispose(isDisposing);
        }        
    }
}
