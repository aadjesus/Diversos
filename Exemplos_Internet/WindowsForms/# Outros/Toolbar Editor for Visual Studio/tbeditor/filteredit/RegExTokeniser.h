#pragma once

#include "DfaRegExTypedefs.h"

// NOTE: std::exception only deals with char *.
// Assign to a CString for automatic conversion.

class CRegExTokeniser
{
public:
	enum e_LexToken {eLexBEGIN = 1, eLexANY, eLexZEROORMORE, eLexCHARSET,
		eLexNEGCHARSET, eLexREPEATN, eLexONEORMORE, eLexOPTIONAL, eLexOR,
		eLexOPENPAREN, eLexCLOSEPAREN, eLexEND};
	TulList m_TokenList;
	TStrList m_StrTokenList;
#ifdef _DEBUG
	TString m_strRegEx;
#endif

	CRegExTokeniser (const TCHAR *pszRegEx);
#ifdef _DEBUG
	TString GetAsText () const;
#endif

private:
	static const bool g_bGrammarTable[eLexEND][eLexEND];
	unsigned int m_uiStrIndex;
	int m_iParenthesisCount;

	void GetNextToken (const TCHAR * &pszRegEx, e_LexToken &eToken,
		TString &strToken);
	void DecodeEscapeSequence (const TCHAR * &pszRegEx, TString &strToken);
	void DecodeOctal (const TCHAR * &pszRegEx, TString &strToken);
	void DecodeControlChar (const TCHAR * &pszRegEx, TString &strToken);
	void DecodeHex (const TCHAR * &pszRegEx, TString &strToken,
		const int iChars);
	void ExpandCharSet (const TCHAR * &pszRegEx, TString &strToken);
	void ValidateRepeatN (const TCHAR * &pszRegEx, TString &strToken);
	void CheckChar (const TCHAR * pszRegEx, const TCHAR *pszValidChars) const;
	void ExpandOneOrMore ();
	void ExpandRepeatN (const TString &strRepeatN);
	void LocateBlockStart (TulList::iterator &TokenIter,
		TStrList::iterator &StrTokenIter) const;
	std::string ConvertStrToANSI (const TString &str) const;

#ifdef _DEBUG
	void AppendToken (const e_LexToken eToken, const TString &strToken);
#endif
};
