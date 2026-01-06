using BankingHexagonal.Domain.Exceptions; // Bizim Domain Hatalarımız
using BankingHexagonal.WebApi.ExceptionModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Presentation.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            // Loglama (Stack trace ile birlikte)
            _logger.LogError(ex, "Bir hata oluştu: {Message}", ex.Message);

            context.Response.ContentType = "application/json";

            HttpStatusCode statusCode;
            string message;
            List<string> errors = null;

            switch (ex)
            {
                // 1. VALIDATION HATALARI (FluentValidation'dan gelebilir)
                case FluentValidation.ValidationException validationEx:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Validasyon hatası.";
                    errors = validationEx.Errors.Select(e => e.ErrorMessage).ToList();
                    break;

                // 2. DOMAIN HATALARI (Bankacılık Kuralları)
                case BaseException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = ex.Message;
                    break;

                // 3. KAYIT BULUNAMADI
                case KeyNotFoundException:
                    // veya bizim oluşturabileceğimiz NotFoundException:
                    // case NotFoundException: 
                    statusCode = HttpStatusCode.NotFound;
                    message = "İstenilen kayıt bulunamadı.";
                    break;

                // 4. BEKLENMEDİK HATALAR (SQL, NullReference vb.)
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = _env.IsDevelopment() ? ex.Message : "Sunucu kaynaklı beklenmedik bir hata oluştu.";
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = new ExceptionResponse
            {
                Success = false,
                Message = message,
                ExceptionType = _env.IsDevelopment() ? ex.GetType().Name : null,
                Errors = errors
            };

            var jsonResult = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResult);
        }
    }
}