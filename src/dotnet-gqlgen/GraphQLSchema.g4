grammar GraphQLSchema;

// this is a simple way to allow keywords as idents
idKeyword   : ENUM | INPUT | SCALAR | TYPE | EXTEND | SCHEMA | FALSE | TRUE | ID;

TRUE        : 'true';
FALSE       : 'false';
EXTEND      : 'extend';
SCHEMA      : 'schema';
TYPE        : 'type';
SCALAR      : 'scalar';
INPUT       : 'input';
ENUM        : 'enum';
ID          : [a-z_A-Z] [a-z_A-Z0-9-]*;

DIGIT       : [0-9];
STRING_CHARS: [a-zA-Z0-9 \t`~!@#$%^&*()_+={}|\\:\"'\u005B\u005D;<>?,./-];

int         : '-'? DIGIT+;
decimal     : '-'? DIGIT+'.'DIGIT+;
boolean     : TRUE | FALSE;
string      : '"' ( '"' | ~('\n'|'\r') | STRING_CHARS )*? '"';
constant    : string | int | decimal | boolean | idKeyword; // id should be an enum

// This is our expression language
schema      : (schemaDef | typeDef | scalarDef | inputDef | enumDef | directiveDef | interfaceDef)+;

schemaDef   : comment* SCHEMA ws* objectDef;
typeDef     : comment* (EXTEND ws+)? TYPE ws+ typeName=idKeyword (ws+ 'implements' ws+ interfaceName=idKeyword)? ws* objectDef;
interfaceDef: comment* 'interface' ws+ typeName=idKeyword ws* objectDef;
scalarDef   : comment* SCALAR ws+ typeName=idKeyword ws+;
inputDef    : comment* INPUT ws+ typeName=idKeyword ws* '{' ws* inputFields ws* comment* ws* '}' ws*;
enumDef     : comment* ENUM ws+ typeName=idKeyword ws* '{' (ws* enumItem ws* comment* ws*)+ '}' ws*;
directiveTarget: 'FIELD' | 'FRAGMENT_SPREAD' | 'INLINE_FRAGMENT';
directiveDef: 'directive' ws+ '@' name=idKeyword ('(' args=arguments ')')? ws+ 'on' ws+ (directiveTarget (ws+ '|' ws+ directiveTarget)*) ws*;

inputFields : fieldDef (ws* '=' ws* constant)? (ws* ',')? (ws* fieldDef (ws* '=' ws* constant)? (ws* ',')?)* ws*;
objectDef   : '{' ws* fieldDef (ws* ',')? (ws* fieldDef (ws* ',')?)* ws* comment* ws* '}' ws*;

fieldDef    : comment* name=idKeyword ('(' args=arguments ')')? ws* ':' ws* type=dataType;
enumItem    : comment* name=idKeyword (ws* '.')?;
arguments   : ws* argument (ws* '=' ws* constant)? (ws* ',' ws* argument (ws* '=' ws* constant)?)*;
argument    : idKeyword ws* ':' ws* dataType;

dataType    : type=idKeyword required='!'? | '[' arrayType=idKeyword elementTypeRequired='!'? ']' arrayRequired='!'?;

comment         : ws* (singleLineDoc | multiLineDoc | ignoreComment) ws*;
ignoreComment   : ('#' ~('\n'|'\r')*) ('\n' | '\r' | EOF);
multiLineDoc    : ('"""' ~'"""'* '"""');
singleLineDoc   : ('"' ~('\n'|'\r')* '"');

ws  : ' ' | '\t' | '\n' | '\r';
