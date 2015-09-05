using System;
using System.Collections.Generic;
using System.Linq;

namespace TraktorMapping.TSI.Format
{
    public enum MappingResolution
    {
        Fine = 0x0000803C,
        Min = 0x0000803D,
        Default = 0x0000803D,
        Coarse = 0x0000003E,
        Switch = 0x0000003F,
    }
}
