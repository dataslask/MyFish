using System.Linq;
using MyFish.Brain;

namespace MyFish.Tests.Helpers
{
    public static class TestBoard
    {
        public static Board With(string pieceList, Position enPassantTarget = null, Color turn = Color.White)
        {
            var pieces = pieceList.Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(CreatePiece);

            return Board.GetBuilder().Build(pieces, turn, enPassantTarget);
        }

        private static Piece CreatePiece(string encodedPiece)
        {
            return PieceFacory.Create(encodedPiece[0], encodedPiece.Substring(1));
        }
    }
}