using System;

namespace TestAPI.Models
{
  public class Forecast
  {
    public DateTime Id { get; set; }
    public int Celsius { get; set; }
    public string SummaryId { get; set; }
    public virtual Summary Summary { get; set; }
  }
}