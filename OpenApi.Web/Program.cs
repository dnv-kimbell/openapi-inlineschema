namespace OpenApi.Web
{
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
    // used by tests
    public class Program
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddOpenApi("Microsoft", o =>
            {
                o.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerUI(o =>
                {
                    o.RoutePrefix = "docs";
                    o.SwaggerEndpoint("Microsoft.json", "Microsoft version (TODO)");
                });
                app.MapOpenApi("/docs/{documentName}.json");
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
