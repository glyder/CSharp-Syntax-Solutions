using BenchmarkDotNet.Running;

//Console.WriteLine("C# 11 Syntax - Tester!");
//Console.WriteLine("=======================\n");

//Benchmarks
//BenchmarkRunner.Run<SpanOfT>(); 

//Smart Programming
//-------------------
//SpanOfT, StringSpanBenchmark, NameParserBenchmarks
//BenchmarkRunner.Run<SpanOfT>();
new SpanOfT().Run();

// C# ??
//------
//InterporlatedStrings.Run();
//NullableReferenceTypes.Run();


// C# 8
//-----
//Ranges.Run();

// C# 9
//------
//Record.Run();
//PatternMatching.Run();

// C# 10
//------
//Deconstruction.Run();

// C# 11
//------

// C# 12
//------


// Design Patterns
// DesignPatterns.Run();

//Console.ReadKey();

/*
 
11
Raw string literals
Generic math support
Generic attributes
UTF-8 string literals
Newlines in string interpolation expressions
List patterns
File-local types
Required members
Auto-default structs
Pattern match Span<char> on a constant string
Extended nameof scope
Numeric IntPtr
ref fields and scoped ref
Improved method group conversion to delegate
Warning wave 7

10
Record structs
Improvements of structure types
Interpolated string handlers
global using directives
File-scoped namespace declaration
Extended property patterns
Improvements on lambda expressions
Allow const interpolated strings
Record types can seal ToString()
Improved definite assignment
Allow both assignment and declaration in the same deconstruction
Allow AsyncMethodBuilder attribute on methods
CallerArgumentExpression attribute
Enhanced #line pragma
Warning wave 6

9
Records
Init only setters
Top-level statements
Pattern matching enhancements
Performance and interop
Native sized integers
Function pointers
Suppress emitting localsinit flag
Fit and finish features
Target-typed new expressions
static anonymous functions
Target-typed conditional expressions
Covariant return types
Extension GetEnumerator support for foreach loops
Lambda discard parameters
Attributes on local functions
Support for code generators
Module initializers
New features for partial methods
Warning wave 5

*/
