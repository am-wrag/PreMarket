using System;

namespace PreMarket.Core
{
    public interface ICommandManager
    {
        void ExecuteCommand(Action executeCommand, Action undoCommand);
        void Undo();
        void Redo();
    }
}