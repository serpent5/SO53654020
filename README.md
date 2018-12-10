# SO53654020

Sample repository for configuring ASP.NET Core to support Google's OAuth login process without using ASP.NET Core Identity, in response to a Stack Overflow question [here](https://stackoverflow.com/questions/53654020/how-to-implement-google-login-in-net-core-without-an-entityframework-provider). It consists of a Razor Page (`Index`) that requires authorisation in order to show the signed-in user's claims. The authentication process is handled in `AccountController`, a standard MVC controller implementation.

The sample should run mostly as is, but requires setting the Google `ClientId`/`ClientSecret` pair in `Startup.cs`.
