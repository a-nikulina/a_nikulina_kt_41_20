using Microsoft.EntityFrameworkCore;
using proekt.Database;
using proekt.Models;
using System.Diagnostics;
using static proekt.Interfaces.StudSessionInterface.IStudSessionInterface;

namespace proekt
{
    public class nikulinakt4120Test
    {
        public readonly DbContextOptions<StudSessionDbContext> _dbContextOptions;

        public nikulinakt4120Test()
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
                FirstName = "Михайлов",
                LastName = "Сергей",
                SecName = "Петрович"
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
                FirstName = "Орлов",
                LastName = "Максим",
                SecName = "Максимович"
            };
            await ctx.Set<Student>().AddAsync(student);
            await ctx.SaveChangesAsync();

            var grade = new StudSession
            {
                GradeNumber = 4,
                GradeDate = new DateTime(2023, 12, 10),
                StudentId = 1
            };
            await ctx.Set<StudSession>().AddAsync(grade);
            await ctx.SaveChangesAsync();

            // Act
            grade.GradeNumber = 5;
            grade.GradeDate = new DateTime(2023, 12, 10);
            grade.StudentId = 1;
            await gradeService.UpdateGradeAsync(grade, CancellationToken.None);

            // Assert
            Assert.Equal(5, grade.GradeNumber);
        }

    }
}
