using ECommerce.Application.Abstractions.Services;
using MediatR;

namespace ECommerce.Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoints;

public class GetRolesToEndpointsQueryHandler:IRequestHandler<GetRolesToEndpointsQueryRequest,GetRolesToEndpointsQueryResponse>
{
    private readonly IAuthorizationEndpointService _authorizationEndpointService;

    public GetRolesToEndpointsQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
    {
        _authorizationEndpointService = authorizationEndpointService;
    }

    public async Task<GetRolesToEndpointsQueryResponse> Handle(GetRolesToEndpointsQueryRequest request, CancellationToken cancellationToken)
    {
        var datas = await _authorizationEndpointService.GetRolesToEndpointAsync(request.Code,request.Menu);
        return new GetRolesToEndpointsQueryResponse
        {
            Roles = datas
        };
    }
}