using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Reports;
using TrainingCenter.Api.Entities.Enums;
using TrainingCenter.Api.Services.IServices;
using TrainingCenterQueries.DTOs.Reports;
using TrainingCenterQueries.DTOs.Tracks;

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
            var studentsCount = await context.Students.CountAsync(s=>!s.IsDeleted);
            var tracksCount = await context.TrainingTracks.CountAsync(t => !t.IsDeleted);
            var activeEnrollments = await context.Enrollments.CountAsync(e => e.Status == EnrollmentStatus.Active);
            var revenue = await context.Payments.Where(p => p.PaymentStatus == PaymentStatus.Paid).SumAsync(p => (decimal?)p.Amount) ?? 0;
            int unpaidCount;
            unpaidCount = await context.Enrollments.CountAsync(e=>e.Payments.Sum(p=>p.Amount)< e.TrainingTrack.Capacity);
            return new DashboardSummaryResponse
            {
                StudentsCount = studentsCount,
                TracksCount = tracksCount,
                ActiveEnrollments = activeEnrollments,
                Revenue = revenue,
                UnpaidCount = unpaidCount
            };
        }
        //Query 12 Unpaid Enrollments
        public async Task<IEnumerable<UnpaidEnrollmentResponse>> GetUnpaidEnrollments()
        {
            return await context.Enrollments
            .Select(e => new UnpaidEnrollmentResponse
            {
                EnrollmentId = e.EnrollmentId,
                StudentName = e.Student.FullName,
                TrackTitle = e.TrainingTrack.Title,
                TotalRequired = e.TrainingTrack.Capacity,
                TotalPaid = e.Payments.Sum(p => p.Amount),
                RemainingAmount = e.TrainingTrack.Capacity - e.Payments.Sum(p => p.Amount),
                PaymentStatus =
                    e.TrainingTrack.Capacity - e.Payments.Sum(p => p.Amount) == 0
                        ? "Paid"
                        : "Unpaid"
            }).Where(e => e.RemainingAmount > 0).OrderByDescending(e => e.RemainingAmount).ToListAsync();
        }
        public async Task<IEnumerable<TrackCapacityResponse>> GetTrackCapacity()
        {
            return await context.TrainingTracks.Where(t => !t.IsDeleted).Select(t => new TrackCapacityResponse
                {
                    TrainingTrackId = t.TrainingTrackId,
                    TrackTitle = t.Title,
                    Capacity = t.Capacity,
                    EnrolledStudents = t.Enrollments.Count,
                    AvailableSeats = t.Capacity - t.Enrollments.Count
                }).ToListAsync();
        }
        //Query 14 Revenue Summary
        public async Task<RevenueSummaryResponse> GetRevenueSummary()
        {
            var payments = context.Payments.AsQueryable();
            var totalRevenue = await payments.SumAsync(p => (decimal?)p.Amount) ?? 0;
            var totalPayments = await payments.CountAsync();
            var paidCount = await payments
                .CountAsync(p => p.PaymentStatus == PaymentStatus.Paid);

            var pendingCount = await payments
                .CountAsync(p => p.PaymentStatus == PaymentStatus.Pending);

            var failedCount = await payments
                .CountAsync(p => p.PaymentStatus == PaymentStatus.Failed);

            return new RevenueSummaryResponse
            {
                TotalRevenue = totalRevenue,
                TotalPayments = totalPayments,
                PaidCount = paidCount,
                PendingCount = pendingCount,
                FailedCount = failedCount
            };
            
        }
        //Query 15 Revenue Per Track
        public async Task<IEnumerable<RevenueByTrackResponse>> GetRevenueByTrack()
        {
            return await context.TrainingTracks.Select(t => new RevenueByTrackResponse
                {
                    TrainingTrackId = t.TrainingTrackId,
                    TrackTitle = t.Title,
                    Revenue = t.Enrollments.SelectMany(e => e.Payments).Where(p => p.PaymentStatus == PaymentStatus.Paid).Sum(p => (decimal?)p.Amount) ?? 0,
                    PaymentCount = t.Enrollments.SelectMany(e => e.Payments).Count(p => p.PaymentStatus == PaymentStatus.Paid),
                    EnrollmentCount = t.Enrollments.Count()
            }).OrderByDescending(x => x.Revenue).ToListAsync();
        }
        //Query 7 Track With Available Seats
        public async Task<IEnumerable<TrackAvailableSeatsResponse>> GetTracksWithAvailSeats()
        {
            var tracks = await context.TrainingTracks.Where(t => !t.IsDeleted).Select(t => new TrackAvailableSeatsResponse
            {
                TrainingTrackId = t.TrainingTrackId,
                Title = t.Title,
                Capacity = t.Capacity,
                ActiveEnrollment = t.Enrollments.Count(e => e.Status == EnrollmentStatus.Active),
                RemainingSeats = t.Capacity - t.Enrollments.Count(e => e.Status == EnrollmentStatus.Active)
            }).Where(t => t.RemainingSeats > 0).OrderBy(t => t.RemainingSeats).ToListAsync();
            return tracks;
        }
        //Query16 Top Track Report
        public async Task<IEnumerable<TopTrackResponse>> GetTopTracksAsync(int top = 5)
        {
            if (top <= 0)
            {
                top = 5;
            }

            return await context.TrainingTracks.Where(t => !t.IsDeleted).Select(t => new TopTrackResponse
                {
                    TrainingTrackId = t.TrainingTrackId,
                    TrackTitle = t.Title,
                    Capacity = t.Capacity,
                    ActiveEnrollmentCount = t.Enrollments.Count(e => e.Status == EnrollmentStatus.Active),
                    RemainingSeats = t.Capacity - t.Enrollments.Count(e => e.Status == EnrollmentStatus.Active)
                }).OrderByDescending(t => t.ActiveEnrollmentCount).ThenBy(t => t.TrackTitle).Take(top).ToListAsync();
        }
        //Query 17 Instructor Workload
        public async Task<IEnumerable<InstructorWorkloadResponse>> GetInstructorWorkload()
        {
            return await context.Instructors.Where(i => i.IsActive).Select(i => new InstructorWorkloadResponse
            {
                InstructorId = i.InstructorId,
                InstructorName = i.FullName,
                TrackCount = i.TrainingTracks.Count(),
                ActiveStudentCount = i.TrainingTracks.SelectMany(t => t.Enrollments).Count(e => e.Status == EnrollmentStatus.Active)
            }).OrderByDescending(i => i.ActiveStudentCount).ThenBy(i => i.InstructorName).ToListAsync();
        }
        //Query 18 Students Without Payments
        public async Task<IEnumerable<StudentWithoutPaymentResponse>> GetStudentsWithoutPayments()
        {
            return await context.Enrollments.Where(e =>
                    (e.Status == EnrollmentStatus.Active || e.Status == EnrollmentStatus.Pending) && !e.Payments.Any())
                .Select(e => new StudentWithoutPaymentResponse
                {
                    StudentId = e.StudentId,

                    FullName = e.Student.FullName,

                    Email = e.Student.Email,

                    EnrollmentId = e.EnrollmentId,

                    TrackTitle = e.TrainingTrack.Title,

                    EnrollmentStatus = e.Status,

                    EnrollmentDate = e.EnrollmentDate
                })
                .OrderBy(e => e.FullName)
                .ToListAsync();
        }
    }
}
