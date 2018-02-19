#pragma once

#include "DfaRegExTypedefs.h"
#include "RegExTokeniser.h"

#pragma warning(disable:4786)

#include <assert.h>
#include <map>
#include <queue>
#include <stack>

class CNfaRegEx
{
public:
	enum e_SynToken {eSynBEGIN = 1, eSynANY, eSynREPEAT, eSynCHAR, eSynNOTCHAR,
		eSynOPTIONAL, eSynALTERNATION, eSynOPENPAREN, eSynCLOSEPAREN,
		eSynREGEX, eSynEND};
	static TString m_strChars; // Must be public for VC++ 6.0
	static _TUCHAR m_cCharOffset; // Must be public for VC++ 6.0

	struct SNfaState
	{
		struct SToken
		{
			enum e_NfaToken {eNfaNULL, eNfaCHAR, eNfaNOTCHAR, eNfaANY};

			e_NfaToken m_eType;
			TString m_strTriggers;
			int m_iNext;

			SToken () : m_eType (eNfaNULL), m_iNext (0) {}
			SToken (const e_NfaToken eType, const TCHAR c, const int iNext) :
				m_eType (eType), m_iNext (iNext) { m_strTriggers = c; }
			bool operator < (const SToken &Token) const
			{
				return m_eType < Token.m_eType || (m_eType == Token.m_eType &&
					m_strTriggers < Token.m_strTriggers) ||
					(m_eType == Token.m_eType &&
					m_strTriggers == Token.m_strTriggers &&
					m_iNext < Token.m_iNext);
			}
			bool Contains (const TCHAR c) const;
			void GetChars (TString &str) const;
			void Remove (const TCHAR c);
		};

		typedef std::deque<SToken> TSTokenDeque;

		SToken m_Token;
		TlList m_EpsList;

		size_t size () const { return (m_Token.m_eType ==
			SToken::eNfaNULL ? 0 : 1) + m_EpsList.size (); }
	};

	struct STrigger
	{
		TString m_strTriggers;
		TulSet m_NextSet;
	};

	typedef std::deque<STrigger> TSTriggerDeque;
	typedef std::deque<SNfaState> TSNfaStateDeque;
	typedef std::set<SNfaState> TSNfaStateSet;
	typedef std::deque<CNfaRegEx> TNfaDeque;

	CNfaRegEx (const CRegExTokeniser &Tokeniser);
	CNfaRegEx (const TNfaDeque &NfaDeque);
	void SetRegEx (const CRegExTokeniser &Tokeniser);
	int EClosure (const TulSet &StateSet,
		TulSetDeque &ClosureStateSetDeque) const;
	void GetTransitions (const TulSet &States,
		TSTriggerDeque &Transitions) const;

	void GetEndStates (TulList &StateList) const;
	const TSNfaStateDeque&GetData () const { return m_NFA; }
	CNfaRegEx::TSNfaStateDeque::size_type size () const
	{ return m_NFA.size ();	}

#ifdef _DEBUG
	const TString &GetRegExStr () { return m_strRegEx; }
	TString GetAsText () const;
#endif

private:
	typedef std::stack<TSNfaStateDeque> TSNfaStateDequeStack;
	typedef std::stack<e_SynToken> TTokenStack;
	typedef std::stack<TString> TStrStack;

	static const char g_cGrammarTable[eSynEND][eSynEND];
	TSNfaStateDeque m_NFA;
#ifdef _DEBUG
	TString m_strRegEx;
#endif

	void Init ();
	void GetNextToken (TulList::const_iterator &LexTokenIter,
		TStrList::const_iterator &LexStrTokenIter, e_SynToken &eToken,
		TString &strToken) const;
	void ProcessTokens (TTokenStack &TokenStack, TStrStack &StrTokenStack,
		e_SynToken &eToken, TString &strToken,
		TulList::const_iterator &LexTokenIter,
		TStrList::const_iterator &LexStrTokenIter,
		TSNfaStateDequeStack &NFAStack);
	void ReduceStack (TTokenStack &TokenStack, TStrStack &StrTokenStack,
		e_SynToken &eToken, TString &strToken,
		TulList::const_iterator &LexTokenIter,
		TStrList::const_iterator &LexStrTokenIter,
		TSNfaStateDequeStack &NFAStack);
	void AddChars (const e_SynToken eToken, const TString strToken,
		TSNfaStateDequeStack &NFAStack) const;
	void PerformRepeat (TSNfaStateDequeStack &NFAStack) const;
	void PerformOptional (TSNfaStateDequeStack &NFAStack) const;
	void PerformJoin (TSNfaStateDequeStack &NFAStack) const;
	void PerformAlternation (TSNfaStateDequeStack &NFAStack) const;
};
