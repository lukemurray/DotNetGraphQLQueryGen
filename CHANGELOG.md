# 0.2.0
- Fixed issues with enum support
- Better support for fields/mutations that take arrays as arguments

# 0.1.0
- Inital version, given a GraphQL schema file we generate
- C# interfaces to write strongly typed queries against
- C# classes for any Input types so we can actually use the classes and initialise with values
- Generate a sample client for users to start implemented their own auth (if required)
- Use scalars from schema to make better choices on generated interfaces
- Support using typed objects instead of anonymous only