namespace CapitalMarketData.Domain.Enums;
public enum Status
{
    /// <summary>
    /// مجاز
    /// </summary>
    Trading = 1,

    /// <summary>
    /// مجاز-محفوظ
    /// امکان ثبت و حذف سفارش
    /// </summary>
    EnteringOrder = 2,

    /// <summary>
    /// مجاز-متوقف
    /// امکان حذف سفارش
    /// </summary>
    RemovingOrder = 3,

    /// <summary>
    /// ممنوع
    /// بازگشایی و انجام معاملات در همان جلسه معاملاتی می باشد
    /// </summary>
    Suspended = 4,

    /// <summary>
    /// ممنوع-متوقف
    /// بازگشایی و انجام معاملات در همان جلسه معاملاتی نمی باشد
    /// </summary>
    Halted = 5,
}
