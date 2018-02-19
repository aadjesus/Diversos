/// Author: Roger Villela 
/// Contact: Roger.Villela@live.com 
/// LinkedIn: http://br.linkedin.com/in/rogeralvesvillela 
/// Blog: http://rogervillelajournal.wordpress.com
/// Products: Microsoft Visual Studio /Microsoft Visual C# 
/// Programming Language: C# 
/// 
/// public Boolean Equals( Boolean value )
/// 
/// DESCRIÇÃO 
/// 
/// Compara a instância atual(this) com outro objeto Boolean e indica se são 
/// iguais ou não.
/// 
/// NOTA: Este método é a implementação de IEquatable<T>.Equals(T).
/// Sendo assim, não sofre a penalização de boxing/unboxing em relação
/// ao outro método Equals( Object obj ) implementado por Boolean.

#region Namespace
using System;
#endregion

namespace Equals00 {

    public class Program {

	    #region Common Messages
        //public static readonly String PauseMessage	= "Pressione <ENTER> para continuar...";
	    public static readonly String EndMessage	= "Pressione <ENTER> para terminar...";
	    #endregion


	   static void Main( String[] args ) {

		  // Resultados
		  /*
		   * true: ambos são iguais( ou são false ou são true ).
		   * false: Um é false e o outro não.
		   **/

		  // Em CIL - Common Intermediate Language, a chamada é essa:
		  // call       instance bool [mscorlib]System.Boolean::Equals(bool)
		  // Veja o exemplo do método Equals( Object obj ) para confirmar que ele
		  // utiliza boxing/unboxing. Isto é citado na documentação do método
		  // IEquatable<T>.Equals(T).

		  Boolean instance = true;
		  Boolean value = true;

		  Console.WriteLine( "instance: true x value: true" );
		  Console.WriteLine( "Result: {0}", instance.Equals( value ).ToString() );

		  instance = false;
		  value = false;
		  Console.WriteLine( "instance: false x value: false" );
		  Console.WriteLine( "Result: {0}", instance.Equals( value ).ToString() );

		  value = false;
		  instance = true;
		  Console.WriteLine( "instance: true x value: false" );
		  Console.WriteLine( "Result: {0}", instance.Equals( value ).ToString() );

		  Console.WriteLine( "\n {0}", Program.EndMessage );
		  Console.ReadKey();            
	   }

    }

}
