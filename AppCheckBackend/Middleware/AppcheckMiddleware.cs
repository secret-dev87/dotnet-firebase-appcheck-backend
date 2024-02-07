using FirebaseAdmin;
using FirebaseAdmin.AppCheck;
using System.Net;

namespace AppCheckBackend.Middleware
{
    public class AppcheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly FirebaseApp _firebaseApp;

        public AppcheckMiddleware(RequestDelegate next, FirebaseApp firebaseApp)
        {
            _next = next;
            _firebaseApp = firebaseApp;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var appCheckToken = context.Request.Headers.FirstOrDefault(x => x.Key == "X-Firebase-AppCheck").Value.ToString();

            if (String.IsNullOrEmpty(appCheckToken))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("Firebase App Check Token is missed");
                return;
            }

            try
            {
                FirebaseAppCheck appCheck = FirebaseAppCheck.GetAppCheck(_firebaseApp);

                var appCheckClaims = await appCheck.VerifyTokenAsync(appCheckToken);

                if (appCheckClaims == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Firebase AppCheck Token is not valid.");
                    return;
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync($"{ex.Message}");
                return;
            }
        }
    }
}
