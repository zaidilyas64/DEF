using DEF.Data.Infrastructure;
using DEF.Model.Pages;
using DEF.Shared;
using System;
using Umbraco.Core.Models.PublishedContent;

namespace DEF.Data.Services
{
    public interface IHomeService : IDisposable
    {
        Home Get();
    }
    public class HomeService : IHomeService
    {
        private bool disposed = false;

        private IContentRepository contentRepository;

        public HomeService(IContentRepository contentRepository)
        {
            this.contentRepository = contentRepository;
        }

        public Home Get()
        {
            return MapHome(contentRepository.Get());
        }

        private Home MapHome(IPublishedContent source)
        {
            if (source == null)
            {
                return null;
            }

            Home model = new Home(source);
            model.Components = source.GetMultiNodesFieldValue("components");
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
