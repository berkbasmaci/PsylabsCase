using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PsylabsCase.API.Services;
using PsylabsCase.Core.Common;
using PsylabsCase.Core.Entities;
using PsylabsCase.Core.Options;
using PsylabsCase.Data;
using PsylabsCase.Service.Services;
using System.Text;

namespace PsylabsCase.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddCors(conf => conf.AddPolicy("corsPolicy", build =>
            {
                build.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
            }));

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<LoginUserInfoProvider>();
            builder.Services.AddScoped<AuthService>();
            builder.Services.AddScoped<AdminService>();

            builder.Services.AddScoped<TokenGenerator>();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddDbContext<PsylabsDbContext>(servProvider =>
            {
                servProvider.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            SetupJwtAuthentication(builder);

            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });

            var app = builder.Build();

            SeedData(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("corsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            
            app.Run();
        }

        private static void SetupJwtAuthentication(WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                        ValidAudience = builder.Configuration["JwtOptions:Audience"],
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SigningKey"]))
                    };
                });

            builder.Services.AddAuthorization();
        }

        private static void SeedData(WebApplication app)
        {
            using var serviceProvider = app.Services.CreateScope();
            var context = serviceProvider.ServiceProvider.GetRequiredService<PsylabsDbContext>();
            context.Database.Migrate();

            if (context.Users.Any() == false)
            {
                var admin = new User()
                {
                    Username = "admin",
                    FullName = "Admin User",
                    IsAdmin = true,
                    Password = "123456"
                };
                var john = new User()
                {
                    Username = "johndoe",
                    FullName = "John Doe",
                    IsAdmin = false,
                    Password = "123456"
                };
                var foobar = new User()
                {
                    Username = "foobar",
                    FullName = "Foo Bar",
                    IsAdmin = false,
                    Password = "123456"
                };

                context.Users.Add(admin);
                context.Users.Add(john);
                context.Users.Add(foobar);
                context.SaveChanges();
            }

            if (context.Questions.Any() == false)
            {
                var questions = new List<Question>()
                {
                    new Question()
                    {
                        Text = "Dünya nedir?",
                        Answers = new List<Answer>()
                        {
                            new Answer() { Text = "A planet", IsCorrectAnswer = false },
                            new Answer() { Text = "A ball", IsCorrectAnswer = true },
                            new Answer() { Text = "A star", IsCorrectAnswer = false },
                            new Answer() { Text = "A flat plane", IsCorrectAnswer = false },
                        }
                    },
                    new Question()
                    {
                        Text = "Soru #1",
                        Answers = new List<Answer>()
                        {
                            new Answer() { Text = "Yanlýþ Cevap #1", IsCorrectAnswer = false },
                            new Answer() { Text = "Yanlýþ Cevap #2", IsCorrectAnswer = false },
                            new Answer() { Text = "Doðru Cevap", IsCorrectAnswer = true },
                            new Answer() { Text = "Yanlýþ Cevap #3", IsCorrectAnswer = false },
                        }
                    },
                    new Question()
                    {
                        Text = "Soru #2",
                        Answers = new List<Answer>()
                        {
                            new Answer() { Text = "Doðru Cevap", IsCorrectAnswer = true },
                            new Answer() { Text = "Yanlýþ Cevap #1", IsCorrectAnswer = false },
                            new Answer() { Text = "Yanlýþ Cevap #2", IsCorrectAnswer = false },
                            new Answer() { Text = "Yanlýþ Cevap #3", IsCorrectAnswer = false },
                        }
                    }
                };

                context.Questions.AddRange(questions);
                context.SaveChanges();

                if (context.Exams.Any() == false)
                {
                    context.Exams.Add(new Exam()
                    {
                        Name = "Süper Exam",
                        CreatedAt = DateTime.UtcNow,
                        ExamQuestions = new List<ExamQuestion>()
                        {
                            new ExamQuestion() { QuestionId = questions[0].Id },
                            new ExamQuestion() { QuestionId = questions[1].Id },
                            new ExamQuestion() { QuestionId = questions[2].Id },
                        },
                    });

                    context.SaveChanges();
                }


                if (!context.UserExams.Any())
                {
                    User user = context.Users.First(t => t.Username == "johndoe");
                    Exam exam = context.Exams.First(t => t.Name == "Süper Exam");

                    context.UserExams.Add(new UserExam()
                    {
                        ExamId = exam.Id,
                        UserId = user.Id,
                        IsCompleted = false,
                        Score = null,
                        ExamLink = "/exam/4403959f-a6c0-45fc-9021-d1089a100508"
                    });
                    context.SaveChanges();
                }
            }
        }
    }
}