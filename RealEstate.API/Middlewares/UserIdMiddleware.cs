namespace RealEstate.API.Middlewares
{
    public class UserIdMiddleware
    {
        private readonly RequestDelegate _next;

        public UserIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // If user is authenticated and has a claim named "UserId"
            if (context.User.Identity?.IsAuthenticated == true)
            {
                var userIdClaim = context.User.FindFirst("userId"); 
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    context.Items["userId"] = userId;
                }
            }

            await _next(context);
        }
    }
}
