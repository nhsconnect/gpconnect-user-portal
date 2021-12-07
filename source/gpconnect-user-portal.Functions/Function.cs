using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using gpconnect_user_portal.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Functions
{
    public class Function
    {
        private static async Task Main(string[] args)
        {
            Func<IReferenceService, ILambdaContext, Task> handler = FunctionHandler;
            using (var handlerWrapper = HandlerWrapper.GetHandlerWrapper(handler, new DefaultLambdaJsonSerializer()))
            using (var bootstrap = new LambdaBootstrap(handlerWrapper))
            {
                await bootstrap.RunAsync();
            }
        }

        public static async Task<Task> FunctionHandler(IReferenceService referenceService, ILambdaContext context)
        {
            var response = await referenceService.GetCCGs();
            return response;
        }
    }
}
