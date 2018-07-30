using System;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace Setup.WPF.ViewModels
{
    public class ShellViewModel : Screen, IHandleWithTask<ClickedMessage>
    {
        private readonly IEventAggregator _events;

        public ShellViewModel()
        {
            _events = new EventAggregator();
            _events.Subscribe(this);
        }

        public string Result { get; set; }

        public void TestPublishOnUIThread()
        {
            Clear();
            AddLine("TestPublishOnUIThread:");
            AddLine("1 - Before PublishOnUIThread");
            _events.PublishOnUIThread(new ClickedMessage());
            AddLine("4 - After PublishOnUIThread");
        } 
        
        public async void TestPublishOnUIThreadAsync()
        {
            Clear();
            AddLine("TestPublishOnUIThreadAsync:");
            AddLine("1 - Before PublishOnUIThreadAsync");
            await _events.PublishOnUIThreadAsync(new ClickedMessage());
            AddLine("4 - After PublishOnUIThreadAsync");
        }

        public async Task Handle(ClickedMessage message)
        {
            AddLine("2 - Before Handle");
            await Task.Delay(2000);
            AddLine("3 - After Handle");
        }

        private void Clear()
        {
            Result = string.Empty;
            NotifyOfPropertyChange(nameof(Result));
        }

        private void AddLine(string text)
        {
            Result += text + Environment.NewLine;
            NotifyOfPropertyChange(nameof(Result));
        }
    }

    public class ClickedMessage
    {
    }
}
