using System;

namespace Apphbify.Models
{
    /// <summary>
    /// Our own wrapper around the Application returned from the AppHarbor API
    /// Allows us to add more data in the future if needed and simplifies some view compilation issues
    /// </summary>
    public class Application
    {
        public string Name { get; set; }

        public string Slug { get; set; }

        public Uri Url { get; set; }

        internal static Application CreateFromAPI(AppHarbor.Model.Application app)
        {
            return new Application
            {
                Name = app.Name,
                Slug = app.Slug,
                Url = app.Url
            };
        }
    }
}