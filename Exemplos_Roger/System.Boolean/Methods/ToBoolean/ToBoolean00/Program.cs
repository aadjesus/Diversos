/// Author: Roger Villela 
/// Contact: Roger.Villela@live.com 
/// LinkedIn: http://br.linkedin.com/in/rogeralvesvillela 
/// Blog: http://rogervillelajournal.wordpress.com
/// Products: Microsoft Visual Studio /Microsoft Visual C# 
/// Programming Language: C# 
/// 
/// Boolean ToBoolean( IFormatProvider provider )
/// 
/// DESCRIÇÃO 
/// 
/// Converte, se possível, o valor da instância numa representação suportada pelo tipo Boolean.
/// Ou seja, true ou false.
///
/// Este método é uma implementação explícita (explicit interface implementation) de
/// IConvertible. Estes são os métodos de IConvertible implementados por Boolean:
/// - IConvertible.ToBoolean
/// - IConvertible.ToByte
/// - IConvertible.ToChar
/// - IConvertible.ToDateTime
/// - IConvertible.ToDecimal
/// - IConvertible.ToDouble
/// - IConvertible.ToInt16
/// - IConvertible.ToInt32
/// - IConvertible.ToInt64
/// - IConvertible.ToSByte
/// - IConvertible.ToSingle
/// - IConvertible.ToType
/// - IConvertible.ToUInt16
/// - IConvertible.ToUInt32
/// - IConvertible.ToUInt64
/// 
/// Nem todos estes métodos realizam a conversão. Alguns podem apenas lançar uma exception, como
/// a InvalidCastException indicando que a conversão não é suportada.

#region Namespaces
using System;
#endregion


namespace ToBoolean00 {

    public class Program {

	    #region Common Messages
        //public static readonly String PauseMessage	= "Pressione <ENTER> para continuar...";
	    public static readonly String EndMessage	= "Pressione <ENTER> para terminar...";
	    #endregion

	   static void Main( String[] args ) {


		  Boolean value = true;

		  /*
		   * Como é uma explicit interface implementation é preciso realizar o casting.
		   **/

		  // O propósito dos métodos de IConvertible, além da conversão é óbvio,
		  // é poder gerar uma saída formatada de acordo com a cultura definida pelo programa
		  // ou pelo sistema operacional.
		  Console.WriteLine( "{0}", ( (IConvertible)value ).ToBoolean( null ).ToString() );

		  Console.WriteLine( "\n {0}", Program.EndMessage );
		  Console.ReadKey();
	   }

    }

}
