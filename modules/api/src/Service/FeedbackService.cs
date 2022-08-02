using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using System;
using System.Data;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service;

public class FeedbackService : IFeedbackService
{
    private readonly IDataService _dataService;

    public FeedbackService(IDataService dataService)
    {
        _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));
    }

    public async Task CreateFeedbackAsync(FeedbackAddRequest feedbackAddRequest)
    {
        var query = "application.add_feedback";
        var parameters = new DynamicParameters();
        parameters.Add("_overall_rating", feedbackAddRequest.OverallRating, DbType.String, ParameterDirection.Input);
        parameters.Add("_improve_service", feedbackAddRequest.ImproveService, DbType.String, ParameterDirection.Input);
        await _dataService.ExecuteQuery(query, parameters);
    }
}



