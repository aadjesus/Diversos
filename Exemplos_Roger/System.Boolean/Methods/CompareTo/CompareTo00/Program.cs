/// Author: Roger Villela 
/// Contact: Roger.Villela@live.com 
/// LinkedIn: http://br.linkedin.com/in/rogeralvesvillela 
/// Blog: http://rogervillelajournal.wordpress.com
/// Products: Microsoft Visual Studio /Microsoft Visual C# 
/// Programming Language: C# 
/// 
/// public Int32 CompareTo( Boolean value )
/// 
/// DESCRIÇÃO 
/// 
/// Compara a instância atual(this) com outro objeto Boolean e 
/// retorna um valor inteiro indicando a relação entre eles.
/// 
/// NOTA: Este método é a implementação de IComparable<T>.CompareTo(T).
/// Sendo assim, não sofre a penalização de boxing/unboxing em relação
/// ao outro método CompareTo( Object obj ) implementado por Boolean.

#region Namespaces
using System;
#endregion


namespace CompareTo00 {

    public class Program {

	    #region Common Messages
        //public static readonly String PauseMessage	= "Pressione <ENTER> para continuar...";
	    public static readonly String EndMessage	    = "Pressione <ENTER> para terminar...";
	    #endregion


	   static void Main( String[] args ) {


		  // Resultados
		  /*
		   * < 0: this é false e o parâmetro value é true.
		   * == 0: ambos tem o mesmo valor ( ou são false ou são true ).
		   * > 0: this é true e o parâmetro value é false.
		   **/

		  // Em CIL - Common Intermediate Language, a chamada é essa:
		  // call       instance int32 [mscorlib]System.Boolean::CompareTo(bool)
		  // Veja o exemplo do método CompareTo( Object obj ) para confirmar que ele
		  // utiliza boxing/unboxing. Isto é citado na documentação do método
		  // IComparable<T>.CompareTo(T).

		  Boolean instance = false;
		  Boolean value = true;

		  Console.WriteLine( "instance: false x value: true" );
		  Console.WriteLine( "Result: {0}" , instance.CompareTo( value ).ToString() );

		  instance = true;
		  Console.WriteLine( "instance: true x value: true" );
		  Console.WriteLine( "Result: {0}", instance.CompareTo( value ).ToString() );

		  value = false;
		  Console.WriteLine( "instance: true x value: false" );
		  Console.WriteLine( "Result: {0}", instance.CompareTo( value ).ToString() );


		  Console.WriteLine( "\n {0}", Program.EndMessage );
		  Console.ReadKey();
	   }

    }

}
