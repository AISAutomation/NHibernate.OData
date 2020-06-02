using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using NHibernate.Type;

namespace NHibernate.OData
{
    internal class ODataSessionFactoryContext
    {
        internal static ODataSessionFactoryContext Empty = new ODataSessionFactoryContext();

        public IDictionary<System.Type, MappedClassMetadata> MappedClassMetadata { get; private set; }

        public IDictionary<System.Type, System.Type> BaseClassToMappedClass { get; private set; }

        private ODataSessionFactoryContext()
        {
            MappedClassMetadata = new Dictionary<System.Type, MappedClassMetadata>();

            BaseClassToMappedClass = new Dictionary<System.Type, System.Type>();
        }

        public ODataSessionFactoryContext(ISessionFactory sessionFactory)
        {
            Require.NotNull(sessionFactory, "sessionFactory");

            /**
             * 01.06.2020: change first parameter from x.GetMappedClass(EntityMode.Poco) to x.MappedClass
             */
            MappedClassMetadata = sessionFactory.GetAllClassMetadata().Values.ToDictionary(
                x => x.MappedClass,
                x => new MappedClassMetadata(x)
            );

            /**
              * 01.06.2020: SessionFactory extended to hold a dictionary with base type (key) and mapped class type (value).
              * The dictionary will be used during building ICriteria if searched type is not mapped, but exists as key in
              * this dictionary. This behavior is required if base controller classes are used and model classes are overwritten in the project
              * and the project models are mapped. If the base model are queried and filtered by name of referenced type, then the
              * OData.nHibernate cannot find the mapping. With the new dictionary the mapping can be found.
              * The "base type to inherited mapping" can only be used, if two or more mapped classes have not the same base type. 
              */
            var baseTypeUse = new Dictionary<string, int>();
            foreach (var mappedClass in MappedClassMetadata)
            {
                var baseTypeName = mappedClass.Key.BaseType.FullName;
                if (baseTypeUse.ContainsKey(baseTypeName))
                {
                    baseTypeUse[baseTypeName] += 1;
                }
                else
                {
                    baseTypeUse[baseTypeName] = 1;
                }
            }

            BaseClassToMappedClass = new Dictionary<System.Type, System.Type>();
            foreach (var mappedClass in MappedClassMetadata)
            {
                var baseTypeName = mappedClass.Key.BaseType.FullName;

                if (baseTypeUse[baseTypeName] == 1)
                {
                    BaseClassToMappedClass[mappedClass.Key.BaseType] = mappedClass.Key;
                }
            }
        }
    }
}
