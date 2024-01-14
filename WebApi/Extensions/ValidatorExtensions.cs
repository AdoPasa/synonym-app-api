using Application.Common.Models;
using Application.Synonyms.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Validators;

namespace WebApi.Extensions
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddScoped<IValidator<AddSynonymRequest>, AddSynonymRequestValidator>();

            return services;
        }

        public static IMvcBuilder AddValidationModelOverride(this IMvcBuilder builder)
        {
            return builder.ConfigureApiBehaviorOptions(options => {
                options.InvalidModelStateResponseFactory = context =>
                {
#pragma warning disable CA1416
                    var errors = context.ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
#pragma warning restore CA1026
                    return new BadRequestObjectResult(new Response<string>(errors));
                };
            });
        }
    }
}
