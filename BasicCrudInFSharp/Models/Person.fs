namespace BasicCrudInFSharp.Data

open BasicCrudInFSharp.Models

module DataSource =
    let private GetPeople() =
        let people = ResizeArray<Person>()

        people.Add(new Person(Id = 1, Name = "Sue", Age = 19))
        people.Add(new Person(Id = 2, Name = "Joe", Age = 17))
        people.Add(new Person(Id = 3, Name = "Luc", Age = 23))

        people

    let People = GetPeople()