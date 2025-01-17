﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;

namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class RegisterStudyConfiguration : IEntityTypeConfiguration<RegisterStudy>
    {
        public void Configure(EntityTypeBuilder<RegisterStudy> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.User).WithMany(c => c.RegisterStudies).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.Course).WithMany(c => c.RegisterStudies).HasForeignKey(c => c.CourseId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.CurrentSubject).WithMany(c => c.RegisterStudies).HasForeignKey(c => c.CurrentSubjectId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
