using System.Collections.Generic;
using System.Linq;
using MyFish.Brain;

namespace MyFish.Tests
{
    public static class TestBoard
    {
        public static Board Empty()
        {
            return Board.GetBuilder().Build(new Piece[0], Color.White);            
        }

        public static Board With(string pieceList)
        {
            var pieces = pieceList.Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(CreatePiece);

            return Board.GetBuilder().Build(pieces, Color.White);
        }

        private static Piece CreatePiece(string encodedPiece)
        {
            return PieceFacory.Create(encodedPiece[0], encodedPiece.Substring(1));
        }

    }
}