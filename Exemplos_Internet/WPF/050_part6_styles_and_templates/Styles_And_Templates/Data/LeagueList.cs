using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Collections.Generic;


namespace Styles_And_Templates
{
    #region Inner data classes

    public class League
    {
        public string Name { get; private set; }
        public List<Division> Divisions { get; private set; }

        public League(string name)
        {
            Name = name;
            Divisions = new List<Division>();
        }
    }
    
    public class Division
    {

        public string Name { get; private set; }
        public List<Team> Teams { get; private set; }

        public Division(string name)
        {
            Name = name;
            Teams = new List<Team>();

        }
    }



    public class Team
    {
        public string Name { get; private set; }

        public Team(string name)
        {
            Name = name;
        }
    }
    #endregion

    #region LeagueList
    
    /// <summary>
    /// Provides a simple LeagueList, holding dummy
    /// data to demonstrate binding to Hierarchical
    /// data structures
    /// </summary>
    public class LeagueList : List<League>
    {
        public LeagueList()
        {
            League l;
            Division d;

            Add(l = new League("League A"));
            l.Divisions.Add((d = new Division("Division A")));
            d.Teams.Add(new Team("Team I"));
            d.Teams.Add(new Team("Team II"));
            d.Teams.Add(new Team("Team III"));
            d.Teams.Add(new Team("Team IV"));
            d.Teams.Add(new Team("Team V"));
            l.Divisions.Add((d = new Division("Division B")));
            d.Teams.Add(new Team("Team Blue"));
            d.Teams.Add(new Team("Team Red"));
            d.Teams.Add(new Team("Team Yellow"));
            d.Teams.Add(new Team("Team Green"));
            d.Teams.Add(new Team("Team Orange"));
            l.Divisions.Add((d = new Division("Division C")));
            d.Teams.Add(new Team("Team East"));
            d.Teams.Add(new Team("Team West"));
            d.Teams.Add(new Team("Team North"));
            d.Teams.Add(new Team("Team South"));
            Add(l = new League("League B"));
            l.Divisions.Add((d = new Division("Division A")));
            d.Teams.Add(new Team("Team 1"));
            d.Teams.Add(new Team("Team 2"));
            d.Teams.Add(new Team("Team 3"));
            d.Teams.Add(new Team("Team 4"));
            d.Teams.Add(new Team("Team 5"));
            l.Divisions.Add((d = new Division("Division B")));
            d.Teams.Add(new Team("Team Diamond"));
            d.Teams.Add(new Team("Team Heart"));
            d.Teams.Add(new Team("Team Club"));
            d.Teams.Add(new Team("Team Spade"));
            l.Divisions.Add((d = new Division("Division C")));
            d.Teams.Add(new Team("Team Alpha"));
            d.Teams.Add(new Team("Team Beta"));
            d.Teams.Add(new Team("Team Gamma"));
            d.Teams.Add(new Team("Team Delta"));
            d.Teams.Add(new Team("Team Epsilon"));
        }

        public League this[string name]
        {
            get
            {
                foreach (League l in this)
                    if (l.Name == name)
                        return l;

                return null;
            }
        }
    }
    #endregion
}