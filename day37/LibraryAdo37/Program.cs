/*var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
*/

using Microsoft.Data.SqlClient;
//using LibraryAdo37.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add MVC
builder.Services.AddControllersWithViews();

// Simple connection factory
builder.Services.AddSingleton<ISqlConnectionFactory>(sp =>
    new SqlConnectionFactory(builder.Configuration.GetConnectionString("Default")!));

// Repositories
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
// TODO: Implement GenreRepository or add the correct using directive if it exists.
// For now, comment out this line to avoid compilation error.

//builder.Services.AddScoped<IGenericRepository, GenericRepository>();
//builder.Services.AddScoped<IGenreRepository, GenreRepository>();

var app = builder.Build();

// Basic global error handler
app.UseExceptionHandler("/Home/Error");
app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();

public interface ISqlConnectionFactory
{
    SqlConnection Create();
}
public sealed class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _cs;
    public SqlConnectionFactory(string cs) => _cs = cs;
    public SqlConnection Create() => new SqlConnection(_cs);
}
