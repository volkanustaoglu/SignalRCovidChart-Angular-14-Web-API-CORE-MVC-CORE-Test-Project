using Microsoft.AspNetCore.SignalR;
using SignalRCovidChart.API.Models;

namespace SignalRCovidChart.API.Hubs
{
    public class CovidHub : Hub
    {
        private readonly CovidService _service;

        public CovidHub(CovidService service)
        {
            _service = service;
        }

        public async Task GetCovidList()
        {
            await Clients.All.SendAsync("ReceiveCovidList", _service.GetCovidChartList());

        }
    }
}
