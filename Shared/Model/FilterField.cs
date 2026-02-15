using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Model
{
    public class FilterField<T>
    {
        public FilterField(T? value, FilterOperator @operator)
        {
            Value = value;
            Operator = @operator;
        }

        public T? Value { get; set; }
        public FilterOperator Operator { get; set; } = FilterOperator.Equal;
    }
}
