using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using proekt.Database.Helpers;
using System.Diagnostics;
using proekt.Models;

namespace proekt.Database.Configurations
{
    public class StudSessionConfiguration : IEntityTypeConfiguration<StudSession>
    {
        private const string TableName = "StudSession";

        public void Configure(EntityTypeBuilder<StudSession> builder)
        {
            builder
                    .HasKey(p => p.GradeId)
                    .HasName($"pk_(TableName)_GradeId");

            builder.Property(p => p.GradeId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.GradeId)
                .HasColumnName("grade_id")
                .HasComment("Идентификатор успеваемости");

            builder.Property(p => p.GradeNumber)
                .IsRequired()
                .HasColumnName("c_student_gradenumber")
                .HasColumnType(ColumnType.Int)
                .HasComment("Оценка");

            builder.Property(p => p.GradeDate)
                .IsRequired()
                .HasColumnName("c_student_gradedate")
                .HasColumnType(ColumnType.Date)
                .HasComment("Дата оценки");

            builder.Property(p => p.StudentId)
                .IsRequired()
                .HasColumnName("c_student_studentid")
                .HasColumnType(ColumnType.Int)
                .HasComment("Идентификатор студента");

            builder.ToTable(TableName)
                .HasOne(p => p.Student)
                .WithMany(a => a.StudSession)
                .HasForeignKey(P => P.StudentId)
                .HasConstraintName("fk_f_student_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(TableName)
                .HasIndex(p => p.StudentId, $"idx_(TableName)_fk_f_student_id");

            builder.Navigation(p => p.Student)
                .AutoInclude();

        }
    }
}

