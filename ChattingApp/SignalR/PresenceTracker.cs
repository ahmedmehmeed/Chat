namespace ChattingApp.SignalR
{
    public class PresenceTracker
    {
        private static readonly Dictionary<string, List<String>> _onlineUsers = new();
         
        public Task UserConnected(string userName,string connectionId)
        {
            lock (_onlineUsers) //  if there is any concurrence operation on dictionary so use 'lock'
            {
                if (_onlineUsers.ContainsKey(userName)) //check if this use have dictionary
                    _onlineUsers[userName].Add(connectionId); // add connection id to list 
                
                else
                    _onlineUsers.Add(userName, new List<String> { connectionId}); // create dictionary 
            }

            return Task.CompletedTask;
        }

        public Task UserDisconnected(string userName, string connectionId)
        {
            lock(_onlineUsers) 
            {
                if (! _onlineUsers.ContainsKey(userName)) return Task.CompletedTask;
                _onlineUsers[userName].Remove(connectionId);
                if (_onlineUsers[userName].Count == 0)
                {
                    _onlineUsers.Remove(userName);
                }
            }



            return Task.CompletedTask;
        }


        public Task<string[]> GetOnlineUsers()
        {
            string[] OnlineUsers;
            lock (_onlineUsers)
            {
                OnlineUsers =_onlineUsers.OrderBy(k=>k.Key).Select(k=>k.Key).ToArray();
            }
                return Task.FromResult(OnlineUsers);
        }
    }
}