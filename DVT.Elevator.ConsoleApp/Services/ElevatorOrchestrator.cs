using DVT.Elevator.ConsoleApp.Models;

namespace DVT.Elevator.ConsoleApp.Services
{
    public class ElevatorOrchestrator : IElevatorOrchestrator
    {
        private List<Models.Elevator> Elevators;

        public ElevatorOrchestrator(List<Models.Elevator> elevators, List<Floor> floors)
        {
            Elevators = elevators;

            floors.ForEach(e =>
            {
                e.OnRequestNewPickupEvent += HandleElevatorPickupArrival;
            });
        }

        public void TimeStep(int pickupFloor, int destinationFloor, bool skipInput = false)
        {
            if (!skipInput)
            {
                var elevator = GetNextAvailableElevator(pickupFloor);

                if (elevator == null)
                {
                    Console.WriteLine("All elevators in motion, please try again later.");
                    return;
                }

                elevator.SetFloors(pickupFloor, destinationFloor);
            }

            Elevators.ForEach(e => e.MoveOneStep());

            ShowElevatorStatuses();
        }

        public void ShowElevatorStatuses()
        {
            if (!Console.IsOutputRedirected) Console.Clear();
            Console.WriteLine("------ Elevators 1-4 Info ------");
            Elevators.ForEach(x => Console.WriteLine(x.ToString()));
            Console.WriteLine("------ Elevators 1-4 Info ------");
        }

        private void HandleElevatorPickupArrival(object sender, int destinationFloor)
        {
            var floor = sender as Floor;
            var nextAvailableElevator = GetNextAvailableElevator(floor.Id);
            nextAvailableElevator.SetFloors(floor.Id, destinationFloor);
        }

        private Models.Elevator GetNextAvailableElevator(int pickupFloor)
        {
            // First find an idle elevator on current floor
            var idleOnCurrentFloor = Elevators
                .Where(x => x.State == Enums.ElevatorState.Idle)
                .Where(x => x.CurrentFloor == pickupFloor);

            if (idleOnCurrentFloor.Any())
            {
                return idleOnCurrentFloor.First();
            }

            // If not, then find the next closest idle elevator
            var closestIdleOnOtherFloors = Elevators
                .Where(x => x.State == Enums.ElevatorState.Idle)
                .OrderBy(x => Math.Abs(x.CurrentFloor - pickupFloor));

            if (closestIdleOnOtherFloors.Any())
            {
                return closestIdleOnOtherFloors.First();
            }

            return null;
        }
    }
}
