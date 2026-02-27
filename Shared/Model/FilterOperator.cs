using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Model
{
    public enum FilterOperator
    {
        Equal,
        NotEqual,
        Contains,
        //TODO: add not contains wherever applicable
        NotContains,
        StartsWith,
        EndsWith,
        GreaterThan,
        LessThan,
        GreaterOrEqual,
        LessOrEqual,
        In
    }
}
