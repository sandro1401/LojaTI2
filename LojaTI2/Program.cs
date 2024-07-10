using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LojaTI2.Areas.Identity.Data;
using LojaTI2.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LojaTI2ContextConnection") ?? throw new InvalidOperationException("Connection string 'LojaTI2ContextConnection' not found.");

builder.Services.AddDbContext<LojaTI2Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<LojaTI2User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<LojaTI2Context>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<LojaTI2User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await CreateRolesAndAdminUser(userManager, roleManager);
}

app.Run();
static async Task CreateRolesAndAdminUser(UserManager<LojaTI2User> userManager, RoleManager<IdentityRole> roleManager)
{
    // Cria o role Admin se não existir
    var adminRole = "Admin";
    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
    }

    // Cria o usuário Admin se não existir
    var adminEmail = "admin@email.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new LojaTI2User
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            Nome = "Administrador", // Preenchendo a propriedade Nome
            Telefone = "1234567890" // Preenchendo a propriedade Telefone
        };
        await userManager.CreateAsync(adminUser, "Admin123!"); // Define uma senha segura para o usuário Admin
        await userManager.AddToRoleAsync(adminUser, adminRole);
    }
}