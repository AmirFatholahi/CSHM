using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSHM.Widget.Log
{
    public interface ILogWidget
    {
        Task ExceptionLog(Exception exception, string? source, int userID = 0);

        public string GetExceptionMessage(Exception ex);

        public string GetExceptionHResult(Exception ex);

        Task EntityLog(string entityName, int entityID, string action, int userID, string changedFields);

        public Task UserLog(string system, int userID, string action, string metaData, string ip);

        Task PasswordLog(int userID, string hashedPassword, string ip);
    }
}
