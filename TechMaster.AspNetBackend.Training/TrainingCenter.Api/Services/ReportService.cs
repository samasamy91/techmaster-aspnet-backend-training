using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Reports;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;

namespace TrainingCenter.Api.Services
{
    public class ReportService : IReportService
    {
        private readonly AppDbContext context;
        public ReportService(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<DashboardSummaryResponse> GetDashboardSummary()
        {
            return new DashboardSummaryResponse
            {
                TotalStudents = await context.Students.CountAsync(s => !s.IsDeleted),
                TotalInstructor = await context.Instructors.CountAsync(),
                TotalTrack = await context.TrainingTracks.CountAsync(t => !t.IsDeleted),
                ActiveEnrollments = await context.Enrollments.CountAsync(e => e.Status == EnrollmentStatus.Active),
                TotalRevenue = await context.Payments.Where(p => p.PaymentStatus == PaymentStatus.Paid).SumAsync(p => (decimal?)p.Amount) ?? 0
            };
        }
        public async Task<IEnumerable<UnpaidEnrollmentResponse>> GetUnpaidEnrollments()
        {
            return await context.Enrollments.Include(e => e.Student).Include(e => e.TrainingTrack).Include(e => e.Payments)
                .Select(e => new UnpaidEnrollmentResponse
                {
                    EnrollmentId = e.EnrollmentId,
                    StudentName = e.Student.FullName,
                    TrackTitle = e.TrainingTrack.Title,
                    TotalPaid = e.Payments.Where(p => p.PaymentStatus == PaymentStatus.Paid).Sum(p => p.Amount),
                    RemainingAmount = Math.Max(0, e.TrainingTrack.Fee - e.Payments.Where(p => p.PaymentStatus == PaymentStatus.Paid).Sum(p => p.Amount)) 
                }).Where(x => x.RemainingAmount > 0).ToListAsync();
        }
        public async Task<IEnumerable<TrackCapacityResponse>> GetTrackCapacity()
        {
            return await context.TrainingTracks.Where(t => !t.IsDeleted).Select(t => new TrackCapacityResponse
                {
                    TrainingTrackId = t.TrainingTrackId,
                    TrackTitle = t.Title,
                    Capacity = t.Capacity,
                    EnrolledStudents = t.Enrollments.Count(e=>e.Status == EnrollmentStatus.Active),
                    AvailableSeats = t.Capacity - t.Enrollments.Count(e=>e.Status == EnrollmentStatus.Active)
                }).ToListAsync();
        }
        public async Task<RevenueSummaryResponse> GetRevenueSummary()
        {
            var payments = context.Payments.Where(p => p.PaymentStatus == PaymentStatus.Paid);
            var totalRevenue = await payments.SumAsync(p => (decimal?)p.Amount) ?? 0;
            var totalPayments = await payments.CountAsync();
            return new RevenueSummaryResponse
            {
                TotalRevenue = totalRevenue,
                TotalPayments = totalPayments,
                AveragePayment = totalPayments == 0 ? 0 : totalRevenue / totalPayments
            };
        }
        public async Task<IEnumerable<RevenueByTrackResponse>> GetRevenueByTrack()
        {
            return await context.TrainingTracks.Where(t => !t.IsDeleted).Select(t => new RevenueByTrackResponse
                {
                    TrackTitle = t.Title,
                    Revenue = t.Enrollments.SelectMany(e => e.Payments).Where(p => p.PaymentStatus == PaymentStatus.Paid).Sum(p => p.Amount),
                    PaymentCount = t.Enrollments.SelectMany(e => e.Payments).Count(p => p.PaymentStatus == PaymentStatus.Paid)
                }).OrderByDescending(x => x.Revenue).ToListAsync();
        }
    }
}
