namespace TrackzamClient
{
    public class UserLogin
    {
        public bool IsLoggedIn = false;

        public bool RetrieveLoginStatus()
        {
            if (InfoSaver.UserIsStored())
            {
                var result = TryLogin(InfoSaver.GetUser(), InfoSaver.GetPass());
                IsLoggedIn = result;
                return result;
            }
            else
            {
                IsLoggedIn = false;
                return false;
            }
        }
        
        public bool TryLogin(string login, string password)
        {
            InfoSaver.StoreUserInfo(login, password);
            return true;
        }
    }
}