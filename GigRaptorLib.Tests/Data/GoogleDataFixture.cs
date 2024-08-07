﻿using GigRaptorLib.Enums;
using GigRaptorLib.Tests.Data.Helpers;
using GigRaptorLib.Utilities.Google;
using Google.Apis.Sheets.v4.Data;

namespace GigRaptorLib.Tests.Data;

public class GoogleDataFixture : IAsyncLifetime // https://xunit.net/docs/shared-context
{
    public async Task InitializeAsync()
    {
        var spreadsheetId = TestConfigurationHelper.GetSpreadsheetId();
        var credential = TestConfigurationHelper.GetJsonCredential();

        var googleSheetHelper = new GoogleSheetService(credential);
        var sheets = Enum.GetValues(typeof(SheetEnum)).Cast<SheetEnum>().ToList();
        var result = await googleSheetHelper.GetBatchData(spreadsheetId!, sheets);

        valueRanges = result?.ValueRanges;
    }

    Task IAsyncLifetime.DisposeAsync()
    {
        return Task.CompletedTask;
    }

    public IList<MatchedValueRange>? valueRanges { get; private set; }
}