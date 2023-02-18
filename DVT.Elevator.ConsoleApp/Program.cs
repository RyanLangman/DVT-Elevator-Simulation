using DVT.Elevator.ConsoleApp.Models;
using DVT.Elevator.ConsoleApp.Models.Interfaces;
using DVT.Elevator.ConsoleApp.Services;

// Instantiating the elevators and orchestrator here can be considered dependency inversion
// as we are instantiating concrete types according to these Interfaces and supplying
// it to the Building class, which shouldn't be concerned with how these objects
// are constructed.
var elevators = new List<IElevator>();
for (var i = 1; i <= 4; i++)
{
    elevators.Add(new Elevator(i, 1));
}

IElevatorOrchestrator orchestrator = new ElevatorOrchestrator(elevators);

var building = new Building(elevators, orchestrator);

while (building.Exists)
{
    building.Exist();
}