﻿using System;

namespace Bruttissimo.Domain.Entity.Entities
{
    public class User
    {
        public long Id { get; set; }

        public long RoleId { get; set; }
        public Role Role { get; set; }

        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }

        public DateTime Created { get; set; }

        public long UserSettingsId { get; set; }
        public UserSettings Settings { get; set; }
    }
}
