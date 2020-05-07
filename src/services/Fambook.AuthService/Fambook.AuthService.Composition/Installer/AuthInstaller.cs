﻿using System;
using System.Collections.Generic;
using System.Text;
using Fambook.AuthService.Composition.Installer.Interface;
using Fambook.AuthService.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fambook.AuthService.Composition.Installer
{
    public class AuthInstaller : IInstaller
    {
        private readonly string _secret;

        public AuthInstaller(string secret)
        {
            _secret = secret;
        }

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(_secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }
    }
}
