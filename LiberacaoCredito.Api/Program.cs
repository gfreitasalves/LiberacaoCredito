using LiberacaoCredito.Application.Abstractions.Commands;
using LiberacaoCredito.Application.Abstractions.Queries;
using LiberacaoCredito.Application.InputPorts;
using LiberacaoCredito.Application.UseCases;
using LiberacaoCredito.Application.UseCases.Interfaces;
using LiberacaoCredito.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LiberacaoCreditoDbContext>(opt => opt.UseInMemoryDatabase("LiberacaoCredito"));


//Add LiberacaoCredito services
builder.Services.AddScoped<IValidarLiberacaoCreditoUseCase, ValidarLiberacaoCreditoUseCase>();
builder.Services.AddScoped<ISelecionarLinhasDeCreditoUseCase, SelecionarLinhasDeCreditoUseCase>();
builder.Services.AddScoped<ISolicitacaoLiberacaoCreditoRepository, SolicitacaoLiberacaoCreditoRepository>();
builder.Services.AddScoped<IObterLinhaCreditoQuery, ObterLinhaCreditoQuery>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/linhaCredito", async (IObterLinhaCreditoQuery obterLinhaCreditoQuery) =>
{
    var list = await obterLinhaCreditoQuery.SelecionarTodosAsync();

    return list;
})
.WithName("linhaCredito")
.WithOpenApi();

app.MapPost("/LiberacaoCredito", async (IValidarLiberacaoCreditoUseCase validarLiberacaoCreditoUseCase, SolicitacaoLiberacaoCreditoInput solicitacaoLiberacaoCreditoInput) =>
{
    var result = await validarLiberacaoCreditoUseCase.ValidarAsync(solicitacaoLiberacaoCreditoInput);

    return result;
})
.WithName("LiberacaoCredito")
.WithOpenApi();

app.Run();