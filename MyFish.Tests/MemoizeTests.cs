using System;
using FluentAssertions;
using MyFish.Tests.Helpers;
using NUnit.Framework;

namespace MyFish.Tests
{
    [TestFixture]
    public class MemoizeTests
    {
        private static int CallCount = 0;

        private static readonly Func<int, int, int> Add = (a, b) =>
        {
            CallCount++;

            return a + b;
        };

        private static readonly Func<int, int> Add5 = a => Add(a, 5);

        [Test]
        public void Function_is_called_first_time_it_is_used()
        {
            CallCount = 0;

            var board = TestBoard.With("");

            board.Memoize(Add5)(12).Should().Be(17);
            board.Memoize(Add5)(3).Should().Be(8);
            board.Memoize(Add)(3, 5).Should().Be(8);
            board.Memoize(Add)(3, 6).Should().Be(9);

            CallCount.Should().Be(4);
        }

        [Test]
        public void Cached_result_is_used_second_time()
        {
            CallCount = 0;

            var board = TestBoard.With("");

            board.Memoize(Add5)(12).Should().Be(17);
            board.Memoize(Add5)(3).Should().Be(8);
            board.Memoize(Add)(3, 5).Should().Be(8);
            board.Memoize(Add)(3, 6).Should().Be(9);

            board.Memoize(Add5)(12).Should().Be(17);
            board.Memoize(Add5)(3).Should().Be(8);
            board.Memoize(Add)(3, 5).Should().Be(8);
            board.Memoize(Add)(3, 6).Should().Be(9);
            
            CallCount.Should().Be(4);            
        }

        [Test]
        public void Cache_is_local_to_board()
        {
            CallCount = 0;

            var board = TestBoard.With("");

            board.Memoize(Add5)(12).Should().Be(17);
            board.Memoize(Add5)(3).Should().Be(8);
            board.Memoize(Add)(3, 5).Should().Be(8);
            board.Memoize(Add)(3, 6).Should().Be(9);

            board = TestBoard.With("");

            board.Memoize(Add5)(12).Should().Be(17);
            board.Memoize(Add5)(3).Should().Be(8);
            board.Memoize(Add)(3, 5).Should().Be(8);
            board.Memoize(Add)(3, 6).Should().Be(9);

            CallCount.Should().Be(8);
        }
    }
}