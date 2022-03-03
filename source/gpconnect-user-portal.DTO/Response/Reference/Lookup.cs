namespace gpconnect_user_portal.DTO.Response.Reference
{
    public class Lookup
    {
        public int LookupId { get; set; }
        public int LookupTypeId { get; set; }
        public string LookupValue { get; set; }
        public int LinkedLookupId { get; set; }
        public string LookupTypeName { get; set; }
        public string LookupTypeDescription { get; set; }        
        public string LinkedLookupValue { get; set; }
        public bool IsDisabled { get; set; }
    }
}
