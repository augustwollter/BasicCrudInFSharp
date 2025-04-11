namespace BasicCrudInFSharp.Controllers

open System.Threading.Tasks
open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.OData.Deltas
open Microsoft.AspNetCore.OData.Query
open Microsoft.AspNetCore.OData.Routing.Controllers
open BasicCrudInFSharp.Data
open BasicCrudInFSharp.Models
type PeopleController() =
    inherit ODataController()
    
    [<HttpGet>]
    [<EnableQuery>]
    member this.Get(key : int) =
        match DataSource.People |> Seq.tryFind(fun d -> d.Id.Equals(key)) with
        | None -> this.NotFound() :> ActionResult
        | Some person -> this.Ok(person) :> ActionResult

    [<HttpPost>]
    member this.Post([<FromBody>] person : Person) =
        if not (this.ModelState.IsValid) || person.Id = 0 then
            this.BadRequest() :> ActionResult
        else
            match DataSource.People |> Seq.tryFind(fun d -> d.Id.Equals(person.Id)) with
            | Some _ -> this.Conflict() :> ActionResult
            | None ->
                DataSource.People.Add(person)
                this.Created(person) :> ActionResult
        

