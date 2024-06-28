using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuanLyKhoaHocThien_LTS.Application;
using QuanLyKhoaHocThien_LTS.Application.Constants;
using QuanLyKhoaHocThien_LTS.Application.HandleEmail;
using QuanLyKhoaHocThien_LTS.Application.ImplementService;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Mappers;
using QuanLyKhoaHocThien_LTS.Domain.Entities;
using QuanLyKhoaHocThien_LTS.Domain.IRespositories;
using QuanLyKhoaHocThien_LTS.Infrastructure.DataContexts;
using QuanLyKhoaHocThien_LTS.Infrastructure.ImplementRespositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString(Constant.AppSettingKeys.DEAFAULT_CONNECTION)));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IBaseRespository<User>, BaseRespository<User>>();
builder.Services.AddScoped<IUserRespository, UserRepository>();
builder.Services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
builder.Services.AddScoped<IDBContext, ApplicationDbContext>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IBaseRespository<Permission>, BaseRespository<Permission>>();
builder.Services.AddScoped<IBaseRespository<Role>, BaseRespository<Role>>();
builder.Services.AddScoped<IBaseRespository<RefreshToken>, BaseRespository<RefreshToken>>();
builder.Services.AddScoped<IBaseRespository<ConfirmEmail>, BaseRespository<ConfirmEmail>>();
builder.Services.AddScoped<IBaseRespository<Course>, BaseRespository<Course>>();
builder.Services.AddScoped<IBaseRespository<Certificate>, BaseRespository<Certificate>>();
builder.Services.AddScoped<IBaseRespository<CertificateType>, BaseRespository<CertificateType>>();
builder.Services.AddScoped<IBaseRespository<RegisterStudy>, BaseRespository<RegisterStudy>>();
builder.Services.AddScoped<IBaseRespository<Notification>, BaseRespository<Notification>>();
builder.Services.AddScoped<IBaseRespository<Subject>, BaseRespository<Subject>>();
builder.Services.AddScoped<IBaseRespository<CourseSubject>, BaseRespository<CourseSubject>>();
builder.Services.AddScoped<IBaseRespository<SubjectDetail>, BaseRespository<SubjectDetail>>();
builder.Services.AddScoped<IBaseRespository<Practice>, BaseRespository<Practice>>();
builder.Services.AddScoped<IBaseRespository<DoHomework>, BaseRespository<DoHomework>>();
builder.Services.AddScoped<IBaseRespository<Blog>, BaseRespository<Blog>>();
builder.Services.AddScoped<IBaseRespository<CommentBlog>, BaseRespository<CommentBlog>>();
builder.Services.AddScoped<IBaseRespository<Answers>, BaseRespository<Answers>>();

builder.Services.AddScoped<IRegisterStudyService,RegisterStudyService>();
builder.Services.AddScoped<IRegisterStudyRepository, RegisterStudyRepository>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<ICertificateTypeServcie, CertificateTypeService>();
builder.Services.AddScoped<IPracticeServcie, PracticeService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IAnswersService, AnswersService>();






builder.Services.AddHttpClient();


var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<UserConverter>();
builder.Services.AddScoped<CourseConverter>();
builder.Services.AddScoped<PracticeConverter>();
builder.Services.AddScoped<BlogConverter>();




builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Auth Api", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"

    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
