open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.AspNetCore.OData
open Microsoft.OData.ModelBuilder
open BasicCrudInFSharp.Models

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)

    let modelBuilder = new ODataConventionModelBuilder()
    modelBuilder.EntitySet<Person>("People") |> ignore

    let model = modelBuilder.GetEdmModel()
    
    builder.Services.AddControllers().AddOData(fun options ->
        options.EnableQueryFeatures().AddRouteComponents(model) |> ignore
    ) |> ignore

    let app = builder.Build()

    app
        .UseODataRouteDebug()
        .UseRouting()
        .UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
        ) |> ignore

    app.Run()

    


    0 // Exit code