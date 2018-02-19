/// Author: Roger Villela 
/// Contact: Roger.Villela@live.com 
/// LinkedIn: http://br.linkedin.com/in/rogeralvesvillela 
/// Blog: http://rogervillelajournal.wordpress.com
/// Products: Microsoft Visual Studio /Microsoft Visual C# 
/// Programming Language: C# 
/// 
/// public override Boolean Equals( Object obj )
/// 
/// DESCRIÇÃO 
/// 
/// Compara a instância atual(this) com outro objeto Boolean e indica se são 
/// iguais ou não.
/// 
/// NOTA: Este método sobrescreve de Object.Equals(Object).
/// Sendo assim, sofre a penalização de boxing/unboxing em relação 
/// ao outro método IEquatable<T>.Equals( T ) implementado por Boolean.

#region Namespaces
using System;
#endregion

namespace Equals01 {

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
		  // call       instance bool [mscorlib]System.Boolean::Equals(object)
		  // Antes, a atribuição da instância de Boolean para Object, gera este CIL:
		  // box        [mscorlib]System.Boolean
		  // Veja o exemplo do método Equals( Boolean ) para confirmar que ele
		  // não utiliza boxing/unboxing. Isto é citado na documentação do método
		  // IEquatable<T>.Equals(T).

		  Boolean instance = true;
		  Object obj = true;

		  Console.WriteLine( "instance: true x obj: true" );
		  Console.WriteLine( "Result: {0}", instance.Equals( obj ).ToString() );

		  instance = false;
		  obj = false;
		  Console.WriteLine( "instance: false x obj: false" );
		  Console.WriteLine( "Result: {0}", instance.Equals( obj ).ToString() );

		  
		  instance = true;
		  obj = false;
		  Console.WriteLine( "instance: true x obj: false" );
		  Console.WriteLine( "Result: {0}", instance.Equals( obj ).ToString() );
		  
		  instance = true;
		  obj = null;
		  Console.WriteLine( "instance: true x obj: null" );
		  Console.WriteLine( "Result: {0}", instance.Equals( obj ).ToString() );

		  Console.WriteLine( "\n {0}", Program.EndMessage );
		  Console.ReadKey();
	   }

    }

}

