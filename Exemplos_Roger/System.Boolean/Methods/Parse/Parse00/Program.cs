/// Author: Roger Villela 
/// Contact: Roger.Villela@live.com 
/// LinkedIn: http://br.linkedin.com/in/rogeralvesvillela 
/// Blog: http://rogervillelajournal.wordpress.com
/// Products: Microsoft Visual Studio /Microsoft Visual C# 
/// Programming Language: C# 
/// 
/// public static Boolean Parse( String value )
/// 
/// DESCRIÇÃO 
/// 
/// Converte a representação string no equivalente Boolean.

#region Namespaces
using System;
#endregion

namespace Parse00 {

    public class Program {

	    #region Common Messages
        //public static readonly String PauseMessage	= "Pressione <ENTER> para continuar...";
	    public static readonly String EndMessage	= "Pressione <ENTER> para terminar...";
	    #endregion

	   static void Main( String[] args ) {


		   Boolean result = true;           
           String exceptionMessage = null;     
           String value; 

		  try {

			 /*
			  * O valor a ser convertido deve ser igual ao valor
			  * de Boolean.FalseString ou ao valor de Boolean.TrueString.
			  * A comparação é case-insensitive.
			  * A string pode ter espaços antes e/ou depois.
			  * Qualquer outro valor na string gera as exceptions apresentadas
			  * no código.
			  **/
              
              value = " true ";
              //value = " false ";
              //value = String.Empty;
              
			  result = Boolean.Parse( value );

		  } catch ( ArgumentNullException argumentNull ) {
			 exceptionMessage = argumentNull.Message;
		  } catch ( FormatException format ) {
			 exceptionMessage = format.Message;
		  }

		  if ( exceptionMessage != null ) Console.WriteLine( "Exception: {0}", exceptionMessage );
		  else Console.WriteLine( "{0}", result );
		  

		  Console.WriteLine( "\n {0}", Program.EndMessage );
		  Console.ReadKey();
	   }

    }

}
