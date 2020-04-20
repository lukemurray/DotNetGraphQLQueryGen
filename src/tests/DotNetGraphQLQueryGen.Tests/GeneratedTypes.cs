
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNetGqlClient;

/// <summary>
/// Generated interfaces for making GraphQL API calls with a typed interface.
///
/// Generated on 20/4/20 6:13:33 pm from ../tests/DotNetGraphQLQueryGen.Tests/schema.graphql -n Generated -c TestHttpClient -m Date=DateTime
/// </summary>

namespace Generated
{


    public interface RootQuery
    {
        /// <summary>
        /// Pagination. [defaults: page = 1, pagesize = 10]
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("actorPager")]
        PersonPagination ActorPager();
        /// <summary>
        /// Pagination. [defaults: page = 1, pagesize = 10]
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("actorPager")]
        TReturn ActorPager<TReturn>(int? page, int? pagesize, string search, Expression<Func<PersonPagination, TReturn>> selection);
        /// <summary>
        /// List of actors
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("actors")]
        List<Person> Actors();
        /// <summary>
        /// List of actors
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("actors")]
        List<TReturn> Actors<TReturn>(Expression<Func<Person, TReturn>> selection);
        /// <summary>
        /// List of directors
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("directors")]
        List<Person> Directors();
        /// <summary>
        /// List of directors
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("directors")]
        List<TReturn> Directors<TReturn>(Expression<Func<Person, TReturn>> selection);
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
        TReturn Movie<TReturn>(int id, Expression<Func<Movie, TReturn>> selection);
        /// <summary>
        /// Collection of Movies
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("movies")]
        List<Movie> Movies();
        /// <summary>
        /// Collection of Movies
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("movies")]
        List<TReturn> Movies<TReturn>(Expression<Func<Movie, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("moviesByIds")]
        List<Movie> MoviesByIds();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("moviesByIds")]
        List<TReturn> MoviesByIds<TReturn>(List<int?> ids, Expression<Func<Movie, TReturn>> selection);
        /// <summary>
        /// Collection of Peoples
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("people")]
        List<Person> People();
        /// <summary>
        /// Collection of Peoples
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("people")]
        List<TReturn> People<TReturn>(Expression<Func<Person, TReturn>> selection);
        /// <summary>
        /// Return a Person by its Id
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("person")]
        Person Person();
        /// <summary>
        /// Return a Person by its Id
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("person")]
        TReturn Person<TReturn>(int id, Expression<Func<Person, TReturn>> selection);
        /// <summary>
        /// List of writers
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("writers")]
        List<Person> Writers();
        /// <summary>
        /// List of writers
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("writers")]
        List<TReturn> Writers<TReturn>(Expression<Func<Person, TReturn>> selection);
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
        List<Person> Actors();
        /// <summary>
        /// Actors in the movie
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("actors")]
        List<TReturn> Actors<TReturn>(Expression<Func<Person, TReturn>> selection);
        /// <summary>
        /// Writers in the movie
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("writers")]
        List<Person> Writers();
        /// <summary>
        /// Writers in the movie
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("writers")]
        List<TReturn> Writers<TReturn>(Expression<Func<Person, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("director")]
        Person Director();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("director")]
        TReturn Director<TReturn>(Expression<Func<Person, TReturn>> selection);
        [GqlFieldName("directorId")]
        int DirectorId { get; }
        [GqlFieldName("rating")]
        double? Rating { get; }
        /// <summary>
        /// Just testing using gql schema keywords here
        /// </summary>
        [GqlFieldName("type")]
        int? Type { get; }
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
        TReturn Person<TReturn>(Expression<Func<Person, TReturn>> selection);
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
        TReturn Movie<TReturn>(Expression<Func<Movie, TReturn>> selection);
    }
    public interface Person
    {
        [GqlFieldName("id")]
        int Id { get; }
        [GqlFieldName("firstName")]
        string FirstName { get; }
        [GqlFieldName("lastName")]
        string LastName { get; }
        /// <summary>
        /// Movies they acted in
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("actorIn")]
        List<Movie> ActorIn();
        /// <summary>
        /// Movies they acted in
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("actorIn")]
        List<TReturn> ActorIn<TReturn>(Expression<Func<Movie, TReturn>> selection);
        /// <summary>
        /// Movies they wrote
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("writerOf")]
        List<Movie> WriterOf();
        /// <summary>
        /// Movies they wrote
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("writerOf")]
        List<TReturn> WriterOf<TReturn>(Expression<Func<Movie, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("directorOf")]
        List<Movie> DirectorOf();
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("directorOf")]
        List<TReturn> DirectorOf<TReturn>(Expression<Func<Movie, TReturn>> selection);
        [GqlFieldName("died")]
        DateTime Died { get; }
        [GqlFieldName("isDeleted")]
        bool IsDeleted { get; }
        /// <summary>
        /// Show the person's age
        /// </summary>
        [GqlFieldName("age")]
        int Age { get; }
        /// <summary>
        /// Person's name
        /// </summary>
        [GqlFieldName("name")]
        string Name { get; }
        [GqlFieldName("dob")]
        DateTime Dob { get; }
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
        TReturn Person<TReturn>(Expression<Func<Person, TReturn>> selection);
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
        TReturn Movie<TReturn>(Expression<Func<Movie, TReturn>> selection);
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
        List<Person> People();
        /// <summary>
        /// collection of people
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("people")]
        List<TReturn> People<TReturn>(Expression<Func<Person, TReturn>> selection);
    }
    public interface Mutation
    {
        /// <summary>
        /// Add a new Movie object
        ///
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addMovie")]
        TReturn AddMovie<TReturn>(string name, double? rating, List<Detail> details, int? genre, DateTime released, Expression<Func<Movie, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addActor")]
        TReturn AddActor<TReturn>(string firstName, string lastName, int? movieId, Expression<Func<Person, TReturn>> selection);
        /// <summary>
        /// </summary>
        /// <param name="selection">Projection of fields to select from the object</param>
        [GqlFieldName("addActor2")]
        TReturn AddActor2<TReturn>(string firstName, string lastName, int? movieId, Expression<Func<Person, TReturn>> selection);
    }
    public class Detail
    {
        [GqlFieldName("description")]
        public string Description { get; set; }
        [GqlFieldName("someNumber")]
        public int? SomeNumber { get; set; }
    }

}