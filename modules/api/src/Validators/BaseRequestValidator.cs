namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public class BaseRequestValidator
{
    public bool EntityFound { get; set; } = true;
    public bool RequestValid { get; set; } = true;
}
