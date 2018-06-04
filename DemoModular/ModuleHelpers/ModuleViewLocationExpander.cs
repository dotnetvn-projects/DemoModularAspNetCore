using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoModular
{
    public class ModuleViewLocationExpander : IViewLocationExpander
    {
        private const string _modulePart = "ModulePart";

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
            IEnumerable<string> viewLocations)
        {
            if (context.Values.ContainsKey(_modulePart))
            {
                var module = context.Values[_modulePart];

                if (!string.IsNullOrWhiteSpace(module))
                {
                    var moduleViewLocations = new string[]
                    {
                       "/Modules/" + module + "/Views/{1}/{0}.cshtml",
                       "/Modules/" + module + "/Views/Shared/{0}.cshtml"
                    };
                    viewLocations = moduleViewLocations.Concat(viewLocations);
                }
            }
            return viewLocations;
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var controller = context.ActionContext.ActionDescriptor.DisplayName;
            var module = controller.Split('.').Take(2);
            
            if (module.FirstOrDefault() == "Modules")
            {
                var name = String.Join(".", module);
                context.Values[_modulePart] = name;
            }
        }
    }
}
