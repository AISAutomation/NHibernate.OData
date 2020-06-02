using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.OData
{
    internal enum MethodType
    {
        IsOf,
        Cast,
        EndsWith,
        IndexOf,
        Replace,
        StartsWith,
        ToLower,
        ToUpper,
        Trim,
        SubString,
        SubStringOf,
        Concat,
        Length,
        Year,
        Month,
        Day,
        Hour,
        Minute,
        Second,
        Round,
        Floor,
        Ceiling,
        Any,
        All,

        /**
          * 01.06.2020: method type Contains added; supported in OData V4
          */
        Contains
    }
}
