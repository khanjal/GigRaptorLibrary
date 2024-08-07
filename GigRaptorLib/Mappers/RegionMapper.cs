using GigRaptorLib.Constants;
using GigRaptorLib.Entities;
using GigRaptorLib.Enums;
using GigRaptorLib.Models;
using GigRaptorLib.Utilities;
using GigRaptorLib.Utilities.Extensions;

namespace GigRaptorLib.Mappers;

public static class RegionMapper
{
    public static List<RegionEntity> MapFromRangeData(IList<IList<object>> values)
    {
        var regions = new List<RegionEntity>();
        var headers = new Dictionary<int, string>();
        var id = 0;

        foreach (var value in values)
        {
            id++;
            if (id == 1)
            {
                headers = HeaderHelper.ParserHeader(value);
                continue;
            }

            if (value[0].ToString() == "")
            {
                continue;
            }

            RegionEntity region = new()
            {
                Id = id,
                Region = HeaderHelper.GetStringValue(HeaderEnum.REGION.DisplayName(), value, headers),
                Trips = HeaderHelper.GetIntValue(HeaderEnum.TRIPS.DisplayName(), value, headers),
                Pay = HeaderHelper.GetDecimalValue(HeaderEnum.PAY.DisplayName(), value, headers),
                Tip = HeaderHelper.GetDecimalValue(HeaderEnum.TIP.DisplayName(), value, headers),
                Bonus = HeaderHelper.GetDecimalValue(HeaderEnum.BONUS.DisplayName(), value, headers),
                Total = HeaderHelper.GetDecimalValue(HeaderEnum.TOTAL.DisplayName(), value, headers),
                Cash = HeaderHelper.GetDecimalValue(HeaderEnum.CASH.DisplayName(), value, headers),
                Distance = HeaderHelper.GetDecimalValue(HeaderEnum.DISTANCE.DisplayName(), value, headers),
            };

            regions.Add(region);
        }
        return regions;
    }

    public static SheetModel GetSheet()
    {
        var sheet = SheetsConfig.RegionSheet;

        var shiftSheet = ShiftMapper.GetSheet();

        sheet.Headers = SheetHelper.GetCommonShiftGroupSheetHeaders(shiftSheet, HeaderEnum.REGION);

        return sheet;
    }
}