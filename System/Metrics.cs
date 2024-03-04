
namespace SystemElements
{
    internal enum Metrics
    {
        /// <summary>
        /// 0 - Normal boot, 1 - Fail safe boot, 2 - Fail safe with network boot
        /// </summary>
        SM_CLEANBOOT = 67,

        /// <summary>
        /// Number of display monitors on the desktop
        /// </summary>
        SM_CMONITORS = 80,

        /// <summary>
        /// The number of buttons on a mouse, or zero if no mouse is installed
        /// </summary>
        SM_CMOUSEBUTTONS = 43,

        /// <summary>
        /// The width of the screen of the primary display monitor, in pixels
        /// </summary>
        SM_CXSCREEN = 0,

        /// <summary>
        /// The height of the screen of the primary display monitor, in pixels
        /// </summary>
        SM_CYSCREEN = 1
    }
}
