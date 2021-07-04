using GRR.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GRR.Data
{
  public static  class ServiceRegistration
    {

        public static void RegisterDbServices(this IServiceCollection services)
        {
            services.AddScoped<UserInfo>();
            services.AddScoped<IUow, Uow>();
        }
    }
}
