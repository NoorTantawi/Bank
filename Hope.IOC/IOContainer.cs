using Hope.Repositories.IRepository;
using Hope.Repositories.Repository;
using Hope.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;  
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Hope.DomainEntities.DBEntities;

namespace Hope.IOC
{
    public static class IOContainer
    {

        public static void ConfigureIOC (this IServiceCollection services) 
        {

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<INationalityRepository, NationalityRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IQualificationRepository, QualificationRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IAccountTypeRepository,  AccountTypeRepository>();

        }
    }
}
