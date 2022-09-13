using EasySurvey.Domain.WriteModels.Responses;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Infrastructure.Database.EntityConfiguration
{
	internal static class Helpers
	{
		internal static PropertyBuilder<Guid> HasGuidConversion(this PropertyBuilder<Guid> builder)
		{
			return builder.HasConversion(v => v.ToString().ToLowerInvariant(), v => Guid.Parse(v));
		}
	}
}
