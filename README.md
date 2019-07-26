![0001_banner](https://user-images.githubusercontent.com/21302583/61870371-9026d880-aefb-11e9-8c92-42eb413692de.png)
# Ocelot

[![Build status](https://ci.appveyor.com/api/projects/status/lyhgbhdhqyepj6xi/branch/master?svg=true)](https://ci.appveyor.com/project/Mahadenamuththa/apigatewayocelot/branch/master)

[![Build history](https://buildstats.info/appveyor/chart/Mahadenamuththa/apigatewayocelot)](https://ci.appveyor.com/project/Mahadenamuththa/apigatewayocelot/history)

## First of All, What Is an API Gateway?
An API gateway takes all API calls from clients, then routes them to the appropriate microservice with request routing, composition, and protocol translation. Typically it handles a request by invoking multiple microservices and aggregating the results, to determine the best path. It can translate between web protocols and web‑unfriendly protocols that are used internally.

Basically, it is used for the following purposes:

* Filtering
* Traffic Routing
* Security Mechanism
* Expose only one endpoint

![1_xYYKZ5n4KLZNIFefKo7eYA](https://user-images.githubusercontent.com/21302583/61867015-ea23a000-aef3-11e9-8e6a-e06afce3e63f.png)

## API Gateway Pattern
When building a large and complex system using the microservices architecture with multiple client apps, a good approach to consider is API Gateway Pattern.

This pattern provides a single entry-point for group(s) of microservices. A variation of the API Gateway pattern is also known as the “backend for frontend” (BFF) because you might create multiple API Gateways depending on the different needs from each client app.

Therefore, the API Gateway sits between the client apps and the microservices. It acts as a reverse proxy, routing requests from clients to services.

It can also provide additional cross-cutting features such as authentication, SSL termination, and cache.

## Now, let's talk our main subject, Ocelot:
Ocelot is an API Gateway for .NET platform. This project is intended for people using .NET/Core running a micro service/service oriented architecture that needs a unified entry point on their system. However, it will work with anything that uses HTTP and runs on any platform supported by ASP.NET Core.

### Ocelot Features:
* Routing
* Request Aggregation
* Service Discovery with Consul & Eureka
* Service Fabric
* WebSockets
* Authentication
* Authorization
* Rate Limiting
* Caching
* Retry policies/QoS
* Load Balancing
* Logging/Tracing/Correlation
* Headers/Query String/Claims Transformation
* Custom Middleware/Delegating Handlers
* Configuration/Administration REST API
* Platform/Cloud Agnostic

![0002_existing app](https://user-images.githubusercontent.com/21302583/61870106-e6474c00-aefa-11e9-8e65-9eb40ab924a9.png)

## How to install

Install Ocelot and it's dependencies using NuGet. 

`Install-Package Ocelot`

Or via the .NET Core CLI:

`dotnet add package ocelot`

All versions can be found [here](https://www.nuget.org/packages/Ocelot/)

## Create Project

01. First Create Project File->New->Project
02. Select ASP.NET Core Web Aplication Named `Ocelot.Gateway`

![Create Gate way Application](https://user-images.githubusercontent.com/21302583/61872479-0ed24480-af01-11e9-96c1-256cb81fcc17.PNG)

03. Select Empty 

![Create Application2](https://user-images.githubusercontent.com/21302583/61873196-fbc07400-af02-11e9-8624-790d870a7ebb.PNG)


04. Create Another Project Named `Ocelot.CustomerApi` On the soluation explore Right Click on Soluation->Right Click->Add->New Project

![Create api Application](https://user-images.githubusercontent.com/21302583/61872775-cd8e6480-af01-11e9-97dc-bf4d3590529d.PNG)

05. Select Api

![Create api Application2](https://user-images.githubusercontent.com/21302583/61873338-5d80de00-af03-11e9-908f-4e0029b14deb.PNG)

06. Go to Tools->Nuget Package Manager->Manage Nuget Packages For Soluation and Install Ocelot

![Install Nuget Ocelot](https://user-images.githubusercontent.com/21302583/61873448-a46ed380-af03-11e9-86c2-453b05dba384.PNG)

07. Create JSON File Named `Ocelot.json` inside `Ocelot.Gateway` and Add Below Code

```json
{
  "GlobalConfiguration": {
    "BaseUrl": "http://localhost:34775"
  },
  "ReRoutes": [
    {
      "UpstreamPathTemplate": "/CustomerService/{catchAll}",
      "DownstreamPathTemplate": "/api/{catchAll}",
      "DownstreamScheme": "http",
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost",
          "Port": 35045
        }
      ],
      "LoadBalancerOptions": {
        "Type": "RoundRobin"
      }
    }
  ]
}
```
>Here Port you can find from Ocelot.CustomerApi->Right Click->Properties
![port](https://user-images.githubusercontent.com/21302583/61945564-eeb48b00-afbd-11e9-8a31-abd3c6d2578d.PNG)

08. Change `Program.cs` in Ocelot.Gateway As below

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
        {
            config
                .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
                .AddJsonFile("Ocelot.json", true, true)
                .AddEnvironmentVariables();
        })
            .UseStartup<Startup>();
}
```

09. Change `Startup.cs` in Ocelot.Gateway As below

```csharp
public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;
            options.MinimumSameSitePolicy = SameSiteMode.None;
        });
        services.AddOcelot();
        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }
        //app.Run(async (context) =>
        //{
        //    await context.Response.WriteAsync("Hello, World!");
        //});
        app.UseStaticFiles();
        app.UseCookiePolicy();
        await app.UseOcelot();
        //app.UseMvc();
    }
}

```

10. Right Click On the Soluation->Set StartUp Projects

![multiple select](https://user-images.githubusercontent.com/21302583/61947196-3a693380-afc2-11e9-8c2c-c8b9e6f5892a.PNG)

11. Now Start Project 

This is Gateway URL [http://localhost:34775/](http://localhost:34775/ "Gate Way URL") Change this to [http://localhost:34775/CustomerService/values](http://localhost:34775/CustomerService/values "Gate Way URL")->Enter

Here `CustomerService` is your `Ocelot.CustomerApi` it is actually [http://localhost:35045/api/values](http://localhost:35045/api/values "Gate Way URL") ,But you are using `http://localhost:35045` as `CustomerService`
