using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Net.WebSockets;

namespace ApiTFG
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseWebSockets();
            /*app.Use(async (http, next) =>
            {
                ChatRequestManager requestsManager = new ChatRequestManager();

                if (http.WebSockets.IsWebSocketRequest)
                {
                    var websocket = await http.WebSockets.AcceptWebSocketAsync();
                    while(websocket.State == System.Net.WebSockets.WebSocketState.Open)
                    {
                        var token = System.Threading.CancellationToken.None;
                        //Recibe el usuario
                        var buffer = new ArraySegment<byte>(new Byte[4096]);
                        var received = await websocket.ReceiveAsync(buffer, token);

                        if(received.MessageType == WebSocketMessageType.Text)
                        {
                            string request = Encoding.UTF8.GetString(buffer.Array,
                                buffer.Offset,
                                buffer.Count);
                            Users user = requestsManager.FindUserById(Int32.Parse(request));
                            requestsManager.userSocket.Add(user, websocket);

                            //Recibe peticion de chat
                            buffer = new ArraySegment<byte>(new byte[4096]);
                            received = await websocket.ReceiveAsync(buffer, token);

                            if(received.MessageType == WebSocketMessageType.Text)
                            {
                                request = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
                                string userToSend = request.Split('-')[0];
                                string userSender = request.Split('-')[1];

                                var data = Encoding.UTF8.GetBytes(userSender);
                                buffer = new ArraySegment<byte>(data);
                                //Envia peticion al usuario
                                requestsManager.userSocket.TryGetValue(requestsManager.FindUserByName(userToSend), out websocket);
                                await websocket.SendAsync(buffer, WebSocketMessageType.Text, true, token);

                                //Recibe respuesta
                                buffer = new ArraySegment<byte>(new byte[4096]);
                                received = await websocket.ReceiveAsync(buffer, token);

                                if(received.MessageType == WebSocketMessageType.Text)
                                {
                                    //Envia la respuesta
                                    request = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count);
                                    requestsManager.userSocket.TryGetValue(user, out websocket);
                                    data = Encoding.UTF8.GetBytes(request);
                                    buffer = new ArraySegment<byte>(data);
                                    await websocket.SendAsync(buffer, WebSocketMessageType.Text, true, token);
                                }
                            }
                        }
                    }
                }
                else
                {
                    await next();
                }
            });*/
        }
    }
}
