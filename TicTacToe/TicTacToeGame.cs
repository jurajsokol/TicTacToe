using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    public enum Players
    {
        X,
        O,
    }

    public class Field
    { 
        public int PositionX { get; }
        public int PositionY { get; }
        public Players PlayerType { get; }

        public Field(int x, int y)
        {
            PositionX = x;
            PositionY = y;
        }
    }

    public class TicTacToeGame
    {
        private const int BOARD_SIZE = 4;
        private SourceCache<Field, int> gameBoard;

        public TicTacToeGame()
        {
            gameBoard = new SourceCache<Field, int>(f => f.PositionX * BOARD_SIZE + f.PositionY);

            gameBoard.Edit(c => c.AddOrUpdate(
                Enumerable.Range(0, BOARD_SIZE)
                .Select(row => Enumerable.Range(0, BOARD_SIZE)
                    .Select(column => new Field(column, row)))
                .SelectMany(x => x)));
        }

        public IDisposable RegisterPlayerMoves(IObservable<Field> fieldChanges)
        {
            return fieldChanges.Subscribe(field => gameBoard.AddOrUpdate(field));
        }
    }
}
