using TrainingCenter.Api.DTOs.Reports;
using TrainingCenterQueries.DTOs.Reports;
using TrainingCenterQueries.DTOs.Tracks;

namespace TrainingCenter.Api.Services.IServices
{
    public interface IReportService
    {
        Task<DashboardSummaryResponse> GetDashboardSummary();
        Task<IEnumerable<UnpaidEnrollmentResponse>> GetUnpaidEnrollments();
        Task<IEnumerable<TrackCapacityResponse>> GetTrackCapacity();
        Task<RevenueSummaryResponse> GetRevenueSummary();
        Task<IEnumerable<RevenueByTrackResponse>> GetRevenueByTrack();
        Task<IEnumerable<TrackAvailableSeatsResponse>> GetTracksWithAvailSeats();
        Task<IEnumerable<TopTrackResponse>> GetTopTracksAsync(int top = 5);
        Task<IEnumerable<InstructorWorkloadResponse>> GetInstructorWorkload();
        Task<IEnumerable<StudentWithoutPaymentResponse>> GetStudentsWithoutPayments();
    }
}
