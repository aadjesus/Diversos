#include "stdafx.h"

#include "RegExTokeniser.h"
#include "DfaRegEx.h"

#include <algorithm>
#include <assert.h>

#ifdef _DEBUG
static const TCHAR szState[] = _T("State: ");
static const TCHAR szNewLine[] = _T("\n");
static const TCHAR szIndent[] = _T("  ");
static const TCHAR szEndState[] = _T("m_bEndState = ");
static const TCHAR sztrue[] = _T("true");
static const TCHAR szfalse[] = _T("false");
static const TCHAR szOpenCurly[] = _T("{");
static const TCHAR szCommaSpace[] = _T(", ");
static const TCHAR szCloseCurly[] = _T("}");
static const TCHAR szChr[] = _T("CHR(");
#endif

CDfaRegEx::CDfaRegEx (const TCHAR *pszRegEx)
{
	assign (pszRegEx);
}

CDfaRegEx::CDfaRegEx (const CNfaRegEx &NFA)
{
	m_DFA.clear ();
#ifdef _DEBUG
	m_strDFA.erase ();
	m_strMinimisedDFA.erase ();
#endif
	BuildDFA (NFA, m_DFA);
#ifdef _DEBUG
	AppendDebugOutput (m_DFA, m_strDFA);
#endif
	MinimiseDFA (m_DFA);
#ifdef _DEBUG
	AppendDebugOutput (m_DFA, m_strMinimisedDFA);
#endif
}

void CDfaRegEx::assign (const TCHAR *pszRegEx)
{
	m_DFA.clear ();
#ifdef _DEBUG
	m_strDFA.erase ();
	m_strMinimisedDFA.erase ();
#endif
	BuildDFA (pszRegEx, m_DFA);
#ifdef _DEBUG
	AppendDebugOutput (m_DFA, m_strDFA);
#endif
	MinimiseDFA (m_DFA);
#ifdef _DEBUG
	AppendDebugOutput (m_DFA, m_strMinimisedDFA);
#endif
}

// Exact Match function
bool CDfaRegEx::match (const TCHAR * &pszStr)
{
	bool bEndState = false;
	unsigned long ulState1 = 0;
	unsigned long ulState2 = 0;
	unsigned long *pInputState = &ulState1;
	unsigned long *pOutputState = &ulState2;

	while (*pszStr && pInputState)
	{
		if (m_DFA[*pInputState].Match (*pszStr, pOutputState))
		{
			pszStr++;
		}

		std::swap (pInputState, pOutputState);
	}

	bEndState = pInputState && m_DFA[*pInputState].m_bEndState;
	return !*pszStr && bEndState;
}

bool CDfaRegEx::search (const TCHAR * &pszStart, const TCHAR * &pszEnd)
{
	bool bFound = false;
	const TCHAR *pszBeginning = pszStart;
	const TCHAR *pszCopy = pszStart;

	while (!bFound && *pszCopy)
	{
		pszEnd = pszStart;
		bFound = MatchEx (pszEnd);

		if (!bFound)
		{
			pszCopy++;
			pszStart = pszCopy;
		}
	}

	if (!bFound && m_DFA[0].m_bEndState)
	{
		bFound = true;
		pszStart = pszEnd = pszBeginning;
	}

	return bFound;
}

#ifdef _DEBUG
const TString &CDfaRegEx::GetDfaAsText () const
{
	return m_strDFA;
}

const TString &CDfaRegEx::GetMinimisedDfaAsText () const
{
	return m_strMinimisedDFA;
}

#include <sstream>

void CDfaRegEx::AppendDebugOutput (const TSDfaStateVector &DFA,
	TString &strDFA) const
{
	TSDfaStateVector::const_iterator StateIter = DFA.begin ();
	int iState = 0;

	while (StateIter != DFA.end ())
	{
		TSDfaLinkDeque::const_iterator LinkIter =
			StateIter->m_LinkDeque.begin ();
		std::basic_stringstream<TCHAR> ss;

		strDFA += szState;
		ss << iState;
		strDFA += ss.str () + szNewLine;
		strDFA += szIndent;
		strDFA += szEndState;
		strDFA += StateIter->m_bEndState ? sztrue : szfalse;
		strDFA += szNewLine;

		while (LinkIter != StateIter->m_LinkDeque.end ())
		{
			TString::const_iterator Iter = LinkIter->m_strChars.begin ();

			while (Iter != LinkIter->m_strChars.end ())
			{
				std::basic_stringstream<TCHAR> ss;

				strDFA += szIndent;
				strDFA += szOpenCurly;

				if (*Iter > 127)
				{
					strDFA += _T("...}\n");
					break;
				}

				if (static_cast<_TUCHAR> (*Iter) < ' ')
				{
					std::basic_stringstream<TCHAR> ss;

					ss << static_cast<unsigned int> (*Iter);
					strDFA += szChr;
					strDFA += ss.str ().c_str ();
					strDFA += ')';
				}
				else
				{
					strDFA += '\'';
					strDFA += *Iter;
					strDFA += '\'';
				}

				Iter++;

				strDFA += szCommaSpace;
				ss << LinkIter->m_ulNext;
				strDFA += ss.str () + szCloseCurly;
				strDFA += szNewLine;
			}

			LinkIter++;
		}

		iState++;
		StateIter++;
	}
}
#endif

void CDfaRegEx::BuildDFA (const TCHAR *pszRegEx, TSDfaStateVector &DFA) const
{
	// Put this in its own function so that the temporary data is destroyed
	// before DFA processing takes place...
	CRegExTokeniser Tokeniser (pszRegEx);
	CNfaRegEx NFA (Tokeniser);

	BuildDFA (NFA, DFA);
}

// The ClosureSetVector is built as the NFA is iterated through.
// No part of it can be discarded until the DFA is built, as existing
// ClosureSets must be checked for each time EClosure is called.
void CDfaRegEx::BuildDFA (const CNfaRegEx &NFA, TSDfaStateVector &DFA) const
{
	unsigned long ulOldEndState = static_cast<unsigned long>
		(NFA.size () - 1);
	TulSet LinkedStatesSet;
	unsigned long ulClosureIndex = 0;
	TulSetDeque ClosureSetDeque;
	CNfaRegEx::TSTriggerDeque Transitions;
	CNfaRegEx::TSTriggerDeque::const_iterator TransIter;
	SDfaState DfaState;

	// Start at the beginning.
	LinkedStatesSet.insert (0);
	NFA.EClosure (LinkedStatesSet, ClosureSetDeque);

	while (ulClosureIndex < ClosureSetDeque.size ())
	{
		NFA.GetTransitions (ClosureSetDeque[ulClosureIndex], Transitions);
		TransIter = Transitions.begin ();

		while (TransIter != Transitions.end ())
		{
			int iTransition = NFA.EClosure (TransIter->m_NextSet,
				ClosureSetDeque);

			if (iTransition >= 0)
			{
				DfaState.m_LinkDeque.push_back (SDfaLink (TransIter->
					m_strTriggers, iTransition));
			}

			TransIter++;
		}

		if (std::binary_search (ClosureSetDeque[ulClosureIndex].begin (),
			ClosureSetDeque[ulClosureIndex].end (), ulOldEndState))
		{
			DfaState.m_bEndState = true;
		}

		ulClosureIndex++;
		DFA.push_back (DfaState);
		DfaState.clear ();
	}
}

void CDfaRegEx::MinimiseDFA (TSDfaStateVector &DFA) const
{
	unsigned long ulSize = 0;

	do
	{
		ulSize = static_cast<unsigned long> (DFA.size ());
		Minimise (DFA);
	} while (DFA.size () != ulSize);
}

void CDfaRegEx::Minimise (TSDfaStateVector &DFA) const
{
	TSDfaStateVector MinDFA;
	TSDfaStateVector MinEndStatesDFA;

	// Split states into end and non-end.
	SplitStates (DFA, MinDFA, MinEndStatesDFA);

	if (DFA.size () != MinDFA.size () + MinEndStatesDFA.size ())
	{
		TSDfaStateVector::const_iterator DfaIter;
		TSDfaLinkDeque::const_iterator StateIter;
		TSDfaStateVector::const_iterator DfaIter2;
		SDfaLink Link;
		SDfaState NewState;
		TSDfaStateVector NewDFA;

		if (DFA.begin ()->m_bEndState)
		{
			MinDFA.insert (MinDFA.begin (), MinEndStatesDFA.begin (),
				MinEndStatesDFA.end ());
		}
		else
		{
			MinDFA.insert (MinDFA.end (), MinEndStatesDFA.begin (),
				MinEndStatesDFA.end ());
		}

		// Renumber transitions
		DfaIter = MinDFA.begin ();

		while (DfaIter != MinDFA.end ())
		{
			NewState.m_bEndState = DfaIter->m_bEndState;
			StateIter = DfaIter->m_LinkDeque.begin ();

			while (StateIter != DfaIter->m_LinkDeque.end ())
			{
				bool bFound = false;
				unsigned long ulIndex = 0;

				DfaIter2 = MinDFA.begin ();

				while (!bFound)
				{
					bFound = DFA[StateIter->m_ulNext] == *DfaIter2++;

					if (!bFound) ulIndex++;
				}
			
				Link = *StateIter++;
				Link.m_ulNext = ulIndex;
				NewState.m_LinkDeque.push_back (Link);
			}

			NewDFA.push_back (NewState);
			NewState.clear ();
			DfaIter++;
		}

		DFA = NewDFA;
	}
}

void CDfaRegEx::SplitStates (const TSDfaStateVector &DFA,
	TSDfaStateVector &MinStatesDFA, TSDfaStateVector &MinEndStatesDFA) const
{
	TSDfaStateVector::const_iterator DfaIter = DFA.begin ();

	while (DfaIter != DFA.end ())
	{
		if (DfaIter->m_bEndState)
		{
			if (std::find (MinEndStatesDFA.begin (), MinEndStatesDFA.end (),
				*DfaIter) == MinEndStatesDFA.end ())
			{
				MinEndStatesDFA.push_back (*DfaIter);
			}
		}
		else
		{
			if (std::find (MinStatesDFA.begin (), MinStatesDFA.end (),
				*DfaIter) == MinStatesDFA.end ())
			{
				MinStatesDFA.push_back (*DfaIter);
			}
		}

		DfaIter++;
	}
}

bool CDfaRegEx::MatchEx (const TCHAR * &pszStr)
{
	const TCHAR *pszMatchEnd = 0;
	unsigned long ulState1 = 0;
	unsigned long ulState2 = 0;
	unsigned long *pInputState = &ulState1;
	unsigned long *pOutputState = &ulState2;

	while (*pszStr && pInputState)
	{
		if (m_DFA[*pInputState].m_bEndState)
		{
			// Keep track of left-most longest match
			pszMatchEnd = pszStr;
		}

		if (m_DFA[*pInputState].Match (*pszStr, pOutputState))
		{
			pszStr++;
		}

		std::swap (pInputState, pOutputState);
	}

	if (pInputState && m_DFA[*pInputState].m_bEndState)
	{
		// Longest match (this function does not need to consume all
		// of the input string)
		pszMatchEnd = pszStr;
	}

	if (pszMatchEnd)
	{
		// return the last match
		pszStr = pszMatchEnd;
	}

	return pszMatchEnd != 0;
}

CDfaRegEx::SDfaLink::SDfaLink (const TString &str,
	const unsigned long ulNext) :
	m_strChars (str), m_ulNext (ulNext)
{
}

bool CDfaRegEx::SDfaState::Match (const TCHAR cChar,
	unsigned long * &pulNext) const
{
	bool bMatch = false;
	TSDfaLinkDeque::const_iterator Iter = m_LinkDeque.begin ();

	while (!bMatch && Iter != m_LinkDeque.end ())
	{
		bMatch = std::binary_search (Iter->m_strChars.begin (),
			Iter->m_strChars.end (), cChar);

		if (bMatch) *pulNext = Iter->m_ulNext;

		Iter++;
	}

	if (!bMatch) pulNext = 0;

	return bMatch;
}
