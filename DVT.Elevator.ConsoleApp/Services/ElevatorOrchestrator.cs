using DVT.Elevator.ConsoleApp.Models;

namespace DVT.Elevator.ConsoleApp.Services
{
    public class ElevatorOrchestrator : IElevatorOrchestrator
    {
        private List<Floor> floors;
        private List<Models.Elevator> elevators;

        public ElevatorOrchestrator(List<Floor> floors, List<Models.Elevator> elevators)
        {
            this.floors = floors;
            this.elevators = elevators; 
        }

        Models.Elevator IElevatorOrchestrator.GetElevatorStatus(int id)
        {
            throw new NotImplementedException();
        }

        void IElevatorOrchestrator.Pickup(int floorId, int destinationFloor)
        {
            // TODO: Get next elevator
            // Set its pickup point to floorId
            // Add event handler for when elevator reaches pickup point, then proceed
            // to destination
            throw new NotImplementedException();
        }

        public void GoToDestination(int floorId)
        {
            // TODO: Ascend/descen elevator towards destination floorId
            // Once reached, add event handler to "disembark" passengers and set
            // state to idle, awaiting next pickup
            throw new NotImplementedException();
        }

        private Models.Elevator GetNextAvailableElevator()
        {
            return elevators.First();
        }

        /// <summary>
        /// Progress time forward one step (ascend/descend elevators 1 floor and embark/disembark 
        /// passengers if necessary).
        /// </summary>
        public void TimeStep(int callToFloor, int destinationFloor, bool skipInput = false)
        {
            if (!skipInput)
            {
                var elevator = GetNextAvailableElevator();
                elevator.PickupFloor = callToFloor;
                elevator.DestinationFloor = destinationFloor;
            }

            elevators.ForEach(e => e.MoveOneStep());
        }
    }
}
