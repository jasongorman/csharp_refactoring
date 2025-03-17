using System.Linq;
using NUnit.Framework;

namespace MarsRover.Test
{
    [TestFixture]
    public class RoverTests
    {
        [TestCase("N", "E")]
        [TestCase("E", "S")]
        [TestCase("S", "W")]
        [TestCase("W", "N")]
        public void TurnsRightClockwise(string startFacing, string endFacing)
        {
            Rover rover = new Rover(startFacing, 0, 0);
            rover.Go("R");
            Assert.AreEqual(endFacing, rover.Facing);
        }

        [TestCase("N", "W")]
        [TestCase("W", "S")]
        [TestCase("S", "E")]
        [TestCase("E", "N")]
        public void TurnsLeftAntiClockwise(string startFacing, string endFacing)
        {
            Rover rover = new Rover(startFacing, 0, 0);
            rover.Go("L");
            Assert.AreEqual(endFacing, rover.Facing);
        }

        [TestCase("N", new[] {5, 6})]
        [TestCase("E", new[] {6, 5})]
        [TestCase("S", new[] {5, 4})]
        [TestCase("W", new[] {4, 5})]
        public void MovesForwardInDirectionFacing(string facing, int[] endPosition)
        {
            Rover rover = new Rover(facing, 5, 5);
            rover.Go("F");
            CollectionAssert.AreEqual(endPosition, rover.Position);
        }

        [TestCase("N", new [] {5, 4})]
        [TestCase("E", new [] {4, 5})]
        [TestCase("S", new [] {5, 6})]
        [TestCase("W", new [] {6, 5})]
        public void MovesBackFromDirectionFacing(string facing, int[] endPosition)
        {
            Rover rover = new Rover(facing, 5, 5);
            rover.Go("B");
            CollectionAssert.AreEqual(endPosition, rover.Position);
        }

        [Test]
        public void ExecutesSequenceOfInstructions()
        {
            Rover rover = new Rover("N", 5, 5);
            rover.Go("RFF");
            Assert.AreEqual("E", rover.Facing);
            CollectionAssert.AreEqual(new [] {7, 5}, rover.Position);
        }
    }

    public class Rover
    {
        public Rover(string facing, int x, int y)
        {
            Position = new [] {x, y};
            Facing = facing;
        }

        public string Facing { get; private set; }
        public int[] Position { get; private set; }

        public void Go(string instructions)
        {
            instructions.ToCharArray().ToList().ForEach((instruction) =>
            {
                if (instruction == 'R')
                {
                    if (Facing == "N")
                    {
                        Facing = "E";
                        return;
                    }

                    if (Facing == "E")
                    {
                        Facing = "S";
                        return;
                    }

                    if (Facing == "S")
                    {
                        Facing = "W";
                        return;
                    }

                    Facing = "N";
                }

                if (instruction == 'L')
                {
                    if (Facing == "N")
                    {
                        Facing = "W";
                        return;
                    }

                    if (Facing == "W")
                    {
                        Facing = "S";
                        return;
                    }

                    if (Facing == "S")
                    {
                        Facing = "E";
                        return;
                    }

                    Facing = "N";
                }

                if (instruction == 'F')
                {
                    if (Facing == "N")
                    {
                        Position = new [] {Position[0], Position[1] + 1};
                    }

                    if (Facing == "E")
                    {
                        Position = new [] {Position[0] + 1, Position[1]};
                    }

                    if (Facing == "S")
                    {
                        Position = new [] {Position[0], Position[1] - 1};
                    }

                    if (Facing == "W")
                    {
                        Position = new [] {Position[0] - 1, Position[1]};
                    }
                }

                if (instruction == 'B')
                {
                    if (Facing == "N")
                    {
                        Position = new [] {Position[0], Position[1] - 1};
                    }

                    if (Facing == "E")
                    {
                        Position = new [] {Position[0] - 1, Position[1]};
                    }

                    if (Facing == "S")
                    {
                        Position = new [] {Position[0], Position[1] + 1};
                    }

                    if (Facing == "W")
                    {
                        Position = new [] {Position[0] + 1, Position[1]};
                    }
                }
            });
        }
    }
}