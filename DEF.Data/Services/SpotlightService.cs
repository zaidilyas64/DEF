using DEF.Model.Components;
using DEF.Shared;
using System;
using Umbraco.Core.Models.PublishedContent;

namespace DEF.Data.Services
{
    public interface ISpotlightService : IDisposable
    {
        Spotlight Get(IPublishedContent source);
    }
    public class SpotlightService : ISpotlightService
    {
        private bool disposed = false;

        public Spotlight Get(IPublishedContent source)
        {
            if (source == null)
            {
                return null;
            }
            return MapSpotlight(source);
        }

        private Spotlight MapSpotlight(IPublishedContent source)
        {
            Spotlight model = new Spotlight(source);
            model.Title = Convert.ToString(source.GetFieldValue("title"));
            return model;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;
            }
        }
    }
}
