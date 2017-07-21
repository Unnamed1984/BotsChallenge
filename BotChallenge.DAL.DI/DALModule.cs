using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using BotChallenge.DAL.Relative.DI;

namespace BotChallenge.DAL.DI
{
    class DALModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DALRelationalModule());
            base.Load(builder);
        }
    }
}
