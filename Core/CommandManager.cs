using System;
using System.Collections.Generic;
using System.Linq;

namespace PreMarket.Core
{
    public class CommandManager : ICommandManager
    {
        public readonly List<(Action execute, Action undo)> Commands = new List<(Action execute, Action undo)>();
        private int _lastExecutedCommandPosition;

        public void ExecuteCommand(Action executeCommand, Action undoCommand)
        {
            if (executeCommand == null)
            {
                throw new ArgumentNullException(nameof(executeCommand));
            }

            if (undoCommand == null)
            {
                throw new ArgumentNullException(nameof(undoCommand));
            }

            RemoveAllRevertedCommand();

            Commands.Add((executeCommand, undoCommand));

            executeCommand();

            _lastExecutedCommandPosition = Commands.Count - 1;
        }
        public void Undo()
        {
            if (!Commands.Any())
            {
                return;
            }

            if (_lastExecutedCommandPosition < 0)
            {
                return;
            }

            Commands[_lastExecutedCommandPosition].undo();
            _lastExecutedCommandPosition--;
        }

        public void Redo()
        {
            if (!Commands.Any())
            {
                return;
            }

            if (_lastExecutedCommandPosition >= Commands.Count)
            {
                return;
            }
            Commands[_lastExecutedCommandPosition + 1].execute();
            _lastExecutedCommandPosition++;
        }

        private void RemoveAllRevertedCommand()
        {
            Commands.RemoveAll(c => Commands.IndexOf(c) > _lastExecutedCommandPosition);
        }
    }
}