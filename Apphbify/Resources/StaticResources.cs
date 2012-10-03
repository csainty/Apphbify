using System;
using System.Collections.Generic;
using System.Reflection;
using Nancy;
using Nancy.Bootstrapper;

namespace Apphbify.Resources
{
    public static class StaticResources
    {
        public static byte[] Robots;
        public static byte[] Humans;

        static StaticResources()
        {
            Robots = ReadFile("robots.txt");
            Humans = ReadFile("humans.txt");
        }

        private static byte[] ReadFile(string name)
        {
            byte[] data;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Apphbify.Resources." + name))
            {
                data = new byte[stream.Length];
                stream.Read(data, 0, (int)stream.Length);
            }
            return data;
        }
    }

    public class StaticResourceStartup : IApplicationStartup
    {
        public void Initialize(IPipelines pipelines)
        {
            RegisterFile("/robots.txt", StaticResources.Robots, pipelines);
            RegisterFile("/humans.txt", StaticResources.Humans, pipelines);
        }

        private void RegisterFile(string name, byte[] data, IPipelines pipelines)
        {
            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
            {
                if (ctx.Request != null && String.Equals(ctx.Request.Path, name, System.StringComparison.InvariantCultureIgnoreCase))
                {
                    var response = new Response
                    {
                        ContentType = "text/plain",
                        StatusCode = HttpStatusCode.OK,
                        Contents = s => s.Write(data, 0, data.Length)
                    };
                    response.Headers["Cache-Control"] = "public, max-age=604800, must-revalidate";
                    return response;
                }
                return null;
            });
        }
    }
}