using Core.Entities;
using Newtonsoft.Json;

namespace DashboardService.Features.DashboardCards
{
    public class DashboardCardApiModel
    {        
        public int DashboardCardId { get; set; }
        public string Name { get; set; }
        public DashboardCardOptions Options { get; set; }

        public static DashboardCardApiModel FromDashboardCard(DashboardCard dashboardCard)
        {
            var model = new DashboardCardApiModel();
            model.DashboardCardId = dashboardCard.DashboardCardId;
            model.Options = JsonConvert.DeserializeObject<DashboardCardOptions>(dashboardCard.Options);
            return model;
        }
    }

    public class DashboardCardOptions {
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
    }
}
