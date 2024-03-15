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
                context.Response.Redirect("/login");
                await Task.CompletedTask;
            }
            await base.OnChallenge(context);
        }
    }
}
