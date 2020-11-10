using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using projetohotelaria.Businnes;
using projetohotelaria.Infraestrutura.Interface;
using projetohotelaria.Infraestrutura.Repositorio;

namespace ProjetoHotelaria
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Contexto>(opt => opt.UseMySql(Configuration.GetConnectionString("MySqlConnectionString")));

            services.AddScoped<ProdutoServices>();
            services.AddScoped<InterfaceProduto, RepositoryProduto>();
            services.AddScoped<InterfaceHospede, RepositoryHospede>();
            services.AddScoped<InterfaceReserva, RepositoryReserva>();
            services.AddScoped<InterfaceAcomodacao, RepositoryAcomodacao>();
            services.AddScoped<InterfaceConsumo, RepositoryConsumo>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
