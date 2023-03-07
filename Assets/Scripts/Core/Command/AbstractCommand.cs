using Cysharp.Threading.Tasks;
using System;
using UDebug = UnityEngine.Debug;

namespace mis.Core
{
    public abstract class AbstractCommand : ICommand
    {
        private UniTaskCompletionSource<ICommand> _taskCompletionSource;

        private static uint _lastCommandId = 0;

        public uint Id { get; private set; }
        public CommandState State { get; private set; }

        public bool IsInProgress => State == CommandState.InProgress;
        public bool IsFinished => State > CommandState.InProgress;
        public bool IsSucceed => State == CommandState.Success;
        public bool IsFailed => State == CommandState.Failure;

        public AbstractCommand()
        {
            Id = +_lastCommandId;
            SetState(CommandState.NotStarted);
        }

        public void Execute()
        {
            if (State != CommandState.NotStarted)
            {
                return;
            }

            ExecuteBasicImplementation();
        }

        public UniTask<ICommand> ExecuteAsync()
        {
            if (State != CommandState.NotStarted)
            {
                if (_taskCompletionSource == null)
                {
                    throw new NullReferenceException("Command already executing without completion source");
                }
            }

            if (_taskCompletionSource != null)
            {
                return _taskCompletionSource.Task;
            }

            _taskCompletionSource = new UniTaskCompletionSource<ICommand>();
            ExecuteBasicImplementation();
            return _taskCompletionSource.Task;
        }

        private void ExecuteBasicImplementation()
        {
            SetState(CommandState.InProgress);

            try
            {
                ExecuteInternal();
            }
            catch (Exception e)
            {
                UDebug.LogError($"Exception during execution of command {this.GetType().Name}");
                UDebug.LogException(e);
                SetState(CommandState.Failure);
            }
        }

        private void SetState(CommandState state)
        {
            if (state == State)
            {
                return;
            }

            State = state;

            if (!IsFinished)
            {
                return;
            }

            try
            {
                OnExecutionComplete();
            }
            catch (Exception e)
            {
                UDebug.LogError($"Exception during setting state {state} to command {this.GetType().Name}");
                UDebug.LogException(e);
                SetState(CommandState.Failure);
            }
        }

        private void OnExecutionComplete()
        {
            _taskCompletionSource?.TrySetResult(this);
        }

        protected abstract void ExecuteInternal();

        protected virtual UniTask ExecuteInternalAsync()
        {
            return UniTask.CompletedTask;
        }

        protected void TryExecuteAsync()
        {
            try
            {
                ExecuteInternalAsync().Forget();
            }
            catch (Exception e)
            {
                UDebug.LogError($"Exception during async execution of command {this.GetType().Name}");
                UDebug.LogException(e);
                SetState(CommandState.Failure);
            }
        }

        protected void SucceedCommand() => SetState(CommandState.Success);

        protected void FailCommand() => SetState(CommandState.Failure);
    }
}
