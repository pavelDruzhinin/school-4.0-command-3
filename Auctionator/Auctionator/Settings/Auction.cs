using Auctionator.Settings.Interface;

namespace Auctionator.Settings
{
    /// <summary>
    /// Конфигурация аукционов
    /// </summary>
    public class Auction : ISettings
    {
        /// <summary>
        /// Аукционы будут начитаться через StartInDays дней
        /// </summary>
        public int StartInDays { get; set; }
        /// <summary>
        /// Аукционы будут начитаться через StartInHours часов
        /// </summary>
        public int StartInHours { get; set; }
        /// <summary>
        /// Аукционы будут начитаться через StartInMinutes минут
        /// </summary>
        public int StartInMinutes { get; set; }
    }
}