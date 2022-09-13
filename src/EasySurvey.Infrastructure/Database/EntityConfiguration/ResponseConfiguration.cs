using EasySurvey.Domain.WriteModels.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EasySurvey.Infrastructure.Database.EntityConfiguration
{
	public class ResponseConfiguration : IEntityTypeConfiguration<Response>
	{
		public void Configure(EntityTypeBuilder<Response> builder)
		{
			builder.ToTable(SchemaObjects.Response, SchemaObjects.SurveysSchema);

			builder.HasKey(t => t.ResponseId);

			builder.Property(t => t.ResponseId)
				.HasColumnName("RESPONSE_ID")
				.HasGuidConversion()
				.IsRequired();

			builder.Property(t => t.SurveyId)
				.HasColumnName("SURVEY_ID")
				.HasGuidConversion()
				.IsRequired();

			builder.Property(t => t.CreatedDate)
				.HasColumnName("CREATED_DTM")
				.IsRequired();

			builder.Property(t => t.UserId)
				.HasColumnName("OWNER_USER_ID")
				.IsRequired();

			builder.Property(t => t.SubmittedDate)
				.HasColumnName("SUBMITTED_DTM");

			builder.HasMany(x => x.Answers)
				.WithOne()
				.HasForeignKey(t => t.ResponseId);
		}
	}
}
