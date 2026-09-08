
using MySurveyBasket.Contracts.Polls;
using MySurveyBasket.Contracts.Question;

namespace MySurveyBasket.Mapping
{
    public class MappingConfigrations : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<QuestionRequest, Question>()
                  .Map(dest => dest.Answers, src => src.Answers.Select(Answer => new Answer { Content = Answer }));


            config.NewConfig<RegisterRequest, ApplicationUser>()
                .Map(dest => dest.UserName, src => src.Email);
        }

    }
}
