using LoremIpsum.UI.Settings;

var builder = WebApplication.CreateBuilder(args);

// Allow overriding settings from environment variables.
builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.Configure<LoremIpsumApiSettings>(builder.Configuration.GetSection("LoremIpsumApi"));

/*builder.Services.AddSingleton(() =>
{
    var section = builder.Configuration.GetSection("LoremIpsumApi");
    return new LoremIpsumApi
    {
        BaseUrl = section.GetValue<string>("BaseUrl")
    };
});*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();

app.Run();
