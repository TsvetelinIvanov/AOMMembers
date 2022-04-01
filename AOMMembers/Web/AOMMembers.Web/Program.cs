using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using AOMMembers.Common;
using AOMMembers.Data;
using AOMMembers.Data.Common;
using AOMMembers.Data.Common.Repositories;
using AOMMembers.Data.Models;
using AOMMembers.Data.Repositories;
using AOMMembers.Data.Seeding;
using AOMMembers.Services.Data;
using AOMMembers.Services.Mapping;
using AOMMembers.Services.Messaging;
using AOMMembers.Services.Data.Interfaces;
using AOMMembers.Services.Data.Services;
using AOMMembers.Web.ViewModels;
using AOMMembers.Web.Infrastructure.ModelBinders;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

//builder.Services.AddAutoMapper();//How to add AutoMapper in ASP.NET 6?

builder.Services.Configure<CookiePolicyOptions>(
                options =>
                {
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });//

builder.Services.AddControllersWithViews().AddMvcOptions(options =>
{
    options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
    options.ModelBinderProviders.Insert(1, new DateTimeModelBinderProvider(GlobalConstants.UsedDateFormat));
    options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
});
builder.Services.AddRazorPages();

// Data repositories
builder.Services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IDbQueryRunner, DbQueryRunner>();

// Application services
builder.Services.AddTransient<IEmailSender, NullMessageSender>();
builder.Services.AddTransient<IMembersService, MembersService>();
builder.Services.AddTransient<ICitizensService, CitizensService>();
builder.Services.AddTransient<IEducationsService, EducationsService>();
builder.Services.AddTransient<IQualificationsService, QualificationsService>();
builder.Services.AddTransient<ICareersService, CareersService>();
builder.Services.AddTransient<IWorkPositionsService, WorkPositionsService>();
builder.Services.AddTransient<IRelationshipsService, RelationshipsService>();
builder.Services.AddTransient<IPartyPositionsService, PartyPositionsService>();
builder.Services.AddTransient<IPartyMembershipsService, PartyMembershipsService>();
builder.Services.AddTransient<IMaterialStatesService, MaterialStatesService>();
builder.Services.AddTransient<IAssetsService, AssetsService>();
builder.Services.AddTransient<IPublicImagesService, PublicImagesService>();
builder.Services.AddTransient<IMediaMaterialsService, MediaMaterialsService>();
builder.Services.AddTransient<ILawStatesService, LawStatesService>();
builder.Services.AddTransient<ILawProblemsService, LawProblemsService>();
builder.Services.AddTransient<ISocietyHelpsService, SocietyHelpsService>();
builder.Services.AddTransient<ISocietyActivitiesService, SocietyActivitiesService>();
builder.Services.AddTransient<IWorldviewsService, WorldviewsService>();
builder.Services.AddTransient<IInterestsService, InterestsService>();
builder.Services.AddTransient<ISettingsService, SettingsService>();

WebApplication app = builder.Build();

AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);//

//// Seed data on application
//using (IServiceScope serviceScope = app.Services.CreateScope())
//{
//    ApplicationDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    if (app.Environment.IsDevelopment())
//    {
//        dbContext.Database.Migrate();
//    }

//    new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();//
    app.UseMigrationsEndPoint();    
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();//

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "Area",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();