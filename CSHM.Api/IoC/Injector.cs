using CSHM.Api.Extensions;
using CSHM.Core.Handlers;
using CSHM.Core.Handlers.Interfaces;
using CSHM.Core.Services;
using CSHM.Core.Services.Interfaces;
using CSHM.Widget.Captcha;
using CSHM.Widget.Dapper;
using CSHM.Widget.Elastic;
using CSHM.Widget.Excel;
using CSHM.Widget.Log;
using CSHM.Widget.Redis;
using CSHM.Widget.Rest;
using Microsoft.Extensions.DependencyInjection.Extensions;
using CSHM.Widget.SMS;
using CSHM.Widget.Email;
using CSHM.Widget.File;
using CSHM.Widget.Config;
using CSHM.Widget.Client;
using CSHM.Widget.PDF;
using CSHM.Widget.Keepa;
using CSHM.Widget.Ticketum;
using StackExchange.Redis;
using Nest;
using CSHM.Core.Repositories;

namespace CSHM.Api.IoC;

public static class Injector
{
    public static void RegisterServices(IServiceCollection service)
    {
        //============================================================ SERVICES
        service.AddScoped<IUserService, UserService>();
        
        service.AddScoped<IControllerActionService, ControllerActionService>();
        
        service.AddScoped<IPageService, PageService>();
        
        service.AddScoped<IPolicyService, PolicyService>();
        
        service.AddScoped<IPolicyParameterService, PolicyParameterService>();
        
        service.AddScoped<IUserPolicyService, UserPolicyService>();

        service.AddScoped<IRoleClaimService, RoleClaimService>();
        
        service.AddScoped<IRoleService, RoleService>();

        service.AddScoped<IUserInRoleService, UserInRoleService>();

        service.AddScoped<IBlogTypeService , BlogTypeService>();
        service.AddScoped<IBlogStatusTypeService , BlogStatusTypeService>();
        service.AddScoped<IBlogService , BlogService>();

        service.AddScoped<IPublishTypeService , PublishTypeService>();
        service.AddScoped<IPublisherService , PublisherService>();
        service.AddScoped<IPublisherBranchService , PublisherBranchService>();

        service.AddScoped<IProductTypeService , ProductTypeService>();
        service.AddScoped<IProductService , ProductService>();
        service.AddScoped<IProductCategoryTypeService , ProductCategoryTypeService>();
        service.AddScoped<IProductLableService , ProductLableService>();
        service.AddScoped<IProductCommentService , ProductCommentService>();
        service.AddScoped<IProductOccupationService , ProductOccupationService>();
        
        service.AddScoped<IMediaTypeService , MediaTypeService>();

        service.AddScoped<IOccupationService , OccupationService>();

        service.AddScoped<IPersonService , PersonService>();
        service.AddScoped<IPersonOccupationService , PersonOccupationService>();

        service.AddScoped<ILableService , LableService>();

        service.AddScoped<ILanguageService , LanguageService>();

        service.AddScoped<ICategoryTypeService , CategoryTypeService>();

        service.AddScoped<ISizeTypeService , SizeTypeService>();

        service.AddScoped<ICoverTypeService , CoverTypeService>();
        //============================================================ Handlers
        service.AddScoped<IUserHandler, UserHandler>();
        
        service.AddScoped<ICalenderHandler, CalenderHandler>();
      
    

        service.AddScoped<IMediaHandler, MediaHandler>();




        //============================================================ THIRD PARTY SERVICES



        //============================================================ WIDGET


        service.AddScoped<IKeepaWidget, KeepaWidget>();

        service.AddScoped<IExcelWidget, ExcelWidget>();

        service.AddScoped<IPDFWidget, PDFWidget>();

        service.AddScoped<ISMSWidget, SMSWidget>();

        service.AddScoped<IEmailWidget, EmailWidget>();

     

        service.AddScoped<IClientWidget, ClientWidget>();



        ////redis
        //service.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
        //    ConnectionMultiplexer.Connect(new ConfigurationOptions
        //    {
        //        EndPoints = { PublicExtension.GetConfigValue<string>("RedisConfiguration:Url") },
        //        AbortOnConnectFail = false,
        //    })
        //);

        var redisConfigSection = ConfigWidget.GetConfigSection("RedisConfiguration");
        var redisConfig = redisConfigSection.Get<RedisConnectionStringViewModel>();

        //redis
        service.AddSingleton<IConnectionMultiplexer>(serviceProvider =>
            ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = { redisConfig.ConnectionStringMaster, redisConfig.ConnectionStringUrl1, redisConfig.ConnectionStringUrl2 },
              //  EndPoints = { redisConfig.ConnectionStringMaster },
                DefaultDatabase = redisConfig.DatabaseNumber,
                User = redisConfig.Username,
                Password = redisConfig.Password,
                AbortOnConnectFail = false,
            })
        ); 

        service.AddSingleton<IRedisWidget, RedisWidget>();


        service.AddScoped<IFileWidget, FileWidget>();

        //============================================================ SINGLETON
        service.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        service.AddSingleton<ILogWidget, LogWidget>();

        service.AddSingleton<IRestWidget, RestWidget>();

        service.AddSingleton<IElasticWidget, ElasticWidget>();


        service.AddSingleton<IDapperWidget, DapperWidget>();

        service.AddSingleton<ICaptchaWidget, CaptchaWidget>();
        service.AddSingleton<IDataRepositoryHandler, DataRepositoryHandler>();

        service.AddSingleton<ITicketumWidget, TicketumWidget>();


        service.AddSingleton<IElasticClient, ElasticClient>(serviceProvider =>
        {
            var url = PublicExtension.GetConfigValue<string>("ELKConfiguration:URL");
            var settings = new ConnectionSettings(new Uri(url))
                .PrettyJson();

            return new ElasticClient(settings);
        });



    }
}