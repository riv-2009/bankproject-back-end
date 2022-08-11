using Domain;
using Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;
using Repository;


var builder = WebApplication.CreateBuilder(args);

// Builder Services
var connectionString = builder.Configuration["DbConnectionString"];

builder.Services.AddControllers();

builder.Services.AddEntityFrameworkMySql()
	.AddDbContext<MyDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 13))));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();

builder.Services.AddCors(options =>
	{
		options.AddPolicy("CorsPolicy",
			builder =>
			{
			    builder
				    .AllowAnyMethod()
				    .AllowAnyHeader()
				    .SetIsOriginAllowed(__ => true)
				    .AllowCredentials();
			});
		});

/*
	These are the services that we are injecting. It sounds scarier than it is
	The ones that I added were the ITransactionService and The ITransactionRepo
	if you not the constructors in each of the different classes they generally will have a dependency inject in at creation time
	This is just one of those things that you have to know is happening.
*/

builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<ITransactionService, TransactionService>();
builder.Services.AddTransient<IAccountService, AccountService>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();
builder.Services.AddTransient<IAccountRepository, AccountRepository>();

builder.Services.AddMvc(options => options.EnableEndpointRouting = false);

// App builder

var app = builder.Build();

app.UseRouting();

app.UseAuthentication();

app.UseCors("CorsPolicy");

app.UseMvc();

app.Run();
