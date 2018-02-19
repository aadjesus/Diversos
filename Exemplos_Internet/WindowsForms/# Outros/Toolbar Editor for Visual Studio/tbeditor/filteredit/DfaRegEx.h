#pragma once

#include <assert.h>
#include <algorithm>
#include <map>
#include <queue>

#include "NfaRegEx.h"

class CDfaRegEx
{
public:
	CDfaRegEx () {}
	CDfaRegEx (const TCHAR *pszRegEx);
	CDfaRegEx (const CNfaRegEx &NFA);

	void assign (const TCHAR *pszRegEx);
	bool match (const TCHAR * &pszStr);
	bool search (const TCHAR * &pszStart, const TCHAR * &pszEnd);
#ifdef _DEBUG
	const TString &GetDfaAsText () const;
	const TString &GetMinimisedDfaAsText () const;
#endif

private:
	struct SDfaLink
	{
		TString m_strChars;
		unsigned long m_ulNext;

		SDfaLink () : m_ulNext (0) {}
		SDfaLink (const TString &str, const unsigned long ulNext);
		// Needed to resolve ambiguity when comparing lists of SDfaLink.
		bool operator == (const SDfaLink &Link) const
		{
			return m_strChars == Link.m_strChars && m_ulNext == Link.m_ulNext;
		}
	};

	typedef std::deque<SDfaLink> TSDfaLinkDeque;

	struct SDfaState
	{
		bool m_bEndState;
		TSDfaLinkDeque m_LinkDeque;

		SDfaState () : m_bEndState (false) {}
		bool operator == (const SDfaState &State) const
		{
			return m_bEndState == State.m_bEndState &&
				m_LinkDeque == State.m_LinkDeque;
		}
		bool Match (const TCHAR cChar, unsigned long * &pulNext) const;
		void clear () { m_bEndState = false; m_LinkDeque.clear (); }
	};

	typedef std::deque<SDfaLink> TSDfaLinkDeque;
	typedef std::vector<SDfaState> TSDfaStateVector;

	TSDfaStateVector m_DFA;

#ifdef _DEBUG
	TString m_strDFA;
	TString m_strMinimisedDFA;

	void AppendDebugOutput (const TSDfaStateVector &DFA,
		TString &strDFA) const;
#endif

	void BuildDFA (const CNfaRegEx &NFA, TSDfaStateVector &DFA) const;
	void BuildDFA (const TCHAR *pszRegEx, TSDfaStateVector &DFA) const;
	void MinimiseDFA (TSDfaStateVector &DFA) const;
	void Minimise (TSDfaStateVector &DFA) const;
	void SplitStates (const TSDfaStateVector &DFA,
		TSDfaStateVector &MinStatesDFA,
		TSDfaStateVector &MinEndStatesDFA) const;
	bool MatchEx (const TCHAR * &pszStr);
};
