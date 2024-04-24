using BlankSolution.Data;
using BlankSolution.Business;
using BlankSolution.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using BlankSolution.Business.DTOs.MovieDTOs;
using FluentValidation;
using BlankSolution.Business.MappingProfiles;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddValidatorsFromAssemblyContaining<MovieCreateDtoValidator>();
//builder.Services.AddFluentValidationAutoValidation();
//builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddControllers().AddFluentValidation(opt =>
{
	opt.RegisterValidatorsFromAssembly(typeof(MovieCreateDtoValidator).Assembly);
}).AddNewtonsoftJson(options =>
	options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
); 

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddRepositories();
builder.Services.AddServices();

builder.Services.AddDbContext<AppDbContext>(opt =>
{
	opt.UseSqlServer(builder.Configuration.GetConnectionString("default"), b => b.MigrationsAssembly("BlankSolution.API"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
