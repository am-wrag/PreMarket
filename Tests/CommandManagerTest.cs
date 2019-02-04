using System;
using NUnit.Framework;
using PreMarket.Core;

namespace PreMarket.Tests
{
    public class CommandManagerTest
    {
        [Test]
        public void AddCommand_NullExecuteCommand_ThrowArgumentNullException()
        {
            var manager = new CommandManager();

            Assert.Throws<ArgumentNullException>(() => manager.ExecuteCommand(null, () => { }));
        }

        [Test]
        public void AddCommand_NullUndoCommand_ThrowArgumentNullException()
        {
            var manager = new CommandManager();

            Assert.Throws<ArgumentNullException>(() => manager.ExecuteCommand(() => { }, null));
        }

        [Test]
        public void ExecuteCommand_ExecuteCorrectCommand_CorrectCommandExecuting()
        {
            const int startValue = 1;
            const int finishValue = 2;

            var testValue = startValue;

            var manager = new CommandManager();
            
            manager.ExecuteCommand(() => { testValue = finishValue; }, () => { testValue = startValue; });
            
            Assert.IsTrue(testValue == finishValue);
        }

        [Test]
        public void Undo_UndoExecuteCommamd_CommandReverted()
        {
            const int startValue = 1;
            const int finishValue = 2;

            var manager = new CommandManager();

            var testValue = startValue;

            manager.ExecuteCommand(() => testValue = finishValue, () => testValue = startValue);
            manager.Undo();

            Assert.IsTrue(testValue == startValue);
        }

        [Test]
        public void Redo_RedoUndoCommand_CommandSuccessExecute()
        {
            const int startValue = 1;
            const int finishValue = 2;

            var manager = new CommandManager();

            var testValue = startValue;

            manager.ExecuteCommand(() => testValue = finishValue, () => testValue = startValue);
            manager.Undo();
            manager.Redo();

            Assert.IsTrue(testValue == finishValue);
        }

        [Test]
        public void ExecuteCommand_ExecuteCommandAfterUndoAnother2Command_CorrectCommandCount()
        {
            var manager = new CommandManager();

            manager.ExecuteCommand(() => { }, () => { });
            manager.ExecuteCommand(() => { }, () => { });
            manager.ExecuteCommand(() => { }, () => { });
            
            manager.Undo();
            manager.Undo();

            manager.ExecuteCommand(() => { }, () => { });

            Assert.IsTrue(manager.Commands.Count == 2);
        }
    }
}