﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MsilDecompiler.Host.Providers;
using System.Linq;
using Mono.Cecil;

namespace MsilDecompiler.Host
{
    public class GetMembersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDecompilationProvider _decompilationProvider;

        public GetMembersMiddleware(RequestDelegate next, IDecompilationProvider decompilationProvider)
        {
            _next = next;
            _decompilationProvider = decompilationProvider;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.HasValue)
            {
                var endpoint = httpContext.Request.Path.Value;
                if (endpoint == MsilDecompilerEndpoints.Members)
                {
                    await Task.Run(() =>
                    {
                        var rid = JsonHelper.DeserializeRequestObject(httpContext.Request.Body)
                            .ToObject<DecompileTypeRequest>().Rid;
                        var members = _decompilationProvider.GetChildren(TokenType.TypeDef, rid);
                        var data = new { Members = members.Select(tuple => new MemberData { Name = tuple.Item1, Token = tuple.Item2 }) };
                        MiddlewareHelpers.WriteTo(httpContext.Response, data);
                    });
                    return;
                }
            }

            await _next(httpContext);
        }
    }
}