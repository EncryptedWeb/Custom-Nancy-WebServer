namespace Nancy.Demo.Authentication.Token
{
    using System.Linq;
    using Nancy;
    using Nancy.Authentication.Basic;
    using Nancy.Authentication.Token;
    using Nancy.Bootstrapper;
    using TinyIoc;

    public class TokenAuthBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            pipelines.EnableBasicAuthentication(new BasicAuthenticationConfiguration(
                container.Resolve<IUserValidator>(),
                "MyRealm"));
        }
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            // Example options for specifying additional values for token generation
            container.Register<IUserValidator>(new UserValidator());
            container.Register<ITokenizer>(new Tokenizer(cfg =>
                                                         cfg.AdditionalItems(
                                                             ctx =>
                                                             ctx.Request.Headers["X-Custom-Header"].FirstOrDefault(),
                                                             ctx => ctx.Request.Query.extraValue)));
         
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            TokenAuthentication.Enable(pipelines, new TokenAuthenticationConfiguration(container.Resolve<ITokenizer>()));
        }
    }
}