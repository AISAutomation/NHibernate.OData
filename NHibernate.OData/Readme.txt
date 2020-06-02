NHibernate.OData depends on the latest version of NHibernate. NHibernate is
not distributed with NHibernate.OData and can be downloaded from
http://sourceforge.net/projects/nhibernate/.

1.9.4.0:
* AliasingNormalizeVisitor: if call to ResolveName fails and used type is key in the "base type to inherited mapping" dictionary, then try to resolve the name again with inherited type. 

1.9.3.0:
* NameResolver: if subclass overrides property with new, then always return property from subclass

1.9.2.0:
* ODataSessionFactoryContext, AliasingNormalizeVisitor: support of aliasing of base classes added

1.9.1.0:
* filter contains (OData V4) added; same behavior like filter subStringOf (OData V3)

1.9.0.0:
* license file included in source folder

1.8.0.0:
* support nHibernate V5.1.3