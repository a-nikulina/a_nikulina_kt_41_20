using Microsoft.AspNetCore.Mvc;
using proekt.Database;
using proekt.Models;
using System.Diagnostics;
using proekt.Interfaces.StudSessionInterface;
using static proekt.Interfaces.StudSessionInterface.IStudSessionInterface;

namespace proekt.Controllers
{
        [ApiController]
        [Route("[controller]")]
        public class StudSessionController : Controller
        {

            private readonly ILogger<StudSessionController> _logger;
            private readonly IStudSessionService _studsessionService;
            private StudSessionDbContext _dbContext;


            public StudSessionController(ILogger<StudSessionController> logger, StudSessionDbContext dbContext, IStudSessionService gradeService)
            {
                _logger = logger;
                _dbContext = dbContext;
                _studsessionService = gradeService;
            }

            [HttpGet("Get grades")]
            public async Task<IActionResult> GetGradesAsync(CancellationToken cancellationToken = default)
            {
                var grades = await _studsessionService.GetGradesAsync(cancellationToken);
                return Ok(grades);
            }

            [HttpPost("Add grades")]
            [ActionName(nameof(AddGradeAsync))]
            public async Task<IActionResult> AddGradeAsync(StudSession grade, CancellationToken cancellationToken = default)
            {
                await _studsessionService.AddGradeAsync(grade, cancellationToken);
                return CreatedAtAction(nameof(AddGradeAsync), new { id = grade.GradeId }, grade);
            }

            [HttpPut("Update grades with id")]
            public async Task<IActionResult> UpdateGradeAsync(int id, StudSession grade, CancellationToken cancellationToken = default)
            {
                if (id != grade.GradeId)
                {
                    return BadRequest();
                }
                await _studsessionService.UpdateGradeAsync(grade, cancellationToken);
                return Ok();
            }
        }
}
