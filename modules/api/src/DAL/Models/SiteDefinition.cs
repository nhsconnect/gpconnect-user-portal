using System;
using GpConnect.NationalDataSharingPortal.Api.Dal.Enumerations;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Models;

public class SiteDefinition
{
    public string OdsCode { get; set; } = "";
    public string PartyKey { get; set; } = "";
    public string Asid { get; set; } = "";
    public Guid UniqueId { get; set; }
    public int Id { get; set; }
    public SiteStatus Status { get; set; }
    public string GpConnectInteractions { get; set; } = "";
    public Guid MasterSiteId { get; set; }
}