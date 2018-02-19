// created by jay 0.7 (c) 1998 Axel.Schreiner@informatik.uni-osnabrueck.de

#line 2 "cs-parser.jay"
//
// cs-parser.jay: The Parser for the C# compiler
//
// Modified by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)
// based on Mono CS parser - see below
//
// Author: Miguel de Icaza (miguel@gnu.org)
//
// Licensed under the terms of the GNU GPL
//
// (C) 2001 Ximian, Inc (http://www.ximian.com)
//

using System.Text;
using System;
using IvanZ.CSParser;
using System.Globalization;

namespace Mono.CSharp
{
	using System.Collections;
	using System.CodeDom;
	using Mono.Languages;
	using IvanZ.CSParser;
	

	/// <summary>
	///    The C# Parser
	/// </summary>
	public class CSharpParser : GenericParser {
		
#line 37 "-"

  /** simplified error message.
      @see <a href="#yyerror(java.lang.String, java.lang.String[])">yyerror</a>
    */
  public void yyerror (string message) {
    yyerror(message, null);
  }

  /** (syntax) error message.
      Can be overwritten to control message format.
      @param message text to be displayed.
      @param expected vector of acceptable tokens, if available.
    */
  public void yyerror (string message, string[] expected) {
    if ((expected != null) && (expected.Length  > 0)) {
      System.Console.Write ("(" + lexer.Line+","+lexer.Col+") "+ message+", expecting");
      for (int n = 0; n < expected.Length; ++ n)
        System.Console.Write (" "+expected[n]);
        System.Console.WriteLine ();
    } else
      System.Console.WriteLine (message);
  }

  /** debugging support, requires the package jay.yydebug.
      Set to null to suppress debugging messages.
    */
  protected yydebug.yyDebug debug;

  protected static  int yyFinal = 2;
  public static  string [] yyRule = {
    "$accept : compilation_unit",
    "compilation_unit : opt_using_directives opt_attributes opt_namespace_member_declarations opt_EOF",
    "opt_EOF :",
    "opt_EOF : EOF",
    "using_directives : using_directive",
    "using_directives : using_directives using_directive",
    "using_directive : using_alias_directive",
    "using_directive : using_namespace_directive",
    "using_alias_directive : USING IDENTIFIER ASSIGN namespace_or_type_name SEMICOLON",
    "using_namespace_directive : USING namespace_name SEMICOLON",
    "$$1 :",
    "namespace_declaration : NAMESPACE qualified_identifier $$1 namespace_body opt_semicolon",
    "opt_semicolon :",
    "opt_semicolon : SEMICOLON",
    "opt_comma :",
    "opt_comma : COMMA",
    "qualified_identifier : IDENTIFIER",
    "qualified_identifier : qualified_identifier DOT IDENTIFIER",
    "namespace_name : namespace_or_type_name",
    "namespace_body : OPEN_BRACE opt_using_directives opt_namespace_member_declarations CLOSE_BRACE",
    "opt_using_directives :",
    "opt_using_directives : using_directives",
    "opt_namespace_member_declarations :",
    "opt_namespace_member_declarations : namespace_member_declarations",
    "namespace_member_declarations : namespace_member_declaration",
    "namespace_member_declarations : namespace_member_declarations namespace_member_declaration",
    "namespace_member_declaration : type_declaration",
    "namespace_member_declaration : namespace_declaration",
    "type_declaration : class_declaration",
    "type_declaration : struct_declaration",
    "type_declaration : interface_declaration",
    "type_declaration : enum_declaration",
    "type_declaration : delegate_declaration",
    "opt_attributes :",
    "opt_attributes : attribute_section opt_attributes",
    "attribute_section : OPEN_BRACKET attribute_target_specifier attribute_list CLOSE_BRACKET",
    "attribute_section : OPEN_BRACKET attribute_list CLOSE_BRACKET",
    "attribute_target_specifier : attribute_target COLON",
    "attribute_target : IDENTIFIER",
    "attribute_target : EVENT",
    "attribute_target : RETURN",
    "attribute_target : ASSEMBLY",
    "attribute_list : attribute",
    "attribute_list : attribute_list COMMA opt_attribute",
    "opt_attribute :",
    "opt_attribute : attribute",
    "attribute : attribute_name opt_attribute_arguments",
    "attribute_name : type_name",
    "opt_attribute_arguments :",
    "opt_attribute_arguments : OPEN_PARENS attribute_arguments_opt CLOSE_PARENS",
    "attribute_arguments_opt :",
    "attribute_arguments_opt : attribute_arguments",
    "attribute_arguments : positional_argument_list",
    "attribute_arguments : positional_argument_list COMMA named_argument_list",
    "attribute_arguments : named_argument_list",
    "positional_argument_list : expression",
    "positional_argument_list : positional_argument_list COMMA expression",
    "named_argument_list : named_argument",
    "named_argument_list : named_argument_list COMMA named_argument",
    "named_argument : IDENTIFIER ASSIGN expression",
    "class_body : OPEN_BRACE opt_class_member_declarations CLOSE_BRACE",
    "opt_class_member_declarations :",
    "opt_class_member_declarations : class_member_declarations",
    "class_member_declarations : class_member_declaration",
    "class_member_declarations : class_member_declarations class_member_declaration",
    "class_member_declaration : constant_declaration",
    "class_member_declaration : field_declaration",
    "class_member_declaration : method_declaration",
    "class_member_declaration : property_declaration",
    "class_member_declaration : event_declaration",
    "class_member_declaration : indexer_declaration",
    "class_member_declaration : operator_declaration",
    "class_member_declaration : constructor_declaration",
    "class_member_declaration : destructor_declaration",
    "class_member_declaration : type_declaration",
    "$$2 :",
    "$$3 :",
    "struct_declaration : opt_attributes opt_modifiers STRUCT IDENTIFIER opt_struct_interfaces $$2 struct_body $$3 opt_semicolon",
    "opt_struct_interfaces :",
    "opt_struct_interfaces : struct_interface",
    "struct_interface : COLON interface_type_list",
    "struct_body : OPEN_BRACE opt_struct_member_declarations CLOSE_BRACE",
    "opt_struct_member_declarations :",
    "opt_struct_member_declarations : struct_member_declarations",
    "struct_member_declarations : struct_member_declaration",
    "struct_member_declarations : struct_member_declarations struct_member_declaration",
    "struct_member_declaration : constant_declaration",
    "struct_member_declaration : field_declaration",
    "struct_member_declaration : method_declaration",
    "struct_member_declaration : property_declaration",
    "struct_member_declaration : event_declaration",
    "struct_member_declaration : indexer_declaration",
    "struct_member_declaration : operator_declaration",
    "struct_member_declaration : constructor_declaration",
    "struct_member_declaration : type_declaration",
    "struct_member_declaration : destructor_declaration",
    "constant_declaration : opt_attributes opt_modifiers CONST type constant_declarators SEMICOLON",
    "constant_declarators : constant_declarator",
    "constant_declarators : constant_declarators COMMA constant_declarator",
    "constant_declarator : IDENTIFIER ASSIGN constant_expression",
    "field_declaration : opt_attributes opt_modifiers type variable_declarators SEMICOLON",
    "variable_declarators : variable_declarator",
    "variable_declarators : variable_declarators COMMA variable_declarator",
    "variable_declarator : IDENTIFIER ASSIGN variable_initializer",
    "variable_declarator : IDENTIFIER",
    "variable_initializer : expression",
    "variable_initializer : array_initializer",
    "variable_initializer : STACKALLOC type OPEN_BRACKET expression CLOSE_BRACKET",
    "$$4 :",
    "method_declaration : opt_attributes opt_modifiers type member_name OPEN_PARENS opt_formal_parameter_list CLOSE_PARENS $$4 method_body",
    "$$5 :",
    "method_declaration : opt_attributes opt_modifiers VOID member_name OPEN_PARENS opt_formal_parameter_list CLOSE_PARENS $$5 method_body",
    "method_body : block",
    "method_body : SEMICOLON",
    "opt_formal_parameter_list :",
    "opt_formal_parameter_list : formal_parameter_list",
    "formal_parameter_list : fixed_parameters",
    "formal_parameter_list : fixed_parameters COMMA parameter_array",
    "formal_parameter_list : parameter_array",
    "fixed_parameters : fixed_parameter",
    "fixed_parameters : fixed_parameters COMMA fixed_parameter",
    "fixed_parameter : opt_attributes opt_parameter_modifier type IDENTIFIER",
    "opt_parameter_modifier :",
    "opt_parameter_modifier : parameter_modifier",
    "parameter_modifier : REF",
    "parameter_modifier : OUT",
    "parameter_array : opt_attributes PARAMS type IDENTIFIER",
    "member_name : qualified_identifier",
    "$$6 :",
    "$$7 :",
    "property_declaration : opt_attributes opt_modifiers type member_name $$6 OPEN_BRACE accessor_declarations $$7 CLOSE_BRACE",
    "accessor_declarations : get_accessor_declaration opt_set_accessor_declaration",
    "accessor_declarations : set_accessor_declaration opt_get_accessor_declaration",
    "opt_get_accessor_declaration :",
    "opt_get_accessor_declaration : get_accessor_declaration",
    "opt_set_accessor_declaration :",
    "opt_set_accessor_declaration : set_accessor_declaration",
    "$$8 :",
    "get_accessor_declaration : opt_attributes GET $$8 accessor_body",
    "$$9 :",
    "set_accessor_declaration : opt_attributes SET $$9 accessor_body",
    "accessor_body : block",
    "accessor_body : SEMICOLON",
    "$$10 :",
    "$$11 :",
    "interface_declaration : opt_attributes opt_modifiers INTERFACE IDENTIFIER opt_interface_base $$10 interface_body $$11 opt_semicolon",
    "opt_interface_base :",
    "opt_interface_base : interface_base",
    "interface_base : COLON interface_type_list",
    "interface_type_list : interface_type",
    "interface_type_list : interface_type_list COMMA interface_type",
    "interface_body : OPEN_BRACE opt_interface_member_declarations CLOSE_BRACE",
    "opt_interface_member_declarations :",
    "opt_interface_member_declarations : interface_member_declarations",
    "interface_member_declarations : interface_member_declaration",
    "interface_member_declarations : interface_member_declarations interface_member_declaration",
    "interface_member_declaration : interface_method_declaration",
    "interface_member_declaration : interface_property_declaration",
    "interface_member_declaration : interface_event_declaration",
    "interface_member_declaration : interface_indexer_declaration",
    "opt_new :",
    "opt_new : NEW",
    "interface_method_declaration : opt_attributes opt_new type IDENTIFIER OPEN_PARENS opt_formal_parameter_list CLOSE_PARENS SEMICOLON",
    "interface_method_declaration : opt_attributes opt_new VOID IDENTIFIER OPEN_PARENS opt_formal_parameter_list CLOSE_PARENS SEMICOLON",
    "$$12 :",
    "$$13 :",
    "interface_property_declaration : opt_attributes opt_new type IDENTIFIER OPEN_BRACE $$12 interface_accesors $$13 CLOSE_BRACE",
    "interface_accesors : opt_attributes GET SEMICOLON",
    "interface_accesors : opt_attributes SET SEMICOLON",
    "interface_accesors : opt_attributes GET SEMICOLON opt_attributes SET SEMICOLON",
    "interface_accesors : opt_attributes SET SEMICOLON opt_attributes GET SEMICOLON",
    "interface_event_declaration : opt_attributes opt_new EVENT type IDENTIFIER SEMICOLON",
    "$$14 :",
    "$$15 :",
    "interface_indexer_declaration : opt_attributes opt_new type THIS OPEN_BRACKET formal_parameter_list CLOSE_BRACKET OPEN_BRACE $$14 interface_accesors $$15 CLOSE_BRACE",
    "operator_declaration : opt_attributes opt_modifiers operator_declarator block",
    "operator_declarator : type OPERATOR overloadable_operator OPEN_PARENS type IDENTIFIER CLOSE_PARENS",
    "operator_declarator : type OPERATOR overloadable_operator OPEN_PARENS type IDENTIFIER COMMA type IDENTIFIER CLOSE_PARENS",
    "operator_declarator : conversion_operator_declarator",
    "overloadable_operator : BANG",
    "overloadable_operator : TILDE",
    "overloadable_operator : OP_INC",
    "overloadable_operator : OP_DEC",
    "overloadable_operator : TRUE",
    "overloadable_operator : FALSE",
    "overloadable_operator : PLUS",
    "overloadable_operator : MINUS",
    "overloadable_operator : STAR",
    "overloadable_operator : DIV",
    "overloadable_operator : PERCENT",
    "overloadable_operator : BITWISE_AND",
    "overloadable_operator : BITWISE_OR",
    "overloadable_operator : CARRET",
    "overloadable_operator : OP_SHIFT_LEFT",
    "overloadable_operator : OP_SHIFT_RIGHT",
    "overloadable_operator : OP_EQ",
    "overloadable_operator : OP_NE",
    "overloadable_operator : OP_GT",
    "overloadable_operator : OP_LT",
    "overloadable_operator : OP_GE",
    "overloadable_operator : OP_LE",
    "conversion_operator_declarator : IMPLICIT OPERATOR type OPEN_PARENS type IDENTIFIER CLOSE_PARENS",
    "conversion_operator_declarator : EXPLICIT OPERATOR type OPEN_PARENS type IDENTIFIER CLOSE_PARENS",
    "conversion_operator_declarator : IMPLICIT error",
    "conversion_operator_declarator : EXPLICIT error",
    "$$16 :",
    "constructor_declaration : opt_attributes opt_modifiers constructor_declarator $$16 block",
    "constructor_declarator : IDENTIFIER OPEN_PARENS opt_formal_parameter_list CLOSE_PARENS opt_constructor_initializer",
    "opt_constructor_initializer :",
    "opt_constructor_initializer : constructor_initializer",
    "constructor_initializer : COLON BASE OPEN_PARENS opt_argument_list CLOSE_PARENS",
    "constructor_initializer : COLON THIS OPEN_PARENS opt_argument_list CLOSE_PARENS",
    "destructor_declaration : opt_attributes TILDE IDENTIFIER OPEN_PARENS CLOSE_PARENS block",
    "event_declaration : opt_attributes opt_modifiers EVENT type variable_declarators SEMICOLON",
    "$$17 :",
    "$$18 :",
    "event_declaration : opt_attributes opt_modifiers EVENT type member_name OPEN_BRACE $$17 event_accessor_declarations $$18 CLOSE_BRACE",
    "event_accessor_declarations : add_accessor_declaration remove_accessor_declaration",
    "event_accessor_declarations : remove_accessor_declaration add_accessor_declaration",
    "$$19 :",
    "add_accessor_declaration : opt_attributes ADD $$19 block",
    "$$20 :",
    "remove_accessor_declaration : opt_attributes REMOVE $$20 block",
    "$$21 :",
    "$$22 :",
    "indexer_declaration : opt_attributes opt_modifiers indexer_declarator OPEN_BRACE $$21 accessor_declarations $$22 CLOSE_BRACE",
    "indexer_declarator : type THIS OPEN_BRACKET opt_formal_parameter_list CLOSE_BRACKET",
    "indexer_declarator : type qualified_identifier DOT THIS OPEN_BRACKET opt_formal_parameter_list CLOSE_BRACKET",
    "$$23 :",
    "$$24 :",
    "enum_declaration : opt_attributes opt_modifiers ENUM IDENTIFIER opt_enum_base $$23 enum_body $$24 opt_semicolon",
    "opt_enum_base :",
    "opt_enum_base : COLON type",
    "enum_body : OPEN_BRACE opt_enum_member_declarations CLOSE_BRACE",
    "opt_enum_member_declarations :",
    "opt_enum_member_declarations : enum_member_declarations opt_comma",
    "enum_member_declarations : enum_member_declaration",
    "enum_member_declarations : enum_member_declarations COMMA enum_member_declaration",
    "enum_member_declaration : opt_attributes IDENTIFIER",
    "enum_member_declaration : opt_attributes IDENTIFIER ASSIGN expression",
    "delegate_declaration : opt_attributes opt_modifiers DELEGATE type IDENTIFIER OPEN_PARENS opt_formal_parameter_list CLOSE_PARENS SEMICOLON",
    "delegate_declaration : opt_attributes opt_modifiers DELEGATE VOID IDENTIFIER OPEN_PARENS opt_formal_parameter_list CLOSE_PARENS SEMICOLON",
    "type_name : namespace_or_type_name",
    "namespace_or_type_name : qualified_identifier",
    "type : type_name",
    "type : builtin_types",
    "type : array_type",
    "type : pointer_type",
    "pointer_type : type STAR",
    "pointer_type : VOID STAR",
    "non_expression_type : builtin_types",
    "non_expression_type : non_expression_type rank_specifier",
    "non_expression_type : non_expression_type STAR",
    "non_expression_type : expression rank_specifiers",
    "non_expression_type : expression STAR",
    "type_list : type",
    "type_list : type_list COMMA type",
    "builtin_types : OBJECT",
    "builtin_types : STRING",
    "builtin_types : BOOL",
    "builtin_types : DECIMAL",
    "builtin_types : FLOAT",
    "builtin_types : DOUBLE",
    "builtin_types : integral_type",
    "integral_type : SBYTE",
    "integral_type : BYTE",
    "integral_type : SHORT",
    "integral_type : USHORT",
    "integral_type : INT",
    "integral_type : UINT",
    "integral_type : LONG",
    "integral_type : ULONG",
    "integral_type : CHAR",
    "interface_type : type_name",
    "array_type : type rank_specifiers",
    "primary_expression : literal",
    "primary_expression : qualified_identifier",
    "primary_expression : parenthesized_expression",
    "primary_expression : member_access",
    "primary_expression : invocation_expression",
    "primary_expression : element_access",
    "primary_expression : this_access",
    "primary_expression : base_access",
    "primary_expression : post_increment_expression",
    "primary_expression : post_decrement_expression",
    "primary_expression : new_expression",
    "primary_expression : typeof_expression",
    "primary_expression : sizeof_expression",
    "primary_expression : checked_expression",
    "primary_expression : unchecked_expression",
    "primary_expression : pointer_member_access",
    "literal : boolean_literal",
    "literal : integer_literal",
    "literal : real_literal",
    "literal : LITERAL_CHARACTER",
    "literal : LITERAL_STRING",
    "literal : NULL",
    "real_literal : LITERAL_FLOAT",
    "real_literal : LITERAL_DOUBLE",
    "real_literal : LITERAL_DECIMAL",
    "integer_literal : LITERAL_INTEGER",
    "boolean_literal : TRUE",
    "boolean_literal : FALSE",
    "parenthesized_expression : OPEN_PARENS expression CLOSE_PARENS",
    "member_access : primary_expression DOT IDENTIFIER",
    "member_access : predefined_type DOT IDENTIFIER",
    "predefined_type : builtin_types",
    "invocation_expression : primary_expression OPEN_PARENS opt_argument_list CLOSE_PARENS",
    "opt_argument_list :",
    "opt_argument_list : argument_list",
    "argument_list : argument",
    "argument_list : argument_list COMMA argument",
    "argument : expression",
    "argument : REF variable_reference",
    "argument : OUT variable_reference",
    "variable_reference : expression",
    "element_access : primary_expression OPEN_BRACKET expression_list CLOSE_BRACKET",
    "element_access : primary_expression rank_specifiers",
    "expression_list : expression",
    "expression_list : expression_list COMMA expression",
    "this_access : THIS",
    "base_access : BASE DOT IDENTIFIER",
    "base_access : BASE OPEN_BRACKET expression_list CLOSE_BRACKET",
    "post_increment_expression : primary_expression OP_INC",
    "post_decrement_expression : primary_expression OP_DEC",
    "new_expression : object_or_delegate_creation_expression",
    "new_expression : array_creation_expression",
    "object_or_delegate_creation_expression : NEW type OPEN_PARENS opt_argument_list CLOSE_PARENS",
    "array_creation_expression : NEW type OPEN_BRACKET expression_list CLOSE_BRACKET opt_rank_specifier opt_array_initializer",
    "array_creation_expression : NEW type rank_specifiers array_initializer",
    "array_creation_expression : NEW type error",
    "opt_rank_specifier :",
    "opt_rank_specifier : rank_specifiers",
    "rank_specifiers : rank_specifier",
    "rank_specifiers : rank_specifiers rank_specifier",
    "rank_specifier : OPEN_BRACKET opt_dim_separators CLOSE_BRACKET",
    "opt_dim_separators :",
    "opt_dim_separators : dim_separators",
    "dim_separators : COMMA",
    "dim_separators : dim_separators COMMA",
    "opt_array_initializer :",
    "opt_array_initializer : array_initializer",
    "array_initializer : OPEN_BRACE CLOSE_BRACE",
    "array_initializer : OPEN_BRACE variable_initializer_list opt_comma CLOSE_BRACE",
    "variable_initializer_list : variable_initializer",
    "variable_initializer_list : variable_initializer_list COMMA variable_initializer",
    "typeof_expression : TYPEOF OPEN_PARENS type CLOSE_PARENS",
    "typeof_expression : TYPEOF OPEN_PARENS VOID CLOSE_PARENS",
    "sizeof_expression : SIZEOF OPEN_PARENS type CLOSE_PARENS",
    "checked_expression : CHECKED OPEN_PARENS expression CLOSE_PARENS",
    "unchecked_expression : UNCHECKED OPEN_PARENS expression CLOSE_PARENS",
    "pointer_member_access : primary_expression OP_PTR IDENTIFIER",
    "unary_expression : primary_expression",
    "unary_expression : BANG prefixed_unary_expression",
    "unary_expression : TILDE prefixed_unary_expression",
    "unary_expression : OPEN_PARENS expression CLOSE_PARENS unary_expression",
    "unary_expression : OPEN_PARENS non_expression_type CLOSE_PARENS prefixed_unary_expression",
    "prefixed_unary_expression : unary_expression",
    "prefixed_unary_expression : PLUS prefixed_unary_expression",
    "prefixed_unary_expression : MINUS prefixed_unary_expression",
    "prefixed_unary_expression : OP_INC prefixed_unary_expression",
    "prefixed_unary_expression : OP_DEC prefixed_unary_expression",
    "prefixed_unary_expression : STAR prefixed_unary_expression",
    "prefixed_unary_expression : BITWISE_AND prefixed_unary_expression",
    "pre_increment_expression : OP_INC prefixed_unary_expression",
    "pre_decrement_expression : OP_DEC prefixed_unary_expression",
    "multiplicative_expression : prefixed_unary_expression",
    "multiplicative_expression : multiplicative_expression STAR prefixed_unary_expression",
    "multiplicative_expression : multiplicative_expression DIV prefixed_unary_expression",
    "multiplicative_expression : multiplicative_expression PERCENT prefixed_unary_expression",
    "additive_expression : multiplicative_expression",
    "additive_expression : additive_expression PLUS multiplicative_expression",
    "additive_expression : additive_expression MINUS multiplicative_expression",
    "shift_expression : additive_expression",
    "shift_expression : shift_expression OP_SHIFT_LEFT additive_expression",
    "shift_expression : shift_expression OP_SHIFT_RIGHT additive_expression",
    "relational_expression : shift_expression",
    "relational_expression : relational_expression OP_LT shift_expression",
    "relational_expression : relational_expression OP_GT shift_expression",
    "relational_expression : relational_expression OP_LE shift_expression",
    "relational_expression : relational_expression OP_GE shift_expression",
    "relational_expression : relational_expression IS type",
    "relational_expression : relational_expression AS type",
    "equality_expression : relational_expression",
    "equality_expression : equality_expression OP_EQ relational_expression",
    "equality_expression : equality_expression OP_NE relational_expression",
    "and_expression : equality_expression",
    "and_expression : and_expression BITWISE_AND equality_expression",
    "exclusive_or_expression : and_expression",
    "exclusive_or_expression : exclusive_or_expression CARRET and_expression",
    "inclusive_or_expression : exclusive_or_expression",
    "inclusive_or_expression : inclusive_or_expression BITWISE_OR exclusive_or_expression",
    "conditional_and_expression : inclusive_or_expression",
    "conditional_and_expression : conditional_and_expression OP_AND inclusive_or_expression",
    "conditional_or_expression : conditional_and_expression",
    "conditional_or_expression : conditional_or_expression OP_OR conditional_and_expression",
    "conditional_expression : conditional_or_expression",
    "conditional_expression : conditional_or_expression INTERR expression COLON expression",
    "assignment_expression : prefixed_unary_expression ASSIGN expression",
    "assignment_expression : prefixed_unary_expression OP_MULT_ASSIGN expression",
    "assignment_expression : prefixed_unary_expression OP_DIV_ASSIGN expression",
    "assignment_expression : prefixed_unary_expression OP_MOD_ASSIGN expression",
    "assignment_expression : prefixed_unary_expression OP_ADD_ASSIGN expression",
    "assignment_expression : prefixed_unary_expression OP_SUB_ASSIGN expression",
    "assignment_expression : prefixed_unary_expression OP_SHIFT_LEFT_ASSIGN expression",
    "assignment_expression : prefixed_unary_expression OP_SHIFT_RIGHT_ASSIGN expression",
    "assignment_expression : prefixed_unary_expression OP_AND_ASSIGN expression",
    "assignment_expression : prefixed_unary_expression OP_OR_ASSIGN expression",
    "assignment_expression : prefixed_unary_expression OP_XOR_ASSIGN expression",
    "expression : conditional_expression",
    "expression : assignment_expression",
    "constant_expression : expression",
    "boolean_expression : expression",
    "$$25 :",
    "$$26 :",
    "class_declaration : opt_attributes opt_modifiers CLASS IDENTIFIER opt_class_base $$25 class_body $$26 opt_semicolon",
    "opt_modifiers :",
    "opt_modifiers : modifiers",
    "modifiers : modifier",
    "modifiers : modifiers modifier",
    "modifier : NEW",
    "modifier : PUBLIC",
    "modifier : PROTECTED",
    "modifier : INTERNAL",
    "modifier : PRIVATE",
    "modifier : ABSTRACT",
    "modifier : SEALED",
    "modifier : STATIC",
    "modifier : READONLY",
    "modifier : VIRTUAL",
    "modifier : OVERRIDE",
    "modifier : EXTERN",
    "modifier : VOLATILE",
    "modifier : UNSAFE",
    "opt_class_base :",
    "opt_class_base : class_base",
    "class_base : COLON type_list",
    "$$27 :",
    "block : OPEN_BRACE $$27 opt_statement_list CLOSE_BRACE",
    "opt_statement_list :",
    "opt_statement_list : statement_list",
    "statement_list : statement",
    "statement_list : statement_list statement",
    "statement : declaration_statement",
    "statement : embedded_statement",
    "statement : labeled_statement",
    "embedded_statement : block",
    "embedded_statement : empty_statement",
    "embedded_statement : expression_statement",
    "embedded_statement : selection_statement",
    "embedded_statement : iteration_statement",
    "embedded_statement : jump_statement",
    "embedded_statement : try_statement",
    "embedded_statement : checked_statement",
    "embedded_statement : unchecked_statement",
    "embedded_statement : lock_statement",
    "embedded_statement : using_statement",
    "embedded_statement : unsafe_statement",
    "embedded_statement : fixed_statement",
    "empty_statement : SEMICOLON",
    "$$28 :",
    "labeled_statement : IDENTIFIER COLON $$28 statement",
    "declaration_statement : local_variable_declaration SEMICOLON",
    "declaration_statement : local_constant_declaration SEMICOLON",
    "local_variable_type : primary_expression opt_rank_specifier",
    "local_variable_type : builtin_types opt_rank_specifier",
    "local_variable_pointer_type : primary_expression STAR",
    "local_variable_pointer_type : builtin_types STAR",
    "local_variable_pointer_type : VOID STAR",
    "local_variable_pointer_type : local_variable_pointer_type STAR",
    "local_variable_declaration : local_variable_type variable_declarators",
    "local_variable_declaration : local_variable_pointer_type opt_rank_specifier variable_declarators",
    "local_constant_declaration : CONST local_variable_type constant_declarator",
    "expression_statement : statement_expression SEMICOLON",
    "statement_expression : invocation_expression",
    "statement_expression : object_creation_expression",
    "statement_expression : assignment_expression",
    "statement_expression : post_increment_expression",
    "statement_expression : post_decrement_expression",
    "statement_expression : pre_increment_expression",
    "statement_expression : pre_decrement_expression",
    "statement_expression : error",
    "object_creation_expression : object_or_delegate_creation_expression",
    "selection_statement : if_statement",
    "selection_statement : switch_statement",
    "if_statement : IF OPEN_PARENS boolean_expression CLOSE_PARENS embedded_statement",
    "if_statement : IF OPEN_PARENS boolean_expression CLOSE_PARENS embedded_statement ELSE embedded_statement",
    "$$29 :",
    "switch_statement : SWITCH OPEN_PARENS $$29 expression CLOSE_PARENS switch_block",
    "switch_block : OPEN_BRACE opt_switch_sections CLOSE_BRACE",
    "opt_switch_sections :",
    "opt_switch_sections : switch_sections",
    "switch_sections : switch_section",
    "switch_sections : switch_sections switch_section",
    "$$30 :",
    "switch_section : switch_labels $$30 statement_list",
    "switch_labels : switch_label",
    "switch_labels : switch_labels switch_label",
    "switch_label : CASE constant_expression COLON",
    "switch_label : DEFAULT COLON",
    "switch_label : error",
    "iteration_statement : while_statement",
    "iteration_statement : do_statement",
    "iteration_statement : for_statement",
    "iteration_statement : foreach_statement",
    "while_statement : WHILE OPEN_PARENS boolean_expression CLOSE_PARENS embedded_statement",
    "do_statement : DO embedded_statement WHILE OPEN_PARENS boolean_expression CLOSE_PARENS SEMICOLON",
    "$$31 :",
    "for_statement : FOR OPEN_PARENS opt_for_initializer SEMICOLON $$31 opt_for_condition SEMICOLON opt_for_iterator CLOSE_PARENS embedded_statement",
    "opt_for_initializer :",
    "opt_for_initializer : for_initializer",
    "for_initializer : local_variable_declaration",
    "for_initializer : statement_expression_list",
    "opt_for_condition :",
    "opt_for_condition : boolean_expression",
    "opt_for_iterator :",
    "opt_for_iterator : for_iterator",
    "for_iterator : statement_expression_list",
    "statement_expression_list : statement_expression",
    "statement_expression_list : statement_expression_list COMMA statement_expression",
    "$$32 :",
    "$$33 :",
    "foreach_statement : FOREACH OPEN_PARENS type IDENTIFIER IN $$32 expression CLOSE_PARENS $$33 embedded_statement",
    "jump_statement : break_statement",
    "jump_statement : continue_statement",
    "jump_statement : goto_statement",
    "jump_statement : return_statement",
    "jump_statement : throw_statement",
    "break_statement : BREAK SEMICOLON",
    "continue_statement : CONTINUE SEMICOLON",
    "goto_statement : GOTO IDENTIFIER SEMICOLON",
    "goto_statement : GOTO CASE constant_expression SEMICOLON",
    "goto_statement : GOTO DEFAULT SEMICOLON",
    "return_statement : RETURN opt_expression SEMICOLON",
    "throw_statement : THROW opt_expression SEMICOLON",
    "opt_expression :",
    "opt_expression : expression",
    "try_statement : TRY block catch_clauses",
    "try_statement : TRY block opt_catch_clauses FINALLY block",
    "try_statement : TRY block error",
    "opt_catch_clauses :",
    "opt_catch_clauses : catch_clauses",
    "catch_clauses : catch_clause",
    "catch_clauses : catch_clauses catch_clause",
    "opt_identifier :",
    "opt_identifier : IDENTIFIER",
    "$$34 :",
    "catch_clause : CATCH opt_catch_args $$34 block",
    "opt_catch_args :",
    "opt_catch_args : catch_args",
    "catch_args : OPEN_PARENS type opt_identifier CLOSE_PARENS",
    "checked_statement : CHECKED block",
    "unchecked_statement : UNCHECKED block",
    "$$35 :",
    "unsafe_statement : UNSAFE $$35 block",
    "$$36 :",
    "fixed_statement : FIXED OPEN_PARENS pointer_type fixed_pointer_declarators CLOSE_PARENS $$36 embedded_statement",
    "fixed_pointer_declarators : fixed_pointer_declarator",
    "fixed_pointer_declarators : fixed_pointer_declarators COMMA fixed_pointer_declarator",
    "fixed_pointer_declarator : IDENTIFIER ASSIGN expression",
    "$$37 :",
    "lock_statement : LOCK OPEN_PARENS expression CLOSE_PARENS $$37 embedded_statement",
    "$$38 :",
    "using_statement : USING OPEN_PARENS resource_acquisition CLOSE_PARENS $$38 embedded_statement",
    "resource_acquisition : local_variable_declaration",
    "resource_acquisition : expression",
  };
  protected static  string [] yyName = {    
    "end-of-file",null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,"'!'",null,null,null,"'%'","'&'",
    null,"'('","')'","'*'","'+'","','","'-'","'.'","'/'",null,null,null,
    null,null,null,null,null,null,null,"':'","';'","'<'","'='","'>'",
    "'?'",null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,"'['",null,"']'","'^'",null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,"'{'","'|'","'}'","'~'",null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,null,null,null,null,null,null,null,null,null,null,null,null,null,
    null,"EOF","NONE","ERROR","ABSTRACT","AS","ADD","ASSEMBLY","BASE",
    "BOOL","BREAK","BYTE","CASE","CATCH","CHAR","CHECKED","CLASS","CONST",
    "CONTINUE","DECIMAL","DEFAULT","DELEGATE","DO","DOUBLE","ELSE","ENUM",
    "EVENT","EXPLICIT","EXTERN","FALSE","FINALLY","FIXED","FLOAT","FOR",
    "FOREACH","GOTO","IF","IMPLICIT","IN","INT","INTERFACE","INTERNAL",
    "IS","LOCK","LONG","NAMESPACE","NEW","NULL","OBJECT","OPERATOR","OUT",
    "OVERRIDE","PARAMS","PRIVATE","PROTECTED","PUBLIC","READONLY","REF",
    "RETURN","REMOVE","SBYTE","SEALED","SHORT","SIZEOF","STACKALLOC",
    "STATIC","STRING","STRUCT","SWITCH","THIS","THROW","TRUE","TRY",
    "TYPEOF","UINT","ULONG","UNCHECKED","UNSAFE","USHORT","USING",
    "VIRTUAL","VOID","VOLATILE","WHILE","GET","\"get\"","SET","\"set\"",
    "OPEN_BRACE","CLOSE_BRACE","OPEN_BRACKET","CLOSE_BRACKET",
    "OPEN_PARENS","CLOSE_PARENS","DOT","COMMA","COLON","SEMICOLON",
    "TILDE","PLUS","MINUS","BANG","ASSIGN","OP_LT","OP_GT","BITWISE_AND",
    "BITWISE_OR","STAR","PERCENT","DIV","CARRET","INTERR","OP_INC",
    "\"++\"","OP_DEC","\"--\"","OP_SHIFT_LEFT","\"<<\"","OP_SHIFT_RIGHT",
    "\">>\"","OP_LE","\"<=\"","OP_GE","\">=\"","OP_EQ","\"==\"","OP_NE",
    "\"!=\"","OP_AND","\"&&\"","OP_OR","\"||\"","OP_MULT_ASSIGN","\"*=\"",
    "OP_DIV_ASSIGN","\"/=\"","OP_MOD_ASSIGN","\"%=\"","OP_ADD_ASSIGN",
    "\"+=\"","OP_SUB_ASSIGN","\"-=\"","OP_SHIFT_LEFT_ASSIGN","\"<<=\"",
    "OP_SHIFT_RIGHT_ASSIGN","\">>=\"","OP_AND_ASSIGN","\"&=\"",
    "OP_XOR_ASSIGN","\"^=\"","OP_OR_ASSIGN","\"|=\"","OP_PTR","\"->\"",
    "LITERAL_INTEGER","\"int literal\"","LITERAL_FLOAT",
    "\"float literal\"","LITERAL_DOUBLE","\"double literal\"",
    "LITERAL_DECIMAL","\"decimal literal\"","LITERAL_CHARACTER",
    "\"character literal\"","LITERAL_STRING","\"string literal\"",
    "IDENTIFIER","LOWPREC","UMINUS","HIGHPREC",
  };

  /** index-checked interface to yyName[].
      @param token single character or %token value.
      @return token name or [illegal] or [unknown].
    */
  public static string yyname (int token) {
    if ((token < 0) || (token > yyName.Length)) return "[illegal]";
    string name;
    if ((name = yyName[token]) != null) return name;
    return "[unknown]";
  }

  /** computes list of expected tokens on error by tracing the tables.
      @param state for which to compute the list.
      @return list of token names.
    */
  protected string[] yyExpecting (int state) {
    int token, n, len = 0;
    bool[] ok = new bool[yyName.Length];

    if ((n = yySindex[state]) != 0)
      for (token = n < 0 ? -n : 0;
           (token < yyName.Length) && (n+token < yyTable.Length); ++ token)
        if (yyCheck[n+token] == token && !ok[token] && yyName[token] != null) {
          ++ len;
          ok[token] = true;
        }
    if ((n = yyRindex[state]) != 0)
      for (token = n < 0 ? -n : 0;
           (token < yyName.Length) && (n+token < yyTable.Length); ++ token)
        if (yyCheck[n+token] == token && !ok[token] && yyName[token] != null) {
          ++ len;
          ok[token] = true;
        }

    string [] result = new string[len];
    for (n = token = 0; n < len;  ++ token)
      if (ok[token]) result[n++] = yyName[token];
    return result;
  }

  /** the generated parser, with debugging messages.
      Maintains a state and a value stack, currently with fixed maximum size.
      @param yyLex scanner.
      @param yydebug debug message writer implementing yyDebug, or null.
      @return result of the last reduction, if any.
      @throws yyException on irrecoverable parse error.
    */
  public Object yyparse (yyParser.yyInput yyLex, Object yyd)
				 {
    this.debug = (yydebug.yyDebug)yyd;
    return yyparse(yyLex);
  }

  /** initial size and increment of the state/value stack [default 256].
      This is not final so that it can be overwritten outside of invocations
      of yyparse().
    */
  protected int yyMax;

  /** executed at the beginning of a reduce action.
      Used as $$ = yyDefault($1), prior to the user-specified action, if any.
      Can be overwritten to provide deep copy, etc.
      @param first value for $1, or null.
      @return first.
    */
  protected Object yyDefault (Object first) {
    return first;
  }

  /** the generated parser.
      Maintains a state and a value stack, currently with fixed maximum size.
      @param yyLex scanner.
      @return result of the last reduction, if any.
      @throws yyException on irrecoverable parse error.
    */
  public Object yyparse (yyParser.yyInput yyLex)
				{
    if (yyMax <= 0) yyMax = 256;			// initial size
    int yyState = 0;                                   // state stack ptr
    int [] yyStates = new int[yyMax];	                // state stack 
    Object yyVal = null;                               // value stack ptr
    Object [] yyVals = new Object[yyMax];	        // value stack
    int yyToken = -1;					// current input
    int yyErrorFlag = 0;				// #tks to shift

    int yyTop = 0;
    goto skip;
    yyLoop:
    yyTop++;
    skip:
    for (;; ++ yyTop) {
      if (yyTop >= yyStates.Length) {			// dynamically increase
        int[] i = new int[yyStates.Length+yyMax];
        System.Array.Copy(yyStates, i, 0);
        yyStates = i;
        Object[] o = new Object[yyVals.Length+yyMax];
        System.Array.Copy(yyVals, o, 0);
        yyVals = o;
      }
      yyStates[yyTop] = yyState;
      yyVals[yyTop] = yyVal;
      if (debug != null) debug.push(yyState, yyVal);

      yyDiscarded: for (;;) {	// discarding a token does not change stack
        int yyN;
        if ((yyN = yyDefRed[yyState]) == 0) {	// else [default] reduce (yyN)
          if (yyToken < 0) {
            yyToken = yyLex.advance() ? yyLex.token() : 0;
            if (debug != null)
              debug.lex(yyState, yyToken, yyname(yyToken), yyLex.value());
          }
          if ((yyN = yySindex[yyState]) != 0 && ((yyN += yyToken) >= 0)
              && (yyN < yyTable.Length) && (yyCheck[yyN] == yyToken)) {
            if (debug != null)
              debug.shift(yyState, yyTable[yyN], yyErrorFlag-1);
            yyState = yyTable[yyN];		// shift to yyN
            yyVal = yyLex.value();
            yyToken = -1;
            if (yyErrorFlag > 0) -- yyErrorFlag;
            goto yyLoop;
          }
          if ((yyN = yyRindex[yyState]) != 0 && (yyN += yyToken) >= 0
              && yyN < yyTable.Length && yyCheck[yyN] == yyToken)
            yyN = yyTable[yyN];			// reduce (yyN)
          else
            switch (yyErrorFlag) {
  
            case 0:
              yyerror("syntax error", yyExpecting(yyState));
              if (debug != null) debug.error("syntax error");
              goto case 1;
            case 1: case 2:
              yyErrorFlag = 3;
              do {
                if ((yyN = yySindex[yyStates[yyTop]]) != 0
                    && (yyN += Token.yyErrorCode) >= 0 && yyN < yyTable.Length
                    && yyCheck[yyN] == Token.yyErrorCode) {
                  if (debug != null)
                    debug.shift(yyStates[yyTop], yyTable[yyN], 3);
                  yyState = yyTable[yyN];
                  yyVal = yyLex.value();
                  goto yyLoop;
                }
                if (debug != null) debug.pop(yyStates[yyTop]);
              } while (-- yyTop >= 0);
              if (debug != null) debug.reject();
              throw new yyParser.yyException("irrecoverable syntax error");
  
            case 3:
              if (yyToken == 0) {
                if (debug != null) debug.reject();
                throw new yyParser.yyException("irrecoverable syntax error at end-of-file");
              }
              if (debug != null)
                debug.discard(yyState, yyToken, yyname(yyToken),
  							yyLex.value());
              yyToken = -1;
              goto yyDiscarded;		// leave stack alone
            }
        }
        int yyV = yyTop + 1-yyLen[yyN];
        if (debug != null)
          debug.reduce(yyState, yyStates[yyV-1], yyN, yyRule[yyN], yyLen[yyN]);
        yyVal = yyDefault(yyV > yyTop ? null : yyVals[yyV]);
        switch (yyN) {
case 1:
#line 214 "cs-parser.jay"
  {  
	  /* Compile unit*/
	  bd.AddAssemblyAttributeMultiple((CodeAttributeDeclarationCollection) yyVals[-2+yyTop]);
	  }
  break;
case 3:
#line 223 "cs-parser.jay"
  {
	/*EOF*/
	/*Console.WriteLine("FILE finished");*/
	}
  break;
case 8:
#line 242 "cs-parser.jay"
  {
	  /* Using alias directive is not supported by CODEDOM*/
	  Report.Warning(9003,lexer.Location, "Using alias directives not supported, they will not be parsed");
	  }
  break;
case 9:
#line 250 "cs-parser.jay"
  {
	  /*Add using directive to ns*/
	  bd.AddUsingDirective(yyVals[-1+yyTop].ToString());
	  }
  break;
case 10:
#line 263 "cs-parser.jay"
  {
	  /* Add ns to compile unit */
	  
	  bd.AddNamespace(yyVals[0+yyTop].ToString());
	  }
  break;
case 11:
#line 269 "cs-parser.jay"
  {
	  /* Leaving ns*/
	  bd.Up();
	  }
  break;
case 17:
#line 287 "cs-parser.jay"
  {
	  /*qualified identifier value*/
	  yyVal = ((yyVals[-2+yyTop]).ToString ()) + "." + (yyVals[0+yyTop].ToString ());
	  }
  break;
case 19:
#line 303 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 22:
#line 315 "cs-parser.jay"
  {yyVal=null;}
  break;
case 26:
#line 327 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 33:
#line 350 "cs-parser.jay"
  {
	  /* No attributes*/
	  yyVal=null;
	  }
  break;
case 34:
#line 355 "cs-parser.jay"
  {
	  /* Attrs section*/
	  CodeAttributeDeclarationCollection atrs=null;
	  
	  if (yyVals[-1+yyTop] != null)
	  {
	    atrs=  (CodeAttributeDeclarationCollection) yyVals[-1+yyTop];
	    if (yyVals[0+yyTop] != null)
	    atrs.AddRange((CodeAttributeDeclarationCollection) yyVals[0+yyTop]);
	  }
	 yyVal=atrs;
	  }
  break;
case 35:
#line 375 "cs-parser.jay"
  {
	  /* Attribute section with target*/
	  
	  CodeAttributeDeclarationCollection c= (CodeAttributeDeclarationCollection) yyVals[-1+yyTop];
	  foreach( CodeAttributeDeclarationExt d in c)
	      d.Target= (AttributeTargets) yyVals[-2+yyTop];
	  yyVal=c;
	  }
  break;
case 36:
#line 384 "cs-parser.jay"
  {
	  /* Attribute section*/
	  yyVal=yyVals[-1+yyTop];
	  }
  break;
case 37:
#line 394 "cs-parser.jay"
  {
	  /* Atr target*/
	  yyVal=yyVals[-1+yyTop];
	  }
  break;
case 38:
#line 402 "cs-parser.jay"
  {
	  /* Other targets*/
	  yyVal=CheckAttributeTarget(yyVals[0+yyTop].ToString());
	  }
  break;
case 39:
#line 406 "cs-parser.jay"
  {
	  /* Event target*/
	  yyVal=AttributeTargets.Event;
	  }
  break;
case 40:
#line 410 "cs-parser.jay"
  {
	  /* Return target*/
	  yyVal=AttributeTargets.Return;
	  }
  break;
case 41:
#line 414 "cs-parser.jay"
  {
	  /* Return target*/
	  yyVal=AttributeTargets.Assembly;
	  }
  break;
case 42:
#line 422 "cs-parser.jay"
  {
	  /* Attribute list*/
	  CodeAttributeDeclarationCollection c= new CodeAttributeDeclarationCollection();
	  c.Add((CodeAttributeDeclaration) yyVals[0+yyTop]);
	  yyVal=c;
	  }
  break;
case 43:
#line 429 "cs-parser.jay"
  {
	  /* Attribute list*/
	  CodeAttributeDeclarationCollection c= (CodeAttributeDeclarationCollection) yyVals[-2+yyTop];
	  if (yyVals[0+yyTop]!= null)
	  c.Add((CodeAttributeDeclaration) yyVals[0+yyTop]);
	  yyVal=c;
	  }
  break;
case 44:
#line 439 "cs-parser.jay"
  { 
	yyVal= null;}
  break;
case 46:
#line 448 "cs-parser.jay"
  {
	  /* Attribute*/
	  CodeAttributeDeclarationExt atr = new CodeAttributeDeclarationExt(yyVals[-1+yyTop].ToString());
	 
	  if (yyVals[0+yyTop]!= null)
	    atr.Arguments.AddRange((CodeAttributeArgumentCollection) yyVals[0+yyTop]);
	  yyVal=atr;
	  }
  break;
case 48:
#line 464 "cs-parser.jay"
  {
	  /* No args*/
	  yyVal=null;
	  }
  break;
case 49:
#line 469 "cs-parser.jay"
  {
	  /* arguments*/
	  yyVal=yyVals[-1+yyTop];
	  }
  break;
case 50:
#line 476 "cs-parser.jay"
  {
	  /* No args*/
	  yyVal=null;
	  }
  break;
case 52:
#line 486 "cs-parser.jay"
  {
	  /* Positional args*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 53:
#line 491 "cs-parser.jay"
  {
	  /* Positional plus named args*/
	  CodeAttributeArgumentCollection c= (CodeAttributeArgumentCollection) yyVals[-2+yyTop];
	  c.AddRange((CodeAttributeArgumentCollection) yyVals[0+yyTop]);
	  yyVal=c;
	  }
  break;
case 54:
#line 498 "cs-parser.jay"
  {
	  /* named args*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 55:
#line 507 "cs-parser.jay"
  {
	  /* Pos args list*/
	  CodeAttributeArgumentCollection c = new CodeAttributeArgumentCollection();
	  c.Add(new CodeAttributeArgument((CodeExpression) yyVals[0+yyTop]));
	  yyVal=c;
	  }
  break;
case 56:
#line 514 "cs-parser.jay"
  {
	  /* Pos args list*/
	  CodeAttributeArgumentCollection c = (CodeAttributeArgumentCollection) yyVals[-2+yyTop];
	  c.Add(new CodeAttributeArgument((CodeExpression) yyVals[0+yyTop]));
	  yyVal=c;
	  }
  break;
case 57:
#line 524 "cs-parser.jay"
  {
	   /* Named args list*/
	  CodeAttributeArgumentCollection c = new CodeAttributeArgumentCollection();
	  c.Add((CodeAttributeArgument) yyVals[0+yyTop]);
	  yyVal=c;
	  }
  break;
case 58:
#line 531 "cs-parser.jay"
  {
	  /* Named args list*/
	  CodeAttributeArgumentCollection c = (CodeAttributeArgumentCollection) yyVals[-2+yyTop];
	  c.Add((CodeAttributeArgument) yyVals[0+yyTop]);
	  yyVal=c;
	  }
  break;
case 59:
#line 541 "cs-parser.jay"
  {
	  /* Name argument*/
	  yyVal= new CodeAttributeArgument(yyVals[-2+yyTop].ToString(), (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 75:
#line 581 "cs-parser.jay"
  {
	  /* add struct to ns*/
	  bd.AddType(yyVals[-1+yyTop].ToString(),(ModifierAttribs)yyVals[-3+yyTop], Types.Struct, 
	  ((CodeTypeReferenceCollection) yyVals[0+yyTop]), classScope>0, 
	  (CodeAttributeDeclarationCollection) yyVals[-4+yyTop]);
	  classScope++;
	  }
  break;
case 76:
#line 589 "cs-parser.jay"
  {
	  /* Leaving struct definition*/
	  classScope--;
	  bd.Up();
	  }
  break;
case 78:
#line 599 "cs-parser.jay"
  {
	/* Empty struct interface*/
	yyVal=null;
	}
  break;
case 80:
#line 616 "cs-parser.jay"
  {
	/* Struct interface*/
	yyVal=yyVals[0+yyTop];
	}
  break;
case 96:
#line 661 "cs-parser.jay"
  {
	  /* Add Const to type*/
	  bd.AddFieldMultiple((CodeTypeMemberCollection) yyVals[-1+yyTop], (ModifierAttribs) yyVals[-4+yyTop], 
	  (CodeTypeReference) yyVals[-2+yyTop], Members.Constant, 
	  (CodeAttributeDeclarationCollection) yyVals[-5+yyTop]);
	  }
  break;
case 97:
#line 672 "cs-parser.jay"
  {
	  /* Const declarations*/
	  CodeTypeMemberCollection mc= new CodeTypeMemberCollection();
	  mc.Add((CodeTypeMember) yyVals[0+yyTop]);
	  yyVal=mc;
	  }
  break;
case 98:
#line 679 "cs-parser.jay"
  {
	  /* Const declarations*/
	  CodeTypeMemberCollection mc= (CodeTypeMemberCollection) yyVals[-2+yyTop];
	  mc.Add((CodeTypeMember) yyVals[0+yyTop]);
	  yyVal=mc;
	  }
  break;
case 99:
#line 689 "cs-parser.jay"
  {
	  /* Const declaration*/
	  /* No init expresion implemented yet*/
	  CodeMemberField mf=new CodeMemberField();
	  mf.Name=yyVals[-2+yyTop].ToString();
	  mf.InitExpression=(CodeExpression) yyVals[0+yyTop];
	  yyVal= mf;
	  }
  break;
case 100:
#line 705 "cs-parser.jay"
  {
	  /* Add Field to type*/
	  bd.AddFieldMultiple((CodeTypeMemberCollection) yyVals[-1+yyTop], (ModifierAttribs) yyVals[-3+yyTop], 
	  (CodeTypeReference) yyVals[-2+yyTop], Members.Field, (CodeAttributeDeclarationCollection) yyVals[-4+yyTop]);
	  }
  break;
case 101:
#line 715 "cs-parser.jay"
  {
	  /* Field declarations*/
	  CodeTypeMemberCollection mc= new CodeTypeMemberCollection();
	  mc.Add((CodeTypeMember) yyVals[0+yyTop]);
	  yyVal=mc;
	  }
  break;
case 102:
#line 722 "cs-parser.jay"
  {
	   /* Field declarations*/
	  CodeTypeMemberCollection mc= (CodeTypeMemberCollection) yyVals[-2+yyTop];
	  mc.Add((CodeTypeMember) yyVals[0+yyTop]);
	  yyVal=mc;
	  }
  break;
case 103:
#line 732 "cs-parser.jay"
  {
	  /* Field declaration*/
	  /* No init expresion implemented yet*/
	  CodeMemberField mf=new CodeMemberField();
	  mf.Name=yyVals[-2+yyTop].ToString();
	  mf.InitExpression=((CodeExpression) yyVals[0+yyTop]);
	  mf.Comments.AddRange(cmtBuilder.CurrComments);
	  cmtBuilder.ClearComments();
	  yyVal= mf;
	  }
  break;
case 104:
#line 743 "cs-parser.jay"
  {
	  /* Field declaration*/
	  CodeMemberField mf=new CodeMemberField();
	  mf.Name=yyVals[0+yyTop].ToString();
	  mf.Comments.AddRange(cmtBuilder.CurrComments);
	  cmtBuilder.ClearComments();
	  yyVal= mf;
	  }
  break;
case 105:
#line 755 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 106:
#line 759 "cs-parser.jay"
  {
	  /* Replaced*/
	  /* TODO: (? Expression for array initializer)*/
	  
	 CodeArrayCreateExpression a = new CodeArrayCreateExpression();
			a.CreateType= new CodeTypeReference("");

			CodeExpressionCollection init =(CodeExpressionCollection) yyVals[0+yyTop];

			if (init!=null && init.Count>0) 
			{
				a.Initializers.AddRange(init);
			}
			

			yyVal=a;
	  }
  break;
case 107:
#line 777 "cs-parser.jay"
  {
	  /* Replaced*/
	  /* Todo*/
	  }
  break;
case 108:
#line 789 "cs-parser.jay"
  {
	  /* Add method*/
	  bd.AddMethod(yyVals[-3+yyTop].ToString(), (ModifierAttribs) yyVals[-5+yyTop], (CodeTypeReference) yyVals[-4+yyTop],
	  (CodeParameterDeclarationExpressionCollection) yyVals[-1+yyTop], Members.Method, 
	  (CodeAttributeDeclarationCollection) yyVals[-6+yyTop]);
	  }
  break;
case 109:
#line 796 "cs-parser.jay"
  {
	  /* Leaving method*/
	  bd.Up();
	  }
  break;
case 110:
#line 805 "cs-parser.jay"
  {
	  /* Add method*/
	  bd.AddMethod(yyVals[-3+yyTop].ToString(), (ModifierAttribs) yyVals[-5+yyTop], null,
	  (CodeParameterDeclarationExpressionCollection) yyVals[-1+yyTop], Members.Method, 
	  (CodeAttributeDeclarationCollection) yyVals[-6+yyTop]);
	  
	  }
  break;
case 111:
#line 813 "cs-parser.jay"
  {
	  /* Leaving method*/
	  bd.Up();
	  }
  break;
case 113:
#line 821 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 114:
#line 827 "cs-parser.jay"
  {
	  /* Empty param list*/
	  yyVal=null;
	  }
  break;
case 116:
#line 836 "cs-parser.jay"
  {
	  /* fixed params*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 117:
#line 841 "cs-parser.jay"
  {
	  /* Adds parram array to fixed params*/
	  CodeParameterDeclarationExpressionCollection parCollection= 
	  (CodeParameterDeclarationExpressionCollection) yyVals[-2+yyTop];
	  parCollection.Add((CodeParameterDeclarationExpression) yyVals[0+yyTop]);
	  yyVal=parCollection;
	  }
  break;
case 118:
#line 849 "cs-parser.jay"
  {
	  /* Param Array*/
	  CodeParameterDeclarationExpressionCollection parCollection= 
	  new CodeParameterDeclarationExpressionCollection();
	  parCollection.Add((CodeParameterDeclarationExpression) yyVals[0+yyTop]);
	  yyVal=parCollection;
	  }
  break;
case 119:
#line 860 "cs-parser.jay"
  {
	  /* Fixed param*/
	  CodeParameterDeclarationExpressionCollection parCollection= 
	  new CodeParameterDeclarationExpressionCollection();
	  parCollection.Add((CodeParameterDeclarationExpression) yyVals[0+yyTop]);
	  yyVal=parCollection;
	  }
  break;
case 120:
#line 868 "cs-parser.jay"
  {
	  /* Fixed params*/
	  CodeParameterDeclarationExpressionCollection parCollection= 
	  (CodeParameterDeclarationExpressionCollection) yyVals[-2+yyTop];
	  parCollection.Add((CodeParameterDeclarationExpression) yyVals[0+yyTop]);
	  yyVal=parCollection;
	  }
  break;
case 121:
#line 882 "cs-parser.jay"
  {
	  /* Param*/
	  CodeParameterDeclarationExpression parDecl= 
	  new CodeParameterDeclarationExpression((CodeTypeReference) yyVals[-1+yyTop], yyVals[0+yyTop].ToString());
	  if (((ParamModifiers)yyVals[-2+yyTop])==ParamModifiers.Out)
		parDecl.Direction=FieldDirection.Out;
	  if (((ParamModifiers)yyVals[-2+yyTop])==ParamModifiers.Ref)
		parDecl.Direction=FieldDirection.Ref;
		if (yyVals[-3+yyTop] !=null)
		bd.AddAttributeToParamMultiple((CodeAttributeDeclarationCollection) yyVals[-3+yyTop], parDecl);
	  
	  parDecl.UserData["Location"]= new CodeLinePragma(name, lexer.Line);
	  yyVal=parDecl;
	  }
  break;
case 122:
#line 899 "cs-parser.jay"
  {
	  /* Empty modifier*/
	  yyVal=ParamModifiers.In;
	  }
  break;
case 124:
#line 907 "cs-parser.jay"
  {
	  /* ref modifier*/
	  yyVal=ParamModifiers.Ref;
	  }
  break;
case 125:
#line 911 "cs-parser.jay"
  {
	  /* out modifier*/
	  yyVal=ParamModifiers.Out;
	  }
  break;
case 126:
#line 919 "cs-parser.jay"
  {
	  /* Params keyword - params array*/
	  Report.Warning(9005, lexer.Location, "Params keyword will not be mapped to CodeDom, it will be a regular param");
	  /* Param*/
	  CodeParameterDeclarationExpression parDecl= 
	  new CodeParameterDeclarationExpression((CodeTypeReference) yyVals[-1+yyTop], yyVals[0+yyTop].ToString());
	  if (yyVals[-3+yyTop] !=null)
		bd.AddAttributeToParamMultiple((CodeAttributeDeclarationCollection) yyVals[-3+yyTop], parDecl);
	  yyVal=parDecl;
	  }
  break;
case 128:
#line 939 "cs-parser.jay"
  {
	  /* Start parsing property body*/
	  lexer.PropertyParsing = true;
	  /* Add property*/
	  bd.AddProperty(yyVals[0+yyTop].ToString(), (ModifierAttribs) yyVals[-2+yyTop], (CodeTypeReference) yyVals[-1+yyTop],
		null, Members.Property, (CodeAttributeDeclarationCollection) yyVals[-3+yyTop]);
	  
	  }
  break;
case 129:
#line 949 "cs-parser.jay"
  {
	  /*End parsing property body*/
	  lexer.PropertyParsing = false;
	  
	  }
  break;
case 130:
#line 955 "cs-parser.jay"
  {
	  /* Leaving property*/
	  bd.Up();
	  }
  break;
case 131:
#line 963 "cs-parser.jay"
  {
	  /* Property accessors */
	  }
  break;
case 132:
#line 967 "cs-parser.jay"
  {
	  /* Property accessors */
	  
	  }
  break;
case 133:
#line 974 "cs-parser.jay"
  {
	  /* Empty get accessor*/
	  yyVal=null;
	  }
  break;
case 135:
#line 982 "cs-parser.jay"
  {
	  /* Empty set accessor*/
	  yyVal=null;
	  }
  break;
case 137:
#line 991 "cs-parser.jay"
  {
	  /* Get Accessor*/
	  lexer.PropertyParsing = false;
	  ((CodeMemberProperty)bd.CurrMember).HasGet=true;
	  }
  break;
case 138:
#line 997 "cs-parser.jay"
  {
	  /* Leaving get accessor*/
	  lexer.PropertyParsing = true;
	  }
  break;
case 139:
#line 1005 "cs-parser.jay"
  {
	  /* Set Accessor*/
	  lexer.PropertyParsing = false;
	  ((CodeMemberProperty)bd.CurrMember).HasSet=true;
	  }
  break;
case 140:
#line 1011 "cs-parser.jay"
  {
	  /* leaving set accessor*/
	  lexer.PropertyParsing = true;
	  
	  }
  break;
case 142:
#line 1020 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 143:
#line 1030 "cs-parser.jay"
  {
	  /* Add interface to ns*/
	  bd.AddType(yyVals[-1+yyTop].ToString(),(ModifierAttribs)yyVals[-3+yyTop], Types.Interface, 
	  ((CodeTypeReferenceCollection) yyVals[0+yyTop]), classScope>0, 
	  (CodeAttributeDeclarationCollection) yyVals[-4+yyTop]);
	  classScope++;
	  }
  break;
case 144:
#line 1038 "cs-parser.jay"
  {
	  /* Leaving interface definnition*/
	  classScope--;
	  bd.Up();
	  }
  break;
case 146:
#line 1047 "cs-parser.jay"
  {
	  /* Interface base*/
	  yyVal=null;
	  }
  break;
case 148:
#line 1055 "cs-parser.jay"
  {
	  /* Interface base*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 149:
#line 1063 "cs-parser.jay"
  {
	  /* Add type to collection of interface types*/
	  CodeTypeReferenceCollection tl=new CodeTypeReferenceCollection();
	  tl.Add((CodeTypeReference) yyVals[0+yyTop]);
	  yyVal=tl;
	  }
  break;
case 150:
#line 1070 "cs-parser.jay"
  {
	  /* Add type to collection of interface types*/
	  CodeTypeReferenceCollection tl= (CodeTypeReferenceCollection) yyVals[-2+yyTop];
	  tl.Add((CodeTypeReference) yyVals[0+yyTop]);
	  yyVal=tl;
	  }
  break;
case 160:
#line 1102 "cs-parser.jay"
  {
	  yyVal=0;
	  }
  break;
case 161:
#line 1105 "cs-parser.jay"
  {
	/* new modifier*/
	  yyVal=  ModifierAttribs.New;
	  }
  break;
case 162:
#line 1115 "cs-parser.jay"
  {
	  /* Add interface method */
	   bd.AddMethod(yyVals[-4+yyTop].ToString(), (ModifierAttribs) yyVals[-6+yyTop], (CodeTypeReference) yyVals[-5+yyTop],
	  (CodeParameterDeclarationExpressionCollection) yyVals[-2+yyTop], Members.Method, 
	  (CodeAttributeDeclarationCollection) yyVals[-7+yyTop]);
	  bd.Up();
	  }
  break;
case 163:
#line 1125 "cs-parser.jay"
  {
	  /* Add Method*/
	  bd.AddMethod(yyVals[-4+yyTop].ToString(), (ModifierAttribs) yyVals[-6+yyTop], null,
	  (CodeParameterDeclarationExpressionCollection) yyVals[-2+yyTop], Members.Method, 
	  (CodeAttributeDeclarationCollection) yyVals[-7+yyTop]);
	  bd.Up();
	  }
  break;
case 164:
#line 1139 "cs-parser.jay"
  {
	  /* Interface property*/
	  lexer.PropertyParsing = true;
	   bd.AddProperty(yyVals[-1+yyTop].ToString(), (ModifierAttribs) yyVals[-3+yyTop], (CodeTypeReference) yyVals[-2+yyTop],
		null, Members.Property, (CodeAttributeDeclarationCollection) yyVals[-4+yyTop]);
	  }
  break;
case 165:
#line 1146 "cs-parser.jay"
  {
	  /* Interface property 2*/
	  lexer.PropertyParsing = false;
	 
	  }
  break;
case 166:
#line 1152 "cs-parser.jay"
  {
	  /*Leaving interface property */
	   bd.Up();
	  }
  break;
case 167:
#line 1159 "cs-parser.jay"
  {
	  /* Interface get accessor*/
	  ((CodeMemberProperty)bd.CurrMember).HasGet=true;
	  }
  break;
case 168:
#line 1163 "cs-parser.jay"
  {
	  /* Interface set accessor*/
	  ((CodeMemberProperty)bd.CurrMember).HasSet=true;
	  }
  break;
case 169:
#line 1168 "cs-parser.jay"
  {
	  /* Intreface get and set accessor*/
	  ((CodeMemberProperty)bd.CurrMember).HasGet=true;
	  ((CodeMemberProperty)bd.CurrMember).HasSet=true;
	  }
  break;
case 170:
#line 1174 "cs-parser.jay"
  {
	  /* Intreface get and set accessor*/
	  ((CodeMemberProperty)bd.CurrMember).HasGet=true;
	  ((CodeMemberProperty)bd.CurrMember).HasSet=true;
	  }
  break;
case 171:
#line 1183 "cs-parser.jay"
  {
	  /* Add evebt*/
	  bd.AddEvent(yyVals[-1+yyTop].ToString(), (ModifierAttribs)yyVals[-4+yyTop], 
	  (CodeTypeReference) yyVals[-2+yyTop], Members.Event, (CodeAttributeDeclarationCollection) yyVals[-5+yyTop]);
	  bd.Up();
	  }
  break;
case 172:
#line 1195 "cs-parser.jay"
  {
	  /* Interface indexer*/
	  
	  bd.AddProperty("Item", (ModifierAttribs) yyVals[-6+yyTop], (CodeTypeReference) yyVals[-5+yyTop],
		(CodeParameterDeclarationExpressionCollection) yyVals[-2+yyTop], Members.Indexer, 
		(CodeAttributeDeclarationCollection) yyVals[-7+yyTop]);
	  lexer.PropertyParsing = true;
	  }
  break;
case 173:
#line 1204 "cs-parser.jay"
  {
	  /* Interface idexer 2*/
	  lexer.PropertyParsing = false;
	  }
  break;
case 174:
#line 1209 "cs-parser.jay"
  {
	  /* Leaving interface indexer*/
	  bd.Up();
	  }
  break;
case 175:
#line 1217 "cs-parser.jay"
  {
	  /* Operator*/
	  Report.Warning(9008, lexer.Location, "Operator will not be parsed (no support in CODEDOM)");
	  }
  break;
case 176:
#line 1226 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 177:
#line 1234 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 179:
#line 1242 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 180:
#line 1245 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 181:
#line 1248 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 182:
#line 1251 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 183:
#line 1254 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 184:
#line 1257 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 185:
#line 1261 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 186:
#line 1264 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 187:
#line 1268 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 188:
#line 1271 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 189:
#line 1274 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 190:
#line 1277 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 191:
#line 1280 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 192:
#line 1283 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 193:
#line 1286 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 194:
#line 1289 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 195:
#line 1292 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 196:
#line 1295 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 197:
#line 1298 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 198:
#line 1301 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 199:
#line 1304 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 200:
#line 1307 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 201:
#line 1314 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 202:
#line 1318 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 203:
#line 1322 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 204:
#line 1326 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 205:
#line 1335 "cs-parser.jay"
  {
	  /* Constructor declaration*/
	  bd.AddConstructor((ConstructorDecl )yyVals[0+yyTop], (ModifierAttribs) yyVals[-1+yyTop], 
		Members.Constructor , (CodeAttributeDeclarationCollection) yyVals[-2+yyTop]);
	  }
  break;
case 206:
#line 1341 "cs-parser.jay"
  {
	  /*leaving constructor declaration*/
	  bd.Up();
	  
	  }
  break;
case 207:
#line 1352 "cs-parser.jay"
  {
	  yyVal= new ConstructorDecl(yyVals[-4+yyTop].ToString(), 
	  (CodeParameterDeclarationExpressionCollection) yyVals[-2+yyTop], 
			null);
	  }
  break;
case 208:
#line 1360 "cs-parser.jay"
  {
	  /* Empty constructor initializer*/
	  yyVal=null;
	  }
  break;
case 210:
#line 1369 "cs-parser.jay"
  {
	  /* base const. initializer*/
	  /*should implement initializer*/
	  yyVal=null;
	  }
  break;
case 211:
#line 1375 "cs-parser.jay"
  {
	  /* this const. initializer*/
	  /*should implement initializer*/
	  yyVal=null;
	  }
  break;
case 212:
#line 1384 "cs-parser.jay"
  {
	  /* Replaced*/
	   Report.Warning(9009, lexer.Location, "Destructor will not be parsed (no support in CODEDOM)");
	  }
  break;
case 213:
#line 1394 "cs-parser.jay"
  {
	  /* Event Declaration*/
	  bd.AddEventMultiple((CodeTypeMemberCollection) yyVals[-1+yyTop], (ModifierAttribs)yyVals[-4+yyTop], 
	  (CodeTypeReference) yyVals[-2+yyTop], Members.Event, (CodeAttributeDeclarationCollection) yyVals[-5+yyTop]);
	  bd.Up();
	  }
  break;
case 214:
#line 1404 "cs-parser.jay"
  {
	  /* Replaced*/
	  lexer.EventParsing = true;
	  bd.AddEvent(yyVals[-1+yyTop].ToString(), (ModifierAttribs)yyVals[-4+yyTop], 
	  (CodeTypeReference) yyVals[-2+yyTop], Members.Event, (CodeAttributeDeclarationCollection) yyVals[-5+yyTop]);
	  }
  break;
case 215:
#line 1411 "cs-parser.jay"
  {
	  /* Replaced*/
	  lexer.EventParsing = false;
	  }
  break;
case 216:
#line 1416 "cs-parser.jay"
  {
	  /* Leaving event*/
	  bd.Up();
	  }
  break;
case 217:
#line 1424 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 218:
#line 1428 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 219:
#line 1435 "cs-parser.jay"
  {
	  /* Replaced*/
	   Report.Warning(9010, lexer.Location, "Add accessor will not be parsed (no support in CODEDOM)");
	  lexer.EventParsing = false;
	  }
  break;
case 220:
#line 1441 "cs-parser.jay"
  {
	  /* Replaced*/
	  lexer.EventParsing = true;
	  }
  break;
case 221:
#line 1449 "cs-parser.jay"
  {
	  /* Replaced*/
	   Report.Warning(9011, lexer.Location, "Remove accessor not be parsed (no support in CODEDOM)");
	  lexer.EventParsing = false;
	  }
  break;
case 222:
#line 1455 "cs-parser.jay"
  {
	  /* Replaced*/
	  lexer.EventParsing = true;
	  }
  break;
case 223:
#line 1464 "cs-parser.jay"
  {
	  /* Add Indexer*/
	  IndexerDecl d =(IndexerDecl) yyVals[-1+yyTop];
	  bd.AddProperty(d.Name, (ModifierAttribs) yyVals[-2+yyTop], d.Type,
		d.Params, Members.Indexer, (CodeAttributeDeclarationCollection) yyVals[-3+yyTop]);
	  lexer.PropertyParsing = true;
	  }
  break;
case 224:
#line 1472 "cs-parser.jay"
  {
	  /* Replaced*/
	  lexer.PropertyParsing = false;
	  }
  break;
case 225:
#line 1477 "cs-parser.jay"
  {
	  /* Exiting Indexer declaration*/
	  bd.Up();
	  }
  break;
case 226:
#line 1485 "cs-parser.jay"
  {
	  /*Indexer declarator*/
	  yyVal=new IndexerDecl("Item", (CodeParameterDeclarationExpressionCollection) yyVals[-1+yyTop], (CodeTypeReference) yyVals[-4+yyTop]);
	  }
  break;
case 227:
#line 1490 "cs-parser.jay"
  {
	  /* Indexer declarator*/
	  yyVal=new IndexerDecl("Item", (CodeParameterDeclarationExpressionCollection) yyVals[-1+yyTop], (CodeTypeReference) yyVals[-6+yyTop]);
	  }
  break;
case 228:
#line 1501 "cs-parser.jay"
  {
	  /* add enum to ns*/
	  bd.AddType(yyVals[-1+yyTop].ToString(),(ModifierAttribs)yyVals[-3+yyTop], Types.Enum, 
	  ((CodeTypeReferenceCollection) yyVals[0+yyTop]), classScope>0, 
	  (CodeAttributeDeclarationCollection) yyVals[-4+yyTop]);
	  classScope++;
	  }
  break;
case 229:
#line 1509 "cs-parser.jay"
  {
	  /* Leaving enum declaration*/
	  classScope--;
	  bd.Up();
	  }
  break;
case 231:
#line 1518 "cs-parser.jay"
  {
	  /* Empty enum base type*/
	  }
  break;
case 232:
#line 1521 "cs-parser.jay"
  {
	  /* Enum base type*/
	  CodeTypeReferenceCollection tl=new CodeTypeReferenceCollection();
	  tl.Add((CodeTypeReference) yyVals[0+yyTop]);
	  yyVal=tl;
	  
	  }
  break;
case 233:
#line 1532 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 234:
#line 1538 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 235:
#line 1541 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 236:
#line 1548 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 237:
#line 1552 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 238:
#line 1559 "cs-parser.jay"
  {
	  /*Enum member*/
	  bd.AddField(yyVals[0+yyTop].ToString(), 0, null, 
	  Members.Constant, (CodeAttributeDeclarationCollection) yyVals[-1+yyTop]);
			
	  }
  break;
case 239:
#line 1567 "cs-parser.jay"
  {
	  /* Enum member*/
	  CodeMemberField m= new CodeMemberField();
	  m.Name=yyVals[-2+yyTop].ToString();
	  m.InitExpression=(CodeExpression) yyVals[0+yyTop];
	  m.Comments.AddRange(cmtBuilder.CurrComments);
	  cmtBuilder.ClearComments();
	  bd.AddField(m, 0, null, 
	  Members.Constant ,  (CodeAttributeDeclarationCollection) yyVals[-3+yyTop]);
	
	  }
  break;
case 240:
#line 1588 "cs-parser.jay"
  {
	  /* Add delegate to ns*/
	  bd.AddDelegate(yyVals[-4+yyTop].ToString(), (ModifierAttribs) yyVals[-7+yyTop], 
			(CodeParameterDeclarationExpressionCollection) yyVals[-2+yyTop],
			(CodeTypeReference) yyVals[-5+yyTop], classScope>0, 
			(CodeAttributeDeclarationCollection) yyVals[-8+yyTop]);
	  }
  break;
case 241:
#line 1602 "cs-parser.jay"
  {
	  /* Add delegate to ns*/
	  bd.AddDelegate(yyVals[-4+yyTop].ToString(), (ModifierAttribs) yyVals[-7+yyTop], 
			(CodeParameterDeclarationExpressionCollection) yyVals[-2+yyTop],
			null, classScope>0, (CodeAttributeDeclarationCollection) yyVals[-8+yyTop]);
	  	
	  }
  break;
case 244:
#line 1626 "cs-parser.jay"
  {
	  /* Type name*/
	  yyVal = new CodeTypeReference(yyVals[0+yyTop].ToString());
	  }
  break;
case 245:
#line 1630 "cs-parser.jay"
  {
	  /* Build In Types*/
	  yyVal = new CodeTypeReference(yyVals[0+yyTop].ToString());
	  }
  break;
case 246:
#line 1635 "cs-parser.jay"
  {
	  /* Array type*/
	  yyVal = yyVals[0+yyTop];
	  }
  break;
case 247:
#line 1639 "cs-parser.jay"
  {
	  /* Pointer Type ???*/
	  yyVal = yyVals[0+yyTop];
	  }
  break;
case 248:
#line 1648 "cs-parser.jay"
  {
	  /* Pointer Type ???*/
	  CodeTypeReference c= (CodeTypeReference) yyVals[-1+yyTop];
	  c.BaseType+="*";
	  yyVal =  c;
	  }
  break;
case 249:
#line 1655 "cs-parser.jay"
  {
	  /* Void pointer*/
	  yyVal = "System.Void*";
	  }
  break;
case 250:
#line 1663 "cs-parser.jay"
  {
	  /* bi types*/
	  /*$$=new CodeTypeReference($1.*/
	  }
  break;
case 251:
#line 1668 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 252:
#line 1672 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 253:
#line 1676 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 254:
#line 1680 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 255:
#line 1687 "cs-parser.jay"
  {
	   /* Add type to collection*/
	  CodeTypeReferenceCollection tl=new CodeTypeReferenceCollection();
	  tl.Add((CodeTypeReference) yyVals[0+yyTop]);
	  yyVal=tl;
	  }
  break;
case 256:
#line 1694 "cs-parser.jay"
  {
	  /* Add type to collection*/
	  CodeTypeReferenceCollection tl= (CodeTypeReferenceCollection) yyVals[-2+yyTop];
	  tl.Add((CodeTypeReference) yyVals[0+yyTop]);
	  yyVal=tl;
	  }
  break;
case 257:
#line 1704 "cs-parser.jay"
  { yyVal = "System.Object"; }
  break;
case 258:
#line 1705 "cs-parser.jay"
  { yyVal = "System.String"; }
  break;
case 259:
#line 1706 "cs-parser.jay"
  { yyVal = "System.Boolean"; }
  break;
case 260:
#line 1707 "cs-parser.jay"
  { yyVal = "System.Decimal"; }
  break;
case 261:
#line 1708 "cs-parser.jay"
  { yyVal = "System.Single"; }
  break;
case 262:
#line 1709 "cs-parser.jay"
  { yyVal = "System.Double"; }
  break;
case 264:
#line 1714 "cs-parser.jay"
  { yyVal = "System.SByte"; }
  break;
case 265:
#line 1715 "cs-parser.jay"
  { yyVal = "System.Byte"; }
  break;
case 266:
#line 1716 "cs-parser.jay"
  { yyVal = "System.Int16"; }
  break;
case 267:
#line 1717 "cs-parser.jay"
  { yyVal = "System.UInt16"; }
  break;
case 268:
#line 1718 "cs-parser.jay"
  { yyVal = "System.Int32"; }
  break;
case 269:
#line 1719 "cs-parser.jay"
  { yyVal = "System.UInt32"; }
  break;
case 270:
#line 1720 "cs-parser.jay"
  { yyVal = "System.Int64"; }
  break;
case 271:
#line 1721 "cs-parser.jay"
  { yyVal = "System.UInt64"; }
  break;
case 272:
#line 1722 "cs-parser.jay"
  { yyVal = "System.Char"; }
  break;
case 273:
#line 1727 "cs-parser.jay"
  {
	/* Interface type*/
	yyVal= new CodeTypeReference(yyVals[0+yyTop].ToString());
	}
  break;
case 274:
#line 1735 "cs-parser.jay"
  {
	  /* Array Type*/
	  
	  yyVal=CommonUtil.CreateArrayType((CodeTypeReference) yyVals[-1+yyTop], yyVals[0+yyTop].ToString());
	  }
  break;
case 276:
#line 1748 "cs-parser.jay"
  {
	/*QI as primary expression*/
	/*this is problem we cannot say what is it right now*/
	yyVal=bd.ResolveIdentifier(yyVals[0+yyTop].ToString());
	}
  break;
case 294:
#line 1775 "cs-parser.jay"
  {
	   /*Char literal*/
	  yyVal=new CodePrimitiveExpression(Char.Parse(yyVals[0+yyTop].ToString()));
	  }
  break;
case 295:
#line 1779 "cs-parser.jay"
  {
	  /* Char literal*/
	  yyVal=new CodePrimitiveExpression(yyVals[0+yyTop].ToString());
	  }
  break;
case 296:
#line 1783 "cs-parser.jay"
  {
	  /* Null literal*/
	  yyVal=new CodePrimitiveExpression(null);
	  }
  break;
case 297:
#line 1790 "cs-parser.jay"
  {
	  /* Real literal*/
	  yyVal=new CodePrimitiveExpression((Single) yyVals[0+yyTop]);
	  }
  break;
case 298:
#line 1794 "cs-parser.jay"
  {
	  /* Double literal*/
	  yyVal=new CodePrimitiveExpression((Double) yyVals[0+yyTop]);
	  
	  }
  break;
case 299:
#line 1799 "cs-parser.jay"
  {
	/*Decimal literal*/
	  yyVal=new CodePrimitiveExpression((Decimal)yyVals[0+yyTop] );
	  }
  break;
case 300:
#line 1806 "cs-parser.jay"
  {
	  /* Int literal*/
	  /*$$=new CodePrimitiveExpression($1 );*/
	  yyVal=new CodePrimitiveExpression(lexer.Value);
	  }
  break;
case 301:
#line 1814 "cs-parser.jay"
  {
	  /* Bool literal*/
	  yyVal=new CodePrimitiveExpression(true);
	  }
  break;
case 302:
#line 1818 "cs-parser.jay"
  {
	   /* Bool literal*/
	    yyVal=new CodePrimitiveExpression(true);
	  }
  break;
case 303:
#line 1826 "cs-parser.jay"
  {
	  /* parenthesized_expression*/
	  yyVal=yyVals[-1+yyTop];
	  }
  break;
case 304:
#line 1834 "cs-parser.jay"
  {
	  /* Field access*/
	  
	  yyVal= new CodeFieldReferenceExpression((CodeExpression) yyVals[-2+yyTop], yyVals[0+yyTop].ToString());
	  }
  break;
case 305:
#line 1840 "cs-parser.jay"
  {
	  /* predef type field access*/
	  yyVal= new CodeFieldReferenceExpression(new CodeTypeReferenceExpression(yyVals[-2+yyTop].ToString()), yyVals[0+yyTop].ToString());
	  }
  break;
case 307:
#line 1852 "cs-parser.jay"
  {
	  /* Method call*/
			CodeExpression e = (CodeExpression) yyVals[-3+yyTop];
			CodeExpression tg;
			string name;
	  
			if ((e is CodeFieldReferenceExpression))
			{
				tg= ((CodeFieldReferenceExpression) e).TargetObject;
				name = ((CodeFieldReferenceExpression) e).FieldName;
			}
			else
			{
				tg=null;
				name=yyVals[-3+yyTop].ToString();
			}

			CodeExpressionCollection p= (CodeExpressionCollection) yyVals[-1+yyTop];
			CodeExpression[] pa= new CodeExpression[p.Count];
			p.CopyTo(pa,0);
	  
			CodeMethodReferenceExpression m= new CodeMethodReferenceExpression(tg, name);
			
			yyVal=new CodeMethodInvokeExpression(m,pa);
	  }
  break;
case 308:
#line 1880 "cs-parser.jay"
  {
	  yyVal = new CodeExpressionCollection();
	  }
  break;
case 310:
#line 1888 "cs-parser.jay"
  {
	  /*args list*/
		CodeExpressionCollection c= new CodeExpressionCollection();
		c.Add((CodeExpression) yyVals[0+yyTop]);
		yyVal=c;
	  
	  }
  break;
case 311:
#line 1896 "cs-parser.jay"
  {
	  /* args list*/
	    CodeExpressionCollection c= (CodeExpressionCollection) yyVals[-2+yyTop];
		c.Add((CodeExpression) yyVals[0+yyTop]);
		yyVal=c;
	  }
  break;
case 313:
#line 1907 "cs-parser.jay"
  {
	  /* Ref direction*/
	  yyVal=new CodeDirectionExpression(FieldDirection.Ref, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 314:
#line 1912 "cs-parser.jay"
  {
	  /* out ref*/
	  yyVal=new CodeDirectionExpression(FieldDirection.Ref, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 315:
#line 1919 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 316:
#line 1926 "cs-parser.jay"
  {
	  /* Indexer access*/
	  CodeExpressionCollection p= (CodeExpressionCollection) yyVals[-1+yyTop];
			CodeExpression[] pa= new CodeExpression[p.Count];
			p.CopyTo(pa,0);
	  yyVal=new CodeIndexerExpression((CodeExpression) yyVals[-3+yyTop],pa); 
	  }
  break;
case 317:
#line 1934 "cs-parser.jay"
  {
	  /* Indexer access*/
	  /* TODO: - need fix*/
	  yyVal=yyVals[-1+yyTop];
	  }
  break;
case 318:
#line 1943 "cs-parser.jay"
  {
	 /*exps list*/
		CodeExpressionCollection c= new CodeExpressionCollection();
		c.Add((CodeExpression) yyVals[0+yyTop]);
		yyVal=c;
	  }
  break;
case 319:
#line 1950 "cs-parser.jay"
  {
	  /* exps list*/
	    CodeExpressionCollection c= (CodeExpressionCollection) yyVals[-2+yyTop];
		c.Add((CodeExpression) yyVals[0+yyTop]);
		yyVal=c;
	  }
  break;
case 320:
#line 1960 "cs-parser.jay"
  {
	  /* this exp*/
	  yyVal=new CodeThisReferenceExpression();
	  }
  break;
case 321:
#line 1968 "cs-parser.jay"
  {
	  /* base access 1*/
	  CodeBaseReferenceExpression b= new CodeBaseReferenceExpression();
	  yyVal = new CodeFieldReferenceExpression(b, yyVals[0+yyTop].ToString());
	  }
  break;
case 322:
#line 1974 "cs-parser.jay"
  {
	  /* base access 2*/
	  CodeBaseReferenceExpression b= new CodeBaseReferenceExpression();
	  CodeExpressionCollection p= (CodeExpressionCollection) yyVals[-1+yyTop];
			CodeExpression[] pa= new CodeExpression[p.Count];
			p.CopyTo(pa,0);
	  yyVal=new CodeIndexerExpression(b,pa); 
	  }
  break;
case 323:
#line 1986 "cs-parser.jay"
  {
	  /* post increment*/
	  /*TODO: extend*/
	  yyVal=new CodeBinaryOperatorExpression((CodeExpression) yyVals[-1+yyTop], CodeBinaryOperatorType.Add, 
	  new CodePrimitiveExpression(1));
	  }
  break;
case 324:
#line 1996 "cs-parser.jay"
  {
	  /* Post Decr*/
	  /*TODO: extend*/
	  yyVal=new CodeBinaryOperatorExpression((CodeExpression) yyVals[-1+yyTop], CodeBinaryOperatorType.Subtract, 
	  new CodePrimitiveExpression(1));
	  }
  break;
case 327:
#line 2011 "cs-parser.jay"
  {
	  /*object creation expression*/
	  CodeExpressionCollection p= (CodeExpressionCollection) yyVals[-1+yyTop];
			CodeExpression[] pa= new CodeExpression[p.Count];
			p.CopyTo(pa,0);
	  yyVal=new CodeObjectCreateExpression((CodeTypeReference) yyVals[-3+yyTop], pa);
	  }
  break;
case 328:
#line 2024 "cs-parser.jay"
  {
	  /* Array creation*/
	  /* TODO: extend*/
	  
	  CodeArrayCreateExpression a= new CodeArrayCreateExpression();
			if (yyVals[-1+yyTop] != null && yyVals[-1+yyTop].ToString()!="")
				a.CreateType=CommonUtil.CreateArrayType((CodeTypeReference) yyVals[-5+yyTop],yyVals[-1+yyTop].ToString());
			
			else 
				a.CreateType= (CodeTypeReference) yyVals[-5+yyTop];

			a.CreateType= (CodeTypeReference) yyVals[-5+yyTop];

			CodeExpressionCollection c= (CodeExpressionCollection) yyVals[-3+yyTop];
			CodeExpressionCollection init =(CodeExpressionCollection) yyVals[0+yyTop];

			if (init!=null && init.Count>0) 
			{
				a.Initializers.AddRange(init);
			}
			
			if  (c!=null && c.Count>0) 
			{
				
				a.SizeExpression= c[0];
				if (c.Count>1)
				
					Report.Warning(9014,lexer.Location,"CodeDom supports only one dim array"); 
			}

           yyVal=a;
	  
	  }
  break;
case 329:
#line 2058 "cs-parser.jay"
  {
	  /* Array*/
	  /*TODO - extend*/
	  
	  CodeArrayCreateExpression a= new CodeArrayCreateExpression();
	  CodeTypeReference type=CommonUtil.CreateArrayType((CodeTypeReference) yyVals[-2+yyTop], yyVals[-1+yyTop].ToString());
	  if (type.ArrayRank>1 )
		  Report.Warning(9014,lexer.Location,"CodeDom supports only one dim array"); 
	  a.CreateType = type.ArrayElementType;
	  
	  
			CodeExpressionCollection init =(CodeExpressionCollection) yyVals[0+yyTop];

			if (init!=null && init.Count>0) 
			{
				a.Initializers.AddRange(init);
			}
			

           yyVal=a;
	  }
  break;
case 330:
#line 2080 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 331:
#line 2087 "cs-parser.jay"
  {
	  /* Empty rank specifier*/
	  yyVal = "";
	  }
  break;
case 332:
#line 2092 "cs-parser.jay"
  {
	  /* Rank specifier*/
	  yyVal = yyVals[0+yyTop];
	  }
  break;
case 333:
#line 2100 "cs-parser.jay"
  {
	  /* Rank specifier*/
	  yyVal = yyVals[0+yyTop];
	  }
  break;
case 334:
#line 2105 "cs-parser.jay"
  {
	  /* Rank specifier*/
	  yyVal = yyVals[-1+yyTop].ToString() + yyVals[0+yyTop].ToString();
	  }
  break;
case 335:
#line 2113 "cs-parser.jay"
  {
	  /* Rank specifier*/
	  yyVal="["+yyVals[-1+yyTop].ToString()+"]";
	  }
  break;
case 336:
#line 2121 "cs-parser.jay"
  {
	  /* Empty separators*/
	  yyVal="";
	  }
  break;
case 337:
#line 2126 "cs-parser.jay"
  {
	  /* Separators*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 338:
#line 2134 "cs-parser.jay"
  {
	  /* Comma*/
	  yyVal=",";
	  }
  break;
case 339:
#line 2139 "cs-parser.jay"
  {
	  /* Separators*/
	  yyVal=yyVals[-1+yyTop].ToString()+ ",";
	  }
  break;
case 340:
#line 2147 "cs-parser.jay"
  {
	  /* Replaced*/
	  yyVal=null;
	  }
  break;
case 341:
#line 2152 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 342:
#line 2159 "cs-parser.jay"
  {
	  /* Replaced*/
	  yyVal=null;
	  }
  break;
case 343:
#line 2164 "cs-parser.jay"
  {
	  /* Replaced*/
	  yyVal=yyVals[-2+yyTop];
	  }
  break;
case 344:
#line 2172 "cs-parser.jay"
  {
	  /* Initializer collection*/
	  CodeExpressionCollection c= new CodeExpressionCollection();
		c.Add((CodeExpression) yyVals[0+yyTop]);
		yyVal=c;
	  }
  break;
case 345:
#line 2179 "cs-parser.jay"
  {
	  /* Initializer collection*/
	  CodeExpressionCollection c= (CodeExpressionCollection) yyVals[-2+yyTop];
		c.Add((CodeExpression) yyVals[0+yyTop]);
		yyVal=c;
	  }
  break;
case 346:
#line 2189 "cs-parser.jay"
  {
	  /* typeof epxression*/
	  yyVal= new CodeTypeOfExpression((CodeTypeReference)yyVals[-1+yyTop]);
	  }
  break;
case 347:
#line 2195 "cs-parser.jay"
  {
	  /* typeof epxression*/
	  yyVal= new CodeTypeOfExpression(new CodeTypeReference("System.Void"));
	  }
  break;
case 348:
#line 2202 "cs-parser.jay"
  {
	  /* Sizeof exp*/
	  /*TODO:*/
	  }
  break;
case 349:
#line 2210 "cs-parser.jay"
  {
	  /* checked*/
	  /*TODO:*/
	  yyVal=yyVals[-1+yyTop];
	  }
  break;
case 350:
#line 2219 "cs-parser.jay"
  {
	  /* unchecked*/
	  /*TODO:*/
	  yyVal=yyVals[-1+yyTop];
	  
	  }
  break;
case 351:
#line 2229 "cs-parser.jay"
  {
	  /* Replaced*/
	  /*TODO:*/
	  }
  break;
case 353:
#line 2240 "cs-parser.jay"
  {
	  /* Negation*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 354:
#line 2245 "cs-parser.jay"
  {
	  /* Replaced*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 355:
#line 2257 "cs-parser.jay"
  {
	  /* Cast 1*/
	  /* TODO*/
	  yyVal=new CodeCastExpression(new CodeTypeReference("UknownType"), (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 356:
#line 2263 "cs-parser.jay"
  {
	  /*TODO*/
	  /* cast 2*/
	  yyVal=new CodeCastExpression(new CodeTypeReference("UknownType"), (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 358:
#line 2275 "cs-parser.jay"
  {
	  /* Replaced*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 359:
#line 2280 "cs-parser.jay"
  {
	  /* Replaced*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 360:
#line 2285 "cs-parser.jay"
  {
	  /* Replaced*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 361:
#line 2290 "cs-parser.jay"
  {
	  /* Replaced*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 362:
#line 2295 "cs-parser.jay"
  {
	  /* Replaced*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 363:
#line 2300 "cs-parser.jay"
  {
	  /* Replaced*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 364:
#line 2308 "cs-parser.jay"
  {
	  /*increment*/
	  /*TODO*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[0+yyTop], CodeBinaryOperatorType.Add, new CodePrimitiveExpression(1));
	  }
  break;
case 365:
#line 2317 "cs-parser.jay"
  {
	  /* decrement*/
	  /*TODO*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[0+yyTop], CodeBinaryOperatorType.Subtract, new CodePrimitiveExpression(1));
	  
	  }
  break;
case 367:
#line 2328 "cs-parser.jay"
  {
	  /* Multiple*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Multiply, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 368:
#line 2333 "cs-parser.jay"
  {
	  /* Divide*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Divide, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 369:
#line 2338 "cs-parser.jay"
  {
	  /* Modulo*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Modulus, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 371:
#line 2347 "cs-parser.jay"
  {
	  /* Add*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Add, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 372:
#line 2352 "cs-parser.jay"
  {
	  /* Subtract*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Subtract, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 374:
#line 2362 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 375:
#line 2366 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 377:
#line 2374 "cs-parser.jay"
  {
	  /* Less than*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.LessThan, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 378:
#line 2379 "cs-parser.jay"
  {
	   /* Greater than*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.GreaterThan, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 379:
#line 2384 "cs-parser.jay"
  {
	   /* Less or equal than*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.LessThanOrEqual, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 380:
#line 2389 "cs-parser.jay"
  {
	   /* Greater or equal than*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.GreaterThanOrEqual, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 381:
#line 2394 "cs-parser.jay"
  {
	  /* Replaced*/
	  /*TODO:*/
	  }
  break;
case 382:
#line 2399 "cs-parser.jay"
  {
	  /* TODO:*/
	  }
  break;
case 384:
#line 2407 "cs-parser.jay"
  {
	  
	   /* Equal*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.IdentityEquality, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 385:
#line 2413 "cs-parser.jay"
  {
	  /* Not Equal*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.IdentityInequality, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 387:
#line 2422 "cs-parser.jay"
  {
	  /* Bit or*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.BitwiseAnd, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 389:
#line 2431 "cs-parser.jay"
  {
	  /* Replaced*/
	  /*TODO:*/
	  }
  break;
case 391:
#line 2440 "cs-parser.jay"
  {
	  /* Bit or*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.BitwiseOr, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 393:
#line 2449 "cs-parser.jay"
  {
	  /* and*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.BooleanAnd, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 395:
#line 2458 "cs-parser.jay"
  {
	  /*  or*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.BooleanOr, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 397:
#line 2467 "cs-parser.jay"
  {
	  /* Replaced*/
	  /* TODO:*/
	  }
  break;
case 398:
#line 2475 "cs-parser.jay"
  {
	  /* Assign*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Assign, (CodeExpression) yyVals[0+yyTop]);
	  }
  break;
case 399:
#line 2480 "cs-parser.jay"
  {
	  /* Assign Multiple*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Assign, 
	  new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop],CodeBinaryOperatorType.Multiply,(CodeExpression) yyVals[0+yyTop]));
	  }
  break;
case 400:
#line 2486 "cs-parser.jay"
  {
	  /* Assign Divide*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Assign, 
	  new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop],CodeBinaryOperatorType.Divide,(CodeExpression) yyVals[0+yyTop]));
	  }
  break;
case 401:
#line 2492 "cs-parser.jay"
  {
	  /* Assign Modulo*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Assign, 
	  new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop],CodeBinaryOperatorType.Modulus,(CodeExpression) yyVals[0+yyTop]));
	  }
  break;
case 402:
#line 2498 "cs-parser.jay"
  {
	 /* Assign Add*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Assign, 
	  new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop],CodeBinaryOperatorType.Add,(CodeExpression) yyVals[0+yyTop]));
	  }
  break;
case 403:
#line 2504 "cs-parser.jay"
  {
	  /* Assign Subtract*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Assign, 
	  new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop],CodeBinaryOperatorType.Subtract,(CodeExpression) yyVals[0+yyTop]));
	  }
  break;
case 404:
#line 2510 "cs-parser.jay"
  {
	  /* Replaced*/
	  /*TODO*/
	  }
  break;
case 405:
#line 2515 "cs-parser.jay"
  {
	  /* Replaced*/
	  /*TODO*/
	  }
  break;
case 406:
#line 2520 "cs-parser.jay"
  {
	  /* Assign and*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Assign, 
	  new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop],CodeBinaryOperatorType.BitwiseAnd,(CodeExpression) yyVals[0+yyTop]));
	  }
  break;
case 407:
#line 2526 "cs-parser.jay"
  {
	  /* Assign or*/
	  yyVal= new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop], CodeBinaryOperatorType.Assign, 
	  new CodeBinaryOperatorExpression((CodeExpression) yyVals[-2+yyTop],CodeBinaryOperatorType.BitwiseOr,(CodeExpression) yyVals[0+yyTop]));
	  }
  break;
case 408:
#line 2532 "cs-parser.jay"
  {
	  /* Replaced*/
	  /*TODO*/
	  }
  break;
case 413:
#line 2559 "cs-parser.jay"
  {
	  /* add class to ns*/
	  bd.AddType(yyVals[-1+yyTop].ToString(),(ModifierAttribs)yyVals[-3+yyTop], Types.Class, 
	  ((CodeTypeReferenceCollection) yyVals[0+yyTop]), classScope>0, 
	  (CodeAttributeDeclarationCollection) yyVals[-4+yyTop]);
	  classScope++;
	  }
  break;
case 414:
#line 2567 "cs-parser.jay"
  {
	  /* Leaving class definition*/
	  classScope--;
	  bd.Up();
	  }
  break;
case 416:
#line 2576 "cs-parser.jay"
  {
	  yyVal =(ModifierAttribs) 0;
	  }
  break;
case 419:
#line 2585 "cs-parser.jay"
  {
	  /* Concat modifiers*/
	  if ((((int)yyVals[-1+yyTop]) & ((int) yyVals[0+yyTop])) != 0) {
			Location l = lexer.Location;
			Report.Error (1004, l, "Duplicate modifier: `" + yyVals[0+yyTop].ToString() + "'");
		}
		yyVal = (ModifierAttribs) (((ModifierAttribs) yyVals[-1+yyTop]) | ((ModifierAttribs) yyVals[0+yyTop]));
	  }
  break;
case 420:
#line 2596 "cs-parser.jay"
  {
	  /* New modifier*/
	  yyVal= ModifierAttribs.New;
	  }
  break;
case 421:
#line 2600 "cs-parser.jay"
  {
	  /* Public modifier*/
	  yyVal= ModifierAttribs.Public;
	  }
  break;
case 422:
#line 2604 "cs-parser.jay"
  {
	  /* Protected modifier*/
	  yyVal= ModifierAttribs.Protected;
	  }
  break;
case 423:
#line 2608 "cs-parser.jay"
  {
	  /* Internal modifier*/
	  yyVal= ModifierAttribs.Internal;
	  }
  break;
case 424:
#line 2612 "cs-parser.jay"
  {
	  /* Private modifier*/
	  yyVal= ModifierAttribs.Private;
	  }
  break;
case 425:
#line 2616 "cs-parser.jay"
  {
	  /* Abstract modifier*/
	  yyVal= ModifierAttribs.Abstract;
	  }
  break;
case 426:
#line 2620 "cs-parser.jay"
  {
	  /* Sealed modifier*/
	  yyVal= ModifierAttribs.Sealed;
	  }
  break;
case 427:
#line 2624 "cs-parser.jay"
  {
	  /* Static modifier*/
	  yyVal= ModifierAttribs.Static;
	  }
  break;
case 428:
#line 2628 "cs-parser.jay"
  {
	  /* Readonly modifier*/
	   Report.Warning(9012, lexer.Location, "Readonly modifier has no support in CODEDOM)");
	  yyVal= ModifierAttribs.Readonly;
	  }
  break;
case 429:
#line 2633 "cs-parser.jay"
  {
	  /* Virtual modifier*/
	  yyVal= ModifierAttribs.Virtual;
	  }
  break;
case 430:
#line 2637 "cs-parser.jay"
  {
	  /* Override modifier*/
	  yyVal= ModifierAttribs.Override;
	  }
  break;
case 431:
#line 2641 "cs-parser.jay"
  {
	  /* Extern modifier*/
	  yyVal= ModifierAttribs.Extern;
	  }
  break;
case 432:
#line 2645 "cs-parser.jay"
  {
	  /* Volatile modifier*/
	  Report.Warning(9012, lexer.Location, "Volatile modifier has no support in CODEDOM)");
	  yyVal= ModifierAttribs.Volatile;
	  }
  break;
case 433:
#line 2650 "cs-parser.jay"
  {
	  /* Unsafe modifier*/
	  Report.Warning(9012, lexer.Location, "Unsafe modifier has no support in CODEDOM)");
	  yyVal= ModifierAttribs.Unsafe;
	  }
  break;
case 434:
#line 2658 "cs-parser.jay"
  {
	  /* Null class base*/
	  yyVal=null;
	  }
  break;
case 435:
#line 2662 "cs-parser.jay"
  {
	  /* Class base*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 436:
#line 2670 "cs-parser.jay"
  {
	  /* Class base*/
	  yyVal=yyVals[0+yyTop];
	  }
  break;
case 437:
#line 2691 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 438:
#line 2695 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 443:
#line 2712 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 444:
#line 2716 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 459:
#line 2740 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 460:
#line 2747 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 462:
#line 2755 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 463:
#line 2760 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 464:
#line 2773 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 465:
#line 2777 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 466:
#line 2784 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 467:
#line 2788 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 468:
#line 2792 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 469:
#line 2796 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 470:
#line 2803 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 471:
#line 2807 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 472:
#line 2814 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 473:
#line 2821 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 474:
#line 2831 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 475:
#line 2834 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 476:
#line 2837 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 477:
#line 2840 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 478:
#line 2843 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 479:
#line 2846 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 480:
#line 2849 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 481:
#line 2852 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 482:
#line 2859 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 485:
#line 2872 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 486:
#line 2877 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 487:
#line 2884 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 488:
#line 2889 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 489:
#line 2898 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 490:
#line 2905 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 492:
#line 2913 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 493:
#line 2917 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 494:
#line 2924 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 495:
#line 2928 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 496:
#line 2935 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 497:
#line 2939 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 498:
#line 2945 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 499:
#line 2948 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 500:
#line 2951 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 505:
#line 2965 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 506:
#line 2973 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 507:
#line 2981 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 508:
#line 2987 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 509:
#line 2993 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 513:
#line 3005 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 515:
#line 3012 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 518:
#line 3024 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 519:
#line 3028 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 520:
#line 3035 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 521:
#line 3039 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 522:
#line 3043 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 528:
#line 3058 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 529:
#line 3065 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 530:
#line 3072 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 531:
#line 3076 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 532:
#line 3080 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 533:
#line 3087 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 534:
#line 3094 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 537:
#line 3106 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 538:
#line 3110 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 539:
#line 3114 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 540:
#line 3120 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 542:
#line 3128 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 543:
#line 3132 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 544:
#line 3138 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 546:
#line 3146 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 547:
#line 3149 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 548:
#line 3155 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 550:
#line 3163 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 551:
#line 3170 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 552:
#line 3177 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 553:
#line 3184 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 554:
#line 3187 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 555:
#line 3196 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 556:
#line 3200 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 557:
#line 3206 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 558:
#line 3210 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 559:
#line 3217 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 560:
#line 3224 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 561:
#line 3228 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 562:
#line 3235 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
case 563:
#line 3239 "cs-parser.jay"
  {
	  /* Replaced*/
	  }
  break;
#line 3748 "-"
        }
        yyTop -= yyLen[yyN];
        yyState = yyStates[yyTop];
        int yyM = yyLhs[yyN];
        if (yyState == 0 && yyM == 0) {
          if (debug != null) debug.shift(0, yyFinal);
          yyState = yyFinal;
          if (yyToken < 0) {
            yyToken = yyLex.advance() ? yyLex.token() : 0;
            if (debug != null)
               debug.lex(yyState, yyToken,yyname(yyToken), yyLex.value());
          }
          if (yyToken == 0) {
            if (debug != null) debug.accept(yyVal);
            return yyVal;
          }
          goto yyLoop;
        }
        if (((yyN = yyGindex[yyM]) != 0) && ((yyN += yyState) >= 0)
            && (yyN < yyTable.Length) && (yyCheck[yyN] == yyState))
          yyState = yyTable[yyN];
        else
          yyState = yyDgoto[yyM];
        if (debug != null) debug.shift(yyStates[yyTop], yyState);
	 goto yyLoop;
      }
    }
  }

   static  short [] yyLhs  = {              -1,
    0,    4,    4,    5,    5,    6,    6,    7,    8,   14,
   11,   15,   15,   16,   16,   12,   12,   10,   13,    1,
    1,    3,    3,   17,   17,   18,   18,   19,   19,   19,
   19,   19,    2,    2,   25,   25,   26,   28,   28,   28,
   28,   27,   27,   30,   30,   29,   31,   32,   32,   34,
   34,   35,   35,   35,   36,   36,   37,   37,   39,   40,
   41,   41,   42,   42,   43,   43,   43,   43,   43,   43,
   43,   43,   43,   43,   56,   57,   21,   54,   54,   58,
   55,   60,   60,   61,   61,   62,   62,   62,   62,   62,
   62,   62,   62,   62,   62,   44,   64,   64,   65,   45,
   67,   67,   68,   68,   69,   69,   69,   74,   46,   75,
   46,   73,   73,   72,   72,   77,   77,   77,   78,   78,
   80,   81,   81,   82,   82,   79,   71,   83,   85,   47,
   84,   84,   89,   89,   87,   87,   91,   86,   92,   88,
   90,   90,   95,   96,   22,   93,   93,   97,   59,   59,
   94,   99,   99,  100,  100,  101,  101,  101,  101,  106,
  106,  102,  102,  108,  109,  103,  107,  107,  107,  107,
  104,  110,  111,  105,   50,  112,  112,  112,  113,  113,
  113,  113,  113,  113,  113,  113,  113,  113,  113,  113,
  113,  113,  113,  113,  113,  113,  113,  113,  113,  113,
  114,  114,  114,  114,  116,   51,  115,  117,  117,  118,
  118,   52,   48,  121,  122,   48,  120,  120,  125,  123,
  126,  124,  128,  129,   49,  127,  127,  132,  133,   23,
  130,  130,  131,  134,  134,  135,  135,  136,  136,   24,
   24,   33,    9,   63,   63,   63,   63,  139,  139,  140,
  140,  140,  140,  140,  143,  143,  137,  137,  137,  137,
  137,  137,  137,  144,  144,  144,  144,  144,  144,  144,
  144,  144,   98,  138,  145,  145,  145,  145,  145,  145,
  145,  145,  145,  145,  145,  145,  145,  145,  145,  145,
  146,  146,  146,  146,  146,  146,  163,  163,  163,  162,
  161,  161,  147,  148,  148,  164,  149,  119,  119,  165,
  165,  166,  166,  166,  167,  150,  150,  168,  168,  151,
  152,  152,  153,  154,  155,  155,  169,  170,  170,  170,
  171,  171,  142,  142,  141,  173,  173,  174,  174,  172,
  172,   70,   70,  175,  175,  156,  156,  157,  158,  159,
  160,  176,  176,  176,  176,  176,  177,  177,  177,  177,
  177,  177,  177,  178,  179,  180,  180,  180,  180,  181,
  181,  181,  182,  182,  182,  183,  183,  183,  183,  183,
  183,  183,  184,  184,  184,  185,  185,  186,  186,  187,
  187,  188,  188,  189,  189,  190,  190,  191,  191,  191,
  191,  191,  191,  191,  191,  191,  191,  191,   38,   38,
   66,  192,  194,  195,   20,   53,   53,  196,  196,  197,
  197,  197,  197,  197,  197,  197,  197,  197,  197,  197,
  197,  197,  197,  193,  193,  198,  200,   76,  199,  199,
  201,  201,  202,  202,  202,  204,  204,  204,  204,  204,
  204,  204,  204,  204,  204,  204,  204,  204,  206,  218,
  205,  203,  203,  221,  221,  222,  222,  222,  222,  219,
  219,  220,  207,  223,  223,  223,  223,  223,  223,  223,
  223,  224,  208,  208,  225,  225,  227,  226,  228,  229,
  229,  230,  230,  233,  231,  232,  232,  234,  234,  234,
  209,  209,  209,  209,  235,  236,  241,  237,  239,  239,
  243,  243,  240,  240,  242,  242,  245,  244,  244,  246,
  247,  238,  210,  210,  210,  210,  210,  248,  249,  250,
  250,  250,  251,  252,  253,  253,  211,  211,  211,  255,
  255,  254,  254,  257,  257,  259,  256,  258,  258,  260,
  212,  213,  261,  216,  263,  217,  262,  262,  264,  265,
  214,  267,  215,  266,  266,
  };
   static  short [] yyLen = {           2,
    4,    0,    1,    1,    2,    1,    1,    5,    3,    0,
    5,    0,    1,    0,    1,    1,    3,    1,    4,    0,
    1,    0,    1,    1,    2,    1,    1,    1,    1,    1,
    1,    1,    0,    2,    4,    3,    2,    1,    1,    1,
    1,    1,    3,    0,    1,    2,    1,    0,    3,    0,
    1,    1,    3,    1,    1,    3,    1,    3,    3,    3,
    0,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    0,    0,    9,    0,    1,    2,
    3,    0,    1,    1,    2,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    6,    1,    3,    3,    5,
    1,    3,    3,    1,    1,    1,    5,    0,    9,    0,
    9,    1,    1,    0,    1,    1,    3,    1,    1,    3,
    4,    0,    1,    1,    1,    4,    1,    0,    0,    9,
    2,    2,    0,    1,    0,    1,    0,    4,    0,    4,
    1,    1,    0,    0,    9,    0,    1,    2,    1,    3,
    3,    0,    1,    1,    2,    1,    1,    1,    1,    0,
    1,    8,    8,    0,    0,    9,    3,    3,    6,    6,
    6,    0,    0,   12,    4,    7,   10,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    7,    7,    2,    2,    0,    5,    5,    0,    1,    5,
    5,    6,    6,    0,    0,   10,    2,    2,    0,    4,
    0,    4,    0,    0,    8,    5,    7,    0,    0,    9,
    0,    2,    3,    0,    2,    1,    3,    2,    4,    9,
    9,    1,    1,    1,    1,    1,    1,    2,    2,    1,
    2,    2,    2,    2,    1,    3,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    2,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    3,    3,    3,    1,    4,    0,    1,    1,
    3,    1,    2,    2,    1,    4,    2,    1,    3,    1,
    3,    4,    2,    2,    1,    1,    5,    7,    4,    3,
    0,    1,    1,    2,    3,    0,    1,    1,    2,    0,
    1,    2,    4,    1,    3,    4,    4,    4,    4,    4,
    3,    1,    2,    2,    4,    4,    1,    2,    2,    2,
    2,    2,    2,    2,    2,    1,    3,    3,    3,    1,
    3,    3,    1,    3,    3,    1,    3,    3,    3,    3,
    3,    3,    1,    3,    3,    1,    3,    1,    3,    1,
    3,    1,    3,    1,    3,    1,    5,    3,    3,    3,
    3,    3,    3,    3,    3,    3,    3,    3,    1,    1,
    1,    1,    0,    0,    9,    0,    1,    1,    2,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    0,    1,    2,    0,    4,    0,    1,
    1,    2,    1,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    1,    1,    1,    1,    1,    0,
    4,    2,    2,    2,    2,    2,    2,    2,    2,    2,
    3,    3,    2,    1,    1,    1,    1,    1,    1,    1,
    1,    1,    1,    1,    5,    7,    0,    6,    3,    0,
    1,    1,    2,    0,    3,    1,    2,    3,    2,    1,
    1,    1,    1,    1,    5,    7,    0,   10,    0,    1,
    1,    1,    0,    1,    0,    1,    1,    1,    3,    0,
    0,   10,    1,    1,    1,    1,    1,    2,    2,    3,
    4,    3,    3,    3,    0,    1,    3,    5,    3,    0,
    1,    1,    2,    0,    1,    0,    4,    0,    1,    4,
    2,    2,    0,    3,    0,    7,    1,    3,    3,    0,
    6,    0,    6,    1,    1,
  };
   static  short [] yyDefRed = {            0,
    0,    0,    0,    0,    4,    6,    7,    0,   18,    0,
    0,    0,    0,    0,    5,    0,    9,    0,   41,   39,
   40,    0,  242,    0,    0,    0,   42,    0,   47,    0,
    0,    0,   27,    0,   24,   26,   28,   29,   30,   31,
   32,   34,   16,    0,   17,    0,   36,    0,   37,    0,
   46,    0,  425,  431,  423,  420,  430,  424,  422,  421,
  428,  426,  427,  433,  429,  432,    0,    0,  418,    3,
    1,   25,    8,   35,   45,   43,    0,  259,  265,  272,
    0,  260,  262,  302,  261,  268,  270,    0,  296,  257,
  264,  266,    0,  258,  320,  301,    0,  269,  271,    0,
  267,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  300,  297,  298,  299,  294,  295,    0,    0,    0,   51,
    0,    0,   55,   57,  306,  263,    0,  275,  277,  278,
  279,  280,  281,  282,  283,  284,  285,  286,  287,  288,
  289,  290,  291,  292,  293,    0,  325,  326,  357,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  409,  410,    0,    0,    0,    0,    0,    0,  419,    0,
    0,    0,    0,  244,    0,  245,  246,  247,    0,    0,
    0,    0,    0,    0,  354,  358,  359,  353,  363,  362,
  360,  361,    0,   49,    0,    0,    0,    0,    0,  323,
  324,    0,  333,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  318,    0,  321,    0,  249,
  330,    0,    0,  248,    0,    0,    0,    0,    0,    0,
    0,  254,    0,    0,  252,  251,   59,    0,   56,    0,
   58,  338,    0,    0,    0,    0,    0,  312,    0,    0,
  310,  304,  351,  334,  305,  398,  399,  400,  401,  402,
  403,  404,  405,  406,  408,  407,  367,  369,  368,  366,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   13,
   11,    0,  413,  435,    0,    0,    0,    0,  228,    0,
  143,  147,    0,   75,   79,  322,    0,  349,    0,    0,
    0,  329,  348,  347,  346,  350,  355,  356,  316,  335,
  339,  315,  314,  313,  307,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  273,    0,  149,    0,    0,
    0,  319,    0,  327,    0,  342,  105,  344,  106,    0,
  311,  397,   19,    0,    0,  414,    0,    0,  115,    0,
  118,  119,    0,    0,  229,    0,    0,  144,    0,   76,
    0,    0,    0,    0,    0,    0,    0,   74,    0,    0,
   63,   65,   66,   67,   68,   69,   70,   71,   72,   73,
    0,  125,    0,  124,    0,  123,    0,    0,    0,    0,
    0,    0,  236,    0,  150,    0,    0,    0,  154,  156,
  157,  158,  159,    0,   94,   86,   87,   88,   89,   90,
   91,   92,   93,   95,    0,    0,   84,    0,  341,  328,
    0,  345,  343,    0,    0,   60,   64,  415,    0,    0,
  241,  117,  120,  240,    0,  233,    0,  235,  230,  161,
    0,  151,  155,  145,   81,   85,   77,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  178,  205,    0,
  126,  121,    0,  237,    0,    0,    0,  107,    0,    0,
    0,  204,    0,  203,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  101,    0,  437,  175,    0,  223,  239,
    0,    0,    0,    0,    0,    0,    0,   97,    0,    0,
    0,    0,    0,    0,  184,  183,  180,  185,  186,  179,
  198,  197,  190,  191,  187,  189,  188,  192,  181,  182,
  193,  194,  200,  199,  195,  196,    0,    0,    0,    0,
    0,  100,    0,    0,    0,  206,    0,    0,    0,    0,
  164,    0,  212,    0,    0,   96,  213,  214,    0,    0,
    0,    0,    0,    0,  103,    0,    0,  102,    0,    0,
  481,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  553,    0,    0,
    0,  459,    0,    0,    0,  446,    0,    0,    0,    0,
    0,    0,    0,  479,  480,  476,    0,    0,  441,  443,
  444,  445,  447,  448,  449,  450,  451,  452,  453,  454,
  455,  456,  457,  458,    0,    0,    0,    0,    0,  475,
  483,  484,  501,  502,  503,  504,  523,  524,  525,  526,
  527,    0,  224,    0,    0,  171,    0,    0,    0,    0,
  411,   99,   98,    0,    0,    0,  110,    0,  207,  209,
    0,  226,    0,  108,  129,  528,  551,    0,    0,    0,
    0,  529,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  536,    0,  487,    0,    0,  552,    0,    0,  468,
    0,    0,    0,  460,  467,  465,  466,    0,  464,  438,
  442,  462,  463,    0,  469,    0,  473,  137,  139,    0,
    0,  131,  136,    0,  134,  132,    0,    0,    0,  165,
    0,    0,  215,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  472,    0,    0,    0,  511,
  518,    0,  510,    0,    0,    0,  532,  530,  412,    0,
    0,  533,    0,  534,  539,    0,    0,    0,  542,  554,
  565,  564,    0,    0,    0,    0,    0,    0,  225,  163,
  172,    0,    0,    0,  162,  219,  221,    0,    0,  217,
    0,  218,  202,  201,  113,  111,  112,    0,    0,  176,
    0,  227,  109,  130,  303,    0,    0,    0,  557,  507,
    0,    0,  531,    0,  560,    0,    0,  546,  549,  543,
    0,  562,    0,  461,  142,  141,  138,  140,    0,    0,
    0,  166,    0,    0,  216,    0,    0,    0,    0,    0,
  555,    0,    0,  519,  520,    0,    0,    0,    0,    0,
  538,    0,  505,  173,    0,    0,  220,  222,  210,  211,
    0,    0,  559,    0,  558,  514,    0,    0,    0,  561,
    0,  488,  545,    0,  547,  563,    0,    0,    0,  177,
  506,  556,    0,    0,  486,  500,    0,    0,    0,    0,
  492,    0,  496,  550,  174,  169,  170,    0,    0,  516,
  521,    0,  499,  489,  493,    0,  497,    0,    0,  498,
    0,  508,  522,
  };
  protected static  short [] yyDgoto  = {             2,
    3,  387,   32,   71,    4,    5,    6,    7,   23,   10,
   33,  118,  239,  163,  321,  405,   34,   35,   36,   37,
   38,   39,   40,   41,   14,   24,   25,   26,   27,   76,
   28,   51,  174,  119,  120,  121,  122,  278,  124,  386,
  409,  410,  411,  412,  413,  414,  415,  416,  417,  418,
  419,  420,   67,  334,  400,  371,  458,  335,  367,  455,
  456,  457,  175,  537,  538,  682,  523,  524,  378,  379,
  517,  388,  816,  763,  758,  626,  389,  390,  391,  392,
  425,  426,  574,  673,  764,  674,  742,  675,  746,  847,
  797,  798,  331,  398,  369,  444,  332,  368,  437,  438,
  439,  440,  441,  442,  443,  481,  750,  679,  804,  849,
  897,  497,  567,  498,  499,  528,  689,  690,  279,  753,
  684,  808,  754,  755,  853,  854,  500,  577,  740,  329,
  395,  365,  434,  431,  432,  433,  125,  177,  178,  184,
  203,  327,  360,  126,  127,  128,  129,  130,  131,  132,
  133,  134,  135,  136,  137,  138,  139,  140,  141,  142,
  143,  144,  145,  146,  280,  281,  353,  247,  147,  148,
  726,  460,  274,  275,  380,  149,  150,  634,  635,  151,
  152,  153,  154,  155,  156,  157,  158,  159,  160,  161,
  162,  780,  323,  361,  421,   68,   69,  324,  637,  575,
  638,  639,  640,  641,  642,  643,  644,  645,  646,  647,
  648,  649,  650,  651,  652,  653,  654,  795,  655,  656,
  657,  658,  659,  660,  661,  662,  783,  892,  909,  910,
  911,  912,  926,  913,  663,  664,  665,  666,  772,  887,
  863,  918,  773,  774,  920,  888,  929,  667,  668,  669,
  670,  671,  713,  787,  788,  789,  894,  838,  870,  839,
  718,  828,  884,  829,  867,  793,  872,
  };
  protected static  short [] yySindex = {         -211,
 -375,    0, -233, -211,    0,    0,    0, -207,    0, -134,
 -115, -222,  -65, -233,    0, -246,    0, -167,    0,    0,
    0,    0,    0, -246, -100, -145,    0, -103,    0, -246,
 5760,   39,    0,  -65,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  -31,    0,   -6,    0, -246,    0, 3812,
    0, -115,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,   89, 5760,    0,    0,
    0,    0,    0,    0,    0,    0,  102,    0,    0,    0,
   46,    0,    0,    0,    0,    0,    0, 4736,    0,    0,
    0,    0,   67,    0,    0,    0,  103,    0,    0,  110,
    0, 3948, 3948, 3948, 3948, 3948, 3948, 3948, 3948, 3948,
    0,    0,    0,    0,    0,    0,   48, -115,   86,    0,
  117,  128,    0,    0,    0,    0,   -8,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  135,    0,    0,    0,  154,
  151,  215, -284, -243,   79,  161,  130,  131,  115, -288,
    0,    0,  181,  108, 4774,  116,  121,  167,    0, 3948,
  186, 3948,  249,    0, -237,    0,    0,    0, 4736, 4818,
 3948, -106,    0,   53,    0,    0,    0,    0,    0,    0,
    0,    0, 3948,    0, 3812,  200, 3540, 3268,  203,    0,
    0,  216,    0,  313,  224, 3948, 3948, 3948, 3948, 3948,
 3948, 3948, 3948, 3948, 3948, 3948, 3948, 3948, 3948, 3948,
 3948, 3948, 3948, 4736, 4736, 3948, 3948, 3948, 3948, 3948,
 3948, 3948, 3948, 3948, 3948, 3948, 3948, -211,  315,  318,
 -320, -245,  322,  323,  324,    0,  122,    0,  335,    0,
    0, 3540, 3268,    0,  218,   58,  -30,  138,  336,  339,
 4084,    0,  313, 3948,    0,    0,    0,  128,    0,   48,
    0,    0,  158,  340,  342, 3948, 3948,    0,  346,  345,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  151,  151,  215,  215, -105, -105, -284, -284, -284, -284,
 -243, -243,   79,  161,  130,  131,  347,  115,  -65,    0,
    0, 4736,    0,    0,  343,  349,  313, 4736,    0, -246,
    0,    0, -246,    0,    0,    0, 3948,    0,  176,  351,
 3132,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 3268, 3948,  353, -105,  350,
  358, -233, -233, -105,  360,    0,  354,    0,  362,  354,
  363,    0,  313,    0, 4736,    0,    0,    0,    0,  357,
    0,    0,    0, 4736, -233,    0,  175,  361,    0,  364,
    0,    0,  365, -233,    0, -246, -233,    0, -233,    0,
  313,  368,  -58, 3404,  371, -105,  460,    0,  372, -233,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  315,    0, 4736,    0, 4736,    0,  366, -233,  373,  296,
  376,  377,    0,  315,    0,  425,  379, -233,    0,    0,
    0,    0,    0,  315,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  384, -233,    0,  315,    0,    0,
 3540,    0,    0,  308, 4256,    0,    0,    0, -221, -143,
    0,    0,    0,    0,  378,    0, -233,    0,    0,    0,
 4660,    0,    0,    0,    0,    0,    0,  385,  383, 4736,
 4736, -206, -159, -318,  387, -283,  393,    0,    0,  396,
    0,    0, 3948,    0, 4736, -282, -290,    0,  392, -140,
 -133,    0, 4736,    0, 4736, -115,  397, -233, 1555,  400,
  389,  398,  253,    0,  402,    0,    0,  393,    0,    0,
 -130,  403,  408,  191,  393,  401,  254,    0,  267,  411,
 -263,  -63, -233,  412,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  416, -233, 3404, -299,
  338,    0, -233,  424, 1535,    0, -233,  413, -233, -233,
    0, -233,    0, 3948,  352,    0,    0,    0, 4736, 4736,
  429,  421, 4736,  432,    0,  436,  389,    0,  434, -233,
    0,  431,  197, 4155,  433, 1694,  437,  441,  442, -253,
  449,  451, 3948,  452, 3948,  393,  205,    0,  453,  439,
  455,    0, 3948, 3948,  454,    0,  -42,  132,    0,    0,
    0,    0,  154,    0,    0,    0,  462, 1535,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  456,  458,  338,   13,  459,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  295,    0, -233, -233,    0,  466,  461, -233,  467,
    0,    0,    0, -233, -125, -104,    0, -215,    0,    0,
  -98,    0, -233,    0,    0,    0,    0, 3948,  313,   -8,
  352,    0,  479, 4736, 1830, 4736, 3948,  468,  470, 3948,
 3948,    0,  471,    0,  472, -174,    0,  393, 3676,    0,
 3948,    0,    0,    0,    0,    0,    0,  313,    0,    0,
    0,    0,    0,  475,    0,  338,    0,    0,    0,  482,
  463,    0,    0,  489,    0,    0,  477,  488,  302,    0,
  481, -185,    0, -233, -233,  486,  487,   -7,  491,  492,
  294,  490,   -7,  496,  493,    0,  495, -105,  430,    0,
    0,  498,    0,  505,  -97,  508,    0,    0,    0,  497,
  509,    0, 3948,    0,    0,  515,  575,  579,    0,    0,
    0,    0,  518,  519, 1535,  475,    4,    4,    0,    0,
    0,  516,  517,  526,    0,    0,    0,  527,  558,    0,
  612,    0,    0,    0,    0,    0,    0, 3268, 3268,    0,
 4736,    0,    0,    0,    0, 3948,  520,  300,    0,    0,
 1966,  581,    0, 1694,    0,  528, 4736,    0,    0,    0,
  393,    0, 1694,    0,    0,    0,    0,    0, -233, -233,
 -233,    0,  393,  393,    0,  530,  531,  -92,  532, 3948,
    0,  430, 3948,    0,    0,  596, 1694,  538,  -83,  393,
    0, 1694,    0,    0,  542,  545,    0,    0,    0,    0,
  546,  547,    0, 1694,    0,    0,  553, 3948, 1694,    0,
  -59,    0,    0,  562,    0,    0,  567,  560,  563,    0,
    0,    0, 1966,  566,    0,    0, 3948,  565,  573,  -59,
    0,  -59,    0,    0,    0,    0,    0,  571,  505,    0,
    0,  572,    0,    0,    0, 1535,    0, 1694, 1694,    0,
 1535,    0,    0,
  };
  protected static  short [] yyRindex = {          989,
    0,    0, 1094,  860,    0,    0,    0,   29,    0,    0,
 2001,    0, 1142,  626,    0,    0,    0,    0,    0,    0,
    0,  240,    0,    0,    0,    0,    0,  208,    0,    0,
  124,  925,    0, 1032,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  210,    0,  591,
    0,  601,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 4324,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0, 4999, 2623,    0,    0,
  597,  602,    0,    0,    0,    0, 5070,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0, 5141,
 5184, 5315, 5445, 2827, 3281, 3310, 1927,  763,  264,   93,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  -48,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  603,  604,    0,    0,
    0,    0,    0, 4855,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 5730,  931,  608,
    0,    0,  610,  611,  614,    0,    0,    0,    0,    0,
    0,  603,  604,    0, -219,    0,    0,    0,    0,  603,
 4928,    0,  -14,    0,    0,    0,    0,  616,    0,    0,
    0,    0,    0,    0,  620,    0,    0,    0,    0,  622,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 5228, 5271, 5358, 5402, 1239, 1398, 5489, 5532, 5576, 5619,
 3009, 3145, 3417, 3446, 3365, 1230,    0, 1389,  153,    0,
    0,    0,    0,    0,    0,    0, 2872,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  -67,  625,
    0, 4432, 4432,  628,    0,    0,  629,    0,    0,  630,
    0,    0, 2386,    0,    0,    0,    0,    0,    0,  631,
    0,    0,    0,    0, 2129,    0, 4850,    0,    0,  309,
    0,    0,    0, -328,    0,    0, 4522,    0, 2212,    0,
 2465, 2544,    0,  632,    0,   47, 4392,    0,    0, 2295,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  743,    0,    0,    0,    0,    0,    0, 4562,    0,    0,
    0,  631,    0,  743,    0, 4692,    0, 4616,    0,    0,
    0,    0,    0,  743,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0, 2378,    0,  743,    0,    0,
  603,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, -178,    0, -301,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0, -274,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  232,    0, 4432,    0,    0,
  160,  232,    0,    0,  634,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0, 4432,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0, 4472,    0,    0,
    0,    0, 4432,    0,  636,    0,  325,    0, 4432, 4562,
    0, 4432,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  635,    0,    0,    0,    0,  141,    0,    0,  325,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  637,    0,  637,    0,    0,    0,    0,    0,
    0,    0,    0,    0, 5888,    0, -310, 2765, 5734, 5757,
 5818, 5841,    0,    0,    0,    0,    0,  638,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  569,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    2,   87,    0,    0,    0,  325,    0,
    0,    0,    0, -166,    0,    0,    0,    0,    0,    0,
    0,    0, 4472,    0,    0,    0,    0,    0, -310,  569,
    0,    0,    0,    0,  639,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  689,    0,    0,    0,    0,
    0, 1681, 1817,    0,    0,    0,    0, 2694,    0,    0,
    0,    0,    0,  228,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  667,  722,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,   25,    0,
    0,    0,    0,  640,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  641, 1217,    0,    0,    0,
    0,    0,    0,    0,    0,  244,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  604,  604,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  325,   72,
  127,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  642,    0,    0, 1376,    0,    0,  645,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  643,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,  648,    0,    0,    0,    0,    0,    0,  653,
    0, 2996,    0,    0,    0,    0,    0,    0,  650,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
 -240,    0,    0,
  };
  protected static  short [] yyGindex = {            0,
  749,   -2,  682,    0,    0,  998,    0,    0,  277,    0,
    0,  198,    0,    0, -108,  574,    0,  970, -146,    0,
    0,    0,    0,    0,    0,    0,  981,    0,  959,    0,
    0,    0,  -10,    0,    0,    0,  814,  -50,  815,    0,
    0,    0,  607, -365, -341, -337, -315, -225, -224, -188,
 -184, -181,  605,    0,    0,    0,    0,    0,  681,    0,
    0,  577,  149,    0, -564, -703, -502,  450, -384, -230,
 -437, -306,  260,    0,    0, -489,  448,    0,  606,  609,
    0,    0,    0,  446,    0,  355,    0,  367,    0,  231,
    0,    0,    0,    0,    0,    0,    0,  646,    0,    0,
  618,    0,    0,    0,    0,    0,  209,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, -250,    0,
    0,    0,  307,  303,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  586,  -72,    0,  380,    0,
 -177, -121,    0,    0, -551,    0,    0,    0, -500,    0,
    0,    0, -248, -109,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  693,  790, -149,  256,    0,
 -368,    0,    0,    0,    0,  807,  -39,    0,    0,  148,
  406,  374,  422,  837,  838,  836,  840,  835,    0,    0,
  291, -692,    0,    0,    0,    0, 1010,    0,    0,    0,
  156, -625,    0, -573,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0, -295,    0,
  494,    0, -695,    0,    0,    0,    0,    0,    0,    0,
  173,    0,    0,  179,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  182,    0,    0,    0,    0,    0,    0,
    0,    0,  478,    0,    0,  305,    0,    0,    0,    0,
    0,    0,    0,  233,    0,    0,    0,
  };
  protected static  short [] yyTable = {           123,
   13,   29,  340,  776,  402,  204,  266,  527,  539,  771,
   31,   42,  731,   29,  707,  176,  234,  224,  251,  462,
  683,  519,  708,  628,  342,  596,  284,  495,  794,  183,
   16,   31,  703,  446,  533,  495,  274,   29,  576,  306,
   19,  520,  250,   15,  250,  583,    8,  273,  759,  512,
   16,  182,  700,  255,  225,  260,  393,  447,  525,   20,
  263,  448,  260,  185,  186,  187,  188,  189,  190,  191,
  192,   16,  254,  540,  629,   16,  806,  284,  236,  254,
  250,  785,  260,  449,  589,  284,  628,  222,   16,  223,
  446,   21,  176,   33,  786,   33,  514,  237,  513,  254,
  260,  325,  339,   43,  495,  629,  176,  176,  252,  760,
  253,  331,   12,  697,  447,  226,  227,  254,  448,  246,
   33,  249,   45,    1,  260,  254,  716,  717,  274,  807,
  259,  534,  228,  859,  229,  864,  766,  629,  521,  532,
  449,  254,  267,  274,  269,  515,  246,   16,   33,  284,
   16,  176,  176,  628,  734,  286,  287,  288,  289,  290,
  291,  292,  293,  294,  295,  296,  238,  628,  709,  844,
  886,  459,  238,  450,  451,   43,  326,  297,  298,  299,
  300,  300,  300,  300,  595,  317,  300,  300,  300,  300,
  300,  300,  300,  300,  300,  300,  906,  300,   11,   22,
  501,  246,  260,  922,  629,  260,   49,  771,  907,   11,
  452,  544,  260,   11,  453,  260,  908,  454,   17,  254,
  260,   11,  254,  284,  348,  352,  352,   52,  790,  254,
  450,  451,  254,  796,   18,   30,  591,  254,  408,  260,
  260,  260,  261,  628,   50,   11,   47,  260,  260,  176,
   48,  401,  445,  260,   45,  176,  262,  254,  254,  729,
  866,  594,  260,  408,  254,  254,  599,  452,  817,  873,
  254,  453,  677,  817,  454,  680,  255,    9,  502,  254,
   12,  536,  260,  255,  590,   11,  372,  461,  521,  736,
  377,  578,   44,  890,  629,   70,  756,  250,  896,  254,
  250,  306,  176,  260,  254,  731,  382,  846,  846,  445,
  902,  176,  468,  242,  250,  905,   31,  757,  344,  366,
  725,   73,  366,  761,  832,  479,  630,  256,  258,  881,
  629,  729,  250,  629,  253,  484,  526,  197,  893,  198,
   74,  199,  629,   33,   48,  815,  135,  526,  253,  487,
  176,  871,  176,  377,  932,  933,  845,  630,  260,  200,
  164,  201,   11,  877,  878,  165,  629,  301,  302,  166,
  247,  629,  305,  306,  628,  735,   11,   11,   16,  628,
  895,   16,  407,  629,  167,  366,  762,  247,  629,  630,
  256,  430,  176,  172,  436,  416,  407,  256,  260,  202,
  416,  264,  629,  260,  416,  193,  343,  407,  176,  770,
  488,  168,   33,   33,  179,  265,  167,  176,  176,  416,
  254,   11,   11,  792,   33,  629,   33,  629,  629,   33,
  629,  133,  176,   33,  194,  436,   33,  396,  396,  396,
  176,  396,  176,  396,  396,  396,  416,  170,   33,   33,
  180,  171,  530,  407,   33,  396,  630,  181,  230,   33,
  231,   33,   33,   33,   33,  631,   33,  195,  336,   33,
  359,  168,  337,   33,  430,   33,  364,  197,  196,  198,
  422,  199,  423,  260,  205,   33,  345,  424,   33,  104,
   33,  104,  234,  104,  727,  233,  631,   22,  235,  200,
  254,  201,  627,   16,  349,  401,  728,   16,  337,   16,
  104,  206,  104,  217,  218,  219,  176,  176,  377,   11,
  176,  232,  373,  403,  238,   11,  337,   11,  631,  240,
   11,  699,  406,  681,  581,  633,  401,  243,  582,  202,
  526,  207,  244,  208,  172,  209,  630,  210,  526,  211,
  284,  212,  181,  213,   48,  214,   44,  215,   48,  216,
   44,  341,  712,  260,  712,  627,  633,  856,  857,  220,
  221,  469,   11,  470,  672,  127,  470,  401,  728,  127,
  470,   11,  630,  722,  723,  630,   16,   16,  245,   16,
   16,   38,  471,   11,  630,  631,  471,  672,  633,  307,
  308,  309,  310,  571,  585,  572,  586,  248,  394,  394,
  394,  250,  394,  496,  394,  394,  394,  571,  630,  587,
   11,  270,   11,  630,  282,   33,  394,  303,  304,  507,
  394,  176,  627,  176,  738,  630,  739,  283,  510,  511,
  630,  802,  820,  803,  821,  285,  627,  765,  861,  394,
  862,  311,  312,  531,  630,  116,  681,  116,  260,  779,
  781,  541,   11,  542,   33,  633,   33,  320,  791,  322,
  779,  741,  744,  328,  330,  333,  749,  630,   11,  630,
  630,  752,  630,  338,  346,  631,  350,   11,   11,  272,
  362,  516,  351,  522,  355,  356,  363,  383,  357,  374,
  384,  385,   11,  394,  396,  397,  399,  404,  516,  427,
   11,  341,   11,  429,  428,  463,  466,  475,  471,   53,
  476,  631,  627,  482,  631,  474,  480,  477,  485,  489,
  509,  508,  836,  631,  518,  503,  526,  685,  686,  529,
  535,  691,   12,   54,  543,  568,  569,  570,  176,  573,
  579,  809,  811,  580,  588,  633,   55,  631,  584,  597,
  592,   56,  631,  593,  176,  676,   57,  600,   58,   59,
   60,   61,  688,  536,  631,  779,   62,  687,  692,  631,
   63,  693,  694,  696,  704,  702,   11,   11,  705,  706,
   11,  633,   64,  631,  633,   65,  710,   66,  711,  714,
  719,  720,  721,  633,  739,  724,  730,  748,  732,  883,
  733,  737,  779,  464,  747,  751,  631,  767,  631,  631,
  777,  631,  778,  782,  784,  571,  799,  633,  738,  800,
  632,  801,  633,  805,  813,  814,  822,  904,  818,  819,
  824,  825,  826,  786,  633,  834,  749,  875,  876,  633,
  830,  827,  768,  627,  775,  831,  681,  835,  627,   21,
  833,  632,  837,  633,  841,  636,  842,  843,  850,  851,
  852,  855,  807,  806,  865,  889,  868,  860,  879,  880,
  882,  891,   33,  898,  899,   33,  633,   33,  633,  633,
   33,  633,   33,  632,  900,   33,  636,   33,   33,  901,
   33,   11,   33,   11,   33,  903,   33,   33,   33,   33,
  914,  915,  916,   33,  921,  917,  923,  924,   33,  928,
   33,   33,   33,  930,    2,   33,   33,   33,  636,   33,
   12,   33,   33,   33,   33,   33,   33,   33,   33,   50,
   33,   33,   33,   33,   10,   52,   33,   33,   33,  336,
   54,  434,  308,  231,  146,   33,   33,   78,   33,   33,
  632,   33,   33,   33,   53,   33,  337,   33,  436,  858,
  309,  232,  148,   80,  540,   14,   15,  128,  208,   33,
  439,   33,  440,   33,  548,  869,  319,  490,   20,  535,
  331,  509,  512,  544,  513,  636,  515,  491,  517,   12,
  358,   15,   12,   72,   46,  478,   75,   12,  268,   12,
  271,  465,   12,  370,   12,   12,  467,   12,   11,   12,
  598,   12,  823,   12,   12,   12,   12,  678,  848,  745,
   12,   23,  486,  472,   11,   12,  473,   12,   12,   12,
  743,  435,   12,   12,   12,  695,   12,   33,  381,   12,
  632,   12,   12,   12,   12,  483,  810,  874,   12,   12,
   12,  812,  504,   12,   12,   12,  354,  347,  313,  315,
  314,  318,   12,   12,  316,   12,   12,  169,   12,   12,
   12,  931,  925,  769,  919,  636,  632,   12,   12,  632,
  927,  840,  715,   33,  885,    0,   12,  701,  632,    0,
    0,    0,    0,    0,    0,    0,    0,  392,  392,  392,
    0,  392,    0,  392,  392,  392,   21,    0,    0,   21,
    0,  636,  632,    0,  636,  392,    0,  632,    0,  392,
    0,   21,    0,  636,    0,    0,   21,    0,    0,  632,
   21,   22,    0,   21,  632,    0,  392,    0,  392,    0,
    0,    0,    0,    0,    0,   21,   21,  636,  632,    0,
   21,   21,  636,    0,   12,    0,   21,    0,   21,   21,
   21,   21,    0,    0,  636,    0,   21,    0,    0,  636,
   21,  632,   21,  632,  632,    0,  632,   12,    0,    0,
   12,    0,   21,  636,    0,   21,    0,   21,    0,    0,
    0,    0,   12,    0,   21,   21,    0,   12,    0,    0,
    0,   12,    0,    0,   12,    0,  636,    0,  636,  636,
    0,  636,    0,    0,    0,    0,   12,   12,    0,    0,
    0,   12,   12,    0,    0,    0,    0,   12,    0,   12,
   12,   12,   12,    0,    0,   20,    0,   12,   20,    0,
    0,   12,    0,   12,    0,    0,    0,    0,    0,    0,
   20,    0,    0,   12,    0,   20,   12,    0,   12,   20,
    0,    0,   20,    0,    0,   12,   12,    0,    0,    0,
    0,    0,    0,    0,   20,   20,    0,    0,   23,   20,
   20,   33,    0,    0,    0,   20,    0,   20,   20,   20,
   20,    0,    0,   33,    0,   20,    0,    0,   33,   20,
    0,   20,   33,    0,    0,   33,    0,    0,    0,    0,
    0,   20,    0,    0,   20,    0,   20,   33,   33,    0,
    0,    0,    0,   33,   20,    0,    0,    0,   33,    0,
   33,   33,   33,   33,    0,    0,    0,    0,   33,    0,
   33,    0,   33,   33,   33,    0,    0,    0,    0,    0,
    0,    0,    0,    0,   33,   33,    0,   33,    0,   33,
   33,    0,    0,    0,   33,    0,   23,   33,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   33,
   33,    0,    0,    0,   33,   33,    0,    0,   22,    0,
   33,   33,   33,   33,   33,   33,    0,    0,    0,    0,
   33,    0,    0,   33,   33,    0,   33,    0,   33,    0,
    0,    0,   33,    0,    0,   33,   33,    0,    0,   33,
    0,   33,    0,    0,    0,    0,    0,   33,   33,    0,
    0,    0,    0,   33,    0,    0,    0,    0,   33,    0,
   33,   33,   33,   33,    0,    0,    0,    0,   33,    0,
    0,    0,   33,    0,   33,    0,    0,    0,    0,    0,
    0,    0,  537,    0,   33,    0,    0,   33,    0,   33,
  537,  537,  537,  537,  537,    0,  537,  537,    0,  537,
  537,  537,  537,    0,  537,  537,  537,    0,    0,  382,
    0,  537,  541,  537,  537,  537,  537,  537,  537,    0,
    0,  537,    0,    0,    0,  537,  537,    0,  537,  537,
  537,    0,    0,    0,    0,    0,    0,    0,    0,    0,
  537,    0,  537,    0,  537,  537,  382,    0,  537,    0,
  537,  537,  537,  537,  537,  537,  537,  537,  537,  537,
  537,  537,    0,  537,    0,  537,    0,    0,    0,    0,
  537,  537,    0,    0,  537,    0,    0,    0,    0,  537,
  537,  537,  537,  537,  393,  393,  393,  537,  393,  537,
  393,  393,  393,  382,  537,  382,  537,  382,    0,  382,
  382,  382,  393,    0,    0,    0,  393,  382,  382,  382,
  382,    0,    0,    0,  382,  382,    0,    0,    0,    0,
    0,    0,    0,  393,  382,  393,  382,    0,  382,    0,
  382,    0,  382,    0,  382,    0,  537,    0,  537,    0,
  537,  485,  537,    0,  537,    0,  537,    0,  537,  485,
  485,  485,  485,  485,    0,  485,  485,    0,  485,  485,
  485,  485,    0,  485,  485,    0,    0,    0,  381,    0,
  485,    0,  485,  485,  485,  485,  485,  485,    0,    0,
  485,    0,    0,    0,  485,  485,    0,  485,  485,  485,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  485,
    0,  485,    0,  485,  485,  381,    0,  485,    0,  485,
  485,  485,  485,  485,  485,  485,  485,  485,  485,  485,
  485,    0,  485,    0,  485,    0,    0,    0,    0,  485,
  485,    0,    0,  485,    0,    0,    0,    0,  485,  485,
  485,  485,  485,  395,  395,  395,  485,  395,  485,  395,
  395,  395,  381,  485,  381,  485,  381,    0,  381,  381,
  381,  395,    0,    0,    0,  395,  381,  381,  381,  381,
    0,    0,    0,  381,  381,    0,    0,    0,    0,    0,
    0,    0,    0,  381,  395,  381,    0,  381,    0,  381,
    0,  381,    0,  381,    0,  485,    0,  485,    0,  485,
  601,  485,    0,  485,    0,  485,    0,  485,   77,   78,
  602,   79,    0,    0,   80,  603,    0,  604,  605,   82,
    0,    0,  606,   83,    0,    0,    0,    0,    0,   84,
    0,  607,   85,  608,  609,  610,  611,    0,    0,   86,
    0,    0,    0,  612,   87,    0,   88,   89,   90,  545,
    0,    0,    0,    0,    0,    0,    0,    0,  613,    0,
   91,    0,   92,   93,    0,    0,   94,    0,  614,   95,
  615,   96,  616,   97,   98,   99,  617,  618,  101,  619,
    0,  620,    0,  621,    0,    0,    0,    0,  526,    0,
    0,  546,  102,    0,    0,    0,    0,  622,  103,  104,
  105,  106,    0,    0,    0,  107,    0,  108,    0,    0,
    0,    0,  623,    0,  624,    0,    0,    0,  547,  548,
  549,  550,    0,  551,  552,  553,  554,  555,  556,  557,
  558,    0,  559,    0,  560,    0,  561,    0,  562,    0,
  563,    0,  564,    0,  565,    0,  566,    0,    0,    0,
    0,    0,    0,    0,  111,    0,  112,    0,  113,  601,
  114,    0,  115,    0,  116,    0,  625,   77,   78,  602,
   79,    0,    0,   80,  603,    0,    0,  605,   82,    0,
    0,  606,   83,    0,    0,    0,    0,    0,   84,    0,
  607,   85,  608,  609,  610,  611,    0,    0,   86,    0,
    0,    0,  612,   87,    0,   88,   89,   90,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  613,    0,   91,
    0,   92,   93,    0,    0,   94,    0,  614,   95,  615,
   96,  616,   97,   98,   99,  617,  618,  101,  619,  364,
    0,  364,  621,  364,    0,    0,    0,  526,  360,    0,
    0,  102,    0,    0,    0,    0,  622,  103,  104,  105,
  106,    0,    0,    0,  107,    0,  108,    0,    0,    0,
    0,  623,    0,  624,    0,    0,    0,    0,  360,    0,
  360,    0,  360,    0,  360,    0,  360,    0,  360,    0,
  360,    0,  360,    0,  360,  601,  360,    0,    0,    0,
    0,    0,    0,   77,   78,    0,   79,    0,    0,   80,
   81,    0,    0,  111,   82,  112,    0,  113,   83,  114,
    0,  115,    0,  116,   84,   43,    0,   85,    0,    0,
    0,    0,    0,    0,   86,    0,    0,    0,    0,   87,
    0,   88,   89,   90,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   91,    0,   92,   93,    0,
    0,   94,    0,    0,   95,    0,   96,    0,   97,   98,
   99,  100,    0,  101,    0,  365,  620,  365,    0,  365,
    0,    0,    0,    0,  361,    0,    0,  102,    0,    0,
    0,    0,    0,  103,  104,  105,  106,    0,    0,    0,
  107,    0,  108,    0,    0,    0,    0,  623,    0,  624,
    0,    0,    0,    0,  361,    0,  361,    0,  361,    0,
  361,    0,  361,    0,  361,    0,  361,    0,  361,    0,
  361,  601,  361,    0,    0,    0,    0,    0,    0,   77,
   78,    0,   79,    0,    0,   80,   81,    0,    0,  111,
   82,  112,    0,  113,   83,  114,    0,  115,    0,  116,
   84,   43,    0,   85,    0,    0,  243,    0,    0,    0,
   86,  243,    0,    0,    0,   87,    0,   88,   89,   90,
    0,  390,  390,  390,    0,  390,    0,  390,  390,  390,
    0,   91,    0,   92,   93,    0,    0,   94,  390,  390,
   95,    0,   96,  390,   97,   98,   99,  100,  243,  101,
    0,    0,    0,    0,    0,  243,    0,    0,    0,    0,
  390,    0,  390,  102,    0,    0,    0,    0,    0,  103,
  104,  105,  106,    0,    0,  243,  107,    0,  108,    0,
    0,    0,    0,  623,    0,  624,    0,    0,    0,    0,
    0,    0,    0,    0,  243,  243,  243,  243,  243,  243,
    0,  243,  243,  243,    0,    0,    0,    0,    0,  243,
  243,  243,  243,  243,    0,    0,  243,  243,    0,    0,
    0,    0,    0,    0,    0,  111,  243,  112,  243,  113,
  243,  114,  243,  115,  243,  116,  243,   43,   33,    0,
    0,    0,    0,   33,    0,   33,    0,    0,   33,    0,
   33,   33,    0,   33,    0,   33,    0,   33,    0,   33,
   33,   33,   33,    0,    0,    0,   33,    0,    0,    0,
    0,   33,  243,   33,   33,   33,    0,    0,   33,    0,
   33,    0,   33,    0,    0,   33,    0,   33,   33,   33,
   33,    0,    0,    0,   33,   33,   33,    0,    0,   33,
   33,   33,    0,    0,    0,    0,    0,    0,   33,   33,
    0,   33,   33,    0,   33,   33,   33,    0,    0,    0,
    0,   33,    0,   61,    0,    0,   33,    0,   33,    0,
    0,   33,   33,   33,   33,    0,   33,    0,   33,    0,
   33,    0,   33,   33,   33,   33,    0,    0,    0,   33,
    0,    0,    0,    0,   33,    0,   33,   33,   33,    0,
    0,   33,    0,   33,    0,   33,    0,    0,   33,    0,
   33,   33,   33,   33,    0,    0,    0,   33,   33,   33,
    0,    0,   33,   33,   33,    0,    0,    0,    0,    0,
    0,   33,   33,    0,   33,   33,    0,   33,   33,   33,
   33,    0,    0,    0,   33,    0,   82,    0,    0,   33,
    0,   33,    0,    0,   33,   33,   33,   33,    0,   33,
    0,   33,    0,   33,    0,   33,   33,   33,   33,    0,
    0,    0,   33,    0,    0,    0,    0,   33,    0,   33,
   33,   33,    0,    0,   33,    0,   33,    0,   33,    0,
    0,   33,    0,   33,   33,   33,   33,    0,    0,    0,
   33,   33,   33,    0,    0,   33,   33,   33,    0,    0,
    0,    0,    0,    0,   33,   33,    0,   33,   33,    0,
   33,   33,   33,   33,    0,    0,    0,   33,    0,   62,
    0,    0,   33,    0,   33,    0,  331,   33,   33,   33,
   33,    0,   33,    0,   33,    0,   33,    0,   33,   33,
   33,   33,    0,    0,    0,   33,    0,    0,    0,    0,
   33,    0,   33,   33,   33,    0,    0,   33,    0,   33,
    0,   33,    0,  331,   33,    0,   33,   33,   33,   33,
    0,    0,    0,   33,   33,   33,    0,    0,   33,   33,
   33,    0,    0,    0,    0,    0,    0,   33,   33,    0,
   33,   33,    0,   33,   33,   33,   33,    0,    0,    0,
    0,    0,   83,    0,    0,  332,    0,    0,    0,  331,
  331,   33,  331,  331,  331,  331,  331,  331,  331,    0,
  331,  331,    0,  331,  331,  331,  331,  331,  331,  331,
  331,  331,  331,  331,    0,  331,    0,  331,    0,  331,
    0,  331,  332,  331,    0,  331,    0,  331,    0,  331,
    0,  331,    0,  331,    0,  331,    0,  331,    0,  331,
    0,  331,    0,  331,    0,  331,    0,  331,    0,  331,
    0,  331,    0,  331,    0,    0,    0,    0,    0,   33,
    0,    0,    0,    0,  340,    0,    0,  331,  332,  332,
    0,  332,  332,  332,  332,  332,  332,  332,    0,  332,
  332,    0,  332,  332,  332,  332,  332,  332,  332,  332,
  332,  332,  332,    0,  332,    0,  332,    0,  332,    0,
  332,  340,  332,    0,  332,    0,  332,    0,  332,    0,
  332,    0,  332,    0,  332,    0,  332,    0,  332,    0,
  332,    0,  332,    0,  332,    0,  332,    0,  332,    0,
  332,    0,  332,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  276,    0,    0,  332,    0,  340,  340,
  340,  340,  340,  340,  340,  340,  340,    0,  340,  340,
    0,  340,  340,  340,  340,  340,  340,  340,  340,  340,
  340,  340,    0,  340,    0,  340,    0,  340,    0,  340,
  276,  340,    0,  340,    0,  340,    0,  340,    0,  340,
    0,  340,    0,  340,    0,  340,    0,  340,    0,  340,
    0,  340,    0,  340,    0,  340,    0,  340,    0,  340,
    0,  340,    0,    0,  317,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  340,    0,  276,  276,  276,
  276,  276,    0,  276,  276,  276,    0,  276,  276,    0,
  276,  276,  276,  276,  276,  276,  276,  276,  276,  276,
  276,  317,  276,    0,  276,    0,  276,    0,  276,    0,
  276,    0,  276,    0,  276,    0,  276,    0,  276,    0,
  276,    0,  276,    0,  276,    0,  276,    0,  276,    0,
  276,    0,  276,    0,  276,  352,  276,    0,  276,    0,
  276,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,  317,  317,  317,  276,    0,    0,    0,  317,  317,
    0,  317,  317,  317,  317,  317,  317,  317,  317,  317,
  317,  317,  352,  317,    0,  317,    0,  317,    0,  317,
    0,  317,    0,  317,    0,  317,    0,  317,    0,  317,
    0,  317,    0,  317,    0,  317,    0,  317,    0,  317,
    0,  317,    0,  317,    0,  317,    0,  317,    0,  317,
    0,  317,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  352,    0,  317,    0,    0,    0,  352,
  352,    0,  352,  352,  352,  352,  352,    0,  352,  352,
  352,  352,  274,    0,    0,    0,  352,    0,  352,    0,
  352,    0,  352,    0,  352,    0,  352,    0,  352,    0,
  352,    0,  352,    0,  352,    0,  352,    0,  352,    0,
  352,    0,  352,    0,  352,    0,  352,    0,  352,  274,
  352,  383,  383,  383,    0,  383,  274,  383,  383,  383,
    0,    0,    0,    0,    0,    0,  331,  383,  383,  383,
    0,    0,  383,  383,    0,    0,  274,    0,    0,    0,
    0,    0,    0,    0,    0,    0,  383,    0,  383,    0,
  383,    0,  383,    0,    0,  274,  274,    0,  274,  274,
  274,    0,  274,  274,  274,    0,    0,    0,    0,    0,
  274,  274,  274,  274,  274,    0,    0,  274,  274,    0,
    0,    0,    0,    0,    0,    0,    0,  274,    0,  274,
    0,  274,    0,  274,    0,  274,    0,  274,    0,  494,
  494,  494,  494,    0,    0,  494,  494,    0,  494,  494,
  494,    0,    0,  494,  494,    0,    0,    0,    0,    0,
  494,    0,  494,  494,  494,  494,  494,  494,    0,    0,
  494,    0,    0,  274,  494,  494,    0,  494,  494,  494,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  494,
    0,  494,    0,  494,  494,    0,    0,  494,    0,  494,
  494,  494,  494,  494,  494,  494,  494,  494,  494,  494,
  494,    0,  494,    0,  494,    0,    0,    0,    0,  494,
    0,    0,    0,  494,    0,    0,    0,    0,  494,  494,
  494,  494,  494,  384,  384,  384,  494,  384,  494,  384,
  384,  384,    0,  494,    0,  494,    0,    0,    0,  384,
  384,  384,    0,    0,  384,  384,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,  384,    0,
  384,    0,  384,    0,  384,   77,   78,    0,   79,    0,
    0,   80,   81,    0,    0,  494,   82,  494,    0,  494,
   83,  494,    0,  494,    0,  494,   84,  494,    0,   85,
    0,    0,    0,    0,    0,    0,   86,    0,    0,    0,
    0,   87,    0,   88,   89,   90,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   91,    0,   92,
   93,  375,    0,   94,    0,    0,   95,    0,   96,    0,
   97,   98,   99,  100,    0,  101,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  341,  376,    0,    0,  102,
    0,    0,    0,    0,    0,  103,  104,  105,  106,  385,
  385,  385,  107,  385,  108,  385,  385,  385,    0,  109,
    0,  110,    0,    0,    0,  385,  385,  385,    0,    0,
  385,  385,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,  385,    0,  385,    0,  385,    0,
  385,   77,   78,    0,   79,    0,    0,   80,   81,    0,
    0,  111,   82,  112,    0,  113,   83,  114,    0,  115,
    0,  116,   84,   43,    0,   85,    0,    0,    0,    0,
    0,    0,   86,    0,    0,    0,    0,   87,    0,   88,
   89,   90,    0,  276,    0,    0,    0,    0,    0,    0,
  277,    0,    0,   91,    0,   92,   93,    0,    0,   94,
    0,    0,   95,    0,   96,    0,   97,   98,   99,  100,
    0,  101,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  102,    0,    0,    0,    0,
    0,  103,  104,  105,  106,  386,  386,  386,  107,  386,
  108,  386,  386,  386,    0,  109,    0,  110,    0,    0,
    0,  386,  386,  386,    0,    0,  386,  386,    0,    0,
    0,    0,    0,    0,  388,  388,  388,    0,  388,    0,
  388,  388,  388,    0,  386,    0,  386,   77,   78,    0,
   79,  388,  388,   80,   81,  388,  388,  111,   82,  112,
    0,  113,   83,  114,    0,  115,    0,  116,   84,   43,
    0,   85,    0,  388,    0,  388,    0,    0,   86,    0,
    0,    0,    0,   87,    0,   88,   89,   90,    0,  391,
  391,  391,    0,  391,    0,  391,  391,  391,    0,   91,
    0,   92,   93,  375,    0,   94,  391,  391,   95,    0,
   96,  391,   97,   98,   99,  100,    0,  101,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  341,  391,    0,
  391,  102,    0,    0,    0,    0,    0,  103,  104,  105,
  106,  387,  387,  387,  107,  387,  108,  387,  387,  387,
    0,  109,    0,  110,    0,    0,    0,  387,  387,  387,
    0,    0,  387,  387,    0,    0,    0,    0,    0,    0,
  389,  389,  389,    0,  389,    0,  389,  389,  389,    0,
  387,    0,  387,   77,   78,    0,   79,  389,  389,   80,
   81,  389,  389,  111,   82,  112,    0,  113,   83,  114,
    0,  115,    0,  116,   84,   43,    0,   85,    0,  389,
    0,  389,    0,    0,   86,    0,    0,    0,    0,   87,
    0,   88,   89,   90,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   91,    0,   92,   93,    0,
    0,   94,    0,    0,   95,    0,   96,    0,   97,   98,
   99,  100,    0,  101,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,  102,    0,    0,
  272,    0,    0,  103,  104,  105,  106,    0,    0,    0,
  107,    0,  108,    0,    0,    0,    0,  109,    0,  110,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   77,
   78,    0,   79,    0,    0,   80,   81,    0,    0,  111,
   82,  112,    0,  113,   83,  114,    0,  115,    0,  116,
   84,   43,    0,   85,    0,    0,    0,    0,    0,    0,
   86,    0,    0,    0,    0,   87,    0,   88,   89,   90,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   91,    0,   92,   93,    0,    0,   94,    0,    0,
   95,    0,   96,    0,   97,   98,   99,  100,    0,  101,
    0,    0,  620,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,  102,    0,    0,    0,    0,    0,  103,
  104,  105,  106,    0,    0,    0,  107,    0,  108,    0,
    0,    0,    0,  109,    0,  110,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   77,   78,    0,   79,    0,
    0,   80,   81,    0,    0,  111,   82,  112,    0,  113,
   83,  114,    0,  115,    0,  116,   84,   43,    0,   85,
    0,    0,    0,    0,    0,    0,   86,    0,    0,    0,
    0,   87,    0,   88,   89,   90,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   91,    0,   92,
   93,    0,    0,   94,    0,    0,   95,    0,   96,    0,
   97,   98,   99,  100,    0,  101,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  102,
    0,    0,    0,    0,    0,  103,  104,  105,  106,    0,
    0,    0,  107,    0,  108,    0,    0,    0,    0,  109,
    0,  110,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,   77,   78,    0,   79,    0,    0,   80,   81,    0,
    0,  111,   82,  112,    0,  113,   83,  114,    0,  115,
    0,  116,   84,  117,    0,   85,    0,    0,    0,    0,
    0,    0,   86,    0,    0,    0,    0,   87,    0,   88,
   89,   90,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,   91,    0,   92,   93,    0,    0,   94,
    0,    0,   95,    0,   96,    0,   97,   98,   99,  100,
    0,  101,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,  102,    0,    0,    0,    0,
    0,  103,  104,  105,  106,    0,    0,    0,  107,    0,
  108,    0,    0,    0,    0,  109,    0,  110,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,   77,   78,    0,
   79,    0,    0,   80,   81,    0,    0,  111,   82,  112,
    0,  113,   83,  114,    0,  115,    0,  116,   84,   43,
    0,   85,    0,    0,    0,    0,    0,    0,   86,    0,
    0,    0,    0,   87,    0,   88,   89,   90,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   91,
    0,   92,   93,    0,    0,   94,    0,    0,   95,    0,
   96,    0,   97,   98,   99,  100,    0,  101,   77,   78,
    0,   79,    0,    0,   80,   81,    0,    0,    0,   82,
    0,  102,    0,   83,    0,    0,    0,  103,    0,   84,
  106,    0,   85,    0,    0,    0,    0,    0,    0,   86,
    0,    0,    0,    0,   87,    0,   88,   89,   90,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   91,    0,   92,   93,    0,    0,   94,    0,    0,   95,
    0,   96,    0,   97,   98,   99,  100,    0,  101,    0,
    0,    0,    0,  111,    0,  112,    0,  113,    0,  114,
    0,  115,  698,  116,    0,   43,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,    0,
   78,    0,   79,    0,    0,   80,    0,  164,  490,    0,
   82,    0,  165,    0,   83,    0,  166,  491,  492,    0,
    0,    0,    0,   85,    0,    0,    0,    0,  493,    0,
   86,  167,    0,    0,    0,   87,    0,    0,    0,   90,
    0,    0,    0,    0,  111,    0,  112,    0,  113,    0,
  114,   91,  115,   92,  116,    0,   43,   94,  168,    0,
    0,    0,    0,    0,    0,   98,   99,    0,  417,  101,
  417,    0,  494,  417,    0,  417,  417,    0,  417,    0,
  417,    0,  417,    0,  417,  417,  417,    0,    0,    0,
    0,  417,    0,    0,    0,    0,  417,    0,  417,  417,
    0,    0,    0,  417,    0,    0,    0,  417,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,  417,
    0,  417,    0,    0,    0,  417,  417,    0,    0,    0,
    0,    0,    0,  417,  417,    0,  416,  417,  416,    0,
  417,  416,    0,  416,  416,    0,  416,    0,  416,    0,
  416,    0,  416,  416,  416,    0,    0,  495,    0,  416,
    0,    0,    0,    0,  416,    0,  416,  416,    0,    0,
    0,  416,    0,    0,    0,  416,   33,    0,   33,    0,
    0,   33,    0,    0,    0,    0,   33,  416,    0,  416,
   33,    0,    0,  416,  416,    0,    0,    0,    0,   33,
    0,  416,  416,    0,    0,  416,   33,    0,  416,    0,
    0,   33,    0,    0,    0,   33,   33,   33,   33,   33,
    0,   33,    0,    0,   33,  417,   33,   33,    0,   33,
   33,    0,    0,   33,    0,    0,    0,    0,    0,   33,
    0,   33,   33,    0,    0,   33,   33,    0,   33,    0,
    0,   33,    0,    0,    0,   33,    0,   33,    0,   33,
  114,    0,    0,    0,   33,    0,   33,   33,   33,   33,
    0,   33,    0,   33,    0,    0,   33,    0,    0,    0,
   33,   33,   33,   33,    0,   33,    0,    0,   33,   33,
    0,    0,    0,  416,    0,    0,   33,    0,  114,    0,
    0,   33,    0,   33,    0,   33,   33,    0,   33,    0,
    0,   33,    0,    0,    0,    0,   33,   33,    0,   33,
   33,    0,    0,   33,    0,    0,    0,    0,    0,   33,
    0,   33,   33,   33,    0,   33,   33,    0,   33,    0,
    0,   33,    0,    0,    0,   33,  152,   33,    0,   33,
    0,    0,    0,    0,   33,    0,    0,   33,    0,   33,
   33,    0,   33,   33,    0,   33,    0,    0,    0,    0,
   33,   33,   33,   33,   33,   33,    0,   33,   33,    0,
    0,    0,    0,   33,    0,    0,    0,    0,    0,    0,
   33,    0,    0,    0,    0,   33,    0,   33,    0,   33,
    0,    0,    0,    0,   78,    0,   79,    0,    0,   80,
    0,   33,    0,   33,   82,    0,    0,   33,   83,    0,
    0,  505,    0,   33,    0,   33,   33,   85,    0,   33,
    0,    0,   33,    0,   86,    0,  160,    0,  160,   87,
  153,  160,    0,   90,    0,    0,  160,    0,    0,    0,
  160,    0,    0,  160,    0,   91,    0,   92,    0,  160,
    0,   94,    0,   33,    0,    0,  160,    0,    0,   98,
   99,  160,    0,  101,    0,  160,  506,    0,    0,    0,
   78,    0,   79,    0,    0,   80,    0,  160,    0,  160,
   82,    0,    0,  160,   83,    0,    0,    0,    0,    0,
    0,  160,  160,   85,    0,  160,    0,    0,  160,    0,
   86,    0,    0,    0,    0,   87,    0,   33,   78,   90,
   79,    0,    0,   80,    0,    0,    0,    0,   82,    0,
    0,   91,   83,   92,    0,    0,    0,   94,    0,    0,
    0,   85,    0,    0,    0,   98,   99,    0,   86,  101,
    0,    0,  173,   87,    0,    0,    0,   90,    0,    0,
    0,   43,   78,    0,   79,    0,    0,   80,    0,   91,
    0,   92,   82,    0,    0,   94,   83,    0,    0,    0,
    0,    0,    0,   98,   99,   85,    0,  101,    0,    0,
  241,    0,   86,  160,  122,  317,  122,   87,    0,  122,
    0,   90,    0,    0,  122,    0,    0,    0,  122,    0,
    0,    0,    0,   91,    0,   92,    0,  122,    0,   94,
    0,    0,    0,    0,  122,    0,    0,   98,   99,  122,
    0,  101,  317,  122,  257,    0,    0,   43,    0,    0,
    0,    0,    0,    0,    0,  122,    0,  122,    0,    0,
    0,  122,    0,    0,    0,    0,    0,    0,    0,  122,
  122,    0,    0,  122,    0,    0,  122,    0,  303,    0,
    0,    0,    0,    0,    0,   43,    0,    0,    0,  317,
    0,  317,  317,  317,  317,  317,  317,  317,    0,  317,
  317,    0,  317,  317,  317,  317,  317,  317,  317,  317,
  317,  317,  317,    0,  317,  303,  317,    0,  317,    0,
  317,    0,  317,    0,  317,    0,  317,    0,  317,   43,
  317,    0,  317,    0,  317,    0,  317,    0,  317,    0,
  317,    0,  317,    0,  317,    0,  317,    0,  317,   16,
  317,    0,  317,    0,    0,    0,    0,    0,    0,    0,
    0,  122,  303,  303,  303,    0,  303,  303,  303,  303,
  303,    0,  303,  303,    0,  303,  303,  303,  303,  303,
  303,  303,  303,  303,  303,  303,   16,  303,    0,  303,
    0,  303,    0,  303,    0,  303,    0,  303,    0,  303,
    0,  303,    0,  303,    0,  303,    0,  303,    0,  303,
    0,  303,    0,  303,    0,  303,    0,  303,    0,  303,
  352,  303,    0,  303,    0,  303,    0,    0,    0,    0,
    0,    0,    0,    0,   16,    0,   16,   16,   16,   16,
    0,    0,    0,   16,   16,    0,    0,   16,   16,   16,
   16,   16,   16,   16,   16,   16,   16,  352,   16,    0,
   16,    0,   16,    0,   16,    0,   16,    0,   16,    0,
   16,    0,   16,    0,   16,    0,   16,    0,   16,    0,
   16,    0,   16,    0,   16,    0,   16,    0,   16,    0,
   16,  366,   16,    0,   16,    0,   16,    0,    0,    0,
    0,    0,    0,    0,  352,    0,  352,    0,  352,    0,
  352,  352,  352,    0,  352,  352,    0,  352,  352,  352,
  352,  352,  352,  352,  352,  352,  352,    0,  366,    0,
    0,  352,    0,  352,  370,  352,    0,  352,    0,  352,
    0,  352,    0,  352,    0,  352,    0,  352,    0,  352,
    0,  352,    0,  352,    0,  352,    0,  352,    0,  352,
    0,  352,    0,  352,    0,  352,    0,    0,    0,    0,
    0,  370,    0,    0,    0,  366,  366,  366,  371,  366,
    0,  366,  366,  366,    0,  366,  366,    0,    0,  366,
  366,  366,  366,  366,  366,  366,  366,  366,    0,    0,
    0,    0,  366,    0,  366,    0,  366,    0,  366,    0,
  366,    0,  366,    0,  366,  371,  366,    0,  370,  370,
  370,  372,  370,    0,  370,  370,  370,    0,  370,  370,
    0,    0,  370,  370,  370,  370,    0,    0,    0,  370,
  370,    0,    0,    0,    0,  370,    0,  370,    0,  370,
    0,  370,    0,  370,    0,  370,    0,  370,  372,  370,
    0,    0,  371,  371,  371,  373,  371,    0,  371,  371,
  371,    0,  371,  371,    0,    0,  371,  371,  371,  371,
    0,    0,    0,  371,  371,    0,    0,    0,    0,  371,
    0,  371,    0,  371,    0,  371,    0,  371,    0,  371,
    0,  371,  373,  371,    0,  372,  372,  372,  374,  372,
    0,  372,  372,  372,    0,  372,  372,    0,    0,  372,
  372,  372,  372,    0,    0,    0,  372,  372,    0,    0,
    0,    0,  372,    0,  372,    0,  372,    0,  372,    0,
  372,    0,  372,    0,  372,  374,  372,    0,    0,  373,
  373,  373,  375,  373,    0,  373,  373,  373,    0,    0,
    0,    0,    0,  373,  373,  373,  373,  373,    0,    0,
  373,  373,    0,    0,    0,    0,  373,    0,  373,    0,
  373,    0,  373,    0,  373,    0,  373,    0,  373,  375,
  373,    0,  374,  374,  374,  376,  374,    0,  374,  374,
  374,    0,    0,    0,    0,    0,  374,  374,  374,  374,
  374,    0,    0,  374,  374,    0,    0,    0,    0,  374,
    0,  374,    0,  374,    0,  374,    0,  374,    0,  374,
    0,  374,  376,  374,    0,    0,  375,  375,  375,  377,
  375,    0,  375,  375,  375,    0,    0,    0,    0,    0,
  375,  375,  375,  375,  375,    0,    0,  375,  375,    0,
    0,    0,    0,  375,    0,  375,    0,  375,    0,  375,
    0,  375,    0,  375,    0,  375,  377,  375,    0,  376,
  376,  376,  378,  376,    0,  376,  376,  376,    0,    0,
    0,    0,    0,  376,  376,  376,  376,  376,    0,    0,
  376,  376,    0,    0,    0,    0,    0,    0,    0,    0,
  376,    0,  376,    0,  376,    0,  376,    0,  376,  378,
  376,    0,    0,  377,  377,  377,  379,  377,    0,  377,
  377,  377,    0,    0,    0,    0,    0,  377,  377,  377,
  377,  377,    0,    0,  377,  377,    0,    0,    0,    0,
    0,    0,    0,    0,  377,    0,  377,    0,  377,    0,
  377,    0,  377,  379,  377,    0,  378,  378,  378,  380,
  378,    0,  378,  378,  378,    0,    0,    0,    0,    0,
  378,  378,  378,  378,  378,    0,    0,  378,  378,    0,
    0,    0,    0,    0,    0,    0,    0,  378,    0,  378,
    0,  378,    0,  378,    0,  378,  380,  378,    0,    0,
  379,  379,  379,    0,  379,    0,  379,  379,  379,    0,
    0,    0,    0,    0,  379,  379,  379,  379,  379,    0,
    0,  379,  379,    0,    0,    0,    0,    0,    0,    0,
    0,  379,    0,  379,    0,  379,    0,  379,    0,  379,
    0,  379,    0,  380,  380,  380,    0,  380,    0,  380,
  380,  380,    0,    0,    0,    0,    0,  380,  380,  380,
  380,  380,    0,    0,  380,  380,    0,    0,    0,   20,
    0,    0,    0,    0,  380,    0,  380,    0,  380,    0,
  380,   20,  380,    0,  380,    0,   20,    0,    0,    0,
   20,    0,    0,   20,    0,    0,    0,    0,    0,   53,
    0,    0,    0,    0,    0,   20,   20,    0,    0,    0,
   20,   20,    0,    0,    0,    0,   20,    0,   20,   20,
   20,   20,    0,   54,    0,    0,   20,    0,    0,    0,
   20,    0,   20,    0,    0,    0,   55,    0,    0,    0,
    0,   56,   20,    0,    0,   20,   57,   20,   58,   59,
   60,   61,    0,    0,   20,   20,   62,    0,    0,  279,
   63,  279,  474,  279,  474,    0,  474,    0,    0,    0,
    0,  279,   64,    0,    0,   65,  279,   66,    0,    0,
    0,  279,  283,  279,  283,  477,  283,  477,    0,  477,
    0,    0,    0,    0,  283,    0,    0,    0,    0,  283,
    0,  279,    0,  279,  283,  279,  283,  279,    0,  279,
    0,  279,    0,  279,    0,  279,    0,  279,    0,  279,
    0,  279,    0,    0,  283,    0,  283,    0,  283,    0,
  283,    0,  283,    0,  283,  279,  283,    0,  283,    0,
  283,    0,  283,  284,  283,  284,  478,  284,  478,    0,
  478,    0,    0,    0,    0,  284,    0,    0,  283,    0,
  284,    0,    0,    0,    0,  284,  325,  284,  325,  482,
  325,  482,    0,  482,    0,    0,    0,    0,  325,    0,
    0,    0,    0,  325,    0,  284,    0,  284,  325,  284,
  325,  284,    0,  284,    0,  284,    0,  284,    0,  284,
    0,  284,    0,  284,    0,  284,    0,    0,  325,    0,
  325,    0,  325,   16,  325,   16,  325,   16,  325,  284,
  325,    0,  325,    0,  325,   16,  325,    0,  325,    0,
   16,    0,    0,    0,    0,   16,    0,   16,    0,    0,
    0,    0,  325,    0,    0,    0,    0,    0,    0,    0,
    0,    0,    0,    0,    0,   16,    0,   16,    0,   16,
    0,   16,    0,   16,    0,   16,    0,   16,    0,   16,
    0,   16,    0,   16,    0,   16,    0,    0,    0,    0,
    0,    0,    0,    0,    0,    0,    0,    0,    0,   16,
  };
  protected static  short [] yyCheck = {            50,
    3,   12,  253,  707,  373,  127,  184,  497,  511,  705,
   13,   14,  638,   24,  268,   88,  345,  261,  256,  404,
  585,  305,  276,  575,  255,  325,  204,  268,  721,  102,
  305,   34,  606,  399,  325,  276,  256,   48,  528,  350,
  263,  325,  363,  345,  363,  535,  422,  197,  264,  256,
  325,  102,  604,  175,  298,  346,  363,  399,  496,  282,
  182,  399,  346,  103,  104,  105,  106,  107,  108,  109,
  110,  346,  363,  511,  575,  350,  262,  255,  367,  363,
  363,  256,  346,  399,  348,  263,  638,  372,  363,  374,
  456,  314,  165,  422,  269,  262,  256,  386,  305,  363,
  346,  422,  252,  422,  345,  606,  179,  180,  346,  325,
  348,  422,  346,  603,  456,  359,  360,  363,  456,  170,
  422,  172,  422,  335,  346,  363,  616,  617,  348,  315,
  181,  422,  376,  826,  378,  831,  701,  638,  422,  422,
  456,  363,  193,  363,  195,  305,  197,  422,  315,  327,
  358,  224,  225,  705,  657,  206,  207,  208,  209,  210,
  211,  212,  213,  214,  215,  216,  345,  719,  422,  795,
  863,  402,  351,  399,  399,  422,  422,  217,  218,  219,
  220,  221,  222,  223,  569,  236,  226,  227,  228,  229,
  230,  231,  232,  233,  234,  235,  256,  237,    1,  422,
  422,  252,  346,  907,  705,  346,  352,  903,  268,   12,
  399,  518,  346,   16,  399,  346,  276,  399,  353,  363,
  346,   24,  363,  401,  264,  276,  277,   30,  718,  363,
  456,  456,  363,  736,  350,  301,  543,  363,  385,  346,
  346,  346,  349,  795,  348,   48,  347,  346,  346,  322,
  351,  373,  399,  346,  422,  328,  363,  363,  363,  628,
  834,  568,  346,  410,  363,  363,  573,  456,  758,  843,
  363,  456,  579,  763,  456,  582,  344,    1,  422,  363,
  346,  422,  346,  351,  348,   88,  337,  346,  422,  658,
  341,  422,   16,  867,  795,  257,  422,  346,  872,  363,
  349,  350,  375,  346,  363,  931,  357,  797,  798,  456,
  884,  384,  421,  165,  363,  889,  319,  422,  349,  330,
  363,  353,  333,  422,  422,  434,  575,  179,  180,  422,
  831,  700,  363,  834,  349,  444,  344,  346,  422,  348,
  347,  350,  843,  342,  351,  353,  345,  344,  363,  458,
  423,  841,  425,  404,  928,  929,  353,  606,  346,  368,
  272,  370,  165,  853,  854,  277,  867,  220,  221,  281,
  346,  872,  224,  225,  926,  363,  179,  180,  350,  931,
  870,  353,  385,  884,  296,  396,  693,  363,  889,  638,
  344,  394,  465,  348,  397,  272,  399,  351,  346,  408,
  277,  349,  903,  346,  281,  358,  349,  410,  481,  705,
  461,  323,  260,  342,  348,  363,  345,  490,  491,  296,
  363,  224,  225,  719,  272,  926,  340,  928,  929,  277,
  931,  345,  505,  281,  349,  438,  284,  345,  346,  347,
  513,  349,  515,  351,  352,  353,  323,  346,  296,  297,
  348,  350,  503,  456,  302,  363,  705,  348,  380,  307,
  382,  309,  310,  311,  312,  575,  340,  351,  347,  317,
  322,  345,  351,  321,  477,  323,  328,  346,  351,  348,
  306,  350,  308,  346,  350,  333,  349,  313,  336,  349,
  338,  351,  362,  353,  363,  366,  606,  345,  384,  368,
  363,  370,  575,  344,  347,  627,  628,  348,  351,  350,
  351,  358,  353,  363,  364,  365,  589,  590,  569,  322,
  593,  361,  347,  375,  344,  328,  351,  330,  638,  422,
  333,  604,  384,  584,  344,  575,  658,  422,  348,  408,
  344,  388,  422,  390,  348,  392,  795,  394,  344,  396,
  728,  398,  348,  400,  347,  402,  347,  404,  351,  406,
  351,  344,  613,  346,  615,  638,  606,  818,  819,  355,
  356,  423,  375,  425,  577,  344,  349,  699,  700,  348,
  353,  384,  831,  623,  624,  834,  347,  348,  422,  350,
  351,  352,  349,  396,  843,  705,  353,  600,  638,  226,
  227,  228,  229,  351,  351,  353,  353,  422,  345,  346,
  347,  363,  349,  465,  351,  352,  353,  351,  867,  353,
  423,  422,  425,  872,  422,    0,  363,  222,  223,  481,
  367,  704,  705,  706,  340,  884,  342,  422,  490,  491,
  889,  340,  349,  342,  351,  422,  719,  698,  349,  386,
  351,  230,  231,  505,  903,  347,  707,  349,  346,  710,
  711,  513,  465,  515,  340,  705,  342,  353,  719,  352,
  721,  674,  675,  352,  352,  352,  679,  926,  481,  928,
  929,  684,  931,  349,  349,  795,  347,  490,  491,  351,
  348,  494,  351,  496,  349,  351,  348,  345,  352,  349,
  351,  344,  505,  344,  351,  344,  344,  351,  511,  349,
  513,  344,  515,  349,  351,  345,  345,  422,  353,  260,
  345,  831,  795,  345,  834,  353,  302,  351,  345,  422,
  348,  347,  783,  843,  348,  358,  344,  589,  590,  344,
  349,  593,    0,  284,  348,  346,  358,  350,  821,  348,
  348,  754,  755,  346,  344,  795,  297,  867,  358,  422,
  349,  302,  872,  348,  837,  353,  307,  344,  309,  310,
  311,  312,  352,  422,  884,  826,  317,  349,  347,  889,
  321,  346,  349,  353,  348,  353,  589,  590,  348,  348,
  593,  831,  333,  903,  834,  336,  348,  338,  348,  348,
  348,  363,  348,  843,  342,  352,  345,  347,  353,  860,
  353,  353,  863,  354,  349,  349,  926,  339,  928,  929,
  353,  931,  353,  353,  353,  351,  345,  867,  340,  353,
  575,  344,  872,  353,  349,  349,  347,  888,  348,  348,
  345,  349,  348,  269,  884,  349,  849,  850,  851,  889,
  353,  422,  704,  926,  706,  351,  907,  349,  931,    0,
  353,  606,  348,  903,  286,  575,  349,  349,  353,  353,
  345,  345,  315,  262,  294,  280,  349,  358,  349,  349,
  349,  344,  257,  342,  340,  260,  926,  262,  928,  929,
  265,  931,  267,  638,  349,  270,  606,  272,  273,  353,
  275,  704,  277,  706,  279,  353,  281,  282,  283,  284,
  349,  345,  353,  288,  349,  353,  352,  345,  293,  349,
  295,  296,  297,  352,    0,  300,  301,  302,  638,  304,
    0,  306,  307,  308,  309,  310,  311,  312,  313,  349,
  315,  316,  317,  318,  344,  349,  321,  322,  323,  347,
  349,  344,  349,  344,  344,  330,  331,  344,  333,  334,
  705,  336,  337,  338,  349,  340,  347,  342,  344,  821,
  349,  344,  344,  344,  286,  345,  345,  344,  344,  354,
  345,  315,  345,  262,  344,  837,  238,  345,    0,  353,
  422,  353,  353,  349,  353,  705,  349,  345,  349,  257,
  319,    4,  260,   34,   24,  432,   48,  265,  195,  267,
  196,  407,  270,  333,  272,  273,  410,  275,  821,  277,
  571,  279,  763,  281,  282,  283,  284,  580,  798,  675,
  288,    0,  456,  428,  837,  293,  428,  295,  296,  297,
  674,  396,  300,  301,  302,  600,  304,  422,  356,  307,
  795,  309,  310,  311,  312,  438,  754,  849,  316,  317,
  318,  755,  477,  321,  322,  323,  277,  261,  232,  234,
  233,  237,  330,  331,  235,  333,  334,   68,  336,  337,
  338,  926,  910,  704,  903,  795,  831,  345,  346,  834,
  912,  787,  615,    0,  862,   -1,  354,  604,  843,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  345,  346,  347,
   -1,  349,   -1,  351,  352,  353,  257,   -1,   -1,  260,
   -1,  831,  867,   -1,  834,  363,   -1,  872,   -1,  367,
   -1,  272,   -1,  843,   -1,   -1,  277,   -1,   -1,  884,
  281,    0,   -1,  284,  889,   -1,  384,   -1,  386,   -1,
   -1,   -1,   -1,   -1,   -1,  296,  297,  867,  903,   -1,
  301,  302,  872,   -1,  422,   -1,  307,   -1,  309,  310,
  311,  312,   -1,   -1,  884,   -1,  317,   -1,   -1,  889,
  321,  926,  323,  928,  929,   -1,  931,  257,   -1,   -1,
  260,   -1,  333,  903,   -1,  336,   -1,  338,   -1,   -1,
   -1,   -1,  272,   -1,  345,  346,   -1,  277,   -1,   -1,
   -1,  281,   -1,   -1,  284,   -1,  926,   -1,  928,  929,
   -1,  931,   -1,   -1,   -1,   -1,  296,  297,   -1,   -1,
   -1,  301,  302,   -1,   -1,   -1,   -1,  307,   -1,  309,
  310,  311,  312,   -1,   -1,  257,   -1,  317,  260,   -1,
   -1,  321,   -1,  323,   -1,   -1,   -1,   -1,   -1,   -1,
  272,   -1,   -1,  333,   -1,  277,  336,   -1,  338,  281,
   -1,   -1,  284,   -1,   -1,  345,  346,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  296,  297,   -1,   -1,  257,  301,
  302,  260,   -1,   -1,   -1,  307,   -1,  309,  310,  311,
  312,   -1,   -1,  272,   -1,  317,   -1,   -1,  277,  321,
   -1,  323,  281,   -1,   -1,  284,   -1,   -1,   -1,   -1,
   -1,  333,   -1,   -1,  336,   -1,  338,  296,  297,   -1,
   -1,   -1,   -1,  302,  346,   -1,   -1,   -1,  307,   -1,
  309,  310,  311,  312,   -1,   -1,   -1,   -1,  317,   -1,
  257,   -1,  321,  260,  323,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  333,  272,   -1,  336,   -1,  338,
  277,   -1,   -1,   -1,  281,   -1,  345,  284,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  296,
  297,   -1,   -1,   -1,  301,  302,   -1,   -1,  257,   -1,
  307,  260,  309,  310,  311,  312,   -1,   -1,   -1,   -1,
  317,   -1,   -1,  272,  321,   -1,  323,   -1,  277,   -1,
   -1,   -1,  281,   -1,   -1,  284,  333,   -1,   -1,  336,
   -1,  338,   -1,   -1,   -1,   -1,   -1,  296,  297,   -1,
   -1,   -1,   -1,  302,   -1,   -1,   -1,   -1,  307,   -1,
  309,  310,  311,  312,   -1,   -1,   -1,   -1,  317,   -1,
   -1,   -1,  321,   -1,  323,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,  256,   -1,  333,   -1,   -1,  336,   -1,  338,
  264,  265,  266,  267,  268,   -1,  270,  271,   -1,  273,
  274,  275,  276,   -1,  278,  279,  280,   -1,   -1,  261,
   -1,  285,  286,  287,  288,  289,  290,  291,  292,   -1,
   -1,  295,   -1,   -1,   -1,  299,  300,   -1,  302,  303,
  304,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  314,   -1,  316,   -1,  318,  319,  298,   -1,  322,   -1,
  324,  325,  326,  327,  328,  329,  330,  331,  332,  333,
  334,  335,   -1,  337,   -1,  339,   -1,   -1,   -1,   -1,
  344,  345,   -1,   -1,  348,   -1,   -1,   -1,   -1,  353,
  354,  355,  356,  357,  345,  346,  347,  361,  349,  363,
  351,  352,  353,  345,  368,  347,  370,  349,   -1,  351,
  352,  353,  363,   -1,   -1,   -1,  367,  359,  360,  361,
  362,   -1,   -1,   -1,  366,  367,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  384,  376,  386,  378,   -1,  380,   -1,
  382,   -1,  384,   -1,  386,   -1,  410,   -1,  412,   -1,
  414,  256,  416,   -1,  418,   -1,  420,   -1,  422,  264,
  265,  266,  267,  268,   -1,  270,  271,   -1,  273,  274,
  275,  276,   -1,  278,  279,   -1,   -1,   -1,  261,   -1,
  285,   -1,  287,  288,  289,  290,  291,  292,   -1,   -1,
  295,   -1,   -1,   -1,  299,  300,   -1,  302,  303,  304,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  314,
   -1,  316,   -1,  318,  319,  298,   -1,  322,   -1,  324,
  325,  326,  327,  328,  329,  330,  331,  332,  333,  334,
  335,   -1,  337,   -1,  339,   -1,   -1,   -1,   -1,  344,
  345,   -1,   -1,  348,   -1,   -1,   -1,   -1,  353,  354,
  355,  356,  357,  345,  346,  347,  361,  349,  363,  351,
  352,  353,  345,  368,  347,  370,  349,   -1,  351,  352,
  353,  363,   -1,   -1,   -1,  367,  359,  360,  361,  362,
   -1,   -1,   -1,  366,  367,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  376,  386,  378,   -1,  380,   -1,  382,
   -1,  384,   -1,  386,   -1,  410,   -1,  412,   -1,  414,
  256,  416,   -1,  418,   -1,  420,   -1,  422,  264,  265,
  266,  267,   -1,   -1,  270,  271,   -1,  273,  274,  275,
   -1,   -1,  278,  279,   -1,   -1,   -1,   -1,   -1,  285,
   -1,  287,  288,  289,  290,  291,  292,   -1,   -1,  295,
   -1,   -1,   -1,  299,  300,   -1,  302,  303,  304,  285,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  314,   -1,
  316,   -1,  318,  319,   -1,   -1,  322,   -1,  324,  325,
  326,  327,  328,  329,  330,  331,  332,  333,  334,  335,
   -1,  337,   -1,  339,   -1,   -1,   -1,   -1,  344,   -1,
   -1,  327,  348,   -1,   -1,   -1,   -1,  353,  354,  355,
  356,  357,   -1,   -1,   -1,  361,   -1,  363,   -1,   -1,
   -1,   -1,  368,   -1,  370,   -1,   -1,   -1,  354,  355,
  356,  357,   -1,  359,  360,  361,  362,  363,  364,  365,
  366,   -1,  368,   -1,  370,   -1,  372,   -1,  374,   -1,
  376,   -1,  378,   -1,  380,   -1,  382,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  410,   -1,  412,   -1,  414,  256,
  416,   -1,  418,   -1,  420,   -1,  422,  264,  265,  266,
  267,   -1,   -1,  270,  271,   -1,   -1,  274,  275,   -1,
   -1,  278,  279,   -1,   -1,   -1,   -1,   -1,  285,   -1,
  287,  288,  289,  290,  291,  292,   -1,   -1,  295,   -1,
   -1,   -1,  299,  300,   -1,  302,  303,  304,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  314,   -1,  316,
   -1,  318,  319,   -1,   -1,  322,   -1,  324,  325,  326,
  327,  328,  329,  330,  331,  332,  333,  334,  335,  349,
   -1,  351,  339,  353,   -1,   -1,   -1,  344,  358,   -1,
   -1,  348,   -1,   -1,   -1,   -1,  353,  354,  355,  356,
  357,   -1,   -1,   -1,  361,   -1,  363,   -1,   -1,   -1,
   -1,  368,   -1,  370,   -1,   -1,   -1,   -1,  388,   -1,
  390,   -1,  392,   -1,  394,   -1,  396,   -1,  398,   -1,
  400,   -1,  402,   -1,  404,  256,  406,   -1,   -1,   -1,
   -1,   -1,   -1,  264,  265,   -1,  267,   -1,   -1,  270,
  271,   -1,   -1,  410,  275,  412,   -1,  414,  279,  416,
   -1,  418,   -1,  420,  285,  422,   -1,  288,   -1,   -1,
   -1,   -1,   -1,   -1,  295,   -1,   -1,   -1,   -1,  300,
   -1,  302,  303,  304,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  316,   -1,  318,  319,   -1,
   -1,  322,   -1,   -1,  325,   -1,  327,   -1,  329,  330,
  331,  332,   -1,  334,   -1,  349,  337,  351,   -1,  353,
   -1,   -1,   -1,   -1,  358,   -1,   -1,  348,   -1,   -1,
   -1,   -1,   -1,  354,  355,  356,  357,   -1,   -1,   -1,
  361,   -1,  363,   -1,   -1,   -1,   -1,  368,   -1,  370,
   -1,   -1,   -1,   -1,  388,   -1,  390,   -1,  392,   -1,
  394,   -1,  396,   -1,  398,   -1,  400,   -1,  402,   -1,
  404,  256,  406,   -1,   -1,   -1,   -1,   -1,   -1,  264,
  265,   -1,  267,   -1,   -1,  270,  271,   -1,   -1,  410,
  275,  412,   -1,  414,  279,  416,   -1,  418,   -1,  420,
  285,  422,   -1,  288,   -1,   -1,  256,   -1,   -1,   -1,
  295,  261,   -1,   -1,   -1,  300,   -1,  302,  303,  304,
   -1,  345,  346,  347,   -1,  349,   -1,  351,  352,  353,
   -1,  316,   -1,  318,  319,   -1,   -1,  322,  362,  363,
  325,   -1,  327,  367,  329,  330,  331,  332,  298,  334,
   -1,   -1,   -1,   -1,   -1,  305,   -1,   -1,   -1,   -1,
  384,   -1,  386,  348,   -1,   -1,   -1,   -1,   -1,  354,
  355,  356,  357,   -1,   -1,  325,  361,   -1,  363,   -1,
   -1,   -1,   -1,  368,   -1,  370,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  344,  345,  346,  347,  348,  349,
   -1,  351,  352,  353,   -1,   -1,   -1,   -1,   -1,  359,
  360,  361,  362,  363,   -1,   -1,  366,  367,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  410,  376,  412,  378,  414,
  380,  416,  382,  418,  384,  420,  386,  422,  260,   -1,
   -1,   -1,   -1,  265,   -1,  267,   -1,   -1,  270,   -1,
  272,  273,   -1,  275,   -1,  277,   -1,  279,   -1,  281,
  282,  283,  284,   -1,   -1,   -1,  288,   -1,   -1,   -1,
   -1,  293,  422,  295,  296,  297,   -1,   -1,  300,   -1,
  302,   -1,  304,   -1,   -1,  307,   -1,  309,  310,  311,
  312,   -1,   -1,   -1,  316,  317,  318,   -1,   -1,  321,
  322,  323,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,
   -1,  333,  334,   -1,  336,  337,  338,   -1,   -1,   -1,
   -1,  260,   -1,  345,   -1,   -1,  265,   -1,  267,   -1,
   -1,  270,  354,  272,  273,   -1,  275,   -1,  277,   -1,
  279,   -1,  281,  282,  283,  284,   -1,   -1,   -1,  288,
   -1,   -1,   -1,   -1,  293,   -1,  295,  296,  297,   -1,
   -1,  300,   -1,  302,   -1,  304,   -1,   -1,  307,   -1,
  309,  310,  311,  312,   -1,   -1,   -1,  316,  317,  318,
   -1,   -1,  321,  322,  323,   -1,   -1,   -1,   -1,   -1,
   -1,  330,  331,   -1,  333,  334,   -1,  336,  337,  338,
  422,   -1,   -1,   -1,  260,   -1,  345,   -1,   -1,  265,
   -1,  267,   -1,   -1,  270,  354,  272,  273,   -1,  275,
   -1,  277,   -1,  279,   -1,  281,  282,  283,  284,   -1,
   -1,   -1,  288,   -1,   -1,   -1,   -1,  293,   -1,  295,
  296,  297,   -1,   -1,  300,   -1,  302,   -1,  304,   -1,
   -1,  307,   -1,  309,  310,  311,  312,   -1,   -1,   -1,
  316,  317,  318,   -1,   -1,  321,  322,  323,   -1,   -1,
   -1,   -1,   -1,   -1,  330,  331,   -1,  333,  334,   -1,
  336,  337,  338,  422,   -1,   -1,   -1,  260,   -1,  345,
   -1,   -1,  265,   -1,  267,   -1,  261,  270,  354,  272,
  273,   -1,  275,   -1,  277,   -1,  279,   -1,  281,  282,
  283,  284,   -1,   -1,   -1,  288,   -1,   -1,   -1,   -1,
  293,   -1,  295,  296,  297,   -1,   -1,  300,   -1,  302,
   -1,  304,   -1,  298,  307,   -1,  309,  310,  311,  312,
   -1,   -1,   -1,  316,  317,  318,   -1,   -1,  321,  322,
  323,   -1,   -1,   -1,   -1,   -1,   -1,  330,  331,   -1,
  333,  334,   -1,  336,  337,  338,  422,   -1,   -1,   -1,
   -1,   -1,  345,   -1,   -1,  261,   -1,   -1,   -1,  344,
  345,  354,  347,  348,  349,  350,  351,  352,  353,   -1,
  355,  356,   -1,  358,  359,  360,  361,  362,  363,  364,
  365,  366,  367,  368,   -1,  370,   -1,  372,   -1,  374,
   -1,  376,  298,  378,   -1,  380,   -1,  382,   -1,  384,
   -1,  386,   -1,  388,   -1,  390,   -1,  392,   -1,  394,
   -1,  396,   -1,  398,   -1,  400,   -1,  402,   -1,  404,
   -1,  406,   -1,  408,   -1,   -1,   -1,   -1,   -1,  422,
   -1,   -1,   -1,   -1,  261,   -1,   -1,  422,  344,  345,
   -1,  347,  348,  349,  350,  351,  352,  353,   -1,  355,
  356,   -1,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,   -1,  370,   -1,  372,   -1,  374,   -1,
  376,  298,  378,   -1,  380,   -1,  382,   -1,  384,   -1,
  386,   -1,  388,   -1,  390,   -1,  392,   -1,  394,   -1,
  396,   -1,  398,   -1,  400,   -1,  402,   -1,  404,   -1,
  406,   -1,  408,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  261,   -1,   -1,  422,   -1,  345,  346,
  347,  348,  349,  350,  351,  352,  353,   -1,  355,  356,
   -1,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,   -1,  370,   -1,  372,   -1,  374,   -1,  376,
  298,  378,   -1,  380,   -1,  382,   -1,  384,   -1,  386,
   -1,  388,   -1,  390,   -1,  392,   -1,  394,   -1,  396,
   -1,  398,   -1,  400,   -1,  402,   -1,  404,   -1,  406,
   -1,  408,   -1,   -1,  261,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  422,   -1,  345,  346,  347,
  348,  349,   -1,  351,  352,  353,   -1,  355,  356,   -1,
  358,  359,  360,  361,  362,  363,  364,  365,  366,  367,
  368,  298,  370,   -1,  372,   -1,  374,   -1,  376,   -1,
  378,   -1,  380,   -1,  382,   -1,  384,   -1,  386,   -1,
  388,   -1,  390,   -1,  392,   -1,  394,   -1,  396,   -1,
  398,   -1,  400,   -1,  402,  261,  404,   -1,  406,   -1,
  408,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  348,  349,  350,  422,   -1,   -1,   -1,  355,  356,
   -1,  358,  359,  360,  361,  362,  363,  364,  365,  366,
  367,  368,  298,  370,   -1,  372,   -1,  374,   -1,  376,
   -1,  378,   -1,  380,   -1,  382,   -1,  384,   -1,  386,
   -1,  388,   -1,  390,   -1,  392,   -1,  394,   -1,  396,
   -1,  398,   -1,  400,   -1,  402,   -1,  404,   -1,  406,
   -1,  408,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  349,   -1,  422,   -1,   -1,   -1,  355,
  356,   -1,  358,  359,  360,  361,  362,   -1,  364,  365,
  366,  367,  261,   -1,   -1,   -1,  372,   -1,  374,   -1,
  376,   -1,  378,   -1,  380,   -1,  382,   -1,  384,   -1,
  386,   -1,  388,   -1,  390,   -1,  392,   -1,  394,   -1,
  396,   -1,  398,   -1,  400,   -1,  402,   -1,  404,  298,
  406,  345,  346,  347,   -1,  349,  305,  351,  352,  353,
   -1,   -1,   -1,   -1,   -1,   -1,  422,  361,  362,  363,
   -1,   -1,  366,  367,   -1,   -1,  325,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,  380,   -1,  382,   -1,
  384,   -1,  386,   -1,   -1,  344,  345,   -1,  347,  348,
  349,   -1,  351,  352,  353,   -1,   -1,   -1,   -1,   -1,
  359,  360,  361,  362,  363,   -1,   -1,  366,  367,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  376,   -1,  378,
   -1,  380,   -1,  382,   -1,  384,   -1,  386,   -1,  264,
  265,  266,  267,   -1,   -1,  270,  271,   -1,  273,  274,
  275,   -1,   -1,  278,  279,   -1,   -1,   -1,   -1,   -1,
  285,   -1,  287,  288,  289,  290,  291,  292,   -1,   -1,
  295,   -1,   -1,  422,  299,  300,   -1,  302,  303,  304,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  314,
   -1,  316,   -1,  318,  319,   -1,   -1,  322,   -1,  324,
  325,  326,  327,  328,  329,  330,  331,  332,  333,  334,
  335,   -1,  337,   -1,  339,   -1,   -1,   -1,   -1,  344,
   -1,   -1,   -1,  348,   -1,   -1,   -1,   -1,  353,  354,
  355,  356,  357,  345,  346,  347,  361,  349,  363,  351,
  352,  353,   -1,  368,   -1,  370,   -1,   -1,   -1,  361,
  362,  363,   -1,   -1,  366,  367,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  380,   -1,
  382,   -1,  384,   -1,  386,  264,  265,   -1,  267,   -1,
   -1,  270,  271,   -1,   -1,  410,  275,  412,   -1,  414,
  279,  416,   -1,  418,   -1,  420,  285,  422,   -1,  288,
   -1,   -1,   -1,   -1,   -1,   -1,  295,   -1,   -1,   -1,
   -1,  300,   -1,  302,  303,  304,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  316,   -1,  318,
  319,  320,   -1,  322,   -1,   -1,  325,   -1,  327,   -1,
  329,  330,  331,  332,   -1,  334,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  344,  345,   -1,   -1,  348,
   -1,   -1,   -1,   -1,   -1,  354,  355,  356,  357,  345,
  346,  347,  361,  349,  363,  351,  352,  353,   -1,  368,
   -1,  370,   -1,   -1,   -1,  361,  362,  363,   -1,   -1,
  366,  367,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  380,   -1,  382,   -1,  384,   -1,
  386,  264,  265,   -1,  267,   -1,   -1,  270,  271,   -1,
   -1,  410,  275,  412,   -1,  414,  279,  416,   -1,  418,
   -1,  420,  285,  422,   -1,  288,   -1,   -1,   -1,   -1,
   -1,   -1,  295,   -1,   -1,   -1,   -1,  300,   -1,  302,
  303,  304,   -1,  306,   -1,   -1,   -1,   -1,   -1,   -1,
  313,   -1,   -1,  316,   -1,  318,  319,   -1,   -1,  322,
   -1,   -1,  325,   -1,  327,   -1,  329,  330,  331,  332,
   -1,  334,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  348,   -1,   -1,   -1,   -1,
   -1,  354,  355,  356,  357,  345,  346,  347,  361,  349,
  363,  351,  352,  353,   -1,  368,   -1,  370,   -1,   -1,
   -1,  361,  362,  363,   -1,   -1,  366,  367,   -1,   -1,
   -1,   -1,   -1,   -1,  345,  346,  347,   -1,  349,   -1,
  351,  352,  353,   -1,  384,   -1,  386,  264,  265,   -1,
  267,  362,  363,  270,  271,  366,  367,  410,  275,  412,
   -1,  414,  279,  416,   -1,  418,   -1,  420,  285,  422,
   -1,  288,   -1,  384,   -1,  386,   -1,   -1,  295,   -1,
   -1,   -1,   -1,  300,   -1,  302,  303,  304,   -1,  345,
  346,  347,   -1,  349,   -1,  351,  352,  353,   -1,  316,
   -1,  318,  319,  320,   -1,  322,  362,  363,  325,   -1,
  327,  367,  329,  330,  331,  332,   -1,  334,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  344,  384,   -1,
  386,  348,   -1,   -1,   -1,   -1,   -1,  354,  355,  356,
  357,  345,  346,  347,  361,  349,  363,  351,  352,  353,
   -1,  368,   -1,  370,   -1,   -1,   -1,  361,  362,  363,
   -1,   -1,  366,  367,   -1,   -1,   -1,   -1,   -1,   -1,
  345,  346,  347,   -1,  349,   -1,  351,  352,  353,   -1,
  384,   -1,  386,  264,  265,   -1,  267,  362,  363,  270,
  271,  366,  367,  410,  275,  412,   -1,  414,  279,  416,
   -1,  418,   -1,  420,  285,  422,   -1,  288,   -1,  384,
   -1,  386,   -1,   -1,  295,   -1,   -1,   -1,   -1,  300,
   -1,  302,  303,  304,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  316,   -1,  318,  319,   -1,
   -1,  322,   -1,   -1,  325,   -1,  327,   -1,  329,  330,
  331,  332,   -1,  334,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  348,   -1,   -1,
  351,   -1,   -1,  354,  355,  356,  357,   -1,   -1,   -1,
  361,   -1,  363,   -1,   -1,   -1,   -1,  368,   -1,  370,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,
  265,   -1,  267,   -1,   -1,  270,  271,   -1,   -1,  410,
  275,  412,   -1,  414,  279,  416,   -1,  418,   -1,  420,
  285,  422,   -1,  288,   -1,   -1,   -1,   -1,   -1,   -1,
  295,   -1,   -1,   -1,   -1,  300,   -1,  302,  303,  304,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  316,   -1,  318,  319,   -1,   -1,  322,   -1,   -1,
  325,   -1,  327,   -1,  329,  330,  331,  332,   -1,  334,
   -1,   -1,  337,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  348,   -1,   -1,   -1,   -1,   -1,  354,
  355,  356,  357,   -1,   -1,   -1,  361,   -1,  363,   -1,
   -1,   -1,   -1,  368,   -1,  370,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  264,  265,   -1,  267,   -1,
   -1,  270,  271,   -1,   -1,  410,  275,  412,   -1,  414,
  279,  416,   -1,  418,   -1,  420,  285,  422,   -1,  288,
   -1,   -1,   -1,   -1,   -1,   -1,  295,   -1,   -1,   -1,
   -1,  300,   -1,  302,  303,  304,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  316,   -1,  318,
  319,   -1,   -1,  322,   -1,   -1,  325,   -1,  327,   -1,
  329,  330,  331,  332,   -1,  334,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  348,
   -1,   -1,   -1,   -1,   -1,  354,  355,  356,  357,   -1,
   -1,   -1,  361,   -1,  363,   -1,   -1,   -1,   -1,  368,
   -1,  370,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  264,  265,   -1,  267,   -1,   -1,  270,  271,   -1,
   -1,  410,  275,  412,   -1,  414,  279,  416,   -1,  418,
   -1,  420,  285,  422,   -1,  288,   -1,   -1,   -1,   -1,
   -1,   -1,  295,   -1,   -1,   -1,   -1,  300,   -1,  302,
  303,  304,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,  316,   -1,  318,  319,   -1,   -1,  322,
   -1,   -1,  325,   -1,  327,   -1,  329,  330,  331,  332,
   -1,  334,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  348,   -1,   -1,   -1,   -1,
   -1,  354,  355,  356,  357,   -1,   -1,   -1,  361,   -1,
  363,   -1,   -1,   -1,   -1,  368,   -1,  370,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  264,  265,   -1,
  267,   -1,   -1,  270,  271,   -1,   -1,  410,  275,  412,
   -1,  414,  279,  416,   -1,  418,   -1,  420,  285,  422,
   -1,  288,   -1,   -1,   -1,   -1,   -1,   -1,  295,   -1,
   -1,   -1,   -1,  300,   -1,  302,  303,  304,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  316,
   -1,  318,  319,   -1,   -1,  322,   -1,   -1,  325,   -1,
  327,   -1,  329,  330,  331,  332,   -1,  334,  264,  265,
   -1,  267,   -1,   -1,  270,  271,   -1,   -1,   -1,  275,
   -1,  348,   -1,  279,   -1,   -1,   -1,  354,   -1,  285,
  357,   -1,  288,   -1,   -1,   -1,   -1,   -1,   -1,  295,
   -1,   -1,   -1,   -1,  300,   -1,  302,  303,  304,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  316,   -1,  318,  319,   -1,   -1,  322,   -1,   -1,  325,
   -1,  327,   -1,  329,  330,  331,  332,   -1,  334,   -1,
   -1,   -1,   -1,  410,   -1,  412,   -1,  414,   -1,  416,
   -1,  418,  348,  420,   -1,  422,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  265,   -1,  267,   -1,   -1,  270,   -1,  272,  273,   -1,
  275,   -1,  277,   -1,  279,   -1,  281,  282,  283,   -1,
   -1,   -1,   -1,  288,   -1,   -1,   -1,   -1,  293,   -1,
  295,  296,   -1,   -1,   -1,  300,   -1,   -1,   -1,  304,
   -1,   -1,   -1,   -1,  410,   -1,  412,   -1,  414,   -1,
  416,  316,  418,  318,  420,   -1,  422,  322,  323,   -1,
   -1,   -1,   -1,   -1,   -1,  330,  331,   -1,  265,  334,
  267,   -1,  337,  270,   -1,  272,  273,   -1,  275,   -1,
  277,   -1,  279,   -1,  281,  282,  283,   -1,   -1,   -1,
   -1,  288,   -1,   -1,   -1,   -1,  293,   -1,  295,  296,
   -1,   -1,   -1,  300,   -1,   -1,   -1,  304,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  316,
   -1,  318,   -1,   -1,   -1,  322,  323,   -1,   -1,   -1,
   -1,   -1,   -1,  330,  331,   -1,  265,  334,  267,   -1,
  337,  270,   -1,  272,  273,   -1,  275,   -1,  277,   -1,
  279,   -1,  281,  282,  283,   -1,   -1,  422,   -1,  288,
   -1,   -1,   -1,   -1,  293,   -1,  295,  296,   -1,   -1,
   -1,  300,   -1,   -1,   -1,  304,  265,   -1,  267,   -1,
   -1,  270,   -1,   -1,   -1,   -1,  275,  316,   -1,  318,
  279,   -1,   -1,  322,  323,   -1,   -1,   -1,   -1,  288,
   -1,  330,  331,   -1,   -1,  334,  295,   -1,  337,   -1,
   -1,  300,   -1,   -1,   -1,  304,  265,  306,  267,  308,
   -1,  270,   -1,   -1,  313,  422,  275,  316,   -1,  318,
  279,   -1,   -1,  322,   -1,   -1,   -1,   -1,   -1,  288,
   -1,  330,  331,   -1,   -1,  334,  295,   -1,  337,   -1,
   -1,  300,   -1,   -1,   -1,  304,   -1,  306,   -1,  308,
  349,   -1,   -1,   -1,  313,   -1,  265,  316,  267,  318,
   -1,  270,   -1,  322,   -1,   -1,  275,   -1,   -1,   -1,
  279,  330,  331,  282,   -1,  334,   -1,   -1,  337,  288,
   -1,   -1,   -1,  422,   -1,   -1,  295,   -1,  347,   -1,
   -1,  300,   -1,  302,   -1,  304,  265,   -1,  267,   -1,
   -1,  270,   -1,   -1,   -1,   -1,  275,  316,   -1,  318,
  279,   -1,   -1,  322,   -1,   -1,   -1,   -1,   -1,  288,
   -1,  330,  331,  422,   -1,  334,  295,   -1,  337,   -1,
   -1,  300,   -1,   -1,   -1,  304,  345,  306,   -1,  308,
   -1,   -1,   -1,   -1,  313,   -1,   -1,  316,   -1,  318,
  265,   -1,  267,  322,   -1,  270,   -1,   -1,   -1,   -1,
  275,  330,  331,  422,  279,  334,   -1,  282,  337,   -1,
   -1,   -1,   -1,  288,   -1,   -1,   -1,   -1,   -1,   -1,
  295,   -1,   -1,   -1,   -1,  300,   -1,  302,   -1,  304,
   -1,   -1,   -1,   -1,  265,   -1,  267,   -1,   -1,  270,
   -1,  316,   -1,  318,  275,   -1,   -1,  322,  279,   -1,
   -1,  282,   -1,  422,   -1,  330,  331,  288,   -1,  334,
   -1,   -1,  337,   -1,  295,   -1,  265,   -1,  267,  300,
  345,  270,   -1,  304,   -1,   -1,  275,   -1,   -1,   -1,
  279,   -1,   -1,  282,   -1,  316,   -1,  318,   -1,  288,
   -1,  322,   -1,  422,   -1,   -1,  295,   -1,   -1,  330,
  331,  300,   -1,  334,   -1,  304,  337,   -1,   -1,   -1,
  265,   -1,  267,   -1,   -1,  270,   -1,  316,   -1,  318,
  275,   -1,   -1,  322,  279,   -1,   -1,   -1,   -1,   -1,
   -1,  330,  331,  288,   -1,  334,   -1,   -1,  337,   -1,
  295,   -1,   -1,   -1,   -1,  300,   -1,  422,  265,  304,
  267,   -1,   -1,  270,   -1,   -1,   -1,   -1,  275,   -1,
   -1,  316,  279,  318,   -1,   -1,   -1,  322,   -1,   -1,
   -1,  288,   -1,   -1,   -1,  330,  331,   -1,  295,  334,
   -1,   -1,  337,  300,   -1,   -1,   -1,  304,   -1,   -1,
   -1,  422,  265,   -1,  267,   -1,   -1,  270,   -1,  316,
   -1,  318,  275,   -1,   -1,  322,  279,   -1,   -1,   -1,
   -1,   -1,   -1,  330,  331,  288,   -1,  334,   -1,   -1,
  337,   -1,  295,  422,  265,  261,  267,  300,   -1,  270,
   -1,  304,   -1,   -1,  275,   -1,   -1,   -1,  279,   -1,
   -1,   -1,   -1,  316,   -1,  318,   -1,  288,   -1,  322,
   -1,   -1,   -1,   -1,  295,   -1,   -1,  330,  331,  300,
   -1,  334,  298,  304,  337,   -1,   -1,  422,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  316,   -1,  318,   -1,   -1,
   -1,  322,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  330,
  331,   -1,   -1,  334,   -1,   -1,  337,   -1,  261,   -1,
   -1,   -1,   -1,   -1,   -1,  422,   -1,   -1,   -1,  345,
   -1,  347,  348,  349,  350,  351,  352,  353,   -1,  355,
  356,   -1,  358,  359,  360,  361,  362,  363,  364,  365,
  366,  367,  368,   -1,  370,  298,  372,   -1,  374,   -1,
  376,   -1,  378,   -1,  380,   -1,  382,   -1,  384,  422,
  386,   -1,  388,   -1,  390,   -1,  392,   -1,  394,   -1,
  396,   -1,  398,   -1,  400,   -1,  402,   -1,  404,  261,
  406,   -1,  408,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  422,  345,  346,  347,   -1,  349,  350,  351,  352,
  353,   -1,  355,  356,   -1,  358,  359,  360,  361,  362,
  363,  364,  365,  366,  367,  368,  298,  370,   -1,  372,
   -1,  374,   -1,  376,   -1,  378,   -1,  380,   -1,  382,
   -1,  384,   -1,  386,   -1,  388,   -1,  390,   -1,  392,
   -1,  394,   -1,  396,   -1,  398,   -1,  400,   -1,  402,
  261,  404,   -1,  406,   -1,  408,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  346,   -1,  348,  349,  350,  351,
   -1,   -1,   -1,  355,  356,   -1,   -1,  359,  360,  361,
  362,  363,  364,  365,  366,  367,  368,  298,  370,   -1,
  372,   -1,  374,   -1,  376,   -1,  378,   -1,  380,   -1,
  382,   -1,  384,   -1,  386,   -1,  388,   -1,  390,   -1,
  392,   -1,  394,   -1,  396,   -1,  398,   -1,  400,   -1,
  402,  261,  404,   -1,  406,   -1,  408,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  345,   -1,  347,   -1,  349,   -1,
  351,  352,  353,   -1,  355,  356,   -1,  358,  359,  360,
  361,  362,  363,  364,  365,  366,  367,   -1,  298,   -1,
   -1,  372,   -1,  374,  261,  376,   -1,  378,   -1,  380,
   -1,  382,   -1,  384,   -1,  386,   -1,  388,   -1,  390,
   -1,  392,   -1,  394,   -1,  396,   -1,  398,   -1,  400,
   -1,  402,   -1,  404,   -1,  406,   -1,   -1,   -1,   -1,
   -1,  298,   -1,   -1,   -1,  345,  346,  347,  261,  349,
   -1,  351,  352,  353,   -1,  355,  356,   -1,   -1,  359,
  360,  361,  362,  363,  364,  365,  366,  367,   -1,   -1,
   -1,   -1,  372,   -1,  374,   -1,  376,   -1,  378,   -1,
  380,   -1,  382,   -1,  384,  298,  386,   -1,  345,  346,
  347,  261,  349,   -1,  351,  352,  353,   -1,  355,  356,
   -1,   -1,  359,  360,  361,  362,   -1,   -1,   -1,  366,
  367,   -1,   -1,   -1,   -1,  372,   -1,  374,   -1,  376,
   -1,  378,   -1,  380,   -1,  382,   -1,  384,  298,  386,
   -1,   -1,  345,  346,  347,  261,  349,   -1,  351,  352,
  353,   -1,  355,  356,   -1,   -1,  359,  360,  361,  362,
   -1,   -1,   -1,  366,  367,   -1,   -1,   -1,   -1,  372,
   -1,  374,   -1,  376,   -1,  378,   -1,  380,   -1,  382,
   -1,  384,  298,  386,   -1,  345,  346,  347,  261,  349,
   -1,  351,  352,  353,   -1,  355,  356,   -1,   -1,  359,
  360,  361,  362,   -1,   -1,   -1,  366,  367,   -1,   -1,
   -1,   -1,  372,   -1,  374,   -1,  376,   -1,  378,   -1,
  380,   -1,  382,   -1,  384,  298,  386,   -1,   -1,  345,
  346,  347,  261,  349,   -1,  351,  352,  353,   -1,   -1,
   -1,   -1,   -1,  359,  360,  361,  362,  363,   -1,   -1,
  366,  367,   -1,   -1,   -1,   -1,  372,   -1,  374,   -1,
  376,   -1,  378,   -1,  380,   -1,  382,   -1,  384,  298,
  386,   -1,  345,  346,  347,  261,  349,   -1,  351,  352,
  353,   -1,   -1,   -1,   -1,   -1,  359,  360,  361,  362,
  363,   -1,   -1,  366,  367,   -1,   -1,   -1,   -1,  372,
   -1,  374,   -1,  376,   -1,  378,   -1,  380,   -1,  382,
   -1,  384,  298,  386,   -1,   -1,  345,  346,  347,  261,
  349,   -1,  351,  352,  353,   -1,   -1,   -1,   -1,   -1,
  359,  360,  361,  362,  363,   -1,   -1,  366,  367,   -1,
   -1,   -1,   -1,  372,   -1,  374,   -1,  376,   -1,  378,
   -1,  380,   -1,  382,   -1,  384,  298,  386,   -1,  345,
  346,  347,  261,  349,   -1,  351,  352,  353,   -1,   -1,
   -1,   -1,   -1,  359,  360,  361,  362,  363,   -1,   -1,
  366,  367,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
  376,   -1,  378,   -1,  380,   -1,  382,   -1,  384,  298,
  386,   -1,   -1,  345,  346,  347,  261,  349,   -1,  351,
  352,  353,   -1,   -1,   -1,   -1,   -1,  359,  360,  361,
  362,  363,   -1,   -1,  366,  367,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,  376,   -1,  378,   -1,  380,   -1,
  382,   -1,  384,  298,  386,   -1,  345,  346,  347,  261,
  349,   -1,  351,  352,  353,   -1,   -1,   -1,   -1,   -1,
  359,  360,  361,  362,  363,   -1,   -1,  366,  367,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,  376,   -1,  378,
   -1,  380,   -1,  382,   -1,  384,  298,  386,   -1,   -1,
  345,  346,  347,   -1,  349,   -1,  351,  352,  353,   -1,
   -1,   -1,   -1,   -1,  359,  360,  361,  362,  363,   -1,
   -1,  366,  367,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,  376,   -1,  378,   -1,  380,   -1,  382,   -1,  384,
   -1,  386,   -1,  345,  346,  347,   -1,  349,   -1,  351,
  352,  353,   -1,   -1,   -1,   -1,   -1,  359,  360,  361,
  362,  363,   -1,   -1,  366,  367,   -1,   -1,   -1,  260,
   -1,   -1,   -1,   -1,  376,   -1,  378,   -1,  380,   -1,
  382,  272,  384,   -1,  386,   -1,  277,   -1,   -1,   -1,
  281,   -1,   -1,  284,   -1,   -1,   -1,   -1,   -1,  260,
   -1,   -1,   -1,   -1,   -1,  296,  297,   -1,   -1,   -1,
  301,  302,   -1,   -1,   -1,   -1,  307,   -1,  309,  310,
  311,  312,   -1,  284,   -1,   -1,  317,   -1,   -1,   -1,
  321,   -1,  323,   -1,   -1,   -1,  297,   -1,   -1,   -1,
   -1,  302,  333,   -1,   -1,  336,  307,  338,  309,  310,
  311,  312,   -1,   -1,  345,  346,  317,   -1,   -1,  346,
  321,  348,  349,  350,  351,   -1,  353,   -1,   -1,   -1,
   -1,  358,  333,   -1,   -1,  336,  363,  338,   -1,   -1,
   -1,  368,  346,  370,  348,  349,  350,  351,   -1,  353,
   -1,   -1,   -1,   -1,  358,   -1,   -1,   -1,   -1,  363,
   -1,  388,   -1,  390,  368,  392,  370,  394,   -1,  396,
   -1,  398,   -1,  400,   -1,  402,   -1,  404,   -1,  406,
   -1,  408,   -1,   -1,  388,   -1,  390,   -1,  392,   -1,
  394,   -1,  396,   -1,  398,  422,  400,   -1,  402,   -1,
  404,   -1,  406,  346,  408,  348,  349,  350,  351,   -1,
  353,   -1,   -1,   -1,   -1,  358,   -1,   -1,  422,   -1,
  363,   -1,   -1,   -1,   -1,  368,  346,  370,  348,  349,
  350,  351,   -1,  353,   -1,   -1,   -1,   -1,  358,   -1,
   -1,   -1,   -1,  363,   -1,  388,   -1,  390,  368,  392,
  370,  394,   -1,  396,   -1,  398,   -1,  400,   -1,  402,
   -1,  404,   -1,  406,   -1,  408,   -1,   -1,  388,   -1,
  390,   -1,  392,  346,  394,  348,  396,  350,  398,  422,
  400,   -1,  402,   -1,  404,  358,  406,   -1,  408,   -1,
  363,   -1,   -1,   -1,   -1,  368,   -1,  370,   -1,   -1,
   -1,   -1,  422,   -1,   -1,   -1,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,  388,   -1,  390,   -1,  392,
   -1,  394,   -1,  396,   -1,  398,   -1,  400,   -1,  402,
   -1,  404,   -1,  406,   -1,  408,   -1,   -1,   -1,   -1,
   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,   -1,  422,
  };

#line 3250 "cs-parser.jay"


Tokenizer lexer;
private CodeDomBuilder bd;
private CommentBuilder cmtBuilder;
private int classScope=0;

public CodeDomBuilder Builder
{
 get 
 {
 return bd;
 }
}

public Tokenizer Lexer {
	get {
		return lexer;
	}
}	

 

public CSharpParser (string name, System.IO.Stream input, ArrayList defines)
{
	
	this.name = name;
	this.input = input;

    cmtBuilder=new CommentBuilder(name);
	lexer = new Tokenizer (input, name, defines, cmtBuilder);
	bd = new CodeDomBuilder(name, lexer, cmtBuilder);
	
	
}

public override int parse ()
{
	StringBuilder value = new StringBuilder ();

	global_errors = 0;
	Report.Clear();
	try {
		if (yacc_verbose_flag)
			yyparse (lexer, new yydebug.yyDebugSimple ());
		else
			yyparse (lexer);
	} 

	
	    catch(Mono.CSharp.yyParser.yyException e)
	    {
	    Report.Error(1, lexer.Location, "Syntax error: " + e.Message + 
	    " <Detail location: "+ name +":"+lexer.location+">");
	    global_errors++;
	    }
	    
		catch (Exception e){	
		Console.WriteLine ("Fatal error [input pos.: {0}]", lexer.location);
		Console.WriteLine (e);
		global_errors++;
		}
	global_errors+=Report.Errors;
	
	return global_errors;
}

AttributeTargets CheckAttributeTarget (string a)
{
	switch (a) {

	case "assembly" : 
	  return AttributeTargets.Assembly;
	case "field" : 
	  return AttributeTargets.Field;
	case "method" : 
	  return AttributeTargets.Method;
	case "param" : 
	  return AttributeTargets.Param;
	case "property" : 
	  return AttributeTargets.Property;
	case "type" :
	  return AttributeTargets.Type;
		
		
	default :
		Location l = lexer.Location;
		Report.Error (658, l, "`" + a + "' is an invalid attribute target");
		return 0;
	}

}
}
#line 5603 "-"
namespace yydebug {
        using System;
	 public interface yyDebug {
		 void push (int state, Object value);
		 void lex (int state, int token, string name, Object value);
		 void shift (int from, int to, int errorFlag);
		 void pop (int state);
		 void discard (int state, int token, string name, Object value);
		 void reduce (int from, int to, int rule, string text, int len);
		 void shift (int from, int to);
		 void accept (Object value);
		 void error (string message);
		 void reject ();
	 }
	 
	 class yyDebugSimple : yyDebug {
		 void println (string s){
			 Console.WriteLine (s);
		 }
		 
		 public void push (int state, Object value) {
			 println ("push\tstate "+state+"\tvalue "+value);
		 }
		 
		 public void lex (int state, int token, string name, Object value) {
			 println("lex\tstate "+state+"\treading "+name+"\tvalue "+value);
		 }
		 
		 public void shift (int from, int to, int errorFlag) {
			 switch (errorFlag) {
			 default:				// normally
				 println("shift\tfrom state "+from+" to "+to);
				 break;
			 case 0: case 1: case 2:		// in error recovery
				 println("shift\tfrom state "+from+" to "+to
					     +"\t"+errorFlag+" left to recover");
				 break;
			 case 3:				// normally
				 println("shift\tfrom state "+from+" to "+to+"\ton error");
				 break;
			 }
		 }
		 
		 public void pop (int state) {
			 println("pop\tstate "+state+"\ton error");
		 }
		 
		 public void discard (int state, int token, string name, Object value) {
			 println("discard\tstate "+state+"\ttoken "+name+"\tvalue "+value);
		 }
		 
		 public void reduce (int from, int to, int rule, string text, int len) {
			 println("reduce\tstate "+from+"\tuncover "+to
				     +"\trule ("+rule+") "+text);
		 }
		 
		 public void shift (int from, int to) {
			 println("goto\tfrom state "+from+" to "+to);
		 }
		 
		 public void accept (Object value) {
			 println("accept\tvalue "+value);
		 }
		 
		 public void error (string message) {
			 println("error\t"+message);
		 }
		 
		 public void reject () {
			 println("reject");
		 }
		 
	 }
}
// %token constants
 class Token {
  public const int EOF = 257;
  public const int NONE = 258;
  public const int ERROR = 259;
  public const int ABSTRACT = 260;
  public const int AS = 261;
  public const int ADD = 262;
  public const int ASSEMBLY = 263;
  public const int BASE = 264;
  public const int BOOL = 265;
  public const int BREAK = 266;
  public const int BYTE = 267;
  public const int CASE = 268;
  public const int CATCH = 269;
  public const int CHAR = 270;
  public const int CHECKED = 271;
  public const int CLASS = 272;
  public const int CONST = 273;
  public const int CONTINUE = 274;
  public const int DECIMAL = 275;
  public const int DEFAULT = 276;
  public const int DELEGATE = 277;
  public const int DO = 278;
  public const int DOUBLE = 279;
  public const int ELSE = 280;
  public const int ENUM = 281;
  public const int EVENT = 282;
  public const int EXPLICIT = 283;
  public const int EXTERN = 284;
  public const int FALSE = 285;
  public const int FINALLY = 286;
  public const int FIXED = 287;
  public const int FLOAT = 288;
  public const int FOR = 289;
  public const int FOREACH = 290;
  public const int GOTO = 291;
  public const int IF = 292;
  public const int IMPLICIT = 293;
  public const int IN = 294;
  public const int INT = 295;
  public const int INTERFACE = 296;
  public const int INTERNAL = 297;
  public const int IS = 298;
  public const int LOCK = 299;
  public const int LONG = 300;
  public const int NAMESPACE = 301;
  public const int NEW = 302;
  public const int NULL = 303;
  public const int OBJECT = 304;
  public const int OPERATOR = 305;
  public const int OUT = 306;
  public const int OVERRIDE = 307;
  public const int PARAMS = 308;
  public const int PRIVATE = 309;
  public const int PROTECTED = 310;
  public const int PUBLIC = 311;
  public const int READONLY = 312;
  public const int REF = 313;
  public const int RETURN = 314;
  public const int REMOVE = 315;
  public const int SBYTE = 316;
  public const int SEALED = 317;
  public const int SHORT = 318;
  public const int SIZEOF = 319;
  public const int STACKALLOC = 320;
  public const int STATIC = 321;
  public const int STRING = 322;
  public const int STRUCT = 323;
  public const int SWITCH = 324;
  public const int THIS = 325;
  public const int THROW = 326;
  public const int TRUE = 327;
  public const int TRY = 328;
  public const int TYPEOF = 329;
  public const int UINT = 330;
  public const int ULONG = 331;
  public const int UNCHECKED = 332;
  public const int UNSAFE = 333;
  public const int USHORT = 334;
  public const int USING = 335;
  public const int VIRTUAL = 336;
  public const int VOID = 337;
  public const int VOLATILE = 338;
  public const int WHILE = 339;
  public const int GET = 340;
  public const int get = 341;
  public const int SET = 342;
  public const int set = 343;
  public const int OPEN_BRACE = 344;
  public const int CLOSE_BRACE = 345;
  public const int OPEN_BRACKET = 346;
  public const int CLOSE_BRACKET = 347;
  public const int OPEN_PARENS = 348;
  public const int CLOSE_PARENS = 349;
  public const int DOT = 350;
  public const int COMMA = 351;
  public const int COLON = 352;
  public const int SEMICOLON = 353;
  public const int TILDE = 354;
  public const int PLUS = 355;
  public const int MINUS = 356;
  public const int BANG = 357;
  public const int ASSIGN = 358;
  public const int OP_LT = 359;
  public const int OP_GT = 360;
  public const int BITWISE_AND = 361;
  public const int BITWISE_OR = 362;
  public const int STAR = 363;
  public const int PERCENT = 364;
  public const int DIV = 365;
  public const int CARRET = 366;
  public const int INTERR = 367;
  public const int OP_INC = 368;
  public const int OP_DEC = 370;
  public const int OP_SHIFT_LEFT = 372;
  public const int OP_SHIFT_RIGHT = 374;
  public const int OP_LE = 376;
  public const int OP_GE = 378;
  public const int OP_EQ = 380;
  public const int OP_NE = 382;
  public const int OP_AND = 384;
  public const int OP_OR = 386;
  public const int OP_MULT_ASSIGN = 388;
  public const int OP_DIV_ASSIGN = 390;
  public const int OP_MOD_ASSIGN = 392;
  public const int OP_ADD_ASSIGN = 394;
  public const int OP_SUB_ASSIGN = 396;
  public const int OP_SHIFT_LEFT_ASSIGN = 398;
  public const int OP_SHIFT_RIGHT_ASSIGN = 400;
  public const int OP_AND_ASSIGN = 402;
  public const int OP_XOR_ASSIGN = 404;
  public const int OP_OR_ASSIGN = 406;
  public const int OP_PTR = 408;
  public const int LITERAL_INTEGER = 410;
  public const int LITERAL_FLOAT = 412;
  public const int LITERAL_DOUBLE = 414;
  public const int LITERAL_DECIMAL = 416;
  public const int LITERAL_CHARACTER = 418;
  public const int LITERAL_STRING = 420;
  public const int IDENTIFIER = 422;
  public const int LOWPREC = 423;
  public const int UMINUS = 424;
  public const int HIGHPREC = 425;
  public const int yyErrorCode = 256;
 }
 namespace yyParser {
  using System;
  /** thrown for irrecoverable syntax errors and stack overflow.
    */
  public class yyException : System.Exception {
    public yyException (string message) : base (message) {
    }
  }

  /** must be implemented by a scanner object to supply input to the parser.
    */
  public interface yyInput {
    /** move on to next token.
        @return false if positioned beyond tokens.
        @throws IOException on input error.
      */
    bool advance (); // throws java.io.IOException;
    /** classifies current token.
        Should not be called if advance() returned false.
        @return current %token or single character.
      */
    int token ();
    /** associated with current token.
        Should not be called if advance() returned false.
        @return value for token().
      */
    Object value ();
  }
 }
} // close outermost namespace, that MUST HAVE BEEN opened in the prolog
