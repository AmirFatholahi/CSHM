using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CSHM.Widget.Log;
using CSHM.Widget.Method;

namespace CSHM.Widget.Client
{
    public class ClientWidget : IClientWidget
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogWidget _log;
        public ClientWidget(IHttpContextAccessor httpContextAccessor, ILogWidget log)
        {
            _httpContextAccessor = httpContextAccessor;
            _log = log;
        }

        /// <summary>
        /// دریافت نشانی اینترنتی کاربر
        /// </summary>
        /// <returns></returns>
        public string ClientIP()
        {
            string result = string.Empty;
            try
            {
                var request = _httpContextAccessor?.HttpContext?.Request;
                if (request is null)
                {
                    return result;
                }

                if (request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    result = request.Headers["X-Forwarded-For"].ToString().Split(',').LastOrDefault()?.Trim();
                }

                if (string.IsNullOrEmpty(result) == true)
                {
                    result = request.HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
                }

                if (result == "0.0.0.1" || result == "::1" || result == "localhost")
                {
                    result = "127.0.0.1";
                }

                return result;
            }
            catch (Exception ex)
            {
                result = "Not Detected!";
                _log.ExceptionLog(ex, MethodBase.GetCurrentMethod()?.GetSourceName(), 0);
                return result;
            }
        }

    }
}
