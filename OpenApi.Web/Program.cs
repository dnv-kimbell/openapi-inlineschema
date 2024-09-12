using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

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
                o.AddSchemaTransformer<SchemaTransformer>();
                o.AddDocumentTransformer<DocumentTransformer>();
                o.AddOperationTransformer<OperationTransformer>();
            });
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
            {
                options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
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

public class SchemaTransformer : IOpenApiSchemaTransformer
{
    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
    {
        //Hacks.AddSchema(schema, context.ApplicationServices, context.DocumentName);
        
        return Task.CompletedTask;
    }
}

public class OperationTransformer : IOpenApiOperationTransformer
{
    public Task TransformAsync(OpenApiOperation operation, OpenApiOperationTransformerContext context, CancellationToken cancellationToken)
    {
        //foreach (var response in operation.Responses)
        //{
        //    if (response.Value.Content.TryGetValue("application/json", out var mediaType))
        //    {
        //        if (mediaType.Schema != null)
        //        {
        //            Hacks.AddSchema(mediaType.Schema, context.ApplicationServices, context.DocumentName);
        //        }
        //    }
        //}

        return Task.CompletedTask;
    }
}

public class DocumentTransformer : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}

public static class Hacks
{
    public static void AddSchema(OpenApiSchema schema, IServiceProvider services, string documentName)
    {
        var schemaStoreType = Type.GetType("Microsoft.AspNetCore.OpenApi.OpenApiSchemaStore, Microsoft.AspNetCore.OpenApi");
        var schemaStore = services.GetKeyedServices(schemaStoreType, documentName).FirstOrDefault();

        if (schema.Type == "object")
        {
            schemaStoreType.InvokeMember("PopulateSchemaIntoReferenceCache", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.Public, null, schemaStore, [schema, true]);
        }
    }
}
