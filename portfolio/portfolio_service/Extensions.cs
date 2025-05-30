﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio_service
{
    public static class Extensions
    {
        public  static void AddGitHubIntegration(this IServiceCollection service, Action<GitHubIntegrationOption> configurOption)
        {
            service.Configure(configurOption);
            service.AddScoped<IGitHubService, GitHubService>();
        }
    }
}
