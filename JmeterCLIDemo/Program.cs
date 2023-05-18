using JmeterCLIDemo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<GujaratCityDBContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/Get", async (GujaratCityDBContext _dbcontext) =>
{
    return await _dbcontext.Districts.ToListAsync();
});

app.MapGet("/Get/{id}", async (GujaratCityDBContext _dbcontext,int id) =>
{
    return await _dbcontext.Districts.FindAsync(id);
});

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

app.MapDelete("/Delete/{id}", async (GujaratCityDBContext _dbcontext,int id) =>
{
    var dist = await _dbcontext.Districts.FindAsync(id);
    if (dist == null)
        return Results.NotFound("City not found.");
    _dbcontext.Districts.Remove(dist);
    return Results.Ok();

});

app.Run();