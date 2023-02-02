using AutoMapper;
using Grpc.Core;
using PlatformService.Data.Contracts;

namespace PlatformService.DataServices.Synchronous.gRPC;

public class GrpcPlatformService : GrpcPlatform.GrpcPlatformBase
{
    private readonly IPlatformRepository _repository;
    private readonly IMapper _mapper;

    public GrpcPlatformService(IPlatformRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public override Task<PlatformsResponse> GetAllPlatforms(GetAllRequest request, ServerCallContext context)
    {
        var response = new PlatformsResponse();
        var platforms = _repository.GetAll().ToList();
        
        platforms.ForEach(platform => response.Platform.Add(_mapper.Map<GrpcPlatformModel>(platform)));

        return Task.FromResult(response); 
    }
}