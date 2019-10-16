
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DotNetGqlClient;

/// <summary>
/// Generated interfaces for making GraphQL API calls with a typed interface.
///
/// Generated on 10/10/19 4:49:07 pm from ../tests/DotNetGraphQLQueryGen.Tests/schema.graphql
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
        [GqlFieldName("movies")]
        List<TReturn> Movies<TReturn>(Expression<Func<Movie, TReturn>> selection);
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
        [GqlFieldName("writers")]
        List<TReturn> Writers<TReturn>(Expression<Func<Person, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("director")]
        Person Director();
        /// <summary>
        /// This allows selection of just the fields you require
        /// </summary>
        [GqlFieldName("director")]
        TReturn Director<TReturn>(Expression<Func<Person, TReturn>> selection);
        [GqlFieldName("directorId")]
        int DirectorId { get; }
        [GqlFieldName("rating")]
        double Rating { get; }
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        [GqlFieldName("dob")]
        DateTime Dob { get; }
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
        [GqlFieldName("writerOf")]
        List<TReturn> WriterOf<TReturn>(Expression<Func<Movie, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("directorOf")]
        List<Movie> DirectorOf();
        /// <summary>
        /// This allows selection of just the fields you require
        /// </summary>
        [GqlFieldName("directorOf")]
        List<TReturn> DirectorOf<TReturn>(Expression<Func<Movie, TReturn>> selection);
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
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
        /// This allows selection of just the fields you require
        /// </summary>
        [GqlFieldName("people")]
        List<TReturn> People<TReturn>(Expression<Func<Person, TReturn>> selection);
    }
    public interface Mutation
    {
        /// <summary>
        /// Add a new Movie object
        ///
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("addMovie")]
        Movie AddMovie();
        /// <summary>
        /// Add a new Movie object
        ///
        /// This allows selection of just the fields you require
        /// </summary>
        [GqlFieldName("addMovie")]
        TReturn AddMovie<TReturn>(string name, double? rating, List<Detail> details, int? genre, DateTime? released, Expression<Func<Movie, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("addActor")]
        Person AddActor();
        /// <summary>
        /// This allows selection of just the fields you require
        /// </summary>
        [GqlFieldName("addActor")]
        TReturn AddActor<TReturn>(string firstName, string lastName, int? movieId, Expression<Func<Person, TReturn>> selection);
        /// <summary>
        /// This shortcut will return a selection of all fields
        /// </summary>
        [GqlFieldName("addActor2")]
        Person AddActor2();
        /// <summary>
        /// This allows selection of just the fields you require
        /// </summary>
        [GqlFieldName("addActor2")]
        TReturn AddActor2<TReturn>(string firstName, string lastName, int? movieId, Expression<Func<Person, TReturn>> selection);
    }
    public interface Detail
    {
        [GqlFieldName("description")]
        string Description { get; }
    }

}