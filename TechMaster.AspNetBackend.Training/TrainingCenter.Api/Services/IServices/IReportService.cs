using TrainingCenter.Api.DTOs.Reports;

namespace TrainingCenter.Api.Services.IServices
{
    public interface IReportService
    {
        Task<DashboardSummaryResponse> GetDashboardSummary();
        Task<IEnumerable<UnpaidEnrollmentResponse>> GetUnpaidEnrollments();
        Task<IEnumerable<TrackCapacityResponse>> GetTrackCapacity();
        Task<RevenueSummaryResponse> GetRevenueSummary();
        Task<IEnumerable<RevenueByTrackResponse>> GetRevenueByTrack();
    }
}
