using AutonomousVehicle.Models;
using Toolbelt.Blazor.SpeechSynthesis;

namespace AutonomousVehicle
{
    public class SystemMessagesSpeechSynthesis
    {
        private readonly SpeechSynthesis _speechSynthesis;

        public SystemMessagesSpeechSynthesis(SpeechSynthesis speechSynthesis)
        {
            _speechSynthesis = speechSynthesis;
        }

        public void SpeakSystemMessages(params SystemMessage[] systemMessages)
        {
            foreach (var systemMessage in systemMessages ?? Enumerable.Empty<SystemMessage>())
            {
                if (systemMessage.MessageCriticality == SystemMessageCriticality.Warning)
                {
                    _speechSynthesis.Speak("Warning");
                }

                _speechSynthesis.Speak(systemMessage.Message);
            }
        }
    }
}
