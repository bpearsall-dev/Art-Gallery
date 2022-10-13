using Microsoft.EntityFrameworkCore.Design;

namespace Art_Gallery;

public class ScaffoldingDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection services)
    {
        services.AddHandlebarsScaffolding();
    }
}