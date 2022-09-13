using EasySurvey.Domain.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Infrastructure.Database.EntityConfiguration
{
	public class LatestAnswerConfiguration : IEntityTypeConfiguration<LatestAnswer>
	{
		public void Configure(EntityTypeBuilder<LatestAnswer> builder)
		{
			builder.ToTable(SchemaObjects.LatestAnswer, SchemaObjects.SurveysSchema);

			builder.HasKey(t => t.AnswerId);

			builder.Property(t => t.AnswerId)
				.HasColumnName("ANSWER_ID")
				.IsRequired();

			builder.Property(t => t.ResponseId)
				.HasColumnName("RESPONSE_ID")
				.HasGuidConversion()
				.IsRequired();

			builder.Property(t => t.QuestionId)
				.HasColumnName("QUESTION_ID")
				.IsRequired();

			builder.Property(t => t.ChangedDtm)
				.HasColumnName("CHANGED_DTM")
				.IsRequired();

			builder.Property(t => t.TextValue)
				.HasColumnName("ANSWER_VALUE");

			builder.Property(t => t.NumericValue)
				.HasColumnName("NUMERIC_VALUE");

			builder.Property(t => t.DateValue)
				.HasColumnName("DATE_VALUE");

			builder.Property(t => t.Comment)
				.HasColumnName("NOTE");

			builder.Ignore(t => t.SelectedQuestionOptionIds);
		}
	}
}
