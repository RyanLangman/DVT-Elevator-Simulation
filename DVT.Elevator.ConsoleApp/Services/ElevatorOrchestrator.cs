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

        private void HandleElevatorPickupArrival(object sender, int destinationFloor)
        {
            var floor = sender as Floor;
            var nextAvailableElevator = GetNextAvailableElevator(floor.Id);
            nextAvailableElevator.PickupFloor = floor.Id;
            nextAvailableElevator.DestinationFloor = destinationFloor;
        }

        private Models.Elevator GetNextAvailableElevator(int pickupFloor)
        {
            // First find an idle elevator on current floor
            var idleOnCurrentFloor = Elevators
                .Where(x => x.Direction == Enums.ElevatorDirection.Idle)
                .Where(x => x.CurrentFloor == pickupFloor);

            if (idleOnCurrentFloor.Any())
            {
                return idleOnCurrentFloor.First();
            }

            // If not, then find the next closest idle elevator
            var closestIdleOnOtherFloors = Elevators
                .Where(x => x.Direction == Enums.ElevatorDirection.Idle)
                .OrderBy(x => Math.Abs(x.CurrentFloor - pickupFloor));

            if (closestIdleOnOtherFloors.Any())
            {
                return closestIdleOnOtherFloors.First();
            }

            return null;

            // TODO: Handle case where all elevators are in motion, this will need a queueing system
            // or for somplicity sake, just reject the input until one is idle.
        }

        /// <summary>
        /// Progress time forward one step (ascend/descend elevators 1 floor and embark/disembark 
        /// passengers if necessary).
        /// </summary>
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

                elevator.PickupFloor = pickupFloor;
                elevator.DestinationFloor = destinationFloor;
            }

            Elevators.ForEach(e => e.MoveOneStep());

            ShowElevatorStatuses();
        }

        public void ShowElevatorStatuses()
        {
            // TODO: Combine elevator and floor information to displaying waiting passengers
            //Console.WriteLine($"{WaitingPassengers - remainingPassengers} passengers boarded on E-{elevator.Id}.");
            //Console.WriteLine($"{remainingPassengers} passengers waiting on F-{Id}.");
            Console.Clear();
            Console.WriteLine("------ Elevators 1-4 Info ------");
            Elevators.ForEach(x => Console.WriteLine(x.ToString()));
            Console.WriteLine("------ Elevators 1-4 Info ------");
        }
    }
}
