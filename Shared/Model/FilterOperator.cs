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
        StartsWith,
        EndsWith,
        GreaterThan,
        LessThan,
        GreaterOrEqual,
        LessOrEqual,
        In
    }
}
