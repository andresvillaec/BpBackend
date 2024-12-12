using Carter;

namespace Account.API.Endpoints
{
    public class MovementEndpoints : CarterModule
    {
        public MovementEndpoints()
            :base("/api/movements")
        {
            
        }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            throw new NotImplementedException();
        }
    }
}
