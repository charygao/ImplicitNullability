﻿using System.Threading.Tasks;
using JetBrains.Annotations;

namespace ImplicitNullability.Plugin.Tests.test.data.Integrative.TypeHighlightingTests
{
    public class TypeHighlightingAsyncMethodsSample
    {
        public async Task<string> AsyncMethod()
        {
            return await Async.NotNullResult("");
        }

        public async Task VoidAsyncMethod()
        {
            await Async.NopTask;
        }

        [ItemCanBeNull]
        public async Task<string> NullableAsyncMethod()
        {
            return await Async.CanBeNullResult<string>();
        }

        public Task<string> NonAsyncButTaskResult()
        {
            return Async.NotNullResult("");
        }

        [ItemCanBeNull]
        public Task<string> NonAsyncButNullableTaskResult()
        {
            return Async.CanBeNullResult<string>();
        }

        [CanBeNull]
        [ItemCanBeNull]
        public Task<string> NonAsyncCanBeNullAndItemCanBeNullMethod()
        {
            return null;
        }

        // Prove the exemption for async void (see TypeHighlightingProblemAnalyzer):
        public async void AsyncVoidResult()
        {
            await Async.NopTask;
        }
    }
}
