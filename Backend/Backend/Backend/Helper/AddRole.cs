using Role = Backend.Backend.Helper.Enum.PosEnum.PosStatus;

namespace Backend.Backend.Helper
{
    public static class AddRole
    {
        /// <summary>
        /// Add role through email, using this method
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static (Role? role, int status_code) AddRoleAccordingToEmail(this string email)
        {
            Role? role;
            int status_code;
            if(email.Contains("@sup.user"))
            {
                role = Role.SUP;
                status_code = 200;
            } else if (email.Contains("@dbtc-cebu"))
            {
                role = Role.STU;
                status_code = 200;
            } else if (email.Contains("@local"))
            {
                role = Role.TEA;
                status_code = 200;
            } else
            {
                role = null;
                status_code = 404;
            }

            return (role, status_code);
        }
    }
}
