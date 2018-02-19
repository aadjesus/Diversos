using System;
using System.Collections.Generic;
using System.Text;

namespace CS.OpenMind.Example01 {
/// <summary>
/// C�digo de exemplo para o artigo sobre Assembly e Managed Module.
/// </summary>
/// <Author>Roger Villela - .NET Technical Advisor/.NET Specialist - Core Platform</Author>
/// <AuthorMail>RogerVillela@gmail.com</AuthorMail>
/// <Company>Open Mind Consultoria em Inform�tica Ltda</Company>
/// <Client>.NET Magazine/DevMedia</Client>
/// <ProjectName>CS.OpenMind.Example01</ProjectName>
/// <DeveloperTeam>   
/// Roger Villela / rogervillela@gmail.cm
/// </DeveloperTeam>
/// <ProgrammingLanguage>C#</ProgrammingLanguage>
/// <CompilerVersion>Microsoft Visual C# 2008 Compiler version 3.5.21022.8</CompilerVersion>
/// <DotNETFrameworkVersion>3.5</DotNETFrameworkVersion>
/// <WorkstationOperatingSystem>
/// Windows Server 2003 Standard Edition, Service Pack 1/
/// Windows Vista Ultimate Edition with Service Pack 1/
/// Windows XP Service Pack 2
/// </WorkstationOperatingSystem>
/// <IDE>Microsoft Visual Studio 2008 Team System/Visual C# 2008</IDE>    
/// <History>    
/// C�digo de exemplo para demonstrar as estruturas internas de um Managed Module.
/// <ModifiedOn>
///     S�bado, 24 de Maio de 2008.
///     <Reason>Cria��o do c�digo de exemplo/Revis�o.</Reason>
/// </ModifiedOn>
/// </History>
    public class Example01 {
        /// <summary>
        /// This is a simple method that shows a message in Console.
        /// </summary>
        public void SomeMethod() {
            Console.WriteLine( " SomeMethod Message " );
        }

        /// <summary>
        /// This is a another simple method that shows a message in Console.
        /// </summary>
        public void AnotherMethod() {
            Console.WriteLine( " AnotherMethod Message " );
        }

        public static void Main( string[] args ) {
            Example01 example = new Example01();
            example.SomeMethod();
            example = null;
        }
    }
}
