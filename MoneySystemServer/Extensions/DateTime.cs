
namespace Logic.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsRangeDate(RangeDate rangeDate1, RangeDate rangeDate2)
        {
            return
          (rangeDate1.Start <= rangeDate2.Start &&
           rangeDate2.Start <= (rangeDate1.End ?? DateTime.MaxValue))
           ||
           (rangeDate1.Start <= (rangeDate2.End ?? DateTime.MaxValue) &&
            (rangeDate2.End ?? DateTime.MaxValue) <= (rangeDate1.End ?? DateTime.MaxValue))
           ||
           (rangeDate2.Start <= rangeDate1.Start &&
            rangeDate1.Start <= (rangeDate2.End ?? DateTime.MaxValue))
           ||
           (rangeDate2.Start <= (rangeDate1.End ?? DateTime.MaxValue) &&
            (rangeDate1.End ?? DateTime.MaxValue) <= (rangeDate2.End ?? DateTime.MaxValue));
        }

    }
    public class RangeDate
    {
        public RangeDate(DateTime start,DateTime? end)
        {
            this.Start = start;
            this.End = end;
        }

        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}
