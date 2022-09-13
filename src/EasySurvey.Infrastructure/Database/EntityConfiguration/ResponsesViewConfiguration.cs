using EasySurvey.Domain.ReadModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasySurvey.Infrastructure.Database.EntityConfiguration
{
	public class ResponsesViewConfiguration : IEntityTypeConfiguration<ResponseBasicView>
	{
		public void Configure(EntityTypeBuilder<ResponseBasicView> builder)
		{
			builder.ToTable(SchemaObjects.ResponsesView, SchemaObjects.SurveysSchema);

			builder.HasKey(t => t.ResponseId);

			builder.Property(t => t.ResponseId)
				.HasColumnName("RESPONSE_ID")
				.IsRequired();

			builder.Property(t => t.CreatedDtm)
				.HasColumnName("CREATED_DTM")
				.IsRequired();	
			
			builder.Property(t => t.LastUpdatedDtm)
				.HasColumnName("LAST_UPDATED_DTM")
				.IsRequired();

			builder.Property(t => t.SubmittedDtm)
				.HasColumnName("SUBMITTED_DTM");
			
			builder.Property(t => t.AnswersCount)
				.HasColumnName("ANSWERS_COUNT")
				.IsRequired();	
			
			builder.Property(t => t.UserId)
				.HasColumnName("OWNER_USER_ID")
				.IsRequired();

			builder.Property(t => t.SurveyId)
				.HasColumnName("SURVEY_ID");


		}
	}
}
