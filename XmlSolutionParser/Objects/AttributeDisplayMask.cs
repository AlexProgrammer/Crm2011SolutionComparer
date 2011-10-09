using System;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    [Flags]
    public enum AttributeDisplayMask
    {
        ValidForAdvancedFind,
        ValidForForm,
        ValidForGrid,
        ActivityRegardingName,
        ActivityPointerRegardingName,
        PrimaryName,
        RequiredForForm,
        RequiredForGrid,
        ObjectTypeCode,
        QueueItemTitle
    }
}
