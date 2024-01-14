using WebApi.Extensions;
using WebApi.Middlewares;
using Infrastructure;
using Application;

/*
Implement a system that works as a synonyms search tool with the following requirements:
The user should be able to add new words with synonyms.
The user should be able to ask for synonyms for a word and lookup should work in both directions. For example, If "wash" is a synonym to "clean", then I should be able to look up both words and get the respective synonym.
A word may have multiple synonyms and all should be returned at a user request.
Make the solution with simple, but fast, data structures in backend's memory - no persistence needed.
Implement the solution in the best possible way, as if it were production code.
Bonus: Deployed so we can test the solution online.
Bonus: Transitive rule implementation, i.e. if "B" is a synonym to "A" and "C" a synonym to "B", then "C" should automatically, by transitive rule, also be the synonym for "A".

Let me know if the problem statement and notes above are clear, otherwise, feel free to ask us anything. 
*/

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()//.WithOrigins(builder.Configuration.GetValue<string>("WebUIBaseUrl")!)
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowAnyOrigin();
        });
});

builder.Services
    .AddControllers(
        options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
    )
    .AddValidationModelOverride();

builder.Services.AddValidators();

builder.Services.AddLogging();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Note: For now I used a base console logger
// An possible extension for this solution would be to imrpove the logging by using a nuget package like serilog (to write the logs to a relational DB or to elastic search or something similar)
app.UseRequestResponseLogging();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();