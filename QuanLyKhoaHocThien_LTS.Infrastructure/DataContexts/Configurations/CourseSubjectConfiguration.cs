﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class CourseSubjectConfiguration : IEntityTypeConfiguration<CourseSubject>
    {
        public void Configure(EntityTypeBuilder<CourseSubject> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.Course).WithMany(c => c.CourseSubjects).HasForeignKey(c => c.CourseId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Subject).WithMany(c => c.CourseSubjects).HasForeignKey(c => c.SubjectId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
