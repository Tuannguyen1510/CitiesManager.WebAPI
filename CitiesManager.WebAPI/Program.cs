using CitiesManager.WebAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// add DbContext
// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);
// Add services to the container.
builder.Services.AddControllers(options =>
{
    // Thêm một filter vào tùy chọn của controllers. Trong trường hợp này, đây là ProducesAttribute với giá trị là "application/json". Điều này chỉ định rằng controllers sẽ sinh ra các phản hồi có kiểu MIME là "application/json".
    options.Filters.Add(new ProducesAttribute("application/json"));
    // thêm một filter ConsumesAttribute vào tùy chọn của controllers. Filter này chỉ định rằng controllers sẽ chỉ chấp nhận các yêu cầu có kiểu MIME là "application/json".
    options.Filters.Add(new ConsumesAttribute("application/json"));

}).AddXmlSerializerFormatters(); // Thêm định dạng XML cho việc định dạng dữ liệu. Nếu yêu cầu có Accept: application/xml, controllers sẽ có khả năng sinh ra phản hồi dưới dạng XML. Điều này mở rộng khả năng của ứng dụng để xử lý cả JSON và XML.

//builder.Services.AddApiVersioning(config =>
//{
//    // add version api
//    config.DefaultApiVersion = new ApiVersion(1, 0);
//    config.AssumeDefaultVersionWhenUnspecified = true;
//    config.ReportApiVersions = true;

//    // 3 cách đọc api version
//    // config.ApiVersionReader = new UrlSegmentApiVersionReader();
//    //config.ApiVersionReader = new QueryStringApiVersionReader();
//    //config.ApiVersionReader = new HeaderApiVersionReader("api-version");

//    config.ApiVersionReader = ApiVersionReader.Combine(
//        new QueryStringApiVersionReader("mbs-api-version"),
//        new HeaderApiVersionReader("mbs-version"),
//        new MediaTypeApiVersionReader("mbs-media-version")
//        ); ;
//}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));   // add services comments 
});



// add cors kết nối api 


//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder =>
//    {
//        builder.WithOrigins("https://localhost:7141");
//    });
//});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularOrigins",
    builder =>
    {
        //builder.WithOrigins("https://localhost:7141", "https://localhost:7162")
        builder.WithOrigins("*")
        .WithHeaders("Authorization", "origin", "accept", "content-type")
        .WithMethods("GET", "POST", "PUT", "DELETE");
    });
   options.AddPolicy("4100Client",
   builder =>
   {
       //builder.WithOrigins("https://localhost:7141", "https://localhost:7162")
       builder.WithOrigins("*")
       .WithHeaders("Authorization", "origin", "accept")
       .WithMethods("GET");
       //.AllowAnyMethod()
       //.AllowAnyOrigin();
   });
});
//

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();
// Api 
app.UseRouting();
app.UseCors("AllowAngularOrigins");
// 

app.UseAuthorization();

//file
app.UseStaticFiles(new StaticFileOptions
{
	FileProvider = new PhysicalFileProvider(
Path.Combine(Directory.GetCurrentDirectory(), "Photos")),
	RequestPath = "/Photos"
});

app.MapControllers();

app.Run();
