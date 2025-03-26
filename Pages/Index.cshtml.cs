using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaperScissorsRock.Operations;

namespace PaperScissorsRock.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public ChoiceOptions PlayerChoice { get; set; }
        private ChoiceOptions computerChoice { get; set; }

        public int GameResultState { get; set; } = 3;

        public string Result { get; set; }

        public void OnPost()
        {
            computerChoice = _computerPickOption();
            GameResultState = _getGameState();
            Scores.ScoreData[GameResultState]++;
            Result = $"You chose {PlayerChoice}, the computer chose {computerChoice}. {ResultStates.ResultState[GameResultState]}";
        }

        private ChoiceOptions _computerPickOption()
        {
            Array options = Enum.GetValues(typeof(ChoiceOptions));
            Random random = new Random();
            return (ChoiceOptions)options.GetValue(random.Next(options.Length));
        }

        private int _getGameState()
        {
            if (PlayerChoice == computerChoice) return 2;

            return (PlayerChoice - computerChoice + 3) % 3 == 1 ? 1 : 0;
        }
    }
}
