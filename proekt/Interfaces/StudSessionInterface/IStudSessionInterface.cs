using Microsoft.EntityFrameworkCore;
using proekt.Database;
using proekt.Models;
using System.Diagnostics;

namespace proekt.Interfaces.StudSessionInterface
{
    public class IStudSessionInterface
    {
        public interface IStudSessionService
        {
            public Task<StudSession[]> GetGradesAsync(CancellationToken cancellationToken);
            public Task AddGradeAsync(StudSession grade, CancellationToken cancellationToken);
            public Task UpdateGradeAsync(StudSession grade, CancellationToken cancellationToken);
        }

        public class StudSessionService : IStudSessionService
        {
            public readonly StudSessionDbContext _dbContext;
            public StudSessionService(StudSessionDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<StudSession[]> GetGradesAsync(CancellationToken cancellationToken = default)
            {
                return await _dbContext.StudSession.Select(d => new StudSession
                {
                    GradeId = d.GradeId,
                    GradeNumber = d.GradeNumber,
                    GradeDate = d.GradeDate,
                    StudentId = d.StudentId
                }).ToArrayAsync();
            }

            public async Task AddGradeAsync(StudSession grade, CancellationToken cancellationToken = default)
            {
                var student = await _dbContext.Students.FindAsync(grade.StudentId);
                grade.Student = student;

                _dbContext.StudSession.Add(grade);
                await _dbContext.SaveChangesAsync();
            }

            public async Task UpdateGradeAsync(StudSession grade, CancellationToken cancellationToken = default)
            {
                var student = await _dbContext.Students.FindAsync(grade.StudentId);
                grade.Student = student;

                _dbContext.StudSession.Update(grade);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
