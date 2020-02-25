using AppStoreConnect;
using AppStoreConnect.Jwt;
using AppStoreConnect.Models.Responses.Common;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AppStoreConnectCli.Common
{
    public static class Extensions
    {
        public static Command AddSubCommandArgument(this Command command)
        {
            command.AddArgument(new Argument<string>("token"));
            return command;
        }

        public static AppStoreConnectionClient GetClient(this string token)
            => AppStoreConnectClientBuilder
                        .GetBuilder()
                        .FromToken(token)
                        .Build();

        public static async Task Handle<T>(this ApplicationResponse result, Func<T, Task> action)
        {
            if (result is T res)
            {
                await action(res);
            }
            else if (result is InternalErrorResponse err)
            {
                err.Print();
            }
            else if(result is ErrorResponse errRes)
            {
                errRes.Print();
            }
        }

        public static void Handle<T>(this ApplicationResponse result, Action<T> action, bool? outJson = null)
        {
            if (result is T res)
            {
                if (outJson is bool oj && oj)
                {
                    Console.WriteLine(JsonSerializer.Serialize(oj, new JsonSerializerOptions { IgnoreNullValues = true }));
                }
                else
                {
                    action(res);
                }
            }
            else if (result is InternalErrorResponse err)
            {
                err.Print();
            }
            else if (result is ErrorResponse errRes)
            {
                errRes.Print();
            }
        }
    }
}
