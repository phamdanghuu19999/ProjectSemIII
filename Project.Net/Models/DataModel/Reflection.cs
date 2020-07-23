using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Project.Net.Models.DataModel
{
    public class Reflection
    {
        public List<Type> GetAllController(string s) {
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = assembly.GetTypes()
                .Where(t => typeof(Controller).IsAssignableFrom(t) && t.Namespace.Contains(s))
                .OrderBy(x => x.Name);
            return types.ToList();
        }
    }
}