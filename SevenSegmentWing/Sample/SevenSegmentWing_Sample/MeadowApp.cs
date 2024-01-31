using Meadow;
using Meadow.Devices;
using System.Threading.Tasks;
using Meadow.Foundation.FeatherWings;

namespace Featherwings.SevenSegmentWing_Sample
{
    // Change F7FeatherV2 to F7FeatherV1 for V1.x boards
    public class MeadowApp : App<F7FeatherV2>
    {
        SevenSegmentWing sevenSegment;

        public override Task Initialize()
        {
            Resolver.Log.Info("Initialize...");

            sevenSegment = new SevenSegmentWing(Device.CreateI2cBus());

            return base.Initialize();
        }

        public override Task Run()
        {
            Resolver.Log.Info("Run...");

            sevenSegment.SetDisplay("H0L2");

            //sevenSegment.SetDigit((byte)'F', 0, true);
            //sevenSegment.SetDigit(7, 1, true);
            //sevenSegment.SetDigit((byte)'X', 2, true);
            //sevenSegment.SetDigit(2, 4, true);

            //sevenSegment.ColonOn = true;

            return Task.CompletedTask;
        }
    }
}