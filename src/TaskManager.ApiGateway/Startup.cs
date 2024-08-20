﻿using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace TaskManager.ApiGateway {
public class Startup {
  public Startup(IConfiguration configuration) {
    Configuration = configuration;
  }

  public IConfiguration Configuration { get; }

  public void ConfigureServices(IServiceCollection services) {
    services.AddControllers();
    services.AddOcelot(Configuration);
  }

  public async void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env) {
    if (env.IsDevelopment()) {
      app.UseDeveloperExceptionPage();
    }

    app.UseRouting();
    await app.UseOcelot();
  }
}
}