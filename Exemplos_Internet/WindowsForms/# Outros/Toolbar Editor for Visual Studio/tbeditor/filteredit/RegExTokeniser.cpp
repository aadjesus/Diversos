#include "stdafx.h"

#include "RegExTokeniser.h"

#include <algorithm>
#include <sstream>

#ifdef _UNICODE
#define _tstoi _wtoi
#else
#define _tstoi atoi
#endif

// This class is used to parse the regex textual representation.
// Validation and pre-processing are performed and syntax and some
// small semantic checks are made.  It outputs a list of tokens.
//
// Throws a std::exception if there is an error (NOTE: std::exception only
// supports char * for its text describing the error - conversion required
// by the client application if it is a UNICODE build (CString will do this)).

// Tasks performed:
// - Tokenise input
//   - Validate and process escape sequences
//   - Validate and expand char sequences
//   - Validate repeat operator (plus semantic check: Min must not be > Max)
// - Expand operators '+' and '{n[,[n]]}'

static const TCHAR szDigits[] = _T("0123456789");
static const TCHAR szZeroOrMore[] = _T("*");
static const TCHAR szCloseSquare[] = _T("]");
static const TCHAR szCloseCurly[] = _T("}");
static const TCHAR szOptional[] = _T("?");
#ifdef _DEBUG
static const TCHAR szBeginToken[] = _T("<BEGIN>");
static const TCHAR szTokenList[] = _T("Token List:");
static const TCHAR szNewLine[] = _T("\n");
static const TCHAR szEndToken[] = _T("<END>");
#endif

// TOKENS:
//            - eBEGIN
// .          - eANY
// *          - eZEROORMORE
// [...]      - eCHARSET
// [^...]     - eNEGCHARSET
// {n,[,[n]]} - eREPEATN
// +          - eONEORMORE
// ?          - eOPTIONAL
// |          - eOR
// (          - eOPENPAREN
// )          - eCLOSEPAREN
//            - eEND

const bool CRegExTokeniser::g_bGrammarTable[eLexEND][eLexEND] = {
//     eBGN, eANY, e0MR, eCS, eNCS, eRPT, e1MR, eOPT, eOR, eOPEN, eCLS, eEND
/*BN*/{false,true,false,true,true, false,false,false,false,true, false,true},
/*AY*/{false,true, true, true,true, true, true, true, true, true, true, true},
/*0M*/{false,true, false,true,true, false,false,false,true, true, true, true},
/*CS*/{false,true, true, true,true, true, true, true, true, true, true, true},
/*NC*/{false,true, true, true,true, true, true, true, true, true, true, true},
/*RP*/{false,true, false,true,true, false,false,false,true, true, true, true},
/*1M*/{false,true, false,true,true, false,false,false,true, true, true, true},
/*OP*/{false,true, false,true,true, false,false,false,true, true, true, true},
/*OR*/{false,true, false,true,true, false,false,false,false,true, false,false},
/*OP*/{false,true, false,true,true, false,false,false,false,true, false, false},
/*CL*/{false,true, true, true,true, true, true, true, true, true, true, true},
/*ED*/{false,false,false,false,false,false,false,false,false,false,false,false}
};

// DESIGN DECISIONS:
// - Smart pointers are not used so as not to introduce a dependency
//   on the boost library.
// - To save memory, tokens are kept in their 'compressed' form until
//   NFA nodes are converted to DFA nodes (this also means that charsets
//   can be manipulated more efficiently).
// - Expansion of '+' and '{n[,[n]]}' is done here at the token level
//   for ease of debugging.

CRegExTokeniser::CRegExTokeniser (const TCHAR *pszRegEx) :
	m_uiStrIndex (0),
	m_iParenthesisCount (0)
{
	e_LexToken eToken = eLexBEGIN;
	TString strToken;

#ifdef _DEBUG
	strToken = szBeginToken;
#endif
	m_TokenList.push_back (eToken);
	m_StrTokenList.push_back (strToken);
	// Don't add "<BEGIN>" to m_strRegEx.

	do
	{
		GetNextToken (pszRegEx, eToken, strToken);

		if (!g_bGrammarTable[m_TokenList.back () - 1][eToken - 1])
		{
			// Always char, as std::exception does not handle wchar_t.
			std::stringstream ss;

			ss << "ERROR: '" << ConvertStrToANSI (m_StrTokenList.back ()) <<
				"' against '" << ConvertStrToANSI (strToken) <<
				"' invalid at position " << m_uiStrIndex << ".";
			throw std::exception (ss.str ().c_str ());
		}

		switch (eToken)
		{
			case eLexOPENPAREN:
				m_iParenthesisCount++;
				break;
			case eLexCLOSEPAREN:
				m_iParenthesisCount--;

				if (m_iParenthesisCount < 0)
				{
					// Always char, as std::exception does not handle wchar_t.
					std::stringstream ss;

					ss << "ERROR: Parenthesis count < 0 at position " <<
						m_uiStrIndex << ".";
					throw std::exception (ss.str ().c_str ());
				}
				break;
			case eLexONEORMORE:
				ExpandOneOrMore ();
				break;
			case eLexREPEATN:
				ExpandRepeatN (strToken);
				break;
		}

		if (eToken != eLexONEORMORE && eToken != eLexREPEATN)
		{
			m_TokenList.push_back (eToken);
			m_StrTokenList.push_back (strToken);
#ifdef _DEBUG
			if (eToken != eLexEND)
			{
				AppendToken (eToken, strToken);
			}
#endif
		}
	} while (eToken != eLexEND);

	if (m_iParenthesisCount != 0)
	{
		throw std::exception ("ERROR: Parenthesis count != 0.");
	}
}

#ifdef _DEBUG
TString CRegExTokeniser::GetAsText () const
{
	TString str;
	TStrList::const_iterator Iter = m_StrTokenList.begin ();

	str = szTokenList;
	str += szNewLine;

	while (Iter != m_StrTokenList.end ())
	{
		str += *Iter++ + szNewLine;
	}

	return str;
}
#endif

void CRegExTokeniser::GetNextToken (const TCHAR * &pszRegEx,
	e_LexToken &eToken, TString &strToken)
{
	strToken.erase ();

	if (*pszRegEx == '\\')
	{
		eToken = eLexCHARSET;
		DecodeEscapeSequence (pszRegEx, strToken);
	}
	else if (*pszRegEx == '[')
	{
		if (*(pszRegEx + 1) == '^')
		{
			eToken = eLexNEGCHARSET;
			pszRegEx++;
			m_uiStrIndex++;
		}
		else
		{
			eToken = eLexCHARSET;
		}

		ExpandCharSet (pszRegEx, strToken);

		_TUCHAR *pszStart = const_cast<_TUCHAR *> (reinterpret_cast
			<const _TUCHAR *> (strToken.c_str ()));
		_TUCHAR *pszEnd = pszStart + strToken.size ();
		// We rely on a sorted char set later.
		std::sort (pszStart, pszEnd);
	}
	else if (*pszRegEx == '{')
	{
		eToken = eLexREPEATN;
		ValidateRepeatN (pszRegEx, strToken);
	}
	else if (*pszRegEx == 0)
	{
		eToken = eLexEND;
#ifdef _DEBUG
		strToken = szEndToken;
#endif
	}
	else
	{
		switch (*pszRegEx)
		{
			case '.':
				eToken = eLexANY;
				break;
			case '?':
				eToken = eLexOPTIONAL;
				break;
			case '*':
				eToken = eLexZEROORMORE;
				break;
			case '+':
				eToken = eLexONEORMORE;
				break;
			case '|':
				eToken = eLexOR;
				break;
			case '(':
				eToken = eLexOPENPAREN;
				break;
			case ')':
				eToken = eLexCLOSEPAREN;
				break;
			default:
				eToken = eLexCHARSET;
				break;
		}

		strToken = *pszRegEx++;
		m_uiStrIndex++;
	}
}

void CRegExTokeniser::DecodeEscapeSequence (const TCHAR * &pszRegEx,
	TString &strToken)
{
	m_uiStrIndex++;

	switch (*++pszRegEx)
	{
		case 0:
			// Must have a least one char following
			throw std::exception ("ERROR: No chars following escape char "
				"('\\')");
			break;
		case '0':
		case '1':
		case '2':
		case '3':
		case '4':
		case '5':
		case '6':
		case '7':
		case '8':
		case '9':
			DecodeOctal (pszRegEx, strToken);
			break;
		case 'a':
			strToken = '\a';
			break;
		case 'b':
			strToken = '\b';
			break;
		case 'c':
			DecodeControlChar (pszRegEx, strToken);
			break;
		case 'e':
			strToken = '\\';
			break;
		case 'f':
			strToken = '\f';
			break;
		case 'n':
			strToken = '\n';
			break;
		case 'r':
			strToken = '\r';
			break;
		case 't':
			strToken = '\t';
			break;
		case 'u':
#ifdef _UNICODE
			DecodeHex (pszRegEx, strToken, 4);
#else
		{
			// Always char, as std::exception does not handle wchar_t.
			std::stringstream ss;

			ss << "ERROR: \\u not supported in ANSI build at position " <<
				m_uiStrIndex << ".";
			throw std::exception (ss.str ().c_str ());
		}
#endif
			break;
		case 'v':
			strToken = '\v';
			break;
		case 'x':
			DecodeHex (pszRegEx, strToken, 2);
			break;
		default:
			strToken = *pszRegEx;
			break;
	}

	pszRegEx++;
	m_uiStrIndex++;
}

void CRegExTokeniser::DecodeOctal (const TCHAR * &pszRegEx, TString &strToken)
{
	int iCount = 3;
	TCHAR c = 0;

	while (iCount)
	{
		c *= 8;

		if (*pszRegEx >= '0' && *pszRegEx <= '7')
		{
			c += *pszRegEx - '0';
		}
		else if (*pszRegEx == 0)
		{
			// Always char, as std::exception does not handle wchar_t.
			std::stringstream ss;

			ss << "ERROR: Unexpected end of string at position " <<
				m_uiStrIndex << ".";
			throw std::exception (ss.str ().c_str ());
		}
		else
		{
			// Always char, as std::exception does not handle wchar_t.
			std::stringstream ss;
			TString c;

			c = *pszRegEx;
			ss << "ERROR: Invalid octal character ('" << ConvertStrToANSI (c) <<
				"') at position " << m_uiStrIndex << ".";
			throw std::exception (ss.str ().c_str ());
		}

		iCount--;
		pszRegEx++;
		m_uiStrIndex++;
	}

	strToken = c;
}

void CRegExTokeniser::DecodeControlChar (const TCHAR * &pszRegEx, TString &strToken)
{
	pszRegEx++;
	m_uiStrIndex++;

	if (*pszRegEx == 0)
	{
		// Always char, as std::exception does not handle wchar_t.
		std::stringstream ss;

		ss << "ERROR: No chars following \"\\c\" at position " <<
			m_uiStrIndex << ".";
		throw std::exception (ss.str ().c_str ());
	}

	if (*pszRegEx >= 'a' && *pszRegEx <= 'z')
	{
		strToken = *pszRegEx - ('a' - 1);
	}
	else if (*pszRegEx >= 'A' && *pszRegEx <= 'Z')
	{
		strToken = *pszRegEx - ('A' - 1);
	}
	else
	{
		// Always char, as std::exception does not handle wchar_t.
		std::stringstream ss;
		TString c;

		c = *pszRegEx;
		ss << "ERROR: Invalid control character ('" << ConvertStrToANSI (c) <<
			"') at position " << m_uiStrIndex << ".";
		throw std::exception (ss.str ().c_str ());
	}
}

void CRegExTokeniser::DecodeHex (const TCHAR * &pszRegEx, TString &strToken,
	const int iChars)
{
	int iCount = iChars;
	TCHAR c = 0;

	pszRegEx++;
	m_uiStrIndex++;

	while (iCount)
	{
		c *= 16;

		if (*pszRegEx >= '0' && *pszRegEx <= '9')
		{
			c += *pszRegEx - '0';
		}
		else if (*pszRegEx >= 'a' && *pszRegEx <= 'f')
		{
			c += 10 + (*pszRegEx - 'a');
		}
		else if (*pszRegEx >= 'A' && *pszRegEx <= 'F')
		{
			c += 10 + (*pszRegEx - 'A');
		}
		else if (*pszRegEx == 0)
		{
			// Always char, as std::exception does not handle wchar_t.
			std::stringstream ss;

			ss << "ERROR: Unexpected end of string at position " <<
				m_uiStrIndex << ".";
			throw std::exception (ss.str ().c_str ());
		}
		else
		{
			// Always char, as std::exception does not handle wchar_t.
			std::stringstream ss;
			TString c;

			c = *pszRegEx;
			ss << "ERROR: Invalid hex character ('" << ConvertStrToANSI (c) <<
				"') at position " << m_uiStrIndex << ".";
			throw std::exception (ss.str ().c_str ());
		}

		iCount--;

		if (iCount != 0)
		{
			pszRegEx++;
			m_uiStrIndex++;
		}
	}

	strToken = c;
}

void CRegExTokeniser::ExpandCharSet (const TCHAR * &pszRegEx,
	TString &strToken)
{
	_TUCHAR cPrev = 0;

	pszRegEx++;
	m_uiStrIndex++;

	while (*pszRegEx && *pszRegEx != ']')
	{
		if (*pszRegEx == '\\')
		{
			TString strEscChar;

			DecodeEscapeSequence (pszRegEx,	strEscChar);
			cPrev = strEscChar[0];

			if (*pszRegEx != '-')
			{
				strToken += strEscChar;
			}
		}
		else
		{
			cPrev = *pszRegEx;

			if (*(pszRegEx + 1) != '-')
			{
				strToken += *pszRegEx;
			}

			pszRegEx++;
			m_uiStrIndex++;
		}

		if (*pszRegEx == '-')
		{
			if (*(pszRegEx + 1) == ']')
			{
				// Always char , as std::exception does not handle wchar_t.
				std::stringstream ss;

				ss << "ERROR: end of charset (']') invalid at position "
					<< m_uiStrIndex + 1 << ".";
				throw std::exception (ss.str ().c_str ());
			}

			pszRegEx++;
			m_uiStrIndex++;

			// Running off the end of the string will be caught by
			// the call to CheckChar (and an exception raised)
			if (*pszRegEx)
			{
				_TUCHAR cCur = *pszRegEx++;

				m_uiStrIndex++;

				if (cCur < cPrev)
				{
					std::swap (cPrev, cCur);
				}

				for (_TUCHAR c = cPrev; c <= cCur; c++)
				{
					strToken += c;
				}
			}
		}
	}

	CheckChar (pszRegEx++, szCloseSquare);
	m_uiStrIndex++;
}

// Only positive integers are accepted for Min and Max.
void CRegExTokeniser::ValidateRepeatN (const TCHAR * &pszRegEx,
	TString &strToken)
{
	unsigned int uiMin = 0;
	unsigned int uiMax = 0;

	m_uiStrIndex++;
	++pszRegEx;

	CheckChar (pszRegEx, szDigits);

	while (*pszRegEx >= '0' && *pszRegEx <= '9')
	{
		strToken += *pszRegEx++;
		m_uiStrIndex++;
	}

	uiMin = _tstoi (reinterpret_cast<const TCHAR *> (strToken.c_str ()));

	if (*pszRegEx == ',')
	{
		strToken += *pszRegEx++;
		m_uiStrIndex++;

		if (*pszRegEx != '}')
		{
			CheckChar (pszRegEx, szDigits);

			while (*pszRegEx >= '0' && *pszRegEx <= '9')
			{
				strToken += *pszRegEx++;
				m_uiStrIndex++;
			}

			const TCHAR *pszMax = reinterpret_cast<const TCHAR *> (strToken.c_str ());

			pszMax = _tcschr (pszMax, ',') + 1;
			uiMax = _tstoi (pszMax);

			if (uiMin > uiMax)
			{
				// Always char, as std::exception does not handle wchar_t.
				std::stringstream ss;

				ss << "ERROR: Min is greater than Max at position " <<
					m_uiStrIndex;
				throw std::exception (ss.str ().c_str ());
			}
		}
	}

	CheckChar (pszRegEx++, szCloseCurly);
	m_uiStrIndex++;
}

void CRegExTokeniser::CheckChar (const TCHAR * pszRegEx,
	const TCHAR *pszValidChars) const
{
	if (!*pszRegEx || !_tcschr (reinterpret_cast<const TCHAR *>
		(pszValidChars), *pszRegEx))
	{
		// Always char, as std::exception does not handle wchar_t.
		std::stringstream ss;

		if (*pszRegEx)
		{
			TString c;

			c = *pszRegEx;
			ss << "EROR: Invalid char '" << ConvertStrToANSI (c) <<
				"' at position " << m_uiStrIndex << ".";
		}
		else
		{
			ss << "ERROR: Unexpected end of string at position " <<
				m_uiStrIndex << ".";
		}

		throw std::exception (ss.str ().c_str ());
	}
}

void CRegExTokeniser::ExpandOneOrMore ()
{
	TulList::iterator TokenIter = m_TokenList.end ();
	TStrList::iterator StrTokenIter = m_StrTokenList.end ();
	TulList::iterator LastTokenIter = m_TokenList.end ();
	TStrList::iterator LastStrTokenIter = m_StrTokenList.end ();

	LastTokenIter--;
	LastStrTokenIter--;
	LocateBlockStart (TokenIter, StrTokenIter);

	do
	{
		m_TokenList.push_back (*++TokenIter);
		m_StrTokenList.push_back (*++StrTokenIter);
#ifdef _DEBUG
		AppendToken (static_cast<const e_LexToken> (*TokenIter),
			*StrTokenIter);
#endif
	} while (TokenIter != LastTokenIter);

	m_TokenList.push_back (eLexZEROORMORE);
	m_StrTokenList.push_back (szZeroOrMore);
#ifdef _DEBUG
	AppendToken (eLexZEROORMORE, szZeroOrMore);
#endif
}

void CRegExTokeniser::ExpandRepeatN (const TString &strRepeatN)
{
	const TCHAR *pszRepeatN = reinterpret_cast<const TCHAR *>
		(strRepeatN.c_str ());
	const TCHAR *pszComma = _tcschr (pszRepeatN, ',');
	const int bMaxSpecified = pszComma && *(pszComma + 1);
	const unsigned int uiMin = _tstoi (pszRepeatN);
	const unsigned int uiMax = pszComma ? _tstoi (pszComma + 1) : 0;
	TulList::iterator TokenIter = m_TokenList.end ();
	TStrList::iterator StrTokenIter = m_StrTokenList.end ();

	LocateBlockStart (TokenIter, StrTokenIter);
	TokenIter++;
	StrTokenIter++;

	if (uiMin == 0 && uiMax == 0)
	{
		if (bMaxSpecified)
		{
#ifdef _DEBUG
			TulList::iterator TokenIter2 = TokenIter;
			TStrList::iterator StrTokenIter2 = StrTokenIter;
			size_t nLen = 0;

			while (StrTokenIter2 != m_StrTokenList.end ())
			{
				nLen += (*StrTokenIter2++).size ();

				if (*TokenIter == eLexCHARSET)
				{
					nLen += 2;
				}
				else if (*TokenIter == eLexNEGCHARSET)
				{
					nLen += 3;
				}

				TokenIter2++;
			}

			m_strRegEx.erase (m_strRegEx.size () - nLen, nLen);
#endif
			m_TokenList.erase (TokenIter, m_TokenList.end ());
			m_StrTokenList.erase (StrTokenIter, m_StrTokenList.end ());
		}
		else
		{
			m_TokenList.push_back (eLexZEROORMORE);
			m_StrTokenList.push_back (szZeroOrMore);
#ifdef _DEBUG
			AppendToken (eLexZEROORMORE, szZeroOrMore);
#endif
		}
	}
	else
	{
		TulList BlockTokenList (TokenIter, m_TokenList.end ());
		TStrList BlockStrTokenList (StrTokenIter, m_StrTokenList.end ());

		unsigned int ui = 0;
		for (ui = 1; ui < uiMin; ui++)
		{
			m_TokenList.insert (m_TokenList.end (), BlockTokenList.begin (),
				BlockTokenList.end ());
			m_StrTokenList.insert (m_StrTokenList.end (),
				BlockStrTokenList.begin (), BlockStrTokenList.end ());
#ifdef _DEBUG
			TulList::const_iterator TokenIter = BlockTokenList.begin ();
			TStrList::const_iterator StrIter = BlockStrTokenList.begin ();

			while (StrIter != BlockStrTokenList.end ())
			{
				AppendToken (static_cast<const e_LexToken> (*TokenIter++),
					*StrIter++);
			}
#endif
		}

		unsigned int uiLeft = uiMax ? uiMax - uiMin : 0;

		if (uiMin == 0)
		{
			m_TokenList.push_back (eLexOPTIONAL);
			m_StrTokenList.push_back (szOptional);
#ifdef _DEBUG
			AppendToken (eLexOPTIONAL, szOptional);
#endif
		}

		for (ui = uiMin == 0 ? 1 : 0; ui < uiLeft; ui++)
		{
			m_TokenList.insert (m_TokenList.end (), BlockTokenList.begin (),
				BlockTokenList.end ());
			m_StrTokenList.insert (m_StrTokenList.end (),
				BlockStrTokenList.begin (), BlockStrTokenList.end ());
			m_TokenList.push_back (eLexOPTIONAL);
			m_StrTokenList.push_back (szOptional);
#ifdef _DEBUG
			TulList::const_iterator TokenIter = BlockTokenList.begin ();
			TStrList::const_iterator StrIter = BlockStrTokenList.begin ();

			while (StrIter != BlockStrTokenList.end ())
			{
				AppendToken (static_cast<const e_LexToken> (*TokenIter++),
					*StrIter++);
			}

			AppendToken (eLexOPTIONAL, szOptional);
#endif
		}

		if (pszComma && uiMax == 0)
		{
			m_TokenList.insert (m_TokenList.end (), BlockTokenList.begin (),
				BlockTokenList.end ());
			m_StrTokenList.insert (m_StrTokenList.end (),
				BlockStrTokenList.begin (), BlockStrTokenList.end ());
			m_TokenList.push_back (eLexZEROORMORE);
			m_StrTokenList.push_back (szZeroOrMore);
#ifdef _DEBUG
			TulList::const_iterator TokenIter = BlockTokenList.begin ();
			TStrList::const_iterator StrIter = BlockStrTokenList.begin ();

			while (StrIter != BlockStrTokenList.end ())
			{
				AppendToken (static_cast<const e_LexToken> (*TokenIter++),
					*StrIter++);
			}

			AppendToken (eLexZEROORMORE, szZeroOrMore);
#endif
		}
	}
}

// This function can be confident that the regex read so far is
// well formed, because we have already run plenty of checks.
void CRegExTokeniser::LocateBlockStart (TulList::iterator &TokenIter,
	TStrList::iterator &StrTokenIter) const
{
	int iCount = 0;

	TokenIter--;
	StrTokenIter--;

	do
	{
		if (*TokenIter == eLexCLOSEPAREN) iCount++;
		else if (*TokenIter == eLexOPENPAREN) iCount--;

		TokenIter--;
		StrTokenIter--;
	} while (iCount != 0);
}

std::string CRegExTokeniser::ConvertStrToANSI (const TString &str) const
{
#ifdef _UNICODE
	std::wstring::const_iterator Iter = str.begin ();
	std::string NewStr;

	while (Iter != str.end ())
	{
		NewStr += static_cast<char> (*Iter++);
	}

	return NewStr;
#else
	return str;
#endif
}

#ifdef _DEBUG
void CRegExTokeniser::AppendToken (const e_LexToken eToken,
	const TString &strToken)
{
	if (eToken == eLexCHARSET ||
		eToken == eLexNEGCHARSET)
		m_strRegEx += '[';

	if (eToken == eLexNEGCHARSET)
		m_strRegEx += '^';

	m_strRegEx += strToken;

	if (eToken == eLexCHARSET ||
		eToken == eLexNEGCHARSET)
		m_strRegEx += ']';
}
#endif
