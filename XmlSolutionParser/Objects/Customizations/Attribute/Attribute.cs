using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class Attribute
    {
        public string PhysicalName { get; set; }
        public Label DisplayName { get; set; }
        public Label Description { get; set; }
        public AttributeType Type { get; set; }
        public string Name { get; set; }
        public string LogicalName { get; set; }
        public RequiredLevel RequiredLevel { get; set; }
        public AttributeDisplayMask DisplayMask { get; set; }
        public ImeMode ImeMode { get; set; }
        public bool IsValidForUpdateApi { get; set; }
        public bool IsValidForReadApi { get; set; }
        public bool IsValidForCreateApi { get; set; }
        public bool IsCustomField { get; set; }
        public bool IsAuditEnabled { get; set; }
        public bool IsCustomizable { get; set; }
        public bool IsRenameable { get; set; }
        public bool IsSecured { get; set; }
        public bool IsLogical { get; set; }
        public bool CanModifySearchSettings { get; set; }
        public bool CanModifyRequirementLevelSettings { get; set; }
        public bool CanModifyAdditionalSettings { get; set; }
        public int AppDefaultValue { get; set; }
        public AttributeFormat Format { get; set; }
        public int MaxLength { get; set; }
        public int Length { get; set; }
        public List<LookupType> LookupTypes { get; set; }
        public LookupStyle LookupStyle { get; set; }
        public string OptionSetName { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int Accuracy { get; set; }
        public int AccuracySource { get; set; }
        public string CalculationOf { get; set; }
        public int ReferencedObjectTypeCode { get; set; }
        public string YomiOf { get; set; }
    }
}
