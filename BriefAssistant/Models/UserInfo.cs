using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;

namespace BriefAssistant.Models
{
    [DataContract]
    public class UserInfo
    {
        [DataMember(Name = OpenIdConnectConstants.Claims.Subject)]
        public string Sub { get; set; }
        [DataMember(Name = OpenIdConnectConstants.Claims.Email)]
        public string Email { get; set; }
        [DataMember(Name = OpenIdConnectConstants.Claims.EmailVerified)]
        public bool EmailVerified { get; set; }
        [DataMember(Name = OpenIdConnectConstants.Claims.Role)]
        public IList<string> Roles { get; set; }
    }
}
