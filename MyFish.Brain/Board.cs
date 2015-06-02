using System.Collections.Generic;
using System.Linq;

namespace MyFish.Brain
{
    public class Board
    {
        private class Builder : IBoardBuilder
        {
            public Board Build(IEnumerable<Piece> pieces, Color turn, Position enPassantTarget)
            {
                return new Board(pieces, turn, enPassantTarget ?? Position.Invalid);
            }
        }

        private readonly Piece[] _pieces;
 
        public Color Turn { get; private set; }
        public Position EnPassantTarget { get; private set; }

        public IEnumerable<Piece> Pieces { get { return _pieces; } }
        public IEnumerable<Piece> WhitePieces { get { return _pieces.Where(x => x.Color == Color.White); } }
        public IEnumerable<Piece> BlackPieces { get { return _pieces.Where(x => x.Color == Color.Black); } }

        public IEnumerable<T> White<T>() where T : Piece
        {
            return WhitePieces.OfType<T>();
        }

        public IEnumerable<T> Black<T>() where T : Piece
        {
            return BlackPieces.OfType<T>();
        }

        public Piece this[Position position] { get { return Pieces.SingleOrDefault(x => x.Position == position); } }

        private Board(IEnumerable<Piece> pieces, Color turn, Position enPassantTarget)
        {
            _pieces = pieces.ToArray();
            Turn = turn;
            EnPassantTarget = enPassantTarget;
        }

        public static IBoardBuilder GetBuilder()
        {
            return new Board.Builder();
        }

        public Board Next(Move move)
        {
            return null;
        }

        public bool Can(Castle castle, Color white)
        {
            return true;
        }
    }
}