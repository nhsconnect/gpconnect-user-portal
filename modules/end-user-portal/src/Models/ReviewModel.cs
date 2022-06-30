using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Apply;

public partial class ReviewModel : BaseModel
{
    public string SupplierName => TempData.Get<SoftwareSupplierResult>("SelectedSoftwareSupplierName")?.SoftwareSupplierName;
    public List<SoftwareSupplierProductResult> SupplierProducts => TempData.Get<List<SoftwareSupplierProductResult>>("SelectedSoftwareSupplierProduct");
    public OrganisationResult Organisation => TempData.Get<OrganisationResult> ("Organisation");
    public string SignatoryName => TempData.Get<string>("SignatoryName");
    public string SignatoryRole => TempData.Get<string>("SignatoryRole");
    public string SignatoryEmail => TempData.Get<string>("SignatoryEmail");
    public string UseCaseDescription => TempData.Get<string>("UseCaseDescription");
}
