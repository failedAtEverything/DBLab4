using WebApp.Models;

namespace WebApp
{
    public class Program
    {
        internal static WebApplication _app;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<TvChannelContext>();

            _app = builder.Build();

            if (!_app.Environment.IsDevelopment())
            {
                _app.UseExceptionHandler("/Home/Error");
                _app.UseHsts();
            }

            _app.UseHttpsRedirection();
            _app.UseStaticFiles();

            _app.UseRouting();
            _app.UseResponseCaching();

            _app.UseAuthorization();

            _app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            _app.Run();
        }
    }
}
