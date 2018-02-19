/// Author: Roger Villela 
/// Contact: Roger.Villela@live.com 
/// LinkedIn: http://br.linkedin.com/in/rogeralvesvillela 
/// Blog: http://rogervillelajournal.wordpress.com
/// Products: Microsoft Visual Studio /Microsoft Visual C# 
/// Programming Language: C# 
/// 
/// Object ToType( Type type, IFormatProvider provider )
/// 
/// DESCRIÇÃO 
/// 
/// Converte, se possível, o valor da instância numa representação suportada pelo tipo indicado por type.
/// 
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

namespace ToType00 {

    public class Program {

		#region Common Messages
        //public static readonly String PauseMessage	= "Pressione <ENTER> para continuar...";
	    public static readonly String EndMessage	= "Pressione <ENTER> para terminar...";
	    #endregion

	   static void Main( String[] args ) {

		  //Boolean value = true;
		  Boolean value = false;

		  /*
		   * Como é uma explicit interface implementation é preciso realizar o casting.
		   **/

		  // O propósito dos métodos de IConvertible, além da conversão é óbvio,
		  // é poder gerar uma saída formatada de acordo com a cultura definida pelo programa
		  // ou pelo sistema operacional.
		  try {

			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(Boolean), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(Byte), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(Char), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(DateTime), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(Decimal), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(Double), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(Int16),  null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(Int32), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(Int64), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(SByte), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(Single), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(UInt16), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(UInt32), null ).ToString() );
			 Console.WriteLine( "{0}", ( (IConvertible)value ).ToType( typeof(UInt64), null ).ToString() );
		  } catch ( InvalidCastException invalidCast ) {
			 Console.WriteLine( "Invalid cast: {0}", invalidCast.Message );
		  }


		  Console.WriteLine( "\n {0}", Program.EndMessage );
		  Console.ReadKey();
	   }

    }

}
