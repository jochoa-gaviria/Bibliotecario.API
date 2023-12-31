﻿using Bibliotecario.Application.Contracts.Interfaces;
using Bibliotecario.Application.Services;
using Bibliotecario.DataAccess.Contracts.Interfaces;
using Bibliotecario.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Bibliotecario.CrossCutting.Register;

public static class Register
{
    #region public methods
    public static IServiceCollection AddRegistration(this IServiceCollection services)
    {
        AddServices(services);
        AddRepositories(services);
        AddOther(services);
        return services;
    }
    #endregion public methods

    #region private methods

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<ILoanService, LoanService>();
        services.AddTransient<IUserService, UserService>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        return services;
    }

    private static void AddOther(this IServiceCollection services)
    {
        JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };
    }
    #endregion private methods
}