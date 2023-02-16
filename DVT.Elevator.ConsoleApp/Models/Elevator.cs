using DVT.Elevator.ConsoleApp.Enums;

namespace DVT.Elevator.ConsoleApp.Models
{
    public class Elevator
    {
        public int Id { get; set; }

        public Floor CurrentFloor { get; set; }

        public Floor DestinationFloor { get; set; }

        public List<Passenger> Passengers { get; set; } = new List<Passenger>();

        public int WeightLimit { get; } = 7;

        public ElevatorDirection Direction
        {
            get
            {
                return CurrentFloor.Id == 1
                    ? ElevatorDirection.Ascending
                    : DestinationFloor.Id > CurrentFloor.Id ? 
                        ElevatorDirection.Ascending :
                        ElevatorDirection.Descending;
            }
        }

        public Elevator(int id)
        {
            Id = id;
        }
    }
}
