grammar GraphQLSchema;

DIGIT       : [0-9];
STRING_CHARS: [a-zA-Z0-9 \t`~!@#$%^&*()_+={}|\\:\"'\u005B\u005D;<>?,./-];
NAME        : [a-z_A-Z] [a-z_A-Z0-9-]*;

int         : '-'? DIGIT+;
decimal     : '-'? DIGIT+'.'DIGIT+;
boolean     : 'true' | 'false';
string      : '"' ( '"' | ~('\n'|'\r') | STRING_CHARS )*? '"';
constant    : string | int | decimal | boolean | NAME; // NAME should be an enum

// This is our expression language
schema      : (schemaDef | typeDef | scalarDef | inputDef | enumDef)+;

schemaDef   : comment* 'schema' ws* objectDef;
typeDef     : comment* ('extend' ws*)? 'type' ws+ typeName=NAME ws* objectDef;
scalarDef   : comment* 'scalar' ws+ typeName=NAME ws+;
inputDef    : comment* 'input' ws+ typeName=NAME ws* '{' ws* inputFields ws* '}' ws*;
enumDef     : comment* 'enum' ws+ typeName=NAME ws* '{' (ws* enumItem ws* comment* ws*)+ '}' ws*;

inputFields : fieldDef (ws* '=' ws* constant)? (ws* fieldDef (ws* '=' ws* constant)?)* ws*;
objectDef   : '{' ws* fieldDef (ws* fieldDef)* ws* '}' ws*;

fieldDef    : comment* name=NAME ('(' args=arguments ')')? ws* ':' ws* type=dataType;
enumItem    : comment* name=NAME;
arguments   : ws* argument (ws* '=' ws* constant)? (ws* ','* ws* argument (ws* '=' ws* constant)?)*;
argument    : NAME ws* ':' ws* dataType;

dataType    : (type=NAME required='!'? | '[' arrayType=NAME elementTypeRequired='!'? ']' arrayRequired='!'?);

comment     : ws* (('"' ~('\n'|'\r')* '"') | ('"""' ~'"""'* '"""') | ('#' ~('\n'|'\r')*)) ws*;

ws  : ' ' | '\t' | '\n' | '\r';
