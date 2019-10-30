grammar GraphQLSchema;

// This is our expression language
schema      : (schemaDef | typeDef | scalarDef | inputDef | enumDef)+;

schemaDef   : comment* 'schema' ws* objectDef;
typeDef     : comment* 'type ' ws* typeName=NAME ws* objectDef;
scalarDef   : comment* 'scalar' ws* typeName=NAME ws+;
inputDef    : comment* 'input' ws* typeName=NAME ws* objectDef;
enumDef     : comment* 'enum' ws* typeName=NAME ws* '{' ws* enumItem+ comment* ws* '}';

objectDef   : '{' ws* fieldDef (ws* fieldDef)* ws* '}' ws*;

fieldDef    : comment* name=NAME ('(' args=arguments ')')? ws* ':' ws* type=dataType;
enumItem    : comment* NAME;
arguments   : ws* argument (ws* ','* ws* argument)*;
argument    : NAME ws* ':' ws* dataType required='!'?;

dataType    : (NAME '!'? | '[' NAME '!'? ']');
NAME        : [a-z_A-Z] [a-z_A-Z0-9-]*;

comment     : ws* (('"' ~('\n'|'\r')* '"') | ('"""' ~'"""'* '"""') | ('#' ~('\n'|'\r')*)) ws*;

ws  : ' ' | '\t' | '\n' | '\r';
