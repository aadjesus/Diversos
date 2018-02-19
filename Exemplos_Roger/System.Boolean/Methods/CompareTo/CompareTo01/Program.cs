/// Author: Roger Villela 
/// Contact: Roger.Villela@live.com 
/// LinkedIn: http://br.linkedin.com/in/rogeralvesvillela 
/// Blog: http://rogervillelajournal.wordpress.com
/// Products: Microsoft Visual Studio /Microsoft Visual C# 
/// Programming Language: C# 
/// 
/// public Int32 CompareTo( Object obj )
/// 
/// DESCRIÇÃO 
/// 
/// Compara a instância atual(this) com outro objeto Boolean e 
/// retorna um valor inteiro indicando a relação entre eles.
/// 
/// NOTA: Este método é a implementação de IComparable.CompareTo(Object).
/// Sendo assim, sofre da penalização de boxing/unboxing em relação 
/// ao outro método IComparable<T>.CompareTo( T ) implementado por Boolean, que não
/// sofre deste problema.
/// 

#region Namespace
using System;
#endregion


namespace CompareTo01 {

    public class Program {

	    #region Common Messages
        //public static readonly String PauseMessage	= "Pressione <ENTER> para continuar...";
	    public static readonly String EndMessage	= "Pressione <ENTER> para terminar...";
	    #endregion

	   static void Main( String[] args ) {


		  // Resultados
		  /*
		   * < 0: this é false e o parâmetro obj é true.
		   * == 0: ambos tem o mesmo valor ( ou são false ou são true ).
		   * > 0: this é true e o parâmetro obj é false OU null.
		   **/

		  // Em CIL - Common Intermediate Language, a chamada é essa:
		  // call       instance int32 [mscorlib]System.Boolean::CompareTo(object)
		  // Antes, a atribuição da instância de Boolean para Object, gera este CIL:
		  // box        [mscorlib]System.Boolean
		  // Veja o exemplo do método CompareTo( Boolean ) para confirmar que ele
		  // não utiliza boxing/unboxing. Isto é citado na documentação do método
		  // IComparable<T>.CompareTo(T).

		  Boolean instance = false;
		  Object obj = true;

		  Console.WriteLine( "instance: false x obj: true" );
		  Console.WriteLine( "Result: {0}", instance.CompareTo( obj ).ToString() );

		  instance = true;
		  Console.WriteLine( "instance: true x obj: true" );
		  Console.WriteLine( "Result: {0}", instance.CompareTo( obj ).ToString() );

		  obj = null;
		  Console.WriteLine( "instance: true x obj: null" );
		  Console.WriteLine( "Result: {0}", instance.CompareTo( obj ).ToString() );

		  Console.WriteLine( "\n {0}", Program.EndMessage );
		  Console.ReadKey();
	   }

    }

}
