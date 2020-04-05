
using System;
using DotNetGqlClient;

/// <summary>
/// Generated interfaces for making GraphQL API calls with a typed interface.
///
/// Generated on 05.04.2020 12:27:18 from ..\..\..\..\tests\DotNetGraphQLQueryGen.Tests\schema.graphql with arguments -n Generated -c TestClient -m Date=DateTime
/// </summary>

namespace Generated
{


    public interface RootQuery
    {
        /// <summary>
        /// Pagination [defaults: page  , pagesize  ]
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("actorPager")]
        PersonPagination ActorPager();
        /// <summary>
        /// Pagination [defaults: page  , pagesize  ]
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("actorPager")]
        TReturn ActorPager<TReturn>(int? page, int? pagesize, string search, Func<PersonPagination, TReturn> selection);
        /// <summary>
        /// List of actors
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("actors")]
        Person[] Actors();
        /// <summary>
        /// List of actors
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("actors")]
        TReturn[] Actors<TReturn>(Func<Person, TReturn> selection);
        /// <summary>
        /// List of directors
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("directors")]
        Person[] Directors();
        /// <summary>
        /// List of directors
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("directors")]
        TReturn[] Directors<TReturn>(Func<Person, TReturn> selection);
        /// <summary>
        /// Return a Movie by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("movie")]
        Movie Movie();
        /// <summary>
        /// Return a Movie by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("movie")]
        TReturn Movie<TReturn>(int id, Func<Movie, TReturn> selection);
        /// <summary>
        /// Collection of Movies
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("movies")]
        Movie[] Movies();
        /// <summary>
        /// Collection of Movies
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("movies")]
        TReturn[] Movies<TReturn>(Func<Movie, TReturn> selection);
        /// <summary>
        /// Collection of Peoples
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("people")]
        Person[] People();
        /// <summary>
        /// Collection of Peoples
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("people")]
        TReturn[] People<TReturn>(Func<Person, TReturn> selection);
        /// <summary>
        /// Return Persons by they Ids
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("person")]
        Person[] Person();
        /// <summary>
        /// Return Persons by they Ids
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("person")]
        TReturn[] Person<TReturn>(int?[] ids, Func<Person, TReturn> selection);
        /// <summary>
        /// List of writers
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("writers")]
        Person[] Writers();
        /// <summary>
        /// List of writers
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("writers")]
        TReturn[] Writers<TReturn>(Func<Person, TReturn> selection);
    }
    public interface SubscriptionType
    {
        [GqlFieldName("name")]
        string Name { get; }
    }
    /// <summary>
    /// This is a movie entity
    /// </summary>
    public interface Movie
    {
        [GqlFieldName("id")]
        int Id { get; }
        [GqlFieldName("name")]
        string Name { get; }
        /// <summary>
        /// Enum of Genre
        /// </summary>
        [GqlFieldName("genre")]
        int Genre { get; }
        [GqlFieldName("released")]
        DateTime Released { get; }
        /// <summary>
        /// Actors in the movie
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("actors")]
        Person[] Actors();
        /// <summary>
        /// Actors in the movie
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("actors")]
        TReturn[] Actors<TReturn>(Func<Person, TReturn> selection);
        /// <summary>
        /// Writers in the movie
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("writers")]
        Person[] Writers();
        /// <summary>
        /// Writers in the movie
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("writers")]
        TReturn[] Writers<TReturn>(Func<Person, TReturn> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("director")]
        Person Director();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("director")]
        TReturn Director<TReturn>(Func<Person, TReturn> selection);
        [GqlFieldName("directorId")]
        int DirectorId { get; }
        [GqlFieldName("rating")]
        double? Rating { get; }
    }
    public interface Actor
    {
        [GqlFieldName("personId")]
        int PersonId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("person")]
        Person Person();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("person")]
        TReturn Person<TReturn>(Func<Person, TReturn> selection);
        [GqlFieldName("movieId")]
        int MovieId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("movie")]
        Movie Movie();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("movie")]
        TReturn Movie<TReturn>(Func<Movie, TReturn> selection);
    }
    public interface Person
    {
        [GqlFieldName("id")]
        int Id { get; }
        [GqlFieldName("firstName")]
        string FirstName { get; }
        [GqlFieldName("lastName")]
        string LastName { get; }
        [GqlFieldName("dob")]
        DateTime Dob { get; }
        /// <summary>
        /// Movies they acted in
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("actorIn")]
        Movie[] ActorIn();
        /// <summary>
        /// Movies they acted in
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("actorIn")]
        TReturn[] ActorIn<TReturn>(Func<Movie, TReturn> selection);
        /// <summary>
        /// Movies they wrote
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("writerOf")]
        Movie[] WriterOf();
        /// <summary>
        /// Movies they wrote
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("writerOf")]
        TReturn[] WriterOf<TReturn>(Func<Movie, TReturn> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("directorOf")]
        Movie[] DirectorOf();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("directorOf")]
        TReturn[] DirectorOf<TReturn>(Func<Movie, TReturn> selection);
        [GqlFieldName("died")]
        DateTime Died { get; }
        [GqlFieldName("isDeleted")]
        bool IsDeleted { get; }
        /// <summary>
        /// Show the persons age
        /// </summary>
        [GqlFieldName("age")]
        int Age { get; }
        /// <summary>
        /// Persons name
        /// </summary>
        [GqlFieldName("name")]
        string Name { get; }
    }
    public interface Writer
    {
        [GqlFieldName("personId")]
        int PersonId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("person")]
        Person Person();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("person")]
        TReturn Person<TReturn>(Func<Person, TReturn> selection);
        [GqlFieldName("movieId")]
        int MovieId { get; }
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("movie")]
        Movie Movie();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("movie")]
        TReturn Movie<TReturn>(Func<Movie, TReturn> selection);
    }
    public interface PersonPagination
    {
        /// <summary>
        /// total records to match search
        /// </summary>
        [GqlFieldName("total")]
        int Total { get; }
        /// <summary>
        /// total pages based on page size
        /// </summary>
        [GqlFieldName("pageCount")]
        int PageCount { get; }
        /// <summary>
        /// collection of people
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("people")]
        Person[] People();
        /// <summary>
        /// collection of people
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("people")]
        TReturn[] People<TReturn>(Func<Person, TReturn> selection);
    }
    public interface Mutation
    {
        /// <summary>
        /// Add a new Movie object
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addMovie")]
        TReturn AddMovie<TReturn>(string name, double? rating, Detail[] details, int? genre, DateTime released, Func<Movie, TReturn> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addActor")]
        TReturn AddActor<TReturn>(string firstName, string lastName, int? movieId, Func<Person, TReturn> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addActor2")]
        TReturn AddActor2<TReturn>(string firstName, string lastName, int? movieId, Func<Person, TReturn> selection);
    }
    public class Detail
    {
        [GqlFieldName("description")]
        public string Description { get; set; }
    }

}