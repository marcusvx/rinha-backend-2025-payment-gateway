
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PaymentMediator.Controllers;
using PaymentMediator.Models;
using PaymentMediator.Services;
using Xunit;

namespace PaymentMediator.Tests
{
    public class PaymentsControllerTests
    {
        private readonly Mock<IPaymentService> _mockPaymentService;
        private readonly PaymentsController _controller;

        public PaymentsControllerTests()
        {
            _mockPaymentService = new Mock<IPaymentService>();
            _controller = new PaymentsController(_mockPaymentService.Object);
        }

        [Fact]
        public async Task ProcessPayment_ReturnsAccepted()
        {
            // Arrange
            var paymentRequest = new PaymentRequest { CorrelationId = Guid.NewGuid(), Amount = 100 };
            _mockPaymentService.Setup(s => s.ProcessAsync(paymentRequest)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.ProcessPayment(paymentRequest);

            // Assert
            Assert.IsType<AcceptedResult>(result);
        }

        [Fact]
        public async Task GetSummary_ReturnsOkWithSummary()
        {
            // Arrange
            var from = new DateTime(2025, 1, 1);
            var to = new DateTime(2025, 1, 31);
            var paymentSummary = new PaymentSummary
            {
                Default = new SummaryDetails { TotalAmount = 1000, TotalRequests = 10 },
                Fallback = new SummaryDetails { TotalAmount = 500, TotalRequests = 5 }
            };
            _mockPaymentService.Setup(s => s.GetSummary(from, to)).ReturnsAsync(paymentSummary);

            // Act
            var result = await _controller.GetSummary(from, to);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var summary = Assert.IsType<PaymentSummary>(okResult.Value);
            Assert.Equal(paymentSummary.Default.TotalAmount, summary.Default.TotalAmount);
            Assert.Equal(paymentSummary.Default.TotalRequests, summary.Default.TotalRequests);
            Assert.Equal(paymentSummary.Fallback.TotalAmount, summary.Fallback.TotalAmount);
            Assert.Equal(paymentSummary.Fallback.TotalRequests, summary.Fallback.TotalRequests);
        }
    }
}
