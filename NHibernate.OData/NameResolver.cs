using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NHibernate.OData
{
    /// <summary>
    /// Name resolver to resolve names from queries to entity names.
    /// </summary>
    public class NameResolver
    {
        /// <summary>
        /// Resolve a query name to an entity name.
        /// </summary>
        /// <param name="name">The name to map.</param>
        /// <param name="type">The type of the entity to map the name for.</param>
        /// <param name="caseSensitive">Whether the <param name="name"> parameter must be treated case sensitive.</param></param>
        /// <returns>The mapped name and member type or null when the name could not be resolved.</returns>
        public virtual ResolvedName ResolveName(string name, System.Type type, bool caseSensitive)
        {

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            /**
             * 01.06.2020: In case a property is overwritten with virtual new, then the call type.GetProperty will fail. Therefore we
             * query a list of properties with this name and check again with declaring type if we found more then one property.
             */
            var properties = type.GetProperties(bindingFlags).Where(x => caseSensitive ? x.Name == name : x.Name.ToLower() == name.ToLower());

            var property = properties.Count() == 1 ? properties.FirstOrDefault() : properties.FirstOrDefault(x => x.DeclaringType.FullName == type.FullName);

            if (property != null)
                return new ResolvedName(property.PropertyType, property.Name);

            var field = type.GetField(name, bindingFlags);

            if (field != null)
                return new ResolvedName(field.FieldType, field.Name);

            return null;
        }
    }
}
