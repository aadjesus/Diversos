// CommonUtil.cs: Different utils
//
// Written by IZ for purposes of CS CODEDOM Parser, 2002
//(c) 2002 Ivan Zderadicka (ivan.zderadicka@seznam.cz)

using System;
using System.CodeDom;
using System.Text.RegularExpressions;

namespace IvanZ.CSParser
{
	/// <summary>
	/// Summary description for CommonUtil.
	/// </summary>
	public class CommonUtil
	{
		public static int CountChars(string s, char c)
		{
			char[] ca=s.ToCharArray();
			int count=0;
			for (int i=0; i<ca.Length;i++)
			{
				if (ca[i]==c)
					count++;
			}
			return count;
		}


        private static Regex re1= new Regex(@"\[([,]*)\]",RegexOptions.Compiled);
		public static CodeTypeReference CreateArrayType(CodeTypeReference type, string rank)
		{
			CodeTypeReference nr;
			if (rank!="" && rank!=null)
			{
				
				Match m= re1.Match(rank);
				string firstRank=m.Value;
				int r=CountChars(firstRank,',')+1;
				rank= rank.Substring(m.Index+firstRank.Length);
				
				rank=rank.Trim();
				nr= new CodeTypeReference(	
				CreateArrayType(type,rank),r);
				
			}
			
			else
				nr= type;
			return nr;


		}

		
	}
}
