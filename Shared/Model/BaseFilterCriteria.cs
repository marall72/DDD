using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Model
{
    public record BaseFilterCriteria
    {
        public int Offset { get; set; }
        public int TopCount { get; set; } = 1000;
        public string? SearchText { get; set; }
    }
}
