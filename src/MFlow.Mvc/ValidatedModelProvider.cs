using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace MFlow.Mvc
{
    class ValidatedModelProvider : ModelValidatorProvider
    {
        readonly static ICollection<Assembly> _assemblies = AppDomain.CurrentDomain.GetAssemblies();
        readonly static ICollection<Type> _types = _assemblies.SelectMany(a => a.GetTypes()).ToList();


        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            var items = new List<TValidator>();
            Type type = null;
            if (!typeof(TValidator).IsGenericType)
            {
                _types.Where(t => typeof(TValidator).IsAssignableFrom(t) && t.IsClass).ToList()
                    .ForEach(i =>
                    {
                        if (!i.IsGenericType)
                        {
                            var constructor = Expression.Lambda(Expression.New(i.GetConstructor(Type.EmptyTypes))).Compile();
                            items.Add((TValidator)constructor.DynamicInvoke());
                        }
                    });
            }
            else
            {
                foreach (var theType in _types)
                {
                    foreach (var item in theType.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(TValidator).GetGenericTypeDefinition())
                            .Select(t => t.GetGenericArguments().First())
                            .ToList())
                    {

                        if (item != null)
                        {
                            type = item.DeclaringType;
                            type = type.MakeGenericType(typeof(TValidator).GetGenericArguments());
                            if (type.IsGenericType)
                            {
                                items.Add((TValidator)(type.GetConstructor(Type.EmptyTypes)).Invoke(new object[] { }));
                            }
                        }
                    }
                }
            }

            return items;
        }
    }
}
