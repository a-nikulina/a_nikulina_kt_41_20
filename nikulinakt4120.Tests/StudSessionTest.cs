namespace nikulinakt4120.Tests
{
    public class StudSessionTest
    {
        [Fact]
        public void IsValidGradeRange()
        {
            var gradeNaumber = 5;

            // Act
            var result = IsInRange(gradeNaumber);

            // Assert
            Assert.True(result);
        }

        public bool IsInRange(int number)
        {
            if (number >= 2 && number <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
