/// Author: Roger Villela 
/// Contact: Roger.Villela@live.com 
/// LinkedIn: http://br.linkedin.com/in/rogeralvesvillela 
/// Blog: http://rogervillelajournal.wordpress.com
/// Products: Microsoft Visual Studio /Microsoft Visual C# 
/// Programming Language: C# 
/// 
/// public static Boolean TryParse( String value, out Boolean result )
/// 
/// DESCRIÇÃO 
/// 
/// Tenta converter o valor informado no parâmetro value para Boolean.
/// Este método realiza a mesma função que Boolean.Parse. 
/// No entanto, ele tem um algoritmo mais eficiente, conforme citado na documentação.
/// Também não lança exception quando não consegue converter. Isto torna o código
/// mais robusto e mais fácil de se trabalhar.


#region Namespaces
using System;
#endregion


namespace TryParse00 {

    public class Program {

		#region Common Messages
        //public static readonly String PauseMessage	= "Pressione <ENTER> para continuar...";
	    public static readonly String EndMessage	= "Pressione <ENTER> para terminar...";
	    #endregion

	   static void Main( String[] args ) {

		  String value = " true ";
		  //String value = " false ";
		  //String value = "";
		  // Como é um value type, o CLR inicializa automaticamente.
		  Boolean result;        

		  
		  /*
		  * O valor a ser convertido deve ser igual ao valor
		  * de Boolean.FalseString ou ao valor de Boolean.TrueString.
		  * A comparação é case-insensitive.
		  * A string pode ter espaços antes e/ou depois.           
		  **/

		  if ( Boolean.TryParse( value, out result ) ) Console.WriteLine( "Value converted: {0}", result );
		  else Console.WriteLine( "Value not converted." );		  
		  
		  Console.WriteLine( "\n {0}", Program.EndMessage );
		  Console.ReadKey();
	   }

    }

}
