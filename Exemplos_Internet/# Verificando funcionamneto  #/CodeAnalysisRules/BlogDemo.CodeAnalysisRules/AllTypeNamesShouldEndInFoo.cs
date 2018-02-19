using System;
using Microsoft.FxCop.Sdk;
using Microsoft.VisualStudio.CodeAnalysis.Extensibility;

namespace BlogDemo.CodeAnalysisRules
{
    public class RegrasBGMRodotec : BaseRule
    {
        public RegrasBGMRodotec()
            : base("RegrasBGMRodotec")
        {
        }

        public override ProblemCollection Check(TypeNode type)
        {
            if (!type.Name.Name.EndsWith("DTO", StringComparison.Ordinal))
            {
                Problems.Add(new Problem(GetResolution(type.Name.Name), type)
                {
                    Certainty = 200,
                    FixCategory = FixCategories.Breaking,
                    MessageLevel = MessageLevel.Warning
                }


                );
            }

            return Problems;
        }

    }
}