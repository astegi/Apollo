using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Apollo.Core.Authentication
{
    public class User
    {
        private static User _currentUser;
        public static User CurrentUser
        {
            get
            {
                return _currentUser ?? (_currentUser = new User());
            }
        }

        private readonly List<string> _groups;
        private readonly bool _isGuest;
        private readonly bool _isSystem;

        public string Name { get; set; }
        public bool IsSystem { get { return _isSystem; } }
        public bool IsGuest { get { return _isGuest; } }
        public List<string> Groups { get { return _groups; } }
        
        public User()
        {
            _groups = new List<string>();

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            Name = identity.Name;
            _isSystem = identity.IsSystem;
            _isGuest = identity.IsGuest;

            foreach (IdentityReference reference in identity.Groups)
            {
                IdentityReference translated = reference.Translate(typeof(NTAccount));
                _groups.Add(translated.Value);
            }
        }
    }
}
