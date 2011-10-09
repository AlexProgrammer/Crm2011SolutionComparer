using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{
    public class Entity
    {
        public int ObjectTypeCode { get; set; }
        public string LogicalName { get; set; }
        public Label Name { get; set; }
        public Label CollectionName { get; set; }
        public Label Description { get; set; }
        public List<Attribute> Attributes { get; set; }
        public bool IsDuplicateCheckSupported { get; set; }
        public bool IsRequiredOffline { get; set; }
        public bool IsCollaboration { get; set; }
        public bool AutoRouteToOwnerQueue { get; set; }
        public bool IsConnectionsEnabled { get; set; }
        public bool IsDocumentManagementEnabled { get; set; }
        /*
OwnershipTypeMask
EntityMask
IsAuditEnabled
IsActivity
ActivityTypeMask
IsActivityParty
IsReplicated
IsReplicationUserFiltered
IsMailMergeEnabled
IsVisibleInMobile
IsMapiGridEnabled
IsReadingPaneEnabled
HasRelatedNotes
HasRelatedActivities
IsCustomizable
IsRenameable
IsMappable
CanModifyAuditSettings
CanModifyMobileVisibility
CanModifyConnectionSettings
CanModifyDuplicateDetectionSettings
CanModifyMailMergeSettings
CanModifyQueueSettings
CanCreateAttributes
CanCreateForms
CanCreateCharts
CanCreateViews
CanModifyAdditionalSettings
        */
    }
}
