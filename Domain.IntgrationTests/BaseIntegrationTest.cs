//using Domain.Persistence;
//using Microsoft.AspNetCore.Hosting.Server;
//using Microsoft.Extensions.DependencyInjection;
//using NSubstitute;

//namespace Domain.IntegrationTests;

//public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
//{
//    private readonly IServiceScope _scope;
//    //protected readonly ISender _sender;
//    protected readonly IDbContext _dbContext;
//    protected readonly IUnitOfWork _unitOfWork;
//    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
//    {
//        _scope = factory.Services.CreateScope();
//        //_sender = _scope.ServiceProvider.GetRequiredService<ISender>();
//        _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//        _unitOfWork = Substitute.For<IUnitOfWork>();

//    }

//}