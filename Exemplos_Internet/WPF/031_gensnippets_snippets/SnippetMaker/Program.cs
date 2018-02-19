using System;
using System.Text;
using System.Threading;
using System.Linq;

namespace SnippetMaker
{
  partial class Program
  {
    private const string author = "C# Snippets Release 5.0 -- Copyright (C) Dmitri Nesteruk, 2008 -- see http://www.codeplex.com/csn for latest releases";
    private const int count = 20;

    // TODO: CopyFrom for entity classes

    static void Main()
    {
      new Program();
    }

    static string ending(int j)
    {
      switch (j % 100)
      {
        case 11:
        case 12:
        case 13:
          return "th";
      }

      switch (j % 10)
      {
        case 1:
          return "st";
        case 2:
          return "nd";
        case 3:
          return "rd";
        default:
          return "th";
      }
    }

    public Program()
    {
      WaitHandle.WaitAll(
        new ThreadStart[]
          {
            makeArglists, 
            makeArrs, 
            makeArrStore, 
            makeCatch, 
            makeFors, 
            makeParrs, 
            makeSimpleTuples, 
            makeEntities,
            makePoly,
            makeVarlist,
            makeGetflags,
            makePow,
            makeFlags,
            makeStateMachine,
            makeDpEntity,
            makeSlimEntities,
            makeSubSup,
            makeNullTest
          }
        .Select(o => o.BeginInvoke(null, null).AsyncWaitHandle).ToArray());
    }

    #region Test Snippets - Not For Production!

    public void makeFuncTest()
    {
      var sc = new SnippetCollection();
      var s = new Snippet(author, "test", "test", "Testing!");
      s.Literals.Add(new SnippetLiteral("expression", "no", "expression", "ListVariablesOfType()", true));
      s.Literals.Add(new SnippetLiteral("cases", "no", "default:", "GenerateSwitchCases($expresion$)", false));
      s.CodeBuilder.AppendLine("$cases$");
      sc.Add(s);
      sc.Save("test", true);
    }

    #endregion

    #region Production Snippets

    // release 5

    private static void makeNullTest()
    {
      var sc = new SnippetCollection();
      for (int i = 2; i <= count; ++i)
      {
        var s = new Snippet
                  {
                    Author = author,
                    Description = "Creates a sequential null-check with " + i + " arguments.",
                    Shortcut = "nulltest" + i,
                    Title = "nulltest" + i
                  };
        var sb = s.CodeBuilder;
        sb.Append("if (");
        for (int j = 1; j <= i; ++j)
        {
          // make a parameter
          var l = new SnippetLiteral("Object" + j, 
            string.Empty + j + ending(j) + " object in the chain", 
            "Object" + j);
          s.Literals.Add(l);
          // use it
          var sb2 = new StringBuilder();
          for (int k = 1; k <= j; ++k)
          {
            sb2.Append("$Object" + k + "$");
            if (k != j)
              sb2.Append(".");
          }
          sb.Append(sb2);
          sb.Append(" != null");
          if (j != i)
            sb.Append(" && ");
        }
        sb.AppendLine(")");
        sb.AppendBetweenCurlyBraces(string.Empty);
        sc.Add(s);
      }
      sc.Save("nulltest");
    }

    // release 4

    private static void makeSubSup()
    {
      // superscript and subscript
      const string subs = "₀₁₂₃₄₅₆₇₈₉₊₋₌₍₎";
      const string sups = "⁰¹²³⁴⁵⁶⁷⁸⁹⁺⁻⁼⁽⁾";
      string[] names = {"0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "+", "-", "=", "(", ")"};
      int len = names.Length;
      var sc = new SnippetCollection();
      for (int i = 0; i < len; ++i)
      {
        var s = new Snippet
        {
          Author = author,
          Description = "Superscript " + names[i] + " symbol.",
          Shortcut = "sup" + names[i],
          Title = "sup" + names[i]
        };
        s.CodeBuilder.Append(sups[i]);
        
        var s2 = new Snippet
        {
          Author = author,
          Description = "Subscript " + names[i] + " symbol.",
          Shortcut = "sub" + names[i],
          Title = "sub" + names[i]
        };
        s2.CodeBuilder.Append(subs[i]);

        sc.Add(s);
        sc.Add(s2);
      }
      sc.Save("subsup");
    }

    private static void makeSlimEntities()
    {
      var tuples = new SnippetCollection();
      for (int i = 1; i <= count; ++i)
      {
        var s = new Snippet
        {
          Author = author,
          Description = "Strongly typed entity class with " + i + " properties controlled via ReaderWriterLockSlim.",
          Shortcut = "entity" + i + "slim",
          Title = "entity" + i + "slim"
        };
        // class name
        SnippetLiteral sl = new SnippetLiteral
        {
          ID = "ClassName",
          Default = ("Entity" + i),
          ToolTip = "Name of this tuple class."
        };
        s.Literals.Add(sl);
        // property names and types
        for (int j = 1; j <= i; ++j)
        {
          var nameLit = new SnippetLiteral
          {
            ID = "Prop" + j + "Name",
            Default = "Prop" + j + "Name",
            ToolTip = "Name of " + j + ending(j) + " property."
          };
          s.Literals.Add(nameLit);
          var typeLit = new SnippetLiteral
          {
            ID = "Prop" + j + "Type",
            Default = "int",
            ToolTip = "Type of " + j + ending(j) + " property."
          };
          s.Literals.Add(typeLit);
        }
        // make the class
        var sb = s.CodeBuilder;
        sb.AppendXmlSummary("$ClassName$.");
        sb.AppendLine("[Serializable]");
        sb.AppendLine("internal class $ClassName$ : IEquatable<$ClassName$>");
        sb.AppendLine("{");
        #region rwls fields
        sb.AppendLine("#region Fields");
        // for each property there are two things - the backing field and the lock
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendLine(string.Format("private $Prop{0}Type$ _$Prop{0}Name$;", j));
          sb.AppendLine(string.Format("private readonly ReaderWriterLockSlim $Prop{0}Name$Lock = new ReaderWriterLockSlim();", j));
        }
        sb.AppendLine("#endregion");
        #endregion
        #region properties
        sb.AppendLine("#region Properties");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendXmlSummary("Gets or sets the $Prop" + j + "Name$.");
          sb.AppendXmlValue("The $Prop" + j + "Name$.");
          //sb.AppendLine(string.Format("internal $Prop{0}Type$ $Prop{0}Name$ {{ get; set; }}", j));
          sb.AppendLine(string.Format("internal $Prop{0}Type$ $Prop{0}Name$", j));
          sb.AppendLine("{");
          sb.AppendLine("get");
          sb.AppendLine("{");
          sb.AppendLine(string.Format("$Prop{0}Name$Lock.EnterReadLock();", j));
          sb.AppendLine(string.Format("$Prop{0}Type$ result = _$Prop{0}Name$;", j));
          sb.AppendLine(string.Format("$Prop{0}Name$Lock.ExitReadLock();", j));
          sb.AppendLine("return result;");
          sb.AppendLine("}");
          sb.AppendLine("set");
          sb.AppendLine("{");
          sb.AppendLine(string.Format("$Prop{0}Name$Lock.EnterWriteLock();", j));
          sb.AppendLine(string.Format("_$Prop{0}Name$ = value;", j));
          sb.AppendLine(string.Format("$Prop{0}Name$Lock.ExitWriteLock();", j));
          sb.AppendLine("}");
          sb.AppendLine("}");
        }
        sb.AppendLine("#endregion");
        #endregion
        #region constructors
        sb.AppendLine("#region Constructors");
        sb.AppendXmlSummary("Initializes a new instance of the <see cref=\"$ClassName$\"/> class.");
        sb.AppendLine("internal $ClassName$() {}");

        sb.AppendXmlSummary("Initializes a new fully specified instance of the <see cref=\"$ClassName$\"/> class.");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendParam("$Prop" + j + "Name$", "The $Prop" + j + "Name$");
        }
        sb.Append("internal $ClassName$(");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendFormat("$Prop{0}Type$ $Prop{0}Name$", j);
          if (j != i)
            sb.Append(", ");
        }
        sb.AppendLine(")");
        sb.AppendLine("{");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendFormat("this.$Prop{0}Name$ = $Prop{0}Name$;{1}", j, Environment.NewLine);
        }
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        #endregion
        #region methods
        sb.AppendLine("#region Methods");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// Determines whether the specified <see cref=\"T:System.Object\"/> is equal to the current <see cref=\"T:System.Object\"/>.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("/// <param name=\"obj\">The <see cref=\"T:System.Object\"/> to compare with the current <see cref=\"T:System.Object\"/>.</param>");
        sb.AppendLine("/// <returns>");
        sb.AppendLine("/// true if the specified <see cref=\"T:System.Object\"/> is equal to the current <see cref=\"T:System.Object\"/>; otherwise, false.");
        sb.AppendLine("/// </returns>");
        sb.AppendLine("/// <exception cref=\"T:System.NullReferenceException\">The <paramref name=\"obj\"/> parameter is null.</exception>");
        sb.AppendLine(@"public override bool Equals(object obj)
{
  $ClassName$ other = obj as $ClassName$;
  if (other != null)
    return Equals(other);
  return false;
}");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// Indicates whether the current object is equal to another object of the same type.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("/// <param name=\"other\">An object to compare with this object.</param>");
        sb.AppendLine("/// <returns>");
        sb.AppendLine(
          "/// true if the current object is equal to the <paramref name=\"other\"/> parameter; otherwise, false.");
        sb.AppendLine("/// </returns>");
        sb.AppendLine("public bool Equals($ClassName$ other)");
        sb.AppendLine("{");
        sb.AppendLine("if (ReferenceEquals(null, other)) return false;");
        sb.AppendLine("if (ReferenceEquals(this, other)) return true;");
        sb.AppendLine("return");
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("  $Prop" + j + "Name$ == other.$Prop" + j + "Name$");
          sb.AppendLine(j == i ? ";" : " &&");
        }
        sb.AppendLine("}");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// Returns a <see cref=\"T:System.String\"/> that represents the current <see cref=\"T:System.Object\"/>.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("/// <returns>");
        sb.AppendLine("/// A <see cref=\"T:System.String\"/> that represents the current <see cref=\"T:System.Object\"/>.");
        sb.AppendLine("/// </returns>");
        sb.AppendLine("public override string ToString()");
        sb.AppendLine("{");
        sb.AppendLine("StringBuilder sb = new StringBuilder();");
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("sb.Append(\"$Prop" + j + "Name$ = \" + $Prop" + j + "Name$");
          if (j != i) sb.Append(" + \";\"");
          sb.AppendLine(");");
        }
        sb.AppendLine("return sb.ToString();");
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        #endregion
        sb.AppendLine("}");
        // add it!
        tuples.Add(s);
      }
      tuples.Save("entityslim");
    }

    private static void makeDpEntity()
    {
      const int total = 10;
      var sc = new SnippetCollection();
      for (int i = 1; i < total; ++i) // ordinary
      {
        for (int j = 0; j < total; ++j) // read-only
        {
          // (0,0) is an unacceptable pair
          if (i == 0) continue;
          // an entity with i r-w DPs and j r-o DPs
          var s = new Snippet
          {
            Author = author,
            Description = "Strongly typed entity class with " + i + " dependency properties and " + j + " read-only dependency properties.",
            Shortcut = "dpentity" + i + (j > 0 ? "by" + j : string.Empty),
            Title = "dpentity" + i + (j > 0 ? "by" + j : string.Empty)
          };
          // params i+j plus defaults for i+j
          SnippetLiteral sl = new SnippetLiteral
          {
            ID = "ClassName",
            Default = ("Entity" + i),
            ToolTip = "Name of this entity class."
          };
          s.Literals.Add(sl);
          // params for read-write properties
          for (int a = 1; a <= i; ++a)
          {
            s.Literals.AddLiteral("Property" + a + "Name", string.Empty + a + ending(a) + " property name.");
            s.Literals.AddLiteral("Property" + a + "Type", string.Empty + a + ending(a) + " property type.");
          }
          // params for read-only properties
          for (int b = 1; b <= j; ++b)
          {
            s.Literals.AddLiteral("ReadOnlyProperty" + b + "Name",
                                  string.Empty + b + ending(b) + " (read-only) property name.");
            s.Literals.AddLiteral("ReadOnlyProperty" + b + "Type",
                                  string.Empty + b + ending(b) + " (read-only) property type.");
          }
          // time to build
          var sb = s.CodeBuilder;
          sb.AppendXmlSummary(
            string.Format("$ClassName$ contains {0} read-write and {1} read-only dependency properties.", i, j));
          sb.AppendLine("public class $ClassName$ : DependencyObject, IEquatable<$ClassName$> {");
          #region Properties
          sb.AppendLine("#region Properties");
          for (int a = 1; a <= i; ++a)
          {
            // read-write
            sb.AppendXmlSummary("Identifies the <see cref=\"$Property" + a + "Name$\"/> dependency property.");
            sb.AppendLine("public static readonly DependencyProperty $Property" + a + "Name$Property =");
            sb.AppendLine("  DependencyProperty.Register(\"$Property" + a + "Name$\", typeof($Property" + a + "Type$), typeof($ClassName$), new PropertyMetadata(default($Property" + a + "Type$)));");
            sb.AppendXmlSummary("Gets or sets a value of $Property" + a + "Name$. This is a dependency property.");
            sb.AppendLine("public $Property" + a + "Type$ $Property" + a + "Name$");
            sb.AppendLine("{");
            sb.AppendLine("get { return ($Property" + a + "Type$)GetValue($Property" + a + "Name$Property); }");
            sb.AppendLine("set { SetValue($Property" + a + "Name$Property, value); }");
            sb.AppendLine("}");
          }
          for (int b = 1; b <= j; ++b)
          {
            // read-only
            // key
            sb.AppendLine("private static readonly DependencyPropertyKey $ReadOnlyProperty" + b + "Name$PropertyKey =");
            sb.AppendLine("  DependencyProperty.RegisterReadOnly(\"ItemsView\", typeof($ReadOnlyProperty" + b + "Type$), typeof($ClassName$),");
            sb.AppendLine("    new PropertyMetadata(default($ReadOnlyProperty" + b + "Type$)));");
            // 
            sb.AppendXmlSummary("Identifies the <see cref=\"$ReadOnlyProperty" + b + "Name$\"/> dependency property.");
            sb.AppendLine("public static readonly DependencyProperty $ReadOnlyProperty" + b + "Name$Property =");
            sb.AppendLine("DependencyProperty.Register(\"$ReadOnlyProperty" + b + "Name$\", typeof($Property" + b + "Type$), typeof($ClassName$), new PropertyMetadata(default($ReadOnlyProperty" + b + "Type$)));");
            sb.AppendXmlSummary("Gets or sets a value of $ReadOnlyProperty" + b + "Name$. This is a dependency property.");
            sb.AppendLine("public $ReadOnlyProperty" + b + "Type$ $ReadOnlyProperty" + b + "Name$");
            sb.AppendLine("{");
            sb.AppendLine("get { return ($ReadOnlyProperty" + b + "Type$)GetValue($ReadOnlyProperty" + b + "Name$Property); }");
            sb.AppendLine("private set { SetValue($ReadOnlyProperty" + b + "Name$Property, value); }");
            sb.AppendLine("}");
          }
          sb.AppendLine("#endregion");
          #endregion
          #region Constructors
          sb.AppendLine("#region Constructors");
          sb.AppendXmlSummary("Initializes a new instance of the <see cref=\"$ClassName$\"/> class.");
          sb.AppendLine("public $ClassName$() {}");

          sb.AppendXmlSummary("Initializes a new fully specified instance of the <see cref=\"$ClassName$\"/> class.");
          for (int k = 1; k <= i; ++k)
          {
            sb.AppendParam("$Property" + k + "Name$", "The $Property" + k + "Name$");
          }
          sb.Append("public $ClassName$(");
          for (int k = 1; k <= i; ++k)
          {
            sb.AppendFormat("$Property{0}Type$ $Property{0}Name$", k);
            if (k != i)
              sb.Append(", ");
          }
          sb.AppendLine(")");
          sb.AppendLine("{");
          for (int k = 1; k <= i; ++k)
          {
            sb.AppendFormat("this.$Property{0}Name$ = $Property{0}Name$;{1}", k, Environment.NewLine);
          }
          sb.AppendLine("}");
          sb.AppendLine("#endregion");
          #endregion
          #region Methods
          sb.AppendLine("#region Methods");
          sb.AppendLine("/// <summary>");
          sb.AppendLine("/// Indicates whether the current object is equal to another object of the same type.");
          sb.AppendLine("/// </summary>");
          sb.AppendLine("/// <param name=\"other\">An object to compare with this object.</param>");
          sb.AppendLine("/// <returns>");
          sb.AppendLine(
            "/// true if the current object is equal to the <paramref name=\"other\"/> parameter; otherwise, false.");
          sb.AppendLine("/// </returns>");
          sb.AppendLine("public bool Equals($ClassName$ other)");
          sb.AppendLine("{");
          sb.AppendLine("if (ReferenceEquals(null, other)) return false;");
          sb.AppendLine("if (ReferenceEquals(this, other)) return true;");
          sb.AppendLine("return");
          for (int k = 1; k <= i; ++k)
          {
            sb.Append("  $Property" + k + "Name$ == other.$Property" + k + "Name$");
            sb.AppendLine(k == i && j == 0 ? ";" : " &&");
          }
          for (int l = 1; l <= j; ++l)
          {
            sb.Append("  $ReadOnlyProperty" + l + "Name$ == other.$ReadOnlyProperty" + l + "Name$");
            sb.AppendLine(l == j ? ";" : " &&");
          }
          sb.AppendLine("}");
          sb.AppendLine("/// <summary>");
          sb.AppendLine("/// Returns a <see cref=\"T:System.String\"/> that represents the current <see cref=\"T:System.Object\"/>.");
          sb.AppendLine("/// </summary>");
          sb.AppendLine("/// <returns>");
          sb.AppendLine("/// A <see cref=\"T:System.String\"/> that represents the current <see cref=\"T:System.Object\"/>.");
          sb.AppendLine("/// </returns>");
          sb.AppendLine("public override string ToString()");
          sb.AppendLine("{");
          sb.AppendLine("StringBuilder sb = new StringBuilder();");
          for (int k = 1; k <= i; ++k)
          {
            sb.Append("sb.Append(\"$Property" + k + "Name$ = \" + $Property" + k + "Name$");
            if (j != i && j == 0) sb.Append(" + \";\"");
            sb.AppendLine(");");
          }
          for (int l = 1; l <= j; ++l )
          {
            sb.Append("sb.Append(\"$ReadOnlyProperty" + l + "Name$ = \" + $ReadOnlyProperty" + l + "Name$");
            if (l != j) sb.Append(" + \";\"");
            sb.AppendLine(");");
          }
          sb.AppendLine("return sb.ToString();");
          sb.AppendLine("}");
          sb.AppendLine("#endregion");
          #endregion
          sb.Append("}");

          sc.Add(s);
        }
      }
      sc.Save("dpentity");
    }

    private static void makeStateMachine()
    { // here we go
      var sc = new SnippetCollection();
      for (int i = 2; i < count*2; ++i)
      {
        var s = new Snippet(author, "fsm" + i, "fsm" + i,
                            "Creates a finite state machine class with " + i + " states.");
        #region parameters
        s.Literals.Add(new SnippetLiteral("MachineName", "Name of state machine", "MachineName"));
        s.Literals.Add(new SnippetLiteral("EnumName", "Name of state enum", "EnumName"));
        s.Literals.Add(new SnippetLiteral("InitialState", "Choose the initial state", "InitialState"));
        for (int j = 1; j <= i; ++j)
          s.Literals.Add(new SnippetLiteral("State" + j, "Name of " + j + ending(j) + " state", "State" + j));
        #endregion

        #region code
        var sb = s.CodeBuilder;

        sb.AppendXmlSummary("$MachineName$ states.");
        sb.AppendLine("internal enum $EnumName$ {");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendXmlSummary("$State" + j + "$.");
          sb.Append("$State" + j + "$");
          if (j != i)
            sb.Append(",");
          sb.AppendLine();
        }
        sb.AppendLine("}");

        // before transition
        sb.AppendLine("internal class Before$MachineName$TransitionEventArgs : CancelEventArgs");
        sb.AppendLine("{");
        sb.AppendXmlSummary("The state from which the transition is occuring.");
        sb.AppendLine("internal $EnumName$ From { get; private set; }");
        sb.AppendXmlSummary("The state to which the transition is happening.");
        sb.AppendLine("internal $EnumName$ To { get; private set; }");
        sb.AppendLine("internal Before$MachineName$TransitionEventArgs($EnumName$ from, $EnumName$ to)");
        sb.AppendLine("{");
        sb.AppendLine("From = from;");
        sb.AppendLine("To = to;");
        sb.AppendLine("}");
        sb.AppendLine("}");

        // after transition - same as before, but not cancelable
        // and thanks to lack of multiple inheritance I cannot really avoid duplicating data
        sb.AppendLine("internal class After$MachineName$TransitionEventArgs : EventArgs");
        sb.AppendLine("{");
        sb.AppendXmlSummary("The state from which the transition is occuring.");
        sb.AppendLine("internal $EnumName$ From { get; private set; }");
        sb.AppendXmlSummary("The state to which the transition is happening.");
        sb.AppendLine("internal $EnumName$ To { get; private set; }");
        sb.AppendLine("internal After$MachineName$TransitionEventArgs($EnumName$ from, $EnumName$ to)");
        sb.AppendLine("{");
        sb.AppendLine("From = from;");
        sb.AppendLine("To = to;");
        sb.AppendLine("}");
        sb.AppendLine("}");

        // the class itself
        sb.AppendXmlSummary("Emulates a finite-state machine with " + i + " states.");
        sb.AppendLine("internal class $MachineName$ {");
        sb.AppendXmlSummary("The current state of the state machine.");
        sb.AppendLine("internal $EnumName$ CurrentState = $EnumName$.$InitialState$;");
        sb.AppendXmlSummary("A dictionary containing all possible transitions allowed by the state machine.");
        sb.AppendLine("internal IDictionary<$EnumName$, $EnumName$> Transitions;");
        sb.AppendXmlSummary("Occurs before a transition is processed. This is a cancelable event.");
        sb.AppendLine("internal event EventHandler<Before$MachineName$TransitionEventArgs> BeforeTransition;");
        sb.AppendXmlSummary(
          "Occurs after a transition is processed. This event is not fired if the transition was cancelled.");
        sb.AppendLine("internal event EventHandler<After$MachineName$TransitionEventArgs> AfterTransition;");

        sb.AppendXmlSummary("Creates a state machine. By default, all transitions are possible.");
        sb.AppendLine("internal $MachineName$()");
        sb.AppendLine("{");
        sb.AppendLine("// remove unnecessary transitions from the list below");
        sb.AppendLine("Transitions = new Dictionary<$EnumName$, $EnumName$> {");
        // now add all transitions, including self-transitions
        for (int a = 1; a <= i; ++a)
        {
          for (int b = 1; b <= i; ++b)
          {
            sb.AppendLine("{ $EnumName$.$State" + a + "$, $EnumName$.$State" + b + "$ },");
          }
        }
        sb.AppendLine("};");
        sb.AppendLine("}");

        sb.AppendXmlSummary("Does the transition.");
        sb.AppendParam("to", "The state to move to.");
        sb.AppendLine("/// <return>Returns <c>true</c> if the transition succeeded, and <c>false</c> otherwise.");
        sb.AppendLine("internal bool DoTransition($EnumName$ to)");
        sb.AppendLine("{");
        sb.AppendLine("  bool canceled = false;");
        sb.AppendLine("  if (BeforeTransition != null)");
        sb.AppendLine("  {");
        sb.AppendLine("    var args = new Before$MachineName$TransitionEventArgs(CurrentState, to);");
        sb.AppendLine("    BeforeTransition(this, args);");
        sb.AppendLine("    canceled = args.Cancel;");
        sb.AppendLine("  }");
        sb.AppendLine("  if (!canceled)");
        sb.AppendBetweenCurlyBraces("CurrentState = to;");
        sb.AppendLine("else");
        sb.AppendBetweenCurlyBraces("return false; // transition was canceled");
        sb.AppendLine("    if (AfterTransition != null)");
        sb.AppendLine("    {");
        sb.AppendLine("      var args = new After$MachineName$TransitionEventArgs(CurrentState, to);");
        sb.AppendLine("      AfterTransition(this, args);");
        sb.AppendLine("    }");
        sb.AppendLine("return true;");
        sb.AppendLine("}");

        // 
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendLine("public bool Is$State" + j + "$ { get { return CurrentState == $EnumName$.$State" + j +
                        "$; } }");
        }

        sb.AppendLine("}");
        #endregion

        sc.Add(s);
      }
      sc.Save("fsm");
    }

    private static void makeFlags()
    { // todo: intersects... maybe... someday :)
      var sc = new SnippetCollection();
      for (int i = 1; i < 64; ++i)
      {
        var s = new Snippet(author, "flags" + i, "flags" + i, "Creates a [Flags]-tagged enum with " + i + " flags.");
        s.Literals.Add(new SnippetLiteral("EnumName", "Name of enumeration", "EnumName"));
        for (int j = 1; j <= i; ++j)
        {
          s.Literals.Add(new SnippetLiteral(
                           "Element" + j, "Name of " + j + ending(j) + " element.", "Element" + j));
        }
        var sb = s.CodeBuilder;
        sb.AppendLine("[Flags]");
        sb.AppendXmlSummary("$EnumName$");
        string type = i <= 8 ? "byte" : (i < 16 ? "short" : (i < 32 ? "int" : "long"));
        sb.AppendLine("enum $EnumName$ : "+type+" {");
        sb.AppendXmlSummary("None (0)");
        sb.AppendLine("None = 0,");
        ulong all = 0;
        for (int j = 1; j <= i; ++j)
        {
          all |= (ulong)1 << (j - 1);
          sb.AppendXmlSummary("$Element" + j + "$ (" + ((ulong)1 << (j - 1)) + ")");
          sb.AppendFormat("$Element{0}$ = {1}", j, (ulong)1 << (j - 1));
          sb.AppendLine(",");
        }
        sb.AppendXmlSummary("All (" + all + ")");
        sb.AppendLine("All = " + all);
        sb.Append("}");
        sc.Add(s);
      }
      sc.Save("flags");
    }

    private static void makePow()
    {
      var sc = new SnippetCollection();
      for (int i = 2; i < count; ++i)
      {
        var s = new Snippet
        {
          Author = author,
          Description = "Creates an inline multiplication equivalent to Math.Pow(..., " + i + ").",
          Shortcut = "pow" + i,
          Title = "pow" + i
        };
        s.Literals.AddLiteral("x", "Variable name");
        var sb = s.CodeBuilder;
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("$x$");
          if (j != i)
            sb.Append("*");
        }
        sc.Add(s);
      }
      sc.Save("pow");
    }

    private static void makeGetflags()
    {
      var sc = new SnippetCollection();
      for (int i = 1; i < count; ++i)
      {
        var s = new Snippet
        {
          Author = author,
          Description = "Tests an enum's bit flags and creates a bool variable for each.",
          Shortcut = "getflags" + i,
          Title = "getflags" + i
        };
        //the literals here are the property and all the flags
        s.Literals.Add(new SnippetLiteral
        {
          ID = "PropName",
          Default = "PropName",
          ToolTip = "Name of property that contains the flags."
        });
        for (int j = 1; j <= i; ++j)
        {
          s.Literals.Add(new SnippetLiteral
                           {
                             ID = "Flag" + j,
                             Default = "Flag" + j,
                             ToolTip = "Name of the " + j + ending(j) + " flag."
                           });
        }
        var sb = s.CodeBuilder;
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendFormat(
            "bool is$Flag{0}$ = (($PropName$ & $Flag{0}$) == $Flag{0}$);", j);
          if (j != i)
            sb.AppendLine();
        }
        sc.Add(s);
      }
      sc.Save("getflags");
    }

    private static void makeVarlist()
    {
      var sc = new SnippetCollection();
      for (int i = 2; i <= 30; ++i) // need more for polys
      {
        var s = new Snippet
                  {
                    Author = author,
                    Description = "Declares and initializes " + i + " variables (a-z) of the same type.",
                    Shortcut = "varlist" + i,
                    Title = "varlist" + i
                  };
        // type and default value
        s.Literals.Add(new SnippetLiteral
                         {
                           ID = "Type",
                           Default = "double",
                           ToolTip = "Data Type"
                         });
        s.Literals.Add(new SnippetLiteral
                         {
                           ID = "DefVal",
                           Default = "0",
                           ToolTip = "Default Value"
                         });
        // body
        var sb = s.CodeBuilder;
        sb.Append("$Type$ ");
        for (int j = 0; j < i; ++j)
        {
          sb.Append((char)('a' + j));
          sb.Append(" = $DefVal$");
          if (j + 1 != i)
            sb.Append(", ");
        }
        sb.AppendLine(";");
        sc.Add(s);
      }
      sc.Save("varlist");
    }

    private static void makePoly()
    {
      var snippets = new SnippetCollection();
      for (int i = 2; i <= count; ++i)
      {
        var s = new Snippet
        {
          Author = author,
          Description = "Creates an " + i + ending(i) + "-order polynomial function (uses Math.Pow).",
          Shortcut = "polyp" + i,
          Title = "polyp" + i
        };
        StringBuilder sb = s.CodeBuilder;
        sb.Append("public double PolyP(double x");
        for (int j = 0; j <= i; ++j)
        {
          sb.Append(", double ");
          sb.Append((char)('a' + j));
        }
        sb.AppendLine(")")
          .AppendLine("{")
          .Append("return ");
        for (int j = i; j >= 0; --j)
        {
          sb.Append((char)('a' + i - j));
          if (j >= 2)
          {
            sb.Append("*");
            sb.AppendFormat("Math.Pow(x, {0})", j);

          }
          else if (j == 1)
          {
            sb.Append("*");
            sb.AppendFormat("x");

          }
          if (j != 0)
            sb.Append(" + ");
        }
        sb.AppendLine(";");
        sb.AppendLine("}");
        snippets.Add(s);

        s = new Snippet
        {
          Author = author,
          Description = "Creates an " + i + ending(i) + "-order polynomial function (uses inlining).",
          Shortcut = "poly" + i,
          Title = "poly" + i
        };
        sb = s.CodeBuilder;
        sb.Append("public double Poly(double x");
        for (int j = 0; j <= i; ++j)
        {
          sb.Append(", double ");
          sb.Append((char)('a' + j));
        }
        sb.AppendLine(")")
          .AppendLine("{")
          .Append("return ");
        for (int j = i; j >= 0; --j)
        {
          sb.Append((char)('a' + i - j));
          if (j >= 2)
          {
            sb.Append("*");
            // now inline jth power of x
            for (int k = 0; k < j; ++k)
            {
              sb.Append("x");
              if (k + 1 != j)
                sb.Append("*");
            }
          }
          else if (j == 1)
          {
            sb.Append("*");
            sb.AppendFormat("x");

          }
          if (j != 0)
            sb.Append(" + ");
        }
        sb.AppendLine(";");
        sb.AppendLine("}");
        snippets.Add(s);
      }
      snippets.Save("poly");
    }

    private static void makeArrStore()
    {
      SnippetCollection stores = new SnippetCollection();
      const int limit = 20;
      for (int i = 1; i <= limit; ++i)
      {
        Snippet sn = new Snippet
                       {
                         Author = author,
                         Description = "Creates an array-based storage class with " + i + " elements.",
                         Shortcut = "arrstore" + i,
                         Title = "arrstore" + i
                       };

        #region params
        sn.Literals.Add(new SnippetLiteral("ClassName", "Name of Class", "ClassName"));
        sn.Literals.Add(new SnippetLiteral("DataType", "Data Type", "int"));
        #endregion
        StringBuilder sb = sn.CodeBuilder;
        sb.AppendXmlSummary("This class stores " + i + " <c>$DataType$</c> values.");
        sb.AppendLine("public sealed class $ClassName$");
        sb.AppendLine("{");
        sb.AppendLine("private $DataType$[] elements;");
        sb.AppendLine("private const int count = " + i + ";");
        sb.AppendXmlSummary("Initializes a new instance of the <see cref=\"$ClassName$\"/> class.");
        sb.AppendLine("public $ClassName$()");
        sb.AppendBetweenCurlyBraces("elements = new $DataType$[count];");
        #region Elements
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendXmlSummary("Gets or sets the " + j + ending(j) + " element.");
          sb.AppendXmlValue("The " + j + ending(j) + " element.");
          sb.AppendLine("public $DataType$ Element" + j);
          sb.AppendLine("{");
          sb.Append("get ");
          sb.AppendBetweenCurlyBraces("return elements[" + (j - 1) + "];");
          sb.Append("set ");
          sb.AppendBetweenCurlyBraces("elements[" + (j - 1) + "] = value;");
          sb.AppendLine("}");
        }
        sb.AppendXmlSummary("Gets all the elements.");
        sb.AppendXmlValue("All the elements.");
        sb.AppendLine("public $DataType$[] Elements");
        sb.AppendBetweenCurlyBraces("get { return elements; }");
        #endregion
        sb.AppendLine("}");
        stores.Add(sn);
      }
      stores.Save("arrstore");
    }

    private static void makeCatch()
    {
      SnippetCollection catches = new SnippetCollection();
      const int limit = 20;
      for (int i = 1; i <= limit; ++i)
      {
        Snippet sn = new Snippet
                       {
                         Author = author,
                         Description = "Creates " + i + " catch statements.",
                         Shortcut = "catch" + i,
                         Title = "catch" + i
                       };
        StringBuilder sb = sn.CodeBuilder;
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendLine("catch (Exception e)");
          sb.AppendLine("{");
          sb.AppendLine("}");
        }
        catches.Add(sn);
      }
      catches.Save("catch");
    }

    private static void makeFors()
    {
      SnippetCollection fors = new SnippetCollection();
      const int limit = 10;
      for (int i = 1; i <= limit; ++i)
      {
        for (char j = 'a'; j <= 'z' && i <= ('z' - j + 1); ++j)
        {
          Snippet sn = new Snippet
          {
            Author = author,
            Description =
              "Creates a " + i + "-level inset for loop with identifiers starting at '" + j + "'.",

            Shortcut = string.Format("for{0}{1}", j, i),
            Title = string.Format("for{0}{1}", j, i)
          };
          StringBuilder sb = sn.CodeBuilder;
          for (char k = j; k <= (char)(j + i - 1); ++k)
          {
            // add the limit literal
            SnippetLiteral sl = new SnippetLiteral
            {
              Default = string.Empty + "" + k + "Limit",
              ID = string.Empty + "" + k + "Limit",
              ToolTip = "Limit of variable " + k
            };
            sn.Literals.Add(sl);

            // append the loop and curly
            sb.AppendFormat("for (int {0} = 0; {0} < ${1}$; ++{0}){2}", k, sl.ID, Environment.NewLine);
            sb.AppendLine("{");
          }
          for (char k = j; k <= (char)(j + i - 1); ++k)
          {
            sb.Append("}");
            if (k != (char)(j + i - 1))
              sb.AppendLine();
          }
          fors.Add(sn);
        }
      }
      fors.Save("for");
    }

    private static void makeParrs()
    {
      SnippetCollection parrs = new SnippetCollection();
      const int limit = 20;
      for (int i = 1; i <= limit; ++i)
      {
        Snippet parr1 = new Snippet
        {
          Author = author,
          Description = "Executes " + i + " processes in parallel.",
          Shortcut = "parr" + i,
          Title = "parr" + i
        };
        StringBuilder sb1 = parr1.CodeBuilder;
        for (int j = 1; j <= i; ++j)
        {
          sb1.AppendFormat("AutoResetEvent are{0} = new AutoResetEvent(false);{1}", j, Environment.NewLine);
        }
        for (int j = 1; j <= i; ++j)
        {
          sb1.AppendLine("ThreadPool.QueueUserWorkItem(delegate");
          sb1.AppendLine("{");
          sb1.AppendLine("// Thread " + j + " code here");
          sb1.AppendLine("are" + j + ".Set();");
          sb1.AppendLine("});");
        }
        sb1.Append("WaitHandle.WaitAll(new WaitHandle[] { ");
        for (int j = 1; j <= i; ++j)
        {
          sb1.Append("are" + j);
          if (j != i)
            sb1.Append(", ");
        }
        sb1.AppendLine("});");
        parrs.Add(parr1);
      }
      parrs.Save("parr");
    }

    private static void makeArrs()
    {
      SnippetCollection arrs = new SnippetCollection();
      const int xLimit = 20, yLimit = 20;
      for (int i = 1; i <= xLimit; ++i)
      {
        #region 1d
        Snippet s1d = new Snippet
        {
          Author = author,
          Description = ("Declares a one-dimensional array with " + i + " elements."),
          Shortcut = ("array" + i),
          Title = ("array" + i)
        };
        #region arguments
        SnippetLiteral name = new SnippetLiteral
        {
          ID = "VarName",
          Default = "VarName",
          ToolTip = "Variable Name"
        };
        SnippetLiteral dv = new SnippetLiteral
        {
          ID = "DefVal",
          Default = "DefVal",
          ToolTip = "Default Value"
        };
        SnippetLiteral tp = new SnippetLiteral
        {
          ID = "Type",
          Default = "Type",
          ToolTip = "Data Type"
        };
        s1d.Literals.Add(name);
        s1d.Literals.Add(dv);
        s1d.Literals.Add(tp);
        #endregion
        StringBuilder sb1 = s1d.CodeBuilder;
        sb1.Append("$Type$[] $VarName$ = { ");
        for (int j = 1; j <= i; ++j)
        {
          sb1.Append("$DefVal$");
          if (i != j)
            sb1.Append(", ");
        }
        sb1.Append(" };");

        arrs.Add(s1d);
        #endregion
        for (int j = 1; j <= yLimit; ++j)
        {
          Snippet s2d = new Snippet
          {
            Author = author,
            Description = ("Declares an " + i + "×" + j + " multidimensional array."),
            Shortcut = ("array" + i + "by" + j),
            Title = ("array" + i + "by" + j)
          };
          // same arglist as above
          #region arguments
          SnippetLiteral name2 = new SnippetLiteral
          {
            ID = "VarName",
            Default = "VarName",
            ToolTip = "Variable Name"
          };
          SnippetLiteral dv2 = new SnippetLiteral
          {
            ID = "DefVal",
            Default = "DefVal",
            ToolTip = "Default Value"
          };
          SnippetLiteral tp2 = new SnippetLiteral
          {
            ID = "Type",
            Default = "Type",
            ToolTip = "Data Type"
          };
          SnippetLiteral diag2 = new SnippetLiteral
           {
             ID = "DiagVal",
             Default = "DiagVal",
             ToolTip = "Diagonal Value"
           };
          s2d.Literals.Add(name2);
          s2d.Literals.Add(dv2);
          s2d.Literals.Add(tp2);
          if (i == j)
            s2d.Literals.Add(diag2);
          #endregion
          // build it!
          StringBuilder sb2 = s2d.CodeBuilder;
          sb2.AppendLine("$Type$[,] $VarName$ = {");
          for (int x = 1; x <= i; ++x)
          {
            sb2.Append("  { ");
            for (int y = 1; y <= j; ++y)
            {
              if (i == j && x == y)
                sb2.Append("$DiagVal$");
              else
                sb2.Append("$DefVal$");
              if (y != j)
                sb2.Append(", ");
            }
            sb2.Append(" }");
            if (x != i)
              sb2.Append(",");
            sb2.AppendLine();
          }
          sb2.Append("};");

          arrs.Add(s2d);
        }
      }
      arrs.Save("arrays");
    }

    private static void makeArglists()
    {
      SnippetCollection arglists = new SnippetCollection();
      for (int i = 1; i <= count; ++i)
      {
        Snippet s = new Snippet
        {
          Author = author,
          Description = ("An argument list with " + i + " elements. Indices start with 1."),
          Shortcut = ("arglist" + i),
          Title = ("arglist" + i)
        };
        SnippetLiteral sl = new SnippetLiteral
        {
          ID = "Arg",
          Default = "Arg",
          ToolTip = "Argument prefix."
        };
        s.Literals.Add(sl);

        StringBuilder sb = s.CodeBuilder;
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("$Arg$" + j);
          if (j != i)
            sb.Append(", ");
        }

        arglists.Add(s);
      }
      arglists.Save("arglist");
    }

    private static void makeEntities()
    {
      var tuples = new SnippetCollection();
      for (int i = 1; i <= count; ++i)
      {
        var s = new Snippet
        {
          Author = author,
          Description = "Strongly typed entity class with " + i + " auto-properties.",
          Shortcut = "entity" + i + "auto",
          Title = "entity" + i + "auto"
        };
        // class name
        SnippetLiteral sl = new SnippetLiteral
        {
          ID = "ClassName",
          Default = ("Entity" + i),
          ToolTip = "Name of this tuple class."
        };
        s.Literals.Add(sl);
        // property names and types
        for (int j = 1; j <= i; ++j)
        {
          var nameLit = new SnippetLiteral
          {
            ID = "Prop" + j + "Name",
            Default = "Prop" + j + "Name",
            ToolTip = "Name of " + j + ending(j) + " property."
          };
          s.Literals.Add(nameLit);
          var typeLit = new SnippetLiteral
          {
            ID = "Prop" + j + "Type",
            Default = "int",
            ToolTip = "Type of " + j + ending(j) + " property."
          };
          s.Literals.Add(typeLit);
        }
        // make the class
        var sb = s.CodeBuilder;
        sb.AppendXmlSummary("$ClassName$.");
        sb.AppendLine("[Serializable]");
        sb.AppendLine("internal class $ClassName$ : IEquatable<$ClassName$>");
        sb.AppendLine("{");
        #region properties
        sb.AppendLine("#region Properties");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendXmlSummary("Gets or sets the $Prop" + j + "Name$.");
          sb.AppendXmlValue("The $Prop" + j + "Name$.");
          sb.AppendLine(string.Format("internal $Prop{0}Type$ $Prop{0}Name$ {{ get; set; }}", j));
        }
        sb.AppendLine("#endregion");
        #endregion
        #region constructors
        sb.AppendLine("#region Constructors");
        sb.AppendXmlSummary("Initializes a new instance of the <see cref=\"$ClassName$\"/> class.");
        sb.AppendLine("internal $ClassName$() {}");

        sb.AppendXmlSummary("Initializes a new fully specified instance of the <see cref=\"$ClassName$\"/> class.");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendParam("$Prop" + j + "Name$", "The $Prop" + j + "Name$");
        }
        sb.Append("internal $ClassName$(");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendFormat("$Prop{0}Type$ $Prop{0}Name$", j);
          if (j != i)
            sb.Append(", ");
        }
        sb.AppendLine(")");
        sb.AppendLine("{");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendFormat("this.$Prop{0}Name$ = $Prop{0}Name$;{1}", j, Environment.NewLine);
        }
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        #endregion
        #region methods
        sb.AppendLine("#region Methods");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// Determines whether the specified <see cref=\"T:System.Object\"/> is equal to the current <see cref=\"T:System.Object\"/>.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("/// <param name=\"obj\">The <see cref=\"T:System.Object\"/> to compare with the current <see cref=\"T:System.Object\"/>.</param>");
        sb.AppendLine("/// <returns>");
        sb.AppendLine("/// true if the specified <see cref=\"T:System.Object\"/> is equal to the current <see cref=\"T:System.Object\"/>; otherwise, false.");
        sb.AppendLine("/// </returns>");
        sb.AppendLine("/// <exception cref=\"T:System.NullReferenceException\">The <paramref name=\"obj\"/> parameter is null.</exception>");
        sb.AppendLine(@"public override bool Equals(object obj)
{
  $ClassName$ other = obj as $ClassName$;
  if (other != null)
    return Equals(other);
  return false;
}");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// Indicates whether the current object is equal to another object of the same type.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("/// <param name=\"other\">An object to compare with this object.</param>");
        sb.AppendLine("/// <returns>");
        sb.AppendLine(
          "/// true if the current object is equal to the <paramref name=\"other\"/> parameter; otherwise, false.");
        sb.AppendLine("/// </returns>");
        sb.AppendLine("public bool Equals($ClassName$ other)");
        sb.AppendLine("{");
        sb.AppendLine("if (ReferenceEquals(null, other)) return false;");
        sb.AppendLine("if (ReferenceEquals(this, other)) return true;");
        sb.AppendLine("return");
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("  $Prop" + j + "Name$ == other.$Prop" + j + "Name$");
          sb.AppendLine(j == i ? ";" : " &&");
        }
        sb.AppendLine("}");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// Returns a <see cref=\"T:System.String\"/> that represents the current <see cref=\"T:System.Object\"/>.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("/// <returns>");
        sb.AppendLine("/// A <see cref=\"T:System.String\"/> that represents the current <see cref=\"T:System.Object\"/>.");
        sb.AppendLine("/// </returns>");
        sb.AppendLine("public override string ToString()");
        sb.AppendLine("{");
        sb.AppendLine("StringBuilder sb = new StringBuilder();");
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("sb.Append(\"$Prop" + j + "Name$ = \" + $Prop" + j + "Name$");
          if (j != i) sb.Append(" + \";\"");
          sb.AppendLine(");");
        }
        sb.AppendLine("return sb.ToString();");
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        #endregion
        sb.AppendLine("}");
        // add it!
        tuples.Add(s);
      }
      tuples.Save("entitiesauto");
    }

    private static void makeSimpleTuples()
    {
      SnippetCollection tuples = new SnippetCollection();
      for (int i = 1; i <= count; ++i)
      {

        // prep type arguments
        StringBuilder typeArgs = new StringBuilder();
        for (int j = 1; j <= i; ++j)
        {
          typeArgs.Append("T" + j);
          if (j != i)
            typeArgs.Append(", ");
        }

        Snippet s = new Snippet
        {
          Author = author,
          Description = ("A tuple of size " + i + "."),
          Shortcut = ("tuple" + i + "simple"),
          Title = ("tuple" + i + "simple")
        };
        // add the literals
        SnippetLiteral sl = new SnippetLiteral
        {
          ID = "ClassName",
          Default = ("Tuple" + i),
          ToolTip = "Name of this tuple class."
        };
        s.Literals.Add(sl);
        // let's build the code
        StringBuilder sb = s.CodeBuilder;
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// A tuple of size " + i + ".");
        sb.AppendLine("/// </summary>");
        for (int j = 1; j <= i; ++j)
          sb.AppendLine("/// <typeparam name=\"T" + j + "\">The type of the " + j + ending(j) +
          " element of the tuple.</typeparam>");
        sb.Append("internal class $ClassName$ <");
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("T" + j);
          if (j != i)
            sb.Append(", ");
        }
        sb.AppendLine("> : IEquatable<$ClassName$<" + typeArgs + ">> {");
        #region properties
        sb.AppendLine("#region Fields");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendLine("  private T" + j + " element" + j + ";");
        }
        sb.AppendLine("#endregion");
        sb.AppendLine("#region Properties");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendLine("  /// <summary>");
          sb.AppendLine("  /// Gets or sets the " + j + ending(j) + " element.");
          sb.AppendLine("  /// </summary>");
          sb.AppendLine("  /// <value>The " + j + ending(j) + " element.</value>");
          sb.AppendLine("  internal T" + j + " Element" + j + " {");
          sb.AppendLine("    get { return element" + j + "; }");
          sb.AppendLine("    set { element" + j + " = value; }");
          sb.AppendLine("  }");
        }
        sb.AppendLine("#endregion");
        #endregion
        #region constructors
        sb.AppendLine("#region Constructors");
        sb.AppendLine("/// <summary>");
        sb.Append("/// Initializes a new instance of the <see cref=\"$ClassName$&lt;");
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("T" + j);
          if (j != i)
            sb.Append(", ");
        }
        sb.AppendLine("&gt;\"/> class.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("internal $ClassName$() {}");
        sb.AppendLine("/// <summary>");
        sb.Append("/// Initializes a new instance of the <see cref=\"$ClassName$&lt;");
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("T" + j);
          if (j != i)
            sb.Append(", ");
        }
        sb.AppendLine("&gt;\"/> class.");
        sb.AppendLine("/// </summary>");
        for (int j = 1; j <= i; ++j)
          sb.AppendLine("/// <param name=\"element" + j + "\">The " + j + ending(j) + " element.</param>");
        sb.Append("  internal $ClassName$(");
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("T" + j + " element" + j);
          if (j != i)
            sb.Append(", ");
        }
        sb.AppendLine(") {");
        for (int j = 1; j <= i; ++j)
        {
          sb.AppendLine("    this.element" + j + " = element" + j + ";");
        }
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        #endregion
        #region methods

        sb.AppendLine("#region Methods");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// Determines whether the specified <see cref=\"T:System.Object\"/> is equal to the current <see cref=\"T:System.Object\"/>.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("/// <param name=\"obj\">The <see cref=\"T:System.Object\"/> to compare with the current <see cref=\"T:System.Object\"/>.</param>");
        sb.AppendLine("/// <returns>");
        sb.AppendLine("/// true if the specified <see cref=\"T:System.Object\"/> is equal to the current <see cref=\"T:System.Object\"/>; otherwise, false.");
        sb.AppendLine("/// </returns>");
        sb.AppendLine("/// <exception cref=\"T:System.NullReferenceException\">The <paramref name=\"obj\"/> parameter is null.</exception>");
        sb.AppendLine(@"public override bool Equals(object obj)
{
  $ClassName$<" + typeArgs + "> other = obj as $ClassName$<" + typeArgs + @">;
  if (other != null)
    return Equals(other);
  return false;
}");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// Indicates whether the current object is equal to another object of the same type.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("/// <param name=\"other\">An object to compare with this object.</param>");
        sb.AppendLine("/// <returns>");
        sb.AppendLine(
          "/// true if the current object is equal to the <paramref name=\"other\"/> parameter; otherwise, false.");
        sb.AppendLine("/// </returns>");
        sb.AppendLine("public bool Equals($ClassName$<" + typeArgs + "> other)");
        sb.AppendLine("{");
        sb.AppendLine("return");
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("  Element" + j + ".Equals(other.Element" + j + ")");
          sb.AppendLine(j == i ? ";" : " &&");
        }
        sb.AppendLine("}");
        sb.AppendLine("/// <summary>");
        sb.AppendLine("/// Returns a <see cref=\"T:System.String\"/> that represents the current <see cref=\"T:System.Object\"/>.");
        sb.AppendLine("/// </summary>");
        sb.AppendLine("/// <returns>");
        sb.AppendLine("/// A <see cref=\"T:System.String\"/> that represents the current <see cref=\"T:System.Object\"/>.");
        sb.AppendLine("/// </returns>");
        sb.AppendLine("public override string ToString()");
        sb.AppendLine("{");
        sb.AppendLine("StringBuilder sb = new StringBuilder();");
        for (int j = 1; j <= i; ++j)
        {
          sb.Append("sb.Append(\"Element" + j + " = \" + Element" + j);
          if (j != i) sb.Append(" + \";\"");
          sb.AppendLine(");");
        }
        sb.AppendLine("return sb.ToString();");
        sb.AppendLine("}");
        sb.AppendLine("#endregion");
        #endregion
        sb.AppendLine("}");

        tuples.Add(s);
      }
      //Console.WriteLine(tuples.ToString());
      tuples.Save("tuples");
    }

    #endregion
  }
}