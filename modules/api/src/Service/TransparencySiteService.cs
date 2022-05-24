using System.Collections.Generic;
using GpConnect.NationalDataSharingPortal.Api.DTO;
using GpConnect.NationalDataSharingPortal.Api.DTO.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Service
{
    public class TransparencySiteService: ITransparencySiteService
    {
        public IEnumerable<TransparencySite> GetMatchingSites(TransparencySiteRequest request) => new List<TransparencySite> 
        {
            { 
                new TransparencySite
                {
                    Name = "Site1",
                    OdsCode = "ODS1",
                    UseCase = "Some Use Case"
                }
            },
            {
                new TransparencySite
                {
                    Name = "Site2",
                    OdsCode = "ODS2",
                    UseCase = "Some Other Use Case"
                }
            }
        };
    }
}