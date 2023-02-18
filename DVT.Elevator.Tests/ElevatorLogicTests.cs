using DVT.Elevator.ConsoleApp.Enums;
using DVT.Elevator.ConsoleApp.Models;
using DVT.Elevator.ConsoleApp.Models.Interfaces;
using DVT.Elevator.ConsoleApp.Services;

namespace DVT.Elevator.Tests
{
    public class ElevatorLogicTests
    {
        private IElevatorOrchestrator ElevatorOrchestrator;
        public List<IFloor> Floors { get; set; } = new List<IFloor>();
        public List<IElevator> Elevators { get; set; } = new List<IElevator>();

        [Fact]
        public void Chooses_Idle_Elevator_On_Current_Floor()
        {
            // Arrange
            SetupForEachTest();

            // Act
            ElevatorOrchestrator.TimeStep(6, 5, false);

            // Assert
            var ele = Elevators.First(x => x.Id == 3);
            Assert.True(ele.State != ElevatorState.Idle);
        }

        [Fact]
        public void Chooses_Closest_Idle_Elevator_On_Different_Floor()
        {
            // Arrange
            SetupForEachTest();

            // Act
            ElevatorOrchestrator.TimeStep(3, 5, false);

            // Assert
            var ele = Elevators.First(x => x.Id == 4);
            Assert.True(ele.State != ElevatorState.Idle);
        }

        [Fact]
        public void Elevator_Ascends_When_Destination_Floor_Is_Higher()
        {
            // Arrange
            SetupForEachTest();

            // Act
            ElevatorOrchestrator.TimeStep(2, 4, false);
            ElevatorOrchestrator.TimeStep(0, 0, true);

            // Assert
            var ele = Elevators.First(x => x.Id == 4);
            Assert.True(ele.State == ElevatorState.Ascending);
        }

        [Fact]
        public void Elevator_Descends_When_Destination_Floor_Is_Lower()
        {
            // Arrange
            SetupForEachTest();

            // Act
            ElevatorOrchestrator.TimeStep(6, 3, false);
            ElevatorOrchestrator.TimeStep(0, 0, true);

            // Assert
            var ele = Elevators.First(x => x.Id == 3);
            Assert.True(ele.State == ElevatorState.Descending);
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