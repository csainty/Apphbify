using System.Collections.Generic;
using AppHarbor;
using Apphbify.Data;

namespace Apphbify.Services
{
    public interface IDeploymentService
    {
        DeploymentResult Deploy(string siteName, App application, Dictionary<string, string> variables, out string slug);
    }
}