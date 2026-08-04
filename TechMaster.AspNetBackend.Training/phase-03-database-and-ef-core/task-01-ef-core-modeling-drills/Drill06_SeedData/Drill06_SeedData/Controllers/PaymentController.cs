using Drill02_OneToOneStudentProfile.Data;
using Drill05_PaymentSummary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Drill05_PaymentSummary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly AppDbContext context;
        public PaymentController(AppDbContext context)
        {
            this.context = context;
        }
        [HttpPost]
        public IActionResult CreatePaymentSummary()
        {
            var payment = new PaymentSummary
            {
                EnrollmentId = 1,
                TotalRequired = 5000m,
                TotalPaid = 2500m,
                PaymentStatus = PaymentStatus.PartiallyPaid
            };
            context.PaymentSummaries.Add(payment);
            context.SaveChanges();
            return Ok(payment);
        }
    }
}
