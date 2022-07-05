using DEF.Data.Infrastructure;
using DEF.Data.Services;
using Umbraco.Core.Composing;

namespace DEF.Data.Composer
{
    public class DEFComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register(typeof(ICmsInfoRepository), typeof(CmsInfoRepository), Lifetime.Transient);
            composition.Register(typeof(IContentRepository), typeof(ContentRepository), Lifetime.Transient);

            composition.Register(typeof(IHomeService), typeof(HomeService), Lifetime.Transient);
            composition.Register(typeof(ISpotlightService), typeof(SpotlightService), Lifetime.Transient);
        }
    }
}
