namespace Nancy.Demo.Authentication.Token
{
    using Nancy.Authentication.Token;
    using Security;

    public class AuthModule : NancyModule
    {
        public AuthModule(ITokenizer tokenizer)
            : base("/auth")
        {
            Get["/"] = x =>
            {
                this.RequiresAuthentication();
                /*                    var userName = (string)this.Request.Form.UserName;
                                    var password = (string)this.Request.Form.Password;
                                    userName = "demo";
                                    password = "demo";
                                    var userIdentity = UserDatabase.ValidateUser(userName, password);
                                    if (userIdentity == null)
                                    {
                                        return HttpStatusCode.Unauthorized;
                                    }
                */
                
                var token = tokenizer.Tokenize(this.Context.CurrentUser, Context);
                
                    return new
                        {
                            Token = token,
                        };
                };

            Get["/validation"] = _ =>
                {
                    this.RequiresAuthentication();
                    return "Yay! You are authenticated!";
                };

            Get["/admin"] = _ =>
            {
                this.RequiresAuthentication();
                this.RequiresClaims(new[] { "admin" });
                return "Yay! You are authorized!";
            };
        }
    }
}