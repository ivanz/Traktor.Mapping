using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace TraktorMapping.TSI.Format
{
    /// <summary>
    /// Describes the target type of a control.
    /// </summary>
    public enum TargetType
    {
        Unknown,
        Global,
        Track,
        Remix,
        FX,
    }
}
