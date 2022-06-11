namespace e_ticaret.Areas.Admin.Controllers
{
    public class AuthorizationClass
    {
        public bool IsAuthorized(string authorization, ISession session)
        {
            string? sessionAuthorization=session.GetString(authorization);
            if (sessionAuthorization=="True")
            {
                return true;
            }
            return false;
        }
    }
}
