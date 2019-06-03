using SIS.MvcFramework.Identity;
using System;

namespace SIS.MvcFramework.Attributes.Security
{
    public class AuthorizeAttribute : Attribute
    {
        public AuthorizeAttribute(string authority = "authorized")
        {
            _authority = authority;
        }

        private readonly string _authority;

        private static bool IsLoggedIn(Principal principal)
        {
            return principal != null;
        }

        public bool IsInAuthority(Principal principal)
        {
            if (!IsLoggedIn(principal))
            {
                return _authority == "anonymous";
            }

            return _authority == "authorized"
                   || principal.Roles.Contains(_authority.ToLower());
        }
    }
}