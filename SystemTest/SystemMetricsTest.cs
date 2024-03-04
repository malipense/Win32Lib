using Xunit;
using SystemElements;

namespace SystemTest
{
    public class SystemMetricsTest
    {
        [Fact]
        public void ShouldGetAmountOfVisibleMonitors()
        {
            SystemMetrics sysMetrics = new SystemMetrics();
            var monitorsCount = sysMetrics.GetDisplayMonitors();

            Assert.True(monitorsCount > 0);
        }

        [Fact]
        public void ShouldGetBootUpType()
        {
            SystemMetrics sysMetrics = new SystemMetrics();
            var bootUpType = sysMetrics.GetBootUpType();

            Assert.True(bootUpType != -1);
        }

        [Fact]
        public void ShouldGetScreenSize()
        {
            SystemMetrics sysMetrics = new SystemMetrics();
            var screenSize = sysMetrics.GetScreenSize();

            Assert.True(screenSize.x > 0);
            Assert.True(screenSize.y > 0);
        }
    }
}