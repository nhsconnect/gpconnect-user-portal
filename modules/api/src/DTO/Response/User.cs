using System;

namespace GpConnect.NationalDataSharingPortal.Api.Dto.Response;

public class User
{
    public int UserId { get; set; } = 0;
    public string EmailAddress { get; set; } = "";
    public DateTime? LastLogonDate { get; set; } = null;
    public bool IsAdmin { get; set; } = false;
}