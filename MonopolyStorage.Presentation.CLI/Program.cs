using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MonopolyStorage.DataAccess.Database.Context;
using MonopolyStorage.DataAccess.Database.Repositories;
using MonopolyStorage.DataAccess.IO;
using MonopolyStorage.DataAccess.IO.Options;
using MonopolyStorage.Domain.Mapper;
using MonopolyStorage.Domain.Repositories.Interfaces;
using MonopolyStorage.Domain.Services;
using MonopolyStorage.Domain.Services.Implementations;
using MonopolyStorage.Infrastructure.Mapper;
using MonopolyStorage.Infrastructure.Services;
using MonopolyStorage.Presentation.Commands;
using MonopolyStorage.Presentation.Commands.Boxes;
using MonopolyStorage.Presentation.Commands.Pallets;
using MonopolyStorage.Presentation.CommandsCache;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;

var rootCommand = new RootCommand("My command app");
rootCommand.AddCommand(new GenerateBoxCommand());
rootCommand.AddCommand(new ShowCommand());
rootCommand.AddCommand(new GetBoxesDataFromUserCommand());
rootCommand.AddCommand(new GetBoxesDataFromDBCommand());
rootCommand.AddCommand(new GetBoxesDataFromFileCommand());
rootCommand.AddCommand(new GeneratePalletCommand());
rootCommand.AddCommand(new GetHighestExpirationCommand());
rootCommand.AddCommand(new GetPalletDataFromUser());
rootCommand.AddCommand(new GetPalletsDataFromDBCommand());
rootCommand.AddCommand(new GetPalletsDataFromFileCommand());
rootCommand.AddCommand(new GroupPalletsCommand());

var parser = new CommandLineBuilder(rootCommand)
    .UseHost(_ => Host.CreateDefaultBuilder(args), builder =>
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json", true, true);
        })
        .ConfigureServices(ConfigureServices)
        .UseCommandHandler<GenerateBoxCommand, GenerateBoxCommand.Handler>()
        .UseCommandHandler<ShowCommand, ShowCommand.Handler>()
        .UseCommandHandler<GetBoxesDataFromUserCommand, GetBoxesDataFromUserCommand.Handler>()
        .UseCommandHandler<GetBoxesDataFromDBCommand, GetBoxesDataFromDBCommand.Handler>()
        .UseCommandHandler<GetBoxesDataFromFileCommand, GetBoxesDataFromFileCommand.Handler>()
        .UseCommandHandler<GeneratePalletCommand, GeneratePalletCommand.Handler>()
        .UseCommandHandler<GetHighestExpirationCommand, GetHighestExpirationCommand.Handler>()
        .UseCommandHandler<GetPalletDataFromUser, GetPalletDataFromUser.Handler>()
        .UseCommandHandler<GetPalletsDataFromDBCommand, GetPalletsDataFromDBCommand.Handler>()
        .UseCommandHandler<GetPalletsDataFromFileCommand, GetPalletsDataFromFileCommand.Handler>()
        .UseCommandHandler<GroupPalletsCommand, GroupPalletsCommand.Handler>();
    })
    .UseDefaults()
    .Build();
await parser.InvokeAsync(args);

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    var connectionString = context.Configuration.GetConnectionString("Default");
    services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

    var ioOptions = context.Configuration.GetSection("DataIOOptions").Get<DataIOOptions>();
    if (ioOptions != null) services.AddSingleton(ioOptions);

    services.AddScoped<IPalletRepository, PalletRepository>();
    services.AddScoped<IBoxRepository, BoxRepository>();

    services.AddScoped<IDataIOService, DataIOService>();
    services.AddScoped<IDataGenerationService, DataGenerationService>();
    services.AddScoped<IBoxMapper, BoxMapper>();
    services.AddScoped<IPalletMapper, PalletMapper>();
    services.AddScoped<IBoxService, BoxService>();
    services.AddScoped<IPalletService, PalletService>();

    services.AddSingleton<CommandsCacheStorage>();

    using var scope = services.BuildServiceProvider().CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}