using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alex.Net.Crm.SolutionCompare.Parser.Objects
{

    /*
Application Ribbons (RibbonCustomization) 	
1   Entity (Entity) 
Report (Report) 
Article Template (KBArticleTemplate) 	
Field Security Profile (FieldSecurityProfile) 	
92  SDK Message Processing Step (SDKMessageProcessingStep) 
Connection Role (ConnectionRole) 	
Mail Merge Template (MailMergeTemplate) 	
20  Security Role (Role) 
Contract Template (ContractTemplate) 	
9   Option Set (OptionSet) 	
Service Endpoint (ServiceEndpoint) 
Dashboard or Entity Form (SystemForm) 	
91  Plug-in Assembly (PluginAssembly) 	
62  Site Map (SiteMap) 
E-mail Template (EmailTemplate) 	
29  Process (Workflow) 	
61  Web Resource (WebResource)
26  View (View)
     */
    public enum ComponentType
    {
        Undefined = -1,
        Entity = 1,
        OptionSet = 9,
        SecurityRole = 20,
        Workflow = 29,
        WebResource = 61,
        PluginAssembly = 91,
        SDKMessageProcessingStep = 92,
        RibbonCustomization,
        Report,
        KBArticleTemplate,
        FieldSecurityProfile,
        ConnectionRole,
        MailMergeTemplate,
        ContractTemplate,
        ServiceEndpoint,
        SystemForm = 60,
        EmailTemplate,
        View = 26
    }
}
