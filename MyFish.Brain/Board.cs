using System;
using System.Collections.Generic;
using System.Linq;
using MyFish.Brain.Exceptions;
using MyFish.Brain.Moves;
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

        public King KingOf(Color color)
        {
            var king = Pieces.OfType<King>().SingleOrDefault(x => x.Color == color);

            if (king == null)
            {
                throw new MissingKingException(string.Format("{0} is missing the king", color));
            }
            return king;
        }

        public IEnumerable<T> White<T>() where T : Piece
        {
            return WhitePieces.OfType<T>();
        }

        public IEnumerable<T> Black<T>() where T : Piece
        {
            return BlackPieces.OfType<T>();
        }

        private Dictionary<Position, Piece> _indexedPieces;

        public Piece this[Position position]
        {
            get
            {
                return LookupPiece(position);
            }
        }

        private Piece LookupPiece(Position position)
        {
            if (_indexedPieces == null)
            {
                _indexedPieces = Pieces.ToDictionary(x => x.Position, x => x);
            }
            Piece piece;

            _indexedPieces.TryGetValue(position, out piece);

            return piece;
        }

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
            var moves = this.MovesFor(piece);

            var move = moves.SingleOrDefault(x => x.Destination == destination);

            if (move == null)
            {
                throw new IllegalMoveException(string.Format("{0} cannot move to {1}", piece, destination));
            }
            return Do(move);
        }

        internal Board Do(Move move)
        {
            var destination = GetPawnEnPassantDestination(move) ?? move.Destination;

            var pieces = Pieces.Where(x => x != move.Piece && x.Position != destination);

            var movedPiece = move.Piece.Move(move.Destination);

            var enPassantTarget = GetEnPassantTarget(move);

            return new Board(pieces.Concat(new[] { movedPiece }), NextTurn(), enPassantTarget);
        }

        private Position GetPawnEnPassantDestination(Move move)
        {
            if (move.Piece is Pawn && EnPassantTarget == move.Destination)
            {
                var direction = Turn == Color.White ? -1 : 1;

                return move.Destination + new Vector(0, direction);
            }
            return null;
        }

        private static Position GetEnPassantTarget(Move move)
        {
            if (move.Piece is Pawn)
            {
                var steps = move.Destination.Rank - move.Piece.Position.Rank;

                if (Math.Abs(steps) == 2)
                {
                    return move.Piece.Position + new Vector(0, steps / 2);
                }
            }
            return null;
        }

        private Color NextTurn()
        {
            return Turn == Color.White ? Color.Black : Color.White;
        }

        private readonly Dictionary<object, object> _cache = new Dictionary<object, object>();

        public Func<TArg, TResult> Memoize<TArg, TResult>(Func<TArg, TResult> func)
        {
            return arg =>
            {
                object result;

                if (!_cache.TryGetValue(arg, out result))
                {
                    result = func(arg);

                    _cache.Add(arg, result);
                }

                return (TResult)result;
            };
        }

        public Func<TArg1, TArg2, TResult> Memoize<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> func)
        {
            Func<Tuple<TArg1, TArg2>, TResult> tupleFunc = arg => func(arg.Item1, arg.Item2);

            return (arg1, arg2) => Memoize(tupleFunc)(Tuple.Create(arg1, arg2));
        }

        public Func<TArg1, TArg2, TArg3, TResult> Memoize<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, TResult> func)
        {
            Func<Tuple<TArg1, TArg2, TArg3>, TResult> tupleFunc = arg => func(arg.Item1, arg.Item2, arg.Item3);

            return (arg1, arg2, arg3) => Memoize(tupleFunc)(Tuple.Create(arg1, arg2, arg3));
        }
    }
}