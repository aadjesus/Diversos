// -----------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq.Expressions;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	internal static class ConstraintServices
	{
		static bool MetadataMatch(this ExportDefinition edef, string key, object constant)
		{
			object value;
			if (!edef.Metadata.TryGetValue(key, out value))
				return false;
			return constant.Equals(value);
		}

		public static Func<ExportDefinition, bool> CreateConstraint(string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, CreationPolicy requiredCreationPolicy)
		{
			return edef =>
			{
				Assumes.NotNull(edef);

				if (edef.ContractName != (contractName ?? ""))
					return false;

				if (!string.IsNullOrEmpty(requiredTypeIdentity) 
					&& !edef.MetadataMatch(CompositionConstants.ExportTypeIdentityMetadataName, requiredTypeIdentity))
					return false;

				ParameterExpression parameter = Expression.Parameter(typeof(ExportDefinition), "exportDefinition");
				if (requiredMetadata != null)
				{
					foreach (KeyValuePair<string, Type> requiredMetadataItem in requiredMetadata)
						if (!edef.MetadataMatch(requiredMetadataItem.Key, requiredMetadataItem.Value))
							return false;
				}

				if (requiredCreationPolicy != CreationPolicy.Any)
				{
					var ok = 
						!edef.Metadata.ContainsKey(CompositionConstants.PartCreationPolicyMetadataName) 
						|| CreationPolicy.Any.Equals(edef.Metadata[CompositionConstants.PartCreationPolicyMetadataName])
						|| requiredCreationPolicy.Equals(edef.Metadata[CompositionConstants.PartCreationPolicyMetadataName]);
					if (!ok)
						return false;
				}

				return true;
			};
		}

		public static Func<ExportDefinition, bool> CreatePartCreatorConstraint(Func<ExportDefinition, bool> baseConstraint, ImportDefinition productImportDefinition)
		{
			Assumes.NotNull(baseConstraint);
			Assumes.NotNull(productImportDefinition);
			return edef => 
			{
				Assumes.NotNull(edef);
				return
					baseConstraint(edef)
					&& edef.Metadata.ContainsKey("ProductDefinition")
					&& productImportDefinition.Constraint((ExportDefinition)edef.Metadata["ProductDefinition"]);
			};
		}
	}
}
