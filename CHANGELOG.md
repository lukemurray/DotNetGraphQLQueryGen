# 0.5.0
- Use an operation name in the built request to help with logging
- Add overrides for naming your operation in `QueryAysnc`/`MutateAsync`

# 0.4.0
- Fix issue with GQL field names that are a single character
- Generate classes for the schema types so if you select the full type we can create the classes
- Split the Query methods into separate generated class from the schema type classes
- Breaking - when supplying `--scalar_mapping`/`-m` arg, types are now split by `;`

# 0.3.0
- Support generating from the GraphQL endpoint with schema introspection query or from a JSON saved result of the query
- Better support for fields that return a scalar but have arguments - no selection argument generated

# 0.2.0
- Fixed issues with enum support
- Better support for fields/mutations that take arrays as arguments
- #4 Support schema with default values - note it doesn't do anything with them. This seems like the responibility of the server you're calling to implement those.
- Fix #5 support schema with `extend type` types. The fields are added to the exisiting type
- Fix #6 schema keywords can be used a ids (field names, argument names etc)
- Fix #8 support optional commas in schema file
- #7 correctly generate multi line doc strings
- #7 correctly ignore `#` comments

# 0.1.0
- Inital version, given a GraphQL schema file we generate
- C# interfaces to write strongly typed queries against
- C# classes for any Input types so we can actually use the classes and initialise with values
- Generate a sample client for users to start implemented their own auth (if required)
- Use scalars from schema to make better choices on generated interfaces
- Support using typed objects instead of anonymous only