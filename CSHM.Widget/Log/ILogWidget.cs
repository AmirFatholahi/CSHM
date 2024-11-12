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
    }
}
