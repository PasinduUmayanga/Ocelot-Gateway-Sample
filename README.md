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
02. Select ASP>NET Core Web Aplication Named `Ocelot.Gateway`

![Create Gate way Application](https://user-images.githubusercontent.com/21302583/61872479-0ed24480-af01-11e9-96c1-256cb81fcc17.PNG)

03. Create Another Project Named `Ocelot.CustomerApi` On the soluation explore Right Click on Soluation->Right Click->Add->New Project

![Create api Application](https://user-images.githubusercontent.com/21302583/61872775-cd8e6480-af01-11e9-97dc-bf4d3590529d.PNG)

