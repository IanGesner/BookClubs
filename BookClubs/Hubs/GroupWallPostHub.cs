using BookClubs.Services;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace BookClubs.Hubs
{
    public class GroupWallPostHub : Hub
    {
        private readonly IGroupService _groupService;

        public GroupWallPostHub(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public Task JoinDiscussion(int groupId)
        {
            return Groups.Add(Context.ConnectionId, groupId.ToString());
        }

        public Task LeaveDiscussion(int groupId)
        {
            return Groups.Remove(Context.ConnectionId, groupId.ToString());
        }

        public void AddPost(string body, string posterId, string groupId)
        {
            _groupService.AddPost(body, posterId, groupId);
        }
    }
}