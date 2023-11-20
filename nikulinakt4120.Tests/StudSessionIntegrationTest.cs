using proekt;
using proekt.Interfaces.StudSessionInterface;
using proekt.Database;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using static proekt.Interfaces.StudSessionInterface.IStudSessionInterface;
using proekt.Models;

namespace nikulinakt4120.Tests
{
    public class StudSessionIntegrationTest
    {
        public readonly DbContextOptions<StudSessionDbContext> _dbContextOptions;

        public StudSessionIntegrationTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<StudSessionDbContext>()
                .UseInMemoryDatabase(databaseName: "test_db")
                .Options;
        }
        [Fact]
        public async Task AddGradeAsync_AddsGrade()
        {
            // Arrange
            var ctx = new StudSessionDbContext(_dbContextOptions);
            var gradeService = new StudSessionService(ctx);

            var student = new Student
            {
                FirstName = "Котик",
                LastName = "София",
                SecName = "Никитьевна"
            };
            await ctx.Set<Student>().AddAsync(student);
            await ctx.SaveChangesAsync();

            var grade = new StudSession
            {
                GradeNumber = 3,
                GradeDate = new DateTime(2023, 01, 25),
                StudentId = student.StudentId
            };

            // Act
            await gradeService.AddGradeAsync(grade);

            // Assert
            var addedGrade = await ctx.Set<StudSession>().SingleOrDefaultAsync(s => s.GradeNumber == 3);
            Assert.NotNull(addedGrade);
            Assert.Equal(new DateTime(2023, 01, 25), addedGrade.GradeDate);
            Assert.Equal(student.StudentId, addedGrade.StudentId);
        }
        [Fact]
        public async Task UpdateGradeAsync_Id_Student()
        {
            // Arrange
            var ctx = new StudSessionDbContext(_dbContextOptions);
            var gradeService = new StudSessionService(ctx);

            var student = new Student
            {
                StudentId = 1,
                FirstName = "Полевой",
                LastName = "Синий",
                SecName = "Трактор"
            };
            await ctx.Set<Student>().AddAsync(student);
            await ctx.SaveChangesAsync();

            var grade = new StudSession
            {
                GradeNumber = 4,
                GradeDate = new DateTime(2023, 12, 12),
                StudentId = 1
            };
            await ctx.Set<StudSession>().AddAsync(grade);
            await ctx.SaveChangesAsync();

            // Act
            grade.GradeNumber = 5;
            grade.GradeDate = new DateTime(2023, 12, 12);
            grade.StudentId = 1;
            await gradeService.UpdateGradeAsync(grade, CancellationToken.None);

            // Assert
            Assert.Equal(5, grade.GradeNumber);
        }
    }
}
