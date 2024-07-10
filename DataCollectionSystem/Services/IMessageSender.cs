using Microsoft.AspNetCore.Mvc;
using DbAccess.Models;

namespace DataCollectionSystem.Services
{
    public interface IMessageSender
    {
        public Task SendMessageAsync(int ownerId, double tax);
    }
}
