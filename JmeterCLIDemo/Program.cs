using JmeterCLIDemo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<GujaratCityDBContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var DBConf = builder.Configuration.GetSection("DBConfiguration");
builder.Services.AddDbContext<GujaratCityDBContext>(options => options.UseSqlServer(
    $"Server={DBConf["DBServer"]},{DBConf["port"]};Initial Catalog={DBConf["Database"]};User ID = {DBConf["DBUser"]};Password= {DBConf["DBPassword"]};TrustServerCertificate=true;"));

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
        ValidateIssuer = true,
        ValidateAudience= true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});
builder.Services.AddAuthorization();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/GetToken", (string password) =>
{
    if(password == "Amit@123")
    {
        var issuer = config["JwtSettings:Issuer"];
        var audience = config["JwtSettings:Audience"];
        var key = Encoding.UTF8.GetBytes(config["JwtSettings:Key"]);
        var tokenDiscriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim("Id", Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, "amit"),
            new Claim(JwtRegisteredClaimNames.Email, "amit.limbasiya@marutitech.com"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
            Expires = DateTime.UtcNow.AddHours(100),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDiscriptor);
        var jwttoken = tokenHandler.WriteToken(token);
        var stringtoken = jwttoken.ToString();
        return Results.Ok(stringtoken);
    }
    return Results.Unauthorized();
});

app.MapGet("/Get", async (GujaratCityDBContext _dbcontext) =>
{
    return await _dbcontext.Districts.ToListAsync();
}).RequireAuthorization();

app.MapGet("/Get/{id}", async (GujaratCityDBContext _dbcontext,int id) =>
{
    
    return await _dbcontext.Districts.FindAsync(id);
}).RequireAuthorization();

app.MapPost("/Post", async([FromBody]District district,GujaratCityDBContext _dbcontext) =>
{
    await _dbcontext.Districts.AddAsync(district);
    await _dbcontext.SaveChangesAsync();
});

app.MapPut("/Put/{id}", async ([FromBody] District district,int id, GujaratCityDBContext _dbcontext) =>
{ 
    var dist = await _dbcontext.Districts.FindAsync(id);
    if(dist==null) return null;

    dist.STCode= district.STCode;
    dist.StateName = district.StateName;
    dist.DTCode= district.DTCode;
    dist.DistrictName = district.DistrictName;
    dist.SDTCode = district.SDTCode;
    dist.SubDistrictName = district.SubDistrictName;
    dist.TownCode= district.TownCode;
    dist.AreaName = district.AreaName;

    await _dbcontext.SaveChangesAsync();
    return dist;
});
app.MapPut("/UpdateByTownCode/{townCode}", async ([FromBody] District district, int townCode, GujaratCityDBContext _dbcontext) =>
{
    Console.WriteLine("Hello put");
    var dist = await _dbcontext.Districts.Where(record => record.TownCode == townCode).FirstOrDefaultAsync();
    if(dist==null) return Results.NotFound();
    dist.STCode = district.STCode;
    dist.StateName = district.StateName;
    dist.DTCode = district.DTCode;
    dist.DistrictName = district.DistrictName;
    dist.SDTCode = district.SDTCode;
    dist.SubDistrictName = district.SubDistrictName;
    dist.TownCode = district.TownCode;
    dist.AreaName = district.AreaName;

    await _dbcontext.SaveChangesAsync();
    
    return Results.Ok();
});

app.MapDelete("/Delete/{id}", async (GujaratCityDBContext _dbcontext,int id) =>
{
    var dist = await _dbcontext.Districts.FindAsync(id);
    if (dist == null)
        return Results.NotFound("City not found.");
    _dbcontext.Districts.Remove(dist);
    return Results.Ok();

});

app.MapDelete("/DeleteByTownCode/{townCode}", async (GujaratCityDBContext _dbcontext, int townCode) =>
{
    Console.WriteLine("Hello delete");
    var dist = await _dbcontext.Districts.Where(record => record.TownCode == townCode).FirstOrDefaultAsync();
    if (dist == null)
        return Results.NotFound();
    _dbcontext.Districts.Remove(dist);
    await _dbcontext.SaveChangesAsync();
    return Results.Ok();
});


app.Run();