using Amazon.Lambda.Core;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenTelemetry;
using OpenTelemetry.Exporter.XRay;
using OpenTelemetry.Instrumentation.AWSLambda;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Diagnostics;
using System.Reflection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace otel_dotnet_lambda_sample;




public class Function
{
    public static TracerProvider tracerProvider;

    public const string TraceKey = "Trace";
    public const string ServiceName = nameof(otel_dotnet_lambda_sample);
    public static ActivitySource Source = new ActivitySource(TraceKey);


    public class TraceBlock : IDisposable
    {
        private Activity activity = default;


        public TraceBlock(MethodBase methodBase)
        {

            var traceKey = methodBase != null ? $"{methodBase.DeclaringType?.FullName}" : "none";

            activity = Source.StartActivity(traceKey);

        }

        public void Dispose()
        {
            activity?.Dispose();
        }
    }


    static Function()
    {
        tracerProvider = Sdk.CreateTracerProviderBuilder()
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(ServiceName))
                .AddSource(TraceKey)
                .AddAWSInstrumentation()
                .AddOtlpExporter()
                .AddAWSLambdaConfigurations()
                .AddXRayExporter()
                .Build();
    }



    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="input"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<string> FunctionHandler(JObject? input, ILambdaContext context)
    {

        return await AWSLambdaWrapper.TraceAsync(tracerProvider, async (_, context) =>
         {
             using var trace = new TraceBlock(MethodBase.GetCurrentMethod());
             methodA();
             methodB();

             return JsonConvert.SerializeObject(input);
         }, new JObject(), context);
    }


    public void methodA()
    {
        using var trace = new TraceBlock(MethodBase.GetCurrentMethod());


        Task.Delay(1000).Wait();

    }


    public void methodB()
    {
        using var trace = new TraceBlock(MethodBase.GetCurrentMethod());

        Task.Delay(2000).Wait();

        methodB1();


    }

    public void methodB1()
    {
        using var trace = new TraceBlock(MethodBase.GetCurrentMethod());

        Task.Delay(3000).Wait();


    }

}
