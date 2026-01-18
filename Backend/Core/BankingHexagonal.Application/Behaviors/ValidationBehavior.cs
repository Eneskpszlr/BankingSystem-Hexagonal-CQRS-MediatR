using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingHexagonal.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // 1. Bu istek için yazılmış bir validator var mı?
            if (!_validators.Any())
            {
                return await next(); // Yoksa devam et
            }

            // 2. Varsa Context oluştur
            var context = new ValidationContext<TRequest>(request);

            // 3. Tüm validatorları çalıştır
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // 4. Hataları topla
            var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            // 5. Hata varsa "ValidationException" fırlat!
            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            // 6. Sorun yoksa Handler'a geç
            return await next();
        }
    }
}
