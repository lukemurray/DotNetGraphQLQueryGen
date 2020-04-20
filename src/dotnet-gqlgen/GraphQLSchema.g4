grammar GraphQLSchema;

DIGIT       : [0-9];
STRING_CHARS: [a-zA-Z0-9 \t`~!@#$%^&*()_+={}|\\:\"'\u005B\u005D;<>?,./-];
TRUE        : 'true';
FALSE       : 'false';
SCHEMA      : 'schema';
EXTEND      : 'extend';
TYPE        : 'type';
SCALAR      : 'scalar';
INPUT       : 'input';
ENUM        : 'enum';
ID        : [a-z_A-Z] [a-z_A-Z0-9-]*;

// this is a simple way to allow keywords as idents
id        : ID | ENUM | INPUT | SCALAR | TYPE | EXTEND | SCHEMA | FALSE | TRUE;

int         : '-'? DIGIT+;
decimal     : '-'? DIGIT+'.'DIGIT+;
boolean     : TRUE | FALSE;
string      : '"' ( '"' | ~('\n'|'\r') | STRING_CHARS )*? '"';
constant    : string | int | decimal | boolean | id; // NAME should be an enum

// This is our expression language
schema      : (schemaDef | typeDef | scalarDef | inputDef | enumDef)+;

schemaDef   : comment* SCHEMA ws* objectDef;
typeDef     : comment* (EXTEND ws*)? TYPE ws+ typeName=id ws* objectDef;
scalarDef   : comment* SCALAR ws+ typeName=id ws+;
inputDef    : comment* INPUT ws+ typeName=id ws* '{' ws* inputFields ws* comment* ws* '}' ws*;
enumDef     : comment* ENUM ws+ typeName=id ws* '{' (ws* enumItem ws* comment* ws*)+ '}' ws*;

inputFields : fieldDef (ws* '=' ws* constant)? (ws* ',')? (ws* fieldDef (ws* '=' ws* constant)? (ws* ',')?)* ws*;
objectDef   : '{' ws* fieldDef (ws* ',')? (ws* fieldDef (ws* ',')?)* ws* comment* ws* '}' ws*;

fieldDef    : comment* name=id ('(' args=arguments ')')? ws* ':' ws* type=dataType;
enumItem    : comment* name=id (ws* '.')?;
arguments   : ws* argument (ws* '=' ws* constant)? (ws* ',' ws* argument (ws* '=' ws* constant)?)*;
argument    : id ws* ':' ws* dataType;

dataType    : (type=id required='!'? | '[' arrayType=id elementTypeRequired='!'? ']' arrayRequired='!'?);

comment         : ws* (singleLineDoc | multiLineDoc | ignoreComment) ws*;
ignoreComment   : ('#' ~('\n'|'\r')*) ('\n' | EOF);
multiLineDoc    : ('"""' ~'"""'* '"""');
singleLineDoc   : ('"' ~('\n'|'\r')* '"');

ws  : ' ' | '\t' | '\n' | '\r';
