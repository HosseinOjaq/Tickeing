namespace Ticketing.Domain.Enums;

using System.ComponentModel.DataAnnotations;

public enum StatusType
{
    [Display(Name = "باز")]
    Open = 1,

    [Display(Name = "در حال پیگیری")]
    InProgress = 2,

    [Display(Name = "بسته")]
    Closed = 3
}
