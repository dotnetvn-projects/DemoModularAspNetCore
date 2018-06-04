using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Loader;

namespace DemoModular
{
    public class ModuleFinder
    {
        private IHostingEnvironment _environment;
        public ModuleFinder(IHostingEnvironment env)
        {
            _environment = env;
        }

        public List<ModuleItem> Find()
        {
            List<ModuleItem> modules = new List<ModuleItem>();
            var moduleRootFolder = new DirectoryInfo(Path.Combine(_environment.ContentRootPath, "Modules"));
            var moduleFolders = moduleRootFolder.GetDirectories();

            foreach (var moduleFolder in moduleFolders)
            {
                var binFolder = new DirectoryInfo(Path.Combine(moduleFolder.FullName, "bin"));
                if (!binFolder.Exists)
                {
                    continue;
                }
                foreach (var file in binFolder.GetFileSystemInfos("*.dll",
                    SearchOption.AllDirectories))
                {
                   var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file.FullName);

                   modules.Add(new ModuleItem { ModuleName = file.FullName, Assembly = assembly });                  
                }
            }
            return modules;
        }
    }
}
