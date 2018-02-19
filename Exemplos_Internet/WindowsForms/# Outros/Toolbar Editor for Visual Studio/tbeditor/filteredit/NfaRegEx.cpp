#include "stdafx.h"

#include "NfaRegEx.h"
#include "RegExTokeniser.h"

#include <assert.h>
#include <algorithm>

static const TCHAR szOpenParen[] = _T("(");
static const TCHAR szCloseParen[] = _T(")");
#ifdef _DEBUG
static const TCHAR szOpenSquareBracket[] = _T("[");
static const TCHAR szNotCharSet[] = _T("[^");
static const TCHAR szState[] = _T("State: ");
static const TCHAR szNewLine[] = _T("\n");
static const TCHAR szIndent[] = _T("  ");
static const TCHAR szToken[] = _T("Token: ");
static const TCHAR szNfaEPS[] = _T("eNfaEPS");
static const TCHAR szNfaANY[] = _T("eNfaANY");
static const TCHAR szNfaCHAR[] = _T("eNfaCHAR");
static const TCHAR szNfaNOTCHAR[] = _T("eNfaNOTCHAR");
static const TCHAR szLinksTo[] = _T(" links to this + ");
#endif

// General rules:
// - All operators except eSynALTERNATION get processed immediately (pop)
// - eSynBEGIN, eSynALTERNATION, eSynOPENPAREN or eSynREGEX against eSynCHAR
//   results in a push (we don't know what follows the charset)
// - Anything except eSynREGEX against eSynALTERNATION results in a pop
//   (unless it's an error)
// - Sub-regex's are reduced as soon as possible, so that there is only one
//   eSynREGEX per block (i.e. only eSynALTERNATION can separate eSynREGEX's).
// - eSynALTERNATION is processed when the final eSynREGEX is against
//   eSynEND (it is explicitly checked for when the stack is reduced).

// TOKENS:
//            - eSynBEGIN
// .          - eSynANY
// *          - eSynREPEAT
// [...]      - eSynCHAR
// [^...]     - eSynNOTCHAR
// ?          - eSynOPTIONAL
// |          - eSynALTERNATION
// (          - eSynOPENPAREN
// )          - eSynCLOSEPAREN
//            - eSynREGEX
//            - eSynEND

// We use char here even in the Unicode build, because we are only using
// limited characters.
const char CNfaRegEx::g_cGrammarTable[eSynEND][eSynEND] = {
//        BEGIN, ANY, RPT, CHAR, NOTCHAR, OPT, ALTERN, OPPAR, CLSPAR, RE, END
/*BEGIN*/{'E',   '<', 'E', '<',  '<',     'E', 'E',    '<',   'E',    '<','='},
/*ANY*/  {'E',   '>', '>', '>',  '>',     '>', '>',    '>',   '>',    '>','>'},
/*RPT*/  {'E',   '>', 'E', '>',  '>',     'E', '>',    '>',   '>',    '>','>'},
/*CHAR*/ {'E',   '>', '>', '>',  '>',     '>', '>',    '>',   '>',    '>','>'},
/*NTCHR*/{'E',   '>', '>', '>',  '>',     '>', '>',    '>',   '>',    '>','>'},
/*OPT*/  {'E',   '>', 'E', '>',  '>',     'E', '>',    '>',   '>',    '>','>'},
/*ALTRN*/{'E',   '<', 'E', '<',  '<',     'E', 'E',    '<',   'E',    '<','E'},
/*OPPRN*/{'E',   '<', 'E', '<',  '<',     'E', 'E',    '<',   '=',    '<','E'},
/*CSPRN*/{'E',   '>', '>', '>',  '>',     '>', '>',    '>',   '>',    '>','>'},
/*RE*/   {'E',   '<', '>', '<',  '<',     '>', '<',    '<',   '>',    '>','>'},
/*END*/  {'E',   'E', 'E', 'E',  'E',     'E', 'E',    'E',   'E',    'E','E'}
};

TString CNfaRegEx::m_strChars;
_TUCHAR CNfaRegEx::m_cCharOffset = 0;

CNfaRegEx::CNfaRegEx (const CRegExTokeniser &Tokeniser)
{
	Init ();
	SetRegEx (Tokeniser);
}

CNfaRegEx::CNfaRegEx (const TNfaDeque &NfaDeque)
{
	TNfaDeque::const_iterator NfaIter = NfaDeque.begin ();
	int iNext = 1;

	Init ();
	m_NFA.push_back (SNfaState ());

	while (NfaIter != NfaDeque.end ())
	{
		m_NFA.front ().m_EpsList.push_back (iNext);
		m_NFA.insert (m_NFA.end (), NfaIter->GetData ().begin (),
			NfaIter->GetData ().end ());
		iNext += static_cast<int> (NfaIter->size ());
		NfaIter++;
	}
}

void CNfaRegEx::SetRegEx (const CRegExTokeniser &Tokeniser)
{
	TTokenStack TokenStack;
	TStrStack StrTokenStack;
	TulList::const_iterator LexTokenIter = Tokeniser.m_TokenList.begin ();
	TStrList::const_iterator LexStrTokenIter = Tokeniser.m_StrTokenList.begin ();
	e_SynToken eToken = eSynEND;
	TString strToken;
	char cAction = 0;
	TSNfaStateDequeStack NFAStack;

	m_NFA.clear ();
	// First token is always eSynBEGIN
	GetNextToken (LexTokenIter, LexStrTokenIter, eToken, strToken);
	TokenStack.push (eToken);
	StrTokenStack.push (strToken);
	// Even if we have an empty regex, we would still read eSynEND.
	// In other words we always have a left hand side (LHS) and
	// always have a right hand side (RHS).
	GetNextToken (LexTokenIter, LexStrTokenIter, eToken, strToken);

	do
	{
#ifdef _DEBUG
		e_SynToken eTok = TokenStack.top ();
		TString strTok = StrTokenStack.top ();

		// Prevent compiler warning
		// 'local variable is initialized but not referenced'
		eTok; strTok;
#endif

		cAction = g_cGrammarTable[TokenStack.top () - 1][eToken - 1];

		switch (cAction)
		{
			case '<':
				TokenStack.push (eToken);
				StrTokenStack.push (strToken);
				GetNextToken (LexTokenIter, LexStrTokenIter, eToken, strToken);
				break;
			case '>':
				ProcessTokens (TokenStack, StrTokenStack, eToken, strToken,
					LexTokenIter, LexStrTokenIter, NFAStack);
				break;
			case '=':
				TokenStack.pop ();
				StrTokenStack.pop ();
				break;
			default:
				// Should never get here
				assert (0);
				break;
		}
	} while (!TokenStack.empty ());

	// We can never have more than one NFA when all processing is finished.
	assert (NFAStack.size () <= 1);

	if (NFAStack.size () > 0)
	{
		m_NFA = NFAStack.top ();
	}

	// We always have one end state for an NFA...
	m_NFA.push_back (SNfaState ());
}

int CNfaRegEx::EClosure (const TulSet &StateSet,
	TulSetDeque &ClosureStateSetDeque) const
{
	int iState = -1; // -1 means no state transitions possible
	unsigned long ulCurrState = 0;
	TulSet::const_iterator StateIter = StateSet.begin ();
	TulList ClosureStateList;
	TulList::const_iterator ClosureIter;
	TulSet ClosureStateSet;
	const SNfaState *pState = 0;
	TlList::const_iterator EpsIter;

	while (StateIter != StateSet.end ())
	{
		ClosureStateList.push_back (*StateIter++);
	}

	ClosureIter = ClosureStateList.begin ();

	while (ClosureIter != ClosureStateList.end ())
	{
		ulCurrState = *ClosureIter;
		ClosureStateSet.insert (ulCurrState);
		pState = &m_NFA[ulCurrState];
		EpsIter = pState->m_EpsList.begin ();

		while (EpsIter != pState->m_EpsList.end ())
		{
			int i = ulCurrState + *EpsIter++;

			if (ClosureStateSet.insert (i).second)
			{
				ClosureStateList.push_back (i);
			}
		}

		ClosureIter++;
	}

	// Add Closure List to Deque (if unique)
	if (ClosureStateSet.size ())
	{
		bool bFound = false;
		TulSetDeque::iterator ClosureStateSetDequeIter =
			ClosureStateSetDeque.begin ();

		iState = 0;

		while (!bFound && ClosureStateSetDequeIter != ClosureStateSetDeque.
			end ())
		{
			bFound = ClosureStateSet == *ClosureStateSetDequeIter++;

			if (!bFound)
			{
				iState++;
			}
		}

		if (!bFound)
		{
			ClosureStateSetDeque.insert (ClosureStateSetDeque.end (),
				ClosureStateSet);
		}
	}

	return iState;
}

void CNfaRegEx::GetTransitions (const TulSet &States,
	TSTriggerDeque &Transitions) const
{
	TulSet::const_iterator StateIter = States.begin ();
	SNfaState::SToken Token;
	SNfaState::TSTokenDeque TempTrans;
	SNfaState::TSTokenDeque::iterator TransIter;
	TString str;
	const TCHAR *psz = 0;
	SNfaState::TSTokenDeque::iterator TransIter2;
	STrigger Trigger;

	while (StateIter != States.end ())
	{
		long lState = *StateIter++;

		Token = m_NFA[lState].m_Token;

		if (Token.m_eType != SNfaState::SToken::eNfaNULL)
		{
			Token.m_iNext += lState;
			TempTrans.push_back (Token);
		}
	}

	std::sort (TempTrans.begin (), TempTrans.end ());
	Transitions.clear ();
	TransIter = TempTrans.begin ();

	while (TransIter != TempTrans.end ())
	{
		TransIter->GetChars (str);
		psz = str.c_str ();

		while (*psz)
		{
			bool bRemove = false;

			TransIter2 = TransIter;
			TransIter2++;

			while (TransIter2 != TempTrans.end ())
			{
				if (TransIter2->Contains (*psz))
				{
					if (!bRemove) Trigger.m_NextSet.clear ();

					Trigger.m_NextSet.insert (TransIter2->m_iNext);
					TransIter2->Remove (*psz);
					bRemove = true;
				}

				TransIter2++;
			}

			if (bRemove)
			{
				Trigger.m_strTriggers = *psz;
				Trigger.m_NextSet.insert (TransIter->m_iNext);
				TransIter->Remove (*psz);
				Transitions.push_back (Trigger);
			}

			psz++;
		}

		if (TransIter->m_eType != SNfaState::SToken::eNfaNULL)
		{
			TransIter->GetChars (Trigger.m_strTriggers);
			Trigger.m_NextSet.clear ();
			Trigger.m_NextSet.insert (TransIter->m_iNext);
			Transitions.push_back (Trigger);
		}

		TransIter++;
	}
}

void CNfaRegEx::GetEndStates (TulList &StateList) const
{
	TSNfaStateDeque::const_iterator StateIter = m_NFA.begin ();
	int iIndex = 0;

	while (StateIter != m_NFA.end ())
	{
		if (StateIter++->size () == 0)
		{
			StateList.push_back (iIndex);
		}

		iIndex++;
	}	
}

#ifdef _DEBUG
#include <sstream>

TString CNfaRegEx::GetAsText () const
{
	TString str;
	TSNfaStateDeque::const_iterator StateIter = m_NFA.begin ();
	int iState = 0;

	while (StateIter != m_NFA.end ())
	{
		TlList::const_iterator EpsIter = StateIter->m_EpsList.begin ();
		TString::const_iterator LinkIter = StateIter->m_Token.m_strTriggers.begin ();
		std::basic_stringstream<TCHAR> ss;

		str += szState;
		ss << iState;
		str += ss.str () + szNewLine;

		while (EpsIter != StateIter->m_EpsList.end ())
		{
			std::basic_stringstream<TCHAR> ss;

			str += szIndent;
			str += szToken;
			str += szNfaEPS;
			str += szLinksTo;
			ss << *EpsIter;
			str += ss.str ();
			str += szNewLine;
			EpsIter++;
		}

		while (LinkIter != StateIter->m_Token.m_strTriggers.end ())
		{
			std::basic_stringstream<TCHAR> ss;

			str += szIndent;
			str += szToken;

			switch (StateIter->m_Token.m_eType)
			{
				case SNfaState::SToken::eNfaCHAR:
					str += szNfaCHAR;
					break;
				case SNfaState::SToken::eNfaNOTCHAR:
					str += szNfaNOTCHAR;
					break;
				case SNfaState::SToken::eNfaANY:
					str += szNfaANY;
					break;
				default:
					// Should never get here
					assert (0);
					break;
			}

			str += szIndent;
			str += '\'';

			str += *LinkIter;

			str += '\'';
			str += szLinksTo;
			ss << StateIter->m_Token.m_iNext;
			str += ss.str () + szNewLine;
			LinkIter++;
		}

		iState++;
		StateIter++;
	}

	return str;
}

#endif

void CNfaRegEx::Init ()
{
	if (m_strChars.size () == 0)
	{
		m_strChars = _T("\a\b\f\n\r\t\v");
		m_cCharOffset = ' ' - m_strChars.size ();
		std::sort (m_strChars.begin (), m_strChars.end ());
		m_strChars.reserve (NUMCHARS - ' ' + m_strChars.size ());

		for (_TUCHAR c = ' '; c != 0; c++)
		{
			m_strChars += c;
		}
	}
}

// If you're wondering why the tokens are converted, it's to clarify what
// each stage in the compilation is doing.  The number of possible tokens
// reduce at each stage in the compilation, until only 3 types remain.
void CNfaRegEx::GetNextToken (TulList::const_iterator &LexTokenIter,
	TStrList::const_iterator &LexStrTokenIter, e_SynToken &eToken,
	TString &strToken) const
{
	switch (*LexTokenIter)
	{
		case CRegExTokeniser::eLexBEGIN:
			eToken = eSynBEGIN;
			break;
		case CRegExTokeniser::eLexANY:
			eToken = eSynANY;
			break;
		case CRegExTokeniser::eLexZEROORMORE:
			eToken = eSynREPEAT;
			break;
		case CRegExTokeniser::eLexCHARSET:
			eToken = eSynCHAR;
			break;
		case CRegExTokeniser::eLexNEGCHARSET:
			eToken = eSynNOTCHAR;
			break;
		case CRegExTokeniser::eLexOPTIONAL:
			eToken = eSynOPTIONAL;
			break;
		case CRegExTokeniser::eLexOR:
			eToken = eSynALTERNATION;
			break;
		case CRegExTokeniser::eLexOPENPAREN:
			eToken = eSynOPENPAREN;
			break;
		case CRegExTokeniser::eLexCLOSEPAREN:
			eToken = eSynCLOSEPAREN;
			break;
		case CRegExTokeniser::eLexEND:
			eToken = eSynEND;
			break;
		default:
			// Should never get here
			assert (0);
			break;
	}

	LexTokenIter++;
	strToken = *LexStrTokenIter++;
}

void CNfaRegEx::ProcessTokens (TTokenStack &TokenStack,
	TStrStack &StrTokenStack, e_SynToken &eToken, TString &strToken,
	TulList::const_iterator &LexTokenIter,
	TStrList::const_iterator &LexStrTokenIter,
	TSNfaStateDequeStack &NFAStack)
{
	switch (eToken)
	{
		// By not pushing the result back on to the stack, automatic token
		// 'reduction' is acheived.
		case eSynREPEAT:
			if (TokenStack.top () != eSynREGEX)
			{
				// Build NFA, push onto stack
				AddChars (TokenStack.top (), StrTokenStack.top (), NFAStack);
			}

			eToken = eSynREGEX;
#ifdef _DEBUG
			if (TokenStack.top () == eSynCHAR ||
				TokenStack.top () == eSynNOTCHAR)
			{
				strToken.insert (strToken.begin (), ']');
			}
#endif
			strToken = StrTokenStack.top () + strToken;
#ifdef _DEBUG
			if (TokenStack.top () == eSynCHAR)
			{
				strToken = szOpenSquareBracket + strToken;
			}
			else if (TokenStack.top () == eSynNOTCHAR)
			{
				strToken = szNotCharSet + strToken;
			}
#endif
			TokenStack.pop ();
			StrTokenStack.pop ();
			PerformRepeat (NFAStack);
			break;
		case eSynOPTIONAL:
			if (TokenStack.top () != eSynREGEX)
			{
				// Build NFA, push onto stack
				AddChars (TokenStack.top (), StrTokenStack.top (), NFAStack);
			}

			eToken = eSynREGEX;
#ifdef _DEBUG
			if (TokenStack.top () == eSynCHAR ||
				TokenStack.top () == eSynNOTCHAR)
			{
				strToken.insert (strToken.begin (), ']');
			}
#endif
			strToken = StrTokenStack.top () + strToken;
#ifdef _DEBUG
			if (TokenStack.top () == eSynCHAR)
			{
				strToken = szOpenSquareBracket + strToken;
			}
			else if (TokenStack.top () == eSynNOTCHAR)
			{
				strToken = szNotCharSet + strToken;
			}
#endif
			TokenStack.pop ();
			StrTokenStack.pop ();
			PerformOptional (NFAStack);
			break;
		case eSynREGEX:
			strToken = StrTokenStack.top () + strToken;

			if (TokenStack.size () > 1)
			{
				PerformJoin (NFAStack);
			}

			TokenStack.pop ();
			StrTokenStack.pop ();
			break;
		default:
			ReduceStack (TokenStack, StrTokenStack, eToken, strToken,
				LexTokenIter, LexStrTokenIter, NFAStack);
			break;
	}
}

void CNfaRegEx::ReduceStack (TTokenStack &TokenStack, TStrStack &StrTokenStack,
	e_SynToken &eToken, TString &strToken,
	TulList::const_iterator &LexTokenIter,
	TStrList::const_iterator &LexStrTokenIter,
	TSNfaStateDequeStack &NFAStack)
{
	switch (TokenStack.top ())
	{
		case eSynANY:
		case eSynCHAR:
		case eSynNOTCHAR:
			LexTokenIter--;
			LexStrTokenIter--;
			eToken = eSynREGEX;
			strToken = StrTokenStack.top ();
			// Build NFA, push onto stack
			AddChars (TokenStack.top (), StrTokenStack.top (),
				NFAStack);
#ifdef _DEBUG
			if (TokenStack.top () == eSynCHAR)
			{
				strToken = szOpenSquareBracket + strToken;
				strToken += ']';
			}
			else if (TokenStack.top () == eSynNOTCHAR)
			{
				strToken = szNotCharSet + strToken;
				strToken += ']';
			}
#endif
			TokenStack.pop ();
			StrTokenStack.pop ();
			break;
		case eSynREGEX:
		{
			e_SynToken eTok = eSynREGEX;
			TString strTok = StrTokenStack.top ();

			TokenStack.pop ();
			StrTokenStack.pop ();

			switch (TokenStack.top ())
			{
				case eSynALTERNATION:
					strTok = StrTokenStack.top () + strTok;
					TokenStack.pop ();
					StrTokenStack.pop ();
					strTok = StrTokenStack.top () + strTok;
					TokenStack.pop ();
					StrTokenStack.pop ();
					PerformAlternation (NFAStack);
					TokenStack.push (eSynREGEX);
					StrTokenStack.push (strTok);
					break;
				case eSynOPENPAREN:
					strTok = szOpenParen + strTok + szCloseParen;
					// Throw away eSynOPENPAREN
					TokenStack.pop ();
					StrTokenStack.pop ();
					// Only push the result on to the stack because
					// the parenthesis have been discarded.
					TokenStack.push (eTok);
					StrTokenStack.push (strTok);
					// Throw away eSynCLOSEPAREN
					GetNextToken (LexTokenIter, LexStrTokenIter, eToken,
						strToken);
					break;
				case eSynREGEX:
					strTok = StrTokenStack.top () + strTok;

					if (TokenStack.size () > 1)
					{
						PerformJoin (NFAStack);
					}

					TokenStack.pop ();
					StrTokenStack.pop ();
					TokenStack.push (eSynREGEX);
					StrTokenStack.push (strTok);
					break;
				default:
#ifdef _DEBUG
					m_strRegEx = strTok;
#endif
					break;
			}
			break;
		}
		default:
			// Should never get here
			assert (0);
			break;
	}
}

void CNfaRegEx::AddChars (const e_SynToken eToken, const TString strToken,
	TSNfaStateDequeStack &NFAStack) const
{
	SNfaState State;
	TSNfaStateDeque NFA;

	switch (eToken)
	{
		case eSynCHAR:
			State.m_Token.m_eType = SNfaState::SToken::eNfaCHAR;
			break;
		case eSynNOTCHAR:
			State.m_Token.m_eType = SNfaState::SToken::eNfaNOTCHAR;
			break;
		case eSynANY:
			State.m_Token.m_eType = SNfaState::SToken::eNfaANY;
			break;
		default:
			// Should never get here
			assert (0);
			break;
	}

	// The token is left as a string to prevent an explosion of NFA states.
	// When the NFA is converted to a DFA a set of characters is output,
	// depending on the type (e.g. eNfaANY resolves to all chars).
	State.m_Token.m_strTriggers = strToken;
	State.m_Token.m_iNext = 1;
	NFA.push_back (State);
	NFAStack.push (NFA);
}

void CNfaRegEx::PerformRepeat (TSNfaStateDequeStack &NFAStack) const
{
	SNfaState State;
	TSNfaStateDeque LHS = NFAStack.top ();

	State.m_EpsList.push_back (-static_cast<int> (LHS.size ()));
	State.m_EpsList.push_back (1);
	LHS.push_back (State);
	State.m_EpsList.pop_front ();
	State.m_EpsList.push_back (static_cast<int> (LHS.size () + 1));
	LHS.insert (LHS.begin (), State);
	NFAStack.pop ();
	NFAStack.push (LHS);
}

void CNfaRegEx::PerformOptional (TSNfaStateDequeStack &NFAStack) const
{
	SNfaState State;
	TSNfaStateDeque LHS = NFAStack.top ();

	State.m_EpsList.push_back (1);
	LHS.push_back (State);
	State.m_EpsList.push_back (static_cast<int> (LHS.size () + 1));
	LHS.insert (LHS.begin (), State);
	NFAStack.pop ();
	NFAStack.push (LHS);
}

void CNfaRegEx::PerformJoin (TSNfaStateDequeStack &NFAStack) const
{
	SNfaState State;
	TSNfaStateDeque RHS = NFAStack.top ();
	TSNfaStateDeque LHS;

	State.m_EpsList.push_back (1);
	NFAStack.pop ();
	LHS = NFAStack.top ();
	NFAStack.pop ();
	LHS.push_back (State);
	LHS.insert (LHS.end (), RHS.begin (), RHS.end ());
	NFAStack.push (LHS);
}

void CNfaRegEx::PerformAlternation (TSNfaStateDequeStack &NFAStack) const
{
	SNfaState State;
	TSNfaStateDeque RHS = NFAStack.top ();
	TSNfaStateDeque LHS;

	NFAStack.pop ();
	LHS = NFAStack.top ();
	State.m_EpsList.push_back (static_cast<int> (RHS.size () + 2));
	LHS.push_back (State);
	State.m_EpsList.front () = 1;
	RHS.push_back (State);
	State.m_EpsList.push_back (static_cast<int> (LHS.size () + 1));
	LHS.insert (LHS.begin (), State);
	LHS.insert (LHS.end (), RHS.begin (), RHS.end ());
	NFAStack.pop ();
	NFAStack.push (LHS);
}

bool CNfaRegEx::SNfaState::SToken::Contains (const TCHAR c) const
{
	bool bSuccess = true;

	switch (m_eType)
	{
		case eNfaNULL:
			bSuccess = false;
			break;
		case eNfaCHAR:
			bSuccess = m_strTriggers.find (c) != TString::npos;
			break;
		case eNfaNOTCHAR:
			bSuccess = !std::binary_search (m_strTriggers.begin (),
				m_strTriggers.end (), c);
			break;
		case eNfaANY:
			bSuccess = true;
			break;
		default:
			// Should never get here
			assert (0);
			break;
	}

	return bSuccess;
}

void CNfaRegEx::SNfaState::SToken::GetChars (TString &str) const
{
	str.erase ();

	switch (m_eType)
	{
		case eNfaNULL:
			break;
		case eNfaCHAR:
			str = m_strTriggers;
			break;
		case eNfaNOTCHAR:
		{
			_TUCHAR cOffset = m_cCharOffset;
			const TCHAR *pszTriggers = m_strTriggers.c_str ();

			str = CNfaRegEx::m_strChars;

			while (*pszTriggers)
			{
				if (*pszTriggers >= ' ')
				{
					_TUCHAR c = (*pszTriggers) - cOffset;
					TCHAR x = str[c];

					assert (str[c] == *pszTriggers);
					str.erase (c, 1);
					cOffset++;
				}
				else
				{
					str.erase (str.find (*pszTriggers), 1);
					cOffset++;
				}

				pszTriggers++;
			}
/*
			const TCHAR *psz = m_strTriggers.c_str ();
			const TCHAR *pszAll = CNfaRegEx::m_strChars.c_str ();

			str.reserve (NUMCHARS - m_strTriggers.size ());

			while (*pszAll)
			{
				if (*psz == *pszAll)
				{
					psz++;
				}
				else
				{
					str += *pszAll;
				}

				pszAll++;
			}
*/
			break;
		}
		case eNfaANY:
			str = CNfaRegEx::m_strChars;
			break;
		default:
			// Should never get here
			assert (0);
			break;
	}
}

void CNfaRegEx::SNfaState::SToken::Remove (const TCHAR c)
{
	switch (m_eType)
	{
		case eNfaCHAR:
		m_strTriggers.erase (m_strTriggers.find (c), 1);

		if (m_strTriggers.size () == 0)
		{
			m_eType = eNfaNULL;
		}
		break;
		case eNfaNOTCHAR:
			if (!std::binary_search (m_strTriggers.begin (),
				m_strTriggers.end (), c))
			{
				m_strTriggers += c;
				// Must keep string sorted for further processing
				_TUCHAR *pszStart = const_cast<_TUCHAR *> (reinterpret_cast
					<const _TUCHAR *> (m_strTriggers.c_str ()));
				_TUCHAR *pszEnd = pszStart + m_strTriggers.size ();
				std::sort (pszStart, pszEnd);
			}

			if (m_strTriggers == CNfaRegEx::m_strChars)
			{
				m_eType = eNfaNULL;
				m_strTriggers.erase ();
			}
			break;
		case eNfaANY:
			m_eType = eNfaNOTCHAR;
			m_strTriggers = c;
			break;
		default:
			// Should never get here
			assert (0);
			break;
	}
}
