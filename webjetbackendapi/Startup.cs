using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using webjetbackendapi.Gateway;
using webjetbackendapi.Mappers;
using webjetbackendapi.Services;
using webjetbackendapi.Services.Interfaces;

namespace webjetbackendapi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string _baseUrl;
        private readonly string _token;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _baseUrl = Configuration.GetSection("BaseUrl").Value;
            _token = Environment.GetEnvironmentVariable("TOKEN");
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddScoped<IFilmWorldService, FilmWorldService>();
            services.AddScoped<ICinemaWorldService, CinemaWorldService>();
            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMemoryCache, MemoryCache>();
            services.AddSingleton(Configuration);
            //Enable Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MovieCombinedMovieMapper());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHttpClient<IMovieServiceGateway, MovieServiceGateway>("MovieDatabaseService", client =>
            {
                client.DefaultRequestHeaders.Add("x-access-token", _token);
                client.BaseAddress = new Uri(_baseUrl);
                client.Timeout = TimeSpan.FromSeconds(10);
            });
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "WebJetMoviesBackendAPI");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
