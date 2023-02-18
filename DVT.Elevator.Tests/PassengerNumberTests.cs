using DVT.Elevator.ConsoleApp.Models;
using DVT.Elevator.ConsoleApp.Models.Interfaces;
using DVT.Elevator.ConsoleApp.Services;

namespace DVT.Elevator.Tests
{
    public class PassengerNumberTests
    {
        private IElevatorOrchestrator ElevatorOrchestrator;
        public List<IFloor> Floors { get; set; } = new List<IFloor>();
        public List<IElevator> Elevators { get; set; } = new List<IElevator>();

        [Fact]
        public void Onboards_All_Passengers_Within_Weight_Limit()
        {
            // Arrange
            SetupForEachTest();
            var sixthFloor = Floors.First(x => x.Id == 6);
            sixthFloor.SetWaitingPassengers(7);

            // Act
            ElevatorOrchestrator.TimeStep(6, 2, false);

            // Assert
            var ele = Elevators.First(x => x.Id == 3);
            Assert.True(ele.Passengers == 7);
        }

        [Fact]
        public void Leaves_Behind_Excess_Passengers_And_Calls_Next_Available_Elevator()
        {
            // Arrange
            SetupForEachTest();
            var sixthFloor = Floors.First(x => x.Id == 6);
            sixthFloor.SetWaitingPassengers(12);

            // Act
            ElevatorOrchestrator.TimeStep(6, 2, false);

            // Since Elevator #2 is on the second floor, we progress time a few steps
            // until it reaches floor 6 and onboards passengers.
            ElevatorOrchestrator.TimeStep(0, 0, true);
            ElevatorOrchestrator.TimeStep(0, 0, true);
            ElevatorOrchestrator.TimeStep(0, 0, true);
            ElevatorOrchestrator.TimeStep(0, 0, true);

            // Assert
            var ele = Elevators.First(x => x.Id == 4);
            Assert.True(ele.Passengers == 5);
        }

        [Fact]
        public void Offboards_All_Passengers_Once_Destination_Floor_Reached()
        {
            // Arrange
            SetupForEachTest();
            var sixthFloor = Floors.First(x => x.Id == 6);
            sixthFloor.SetWaitingPassengers(7);

            // Act
            ElevatorOrchestrator.TimeStep(6, 5, false);
            ElevatorOrchestrator.TimeStep(0, 0, true);
            ElevatorOrchestrator.TimeStep(0, 0, true);

            // Assert
            var ele = Elevators.First(x => x.Id == 3);
            Assert.True(ele.Passengers == 0);
        }


        private void SetupForEachTest()
        {
            Elevators.Add(new ConsoleApp.Models.Elevator(1, 1));
            Elevators.Add(new ConsoleApp.Models.Elevator(2, 1));
            Elevators.Add(new ConsoleApp.Models.Elevator(3, 6));
            Elevators.Add(new ConsoleApp.Models.Elevator(4, 2));
            ElevatorOrchestrator = new ElevatorOrchestrator(Elevators);

            for (var i = 1; i <= 10; i++)
            {
                Floors.Add(new Floor(i, Elevators));
            }

            ElevatorOrchestrator.SetupFloors(Floors);
        }
    }
}