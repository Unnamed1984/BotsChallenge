using BotChallenge.DAL.Shared;
using BotChallenge.DAL.Shared.Models;
using BotChallenge.DAL.Relative.Repositories;
using Autofac;

namespace BotChallenge.DAL.Relative.DI
{
    public class DALRelationalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IRepository<User>>();
            builder.RegisterType<GameRepository>().As<IRepository<Game>>();
            builder.RegisterType<GameParticipantRepository>().As<IRepository<GameParticipant>>();

            base.Load(builder);
        }
    }
}
