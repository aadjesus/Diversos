using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using CPPCLIOpenMindExample01;

namespace CSHARP.Test.Application {
/// <summary>
/// Exemplo de código em C# para ilustrar o conceito de Assembly e Managed Module.
/// </summary>
/// <Author>Roger Villela - .NET Technical Advisor/.NET Specialist - Core Platform</Author>
/// <AuthorMail>RogerVillela@gmail.com</AuthorMail>
/// <Company>Open Mind Consultoria em Informática Ltda</Company>
/// <Client>.NET Magazine/DevMedia</Client>
/// <ProjectName>CSHARP.Test.Application</ProjectName>
/// <DeveloperTeam>   
/// Roger Villela / rogervillela@gmail.com
/// </DeveloperTeam>
/// <ProgrammingLanguage>C#</ProgrammingLanguage>
/// <CompilerVersion>Visual C# 2008 Compiler version 3.5.21022.8</CompilerVersion>
/// <DotNETFrameworkVersion>3.5</DotNETFrameworkVersion>
/// <WorkstationOperatingSystem>
/// - Windows Server 2003 Standard Edition, Service Pack 1
/// - Windows Vista Ultimate Edition with Service Pack1
/// - Windows XP Service Pack 2 
/// </WorkstationOperatingSystem>
/// <IDE>Microsoft Visual Studio 2008 Team System/Visual C# 2008</IDE>    
/// <History>    
/// Criado para demonstrar o conceito de Assembly and Managed Module.
/// <ModifiedOn>
    ///     Sábado, 24 de Maio de 2008.
///     <Reason>Implementação de métodos para demonstrar os conceitos de Assembly e Managed Module.
///     </Reason>
/// </ModifiedOn>
/// </History>
    public class Program {
        static void Main( string[] args ) {
            Example01 test = new Example01();

            test.SomeMethod();
            test.AnotherMethod();

            test = null;

            #region Show Information About Modules and Assemblies
            Console.WriteLine( "{0}{1}", Environment.NewLine, "Modules and Assemblies" );
            Program.ShowInfoAboutModulesAndAssemblies();

            #endregion

        }


        /// <summary>
        /// Show information about modules and assemblies for Program class type.
        /// </summary>
        static void ShowInfoAboutModulesAndAssemblies() {

            Module module = typeof(Program).Module;

            Console.WriteLine( "Module Name: {0}{1}", module.Name, Environment.NewLine );
            Console.WriteLine( "Assembly FullName {0} {1}:", module.Assembly.FullName.ToString(), Environment.NewLine );

            AssemblyName[] referencedAssemblies = module.Assembly.GetReferencedAssemblies();

            foreach (AssemblyName assemblyName in referencedAssemblies) {
                Console.WriteLine( "Referenced Assembly FullName: {0} {1}", assemblyName.FullName, Environment.NewLine );
            }
        }
        
    }
}
