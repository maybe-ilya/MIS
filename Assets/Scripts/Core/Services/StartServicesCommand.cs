using System.Linq;

namespace mis.Core
{
    public class StartServicesCommand : AbstractCommand
    {
        protected override void ExecuteInternal()
        {
            var startableServices = GameServices.GetMultiple<IStartableService>()
                .OrderBy(service => service.StartPriority);

            foreach (var service in startableServices)
            {
                service.OnServiceStart();
            }

            SucceedCommand();
        }
    }
}