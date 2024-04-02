using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace WebApplication1.JWT
{
    public class JwtEventsHandler : JwtBearerEvents
    {
        public new async Task OnChallenge(JwtBearerChallengeContext context)
        {
            if (context.AuthenticateFailure == null)
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.Redirect($"/Credentials/Login?ReturnUrl={context.Request.Headers["Referer"]}");
                await Task.CompletedTask;
            }
            await base.OnChallenge(context);
        }

        public new async Task OnForbidden(ForbiddenContext context)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            if (!string.IsNullOrEmpty(context.Request.Headers["Referer"]))
            {
                context.Response.Redirect(context.Request.Headers["Referer"]);
            }
            else
            {
                context.Response.Redirect(context.Request.Headers["Origin"]);
            }
            await Task.CompletedTask;
        }
    }
}
