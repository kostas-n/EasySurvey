using EasySurvey.Domain.WriteModels.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EasySurvey.Infrastructure.Database.EntityConfiguration
{
	public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
	{
		public void Configure(EntityTypeBuilder<Answer> builder)
		{
			builder.ToTable(SchemaObjects.Answer, SchemaObjects.SurveysSchema);

			builder.HasKey(t => t.AnswerId);

			builder.Property(t => t.AnswerId)
				.HasColumnName("ANSWER_ID")
				.UseOracleIdentityColumn()
				.IsRequired();

			builder.Property(t => t.ResponseId)
				.HasColumnName("RESPONSE_ID")
				.HasGuidConversion()
				.IsRequired();

			builder.Property(t => t.QuestionId)
				.HasColumnName("QUESTION_ID")
				.IsRequired();

			builder.Property(t => t.Text)
				.HasColumnName("ANSWER_VALUE");

			builder.Property(t => t.Number)
				.HasColumnName("NUMERIC_VALUE");		
			
			builder.Property(t => t.Comment)
				.HasColumnName("NOTE");

			builder.Property(t => t.UpdatedDtm)
				.HasColumnName("CHANGED_DTM");

			builder.Ignore(t => t.SelectedQuestionOptionIds);
		}
	}
}
