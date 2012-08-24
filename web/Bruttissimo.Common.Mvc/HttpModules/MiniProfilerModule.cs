using System;
using System.Web;
using StackExchange.Profiling;

namespace Bruttissimo.Common.Mvc
{
    public class MiniProfilerModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.PostAuthenticateRequest += PostAuthenticateRequest;
            context.EndRequest += EndRequest;
        }

        public void Dispose()
        {
        }

        protected void BeginRequest(object sender, EventArgs args)
        {
            MiniProfiler.Start();
        }

        protected void PostAuthenticateRequest(object sender, EventArgs args)
        {
            HttpApplication application = (HttpApplication)sender;
            HttpRequest request = application.Request;
            
            if (!request.CanDisplayDebuggingDetails())
            {
                // abort profiling session if this isn't a local request and the user is not an administrator.
                MiniProfiler.Stop(discardResults: true);
            }
        }

        protected void EndRequest(object sender, EventArgs args)
        {
            MiniProfiler.Stop();
        }
    }
}
