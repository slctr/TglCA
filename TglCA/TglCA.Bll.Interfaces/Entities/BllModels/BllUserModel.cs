﻿namespace TglCA.Bll.Interfaces.Entities.BllModels
{
    public class BllUserModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string UserName => GetUserName();

        private string GetUserName()
        {
            if (Email == null) return string.Empty;
            Span<char> emailSpan = new Span<char>(Email.ToCharArray());
            return emailSpan.Slice(0, emailSpan.IndexOf("@")).ToString();
        }
    }
}
