using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DemoModular
{
    public static class ModuleLoader
    {
        public static void RegisterModules(this IMvcBuilder mvcBuilder, 
            IServiceCollection services, IHostingEnvironment env)
        {
            ModuleFinder moduleFinder = new ModuleFinder(env);
            var modules = moduleFinder.Find();

            if (modules.Any())
            {
                foreach (var module in modules)
                {
                    mvcBuilder.AddApplicationPart(module.Assembly);
                }

                services.Configure<RazorViewEngineOptions>(options =>
                {
                    options.ViewLocationExpanders.Add(new ModuleViewLocationExpander());
                });
            }
        }
    }
}
