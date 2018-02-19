#pragma once

#include <deque>
#include <list>
#include <set>
#include <string>

#pragma warning(disable:4786)

typedef std::list<long> TlList;
typedef std::list<unsigned long> TulList;
typedef std::deque<TulList> TulListDeque;
typedef std::set<unsigned long> TulSet;
typedef std::deque<TulSet> TulSetDeque;
typedef std::basic_string<TCHAR> TString;
typedef std::list<TString> TStrList;

#define NUMCHARS (sizeof (TCHAR) == 1 ? 256 : 65536)
