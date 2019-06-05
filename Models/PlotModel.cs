using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.PowerBI.Api.V2;
using Microsoft.Rest;
using Microsoft.PowerBI.Api.V2.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Collections.Specialized;
using System.Web;

namespace DashboardPBI.Models
{
    public class PlotModel : PageModel
    {
        private AppSettings _settings { get; }
        public PlotModel(AppSettings settings)
        {
            _settings = settings;
        }
        public async Task<List<ReportModel>> GetEmbedReport()
        {
            string AccessToken = await GetToken();
            using (var client = new PowerBIClient(new Uri(_settings.ApiUrl), new TokenCredentials(AccessToken, "Bearer")))
            {
                string groupId;

                if (string.IsNullOrEmpty(_settings.GroupId))
                {
                    groupId = (await client.Groups.GetGroupsAsync()).Value.FirstOrDefault()?.Id;
                }
                else
                {
                    groupId = _settings.GroupId;
                }

                if (string.IsNullOrEmpty(groupId))
                {
                    string message = "No group available, need to create a group and upload a report";
                    throw new Exception(message);
                }
                var result = await client.Reports.GetReportsInGroupAsync(groupId);

                List<ReportModel> list = new List<ReportModel>();
                foreach(var report in result.Value)
                {
                    var paramets = new GenerateTokenRequest(accessLevel: "view");
                    var token = await client.Reports.GenerateTokenInGroupAsync(groupId, report.Id, paramets);
                    list.Add(new ReportModel()
                    {
                        Id = report.Id,
                        Name = report.Name,
                        EmbedUrl = report.EmbedUrl,
                        AccessToken = token.Token
                    });
                }
                return list;
            }
        }


        #region Get an authentication access token
        private async Task<string> GetToken()
        {
            AuthenticationContext authContext = new AuthenticationContext(_settings.AuthorityUri);
            var credential = new ClientCredential(_settings.ApplicationId, _settings.Secret);
            var result = await authContext.AcquireTokenAsync(_settings.ResourceUrl, credential);
            return result.AccessToken;
        }

        #endregion

    }
}
