namespace RefactoredEnrollment.DTOs
{
    public class EnrollmentResponseDto
    {
        public int EnrollmentId { get; set; }

        public string StudentName { get; set; } 

        public string TrackTitle { get; set; } 

        public DateTime EnrollmentDate { get; set; }

        public string Status { get; set; } 

        public decimal ProgressPercentage { get; set; }

        public decimal? FinalResult { get; set; }
    }
}
