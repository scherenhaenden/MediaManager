using BeOneSender.BusinessLogic.Services.Bootstrap;
using BeOneSender.BusinessLogic.Services.SongLibraryManagement;
using BeOneSender.Data.Database.Core.Configuration;
using BeOneSender.Gui.Aspnet.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();


builder.Services.AddDbContext<BeOneSenderDataContext>(
    options => options.UseSqlite("Data Source=BookStoreContext.db;"));

//builder.Services.AddDbContext<BeOneSenderDataContext>(_ => new BeOneSenderDataContext(_options));

builder.Services.AddScoped<ISongsManagerService, SongsManagerService>();
builder.Services.AddScoped<IMusicSearchService, MusicSearchService>();
builder.Services.AddScoped<IMusicEditService, MusicEditService>();

builder.Services.AddScoped<ILoadInformation, LoadInformation>();
builder.Services.AddScoped<IBootsTrapService, BootsTrapService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();