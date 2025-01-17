﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuanLyKhoaHocThien_LTS.Domain.Entities;

namespace QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts.Configurations
{
    public class RunTestCaseConfiguration : IEntityTypeConfiguration<RunTestCase>
    {
        public void Configure(EntityTypeBuilder<RunTestCase> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.DoHomework).WithMany(c => c.RunTestCases).HasForeignKey(c => c.DoHomeworkId).OnDelete(DeleteBehavior.ClientNoAction);

            builder.HasOne(c => c.TestCase).WithMany(c => c.RunTestCases).HasForeignKey(c => c.TestCaseId).OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
