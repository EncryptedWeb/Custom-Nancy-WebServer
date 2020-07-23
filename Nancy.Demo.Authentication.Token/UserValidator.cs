﻿namespace Nancy.Demo.Authentication.Token
{
    using Nancy.Authentication.Basic;
    using Nancy.Security;

    public class UserValidator : IUserValidator
    {
        public IUserIdentity Validate(string username, string password)
        {
            if (username == "demo" && password == "demo")
            {
                return new DemoUserIdentity { UserName = username, Claims = new []{"SessionToken"}};
            }

            // Not recognised => anonymous.
            return null;
        }
    }
}
