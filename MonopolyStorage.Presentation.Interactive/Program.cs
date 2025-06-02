using Microsoft.Extensions.DependencyInjection;
using MonopolyStorage.DataAccess.Database.Context;
using MonopolyStorage.DataAccess.Database.Repositories;
using MonopolyStorage.DataAccess.IO.Options;
using MonopolyStorage.DataAccess.IO;
using MonopolyStorage.Domain.Mapper;
using MonopolyStorage.Domain.Repositories.Interfaces;
using MonopolyStorage.Domain.Services.Implementations;
using MonopolyStorage.Domain.Services;
using MonopolyStorage.Infrastructure.Mapper;
using MonopolyStorage.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MonopolyStorage.Presentation.Interactive.CommandsCache;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using MonopolyStorage.Presentation.Interactive.Commands.Boxes;
using MonopolyStorage.Presentation.Interactive.Commands.Pallets;
using MonopolyStorage.Presentation.Interactive.Commands.General;
using MonopolyStorage.Presentation.Interactive.Commands.Base;
using MonopolyStorage.Presentation.Interactive.Extensions;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", true, true);
    })
    .ConfigureServices(ConfigureServices)
    .Build();

host.StartInteractive();

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    var connectionString = context.Configuration.GetConnectionString("Default");
    services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

    var ioOptions = context.Configuration.GetSection("DataIOOptions").Get<DataIOOptions>();
    if (ioOptions != null) services.AddSingleton(ioOptions);

    var storageOptions = context.Configuration.GetSection("CommandStorageOptions")
        .Get<CommandStorageOptions>();
    if (storageOptions != null) services.AddSingleton(storageOptions);

    services.AddScoped<IPalletRepository, PalletRepository>();
    services.AddScoped<IBoxRepository, BoxRepository>();

    services.AddScoped<IDataIOService, DataIOService>();
    services.AddScoped<IDataGenerationService, DataGenerationService>();
    services.AddScoped<IBoxMapper, BoxMapper>();
    services.AddScoped<IPalletMapper, PalletMapper>();
    services.AddScoped<IBoxService, BoxService>();
    services.AddScoped<IPalletService, PalletService>();

    services.AddSingleton<CommandsCacheStorage>();

    services.AddTransient<Command, GenerateBoxCommand>();
    services.AddTransient<Command, GetBoxesDataFromUserCommand>();
    services.AddTransient<Command, GetBoxesDataFromDBCommand>();
    services.AddTransient<Command, GetBoxesDataFromFileCommand>();

    services.AddTransient<Command, GeneratePalletCommand>();
    services.AddTransient<Command,GetHighestExpirationCommand>();
    services.AddTransient<Command,GetPalletDataFromUser>();
    services.AddTransient<Command,GetPalletsDataFromDBCommand>();
    services.AddTransient<Command,GetPalletsDataFromFileCommand>();
    services.AddTransient<Command,GroupPalletsCommand>();

    services.AddTransient<Command, ShowCommand>();

    using var scope = services.BuildServiceProvider().CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}