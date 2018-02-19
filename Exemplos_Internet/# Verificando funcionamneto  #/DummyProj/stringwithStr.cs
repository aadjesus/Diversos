using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.FxCop.Sdk;
//using Microsoft.FxCop.Sdk.Introspection;
//using Microsoft.Cci;
using System.Text.RegularExpressions;

//using Microsoft.FxCop. Cci;
using Microsoft.FxCop.Sdk;
//using Microsoft.FxCop.Sdk.Introspection;

public class stringwithStr : BaseIntrospectionRule
{
    public stringwithStr()
        : base(@"stringwithStr", "DummyProj.DLADesignRules", typeof(stringwithStr).Assembly)
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public override ProblemCollection Check(Member member)
    {
        // Member Variables

        // Look for str in local variable names
        Method method = member as Method;
        if (method == null)
        {
            return null;
        }
        LocalList list = null;

        if (method.Instructions.Length > 0)
        {
            list = method.Instructions[0].Value as LocalList;
        }
        if (list != null)
        {
            for (int i = 0; i < list.Length; i++)
            {
                Local local = list[i];

                if (local.Type == SystemTypes.String)
                {
                    if (ContainsStr(local.Name.Name))
                    {
                        // Found a local containing a numeric in the name
                        base.Problems.Add(new Problem(base.GetResolution(local.Name.Name), local.Name.Name));

                    }
                }
            }
        }

        return base.Problems;
    }


    private bool ContainsStr(string variableName)
    {

        //NOTE:
        //Use regexp instaed of substring, which will be more faster.
        if (!(variableName.Substring(0, 3).ToString().Trim() == "str"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }


}

