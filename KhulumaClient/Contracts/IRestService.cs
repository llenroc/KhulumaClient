using KhulumaClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KhulumaClient
{
	public interface IRestService
	{
		// Get locations
		Task<List<locationModel>> GetLocationsAsync();
		// Get Messages
		Task<List<ChatModel>> GetChatsAsync();
		// Get Mobile Pages
		Task<List<MobilePageModel>> GetMobilePagesAsync();
		// Get Flagged Content
		Task<List<FlaggedContentModel>> GetFlaggedContentAsync();


		// Create user
		Task<StatusReportUser> PostUser(PostUserModel appuser);

        // Get user
        Task<userModel> GetThisUser();
    }

}
