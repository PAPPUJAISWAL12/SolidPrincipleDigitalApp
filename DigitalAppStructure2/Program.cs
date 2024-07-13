using DigitalAppStructure2.Security;
using DigitalAppStructure2.SolidPrinciple;

namespace DigitalAppStructure2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            //Dependency Injection
            builder.Services.AddScoped<IStudentService,StudentService>();
            builder.Services.AddScoped<IStudentRepositpry,StudentRepository>();
            builder.Services.AddSingleton<DataSecurityProvider>();

            var app = builder.Build();

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                 name:"default",
                 pattern:"{Controller=Home}/{Action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
