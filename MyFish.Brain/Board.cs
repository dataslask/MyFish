using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFish.Brain.Exceptions;
using MyFish.Brain.Pieces;

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

        public bool Can(Castle castle, Color white)
        {
            return true;
        }

        public Board Move(string encodedMove)
        {
            if (encodedMove.Length != 5)
            {
                throw new ParseMoveException(string.Format("Don't understand move: {0}", encodedMove));
            }
            var encodedPiece = encodedMove.Substring(0, 3);

            var destination = encodedMove.Substring(3);

            var piece = PieceFacory.Create(encodedPiece);

            return Move(piece, destination);
        }

        public Board Move(Piece piece, Position destination)
        {
            if (piece.Color != Turn)
            {
                throw new WrongTurnException(string.Format("{0} cannot move because it is {1}s turn", piece, Turn));
            }
            var moves = Moves.Moves.For(piece, this, false);

            var move = moves.SingleOrDefault(x => x.Destination == destination);

            if (move == null)
            {
                throw new IllegalMoveException(string.Format("{0} cannot move to {1}", piece, destination));
            }
            var pieces = Pieces.Where(x => x != piece && x.Position != destination);

            var movedPiece = piece.Move(destination);
            
            return new Board(pieces.Concat(new[] { movedPiece }), NextTurn(), null);
        }

        private Color NextTurn()
        {
            return Turn == Color.White ? Color.Black : Color.White;
        }
    }
}