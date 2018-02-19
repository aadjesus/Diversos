/// Author: Roger Villela 
/// Contact: Roger.Villela@live.com 
/// LinkedIn: http://br.linkedin.com/in/rogeralvesvillela 
/// Blog: http://rogervillelajournal.wordpress.com
/// Products: Microsoft Visual Studio /Microsoft Visual C# 
/// Programming Language: C# 
/// 
/// public static readonly String FalseString
/// 
/// DESCRIÇÃO 
/// 
/// Retorna a palavra False (com a primeira letra em maiúscula).

#region Namespaces
using System;
#endregion

namespace FalseString00 {

    public class Program {

		#region Common Messages
         //static readonly String PauseMessage	  = "Pressione <ENTER> para continuar...";
		 static readonly String EndMessage      = "Pressione <ENTER> para terminar...";
		#endregion

		static void Main( String[] args ) {

		  Console.WriteLine( "{0}", Boolean.FalseString );
		  // Para comprovar que é um valor string
		  Console.WriteLine( "{0}", "False" == Boolean.FalseString );

		  Console.WriteLine( "\n{0}", Program.EndMessage );
		  Console.ReadKey();

	   }

    }
}
