/// Author: Roger Villela 
/// Contact: Roger.Villela@live.com 
/// LinkedIn: http://br.linkedin.com/in/rogeralvesvillela 
/// Blog: http://rogervillelajournal.wordpress.com
/// Products: Microsoft Visual Studio /Microsoft Visual C# 
/// Programming Language: C# 
/// 
/// public override String ToString()
/// 
/// DESCRIÇÃO 
/// 
/// Este método sobrescreve Object.ToString.


#region Namespaces
using System;
#endregion

namespace ToString00 {

    public class Program {

		#region Common Messages
        //public static readonly String PauseMessage	= "Pressione <ENTER> para continuar...";
	    public static readonly String EndMessage	= "Pressione <ENTER> para terminar...";
	    #endregion

	   static void Main( String[] args ) {

		  Boolean value = true;

		  // Retorna o valor do campo Boolean.TrueString
		  Console.WriteLine( "{0}", value.ToString() );

		  value = false;
		  // Retorna o valor do campo Boolean.FalseString
		  Console.WriteLine( "{0}", value.ToString() );

		  Console.WriteLine( "\n {0}", Program.EndMessage );
		  Console.ReadKey();
	   }

    }

}
