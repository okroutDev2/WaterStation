using WaterStation.Models;
using WaterStation.Services;

namespace WaterStation.Forms;

/// <summary>
/// Read-only screen listing all recorded meter readings.
/// </summary>
public sealed class ReadingsViewForm : RecordsViewForm<MeterReading>
{
    private readonly MeterService _meterService;

    public ReadingsViewForm(MeterService meterService)
    {
        _meterService = meterService ?? throw new ArgumentNullException(nameof(meterService));
    }

    protected override string ScreenTitle => "القراءات - WaterStation";

    protected override string SearchCaption => "بحث برقم العداد:";

    protected override string SearchPlaceholder => "رقم العداد (مثال: 100002)";

    protected override void ConfigureGrid()
    {
        dgvList.AccessibleName = "جدول القراءات";
        dgvList.Columns.Add(Column(nameof(MeterReading.MeterReadingId), "رقم القراءة", 70, "0"));
        dgvList.Columns.Add(Column(nameof(MeterReading.MeterNumber), "رقم العداد", 90));
        dgvList.Columns.Add(Column(nameof(MeterReading.ReadingDate), "تاريخ القراءة", 95, "yyyy-MM-dd"));
        dgvList.Columns.Add(Column(nameof(MeterReading.ReadingValue), "قيمة القراءة", 85, "N3"));
        dgvList.Columns.Add(Column(nameof(MeterReading.PreviousReading), "القراءة السابقة", 85, "N3"));
        dgvList.Columns.Add(Column(nameof(MeterReading.Consumption), "الاستهلاك", 80, "N3"));
        dgvList.Columns.Add(Column(nameof(MeterReading.Notes), "ملاحظات", 150));
    }

    protected override async Task<IReadOnlyList<MeterReading>> LoadCoreAsync(string? filter, CancellationToken cancellationToken)
    {
        var readings = await _meterService.GetMeterReadingsAsync(cancellationToken);
        if (string.IsNullOrWhiteSpace(filter))
        {
            return readings;
        }

        return readings
            .Where(r => Contains(r.MeterNumber, filter))
            .ToList();
    }

    private static bool Contains(string? value, string filter) =>
        !string.IsNullOrWhiteSpace(value) && value.Contains(filter, StringComparison.OrdinalIgnoreCase);
}