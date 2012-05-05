using System.Collections.Generic;
using AppHarbor;
using Apphbify.Data;

namespace Apphbify.Services
{
    public interface IDeploymentService
    {
        DeploymentResult Deploy(AppHarborApi api, string access_token, string siteName, App application, Dictionary<string, string> variables, out string slug);
    }
}