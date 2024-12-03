using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.RePurpose;

public class Application : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [];
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}