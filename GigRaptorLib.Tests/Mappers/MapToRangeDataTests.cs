﻿using FluentAssertions;
using GigRaptorLib.Entities;
using GigRaptorLib.Enums;
using GigRaptorLib.Mappers;
using GigRaptorLib.Tests.Data.Helpers;
using GigRaptorLib.Utilities;
using GigRaptorLib.Utilities.Extensions;

namespace GigRaptorLib.Tests.Mappers;

public class MapToRangeDataTests
{
    private static SheetEntity? _sheetData;

    public MapToRangeDataTests()
    {
        _sheetData = JsonHelpers.LoadSheetJson();
    }

    [Fact]
    public void GivenSheetData_ThenReturnShiftData()
    {
        var shiftHeaders = JsonHelpers.LoadJsonSheetData("Shift")![0];
        var shifts = ShiftMapper.MapToRangeData(_sheetData!.Shifts, shiftHeaders);
        var headers = HeaderHelper.ParserHeader(shiftHeaders);

        Assert.NotNull(shifts);
        shifts.Count.Should().Be(3);

        for (int i = 0; i < shifts.Count; i++)
        {
            var shift = shifts[i];
            if (shift == null) continue;
            var shiftData = _sheetData.Shifts[i];

#pragma warning disable CS8602 // Rethrow to preserve stack details
            shiftData.Date.Should().BeEquivalentTo(shift[shiftHeaders.IndexOf(HeaderEnum.DATE.DisplayName())].ToString());
            shiftData.Start.Should().BeEquivalentTo(shift[shiftHeaders.IndexOf(HeaderEnum.TIME_START.DisplayName())].ToString());
            shiftData.Finish.Should().BeEquivalentTo(shift[shiftHeaders.IndexOf(HeaderEnum.TIME_END.DisplayName())].ToString());
            shiftData.Service.Should().BeEquivalentTo(shift[shiftHeaders.IndexOf(HeaderEnum.SERVICE.DisplayName())].ToString());
            shiftData.Active.Should().BeEquivalentTo(shift[shiftHeaders.IndexOf(HeaderEnum.TIME_ACTIVE.DisplayName())].ToString());
            shiftData.Time.Should().BeEquivalentTo(shift[shiftHeaders.IndexOf(HeaderEnum.TIME_TOTAL.DisplayName())].ToString());
            shiftData.Region.Should().BeEquivalentTo(shift[shiftHeaders.IndexOf(HeaderEnum.REGION.DisplayName())].ToString());
            shiftData.Note.Should().BeEquivalentTo(shift[shiftHeaders.IndexOf(HeaderEnum.NOTE.DisplayName())].ToString());
#pragma warning restore CS8602 // Rethrow to preserve stack details

            if (shiftData.Number == null)
                HeaderHelper.GetIntValue(HeaderEnum.NUMBER.DisplayName(), shift!, headers).Should().Be(0);
            else
                shiftData.Number.Should().Be(HeaderHelper.GetIntValue(HeaderEnum.NUMBER.DisplayName(), shift!, headers));

            if (shiftData.Omit == null)
                HeaderHelper.GetBoolValue(HeaderEnum.TIME_OMIT.DisplayName(), shift!, headers).Should().Be(false);
            else
                shiftData.Omit.Should().Be(HeaderHelper.GetBoolValue(HeaderEnum.TIME_OMIT.DisplayName(), shift!, headers));

            // TODO: Future support of shift only would use this.
            //shiftData.Pay.Should().Be(HeaderParser.GetDecimalValue(HeaderEnum.PAY.DisplayName(), shift, headers));
            //shiftData.Tip.Should().Be(HeaderParser.GetDecimalValue(HeaderEnum.TIPS.DisplayName(), shift, headers));
            //shiftData.Bonus.Should().Be(HeaderParser.GetDecimalValue(HeaderEnum.BONUS.DisplayName(), shift, headers));
            //shiftData.Total.Should().Be(HeaderParser.GetDecimalValue(HeaderEnum.TOTAL.DisplayName(), shift, headers));
            //shiftData.Cash.Should().Be(HeaderParser.GetDecimalValue(HeaderEnum.CASH.DisplayName(), shift, headers));
        }
    }

    [Fact]
    public void GivenSheetData_ThenReturnTripData()
    {
        var tripHeaders = JsonHelpers.LoadJsonSheetData("Trip")![0];
        var trips = TripMapper.MapToRangeData(_sheetData!.Trips, tripHeaders);
        var headers = HeaderHelper.ParserHeader(tripHeaders);

        Assert.NotNull(trips);
        trips.Count.Should().Be(4);

        for (int i = 0; i < trips.Count; i++)
        {
            var trip = trips[i];
            if (trip == null) continue;
            var tripData = _sheetData.Trips[i];

#pragma warning disable CS8602 // Rethrow to preserve stack details
            tripData.Date.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.DATE.DisplayName())].ToString());
            tripData.Service.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.SERVICE.DisplayName())].ToString());
            tripData.Place.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.PLACE.DisplayName())].ToString());
            tripData.Pickup.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.PICKUP.DisplayName())].ToString());
            tripData.Dropoff.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.DROPOFF.DisplayName())].ToString());
            tripData.Duration.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.DURATION.DisplayName())].ToString());
            tripData.Name.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.NAME.DisplayName())].ToString());
            tripData.StartAddress.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.ADDRESS_START.DisplayName())].ToString());
            tripData.EndAddress.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.ADDRESS_END.DisplayName())].ToString());
            tripData.EndUnit.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.UNIT_END.DisplayName())].ToString());
            tripData.OrderNumber.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.ORDER_NUMBER.DisplayName())].ToString());
            tripData.Note.Should().BeEquivalentTo(trip[tripHeaders.IndexOf(HeaderEnum.NOTE.DisplayName())].ToString());
#pragma warning restore CS8602 // Rethrow to preserve stack details

            // Number
            if (tripData.Number == null)
                HeaderHelper.GetIntValue(HeaderEnum.NUMBER.DisplayName(), trip!, headers).Should().Be(0);
            else
                tripData.Number.Should().Be(HeaderHelper.GetIntValue(HeaderEnum.NUMBER.DisplayName(), trip!, headers));

            // Odometer Start
            if (tripData.OdometerStart == null)
                HeaderHelper.GetDecimalValue(HeaderEnum.ODOMETER_START.DisplayName(), trip!, headers).Should().Be(0);
            else
                tripData.OdometerStart.Should().Be(HeaderHelper.GetDecimalValue(HeaderEnum.ODOMETER_START.DisplayName(), trip!, headers));

            // Odoemeter End
            if (tripData.OdometerEnd == null)
                HeaderHelper.GetDecimalValue(HeaderEnum.ODOMETER_END.DisplayName(), trip!, headers).Should().Be(0);
            else
                tripData.OdometerEnd.Should().Be(HeaderHelper.GetDecimalValue(HeaderEnum.ODOMETER_START.DisplayName(), trip!, headers));

            // Distance
            if (tripData.Distance == null)
                HeaderHelper.GetDecimalValue(HeaderEnum.DISTANCE.DisplayName(), trip!, headers).Should().Be(0);
            else
                tripData.Distance.Should().Be(HeaderHelper.GetDecimalValue(HeaderEnum.DISTANCE.DisplayName(), trip!, headers));

            // Pay
            if (tripData.Pay == null)
                HeaderHelper.GetDecimalValue(HeaderEnum.PAY.DisplayName(), trip!, headers).Should().Be(0);
            else
                tripData.Pay.Should().Be(HeaderHelper.GetDecimalValue(HeaderEnum.PAY.DisplayName(), trip!, headers));

            // Tip
            if (tripData.Tip == null)
                HeaderHelper.GetDecimalValue(HeaderEnum.TIPS.DisplayName(), trip!, headers).Should().Be(0);
            else
                tripData.Tip.Should().Be(HeaderHelper.GetDecimalValue(HeaderEnum.TIPS.DisplayName(), trip!, headers));

            // Bonus
            if (tripData.Bonus == null)
                HeaderHelper.GetDecimalValue(HeaderEnum.BONUS.DisplayName(), trip!, headers).Should().Be(0);
            else
                tripData.Bonus.Should().Be(HeaderHelper.GetDecimalValue(HeaderEnum.BONUS.DisplayName(), trip!, headers));

            // Cash
            if (tripData.Cash == null)
                HeaderHelper.GetDecimalValue(HeaderEnum.CASH.DisplayName(), trip!, headers).Should().Be(0);
            else
                tripData.Cash.Should().Be(HeaderHelper.GetDecimalValue(HeaderEnum.CASH.DisplayName(), trip!, headers));
        }
    }
}
