using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdlegharDotnetDomain.Entities;
using IdlegharDotnetDomain.Providers;

namespace IdlegharDotnetDomain.Tests.MockProviders
{
    public class MockQuestsProvider : IQuestsProvider
    {
        QuestBatch? CurrentBatch = null;
        public async Task<QuestBatch?> GetCurrentQuestBatch()
        {
            await Task.Yield();
            return CurrentBatch;
        }

        public async Task UpdateQuestBatch(QuestBatch batch)
        {
            await Task.Yield();
            CurrentBatch = batch;
        }
    }
}