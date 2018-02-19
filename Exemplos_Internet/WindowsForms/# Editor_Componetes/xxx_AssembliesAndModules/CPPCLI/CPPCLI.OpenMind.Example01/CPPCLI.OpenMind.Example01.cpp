/// <summary>
/// Exemplo de código em C++/CLI para ilustrar o conceito de Assembly e Managed Module.
/// </summary>
/// <Author>Roger Villela - .NET Technical Advisor/.NET Specialist - Core Platform</Author>
/// <AuthorMail>RogerVillela@gmail.com</AuthorMail>
/// <Company>Open Mind Consultoria em Informática Ltda</Company>
/// <Client>.NET Magazine/DevMedia</Client>
/// <ProjectName>CPPCLI.OpenMind.Example01</ProjectName>
/// <DeveloperTeam>   
/// Roger Villela / rogervillela@gmail.com
/// </DeveloperTeam>
/// <ProgrammingLanguage>C++/CLI</ProgrammingLanguage>
/// <CompilerVersion>Visual C++ 2008 Compiler version 15.00.21022.08 for 80x86</CompilerVersion>
/// <DotNETFrameworkVersion>3.5</DotNETFrameworkVersion>
/// <WorkstationOperatingSystem>
/// - Windows Server 2003 Standard Edition, Service Pack 1
/// - Windows Vista Ultimate Edition with Service Pack1
/// - Windows XP Service Pack 2 
/// </WorkstationOperatingSystem>
/// <IDE>Microsoft Visual Studio 2008 Team System/Visual C++ 2008</IDE>    
/// <History>    
/// Criado para demonstrar o conceito de Assembly e Managed Module.
/// <ModifiedOn>
///     Sábado, 24 de Maio de 2008.
///     <Reason>Implementação de métodos para demonstrar os conceitos de Assembly e Managed Module.
///     </Reason>
/// </ModifiedOn>
/// </History>
#pragma region Headers
#include "stdafx.h"
#include "CPPCLI.OpenMind.Example01.h"
#pragma endregion

#pragma region Namespace for class definitions
using namespace CPPCLIOpenMindExample01;
#pragma endregion


///<summary>
/// This is a simple method that shows a message in Console.
///</summary>
void Example01::SomeMethod() {
	Console::WriteLine( " SomeMethod Message " );
}



///<summary>
/// This is a another simple method that shows a message in Console.
///</summary>
void Example01::AnotherMethod() {
	Console::WriteLine( "AnotherMethod Message " );
}

