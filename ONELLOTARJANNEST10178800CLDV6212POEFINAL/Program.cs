using ONELLOTARJANNEST10178800CLDV6212POEFINAL.Services;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //Services
        builder.Services.AddControllersWithViews();
        builder.Services.AddSingleton<BlobService>();
        builder.Services.AddSingleton<TableService>();
        builder.Services.AddSingleton<QueueService>();
        builder.Services.AddSingleton<FileService>();
        builder.Services.AddSingleton<CustomerTblService>();
        builder.Services.AddSingleton<ProductsTblService>();
        builder.Services.AddSingleton<QueueLogService>();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
