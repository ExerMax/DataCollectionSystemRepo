using DbAccess.ApiModels;
using DbAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataCollectionSystem.Services
{
    public interface IFixationHandler
    {
        public Task<ActionResult<FixationPoint>> ProcessAsync(FixationData data);
    }
}
